﻿''' <summary>
''' TR-064 Support – WAN IP Connection
''' Date: 2021-09-08
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanipconnSCPD.pdf</see>
''' </summary>
''' <remarks>
''' This service supports only IP based WAN connections. E.g. the FRITZ!Box cable products typically use such a connection type. 
''' If your WAN interface uses a PPP connection, please have a look at the WANPPPConnection1 service description. 
''' To make sure which kind of connection type is used, you can make a TR-064 request to Layer3Forwarding:GetDefaultConnectionService which provides this information.
''' </remarks>
Friend Class WANIPConnectionSCPD
    Implements IWANIPConnectionSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2021, 9, 8) Implements IWANIPConnectionSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANIPConnectionSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wanipconnSCPD Implements IWANIPConnectionSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IWANIPConnectionSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As WANInfo) As Boolean Implements IWANIPConnectionSCPD.GetInfo
        If Info Is Nothing Then Info = New WANInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewEnable", Info.Enable) And
                   .TryGetValueEx("NewConnectionStatus", Info.ConnectionStatus) And
                   .TryGetValueEx("NewPossibleConnectionTypes", Info.PossibleConnectionTypes) And
                   .TryGetValueEx("NewConnectionType", Info.ConnectionType) And
                   .TryGetValueEx("NewName", Info.Name) And
                   .TryGetValueEx("NewUptime", Info.Uptime) And
                   .TryGetValueEx("NewLastConnectionError", Info.LastConnectionError) And
                   .TryGetValueEx("NewRSIPAvailable", Info.RSIPAvailable) And
                   .TryGetValueEx("NewNATEnabled", Info.NATEnabled) And
                   .TryGetValueEx("NewExternalIPAddress", Info.ExternalIPAddress) And
                   .TryGetValueEx("NewDNSServers", Info.DNSServers) And
                   .TryGetValueEx("NewMACAddress", Info.MACAddress) And
                   .TryGetValueEx("NewConnectionTrigger", Info.ConnectionTrigger) And
                   .TryGetValueEx("NewRouteProtocolRx", Info.RouteProtocolRx) And
                   .TryGetValueEx("NewDNSEnabled", Info.DNSEnabled) And
                   .TryGetValueEx("NewDNSOverrideAllowed", Info.DNSOverrideAllowed)
        End With
    End Function

    Public Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As ConnectionTypeEnum) As Boolean Implements IWANIPConnectionSCPD.GetConnectionTypeInfo
        With TR064Start(ServiceFile, "GetConnectionTypeInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewConnectionType", ConnectionType) And
                   .TryGetValueEx("NewPossibleConnectionTypes", PossibleConnectionTypes)
        End With
    End Function

    Public Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean Implements IWANIPConnectionSCPD.SetConnectionType
        Return Not TR064Start(ServiceFile, "SetConnectionType", ServiceID, New Dictionary(Of String, String) From {{"NewConnectionType", ConnectionType.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum, ByRef LastConnectionError As LastConnectionErrorEnum, ByRef NewUptime As Integer) As Boolean Implements IWANIPConnectionSCPD.GetStatusInfo
        With TR064Start(ServiceFile, "GetStatusInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewConnectionStatus", ConnectionStatus) And
                   .TryGetValueEx("NewLastConnectionError", LastConnectionError) And
                   .TryGetValueEx("NewUptime", NewUptime)
        End With
    End Function

    Public Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean, ByRef NATEnabled As Boolean) As Boolean Implements IWANIPConnectionSCPD.GetNATRSIPStatus
        With TR064Start(ServiceFile, "GetNATRSIPStatus", ServiceID, Nothing)

            Return .TryGetValueEx("NewRSIPAvailable", RSIPAvailable) And
                   .TryGetValueEx("NewNATEnabled", NATEnabled)
        End With
    End Function

    Public Function SetConnectionTrigger(ConnectionTrigger As String) As Boolean Implements IWANIPConnectionSCPD.SetConnectionTrigger
        Return Not TR064Start(ServiceFile, "SetConnectionTrigger", ServiceID, New Dictionary(Of String, String) From {{"NewConnectionTrigger", ConnectionTrigger}}).ContainsKey("Error")
    End Function

    Public Function ForceTermination() As Boolean Implements IWANIPConnectionSCPD.ForceTermination
        Return Not TR064Start(ServiceFile, "ForceTermination", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function RequestConnection() As Boolean Implements IWANIPConnectionSCPD.RequestConnection
        Return Not TR064Start(ServiceFile, "RequestConnection", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function X_GetDNSServers(ByRef DNSServers As String) As Boolean Implements IWANIPConnectionSCPD.X_GetDNSServers
        Return TR064Start(ServiceFile, "X_GetDNSServers", ServiceID, Nothing).TryGetValueEx("NewDNSServers", DNSServers)
    End Function

    Public Function GetPortMappingNumberOfEntries(ByRef PortMappingNumberOfEntries As Integer) As Boolean Implements IWANIPConnectionSCPD.GetPortMappingNumberOfEntries
        Return TR064Start(ServiceFile, "GetPortMappingNumberOfEntries", ServiceID, Nothing).TryGetValueEx("NewPortMappingNumberOfEntries", PortMappingNumberOfEntries)
    End Function

    Public Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.GetGenericPortMappingEntry
        If GenericPortMappingEntry Is Nothing Then GenericPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetGenericPortMappingEntry", ServiceID, New Dictionary(Of String, String) From {{"NewPortMappingIndex", PortMappingIndex.ToString}})

            Return .TryGetValueEx("NewRemoteHost", GenericPortMappingEntry.RemoteHost) And
                   .TryGetValueEx("NewExternalPort", GenericPortMappingEntry.ExternalPort) And
                   .TryGetValueEx("NewProtocol", GenericPortMappingEntry.PortMappingProtocol) And
                   .TryGetValueEx("NewInternalPort", GenericPortMappingEntry.InternalPort) And
                   .TryGetValueEx("NewInternalClient", GenericPortMappingEntry.InternalClient) And
                   .TryGetValueEx("NewEnabled", GenericPortMappingEntry.PortMappingEnabled) And
                   .TryGetValueEx("NewPortMappingDescription", GenericPortMappingEntry.PortMappingDescription) And
                   .TryGetValueEx("NewLeaseDuration", GenericPortMappingEntry.PortMappingLeaseDuration)
        End With
    End Function

    Public Function GetSpecificPortMappingEntry(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum, ByRef SpecificPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.GetSpecificPortMappingEntry
        If SpecificPortMappingEntry Is Nothing Then SpecificPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetSpecificPortMappingEntry", ServiceID,
                        New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                                {"NewExternalPort", ExternalPort.ToString},
                                                                {"NewProtocol", PortMappingProtocol.ToString}})

            SpecificPortMappingEntry.RemoteHost = RemoteHost
            SpecificPortMappingEntry.ExternalPort = ExternalPort
            SpecificPortMappingEntry.PortMappingProtocol = PortMappingProtocol

            Return .TryGetValueEx("NewInternalPort", SpecificPortMappingEntry.InternalPort) And
                   .TryGetValueEx("NewInternalClient", SpecificPortMappingEntry.InternalClient) And
                   .TryGetValueEx("NewEnabled", SpecificPortMappingEntry.PortMappingEnabled) And
                   .TryGetValueEx("NewPortMappingDescription", SpecificPortMappingEntry.PortMappingDescription) And
                   .TryGetValueEx("NewLeaseDuration", SpecificPortMappingEntry.PortMappingLeaseDuration)
        End With
    End Function

    Public Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean Implements IWANIPConnectionSCPD.AddPortMapping
        If NewPortMappingEntry IsNot Nothing Then
            With NewPortMappingEntry
                Return Not TR064Start(ServiceFile, "AddPortMapping", ServiceID,
                                      New Dictionary(Of String, String) From {{"NewRemoteHost", .RemoteHost},
                                                                              {"NewExternalPort", .ExternalPort.ToString},
                                                                              {"NewProtocol", .PortMappingProtocol.ToString},
                                                                              {"NewInternalPort", .InternalPort.ToString},
                                                                              {"NewInternalClient", .InternalClient},
                                                                              {"NewEnabled", .PortMappingEnabled.ToBoolStr},
                                                                              {"NewPortMappingDescription", .PortMappingDescription},
                                                                              {"NewLeaseDuration", .PortMappingLeaseDuration.ToString}}).
                                                                              ContainsKey("Error")
            End With
        Else
            Return False
        End If
    End Function

    Public Function DeletePortMapping(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum) As Boolean Implements IWANIPConnectionSCPD.DeletePortMapping
        Return TR064Start(ServiceFile, "DeletePortMapping", ServiceID,
                        New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                                {"NewExternalPort", ExternalPort.ToString},
                                                                {"NewProtocol", PortMappingProtocol.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean Implements IWANIPConnectionSCPD.GetExternalIPAddress
        Return TR064Start(ServiceFile, "GetExternalIPAddress", ServiceID, Nothing).TryGetValueEx("NewExternalIPAddress", ExternalIPAddress)
    End Function

    Public Function SetRouteProtocolRx(RouteProtocolRx As String) As Boolean Implements IWANIPConnectionSCPD.SetRouteProtocolRx
        Return Not TR064Start(ServiceFile, "SetRouteProtocolRx", ServiceID, New Dictionary(Of String, String) From {{"NewRouteProtocolRx", RouteProtocolRx}}).ContainsKey("Error")
    End Function

    Public Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean Implements IWANIPConnectionSCPD.SetIdleDisconnectTime
        Return Not TR064Start(ServiceFile, "SetIdleDisconnectTime", ServiceID, New Dictionary(Of String, String) From {{"NewIdleDisconnectTime", IdleDisconnectTime.ToString}}).ContainsKey("Error")
    End Function
End Class
