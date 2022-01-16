''' <summary>
''' TR-064 Support – WANCommonInterfaceConfig
''' Date: 2018-09-05
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf</see>
''' </summary>
Public Interface IWANCommonInterfaceConfigSCPD
    Inherits IServiceBase

    Function GetCommonLinkProperties(ByRef WANAccessType As AccessTypeEnum,
                                     ByRef Layer1UpstreamMaxBitRate As Integer,
                                     ByRef Layer1DownstreamMaxBitRate As Integer,
                                     ByRef PhysicalLinkStatus As PhysicalLinkStatusEnum) As Boolean

    ''' <summary>
    ''' Needs IGD to work
    ''' </summary>
    Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean

    ''' <summary>
    ''' Needs IGD to work
    ''' </summary>
    Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean

    ''' <summary>
    ''' Needs IGD to work
    ''' </summary>
    Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean

    ''' <summary>
    ''' Needs IGD to work
    ''' </summary>
    Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean

    Function SetWANAccessType(AccessType As AccessTypeEnum) As Boolean

    Function GetOnlineMonitor(ByRef OnlineMonitor As OnlineMonitor, SyncGroupIndex As Integer) As Boolean

End Interface