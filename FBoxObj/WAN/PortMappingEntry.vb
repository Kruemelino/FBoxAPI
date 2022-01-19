Public Class PortMappingEntry
    ''' <summary>
    ''' This variable represents the source of inbound IP packets. 
    ''' This will be a wildcard in most cases (i.e. an empty string). NAT vendors are only required to support wildcards. 
    ''' A non-wildcard value will allow for “narrow” port mappings, which may be desirable in some usage scenarios. 
    ''' When RemoteHost is a wildcard, all traffic sent to the ExternalPort on the WAN interface of the gateway is forwarded to the InternalClient on the InternalPort. 
    ''' When RemoteHost is specified as one external IP address as opposed to a wildcard, the NAT will only forward inbound packets from this RemoteHost to the InternalClient, all other packets will be dropped.
    ''' </summary>
    Public Property RemoteHost As String

    ''' <summary>
    ''' This variable represents the external port that the NAT gateway would "listen" on for connection requests to a corresponding InternalPort on an InternalClient.. 
    ''' Inbound packets to this external port on the WAN interface of the gateway should be forwarded to InternalClient on the InternalPort on which the message was received. 
    ''' If this value is specified as a wildcard (i.e. 0), connection request on all external ports (that are not otherwise mapped) will be forwarded to InternalClient. 
    ''' In the wildcard case, the value(s) of InternalPort on InternalClient are ignored by the IGD for those connections that are forwarded to InternalClient. 
    ''' Obviously only one such entry can exist in the NAT at any time and conflicts are handled with a "first write wins" behavior. 
    ''' </summary>
    Public Property ExternalPort As Integer

    ''' <summary>
    ''' This variable represents the protocol of the port mapping. Possible values are TCP or UDP.
    ''' </summary>
    Public Property PortMappingProtocol As PortMappingProtocolEnum

    ''' <summary>
    ''' This variable represents the port on InternalClient that the gateway should forward connection requests to. 
    ''' A value of 0 is not allowed. NAT implementations that do not permit different values for ExternalPort and InternalPort will return an error.
    ''' </summary>
    Public Property InternalPort As Integer

    ''' <summary>
    ''' This variable represents the IP address or DNS host name of an internal client (on the residential LAN). 
    ''' Note that if the gateway does not support DHCP, it does not have to support DNS host names. 
    ''' Consequently, support for an IP address is mandatory and support for DNS host names is recommended. 
    ''' This value cannot be a wildcard (i.e. empty string). It must be possible to set the InternalClient to the broadcast IP address 255.255.255.255 for UDP mappings. 
    ''' This is to enable multiple NAT clients to use the same well-known port simultaneously.
    ''' </summary>
    Public Property InternalClient As String

    ''' <summary>
    ''' This variable allows security conscious users to disable and enable dynamic and static NAT port mappings on the IGD.
    ''' </summary>
    Public Property PortMappingEnabled As Boolean

    ''' <summary>
    ''' This variable represents the external port that the NAT gateway would "listen" on for connection requests to a corresponding InternalPort on an InternalClient.. 
    ''' Inbound packets to this external port on the WAN interface of the gateway should be forwarded to InternalClient on the InternalPort on which the message was received. 
    ''' If this value is specified as a wildcard (i.e. 0), connection request on all external ports (that are not otherwise mapped) will be forwarded to InternalClient. 
    ''' In the wildcard case, the value(s) of InternalPort on InternalClient are ignored by the IGD for those connections that are forwarded to InternalClient. 
    ''' Obviously only one such entry can exist in the NAT at any time and conflicts are handled with a "first write wins" behavior. 
    ''' </summary>
    Public Property PortMappingDescription As String

    ''' <summary>
    ''' This variable determines the time to live in seconds of a port-mapping lease. 
    ''' A value of 0 means the port mapping is static. Non-zero values will allow support for dynamic port mappings. 
    ''' Note that static port mappings do not necessarily mean persistence of these mappings across device resets or reboots. 
    ''' It is up to a gateway vendor to implement persistence as appropriate for their IGD device.
    ''' </summary>
    Public Property PortMappingLeaseDuration As Integer
End Class
