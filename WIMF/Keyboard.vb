Public Class Keyboard


    Public Declare Function RegisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer, ByVal fsModifiers As Integer, ByVal vk As Integer) As Integer
    Public Declare Function UnregisterHotKey Lib "user32" (ByVal hwnd As IntPtr, ByVal id As Integer) As Integer
    Private Declare Function BlockInput Lib "user32" Alias "BlockInput" (ByVal fBlock As Integer) As Integer
    Private Declare Function ShowCursor Lib "user32" (ByVal lShow As Long) As Long

    Public Const WM_HOTKEY As Integer = &H312

    Enum KeyModifier
        None = 0
        Alt = &H1
        Control = &H2
        Shift = &H4
        Winkey = &H8
    End Enum 'This enum is just to make it easier to call the registerHotKey function: The modifier integer codes are replaced by a friendly "Alt","Shift" etc.

    Public Shared Sub registerHotkey(ByRef sourceForm As Form, ByVal triggerKey As Object, ByVal modifier As KeyModifier, Optional ByVal id As Integer = 1)
        If TypeOf (triggerKey) Is String Then
            RegisterHotKey(sourceForm.Handle, id, modifier, Asc(triggerKey.ToUpper))
            Exit Sub
        End If
        RegisterHotKey(sourceForm.Handle, id, modifier, triggerKey)
    End Sub

    Public Shared Sub unregisterHotkeys(ByRef sourceForm As Form, Optional ByVal id As Integer = 1)
        UnregisterHotKey(sourceForm.Handle, id)  'Remember to call unregisterHotkeys() when closing your application.
    End Sub


    Public Shared Sub Disable_inputs()
        BlockInput(1)
        ShowCursor(0)
    End Sub

    Public Shared Sub Enable_inputs()
        BlockInput(0)
        ShowCursor(1)
    End Sub


End Class

