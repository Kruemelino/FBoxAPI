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
''' WANDSLLinkConfig:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANDSLLinkConfig-v1-Service.pdf"/>
''' </remarks> 
Public Interface IIGDI1dslSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' This action retrieves the type of DSL physical connection and the status of the link of the WANConnectionDevice device. 
    ''' </summary>
    Function GetDSLLinkInfo(ByRef LinkType As LinkTypeEnum, LinkStatus As LinkStatusEnum) As Boolean

    ''' <summary>
    ''' This action retrieves the variable that indicates if the modem is using an auto configuration mechanism.
    ''' </summary>
    ''' <param name="AutoConfig">This variable indicates if the modem is currently using some auto configuration mechanisms for this connection. AutoConfig specified by DSL Forum is one such mechanism. 
    ''' This variable is read-only. In this case, variables such as LinkType, DestinationAddress, ATMEncapsulation provided by the mechanism will become read-only. 
    ''' Any attempt to change one of these variables should result in a failure and an error should be returned. 
    ''' If a modem doesn't support such mechanisms, this variable should always be set to false (0). </param>
    Function GetAutoConfig(ByRef AutoConfig As Boolean) As Boolean

    ''' <summary>
    ''' This action retrieves the type of modulation used on the connection. 
    ''' </summary>
    ''' <param name="ModulationType">This variable indicates the type of modulation used on the connection<br/>
    ''' <list type="bullet">
    ''' <item>ADSL_G.dmt</item>
    ''' <item>ADSL_G.lite</item>
    ''' <item>G.shdsl</item>
    ''' <item>IDSL</item>
    ''' <item>HDSL</item>
    ''' <item>SDSL</item>
    ''' </list>
    ''' </param>
    Function GetModulationType(ByRef ModulationType As String) As Boolean

    ''' <summary>
    ''' This action retrieves the ATM destination address. 
    ''' </summary>
    ''' <param name="DestinationAddress">This variable indicates ATM destination address. This address identifies the other end of the WAN connection. It can define either a Permanent Virtual Circuit (PVC) or a Switched Virtual Circuit (SVC) according to a standard syntax.<br/>
    ''' For a PVC, syntax is "PVC:VPI/VCI", i.e. "PVC:8/23"<br/>
    ''' For a SVC, syntax can be either
    ''' <list type="bullet">
    ''' <item>"SVC:ATM connection name"</item>
    ''' <item>"SVC:ATM address"</item>
    ''' </list>
    ''' ATM address is a BCD number whose format can be either 
    ''' <list type="bullet">
    ''' <item>A NSAP format, itself in one of following three formats
    ''' <list type="bullet">
    ''' <item>DCC format</item>
    ''' <item>ICD format</item>
    ''' <item>E.164 format</item>
    ''' </list>
    ''' </item>
    ''' <item>A CCITT E.164 format</item>
    ''' </list>
    ''' </param>
    Function GetDestinationAddress(ByRef DestinationAddress As String) As Boolean

    ''' <summary>
    ''' This action retrieves the method to de/encapsulate IP or Ethernet packets from/to ATM payloads according to RFC 1483. 
    ''' </summary>
    ''' <param name="ATMEncapsulation">This variable indicates the method used to de/encapsulate IP or Ethernet packets from/to ATM payloads according to RFC 1483.</param>
    Function GetATMEncapsulation(ByRef ATMEncapsulation As ATMEncapsulationEnum) As Boolean

    ''' <summary>
    ''' This action retrieves the flag value that indicates if a checksum in the ATM payload should be added. 
    ''' </summary>
    ''' <param name="FCSPreserved">This flag tells if a checksum should be added in the ATM payload. It does not refer to the checksum of one of the ATM cells or AALX packets. In case of LLC or VCMUX encapsulation, this ATM checksum is the FCS field described in RFC 1483. It is only applicable in the upstream direction. The value of this variable is required for EoA and PPPoE link types. </param>
    Function GetFCSPreserved(ByRef FCSPreserved As Boolean) As Boolean

End Interface
