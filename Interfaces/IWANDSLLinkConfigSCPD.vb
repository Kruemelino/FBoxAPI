''' <summary>
''' TR-064 Support – WANDSLLinkConfig
''' Date: 2015-11-20 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandsllinkconfigSCPD.pdf</see>
''' </summary>
Public Interface IWANDSLLinkConfigSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As DSLLinkInfo) As Boolean

    ''' <summary>
    ''' Default connection cannot be disabled. 
    ''' </summary>
    Function SetEnable(Enable As Boolean) As Boolean
    Function SetDSLLinkType(LinkType As LinkTypeEnum) As Boolean
    Function GetDSLLinkInfo(ByRef LinkType As LinkTypeEnum,
                            ByRef LinkStatus As LinkStatusEnum) As Boolean
    Function SetDestinationAddress(DestinationAddress As String) As Boolean
    Function GetDestinationAddress(ByRef DestinationAddress As String) As Boolean
    Function SetATMEncapsulation(ATMEncapsulation As String) As Boolean
    Function GetATMEncapsulation(ByRef ATMEncapsulation As String) As Boolean
    Function GetAutoConfig(ByRef AutoConfig As Boolean) As Boolean
    Function GetStatistics(ByRef ATMTransmittedBlocks As Integer,
                           ByRef ATMReceivedBlocks As Integer,
                           ByRef AAL5CRCErrors As Integer,
                           ByRef ATMCRCErrors As Integer) As Boolean

End Interface
