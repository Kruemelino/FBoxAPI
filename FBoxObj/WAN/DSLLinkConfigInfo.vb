Public Class DSLLinkConfigInfo
    Public Property Enable As Boolean
    Public Property Status As String
    Public Property DataPath As String
    Public Property UpstreamCurrRate As Integer
    Public Property DownstreamCurrRate As Integer
    Public Property UpstreamMaxRate As Integer
    Public Property DownstreamMaxRate As Integer
    Public Property UpstreamNoiseMargin As Integer
    Public Property DownstreamNoiseMargin As Integer
    Public Property UpstreamAttenuation As Integer
    Public Property DownstreamAttenuation As Integer
    Public Property ATURVendor As String

    ''' <summary>
    ''' Not supported. Returns '0'.
    ''' </summary>
    Public Property ATURCountry As String

    ''' <summary>
    ''' Not supported. Returns '0'.
    ''' </summary>
    Public Property UpstreamPower As Integer

    ''' <summary>
    ''' Not supported. Returns '0'.
    ''' </summary>
    Public Property DownstreamPower As Integer
End Class
