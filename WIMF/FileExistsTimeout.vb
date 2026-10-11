Public Class FileExistsTimeout

    Shared ForGet_Available_Path_Sec As Integer = 60
    Shared Available_Path As New Dictionary(Of String, Boolean)
    Shared Available_Path_Date As New Dictionary(Of String, Date)
    Private m_Timeout As Integer = 100
    Private m_Thread As System.Threading.Thread
    Private m_File As String = String.Empty
    Private m_Exists As Boolean = False

    Public Function Available_server(ByVal server As String) As Boolean
        server = LCase(server)
        Console.WriteLine("Available_server [" & server & "]")
        If Available_Path.ContainsKey(server) Then
            Console.WriteLine("  - serveur connu")
            If DateDiff(DateInterval.Minute, Now, Available_Path_Date(server)) < ForGet_Available_Path_Sec Then
                Return Available_Path(server)
            Else
                Available_Path.Remove(server)
                Available_Path_Date.Remove(server)
            End If
        End If
        Console.WriteLine("  - serveur inconnu")
        Available_Path.Add(server, False)
        Available_Path_Date.Add(server, Now)
        Available_Path(server) = WIMF_Utils.ServerExists(server, m_Timeout * 0.8)
        Console.WriteLine("** Server available : " & server & " " & Available_Path(server))
        Return Available_Path(server)
    End Function


    Public Function FileExists(
            ByVal file As String,
            ByVal timeOut As Integer) As Boolean

        m_File = file
        m_Exists = False
        m_Thread = New System.Threading.Thread(AddressOf CallFileExists)
        m_Timeout = timeOut
        m_Thread.Start()
        m_Thread.Join(timeOut)
        m_Thread.Abort()
        Console.WriteLine("** File exists  : " & file & " " & m_Exists)
        Return m_Exists
    End Function

    Private Sub CallFileExists()
        Dim in_network As Boolean
        Dim server As String
        Dim p As Integer
        in_network = m_File.StartsWith("\\")
        If in_network Then
            server = Mid(m_File, 3)
            p = InStr(server, "\")
            server = Mid(server, 1, p - 1)
            If Not Available_server(server) Then
                Exit Sub
            End If
        End If
redo:
        m_Exists = My.Computer.FileSystem.FileExists(m_File)
        If Not m_Exists And in_network Then
            System.Threading.Thread.Sleep(m_Timeout / 10)
            GoTo redo
        End If

    End Sub

    Public Function DirectoryExists(
            ByVal file As String,
            ByVal timeOut As Integer) As Boolean

        m_File = file
        m_Exists = False
        m_Thread = New System.Threading.Thread(AddressOf CallDirectoryExists)
        m_Timeout = timeOut
        m_Thread.Start()
        m_Thread.Join(timeOut)
        m_Thread.Abort()
        Return m_Exists
    End Function

    Private Sub CallDirectoryExists()
        m_Exists = My.Computer.FileSystem.DirectoryExists(m_File)
    End Sub


    Public Function ServerExists(
            ByVal file As String,
            ByVal timeOut As Integer) As Boolean

        m_File = file
        m_Exists = False
        m_Thread = New System.Threading.Thread(AddressOf CallServerExists)
        m_Timeout = timeOut
        m_Thread.Start()
        m_Thread.Join(timeOut)
        m_Thread.Abort()
        Return m_Exists
    End Function

    Private Sub CallServerExists()
        Dim output As System.Net.NetworkInformation.PingReply
        Dim pinger As New System.Net.NetworkInformation.Ping
        Try
            output = pinger.Send(m_File)
            m_Exists = True
        Catch ex As Exception
            m_Exists = False
        End Try
    End Sub

End Class


