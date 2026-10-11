Imports System.IO
Imports System.IO.File
Imports System.IO.Compression
Imports System.IO.Compression.CompressionMode
Imports System.Threading.Thread
Imports System.Runtime.Serialization.Formatters.Binary.BinaryFormatter

Module WIMF_Utils

    Function File_UnCompressGZ(Optional FileName As String = "") As Boolean
        Dim FileNameUNGZ As String
        Dim FileReader As System.IO.StreamReader
        Dim FileWriter As System.IO.StreamWriter

        File_UnCompressGZ = False
        If Not FileName.ToUpper.EndsWith(".GZ") Then
            Exit Function
        End If

        If Not FileExists(FileName) Then
            Exit Function
        End If

        FileNameUNGZ = Left(FileName, Len(FileName) - 3)
        If FileExists(FileNameUNGZ) Then
            If File.GetLastWriteTime(FileName) < File.GetLastWriteTime(FileNameUNGZ) Then
                File_UnCompressGZ = True
                Exit Function
            End If
            Try
                File.Delete(FileNameUNGZ)
            Catch ex As Exception
                Exit Function
            End Try
        End If

        Console.WriteLine("Décompression de = {0}", FileName)

        Try
            FileReader = New StreamReader(New GZipStream(System.IO.File.Open(FileName, FileMode.Open), CompressionMode.Decompress))
            FileWriter = New StreamWriter(System.IO.File.Open(FileNameUNGZ, FileMode.Create))
            While (Not FileReader.EndOfStream)
                FileWriter.WriteLine(FileReader.ReadLine())
            End While

            FileReader.Close()
            FileWriter.Close()
        Catch ex As Exception
            Exit Function
        End Try

        File_UnCompressGZ = True
    End Function

    Sub File_CompressGZ(Optional FileName As String = "")
        Dim FileNameGZ As String
        Dim FileReader As System.IO.StreamReader
        Dim FileWriter As System.IO.StreamWriter

        FileNameGZ = FileName + ".gz"
        If FileExists(FileNameGZ) Then
            If File.GetLastWriteTime(FileName) < File.GetLastWriteTime(FileNameGZ) Then
                Exit Sub
            End If
            Try
                File.Delete(FileNameGZ)
            Catch ex As Exception
                Exit Sub
            End Try
        End If

        FileReader = New StreamReader(System.IO.File.Open(FileName, FileMode.Open))
        FileWriter = New StreamWriter(New GZipStream(System.IO.File.Open(FileNameGZ, FileMode.Create), CompressionMode.Compress))

        While (Not FileReader.EndOfStream)
            FileWriter.WriteLine(FileReader.ReadLine())
        End While

        FileReader.Close()
        FileWriter.Close()

    End Sub

    Public Function FileExists(filename As String, Optional timeout As Integer = 0) As Boolean
        'Static FS As New Scripting.FileSystemObject
        'FS.FileExists(file)
        'FileExists = File.Exists(filename)
        Dim X As New FileExistsTimeout()
        If timeout = 0 Then
            timeout = WIMF_Main.option_timeout
        End If
        FileExists = X.FileExists(filename, timeout)

    End Function

    Public Function DirectoryExists(filename As String, Optional timeout As Integer = 0) As Boolean
        Dim X As New FileExistsTimeout()
        If timeout = 0 Then
            timeout = WIMF_Main.option_timeout
        End If
        DirectoryExists = X.DirectoryExists(filename, WIMF_Main.option_timeout)
    End Function

    Public Function ServerExists(filename As String, Optional timeout As Integer = 0) As Boolean
        Dim X As New FileExistsTimeout()
        If timeout = 0 Then
            timeout = WIMF_Main.option_timeout
        End If
        ServerExists = X.ServerExists(filename, timeout)
    End Function

    Public Function FileIsLocal(fullname As String) As Boolean
        'FileIsLocal = Mid(fullname, 2, 1) = ":"
        Select Case Left(fullname, 2).ToUpper()
            Case "C:"
                FileIsLocal = True
            Case "D:"
                FileIsLocal = True
            Case Else
                FileIsLocal = False
        End Select
    End Function

    Public Function FileShortName(fullname As String) As String
        FileShortName = Split(fullname.Replace("/", "\"), "\").Last()
    End Function

    Public Function FileDate(fullname As String) As String
        FileDate = File.GetLastWriteTime(fullname).ToString()
    End Function

    Function IsInArray(stringToBeFound As String, arr As Object) As Boolean
        IsInArray = (UBound(Filter(arr, stringToBeFound)) > -1)
    End Function

    Public Sub DeleteArrayItem(ByRef ItemArray As Object, ByVal ItemElement As Long)
        Dim i As Long
        If Not IsArray(ItemArray) Then
            Err.Raise(13, , "Type Mismatch")
            Exit Sub
        End If
        If ItemElement < 0 Or ItemElement > UBound(ItemArray) Then
            Err.Raise(9, , "Subscript out of Range")
            Exit Sub
        End If
        For i = ItemElement To UBound(ItemArray) - 1
            ItemArray(i) = ItemArray(i + 1)
        Next
        'Try
        'ReDim Preserve ItemArray(UBound(ItemArray) - 1)
        '
        '        Catch ex As Exception
        'Err.Raise(Err.Number, , "Array not resizable.")
        '
        'End Try
    End Sub

End Module
