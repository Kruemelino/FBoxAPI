''' <summary>
''' TR-064 Support – WANCommonInterfaceConfig
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf</see>
''' </summary>
Public Interface IWANCommonInterfaceConfigSCPD
    Inherits IServiceBase

    Function GetCommonLinkProperties(ByRef WANAccessType As String,
                                     ByRef Layer1UpstreamMaxBitRate As Integer,
                                     ByRef Layer1DownstreamMaxBitRate As Integer,
                                     ByRef PhysicalLinkStatus As LinkStatusEnum,
                                     ByRef DownStreamCurrentUtilization As String,
                                     ByRef UpstreamCurrentUtilization As String,
                                     ByRef DownstreamCurrentMaxSpeed As Integer,
                                     ByRef UpstreamCurrentMaxSpeed As Integer) As Boolean

    ''' <summary>
    ''' Returns the total number of bytes sent over all interfaces during their connection interval. 
    ''' If an interface establish a new connection, its counter gets reset to 0.
    ''' </summary>
    Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean

    ''' <summary>
    ''' Returns the total number of bytes received over all interfaces during their connection interval. 
    ''' If an interface establish a new connection, its counter gets reset to 0.
    ''' </summary>
    Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean

    ''' <summary>
    ''' Returns the total number of packets sent over all interfaces during their connection interval. 
    ''' If an interface establish a new connection, its counter gets reset to 0.
    ''' </summary>
    Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean

    ''' <summary>
    ''' Returns the total number of packets received over all interfaces during their connection interval. 
    ''' If an interface establish a new connection, its counter gets reset to 0.
    ''' </summary>
    Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean

    ''' <param name="AccessType">DSL, Ethernet, X_AVM-DE_Fiber, X_AVMDE_UMTS, X_AVM-DE_Cable, X_AVM-DE_LTE, unknown</param>
    Function SetWANAccessType(AccessType As String) As Boolean

    Function GetActiveProvider(ByRef Provider As String) As Boolean

    Function GetOnlineMonitor(ByRef OnlineMonitor As OnlineMonitor, SyncGroupIndex As Integer) As Boolean

End Interface