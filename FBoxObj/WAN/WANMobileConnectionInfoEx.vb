''' <summary>
''' TR-064 Support – X_AVM-DE_WANMobileConnection
''' Date: 2023-04-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanmobileconnSCPD.pdf</see>
''' </summary>
Public Class WANMobileConnectionInfoEx
    Public Property SerialNumber As String
    Public Property EnableVoIPPDN As Boolean
    Public Property PPPUsername As String
    Public Property PPPUsernameVoIP As String
    Public Property PPPAuthProtocol As String
    Public Property PPPAuthProtocolVoIP As String
    Public Property SoftwareVersion As String
    Public Property Uptime As Integer
    Public Property PDN1_MTU As Integer
    Public Property PDN2_MTU As Integer
    Public Property IMSI As String
    Public Property APN_VoIP As String
    Public Property APN As String
    Public Property Roaming As Boolean

    ''' <summary>
    ''' unkown(*), GSM, UMTS, EDGE, HSDPA, LTE, LTE5GCN, 5GCN, 5G-SA, 5GNSA
    ''' </summary>
    Public Property CurrentAccessTechnology As String
    Public Property SignalRSRP0 As Integer
    Public Property SignalRSRP1 As Integer
    Public Property CellList As String
End Class
