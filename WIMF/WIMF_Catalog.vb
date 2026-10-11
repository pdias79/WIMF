Imports System.IO.File
Imports System.IO.Compression
Imports System.IO.Compression.CompressionMode
Imports System.Threading.Thread
Imports System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

Public Class WIMF_Catalog

    Public Name As String  ' Nom de la base
    Public Database As String  ' Fichier contenant la base
    Public Catalog_Path As String  ' Répertoire qui a été catalogué
    Public Last_Update As DateTime        ' Dernière update
    Public List_ext As String
    Public Extensions() As String
    Public IsLocal As Boolean
    Public LoadedOldCopie As Integer = -1
    Public Const LoadedOldCopie_Local_No_update_needed = 1
    Public Const LoadedOldCopie_Local_Updated = 2
    Public Const LoadedOldCopie_Network_Not_found = 3

    Public Const NoDate As Date = #01/01/1979#

    <Serializable()> Public Structure FileInfo
        Public Ext As String
        Public Name As String
        Public Size As UInteger
        Public Parent As UInteger
        Public DateDoc As Date
    End Structure

    Public Files() As FileInfo
    Public File_Count As Long

    Public Sub CALL_Update_Status(message As String)
        Static callparam As Object() = {""}
        callparam(0) = message
        WIMF_Main.MainForm.Invoke(WIMF_Main.MainForm.Change_Status, callparam)
    End Sub

    Public Sub CALL_Update_ProgressBar(current As Integer, Optional max As Integer = -1)
        Static callparam As Object() = {True, 0, 0}
        callparam(1) = current
        If max >= 0 Then
            callparam(2) = max
        End If
        WIMF_Main.MainForm.Invoke(WIMF_Main.MainForm.Update_ProgressBar_Delegate, callparam)
    End Sub


    Function GetBaseOrLocal() As String
        Dim backupname As String
        Dim FileName As String
        'Dim Current_date As Date
        'Dim Backup_date As Date

        FileName = Database

        Console.WriteLine("[WIMF_Main.GetBaseOrLocal] Localisation de la base = {0}", FileName)

        If Not FileExists(FileName) Then
            Dim FileName_local As String
            FileName_local = WIMF_Main.executable_folder & "\" & FileName
            Console.WriteLine("[WIMF_Main.GetBaseOrLocal] Recherche dans local = {0}", WIMF_Main.executable_folder)
            If FileExists(FileName) Then
                FileName = FileName_local
                Console.WriteLine("[WIMF_Main.GetBaseOrLocal] Trouvé dans = {0}", FileName)
            End If
        End If

        If Not FileIsLocal(FileName) Then
            If WIMF_Main.option_use_local_copy <> "" Then
                If DirectoryExists(WIMF_Main.option_use_local_copy) Then
                    backupname = WIMF_Main.option_use_local_copy & "\temp_" & FileShortName(FileName)
                    ' Si le fichier de base n'est pas accessible alors taper dans le backup
                    If FileExists(FileName) Then
                        If Not FileExists(backupname) Then
                            Console.WriteLine("### Copie locale")
                            Try
                                File.Copy(FileName, backupname, True)
                            Catch ex As Exception
                                MsgBox("Cannot copy " & FileName & " to " & backupname)
                            End Try
                            LoadedOldCopie = LoadedOldCopie_Local_Updated
                        Else
                            'Current_date = File.GetLastWriteTime(FileName)
                            'Backup_date = File.GetLastWriteTime(backupname)
                            If File.GetLastWriteTime(FileName) > File.GetLastWriteTime(backupname) Then
                                Console.WriteLine("### Mise à jour de la copie locale")
                                Try
                                    File.Copy(FileName, backupname, True)
                                Catch ex As Exception
                                    MsgBox("Cannot update " & FileName & " to " & backupname)
                                End Try
                                LoadedOldCopie = LoadedOldCopie_Local_Updated
                            Else
                                Console.WriteLine("### Copie locale à jour")
                                LoadedOldCopie = LoadedOldCopie_Local_No_update_needed
                            End If
                        End If
                    Else
                        Console.WriteLine("### Base non accessible")
                        LoadedOldCopie = LoadedOldCopie_Network_Not_found
                    End If
                    FileName = backupname
                End If
            End If
        End If


        If Not FileExists(FileName) Then
            FileName = ""
        End If

        Console.WriteLine("### Utilisation du fichier = {0}", FileName)

        GetBaseOrLocal = FileName
    End Function

    Function GetReader(Optional FileName As String = "") As StreamReader
        Dim FileReader As System.IO.FileStream
        Dim Archive As GZipStream

        If FileName = "" Then
            FileName = Database
        End If


        Try
            FileReader = System.IO.File.Open(FileName, FileMode.Open)
            If Len(FileName) > 3 Then
                If Right(FileName, 3).ToUpper = ".GZ" Then
                    Archive = New GZipStream(FileReader, CompressionMode.Decompress)
                    GetReader = New StreamReader(Archive)
                    Exit Function
                End If
            End If
            GetReader = New StreamReader(FileReader)
        Catch ex As Exception
            GetReader = StreamReader.Null
        End Try

    End Function


    ' CHARGE UNE BASE
    Public Sub Load()
        Dim reader As System.IO.StreamReader
        Dim localbase As String
        Dim ligne_nom_fichier As Long
        Dim ligne_nom_premier_fichier As Long
        Const premiere_ligne_fichier As ULong = 6
        Const nombre_lignes_pour_un_fichier As ULong = 5
        Dim File_Count_CountDown As Integer = 0
        Dim File_Count_CountDown_Max As Integer
        Dim ligne As String
        Dim number_of_files As Long = 0
        Dim sep1, sep2, sep3, sep4 As Integer

        CALL_Update_Status("Lecture de la base [" & Database & "]")
        localbase = GetBaseOrLocal()
        CALL_Update_Status("Lecture de la base [ local : " & localbase & "]")
        CALL_Update_ProgressBar(0, 100)

        IsLocal = FileIsLocal(Database)

        ' D'abord lit le fichier en entier pour identifier le nombre de fichiers

        reader = GetReader(localbase)

        'Lecture de [WIMF]
        reader.ReadLine()
        Name = reader.ReadLine()
        Catalog_Path = reader.ReadLine()
        Last_Update = DateConversion.FromUnix(CLng(reader.ReadLine()))
        List_ext = reader.ReadLine().ToLower
        While reader.ReadLine() IsNot Nothing
            number_of_files += 1
        End While

        reader.Close()

        ' Ensuite remplit la base

        reader = GetReader(localbase)
        For k = 1 To premiere_ligne_fichier - 1
            ligne = reader.ReadLine()
        Next

        ReDim Files(number_of_files)
        File_Count_CountDown_Max = CInt(number_of_files / 50)

        CALL_Update_Status("Lecture de la base " & Name)
        CALL_Update_ProgressBar(0, number_of_files)

        ligne_nom_premier_fichier = 5
        ligne_nom_fichier = ligne_nom_premier_fichier
        File_Count = 0
        Do
            ligne = reader.ReadLine()
            If ligne Is Nothing Then Exit Do

            sep1 = ligne.IndexOf(";")
            sep2 = ligne.IndexOf(";", sep1 + 1)
            sep3 = ligne.IndexOf(";", sep2 + 1)
            sep4 = ligne.IndexOf(";", sep3 + 1)
            File_Count += 1
            Files(File_Count).Name = ligne.Substring(0, sep1)
            Files(File_Count).Ext = ligne.Substring(sep1 + 1, sep2 - sep1 - 1)
            Try
                Files(File_Count).Size = CUInt(ligne.Substring(sep2 + 1, sep3 - sep2 - 1))
                Files(File_Count).Parent = CUInt(ligne.Substring(sep3 + 1, sep4 - sep3 - 1))
                Files(File_Count).DateDoc = DateConversion.FromUnix(3600 * 24 * CULng(ligne.Substring(sep4 + 1)))
            Catch
                Files(File_Count).Size = 0
                Files(File_Count).Parent = 0
                Files(File_Count).DateDoc = NoDate
            End Try
            ligne_nom_fichier += nombre_lignes_pour_un_fichier
            If File_Count_CountDown <= 0 Then
                CALL_Update_ProgressBar(File_Count)
                File_Count_CountDown = File_Count_CountDown_Max
            End If
            File_Count_CountDown -= 1

        Loop

        reader.Close()
        CALL_Update_ProgressBar(0, 100)

    End Sub


End Class
