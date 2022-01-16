Public Class DSLInfo
    Public Property SNRGds As Integer

    ''' <summary>
    ''' Always returns 1
    ''' </summary>
    Public Property SNRGus As Integer
    Public Property SNRpsds As String
    Public Property SNRpsus As String
    Public Property SNRMTds As Integer
    Public Property SNRMTus As Integer
    Public Property LATNds As String
    Public Property LATNus As String
    Public Property FECErrors As Integer
    Public Property CRCErrors As Integer
    Public Property LinkStatus As LinkStatusEnum
    Public Property ModulationType As String
    Public Property CurrentProfile As String
    Public Property UpstreamCurrRate As Integer
    Public Property DownstreamCurrRate As Integer
    Public Property UpstreamMaxRate As Integer
    Public Property DownstreamMaxRate As Integer
    Public Property UpstreamNoiseMargin As Integer
    Public Property DownstreamNoiseMargin As Integer
    Public Property UpstreamAttenuation As Integer
    Public Property DownstreamAttenuation As Integer
    Public Property ATURVendor As Integer
    Public Property ATURCountry As Integer
    Public Property UpstreamPower As Integer
    Public Property DownstreamPower As Integer
End Class
