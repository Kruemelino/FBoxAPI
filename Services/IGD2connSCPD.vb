﻿''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD2.pdf</see>
''' </summary>
''' <remarks>
''' BBased on the Internet Gateway Device (IGD) V2.0 specification proposed by UpnP™ Forum at
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANIPConnection:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPConnection-v1-Service.pdf"/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPConnection-v2-Service.pdf"/>
''' </remarks> 
Public Class IGD2connSCPD
    Implements IIGD2connSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IIGD2connSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IIGD2connSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.igd2connSCPD Implements IIGD2connSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

#Region "WANIPConnection:1"
    Public Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean Implements IIGD2connSCPD.SetConnectionType
        Return Not TR064Start(ServiceFile, "SetConnectionType", New Dictionary(Of String, String) From {{"NewConnectionType", ConnectionType.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As String) As Boolean Implements IIGD2connSCPD.GetConnectionTypeInfo
        With TR064Start(ServiceFile, "GetConnectionTypeInfo", Nothing)

            Return .TryGetValueEx("NewConnectionType", ConnectionType) And
                   .TryGetValueEx("NewPossibleConnectionTypes", PossibleConnectionTypes)
        End With
    End Function

    Public Function RequestConnection() As Boolean Implements IIGD2connSCPD.RequestConnection
        Return Not TR064Start(ServiceFile, "RequestConnection", Nothing).ContainsKey("Error")
    End Function

    Public Function RequestTermination() As Boolean Implements IIGD2connSCPD.RequestTermination
        Return Not TR064Start(ServiceFile, "RequestTermination", Nothing).ContainsKey("Error")
    End Function

    Public Function ForceTermination() As Boolean Implements IIGD2connSCPD.ForceTermination
        Return Not TR064Start(ServiceFile, "ForceTermination", Nothing).ContainsKey("Error")
    End Function

    Public Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum, ByRef LastConnectionError As LastConnectionErrorEnum, ByRef Uptime As Integer) As Boolean Implements IIGD2connSCPD.GetStatusInfo
        With TR064Start(ServiceFile, "GetStatusInfo", Nothing)

            Return .TryGetValueEx("NewConnectionStatus", ConnectionStatus) And
                   .TryGetValueEx("NewLastConnectionError", LastConnectionError) And
                   .TryGetValueEx("NewUptime", Uptime)
        End With
    End Function

    Public Function GetAutoDisconnectTime(ByRef AutoDisconnectTime As Integer) As Boolean Implements IIGD2connSCPD.GetAutoDisconnectTime
        Return TR064Start(ServiceFile, "GetAutoDisconnectTime", Nothing).TryGetValueEx("NewAutoDisconnectTime", AutoDisconnectTime)
    End Function

    Public Function SetAutoDisconnectTime(AutoDisconnectTime As Integer) As Boolean Implements IIGD2connSCPD.SetAutoDisconnectTime
        Return Not TR064Start(ServiceFile, "SetAutoDisconnectTime", New Dictionary(Of String, String) From {{"NewAutoDisconnectTime", AutoDisconnectTime.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetIdleDisconnectTime(ByRef IdleDisconnectTime As Integer) As Boolean Implements IIGD2connSCPD.GetIdleDisconnectTime
        Return TR064Start(ServiceFile, "GetIdleDisconnectTime", Nothing).TryGetValueEx("NewIdleDisconnectTime", IdleDisconnectTime)
    End Function
    Public Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean Implements IIGD2connSCPD.SetIdleDisconnectTime
        Return Not TR064Start(ServiceFile, "SetIdleDisconnectTime", New Dictionary(Of String, String) From {{"NewIdleDisconnectTime", IdleDisconnectTime.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetWarnDisconnectDelay(ByRef WarnDisconnectDelay As Integer) As Boolean Implements IIGD2connSCPD.GetWarnDisconnectDelay
        Return TR064Start(ServiceFile, "GetWarnDisconnectDelay", Nothing).TryGetValueEx("NewWarnDisconnectDelay", WarnDisconnectDelay)
    End Function

    Public Function SetWarnDisconnectDelay(WarnDisconnectDelay As Integer) As Boolean Implements IIGD2connSCPD.SetWarnDisconnectDelay
        Return Not TR064Start(ServiceFile, "SetIdleDisconnectTime", New Dictionary(Of String, String) From {{"NewWarnDisconnectDelay", WarnDisconnectDelay.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean, ByRef NATEnabled As Boolean) As Boolean Implements IIGD2connSCPD.GetNATRSIPStatus
        With TR064Start(ServiceFile, "GetNATRSIPStatus", Nothing)

            Return .TryGetValueEx("NewRSIPAvailable", RSIPAvailable) And
                   .TryGetValueEx("NewNATEnabled", NATEnabled)
        End With
    End Function

    Public Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD2connSCPD.GetGenericPortMappingEntry
        If GenericPortMappingEntry Is Nothing Then GenericPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetGenericPortMappingEntry", New Dictionary(Of String, String) From {{"NewPortMappingIndex", PortMappingIndex.ToString}})

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

    Public Function GetSpecificPortMappingEntry(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum, ByRef SpecificPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD2connSCPD.GetSpecificPortMappingEntry
        If SpecificPortMappingEntry Is Nothing Then SpecificPortMappingEntry = New PortMappingEntry

        With TR064Start(ServiceFile, "GetSpecificPortMappingEntry",
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

    Public Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD2connSCPD.AddPortMapping
        If NewPortMappingEntry IsNot Nothing Then
            With NewPortMappingEntry
                Return Not TR064Start(ServiceFile, "AddPortMapping",
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

    Public Function DeletePortMapping(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum) As Boolean Implements IIGD2connSCPD.DeletePortMapping
        Return Not TR064Start(ServiceFile, "DeletePortMapping",
                New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                        {"NewExternalPort", ExternalPort.ToString},
                                                        {"NewProtocol", PortMappingProtocol.ToString}}).ContainsKey("Error")

    End Function

    Public Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean Implements IIGD2connSCPD.GetExternalIPAddress
        Return TR064Start(ServiceFile, "GetExternalIPAddress", Nothing).TryGetValueEx("NewExternalIPAddress", ExternalIPAddress)
    End Function
#End Region

#Region "Additional actions"
    Public Function GetDNSServer(ByRef IPv4DNSServer1 As String, ByRef IPv4DNSServer2 As String) As Boolean Implements IIGD2connSCPD.GetDNSServer
        With TR064Start(ServiceFile, "X_AVM_DE_GetDNSServer", Nothing)

            Return .TryGetValueEx("NewIPv4DNSServer1", IPv4DNSServer1) And
                   .TryGetValueEx("NewIPv4DNSServer2", IPv4DNSServer2)
        End With
    End Function
    Public Function GetIPv6DNSServer(ByRef IPv6DNSServer1 As String, ByRef ValidLifetime1 As Integer, ByRef IPv6DNSServer2 As String, ByRef ValidLifetime2 As Integer) As Boolean Implements IIGD2connSCPD.GetIPv6DNSServer
        With TR064Start(ServiceFile, "X_AVM_DE_GetIPv6DNSServer", Nothing)

            Return .TryGetValueEx("NewIPv6DNSServer1", IPv6DNSServer1) And
                   .TryGetValueEx("NewValidLifetime1", ValidLifetime1) And
                   .TryGetValueEx("NewIPv6DNSServer2", ValidLifetime2) And
                   .TryGetValueEx("NewValidLifetime2", ValidLifetime2)
        End With
    End Function

    Public Function GetExternalIPv6Address(ByRef ExternalIPv6Address As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean Implements IIGD2connSCPD.GetExternalIPv6Address
        With TR064Start(ServiceFile, "X_AVM_DE_GetExternalIPv6Address", Nothing)

            Return .TryGetValueEx("NewExternalIPv6Address", ExternalIPv6Address) And
                   .TryGetValueEx("NewPrefixLength", PrefixLength) And
                   .TryGetValueEx("NewValidLifetime", ValidLifetime) And
                   .TryGetValueEx("NewPreferedLifetime", PreferedLifetime)
        End With
    End Function

    Public Function GetIPv6Prefix(ByRef IPv6Prefix As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean Implements IIGD2connSCPD.GetIPv6Prefix
        With TR064Start(ServiceFile, "X_AVM_DE_GetIPv6Prefix", Nothing)

            Return .TryGetValueEx("NewIPv6Prefix", IPv6Prefix) And
                   .TryGetValueEx("NewPrefixLength", PrefixLength) And
                   .TryGetValueEx("NewValidLifetime", ValidLifetime) And
                   .TryGetValueEx("NewPreferedLifetime", PreferedLifetime)
        End With
    End Function


#End Region

#Region "WANIPConnection:2"

    Public Function DeletePortMappingRange(StartPort As Integer, EndPort As Integer, Protocol As PortMappingProtocolEnum, Manage As Boolean) As Boolean Implements IIGD2connSCPD.DeletePortMappingRange
        Return Not TR064Start(ServiceFile, "DeletePortMappingRange",
                          New Dictionary(Of String, String) From {{"NewStartPort", StartPort.ToString},
                                                                  {"NewEndPort", EndPort.ToString},
                                                                  {"NewProtocol", Protocol.ToString},
                                                                  {"NewManage", Manage.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetListOfPortMappings(StartPort As Integer, EndPort As Integer, Protocol As PortMappingProtocolEnum, Manage As Boolean, NumberOfPorts As Integer, ByRef PortListing As String) As Boolean Implements IIGD2connSCPD.GetListOfPortMappings
        Return TR064Start(ServiceFile, "GetListOfPortMappings",
                  New Dictionary(Of String, String) From {{"NewStartPort", StartPort.ToString},
                                                          {"NewEndPort", EndPort.ToString},
                                                          {"NewProtocol", Protocol.ToString},
                                                          {"NewManage", Manage.ToBoolStr},
                                                          {"NewNumberOfPorts", NumberOfPorts.ToString}}).TryGetValueEx("NewPreferedLifetime", PortListing)

    End Function

    Public Function AddAnyPortMapping(NewPortMappingEntry As PortMappingEntry, ByRef ReservedPort As Integer) As Boolean Implements IIGD2connSCPD.AddAnyPortMapping
        If NewPortMappingEntry IsNot Nothing Then
            With NewPortMappingEntry
                Return TR064Start(ServiceFile, "AddAnyPortMapping",
                                  New Dictionary(Of String, String) From {{"NewRemoteHost", .RemoteHost},
                                                                          {"NewExternalPort", .ExternalPort.ToString},
                                                                          {"NewProtocol", .PortMappingProtocol.ToString},
                                                                          {"NewInternalPort", .InternalPort.ToString},
                                                                          {"NewInternalClient", .InternalClient},
                                                                          {"NewEnabled", .PortMappingEnabled.ToBoolStr},
                                                                          {"NewPortMappingDescription", .PortMappingDescription},
                                                                          {"NewLeaseDuration", .PortMappingLeaseDuration.ToString}}).
                                                                          TryGetValueEx("NewReservedPort", ReservedPort)
            End With
        Else
            Return False
        End If
    End Function
#End Region


End Class