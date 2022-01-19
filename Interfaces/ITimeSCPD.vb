''' <summary>
''' TR-064 Support – Time
''' Date: 2009-07-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/timeSCPD.pdf</see>
''' </summary>
Public Interface ITimeSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As TimeInfo) As Boolean

    Function GetInfo(ByRef NTPServer1 As String, ByRef NTPServer2 As String, ByRef CurrentLocalTime As String) As Boolean

    Function SetNTPServers(NTPServer1 As String, NTPServer2 As String) As Boolean

End Interface
