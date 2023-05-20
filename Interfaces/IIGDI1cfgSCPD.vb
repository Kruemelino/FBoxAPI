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
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANCommonInterfaceConfig-v1-Service.pdf"/>
''' </remarks> 

Public Interface IIGDI1cfgSCPD
    Inherits IServiceBase

#Region "WANCommonInterfaceConfig:1"
    ''' <summary>
    ''' This action retrieves physical link properties of the WAN interface (WANDevice). 
    ''' </summary>
    ''' <param name="WANAccessType">This variable specifies the type of WAN access (modem) between the residential network and the 
    ''' Internet Service Provider (ISP). Ethernet refers to an Ethernet-attached external modem. 
    ''' Other values are self-explanatory.</param>
    ''' <param name="Layer1UpstreamMaxBitRate">This variable specifies the maximum downstream (from the ISP to WANDevice) theoretical bit rate (in bits per second) for the WAN device. For example, 56000 for a POTS V.90 modem.</param>
    ''' <param name="Layer1DownstreamMaxBitRate">This variable specifies the maximum downstream (from the ISP to WANDevice) theoretical bit rate (in bits per second) for the WAN device. For example, 56000 for a POTS V.90 modem.</param>
    ''' <param name="PhysicalLinkStatus">This variable indicates the state of the physical connection (link) from WANDevice to a connected entity (could be ISP CO for an integrated modem and Ethernet link status for an external Ethernet-connected modem).</param>
    Function GetCommonLinkProperties(ByRef WANAccessType As WANAccessTypeEnum,
                                     ByRef Layer1UpstreamMaxBitRate As Integer,
                                     ByRef Layer1DownstreamMaxBitRate As Integer,
                                     ByRef PhysicalLinkStatus As LinkStatusEnum) As Boolean

    ''' <summary>
    ''' This action retrieves the cumulative count of bytes sent upstream across all connection instances on a WANDevice. 
    ''' </summary>
    ''' <param name="TotalBytesSent">Represents the total number of bytes sent over all interfaces during their connection interval. If an interface establish a new connection, its counter gets reset to 0.</param>
    Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the cumulative count of bytes received downstream across all connection instances on a WANDevice. 
    ''' </summary>
    ''' <param name="TotalBytesReceived">Represents the total number of bytes received over all interfaces during their connection interval. If an interface establish a new connection, its counter gets reset to 0.</param>
    Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the cumulative count of IP or PPP packets sent upstream across all connection instances on a WANDevice. 
    ''' </summary>
    ''' <param name="TotalPacketsSent">Represents the total number of packets sent over all interfaces during their connection interval. If an interface establish a new connection, its counter gets reset to 0.</param>
    Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the cumulative count of IP or PPP packets sent upstream across all connection instances on a WANDevice. 
    ''' </summary>
    ''' <param name="TotalPacketsReceived">Represents the total number of packets received over all interfaces during their connection interval. If an interface establish a new connection, its counter gets reset to 0.</param>
    Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean
#End Region

#Region "Additional actions"
    Function GetAddonInfos(ByRef Info As WANAddonInfo) As Boolean
    Function GetDsliteStatus(ByRef DsliteStatus As Boolean) As Boolean
    Function GetIPTVInfos(ByRef IPTV_Enabled As Boolean,
                          ByRef IPTV_Provider As String,
                          ByRef IPTV_URL As String) As Boolean
#End Region

End Interface
