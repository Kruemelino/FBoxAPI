''' <summary>
''' TR-064 Support – WAN IP Connection
''' Date: 2019-04-09 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanipconnSCPD.pdf</see>
''' </summary>
''' <remarks>
''' This service supports only IP based WAN connections. E.g. the FRITZ!Box cable products typically use such a connection type. 
''' If your WAN interface uses a PPP connection, please have a look at the WANPPPConnection1 service description. 
''' To make sure which kind of connection type is used, you can make a TR-064 request to Layer3Forwarding:GetDefaultConnectionService which provides this information.
''' </remarks>
Friend Class WANIPConnectionSCPD
    Implements IWANIPConnectionSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANIPConnectionSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wanipconnSCPD Implements IWANIPConnectionSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As WANInfo) As Boolean Implements IWANIPConnectionSCPD.GetInfo
        If Info Is Nothing Then Info = New WANInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewEnable", Info.Enable) And
                   .TryGetValue("NewConnectionStatus", Info.ConnectionStatus) And
                   .TryGetValue("NewPossibleConnectionTypes", Info.PossibleConnectionTypes) And
                   .TryGetValue("NewConnectionType", Info.ConnectionType) And
                   .TryGetValue("NewName", Info.Name) And
                   .TryGetValue("NewUptime", Info.Uptime) And
                   .TryGetValue("NewLastConnectionError", Info.LastConnectionError) And
                   .TryGetValue("NewRSIPAvailable", Info.RSIPAvailable) And
                   .TryGetValue("NewNATEnabled", Info.NATEnabled) And
                   .TryGetValue("NewExternalIPAddress", Info.ExternalIPAddress) And
                   .TryGetValue("NewDNSServers", Info.DNSServers) And
                   .TryGetValue("NewMACAddress", Info.MACAddress) And
                   .TryGetValue("NewConnectionTrigger", Info.ConnectionTrigger) And
                   .TryGetValue("NewRouteProtocolRx", Info.RouteProtocolRx) And
                   .TryGetValue("NewDNSEnabled", Info.DNSEnabled) And
                   .TryGetValue("NewDNSOverrideAllowed", Info.DNSOverrideAllowed)
        End With
    End Function

    Public Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As ConnectionTypeEnum) As Boolean Implements IWANIPConnectionSCPD.GetConnectionTypeInfo
        With TR064Start(ServiceFile, "GetConnectionTypeInfo", Nothing)

            Return .TryGetValue("NewConnectionType", ConnectionType) And
                   .TryGetValue("NewPossibleConnectionTypes", PossibleConnectionTypes)
        End With
    End Function

    Public Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean Implements IWANIPConnectionSCPD.SetConnectionType
        Return Not TR064Start(ServiceFile, "SetConnectionType", New Dictionary(Of String, String) From {{"NewConnectionType", ConnectionType}}).ContainsKey("Error")
    End Function

    Public Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum, ByRef LastConnectionError As LastConnectionErrorEnum, ByRef NewUptime As Integer) As Boolean Implements IWANIPConnectionSCPD.GetStatusInfo
        With TR064Start(ServiceFile, "GetStatusInfo", Nothing)

            Return .TryGetValue("NewConnectionStatus", ConnectionStatus) And
                   .TryGetValue("NewLastConnectionError", LastConnectionError) And
                   .TryGetValue("NewUptime", NewUptime)
        End With
    End Function

    Public Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean, ByRef NATEnabled As Boolean) As Boolean Implements IWANIPConnectionSCPD.GetNATRSIPStatus
        With TR064Start(ServiceFile, "GetNATRSIPStatus", Nothing)

            Return .TryGetValue("NewRSIPAvailable", RSIPAvailable) And
                   .TryGetValue("NewNATEnabled", NATEnabled)
        End With
    End Function

    Public Function SetConnectionTrigger(ConnectionTrigger As String) As Boolean Implements IWANIPConnectionSCPD.SetConnectionTrigger
        Return Not TR064Start(ServiceFile, "SetConnectionTrigger", New Dictionary(Of String, String) From {{"NewConnectionTrigger", ConnectionTrigger}}).ContainsKey("Error")
    End Function

    Public Function ForceTermination() As Boolean Implements IWANIPConnectionSCPD.ForceTermination
        Return Not TR064Start(ServiceFile, "ForceTermination", Nothing).ContainsKey("Error")
    End Function

    Public Function RequestConnection() As Boolean Implements IWANIPConnectionSCPD.RequestConnection
        Return Not TR064Start(ServiceFile, "RequestConnection", Nothing).ContainsKey("Error")
    End Function

    Public Function X_GetDNSServers(ByRef DNSServers As String) As Boolean Implements IWANIPConnectionSCPD.X_GetDNSServers
        Return TR064Start(ServiceFile, "X_GetDNSServers", Nothing).TryGetValue("NewDNSServers", DNSServers)
    End Function

    Public Function GetPortMappingNumberOfEntries(ByRef PortMappingNumberOfEntries As Integer) As Boolean Implements IWANIPConnectionSCPD.GetPortMappingNumberOfEntries
        Return TR064Start(ServiceFile, "GetPortMappingNumberOfEntries", Nothing).TryGetValue("NewPortMappingNumberOfEntries", PortMappingNumberOfEntries)
    End Function

    Public Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.GetGenericPortMappingEntry
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

    Public Function GetSpecificPortMappingEntry(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.GetSpecificPortMappingEntry
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

    Public Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.AddPortMapping
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

    Public Function DeletePortMapping(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum) As Boolean Implements IWANIPConnectionSCPD.DeletePortMapping
        Return TR064Start(ServiceFile, "DeletePortMapping",
                        New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                                {"NewExternalPort", ExternalPort},
                                                                {"NewProtocol", PortMappingProtocol}}).ContainsKey("Error")
    End Function

    Public Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean Implements IWANIPConnectionSCPD.GetExternalIPAddress
        Return TR064Start(ServiceFile, "GetExternalIPAddress", Nothing).TryGetValue("NewExternalIPAddress", ExternalIPAddress)
    End Function

    Public Function SetRouteProtocolRx(RouteProtocolRx As String) As Boolean Implements IWANIPConnectionSCPD.SetRouteProtocolRx
        Return Not TR064Start(ServiceFile, "SetRouteProtocolRx", New Dictionary(Of String, String) From {{"NewRouteProtocolRx", RouteProtocolRx}}).ContainsKey("Error")
    End Function

    Public Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean Implements IWANIPConnectionSCPD.SetIdleDisconnectTime
        Return Not TR064Start(ServiceFile, "SetIdleDisconnectTime", New Dictionary(Of String, String) From {{"NewIdleDisconnectTime", IdleDisconnectTime}}).ContainsKey("Error")
    End Function
End Class
