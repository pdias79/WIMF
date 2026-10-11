Imports System.Collections
Imports System.Windows.Forms


Public Class ListViewColumnSorter
    Implements IComparer
    Public Enum SortOrder
        Ascending
        Descending
    End Enum

    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            Result = DateTime.Compare(CType(ItemX.Text, DateTime), CType(ItemY.Text, DateTime))
        Else
            Result = DateTime.Compare(CType(ItemX.SubItems(mSortColumn).Text, DateTime), CType(ItemY.SubItems(mSortColumn).Text, DateTime))
        End If

        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class

Public Class ListViewStringSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            Result = ItemX.Text.CompareTo(ItemY.Text)
        Else
            Result = ItemX.SubItems(mSortColumn).Text.CompareTo(ItemY.SubItems(mSortColumn).Text)
        End If
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class

Public Class ListViewSizeSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        Dim ix, iy As UInteger
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)


        If mSortColumn = 0 Then
            ix = InStr(ItemX.Text, " ")
            iy = InStr(ItemY.Text, " ")
            If ix > 0 Then
                ix = CUInt(Left(ItemX.Text, ix - 1))
            End If
            If iy > 0 Then
                iy = CUInt(Left(ItemY.Text, iy - 1))
            End If
            Result = ix.CompareTo(iy)
        Else
            ix = InStr(ItemX.SubItems(mSortColumn).Text, " ")
            iy = InStr(ItemY.SubItems(mSortColumn).Text, " ")
            If ix > 0 Then
                ix = CUInt(Left(ItemX.SubItems(mSortColumn).Text, ix - 1))
            End If
            If iy > 0 Then
                iy = CUInt(Left(ItemY.SubItems(mSortColumn).Text, iy - 1))
            End If
            Result = ix.CompareTo(iy)

        End If

        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class


Public Class ListViewNumericSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            Result = Decimal.Compare(CType(ItemX.Text, Decimal), CType(ItemY.Text, Decimal))
        Else
            Result = Decimal.Compare(CType(ItemX.SubItems(mSortColumn).Text, Decimal), CType(ItemY.SubItems(mSortColumn).Text, Decimal))
        End If
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class

Public Class ListViewDateSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            x = ItemX.Text
            y = ItemY.Text
        Else
            x = ItemX.SubItems(mSortColumn).Text
            y = ItemY.SubItems(mSortColumn).Text
        End If

        If x = "" And y = "" Then
            Result = 0
        ElseIf x = "" Then
            Result = -1
        ElseIf y = "" Then
            Result = 1
        Else
            Result = DateTime.Compare(CType(x, DateTime), CType(y, DateTime))
        End If

        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class

Module SortMyListView
    Friend Sub SortMyListView(ByVal ListViewToSort As ListView, ByVal ColumnNumber As Integer, Optional ByVal Resort As Boolean = False, Optional ByVal ForceSort As Boolean = False)
        ' Sorts a list view column by string, number, or date.  The three types of sorts must be specified within the listview columns "tag" property unless the ForceSort option is enabled.
        ' ListViewToSort - The listview to sort
        ' ColumnNumber - The column number to sort by
        ' Resort - Resorts a listview in the same direction as the last sort
        ' ForceSort - Forces a sort even if no listview tag data exists and assumes a string sort.  If this is false (default) and no tag is specified the procedure will exit
        Dim SortOrder As SortOrder
        Static LastSortColumn As Integer = -1
        Static LastSortOrder As SortOrder = SortOrder.Ascending
        If Resort = True Then
            SortOrder = LastSortOrder
        Else
            If LastSortColumn = ColumnNumber Then
                If LastSortOrder = SortOrder.Ascending Then
                    SortOrder = SortOrder.Descending
                Else
                    SortOrder = SortOrder.Ascending
                End If
            Else
                SortOrder = SortOrder.Ascending
            End If
        End If

        ' In case a tag wasn't specified check if we should force a string sort
        If String.IsNullOrEmpty(CStr(ListViewToSort.Columns(ColumnNumber).Tag)) Then
            If ForceSort = True Then
                ListViewToSort.Columns(ColumnNumber).Tag = "String"
            Else
                ' don't sort since no tag was specified.
                Exit Sub
            End If
        End If

        Select Case ListViewToSort.Columns(ColumnNumber).Tag.ToString
            Case "Numeric"
                ListViewToSort.ListViewItemSorter = New ListViewNumericSort(ColumnNumber, SortOrder)
            Case "Date"
                ListViewToSort.ListViewItemSorter = New ListViewDateSort(ColumnNumber, SortOrder)
            Case "String"
                ListViewToSort.ListViewItemSorter = New ListViewStringSort(ColumnNumber, SortOrder)
            Case "Size"
                ListViewToSort.ListViewItemSorter = New ListViewSizeSort(ColumnNumber, SortOrder)
        End Select
        LastSortColumn = ColumnNumber
        LastSortOrder = SortOrder
        ListViewToSort.ListViewItemSorter = Nothing
    End Sub
End Module


