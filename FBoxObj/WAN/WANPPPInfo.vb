Public Class WANPPPInfo
    Inherits WANInfo

    Public Property UpstreamMaxBitRate As Integer
    Public Property DownstreamMaxBitRate As Integer
    Public Property UserName As String
    Public Property LastAuthErrorInfo As String
    Public Property MaxCharsUsername As Integer
    Public Property MinCharsUsername As Integer
    Public Property AllowedCharsUsername As String
    Public Property MaxCharsPassword As Integer
    Public Property MinCharsPassword As Integer
    Public Property AllowedCharsPassword As String
    Public Property TransportType As String

    ''' <summary>
    ''' Not supported, returns ""
    ''' </summary>
    Public Property PPPoEServiceName As String

    ''' <summary>
    ''' Not supported, returns ""
    ''' </summary>
    Public Property RemoteIPAddress As String
    Public Property PPPoEACName As String
End Class
