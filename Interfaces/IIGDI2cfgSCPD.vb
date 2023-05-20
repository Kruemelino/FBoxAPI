''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD2.pdf</see>
''' </summary>
''' <remarks>
''' BBased on the Internet Gateway Device (IGD) V2.0 specification proposed by UpnP™ Forum at
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANCommonInterfaceConfig:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANCommonInterfaceConfig-v1-Service.pdf"/>
''' </remarks> 
Public Interface IIGDI2dslSCPD
    Inherits IIGDI1dslSCPD
End Interface
