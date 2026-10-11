Imports System.Threading.Thread

Public Class WIMF_UI
    Delegate Sub Basic_Sub()
    Delegate Sub Change_Status_Function(S As String)
    Delegate Sub Update_ProgressBar_Function(show As Boolean, value As Long, maximum As Long)
    Public Change_Status As Change_Status_Function
    Public Update_ListView_Delegate As Basic_Sub
    Public Update_ProgressBar_Delegate As Update_ProgressBar_Function
    Public Clear_Delegate As Basic_Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DémarrerDansLeSystrayToolStripMenuItem.Checked = option_start_in_tray
        MinimiserDansLeSystrayToolStripMenuItem.Checked = option_minimize_in_tray
        TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Checked = option_regexp
        TraiterCommeUneUniqueChaineToolStripMenuItem.Checked = option_unique_exp
        VérifierSiLeFichierExisteToolStripMenuItem.Checked = option_check_exists
        ToujoursVisibleToolStripMenuItem.Checked = option_always_visible
        IgnorerLesSéparateursToolStripMenuItem.Checked = option_ignore_separators
        Me.TopMost = option_always_visible
        If option_start_in_tray Then
            NotifyIcon1.Visible = True
            Me.Visible = False
            Me.WindowState = FormWindowState.Minimized
            'Me.Hide()
            'Me.ShowInTaskbar = False
        End If
        Change_Status = New Change_Status_Function(AddressOf Change_Status_Method)
        Update_ListView_Delegate = New Basic_Sub(AddressOf Update_ListView)
        Clear_Delegate = New Basic_Sub(AddressOf Clear)
        Update_ProgressBar_Delegate = New Update_ProgressBar_Function(AddressOf Update_ProgressBar)
        TimerListView.Enabled = True
        Me.ComboBox1.SelectedIndex = 0
    End Sub


    Private Sub Form1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        If Me.WindowState = FormWindowState.Minimized Then
            If option_minimize_in_tray Then
                NotifyIcon1.Visible = True
                Me.Visible = False
            End If
        Else
            With Me.ListView1
                .Columns.Item(3).Width = .Width - .Columns.Item(0).Width - .Columns.Item(1).Width - .Columns.Item(2).Width - 21
            End With
        End If
    End Sub


    Private Sub CatalogMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CatalogMenuItem.Click
        WIMF_UI_Catalogs.Show()
    End Sub


    Private Sub AProposToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AProposToolStripMenuItem.Click
        WIMF_about.Show()
    End Sub

    Private Sub ListView1_ColumnClick(ByVal sender As Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles ListView1.ColumnClick
        SortMyListView.SortMyListView(Me.ListView1, e.Column, , True)
    End Sub


    Private Sub ListView1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ListView1.KeyPress
        If (e.KeyChar = Convert.ToChar(13)) Then
            If (ListView1.SelectedItems.Count > 0) Then
                Execute(ListView1.SelectedItems.Item(0).Text, ListView1.SelectedItems.Item(0).SubItems(3).Text, ListView1.SelectedItems.Item(0).SubItems(1).Text)
            End If
            e.Handled = True
        End If
    End Sub

    Private Sub ListView1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles ListView1.MouseDoubleClick
        If (ListView1.SelectedItems.Count > 0) Then
            Execute(ListView1.SelectedItems.Item(0).Text, ListView1.SelectedItems.Item(0).SubItems(3).Text, ListView1.SelectedItems.Item(0).SubItems(1).Text)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox2_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox2.KeyDown
        If (e.KeyCode = Keys.Enter) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If (e.KeyChar = Convert.ToChar(13)) Then
            e.Handled = True
            Search()
        End If
        If (e.KeyChar = Convert.ToChar(27)) Then
            e.Handled = True
            TextBox1.Text = ""
        End If
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If (e.KeyChar = Convert.ToChar(13)) Then
            e.Handled = True
            Search()
        End If
        If (e.KeyChar = Convert.ToChar(27)) Then
            e.Handled = True
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub TextBox1_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyUp
        TimerBeginSearch.Stop()
        TimerBeginSearch.Start()
        TimerBeginSearch.Enabled = True
    End Sub

    Private Sub TextBox2_KeyUp(sender As Object, e As KeyEventArgs) Handles TextBox2.KeyUp
        TimerBeginSearch.Stop()
        TimerBeginSearch.Start()
        TimerBeginSearch.Enabled = True
    End Sub


    Private Sub NotifyIcon1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False
        End If
    End Sub


    Private Sub DémarrerDansLeSystrayToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DémarrerDansLeSystrayToolStripMenuItem.Click
        option_start_in_tray = Me.DémarrerDansLeSystrayToolStripMenuItem.Checked
        write_options()
    End Sub


    Private Sub MinimiserDansLeSystrayToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MinimiserDansLeSystrayToolStripMenuItem.Click
        option_minimize_in_tray = Me.MinimiserDansLeSystrayToolStripMenuItem.Checked
        write_options()
    End Sub

    Private Sub AToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AToolStripMenuItem.Click
        Dim file As String
        If (ListView1.SelectedItems.Count > 0) Then
            file = ListView1.SelectedItems.Item(0).SubItems(3).Text & ListView1.SelectedItems.Item(0).Text
            If file.IndexOf(" # ") > 0 Then
                file = file.Substring(0, file.IndexOf(" # "))
            End If
            Shell("explorer /select,""" & file & """", AppWinStyle.NormalFocus)
        End If

    End Sub

    Private Sub CopierAdresseToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopierAdresseToolStripMenuItem.Click
        Dim file As String
        If (ListView1.SelectedItems.Count > 0) Then
            file = ListView1.SelectedItems.Item(0).SubItems(3).Text & ListView1.SelectedItems.Item(0).Text
            If file.IndexOf(" # ") > 0 Then
                file = file.Substring(0, file.IndexOf(" # "))
            End If
            My.Computer.Clipboard.SetText(file)
        End If
    End Sub

    Private Sub CopierToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopierToolStripMenuItem.Click
        Dim file As String
        Dim list As System.Collections.Specialized.StringCollection
        If (ListView1.SelectedItems.Count > 0) Then
            file = ListView1.SelectedItems.Item(0).SubItems(3).Text & ListView1.SelectedItems.Item(0).Text
            If file.IndexOf(" # ") > 0 Then
                file = file.Substring(0, file.IndexOf(" # "))
            End If
            list = New System.Collections.Specialized.StringCollection
            list.Add(file)
            My.Computer.Clipboard.SetFileDropList(list)
        End If
    End Sub

    Private Sub CopierNomToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopierNomToolStripMenuItem.Click
        Dim file As String
        If (ListView1.SelectedItems.Count > 0) Then
            file = ListView1.SelectedItems.Item(0).Text
            If file.IndexOf(" # ") > 0 Then
                file = file.Substring(0, file.IndexOf(" # "))
            End If
            My.Computer.Clipboard.SetText(file)
        End If
    End Sub

    Private Sub ListView1_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles ListView1.ItemDrag
        Dim file As String
        Dim data As DataObject = New DataObject()
        If (ListView1.SelectedItems.Count > 0) Then
            file = ListView1.SelectedItems.Item(0).SubItems(3).Text & ListView1.SelectedItems.Item(0).Text
            If file.IndexOf(" # ") > 0 Then
                file = file.Substring(0, file.IndexOf(" # "))
            End If
            data.SetData(DataFormats.FileDrop, New String() {file})
            ListView1.DoDragDrop(data, DragDropEffects.Copy)
        End If
    End Sub

    ' Procedure ajoutée pour essayer de corriger le bug d'affichage du texte
    Private Sub TextBox1_GotFocus(sender As Object, e As EventArgs) Handles TextBox1.GotFocus
        Dim T As String
        T = TextBox1.Text
        TextBox1.ResetText()
        TextBox1.Text = T
    End Sub


    Private Sub IgnorerLesSéparateursToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles IgnorerLesSéparateursToolStripMenuItem.Click
        option_ignore_separators = Me.IgnorerLesSéparateursToolStripMenuItem.Checked
        write_options()
    End Sub

    Private Sub TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Click
        option_regexp = Me.TraiterCommeUneUniqueExpressionRégulièreToolStripMenuItem.Checked
        write_options()
    End Sub

    Private Sub ToujoursVisibleToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ToujoursVisibleToolStripMenuItem.Click
        option_always_visible = Me.ToujoursVisibleToolStripMenuItem.Checked
        Me.TopMost = option_always_visible
        write_options()
    End Sub

    Private Sub TraiterCommeUneUniqueChaineToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TraiterCommeUneUniqueChaineToolStripMenuItem.Click
        option_unique_exp = Me.TraiterCommeUneUniqueChaineToolStripMenuItem.Checked
        write_options()
    End Sub


    Private Sub VérifierSiLeFichierExisteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VérifierSiLeFichierExisteToolStripMenuItem.Click
        option_check_exists = Me.VérifierSiLeFichierExisteToolStripMenuItem.Checked
        write_options()
    End Sub

    Private Sub AssociateMenuItem_Click(sender As Object, e As EventArgs) Handles AssociateMenuItem.Click
        Dim ext As String
        Dim command As String
        MsgBox("Cette manipulation permet de définir une commande associée à un type de fichier. Pour supprimer cette association, il faut éditer et supprimer les lignes de type (:association) dans le fichier d'option de WIMF." & vbNewLine & "Le caractère spécial * peut être utilisé pour désigner le nom du fichier.", MsgBoxStyle.Information)
        ext = InputBox("Extension (folder pour les répertoires) :")
        If ext = "" Then Exit Sub
        command = InputBox("Commande à executer :")
        If command = "" Then Exit Sub
        ReDim Preserve new_association_ext(UBound(new_association_ext) + 1)
        ReDim Preserve new_association_command(UBound(new_association_command) + 1)
        new_association_ext(UBound(new_association_ext)) = ext
        new_association_command(UBound(new_association_command)) = command
        write_options()
    End Sub

    Private Sub Form1_HandleCreated(sender As Object, e As EventArgs) Handles Me.HandleCreated
        Keyboard.registerHotkey(Me, Asc(option_hotkey.ToUpper), Keyboard.KeyModifier.Winkey, 100)
    End Sub

    Private Sub Form1_HandleDestroyed(sender As Object, e As EventArgs) Handles Me.HandleDestroyed
        Keyboard.unregisterHotkeys(Me, 100)
        Quit()
    End Sub

    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        If m.Msg = Keyboard.WM_HOTKEY Then
            Select Case m.WParam
                Case 100
                    If Me.WindowState = FormWindowState.Minimized Then
                        Me.Show()
                        Me.WindowState = FormWindowState.Normal
                        Me.ShowInTaskbar = True
                        NotifyIcon1.Visible = False
                        Me.TextBox1.Focus()
                    Else
                        Me.WindowState = FormWindowState.Minimized
                    End If
            End Select
        End If
        MyBase.WndProc(m)
    End Sub

    Private Sub ShortcutMenuItem_Click(sender As Object, e As EventArgs) Handles ShortcutMenuItem.Click
        Dim sht As String
        sht = InputBox("Raccourci clavier = WINDOWS + ")
        If sht = "" Then Exit Sub
        option_hotkey = sht
        Keyboard.unregisterHotkeys(Me, 100)
        Keyboard.RegisterHotKey(Me, Asc(sht.ToUpper), Keyboard.KeyModifier.Winkey, 100)
        write_options()
    End Sub

    Public Sub Change_Status_Method(S As String)
        ToolStripStatusLabel1.Text = S
    End Sub


    Public Sub Update_ListView()
        Static last_search_table_id As Integer = -1
        Static last_index_shown As Integer = -1
        Dim it As ListViewItem
        Dim iconname As String
        Dim sit As ListViewItem.ListViewSubItem
        Dim i As Integer
        Dim IconForFile As Icon = SystemIcons.WinLogo

        If search_id <> search_table_id Then
            Exit Sub
        End If

        If search_found < 0 Then
            Exit Sub
        End If

        If (search_table_id <> last_search_table_id) Then
            last_index_shown = -1
            last_search_table_id = search_table_id
        End If

        If search_found <= last_index_shown - 1 And search_table_done Then
            'Task_Search.Start()
            Exit Sub
        End If

        ListView1.BeginUpdate()


        For i = (last_index_shown + 1) To search_found
            it = ListView1.Items.Add(search_table(i, 1))
            last_index_shown = i
            If search_table(i, 2) = "folder" Then
                it.SubItems.Add("")
                it.SubItems.Add("")
            Else
                sit = it.SubItems.Add(search_table(i, 2))
                it.SubItems.Add(search_table(i, 3))
                ListView1.SetSubItemImageIndex(0, 2, 0)
            End If

            it.SubItems.Add(search_table(i, 4))
            it.SubItems.Add(search_table(i, 5))
            If Not (IsNothing(search_table_color(i))) Then
                it.ForeColor = search_table_color(i)
            End If

            iconname = search_table(i, 2)

            If iconname = "folder" Then
                it.BackColor = Color.LightGoldenrodYellow
            Else
                it.BackColor = Color.White
            End If

            If Not (ImageExtensions.Images.ContainsKey(iconname)) Then
                IconForFile = IconTools.IconTools.GetIconForExtension("." & iconname, IconTools.ShellIconSize.SmallIcon)
                ImageExtensions.Images.Add(iconname, IconForFile)
            End If

            If (ImageExtensions.Images.ContainsKey(iconname)) Then
                it.ImageKey = iconname
            Else
                it.ImageKey = "default"
            End If

            If (i - last_index_shown) >= option_max_shown_per_update Then
                Exit For
            End If
        Next
        ListView1.EndUpdate()
    End Sub



    Private Sub TimerListView_Tick(sender As Object, e As EventArgs) Handles TimerListView.Tick
        Update_ListView()
    End Sub

    Private Sub TimerBeginSearch_Tick(sender As Object, e As EventArgs) Handles TimerBeginSearch.Tick
        TimerBeginSearch.Enabled = False
        Search()
    End Sub

    Public Sub Update_ProgressBar(show As Boolean, Optional value As Long = 0, Optional maximum As Long = 100)

        ToolStripProgressBar1.Visible = show
        ToolStripProgressBar1.Minimum = 0

        ToolStripProgressBar1.Maximum = maximum
        ToolStripProgressBar1.Value = value
        If value > 0 Then
            ToolStripProgressBar1.Value = value - 1
            ToolStripProgressBar1.Value = value
        End If
        Refresh()
    End Sub

    Private Sub RechargerLesCataloguesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RechargerLesCataloguesToolStripMenuItem.Click
        Load_All_Catalogs()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim d As String

        If InStr(ComboBox1.Text.ToLower, "toujours") Then
            ComboBox1.Tag = "01/01/78"
        ElseIf InStr(ComboBox1.Text.ToLower, "semaine") Then
            ComboBox1.Tag = Now.AddDays(-7).ToShortDateString
        ElseIf InStr(ComboBox1.Text.ToLower, "mois") Then
            ComboBox1.Tag = Now.AddMonths(-1).ToShortDateString
        ElseIf InStr(ComboBox1.Text.ToLower, "année") Then
            ComboBox1.Tag = Now.AddYears(-1).ToShortDateString
        ElseIf InStr(ComboBox1.Text.ToLower, "autre") Then
            d = InputBox("Date", "Entrer date")
            If d <> "" Then
                Try
                    ComboBox1.Tag = Date.Parse(d).ToShortDateString
                    ComboBox1.Items.Add(ComboBox1.Tag)
                    ComboBox1.SelectedIndex = ComboBox1.Items.Count - 1
                Catch ex As Exception
                    MsgBox("Date erronée")
                    ComboBox1.SelectedIndex = 0
                End Try
            End If
        Else
            ComboBox1.Tag = ComboBox1.Items(ComboBox1.SelectedIndex)
        End If
        Console.WriteLine("Date {0}" & ComboBox1.Tag)

        TimerBeginSearch.Enabled = False
        Search()

    End Sub


End Class

