''' <summary>
''' TR-064 Support – WAN Ethernet Link Config
''' Date: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanethlinkconfigSCPD.pdf</see>
''' </summary>
Public Interface IWANEthernetLinkConfigSCPD
    Inherits IServiceBase
    Function GetEthernetLinkStatus(ByRef EthernetLinkStatus As EthernetLinkStatusEnum)

End Interface