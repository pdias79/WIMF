Imports System.Runtime.InteropServices

Public Class myListView
    Inherits ListView

    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Friend Structure LVITEM
        Friend mask As Integer
        Friend iItem As Integer
        Friend subItem As Integer
        Friend state As Integer
        Friend stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)>
        Friend lpszText As String
        Friend cchTextMax As Integer
        Friend iImage As Integer
        Friend lParam As IntPtr
        Friend iIndent As Integer
    End Structure

    Friend Const LVM_FIRST As Integer = &H1000
    Friend Const LVM_GETITEMA As Integer = LVM_FIRST + 5
    Friend Const LVM_GETITEMW As Integer = LVM_FIRST + 75
    Friend Shared ReadOnly LVM_GETITEM As Integer =
    CInt(IIf(Marshal.SystemDefaultCharSize = 1, LVM_GETITEMA, LVM_GETITEMW))
    Friend Const LVM_SETITEMA As Integer = LVM_FIRST + 6
    Friend Const LVM_SETITEMW As Integer = LVM_FIRST + 76
    Friend Shared ReadOnly LVM_SETITEM As Integer =
    CInt(IIf(Marshal.SystemDefaultCharSize = 1, LVM_SETITEMA, LVM_SETITEMW))

    Friend Const LVM_SETEXTENDEDLISTVIEWSTYLE As Integer = LVM_FIRST + 54
    Friend Const LVIF_IMAGE As Integer = &H2
    Friend Const LVS_EX_SUBITEMIMAGES As Integer = &H2

    Friend Shared Function ListView_GetItem(ByVal hwnd As IntPtr, ByRef lvi As LVITEM) As Boolean
        Return CBool(SendMessage(hwnd, LVM_GETITEM,
        IntPtr.Zero, lvi))
    End Function

    Friend Shared Function ListView_SetItem(ByVal hwnd As IntPtr, ByRef lvi As LVITEM) As Boolean
        Return CBool(SendMessage(hwnd, LVM_SETITEM,
        IntPtr.Zero, lvi))
    End Function

    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As LVITEM) As Integer
    Friend Overloads Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer

    Public Sub SetSubItemImageIndex(ByVal index As Integer, ByVal subItemIndex As Integer, ByVal imageIndex As Integer)

        Dim lvi As LVITEM

        lvi = Nothing

        lvi.iItem = index
        lvi.subItem = subItemIndex
        lvi.iImage = imageIndex
        lvi.mask = LVIF_IMAGE
        ListView_SetItem(Me.Handle, lvi)

    End Sub

    Public Function GetSubItemImageIndex(ByVal index As Integer, ByVal subItemIndex As Integer) As Integer

        Dim lvi As LVITEM

        lvi = Nothing

        lvi.iItem = index
        lvi.subItem = subItemIndex
        lvi.mask = LVIF_IMAGE
        If ListView_GetItem(Me.Handle, lvi) Then
            Return lvi.iImage
        Else
            Return -1
        End If

    End Function


    Public Sub New()
        Me.DoubleBuffered = True
    End Sub

    Public Sub Allow_Change_Subitems()
        'set listview to allow changes on subitems
        SendMessage(Me.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, LVS_EX_SUBITEMIMAGES, LVS_EX_SUBITEMIMAGES)
    End Sub

    ' Changer une image
    ' SetSubItemImageIndex(intY, ListView1.Columns(intY).Index, 0)

End Class
