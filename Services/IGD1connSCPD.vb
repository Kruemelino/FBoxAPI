Imports System.Data
Imports System.Xml
''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD1.pdf</see>
''' </summary>
''' <remarks>
''' Based on the Internet Gateway Device (IGD) V1.0 and Internet Gateway Device (IGD) V2.0 
''' specification proposed by UpnP™ Forum at <see href="http://upnp.org/specs/gw/igd1/"/> and 
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANCommonInterfaceConfig:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPConnection-v1-Service.pdf"/>
''' </remarks> 
Public Class IGD1connSCPD
    Implements IIGD1connSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IIGD1connSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IIGD1connSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.igd1connSCPD Implements IIGD1connSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IIGD1connSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

#Region "WANIPConnection:1"
    Public Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean Implements IIGD1connSCPD.SetConnectionType
        Return Not TR064Start(ServiceFile, "SetConnectionType", ServiceID, New Dictionary(Of String, String) From {{"NewConnectionType", ConnectionType.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As String) As Boolean Implements IIGD1connSCPD.GetConnectionTypeInfo
        With TR064Start(ServiceFile, "GetConnectionTypeInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewConnectionType", ConnectionType) And
                   .TryGetValueEx("NewPossibleConnectionTypes", PossibleConnectionTypes)
        End With
    End Function

    Public Function RequestConnection() As Boolean Implements IIGD1connSCPD.RequestConnection
        Return Not TR064Start(ServiceFile, "RequestConnection", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function RequestTermination() As Boolean Implements IIGD1connSCPD.RequestTermination
        Return Not TR064Start(ServiceFile, "RequestTermination", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function ForceTermination() As Boolean Implements IIGD1connSCPD.ForceTermination
        Return Not TR064Start(ServiceFile, "ForceTermination", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum, ByRef LastConnectionError As LastConnectionErrorEnum, ByRef Uptime As Integer) As Boolean Implements IIGD1connSCPD.GetStatusInfo
        With TR064Start(ServiceFile, "GetStatusInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewConnectionStatus", ConnectionStatus) And
                   .TryGetValueEx("NewLastConnectionError", LastConnectionError) And
                   .TryGetValueEx("NewUptime", Uptime)
        End With
    End Function

    Public Function GetAutoDisconnectTime(ByRef AutoDisconnectTime As Integer) As Boolean Implements IIGD1connSCPD.GetAutoDisconnectTime
        Return TR064Start(ServiceFile, "GetAutoDisconnectTime", ServiceID, Nothing).TryGetValueEx("NewAutoDisconnectTime", AutoDisconnectTime)
    End Function

    Public Function GetIdleDisconnectTime(ByRef IdleDisconnectTime As Integer) As Boolean Implements IIGD1connSCPD.GetIdleDisconnectTime
        Return TR064Start(ServiceFile, "GetIdleDisconnectTime", ServiceID, Nothing).TryGetValueEx("NewIdleDisconnectTime", IdleDisconnectTime)
    End Function

    Public Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean, ByRef NATEnabled As Boolean) As Boolean Implements IIGD1connSCPD.GetNATRSIPStatus
        With TR064Start(ServiceFile, "GetNATRSIPStatus", ServiceID, Nothing)

            Return .TryGetValueEx("NewRSIPAvailable", RSIPAvailable) And
                   .TryGetValueEx("NewNATEnabled", NATEnabled)
        End With
    End Function

    Public Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD1connSCPD.GetGenericPortMappingEntry
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

    Public Function GetSpecificPortMappingEntry(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum, ByRef SpecificPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD1connSCPD.GetSpecificPortMappingEntry
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

    Public Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean Implements IIGD1connSCPD.AddPortMapping
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

    Public Function DeletePortMapping(RemoteHost As String, ExternalPort As Integer, PortMappingProtocol As PortMappingProtocolEnum) As Boolean Implements IIGD1connSCPD.DeletePortMapping
        Return TR064Start(ServiceFile, "DeletePortMapping", ServiceID,
                New Dictionary(Of String, String) From {{"NewRemoteHost", RemoteHost},
                                                        {"NewExternalPort", ExternalPort.ToString},
                                                        {"NewProtocol", PortMappingProtocol.ToString}}).ContainsKey("Error")

    End Function

    Public Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean Implements IIGD1connSCPD.GetExternalIPAddress
        Return TR064Start(ServiceFile, "GetExternalIPAddress", ServiceID, Nothing).TryGetValueEx("NewExternalIPAddress", ExternalIPAddress)
    End Function
#End Region

#Region "Additional actions"
    Public Function GetDNSServer(ByRef IPv4DNSServer1 As String, ByRef IPv4DNSServer2 As String) As Boolean Implements IIGD1connSCPD.GetDNSServer
        With TR064Start(ServiceFile, "X_AVM_DE_GetDNSServer", ServiceID, Nothing)

            Return .TryGetValueEx("NewIPv4DNSServer1", IPv4DNSServer1) And
                   .TryGetValueEx("NewIPv4DNSServer2", IPv4DNSServer2)
        End With
    End Function
    Public Function GetIPv6DNSServer(ByRef IPv6DNSServer1 As String, ByRef ValidLifetime1 As Integer, ByRef IPv6DNSServer2 As String, ByRef ValidLifetime2 As Integer) As Boolean Implements IIGD1connSCPD.GetIPv6DNSServer
        With TR064Start(ServiceFile, "X_AVM_DE_GetIPv6DNSServer", ServiceID, Nothing)

            Return .TryGetValueEx("NewIPv6DNSServer1", IPv6DNSServer1) And
                   .TryGetValueEx("NewValidLifetime1", ValidLifetime1) And
                   .TryGetValueEx("NewIPv6DNSServer2", ValidLifetime2) And
                   .TryGetValueEx("NewValidLifetime2", ValidLifetime2)
        End With
    End Function

    Public Function GetExternalIPv6Address(ByRef ExternalIPv6Address As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean Implements IIGD1connSCPD.GetExternalIPv6Address
        With TR064Start(ServiceFile, "X_AVM_DE_GetExternalIPv6Address", ServiceID, Nothing)

            Return .TryGetValueEx("NewExternalIPv6Address", ExternalIPv6Address) And
                   .TryGetValueEx("NewPrefixLength", PrefixLength) And
                   .TryGetValueEx("NewValidLifetime", ValidLifetime) And
                   .TryGetValueEx("NewPreferedLifetime", PreferedLifetime)
        End With
    End Function

    Public Function GetIPv6Prefix(ByRef IPv6Prefix As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean Implements IIGD1connSCPD.GetIPv6Prefix
        With TR064Start(ServiceFile, "X_AVM_DE_GetIPv6Prefix", ServiceID, Nothing)

            Return .TryGetValueEx("NewIPv6Prefix", IPv6Prefix) And
                   .TryGetValueEx("NewPrefixLength", PrefixLength) And
                   .TryGetValueEx("NewValidLifetime", ValidLifetime) And
                   .TryGetValueEx("NewPreferedLifetime", PreferedLifetime)
        End With
    End Function
#End Region

End Class
