''' <summary>
''' TR-064 Support – X AVM Remote Access
''' Date: 2022-10-17
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_remoteSCPD.pdf</see>
''' </summary>
Friend Class X_remoteSCPD
    Implements IX_remoteSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 17) Implements IX_remoteSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_remoteSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_remoteSCPD Implements IX_remoteSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_remoteSCPD.ServiceID

    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)), XMLSerializer As Serializer)

        TR064Start = Start

        XML = XMLSerializer

    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean, ByRef Port As Integer, ByRef Username As String) As Boolean Implements IX_remoteSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)
            Return .TryGetValueEx("NewEnabled", Enabled) And
                   .TryGetValueEx("NewPort", Port) And
                   .TryGetValueEx("NewUsername", Username)
        End With
    End Function

    Public Function GetInfo(ByRef Info As XRemoteInfo) As Boolean Implements IX_remoteSCPD.GetInfo
        If Info Is Nothing Then Info = New XRemoteInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewEnabled", Info.Enabled) And
                   .TryGetValueEx("NewPort", Info.Port) And
                   .TryGetValueEx("NewUsername", Info.Username)
        End With
    End Function

    Public Function SetConfig(Enabled As Boolean, Port As Integer, Username As String, Password As String) As Boolean Implements IX_remoteSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnabled", Enabled.ToBoolStr},
                                                                      {"NewPort", Port.ToString},
                                                                      {"NewUsername", Username},
                                                                      {"NewPassword", Password}}).ContainsKey("Error")

    End Function

    Public Function GetDDNSInfo(ByRef Info As DDNSInfo) As Boolean Implements IX_remoteSCPD.GetDDNSInfo
        If Info Is Nothing Then Info = New DDNSInfo

        With TR064Start(ServiceFile, "GetDDNSInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewDomain", Info.Domain) And
                   .TryGetValueEx("NewEnabled", Info.Enabled) And
                   .TryGetValueEx("NewMode", Info.Mode) And
                   .TryGetValueEx("NewProviderName", Info.ProviderName) And
                   .TryGetValueEx("NewServerIPv4", Info.ServerIPv4) And
                   .TryGetValueEx("NewServerIPv6", Info.ServerIPv6) And
                   .TryGetValueEx("NewStatusIPv4", Info.StatusIPv4) And
                   .TryGetValueEx("NewStatusIPv6", Info.StatusIPv6) And
                   .TryGetValueEx("NewUpdateURL", Info.UpdateURL) And
                   .TryGetValueEx("NewUsername", Info.Username)

        End With
    End Function

    Public Function GetDDNSProviders(ByRef ProviderList As String) As Boolean Implements IX_remoteSCPD.GetDDNSProviders
        Return TR064Start(ServiceFile, "GetDDNSProviders", ServiceID, Nothing).TryGetValueEx("NewProviderList", ProviderList)
    End Function

    Public Function GetDDNSProviders(ByRef List As ProviderList) As Boolean Implements IX_remoteSCPD.GetDDNSProviders
        Dim ListData As String = String.Empty
        Return GetDDNSProviders(ListData) AndAlso XML.Deserialize(ListData, False, List)
    End Function

    Public Function SetDDNSConfig(Info As DDNSInfo, Password As String) As Boolean Implements IX_remoteSCPD.SetDDNSConfig
        Return Not TR064Start(ServiceFile, "SetDDNSConfig", ServiceID,
                      New Dictionary(Of String, String) From {{"NewEnabled", Info.Enabled.ToBoolStr},
                                                              {"NewProviderName", Info.ProviderName},
                                                              {"NewUpdateURL", Info.UpdateURL},
                                                              {"NewServerIPv4", Info.ServerIPv4},
                                                              {"NewServerIPv6", Info.ServerIPv6},
                                                              {"NewDomain", Info.Domain},
                                                              {"NewUsername", Info.Username},
                                                              {"NewPassword", Password},
                                                              {"NewMode", Info.Mode.ToString}}).ContainsKey("Error")
    End Function

    Public Function SetEnable(Enabled As Boolean, ByRef Port As Integer) As Boolean Implements IX_remoteSCPD.SetEnable
        Return TR064Start(ServiceFile, "SetEnable", ServiceID, New Dictionary(Of String, String) From {{"NewEnabled", Enabled.ToBoolStr}}).TryGetValueEx("NewPort", Port)
    End Function
End Class