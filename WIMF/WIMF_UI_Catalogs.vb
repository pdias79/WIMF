Public Class WIMF_UI_Catalogs

    Private Sub Button_ok_Click(sender As Object, e As EventArgs) Handles Button_ok.Click
        Me.Close()
    End Sub

    Private Sub WIMF_UI_Catalogs_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Update_View()

    End Sub


    Private Sub Update_View()
        Dim C As Long
        Dim it As ListViewItem

        ListView_Catalogs.Items().Clear()

        If UBound(WIMF_Main.Catalogs) < 1 Then Exit Sub

        For C = 1 To UBound(WIMF_Main.Catalogs)
            it = ListView_Catalogs.Items.Add(WIMF_Main.Catalogs(C).Name)
            it.SubItems.Add(WIMF_Main.Catalogs(C).Catalog_Path)
            it.SubItems.Add(WIMF_Main.Catalogs(C).File_Count)
            it.SubItems.Add(WIMF_Main.Catalogs(C).Last_Update.ToString)
            it.SubItems.Add(WIMF_Main.Catalogs(C).List_ext)
            If WIMF_Main.Catalogs(C).IsLocal Then
                it.ForeColor = Color.Black
            ElseIf WIMF_Main.Catalogs(C).LoadedOldCopie = WIMF_Catalog.LoadedOldCopie_Network_Not_found Then
                it.ForeColor = Color.Red
            ElseIf WIMF_Main.Catalogs(C).LoadedOldCopie = WIMF_Catalog.LoadedOldCopie_Local_Updated Then
                it.ForeColor = Color.Blue
            ElseIf WIMF_Main.Catalogs(C).LoadedOldCopie = WIMF_Catalog.LoadedOldCopie_Local_No_update_needed Then
                it.ForeColor = Color.Green
            Else
                it.ForeColor = Color.Gray
            End If

        Next

        Me.Refresh()

    End Sub

    Private Sub WIMF_UI_Catalogs_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Update_View()
    End Sub


End Class