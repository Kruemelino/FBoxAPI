''' <summary>
''' TR-064 Support – WAN PPP Connection
''' Date: 2019-04-09  
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanpppconnSCPD.pdf</see>
''' </summary>
''' <remarks>
''' This service supports only PPP based WAN connections. E.g. the FRITZ!Box products with a DSL interface have typically such a connection type. 
''' If your WAN interface uses an IP connection, please have a look at the WANIPConnection1 service description. 
''' To make sure which kind of connection type is used, you can make a TR-064 request to Layer3Forwarding:GetDefaultConnectionService which provides this information.
''' </remarks>
Friend Class WANPPPConnectionSCPD
    Implements IWANPPPConnectionSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANPPPConnectionSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wanpppconnSCPD Implements IWANPPPConnectionSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As WANPPPInfo) As Boolean Implements IWANPPPConnectionSCPD.GetInfo
        If Info Is Nothing Then Info = New WANPPPInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewEnable", Info.Enable) And
                   .TryGetValue("NewConnectionStatus", Info.ConnectionStatus) And
                   .TryGetValue("NewPossibleConnectionTypes", Info.PossibleConnectionTypes) And
                   .TryGetValue("NewConnectionType", Info.ConnectionType) And
                   .TryGetValue("NewName", Info.Name) And
                   .TryGetValue("NewUptime", Info.Uptime) And
                   .TryGetValue("NewUpstreamMaxBitRate", Info.UpstreamMaxBitRate) And
                   .TryGetValue("NewDownstreamMaxBitRate", Info.DownstreamMaxBitRate) And
                   .TryGetValue("NewLastConnectionError", Info.LastConnectionError) And
                   .TryGetValue("NewRSIPAvailable", Info.RSIPAvailable) And
                   .TryGetValue("NewUserName", Info.UserName) And
                   .TryGetValue("NewNATEnabled", Info.NATEnabled) And
                   .TryGetValue("NewExternalIPAddress", Info.ExternalIPAddress) And
                   .TryGetValue("NewDNSServers", Info.DNSServers) And
                   .TryGetValue("NewMACAddress", Info.MACAddress) And
                   .TryGetValue("NewConnectionTrigger", Info.ConnectionTrigger) And
                   .TryGetValue("NewLastAuthErrorInfo", Info.LastAuthErrorInfo) And
                   .TryGetValue("NewMaxCharsUsername", Info.MaxCharsUsername) And
                   .TryGetValue("NewMinCharsUsername", Info.MinCharsUsername) And
                   .TryGetValue("NewAllowedCharsUsername", Info.AllowedCharsUsername) And
                   .TryGetValue("NewMaxCharsPassword", Info.MaxCharsPassword) And
                   .TryGetValue("NewMinCharsPassword", Info.MinCharsPassword) And
                   .TryGetValue("NewAllowedCharsPassword", Info.AllowedCharsPassword) And
                   .TryGetValue("NewTransportType", Info.TransportType) And
                   .TryGetValue("NewRouteProtocolRx", Info.RouteProtocolRx) And
                   .TryGetValue("NewPPPoEServiceName", Info.PPPoEServiceName) And
                   .TryGetValue("NewRemoteIPAddress", Info.RemoteIPAddress) And
                   .TryGetValue("NewPPPoEACName", Info.PPPoEACName) And
                   .TryGetValue("NewDNSEnabled", Info.DNSEnabled) And
                   .TryGetValue("NewDNSOverrideAllowed", Info.DNSOverrideAllowed)
        End With
    End Function
    Public Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As ConnectionTypeEnum) As Boolean Implements IWANPPPConnectionSCPD.GetConnectionTypeInfo
        With TR064Start(ServiceFile, "GetConnectionTypeInfo", Nothing)

            Return .TryGetValue("NewConnectionType", ConnectionType) And
                   .TryGetValue("NewPossibleConnectionTypes", PossibleConnectionTypes)
        End With
    End Function

    Public Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean Implements IWANPPPConnectionSCPD.SetConnectionType
        Return Not TR064Start(ServiceFile, "SetConnectionType", New Dictionary(Of String, String) From {{"NewConnectionType", ConnectionType}}).ContainsKey("Error")
    End Function

    Public Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum, ByRef LastConnectionError As LastConnectionErrorEnum, ByRef NewUptime As Integer) As Boolean Implements IWANPPPConnectionSCPD.GetStatusInfo
        With TR064Start(ServiceFile, "GetStatusInfo", Nothing)

            Return .TryGetValue("NewConnectionStatus", ConnectionStatus) And
                   .TryGetValue("NewLastConnectionError", LastConnectionError) And
                   .TryGetValue("NewUptime", NewUptime)
        End With
    End Function

    Public Function GetLinkLayerMaxBitRates(ByRef UpstreamMaxBitRate As Integer, ByRef DownstreamMaxBitRate As Integer) As Boolean Implements IWANPPPConnectionSCPD.GetLinkLayerMaxBitRates
        With TR064Start(ServiceFile, "GetLinkLayerMaxBitRates", Nothing)

            Return .TryGetValue("NewUpstreamMaxBitRate", UpstreamMaxBitRate) And
                   .TryGetValue("NewDownstreamMaxBitRate", DownstreamMaxBitRate)
        End With
    End Function

    Public Function GetUserName(ByRef UserName As String) As Boolean Implements IWANPPPConnectionSCPD.GetUserName
        Return TR064Start(ServiceFile, "GetUserName", Nothing).TryGetValue("NewUserName", UserName)
    End Function

    Public Function SetUserName(UserName As String) As Boolean Implements IWANPPPConnectionSCPD.SetUserName
        Return Not TR064Start(ServiceFile, "SetUserName", New Dictionary(Of String, String) From {{"NewUserName", UserName}}).ContainsKey("Error")
    End Function

    Public Function SetPassword(Password As String) As Boolean Implements IWANPPPConnectionSCPD.SetPassword
        Return Not TR064Start(ServiceFile, "SetPassword", New Dictionary(Of String, String) From {{"NewPassword", Password}}).ContainsKey("Error")
    End Function

    Public Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean, ByRef NATEnabled As Boolean) As Boolean Implements IWANPPPConnectionSCPD.GetNATRSIPStatus
        With TR064Start(ServiceFile, "GetNATRSIPStatus", Nothing)

            Return .TryGetValue("NewRSIPAvailable", RSIPAvailable) And
                   .TryGetValue("NewNATEnabled", NATEnabled)
        End With
    End Function

    Public Function SetConnectionTrigger(ConnectionTrigger As String) As Boolean Implements IWANPPPConnectionSCPD.SetConnectionTrigger
        Return Not TR064Start(ServiceFile, "SetConnectionTrigger", New Dictionary(Of String, String) From {{"NewConnectionTrigger", ConnectionTrigger}}).ContainsKey("Error")
    End Function

    Public Function ForceTermination() As Boolean Implements IWANPPPConnectionSCPD.ForceTermination
        Return Not TR064Start(ServiceFile, "ForceTermination", Nothing).ContainsKey("Error")
    End Function

    Public Function RequestConnection() As Boolean Implements IWANPPPConnectionSCPD.RequestConnection
        Return Not TR064Start(ServiceFile, "RequestConnection", Nothing).ContainsKey("Error")
    End Function

    Public Function X_GetDNSServers(ByRef DNSServers As String) As Boolean Implements IWANPPPConnectionSCPD.X_GetDNSServers
        Return TR064Start(ServiceFile, "X_GetDNSServers", Nothing).TryGetValue("NewDNSServers", DNSServers)
    End Function

    Public Function GetPortMappingNumberOfEntries(ByRef PortMappingNumberOfEntries As Integer) As Boolean Implements IWANPPPConnectionSCPD.GetPortMappingNumberOfEntries
        Return TR064Start(ServiceFile, "GetPortMappingNumberOfEntries", Nothing).TryGetValue("NewPortMappingNumberOfEntries", PortMappingNumberOfEntries)
    End Function

    Public Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IWANPPPConnectionSCPD.GetGenericPortMappingEntry
        If GenericPortMappingEntry Is Nothing Then GenericPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetGenericPortMappingEntry", New Dictionary(Of String, String) From {{"NewPortMappingIndex", PortMappingIndex}})

            Return .TryGetValue("NewRemoteHost", GenericPortMappingEntry.RemoteHost) And
                   .TryGetValue("NewExternalPort", GenericPortMappingEntry.ExternalPort) And
                   .TryGetValue("NewProtocol", GenericPortMappingEntry.PortMappingProtocol) And
                   .TryGetValue("NewInternalPort", GenericPortMappingEntry.InternalPort) And
                   .TryGetValue("NewInternalClient", GenericPortMappingEntry.InternalClient) And
                   .TryGetValue("NewEnabled", GenericPortMappingEntry.PortMappingEnabled) And
                   .TryGetValue("NewPortMappingDescription", GenericPortMappingEntry.PortMappingDescription) And
                   .TryGetValue("NewLeaseDuration", GenericPortMappingEntry.PortMappingLeaseDuration)
        End With
    End Function

    Public Function GetSpecificPortMappingEntry(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IWANPPPConnectionSCPD.GetSpecificPortMappingEntry
        If GenericPortMappingEntry Is Nothing Then GenericPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetSpecificPortMappingEntry",
                        New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                                {"NewExternalPort", ExternalPort},
                                                                {"NewProtocol", PortMappingProtocol}})

            GenericPortMappingEntry.RemoteHost = RemoteHost
            GenericPortMappingEntry.ExternalPort = ExternalPort
            GenericPortMappingEntry.PortMappingProtocol = PortMappingProtocol

            Return .TryGetValue("NewInternalPort", GenericPortMappingEntry.InternalPort) And
                   .TryGetValue("NewInternalClient", GenericPortMappingEntry.InternalClient) And
                   .TryGetValue("NewEnabled", GenericPortMappingEntry.PortMappingEnabled) And
                   .TryGetValue("NewPortMappingDescription", GenericPortMappingEntry.PortMappingDescription) And
                   .TryGetValue("NewLeaseDuration", GenericPortMappingEntry.PortMappingLeaseDuration)
        End With
    End Function

    Public Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean Implements IWANPPPConnectionSCPD.AddPortMapping
        If NewPortMappingEntry IsNot Nothing Then
            With NewPortMappingEntry
                Return Not TR064Start(ServiceFile, "AddPortMapping",
                                      New Dictionary(Of String, String) From {{"NewRemoteHost", .RemoteHost},
                                                                              {"NewExternalPort", .ExternalPort},
                                                                              {"NewProtocol", .PortMappingProtocol},
                                                                              {"NewInternalPort", .InternalPort},
                                                                              {"NewInternalClient", .InternalClient},
                                                                              {"NewEnabled", .PortMappingEnabled.ToBoolStr},
                                                                              {"NewPortMappingDescription", .PortMappingDescription},
                                                                              {"NewLeaseDuration", .PortMappingLeaseDuration}}).
                                                                              ContainsKey("Error")
            End With
        Else
            Return False
        End If
    End Function

    Public Function DeletePortMapping(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum) As Boolean Implements IWANPPPConnectionSCPD.DeletePortMapping
        Return TR064Start(ServiceFile, "DeletePortMapping",
                        New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                                {"NewExternalPort", ExternalPort},
                                                                {"NewProtocol", PortMappingProtocol}}).ContainsKey("Error")
    End Function

    Public Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean Implements IWANPPPConnectionSCPD.GetExternalIPAddress
        Return TR064Start(ServiceFile, "GetExternalIPAddress", Nothing).TryGetValue("NewExternalIPAddress", ExternalIPAddress)
    End Function

    Public Function SetRouteProtocolRx(RouteProtocolRx As String) As Boolean Implements IWANPPPConnectionSCPD.SetRouteProtocolRx
        Return Not TR064Start(ServiceFile, "SetRouteProtocolRx", New Dictionary(Of String, String) From {{"NewRouteProtocolRx", RouteProtocolRx}}).ContainsKey("Error")
    End Function

    Public Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean Implements IWANPPPConnectionSCPD.SetIdleDisconnectTime
        Return Not TR064Start(ServiceFile, "SetIdleDisconnectTime", New Dictionary(Of String, String) From {{"NewIdleDisconnectTime", IdleDisconnectTime}}).ContainsKey("Error")
    End Function

    Public Function X_AVM_DE_GetAutoDisconnectTimeSpan(ByRef DisconnectPreventionEnable As Boolean, ByRef DisconnectPreventionHour As Integer) As Boolean Implements IWANPPPConnectionSCPD.X_AVM_DE_GetAutoDisconnectTimeSpan
        With TR064Start(ServiceFile, "X_AVM_DE_GetAutoDisconnectTimeSpan ", Nothing)

            Return .TryGetValue("NewX_AVM-DE_DisconnectPreventionEnable", DisconnectPreventionEnable) And
                   .TryGetValue("NewX_AVM-DE_DisconnectPreventionHour", DisconnectPreventionHour)
        End With
    End Function

    Public Function X_AVM_DE_SetAutoDisconnectTimeSpan(DisconnectPreventionEnable As Boolean, DisconnectPreventionHour As Integer) As Boolean Implements IWANPPPConnectionSCPD.X_AVM_DE_SetAutoDisconnectTimeSpan
        Return TR064Start(ServiceFile, "X_AVM_DE_SetAutoDisconnectTimeSpan",
                New Dictionary(Of String, String) From {{"NewX_AVM-DE_DisconnectPreventionEnable", DisconnectPreventionEnable.ToBoolStr},
                                                        {"NewX_AVM-DE_DisconnectPreventionHour", DisconnectPreventionHour}}).ContainsKey("Error")

    End Function
End Class
