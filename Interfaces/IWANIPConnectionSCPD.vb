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
Public Interface IWANIPConnectionSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As WANInfo) As Boolean
    Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum,
                                   ByRef PossibleConnectionTypes As ConnectionTypeEnum) As Boolean
    Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean

    Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum,
                           ByRef LastConnectionError As LastConnectionErrorEnum,
                           ByRef NewUptime As Integer) As Boolean

    Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean,
                              ByRef NATEnabled As Boolean) As Boolean

    Function SetConnectionTrigger(ConnectionTrigger As String) As Boolean

    Function ForceTermination() As Boolean

    Function RequestConnection() As Boolean

    Function X_GetDNSServers(ByRef DNSServers As String) As Boolean

    Function GetPortMappingNumberOfEntries(ByRef PortMappingNumberOfEntries As Integer) As Boolean

    Function GetGenericPortMappingEntry(PortMappingIndex As Integer, ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean

    Function GetSpecificPortMappingEntry(RemoteHost As String,
                                         ExternalPort As Integer,
                                         PortMappingProtocol As PortMappingProtocolEnum,
                                         ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean

    ''' <summary>
    ''' Port mapping entries are only allowed for hosts inside of LAN. 
    ''' Furthermore hosts can only add port mapping entries for themselves and not for other hosts in the LAN. 
    ''' It is not intended to allow port mapping entries for the guest network or hosts with IP addresses routed into WAN. 
    ''' </summary>
    Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean

    Function DeletePortMapping(RemoteHost As String,
                               ExternalPort As Integer,
                               PortMappingProtocol As PortMappingProtocolEnum) As Boolean

    Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean

    Function SetRouteProtocolRx(RouteProtocolRx As String) As Boolean

    Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean
End Interface
