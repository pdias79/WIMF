#!/usr/bin/python3
import os
import sys
import threading
import time
import codecs
import gzip
import shutil

from dataclasses import dataclass, field
from threading import Thread, Lock
from typing import Tuple, List, Set, Union, ClassVar
import time

#sys.stdout = open('out.txt', 'w')
#sys.stderr = open('err.txt', 'w')

@dataclass
class Folder_Explorer:
    max_threads: int = 50
    _sleep: float = 0.01
    _sleep_lecture: float = 0.001
    _folders: list[tuple[int, str]] = field(default_factory=list)
    _threads: list[Thread] = field(default_factory=list)
    _stop: bool = False
    _results: dict[str, list] = field(default_factory=dict)
    _lock_folders: Lock = field(default_factory=Lock)
    _lock_results: Lock = field(default_factory=Lock)
    _tmp_folders: list[str] = field(default_factory=list)

    def __post_init__(self):
        for _ in range(self.max_threads):
            t = Thread(target=self.thread_job)
            t.start()
            self._threads.append(t)

    def stop(self):
        try:
            self._lock_folders.acquire()
            self._stop = True
            self._results.clear()
            self._folders.clear()
        finally:
            self._lock_folders.release()
        for _ in self._threads:
            _.join()
        self._threads.clear()

    def prepare(self, path: str):
        self._tmp_folders.append(path)

    def get(self, path: str) -> list:
        while True:
            try:
                return self._results.pop(path)
            except:
                time.sleep(self._sleep_lecture)


    def thread_job(self):
        while True:
            if self._stop:
                break
            if not self._folders:
                if self._tmp_folders:
                    self._lock_folders.acquire()
                    try:
                        while self._tmp_folders:
                            self._folders.append(self._tmp_folders.pop(0))
                    finally:
                        self._lock_folders.release()
                else:
                    time.sleep(self._sleep)
                    continue
            # Cherche le prochain répertoire
            self._lock_folders.acquire()
            try:
                folder = self._folders.pop(0)
            finally:
                self._lock_folders.release()
            # Fait le scandir
            try:
                r = list(os.scandir(folder))
            except Exception as e:
                r = []
            self._results[folder] = r



@dataclass
class BaseWIMF:
    nom: str
    répertoire: str
    chemin: str
    extensions: Union[str, Set[str]] = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp"
    copies: List[str] = field(default_factory=list)
    exclude: List[str] = field(default_factory=list)
    bypass_si_moins_x_jours: float = 0.5

    def __post_init__(self):
        if isinstance(self.extensions, str):
            self.extensions = set(self.extensions.split(" "))
        if isinstance(self.exclude, str):
            self.exclude = {self.exclude}

    def update(self, force: bool = False, threaded: bool = False):
        bypass = False
        if os.path.exists(self.chemin):
            if abs(os.stat(self.chemin).st_mtime - time.time()) < self.bypass_si_moins_x_jours*24*60*60:  # Si MAJ de moins de 6h on ignore l'indexation
                bypass = True
        if force:
            bypass = False
        if not bypass:
            if os.path.exists(self.répertoire):
                self.update_base_only(threaded=threaded)
            else:
                print(f"Bypass de l'update de la base {self.nom} (répertoire non accessible: {self.répertoire})")
        else:
            print(f"Bypass de l'update de la base {self.nom} (maj < {self.bypass_si_moins_x_jours} jours)")
        self.make_copies()

    def update_base_only(self, threaded: bool = False):
        t0 = time.time()
        exclude = [e.lower() for e in self.exclude]
        detail={}
        detail_order=[]
        keep_folder = {self.répertoire:True}
        print(f"*************************************************************")
        print(f"UPDATE de la base {self.nom}")
        print(f"*************************************************************")

        def save_base_partial(partial: bool = True):
            # Open a file
            vitesse = len(detail)//(1+abs(int((time.time()-t0))))
            print(f" * SAUVEGARDE {'PARTIELLE' if partial else ''} (nombre de fichiers indexés = {len(detail)}, {vitesse} fichiers/sec)")
            with codecs.open(self.chemin+".tmp", "w", "utf-8") as db_io:
                db_io.write("[WIMF Catalog]\n")
                db_io.write(f"{self.nom}\n".format(self.nom))
                db_io.write(f"{self.répertoire}\n".format(self.répertoire))
                db_io.write(f"{int(time.mktime(time.localtime()))}\n")
                db_io.write(f"{" ".join(sorted(self.extensions))}\n")
                # Write all files
                current_number = 0
                for fullname in detail_order:
                    current_file = detail[fullname]
                    if current_file[1] == "folder":
                        if not keep_folder[fullname]:
                            continue
                    current_number  = current_number + 1
                    current_file[4] = current_number
                    parent_fullname = current_file[3]
                    if parent_fullname == self.répertoire:
                        current_file[5] = 0
                    else:
                        current_file[5] = detail[parent_fullname][4]
                    db_io.write(f"{current_file[0]};{current_file[1]};{current_file[2]};{current_file[5]};{current_file[6]}\n")

        def add_elem(fullname, filename, extension, size, folder, date):
            #print("%s : %s" %(len(detail), fullname))
            detail[fullname] = [filename, extension, size, folder, -1, -1, date] # Current number / Parent number
            detail_order.append(fullname)
            if len(detail) % 10000 == 0:
                save_base_partial()


        def list_folder_rec_no_threaded(folder):
            try:
                for f in os.scandir(folder):
                    if (';' in f.name) or ('~' in f.name) or ('$' in f.name) or ('{' in f.name) or ('}' in f.name):
                        continue
                    if len(exclude) > 0:
                        fullnamel = f.path.lower()
                        skip = False
                        for ex in exclude:
                            if ex in fullnamel: 
                                skip = True
                                continue
                        if skip:
                            print("SKIP : ", f.path)
                            continue
                    if f.is_dir():
                        keep_folder[f.path] = False # Par défaut on ne conserve pas un répertoire (on change s'il y a un contenu à retenir)
                        stat = f.stat()
                        mtime = int(stat.st_mtime/(24*3600))
                        add_elem(f.path, f.name, "folder", 0, folder, mtime)
                        list_folder_rec_no_threaded(f.path)
                    else:
                        basename, extension = os.path.splitext(f.path)
                        if extension:
                            if extension.startswith("."):
                                extension = extension[1:]
                            if extension.lower() in self.extensions:
                                stat = f.stat()
                                mtime = int(stat.st_mtime/(24*3600))
                                add_elem(f.path, f.name, extension, stat.st_size, folder, mtime)
                                parent = folder
                                while not keep_folder[parent]:
                                    keep_folder[parent] = True
                                    parent = detail[parent][3]
            except Exception as e:
                print("Unexpected error:", sys.exc_info()[0])
                print(" - erreur  : ", e)
                print(" - folder  : ", folder)
                try:
                    # Si on a pas pu parcourrir le réperotire, f n'est pas initialisée
                    print(" - fichier : ", f.name)
                except Exception:
                    pass
                pass

        def list_folder_rec_threaded(initial_folder):
            explorer = Folder_Explorer()
            explorer.prepare(path=initial_folder)
            folders_to_explore = [initial_folder]
            while folders_to_explore:
                folder = folders_to_explore.pop(0)
                try:
                    for f in explorer.get(folder):
                        if (';' in f.name) or ('~' in f.name) or ('$' in f.name) or ('{' in f.name) or ('}' in f.name):
                            continue
                        if len(exclude) > 0:
                            fullnamel = f.path.lower()
                            skip = False
                            for ex in exclude:
                                if ex in fullnamel:
                                    skip = True
                                    continue
                            if skip:
                                print("SKIP : ", f.path)
                                continue
                        if f.is_dir():
                            keep_folder[f.path] = False # Par défaut on ne conserve pas un répertoire (on change s'il y a un contenu à retenir)
                            stat = f.stat()
                            mtime = int(stat.st_mtime/(24*3600))
                            explorer.prepare(path=f.path)
                            folders_to_explore.append(f.path)
                            add_elem(f.path, f.name, "folder", 0, folder, mtime)
                        else:
                            basename, extension = os.path.splitext(f.path)
                            if extension:
                                if extension.startswith("."):
                                    extension = extension[1:]
                                if extension.lower() in self.extensions:
                                    stat = f.stat()
                                    mtime = int(stat.st_mtime/(24*3600))
                                    add_elem(f.path, f.name, extension, stat.st_size, folder, mtime)
                                    parent = folder
                                    while(keep_folder[parent] == False):
                                        keep_folder[parent] = True
                                        parent = detail[parent][3]
                except Exception as e:
                    print("Unexpected error:", sys.exc_info()[0])
                    print(" - erreur  : ", e)
                    print(" - folder  : ", folder)
                    try:
                        # Si on a pas pu parcourrir le réperotire, f n'est pas initialisée
                        print(" - fichier : ", f.name)
                    except Exception:
                        pass
                    pass
            explorer.stop()

        # On liste
        if threaded:
            list_folder_rec_threaded(self.répertoire)
        else:
            list_folder_rec_no_threaded(self.répertoire)

        # On sauvegarde
        save_base_partial(partial=False)
        # Rename
        if os.path.isfile(self.chemin):
            os.remove(self.chemin)
        os.rename(self.chemin+".tmp", self.chemin)
        #Create gz file
        with open(self.chemin, 'rb') as f_in:
            with gzip.open(self.chemin+".gz", 'wb') as f_out:
                shutil.copyfileobj(f_in, f_out)

    def make_copies(self):
        chemin = self.chemin+".gz"
        for path in self.copies:
            if not os.path.exists(path):
                print(f"Le répertoire '{path}' est inexistant")
                continue
            if not os.path.isdir(path):
                print(f"Le chemin '{path}' n'est pas un répertoire")
                continue
            try:
                dest = os.path.join(path, os.path.basename(chemin))
                if os.path.exists(dest):
                    if os.stat(dest).st_mtime < os.stat(chemin).st_mtime:
                        os.remove(dest)
                if not os.path.exists(dest):
                    print(f"Copie de '{chemin}' vers '{dest}'")
                    shutil.copy(chemin, dest)
            except Exception as e:
                print("Unexpected error:", sys.exc_info()[0])
                print(" - erreur  : ", e)
                pass

if __name__ == "__main__":

    SCRIPT_FOLDER = os.path.dirname(os.path.realpath(__file__))
    PERSONAL_FOLDER = fr"C:\Users\{os.getlogin()}"
    DATABASE_FOLDER = fr"{SCRIPT_FOLDER}\DATABASES"
    THREADED = True

    os.makedirs(DATABASE_FOLDER, exist_ok=True)
    assert os.path.exists(DATABASE_FOLDER)

    os.chdir(SCRIPT_FOLDER)

    espace_NA = r"\\atlas\CO\disc-dt\05-PRC.105\05-NA.005\Truc_utile\WIMF"
    espace_PR = r"\\atlas\CO\disc-dt\05-PRC.105\99-COMMUN.099\14- INNOVATIONS\02- Mise a disposition d'outils\02 - WIMF"
    espace_PPC_HPC = r"\\atlas\CO\disc-dt\02-PPC.102\60-EPR-HPC.060\WIMF"
    espace_PPC_FA3 = r"\\atlas.edf.fr\CO\disc-dt\02-PPC.102\58-EPR.058\WIMF"
    espace_PPC_VD = r"\\atlas.edf.fr\CO\disc-dt\02-PPC.102\55-REEXAMENS-VD.055\8_Divers\WIMF"

    #espace_TC = r"\\atlas\CO\disc-dt\05-PRC.105\06-TC.006\12. Truc_utile\WIMF"
    espaces_communs_avec_PPC = [espace_NA, espace_PR, espace_PPC_HPC, espace_PPC_FA3, espace_PPC_VD]
    espaces_communs = [espace_NA, espace_PR]

    Documents = BaseWIMF(
        nom = "Documents",
        répertoire= fr"{PERSONAL_FOLDER}\Documents",
        chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_Documents.txt",
        extensions = "pdf txt doc docx xls xlsx ppt pptx pps",
        exclude = ["Gratuiciels", "vlc"],
        bypass_si_moins_x_jours = 0, #0.1,
    )
    Documents.update(threaded=False)

    Documents = BaseWIMF(
        nom = "Disque",
        répertoire= fr"C:\\",
        chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_Disque.txt",
        extensions = "pdf txt doc docx xls xlsx ppt pptx pps",
        exclude = [],
        bypass_si_moins_x_jours = 0, #0.1,
    )
    Documents.update(threaded=False)


    Bureau = BaseWIMF(
        nom = "Bureau",
        répertoire= fr"{PERSONAL_FOLDER}\Desktop",
        chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_Bureau.txt",
        extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
        exclude = [],
        bypass_si_moins_x_jours = 0, #0.1,
    )
    Bureau.update(threaded=False)

    #
    #
    # VD = BaseWIMF(
    #     nom = "VD",
    #     répertoire= r"\\atlas\CO\disc-dt\02-PPC.102\55-REEXAMENS-VD.055",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_VD.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs_avec_PPC,
    #     bypass_si_moins_x_jours = 2,
    # )
    # VD.update(threaded=THREADED)
    #
    # DA = BaseWIMF(
    #     nom = "DA",
    #     répertoire= r"\\atlas.edf.fr\CO\disc-dt\02-PPC.102\56-DE-DA-INTER-VD.056",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_DA.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs_avec_PPC,
    #     bypass_si_moins_x_jours = 3,
    # )
    # DA.update(threaded=THREADED)
    #
    # HPC = BaseWIMF(
    #     nom = "HPC",
    #     répertoire= r"\\atlas\CO\disc-dt\02-PPC.102\60-EPR-HPC.060",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_HPC.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs_avec_PPC,
    #     bypass_si_moins_x_jours = 15,
    # )
    # HPC.update(threaded=THREADED)
    #
    #
    # FA3 = BaseWIMF(
    #     nom = "FA3",
    #     répertoire= r"\\atlas\CO\disc-dt\02-PPC.102\58-EPR.058",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_FA3.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs_avec_PPC,
    #     bypass_si_moins_x_jours = 3,
    # )
    # FA3.update(threaded=THREADED)
    #
    # EPR2 = BaseWIMF(
    #     nom = "EPR2",
    #     répertoire= r"\\atlas\CO\disc-dt\02-PPC.102\08-EPR2.008",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_EPR2.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs_avec_PPC,
    #     bypass_si_moins_x_jours = 3,
    # )
    # EPR2.update(threaded=THREADED)
    #
    # COYOTE = BaseWIMF(
    #     nom = "COYOTE",
    #     répertoire= r"\\smb.mnemo.edf.fr\RD\work\projets.001\coyote.089",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_COYOTE.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [],
    #     copies = espaces_communs,
    #     bypass_si_moins_x_jours = 7,
    # )
    # COYOTE.update(threaded=THREADED)
    #
    # PR =  BaseWIMF(
    #     nom = "PR",
    #     répertoire= r"\\atlas\CO\disc-dt\05-PRC.105",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_PR.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = [
    #         "AbaquesH3",
    #     ],
    #     copies = espaces_communs,
    #     bypass_si_moins_x_jours = 3,
    # )
    # PR.update(threaded=THREADED)

    # MNEMO = BaseWIMF(
    #     nom = "MNEMO",
    #     répertoire= r"\\smb.mnemo.edf.fr\IN\work\dt_prc.007",
    #     chemin = fr"{DATABASE_FOLDER}\WIMF_DATABASE_MNEMO.txt",
    #     extensions = "pdf txt doc docx xls xlsx ppt pptx pps jpeg jpg png gif bmp",
    #     exclude = ["init", "evol", "GSR"],
    #     copies = espaces_communs,
    #     bypass_si_moins_x_jours = 30,
    # )
    # MNEMO.update(threaded=THREADED)





