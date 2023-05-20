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
''' <seealso href="http://upnp.org/specs/gw/UpnP-gwWANDSLLinkConfig-v1-Service.pdf"/>
''' </remarks> 
Public Class IGDI2cfgSCPD
    Implements IIGDI2cfgSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IIGDI2cfgSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IIGDI2cfgSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.igd2icfgSCPD Implements IIGDI2cfgSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetCommonLinkProperties(ByRef WANAccessType As WANAccessTypeEnum,
                                            ByRef Layer1UpstreamMaxBitRate As Integer,
                                            ByRef Layer1DownstreamMaxBitRate As Integer,
                                            ByRef PhysicalLinkStatus As LinkStatusEnum) As Boolean Implements IIGDI1cfgSCPD.GetCommonLinkProperties

        With TR064Start(ServiceFile, "GetCommonLinkProperties", Nothing)

            Return .TryGetValueEx("NewWANAccessType", WANAccessType) And
                   .TryGetValueEx("NewLayer1UpstreamMaxBitRate", Layer1UpstreamMaxBitRate) And
                   .TryGetValueEx("NewLayer1DownstreamMaxBitRate", Layer1DownstreamMaxBitRate) And
                   .TryGetValueEx("NewPhysicalLinkStatus", PhysicalLinkStatus)
        End With

    End Function

    Public Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean Implements IIGDI1cfgSCPD.GetTotalBytesSent
        Return TR064Start(ServiceFile, "GetTotalBytesSent", Nothing).TryGetValueEx("NewTotalBytesSent", TotalBytesSent)
    End Function

    Public Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean Implements IIGDI1cfgSCPD.GetTotalBytesReceived
        Return TR064Start(ServiceFile, "GetTotalBytesReceived", Nothing).TryGetValueEx("NewTotalBytesReceived", TotalBytesReceived)
    End Function

    Public Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean Implements IIGDI1cfgSCPD.GetTotalPacketsSent
        Return TR064Start(ServiceFile, "GetTotalPacketsSent", Nothing).TryGetValueEx("NewTotalPacketsSent", TotalPacketsSent)
    End Function

    Public Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean Implements IIGDI1cfgSCPD.GetTotalPacketsReceived
        Return TR064Start(ServiceFile, "GetTotalPacketsReceived", Nothing).TryGetValueEx("NewTotalPacketsReceived", TotalPacketsReceived)
    End Function

    Public Function GetAddonInfos(ByRef Info As WANAddonInfo) As Boolean Implements IIGDI1cfgSCPD.GetAddonInfos
        If Info Is Nothing Then Info = New WANAddonInfo

        With TR064Start(ServiceFile, "GetAddonInfos", Nothing)

            Return .TryGetValueEx("NewByteSendRate", Info.ByteSendRate) And
                   .TryGetValueEx("NewByteReceiveRate", Info.ByteReceiveRate) And
                   .TryGetValueEx("NewPacketSendRate", Info.PacketSendRate) And
                   .TryGetValueEx("NewPacketReceiveRate", Info.PacketReceiveRate) And
                   .TryGetValueEx("NewTotalBytesSent", Info.TotalBytesSent) And
                   .TryGetValueEx("NewTotalBytesReceived", Info.TotalBytesReceived) And
                   .TryGetValueEx("NewAutoDisconnectTime", Info.AutoDisconnectTime) And
                   .TryGetValueEx("NewIdleDisconnectTime", Info.IdleDisconnectTime) And
                   .TryGetValueEx("NewDNSServer1", Info.DNSServer1) And
                   .TryGetValueEx("NewDNSServer2", Info.DNSServer2) And
                   .TryGetValueEx("NewVoipDNSServer1", Info.VoipDNSServer1) And
                   .TryGetValueEx("NewVoipDNSServer2", Info.VoipDNSServer2) And
                   .TryGetValueEx("NewUpnpControlEnabled", Info.UpnpControlEnabled) And
                   .TryGetValueEx("NewRoutedBridgedModeBoth", Info.RoutedBridgedModeBoth) And
                   .TryGetValueEx("NewX_AVM_DE_TotalBytesSent64", Info.TotalBytesSent64) And
                   .TryGetValueEx("NewX_AVM_DE_TotalBytesReceived64", Info.TotalBytesReceived64) And
                   .TryGetValueEx("NewX_AVM_DE_WANAccessType", Info.WANAccessType)

        End With
    End Function

    Public Function GetDsliteStatus(ByRef DsliteStatus As Boolean) As Boolean Implements IIGDI1cfgSCPD.GetDsliteStatus
        Return TR064Start(ServiceFile, "X_AVM_DE_GetDsliteStatus", Nothing).TryGetValueEx("NewX_AVM_DE_DsliteStatus", DsliteStatus)
    End Function

    Public Function GetIPTVInfos(ByRef IPTV_Enabled As Boolean,
                                 ByRef IPTV_Provider As String,
                                 ByRef IPTV_URL As String) As Boolean Implements IIGDI1cfgSCPD.GetIPTVInfos

        With TR064Start(ServiceFile, "X_AVM_DE_GetIPTVInfos", Nothing)

            Return .TryGetValueEx("NewX_AVM_DE_IPTV_Enabled", IPTV_Enabled) And
                   .TryGetValueEx("NewX_AVM_DE_IPTV_Provider", IPTV_Provider) And
                   .TryGetValueEx("NewX_AVM_DE_IPTV_URL", IPTV_URL)
        End With

    End Function
End Class
