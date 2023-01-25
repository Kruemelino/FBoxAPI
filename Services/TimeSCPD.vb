''' <summary>
''' TR-064 Support – Time
''' Date: 2022-02-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/timeSCPD.pdf</see>
''' </summary>
Friend Class TimeSCPD
    Implements ITimeSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 15) Implements ITimeSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements ITimeSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.timeSCPD Implements ITimeSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As TimeInfo) As Boolean Implements ITimeSCPD.GetInfo
        If Info Is Nothing Then Info = New TimeInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewNTPServer1", Info.NTPServer1) And
                   .TryGetValueEx("NewNTPServer2", Info.NTPServer2) And
                   .TryGetValueEx("NewCurrentLocalTime", Info.CurrentLocalTime) And
                   .TryGetValueEx("NewLocalTimeZone", Info.LocalTimeZone) And
                   .TryGetValueEx("NewLocalTimeZoneName", Info.LocalTimeZoneName) And
                   .TryGetValueEx("NewDaylightSavingsUsed", Info.DaylightSavingsUsed) And
                   .TryGetValueEx("NewDaylightSavingsStart", Info.DaylightSavingsStart) And
                   .TryGetValueEx("NewDaylightSavingsEnd", Info.DaylightSavingsEnd)
        End With
    End Function

    Public Function GetInfo(ByRef NTPServer1 As String, ByRef NTPServer2 As String, ByRef CurrentLocalTime As String) As Boolean Implements ITimeSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewNTPServer1", NTPServer1) And
                   .TryGetValueEx("NewNTPServer2", NTPServer2) And
                   .TryGetValueEx("NewCurrentLocalTime", CurrentLocalTime)
        End With
    End Function

    Public Function SetConnectionRequestAuthentication(NTPServer1 As String, NTPServer2 As String) As Boolean Implements ITimeSCPD.SetNTPServers
        Return Not TR064Start(ServiceFile, "SetNTPServers",
                              New Dictionary(Of String, String) From {{"NewNTPServer1", NTPServer1},
                                                                      {"NewNTPServer2", NTPServer2}}).ContainsKey("Error")
    End Function

End Class
