Imports System.Text.RegularExpressions
Imports IconTools.IconTools
Imports System
Imports System.Threading
Imports System.Windows.Forms
Imports System.Reflection

Module WIMF_Main
    'Public Const update = 50
    Public Catalogs(0) As WIMF_Catalog
    Public ImageExtensions As ImageList
    Public loaded As Boolean = False
    Public executable_folder As String
    Public option_file As String
    Public executing_in_local As Boolean
    'Public option_use_binary As Boolean = True
    Public option_use_local_copy As String = "c:\temp"
    Public option_start_in_tray As Boolean = False
    Public option_minimize_in_tray As Boolean = False
    Public option_regexp As Boolean = False
    Public option_timeout As Integer = 5000
    Public option_unique_exp As Boolean = False
    Public option_list_ext As String = "pdf txt doc docx xls xlsx ppt pps jpeg jpg png gif bmp pptx"
    Public option_check_exists As Boolean = False
    Public option_always_visible As Boolean = False
    Public option_max_searched = 4000
    Public option_max_shown_per_update = 200
    Public new_association_ext(0) As String
    Public new_association_command(0) As String
    Public option_hotkey As String = "n"
    Public search_id As Integer = 0
    Public search_found As Integer = -1
    Public search_table(1, 5) As String
    Public search_table_color(0) As Color
    Public search_table_id As Integer
    Public search_table_done As Boolean = True
    Public option_ignore_separators As Boolean = False
    Public task_loading_catalogs_done As Boolean = True
    Public my_version_code As Integer = (10000 * My.Application.Info.Version.Major + 100 * My.Application.Info.Version.Minor + My.Application.Info.Version.Build)

    Dim fileW As System.IO.StreamWriter
    Dim fileR As System.IO.StreamReader

    Public MainForm As WIMF_UI

    Dim Task_LoadCatalog As Thread = Nothing
    Dim Task_Search As Thread = Nothing
    Dim Task_Search_Input1 As String
    Dim Task_Search_Input2 As String
    Dim Task_Search_Date As String = "01/01/78"

    Public Sub Main()

        ' Lire les options
        executing_in_local = True
        executable_folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).Substring(6)
        If Not FileIsLocal(executable_folder) Then
            executable_folder = "\\" & executable_folder
            executing_in_local = False
        End If
        option_file = executable_folder & "\WIMF_OPTIONS.txt"
        read_options()

        ' Dimensionner les tableaux
        ReDim search_table(option_max_searched, 5)
        ReDim search_table_color(option_max_searched)

        'Lire les arguments
        Console.WriteLine()
        '  Invoke this sample with an arbitrary set of command line arguments.
        Dim arguments As String() = Environment.GetCommandLineArgs()
        Dim opt As String
        Console.WriteLine("GetCommandLineArgs: {0}", String.Join(", ", arguments))

        ' Analyse des lignes de commande
        For Each opt In arguments
            Select Case opt
                Case "-version"
                    Environment.ExitCode = my_version_code
                    Exit Sub
            End Select
        Next
        ' Set a default icon for the file.
        Dim DefaultIconForFile As Icon = SystemIcons.WinLogo
        ImageExtensions = New ImageList()
        ImageExtensions.ImageSize = New Size(16, 16)
        ImageExtensions.ColorDepth = ColorDepth.Depth32Bit
        ImageExtensions.Images.Add("default", DefaultIconForFile)
        ImageExtensions.Images.Add("folder", IconTools.IconTools.GetIconForFolder(IconTools.ShellIconSize.SmallFolderIcon))

        Application.EnableVisualStyles()
        MainForm = New WIMF_UI

        MainForm.ListView1.SmallImageList = ImageExtensions

        If WIMF_Main.option_start_in_tray Then
            MainForm.WindowState = FormWindowState.Minimized
        End If

        MainForm.Show()
        MainForm.TextBox2.Select()
        Console.WriteLine("[WIMF_UI.Main] INFO : Load_All_Catalogs > start")
        Try
            Load_All_Catalogs()
        Catch exception As Exception
            Console.WriteLine("[WIMF_UI.Main] Erreur Fatale pendant Load_All_Catalogs : " & exception.Message)
            MsgBox("[WIMF_UI.Main] Erreur Fatale pendant Load_All_Catalogs : " & exception.Message)
        End Try
        Console.WriteLine("[WIMF_UI.Main] INFO : Load_All_Catalogs > end")

        Try
            Application.Run()
        Catch exception As Exception
            Console.WriteLine("[WIMF_UI.Main] Erreur Fatale pendant Run : " & exception.Message)
            MsgBox("[WIMF_UI.Main] Erreur Fatale pendant Run : " & exception.Message)
        End Try

    End Sub

    Sub Quit()
        If Not (IsNothing(Task_Search)) Then
            Task_Search.Abort()
            Task_Search = Nothing
        End If
        Application.Exit()

    End Sub


    Function read_boolean(ByRef MyReader As Microsoft.VisualBasic.FileIO.TextFieldParser, ByVal NameOption As String)
        Dim res As String
        res = MyReader.ReadLine().ToUpper
        If Left(res, 1) = "T" Then
            read_boolean = True
        ElseIf Left(res, 1) = "F" Then
            read_boolean = False
        Else
            MsgBox("Instead of boolean, I have found '" + res + "'" + vbNewLine + "for option '" + NameOption + "'", , "Error in WIMF_Module:read_boolean")
            read_boolean = False
        End If
    End Function

    Public Sub read_options()
        Dim MyReader As FileIO.TextFieldParser
        Dim line As String
        If Not FileExists(option_file) Then Exit Sub
        MyReader = New Microsoft.VisualBasic.FileIO.TextFieldParser(option_file)
        Do Until MyReader.EndOfData
            line = MyReader.ReadLine()
            If Left(line, 1) = "#" Then Continue Do
            Select Case line
                Case ":start_in_tray"
                    option_start_in_tray = read_boolean(MyReader, ":start_in_tray")
                Case ":minimize_to_tray"
                    option_minimize_in_tray = read_boolean(MyReader, ":minimize_to_tray")
                Case ":search_as_regexp"
                    option_regexp = read_boolean(MyReader, ":search_as_regexp")
                Case ":search_as_unique_exp"
                    option_unique_exp = read_boolean(MyReader, ":search_as_unique_exp")
                Case ":option_check_exists"
                    option_check_exists = read_boolean(MyReader, ":option_check_exists")
                Case ":option_always_visible"
                    option_always_visible = read_boolean(MyReader, ":option_always_visible")
                Case ":option_ignore_separators"
                    option_ignore_separators = read_boolean(MyReader, ":option_ignore_separators")
                Case ":option_use_local_copy"
                    option_use_local_copy = MyReader.ReadLine()
                Case ":option_timeout"
                    option_timeout = CUInt(MyReader.ReadLine())
                Case ":option_max_searched"
                    option_max_searched = CUInt(MyReader.ReadLine())
                Case ":option_max_shown_per_update"
                    option_max_shown_per_update = CUInt(MyReader.ReadLine())
                Case ":catalog"
                    ReDim Preserve Catalogs(UBound(Catalogs) + 1)
                    Catalogs(UBound(Catalogs)) = New WIMF_Catalog
                    Catalogs.Last.Database = MyReader.ReadLine()
                Case ":association"
                    ReDim Preserve new_association_ext(UBound(new_association_ext) + 1)
                    ReDim Preserve new_association_command(UBound(new_association_command) + 1)
                    new_association_ext(UBound(new_association_ext)) = MyReader.ReadLine()
                    new_association_command(UBound(new_association_command)) = MyReader.ReadLine()
                Case ":hotkey"
                    option_hotkey = MyReader.ReadLine()
                    'Keyboard.unregisterHotkeys(MainForm, 100)
                    'Keyboard.registerHotkey(MainForm, Asc(option_hotkey.ToUpper), Keyboard.KeyModifier.Winkey, 100)
                Case ":default_ext"
                    option_list_ext = MyReader.ReadLine()
                Case Else
                    Console.WriteLine("[read_options] Option non connue " & line)
                    ' On lit une ligne si la ligne en cours commence par ":"
                    If Left(line, 1) = ":" Then MyReader.ReadLine()
            End Select
        Loop
        MyReader.Close()
    End Sub

    Public Sub write_options()
        Dim fileW As System.IO.StreamWriter
        fileW = My.Computer.FileSystem.OpenTextFileWriter(option_file, False)
        fileW.WriteLine("[WIMF]")
        fileW.WriteLine(":start_in_tray")
        fileW.WriteLine(option_start_in_tray)
        fileW.WriteLine(":minimize_to_tray")
        fileW.WriteLine(option_minimize_in_tray)
        fileW.WriteLine(":search_as_regexp")
        fileW.WriteLine(option_regexp)
        fileW.WriteLine(":search_as_unique_exp")
        fileW.WriteLine(option_unique_exp)
        fileW.WriteLine(":option_check_exists")
        fileW.WriteLine(option_check_exists)
        fileW.WriteLine(":option_always_visible")
        fileW.WriteLine(option_always_visible)
        fileW.WriteLine(":option_always_visible")
        fileW.WriteLine(option_always_visible)
        fileW.WriteLine(":option_ignore_separators")
        fileW.WriteLine(option_ignore_separators)
        fileW.WriteLine(":option_use_local_copy")
        fileW.WriteLine(option_use_local_copy)
        fileW.WriteLine(":option_timeout")
        fileW.WriteLine(option_timeout)
        fileW.WriteLine(":option_max_searched")
        fileW.WriteLine(option_max_searched)
        fileW.WriteLine(":option_max_shown_per_update")
        fileW.WriteLine(option_max_shown_per_update)
        If UBound(Catalogs) >= 1 Then
            For i = 1 To UBound(Catalogs)
                fileW.WriteLine(":catalog")
                fileW.WriteLine(Catalogs(i).Database)
            Next
        End If
        If UBound(new_association_ext) >= 1 Then
            For i = 1 To UBound(new_association_ext)
                fileW.WriteLine(":association")
                fileW.WriteLine(new_association_ext(i))
                fileW.WriteLine(new_association_command(i))
            Next
        End If
        fileW.WriteLine(":hotkey")
        fileW.WriteLine(option_hotkey)
        fileW.WriteLine(":default_ext")
        fileW.WriteLine(option_list_ext)
        fileW.Close()
    End Sub

    Public Sub Execute(file As String, path As String, ext As String)
        Dim i As Long
        Dim j As Long
        Dim command As String
        Dim s As System.Diagnostics.ProcessStartInfo

        If file.IndexOf(" # ") > 0 Then
            file = file.Substring(0, file.IndexOf(" # "))
        End If
        If path.IndexOf(" # ") > 0 Then
            path = path.Substring(0, path.IndexOf(" # "))
        End If

        Try
            If UBound(new_association_command) >= 1 Then
                For i = 1 To UBound(new_association_command)
                    If ext = new_association_ext(i) Then
                        j = InStr(new_association_command(i), "*")
                        If j > 0 Then
                            command = Left(new_association_command(i), j - 1) & path & file & Mid(new_association_command(i), j + 1, Len(new_association_command(i)))
                        Else
                            command = new_association_command(i) & " " & path & file
                        End If
                        Shell(command)
                        Exit Sub
                    End If
                Next
            End If
            'System.Diagnostics.Process.Start(file)
            Dim p As New System.Diagnostics.Process
            If path.ToLower.StartsWith("http") Then
                s = New System.Diagnostics.ProcessStartInfo(path)
            Else
                s = New System.Diagnostics.ProcessStartInfo(path & file)
            End If
            s.UseShellExecute = True
            s.WindowStyle = ProcessWindowStyle.Normal
            p.StartInfo = s
            p.Start()
        Catch ex As Exception
            MsgBox("Je ne sais pas ouvrir ce type de fichier")
        End Try
    End Sub

    Public Sub Load_All_Catalogs()
        Task_LoadCatalog = New Thread(AddressOf Load_All_Catalogs_thread)
        Task_LoadCatalog.Start()
    End Sub

    Public Sub Load_All_Catalogs_thread()
        Dim i As Long
        Dim Stopwatch As New Stopwatch()
        task_loading_catalogs_done = False
        MainForm.Invoke(MainForm.Change_Status, New Object() {"Lecture des bases ..."})
        If UBound(Catalogs) < 1 Then Exit Sub

        Console.WriteLine("Testing load catalogs...")
        Stopwatch.Reset()
        Stopwatch.Start()
        For i = 1 To UBound(Catalogs)
            Catalogs(i).Load()
        Next
        Stopwatch.Stop()
        Console.WriteLine("Testing load catalogs took {0}ms.", Stopwatch.Elapsed.TotalMilliseconds)
        MainForm.Invoke(MainForm.Clear_Delegate)
        task_loading_catalogs_done = True
    End Sub




    Public Sub Delete_Catalog(name As String)
        Dim i As Long

        For i = UBound(Catalogs) To 1 Step -1
            If Catalogs(i).Name = name Then

                ' Suprresion de la base
                Call DeleteArrayItem(Catalogs, i)
                ReDim Preserve Catalogs(UBound(Catalogs) - 1)

                ' Mise à jour des options
                write_options()
                ' Clear
                Clear()
                Exit Sub

            End If

        Next

    End Sub


    Public Sub Clear()
        Dim i As Long
        Dim c As Long
        i = 0
        For c = 1 To UBound(Catalogs)
            i = i + Catalogs(c).File_Count
        Next
        MainForm.ListView1.Items().Clear()
        MainForm.ToolStripStatusLabel1.Text = i & " fichiers"
    End Sub

    Sub Search()
        Debug.WriteLine("<Sub Search>")

        ' Si recherche des catalogues alors attente de la fin du thread
        If Not (task_loading_catalogs_done) Then
            MainForm.TimerBeginSearch.Stop()
            MainForm.TimerBeginSearch.Start()
            MainForm.TimerBeginSearch.Enabled = True
            Debug.WriteLine("Sub Search : Les catalogues sont en cours de chargement => lancement du timer pour relancer la recherche")
            Exit Sub
        End If

        ' Si la recherche n'a pas changé, ne rien faire
        If Task_Search_Input1 = MainForm.TextBox1.Text And Task_Search_Input2 = MainForm.TextBox2.Text And Task_Search_Date = MainForm.ComboBox1.Tag Then
            Debug.WriteLine("Sub Search : Pas de nouvelle recherche")
            Exit Sub
        End If

        ' Change l'ID => Cet Id permet de valider les ajouts
        search_id = search_id + 1
        If Not (IsNothing(Task_Search)) Then
            Task_Search.Abort()
            Task_Search.Join()
            Task_Search = Nothing
        End If

        ' On efface tout
        Clear()
        Task_Search_Input1 = MainForm.TextBox1.Text
        Task_Search_Input2 = MainForm.TextBox2.Text
        Task_Search_Date = MainForm.ComboBox1.Tag

        ' On lance une nouvelle recherche
        MainForm.Invoke(MainForm.Change_Status, New Object() {"Recherche en cours ..."})
        search_table_done = False
        search_found = -1
        Task_Search = New Thread(AddressOf Search_threaded)
        Task_Search.Start()
    End Sub

    Sub Search_threaded()
        Dim c As Long
        Dim i As Long
        Dim j As Long
        Dim folder As String
        Dim keep As Boolean
        Dim error_regexp As Boolean
        Dim patterns() As String
        Dim patterns_ext() As String
        Dim reg_patterns() As Regex
        Dim reg_patterns_ext() As Regex
        Dim pattern As String
        Dim file_name As String
        Dim file_ext As String
        Dim catalogue As WIMF_Catalog
        Dim search_byname As Boolean
        Dim search_byext As Boolean
        Dim last_parent_id As Integer
        Dim last_parent_name As String
        Dim IconForFile As Icon = SystemIcons.WinLogo
        Dim FullFileName As String
        Dim search_id_begin As Integer
        Dim search_pattern_filename As String
        Dim search_pattern_ext As String
        Dim search_date As Date
        Dim do_search_date As Boolean

        search_found = -1
        search_id_begin = search_id
        search_table_id = search_id
        last_parent_id = -1
        last_parent_name = "error ??"
        search_date = Date.Parse(MainForm.ComboBox1.Tag)
        do_search_date = search_date >= #01/01/1979#

        search_pattern_filename = Trim(Task_Search_Input2) '.ToLower())
        search_pattern_ext = Trim(Task_Search_Input1) '.ToLower())

        ' Supression des separateurs
        If option_ignore_separators And Not option_regexp Then
            search_pattern_filename = Replace(search_pattern_filename, "-", "")
            search_pattern_filename = Replace(search_pattern_filename, "_", "")
        End If

        If option_unique_exp Then
            patterns = {search_pattern_filename}
            patterns_ext = {search_pattern_ext}
        Else
            patterns = Split(search_pattern_filename)
            patterns_ext = Split(search_pattern_ext)
        End If

        'Convertit les cahines de caractgeères en regexp
        If option_regexp Then
            error_regexp = False
            reg_patterns = {}
            reg_patterns_ext = {}
            Array.Resize(reg_patterns, patterns.Length)
            Array.Resize(reg_patterns_ext, patterns_ext.Length)
            Try
                For i = 0 To UBound(patterns)
                    reg_patterns(i) = New Regex(patterns(i))
                Next
                If MainForm.TextBox1.ForeColor = Color.Red Then
                    MainForm.TextBox1.ForeColor = Color.Black
                End If
            Catch ex As Exception
                MainForm.TextBox1.ForeColor = Color.Red
                error_regexp = True
            End Try
            Try
                For i = 0 To UBound(patterns_ext)
                    reg_patterns_ext(i) = New Regex(patterns_ext(i))
                Next
                If MainForm.TextBox2.ForeColor = Color.Red Then
                    MainForm.TextBox2.ForeColor = Color.Black
                End If
            Catch ex As Exception
                MainForm.TextBox2.ForeColor = Color.Red
                error_regexp = True
            End Try
            If error_regexp Then
                Exit Sub
            End If
        End If

        search_byname = Task_Search_Input2.Length > 0
        search_byext = Task_Search_Input1.Length > 0

        If Task_Search_Input1.Length <= 0 And Task_Search_Input2.Length <= 0 And Not do_search_date Then Exit Sub
        If UBound(Catalogs) <= 0 Then Exit Sub

        For c = 1 To UBound(Catalogs)
            catalogue = Catalogs(c)

            For i = 1 To catalogue.File_Count
                ' Si le search_id a changé, on sort du Search
                If search_id_begin <> search_id Then
                    MainForm.Invoke(MainForm.Change_Status, New Object() {""})
                    Exit Sub
                End If

                If (search_found + 1) >= option_max_searched Then
                    MainForm.Invoke(MainForm.Change_Status, New Object() {"Plus de " & option_max_searched & " fichiers trouvés ..."})
                    Exit Sub
                End If

                file_name = catalogue.Files(i).Name 'LowerName '.Name.ToLower()
                file_ext = catalogue.Files(i).Ext '.ToLower()

                If do_search_date Then
                    If catalogue.Files(i).DateDoc < search_date Then
                        Continue For
                    End If
                End If

                If search_byext Then
                    keep = False
                    For Each pattern In patterns_ext
                        If option_regexp Then
                            keep = Regex.Match(file_ext, pattern).Success
                            Exit For
                        Else
                            keep = file_ext.Contains(pattern)
                            Exit For
                        End If
                    Next
                    If keep = False Then
                        Continue For
                    End If
                End If

                If search_byname Then
                    ' Supression des separateurs
                    If option_ignore_separators Then
                        file_name = Replace(file_name, "-", "")
                        file_name = Replace(file_name, "_", "")
                        file_name = Replace(file_name, " ", "")
                        If file_name Is Nothing Then GoTo next_file
                    End If

                    If option_regexp Then
                        For Each pattern In patterns
                            If Not Regex.Match(file_name, pattern).Success Then
                                GoTo next_file
                            End If
                        Next
                    Else
                        For Each pattern In patterns
                            If file_name.IndexOf(pattern, StringComparison.OrdinalIgnoreCase) < 0 Then
                                GoTo next_file
                            End If
                        Next
                    End If
                End If

                search_found = search_found + 1
                search_table(search_found, 1) = catalogue.Files(i).Name
                search_table(search_found, 2) = catalogue.Files(i).Ext
                search_table(search_found, 3) = Format(Math.Ceiling(catalogue.Files(i).Size / 1024), "0 Ko")
                If last_parent_id <> catalogue.Files(i).Parent Then
                    folder = ""
                    j = i
                    While (catalogue.Files(j).Parent > 0)
                        j = catalogue.Files(j).Parent
                        folder = catalogue.Files(j).Name & "\" & folder
                    End While
                    last_parent_id = catalogue.Files(i).Parent
                    last_parent_name = catalogue.Catalog_Path & "\" & folder
                End If
                search_table(search_found, 4) = last_parent_name
                search_table(search_found, 5) = catalogue.Files(i).DateDoc
                If search_table(search_found, 5) = WIMF_Catalog.NoDate Then
                    search_table(search_found, 5) = ""
                End If

                search_table_color(search_found) = Nothing

                If option_check_exists Then
                    FullFileName = last_parent_name & "\" & catalogue.Files(i).Name
                    If FullFileName.IndexOf(" # ") > 0 Then
                        FullFileName = FullFileName.Substring(0, FullFileName.IndexOf(" # "))
                    End If
                    If catalogue.Files(i).Ext = "folder" Then
                        If Not Directory.Exists(FullFileName) Then
                            search_table_color(search_found) = Color.Gray
                        End If
                    Else
                        If Not FileExists(FullFileName) Then
                            search_table_color(search_found) = Color.Gray
                        End If
                    End If
                End If
next_file:  Next
        Next
        MainForm.Invoke(MainForm.Change_Status, New Object() {(search_found + 1) & " fichiers trouvés"})
        search_table_done = True
    End Sub


End Module

