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
''' WANIPv6FirewallControl:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPv6FirewallControl-v1-Service.pdf"/>
''' </remarks> 
Public Interface IIGD2ipv6fwcSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' This action is to detect if the firewall is active and allows creating pinholes by control points.
    ''' </summary>
    ''' <param name="FirewallEnabled">FirewallEnabled has type boolean and informs if the firewall is active. </param>
    ''' <param name="InboundPinholeAllowed">InboundPinholeAllowed has type boolean and it informs if inbound pinhole can be created through UPnP.</param>
    Function GetFirewallStatus(ByRef FirewallEnabled As Boolean, ByRef InboundPinholeAllowed As Boolean) As Boolean

    ''' <summary>
    ''' This action returns the outbound pinhole timeout for the "automatic pinhole" defined by arguments
    ''' </summary>
    ''' <param name="RemoteHost">RemoteHost is string type variable that describes source address for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="RemotePort">RemotePort is ui2 type variable that describes source port for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="InternalClient">InternalClient is string type variable that describes destination address for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="InternalPort">InternalPort is ui2 type variable that describes destination port for the outbound pinhole. This can be wildcarded.</param>
    ''' <param name="Protocol">Protocol is ui2 type variable that describes the protocol for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="OutboundPinholeTimeout">OutboundPinholeTimeout is ui4 type variable that defines outbound pinhole timeout for "automatic pinhole". </param>
    Function GetOutboundPinholeTimeout(RemoteHost As String,
                                       RemotePort As Integer,
                                       InternalClient As String,
                                       InternalPort As Integer,
                                       Protocol As Integer,
                                       ByRef OutboundPinholeTimeout As Integer) As Boolean

    ''' <summary>
    ''' This action allows a control point to create a new pinhole that allows incoming traffic to pass through firewall. This action can also be used by a control point to extend the lease time of an existing pinhole. 
    ''' </summary>
    ''' <param name="RemoteHost">RemoteHost is string type variable that describes source address for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="RemotePort">RemotePort is ui2 type variable that describes source port for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="InternalClient">InternalClient is string type variable that describes destination address for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="InternalPort">InternalPort is ui2 type variable that describes destination port for the outbound pinhole. This can be wildcarded.</param>
    ''' <param name="Protocol">Protocol is ui2 type variable that describes the protocol for the outbound pinhole. This can be wildcarded. </param>
    ''' <param name="LeaseTime">LeaseTime is ui4 type variable that defines expiration time of the pinhole. </param>
    ''' <param name="UniqueID">UniqueID is ui2 type variable that identifies the firewall pinhole. </param>
    Function AddPinhole(RemoteHost As String,
                        RemotePort As Integer,
                        InternalClient As String,
                        InternalPort As Integer,
                        Protocol As Integer,
                        LeaseTime As Integer,
                        ByRef UniqueID As Integer) As Boolean

    ''' <summary>
    ''' This action updates a pinhole’s lease time. 
    ''' </summary>
    ''' <param name="UniqueID">UniqueID argument gives unique identifier assigned earlier by the gateway. Type is ui2. </param>
    ''' <param name="NewLeaseTime">NewLeaseTime defines how long pinhole will exist.</param>
    Function UpdatePinhole(UniqueID As Integer, NewLeaseTime As Integer) As Boolean

    ''' <summary>
    ''' This action removes a pinhole.  
    ''' </summary>
    ''' <param name="UniqueID">This argument gives unique identifier assigned earlier by the gateway. Type is ui2. </param>
    ''' <returns></returns>
    Function DeletePinhole(UniqueID As Integer) As Boolean

    ''' <summary>
    ''' This action allows a control point to get the total number of IP packets which have been going through the specified pinhole. 
    ''' </summary>
    ''' <param name="UniqueID">UniqueID is unique identifier for a pinhole returned by AddPinhole() action. Type is ui2. </param>
    ''' <param name="PinholePackets">PinholePackets is a ui4 type variable describing how many IP packets have been going through the specified pinhole. </param>
    Function GetPinholePackets(UniqueID As Integer, ByRef PinholePackets As Integer) As Boolean

    ''' <summary>
    ''' This action allows a control point to verify if a certain pinhole allows traffic to pass through the firewall.
    ''' </summary>
    ''' <param name="UniqueID">UniqueID is unique identifier for a pinhole returned by AddPinhole() action. Type is ui2</param>
    ''' <param name="IsWorking">IsWorking is a boolean type variable describing if specified pinhole will allow traffic to pass. </param>
    Function CheckPinholeWorking(UniqueID As Integer, ByRef IsWorking As Boolean) As Boolean
End Interface
