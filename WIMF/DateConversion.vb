Module DateConversion

    Public ReadOnly Property Epoch() As DateTime
        Get
            Return New DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        End Get
    End Property

    Public Function FromUnix(ByVal seconds As Long, Optional local As Boolean = True) As DateTime
        Dim dt = Epoch.AddSeconds(seconds)
        If local Then dt = dt.ToLocalTime
        Return dt
    End Function

    Public Function ToUnix(ByVal dt As DateTime) As Long
        If dt.Kind = DateTimeKind.Local Then dt = dt.ToUniversalTime
        Return CInt((dt - Epoch).TotalSeconds)
    End Function

End Module
