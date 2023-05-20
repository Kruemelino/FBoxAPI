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
Public Interface IIGD1connSCPD
    Inherits IServiceBase

#Region "WANIPConnection:1"

    ''' <summary>
    ''' This action sets up a specific connection type. Clients on the LAN may initiate or share connection only after this action completes or ConnectionType is set to a value other than Unconfigured. 
    ''' ConnectionType can be a read-only variable in cases where some form of auto configuration is employed. 
    ''' </summary>
    ''' <param name="ConnectionType">This variable is set to specify the connection type for a specific active connection.</param>
    Function SetConnectionType(ConnectionType As ConnectionTypeEnum) As Boolean

    ''' <summary>
    ''' This action retrieves the values of the current connection type and allowable connection types. 
    ''' </summary>
    ''' <param name="ConnectionType">This variable is set to specify the connection type for a specific active connection. </param>
    ''' <param name="PossibleConnectionTypes">This variable represents a comma-separated string indicating the types of connections possible in the context of a specific modem and link type.</param>
    Function GetConnectionTypeInfo(ByRef ConnectionType As ConnectionTypeEnum, ByRef PossibleConnectionTypes As String) As Boolean

    ''' <summary>
    ''' A client sends this action to initiate a connection on an instance of a connection service that has a configuration already defined. 
    ''' RequestConnection causes the ConnectionStatus to immediately change to Connecting (if implemented) unless the action is not permitted in the current state of the IGD or the specific service instance. 
    ''' This change of state will be evented.
    ''' RequestConnection should synchronously return at this time in accordance with UPnP architecture requirements that mandate that an action can take no more than 30 seconds to respond synchronously. 
    ''' However, the actual connection setup may take several seconds more to complete. 
    ''' If the connection setup is successful, ConnectionStatus will change to Connected and will be evented. 
    ''' If the connection setup is not successful, ConnectionStatus will eventually revert back to Disconnected and will be evented. 
    ''' LastConnectionError will be set appropriately in either case. 
    ''' While this may be obvious, it is worth noting that a control point must not source packets to the Internet until ConnectionStatus is updated to Connected, or the IGD may drop packets until it transitions to the Connected state. 
    ''' The following implementation guidelines are also worth noting: 
    ''' <list type="bullet">
    ''' <item>The IGD should implement a timeout mechanism to ensure that it does not remain in the Connecting state forever. The timeout values are implementation dependent.</item>
    ''' <item>The IGD may take several seconds (or even a few minutes) to transition from the Connecting state to the Connected state. Control points should moderate the polling frequency of the ConnectionStatus variable on the IGD so as to not create data storms on the network</item>
    ''' <item>Control points should manage a timeout for initiated connections to recover from catastrophic failures on the IGD. The timeout values are implementation dependent.</item>
    ''' </list>
    ''' </summary>
    Function RequestConnection() As Boolean

    ''' <summary>
    ''' A client may send this command to any connection instance in Connected or Connecting state to change ConnectionStatus to Disconnected. 
    ''' Connection state changes to PendingDisconnect depending on the value of WarnDisconnectDelay variable. 
    ''' Connection termination will depend on whether other clients intend to continue to use the connection. 
    ''' </summary>
    Function RequestTermination() As Boolean

    ''' <summary>
    ''' A client may send this command to any connection instance in Connected,Connecting, PendingDisconnect or Disconnecting state to change ConnectionStatus to Disconnected. 
    ''' Connection state immediately transitions to Disconnected irrespective of the setting of WarnDisconnectDelay variable. 
    ''' </summary>
    Function ForceTermination() As Boolean

    ''' <summary>
    ''' This action retrieves the values of state variables pertaining to connection status.
    ''' </summary>
    ''' <param name="ConnectionStatus">This variable represents current status of an Internet connection. </param>
    ''' <param name="LastConnectionError">This variable is a string that provides information about the cause of failure for the last connection setup attempt. </param>
    ''' <param name="Uptime">This variable represents the time in seconds that this connection has stayed up</param>
    Function GetStatusInfo(ByRef ConnectionStatus As ConnectionStatusEnum,
                           ByRef LastConnectionError As LastConnectionErrorEnum,
                           ByRef Uptime As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the values of various timeouts related to the termination of a connection. 
    ''' </summary>
    ''' <param name="AutoDisconnectTime">This variable represents time in seconds (since the establishment of the connection – measured from the time ConnectionStatus transitions to Connected), 
    ''' after which connection termination is automatically initiated by the gateway.</param>
    Function GetAutoDisconnectTime(ByRef AutoDisconnectTime As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the values of various timeouts related to the termination of a connection. 
    ''' </summary>
    ''' <param name="IdleDisconnectTime">It represents the idle time of a connection in seconds (since the establishment of the connection), after which connection termination is initiated by the gateway.</param>
    Function GetIdleDisconnectTime(ByRef IdleDisconnectTime As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the current state of NAT and RSIP on the gateway for this connection. 
    ''' </summary>
    ''' <param name="RSIPAvailable">This variable indicates if Realm-specific IP (RSIP) is available as a feature on the InternetGatewayDevice. 
    ''' RSIP is being defined in the NAT working group in the IETF to allow hostNATing using a standard set of message exchanges. 
    ''' It also allows end-to-end applications that otherwise break if NAT is introduced (e.g. IPsec-based VPNs). 
    ''' A gateway that does not support RSIP should set this variable to 0.</param>
    ''' <param name="NATEnabled">This variable indicates if Network Address Translation (NAT) is enabled for this connection.</param>
    Function GetNATRSIPStatus(ByRef RSIPAvailable As Boolean,
                              ByRef NATEnabled As Boolean) As Boolean
    ''' <summary>
    ''' This action retrieves NAT port mappings one entry at a time. Control points can call this action
    ''' with an incrementing array index until no more entries are found on the gateway. 
    ''' If PortMappingNumberOfEntries is updated during a call, the process may have to start over.
    ''' Entries in the array are contiguous. 
    ''' As entries are deleted, the array is compacted, and the evented variable PortMappingNumberOfEntries is decremented. 
    ''' Port mappings are logically stored as an array on the IGD and retrieved using an array index ranging from 0 to PortMappingNumberOfEntries-1. 
    ''' </summary>
    Function GetGenericPortMappingEntry(PortMappingIndex As Integer,
                                        ByRef GenericPortMappingEntry As PortMappingEntry) As Boolean

    ''' <summary>
    ''' This action reports the Static Port Mapping specified by the unique tuple of RemoteHost, ExternalPort and PortMappingProtocol.  
    ''' </summary>
    ''' <param name="RemoteHost">
    ''' This variable represents the source of inbound IP packets. This will be a wildcard in most cases
    ''' (i.e. an empty string). NAT vendors are only required to support wildcards. A non-wildcard value
    ''' will allow for “narrow” port mappings, which may be desirable in some usage scenarios.When
    ''' RemoteHost is a wildcard, all traffic sent to the ExternalPort on the WAN interface of the
    ''' gateway is forwarded to the InternalClient on the InternalPort. When RemoteHost is
    ''' specified as one external IP address as opposed to a wildcard, the NAT will only forward inbound
    ''' packets from this RemoteHost to the InternalClient, all other packets will be dropped. </param>
    ''' <param name="PortMappingProtocol">This variable represents the protocol of the port mapping. Possible values are TCP or UDP. </param>
    Function GetSpecificPortMappingEntry(RemoteHost As String,
                                         ExternalPort As Integer,
                                         PortMappingProtocol As PortMappingProtocolEnum,
                                         ByRef SpecificPortMappingEntry As PortMappingEntry) As Boolean

    ''' <summary>
    ''' This action creates a new port mapping or overwrites an existing mapping with the same internal client. If the ExternalPort and PortMappingProtocol pair is already mapped to another internal client, an error is returned. 
    ''' </summary>
    Function AddPortMapping(NewPortMappingEntry As PortMappingEntry) As Boolean

    ''' <summary>
    ''' This action deletes a previously instantiated port mapping. As each entry is deleted, the array is compacted, and the evented variable PortMappingNumberOfEntries is decremented. 
    ''' </summary>
    Function DeletePortMapping(RemoteHost As String,
                               ExternalPort As Integer,
                               PortMappingProtocol As PortMappingProtocolEnum) As Boolean

    ''' <summary>
    ''' This action retrieves the value of the external IP address on this connection instance. 
    ''' </summary>
    ''' <param name="ExternalIPAddress">This is the external IP address used by NAT for the connection. </param>
    Function GetExternalIPAddress(ByRef ExternalIPAddress As String) As Boolean

#End Region

#Region "Additional actions"
    Function GetDNSServer(ByRef IPv4DNSServer1 As String, ByRef IPv4DNSServer2 As String) As Boolean
    Function GetIPv6DNSServer(ByRef IPv6DNSServer1 As String, ByRef ValidLifetime1 As Integer, ByRef IPv6DNSServer2 As String, ByRef ValidLifetime2 As Integer) As Boolean
    Function GetExternalIPv6Address(ByRef ExternalIPv6Address As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean
    Function GetIPv6Prefix(ByRef IPv6Prefix As String, ByRef PrefixLength As Integer, ByRef ValidLifetime As Integer, ByRef PreferedLifetime As Integer) As Boolean
#End Region

End Interface
