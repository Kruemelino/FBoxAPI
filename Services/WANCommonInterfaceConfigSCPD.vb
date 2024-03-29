﻿''' <summary>
''' TR-064 Support – WANCommonInterfaceConfig
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf</see>
''' </summary>
Friend Class WANCommonInterfaceConfigSCPD
    Implements IWANCommonInterfaceConfigSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IWANCommonInterfaceConfigSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANCommonInterfaceConfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wancommonifconfigSCPD Implements IWANCommonInterfaceConfigSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IWANCommonInterfaceConfigSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetCommonLinkProperties(ByRef WANAccessType As String,
                                            ByRef Layer1UpstreamMaxBitRate As Integer,
                                            ByRef Layer1DownstreamMaxBitRate As Integer,
                                            ByRef PhysicalLinkStatus As LinkStatusEnum,
                                            ByRef DownStreamCurrentUtilization As String,
                                            ByRef UpstreamCurrentUtilization As String,
                                            ByRef DownstreamCurrentMaxSpeed As Integer,
                                            ByRef UpstreamCurrentMaxSpeed As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetCommonLinkProperties

        With TR064Start(ServiceFile, "GetCommonLinkProperties", ServiceID, Nothing)

            Return .TryGetValueEx("NewWANAccessType", WANAccessType) And
                   .TryGetValueEx("NewLayer1UpstreamMaxBitRate", Layer1UpstreamMaxBitRate) And
                   .TryGetValueEx("NewLayer1DownstreamMaxBitRate", Layer1DownstreamMaxBitRate) And
                   .TryGetValueEx("NewPhysicalLinkStatus", PhysicalLinkStatus) And
                   .TryGetValueEx("NewX_AVM-DE_DownstreamCurrentUtilization ", DownStreamCurrentUtilization) And
                   .TryGetValueEx("NewX_AVM-DE_UpstreamCurrentUtilization", UpstreamCurrentUtilization) And
                   .TryGetValueEx("NewX_AVM-DE_DownstreamCurrentMaxSpeed", DownstreamCurrentMaxSpeed) And
                   .TryGetValueEx("NewX_AVM-DE_UpstreamCurrentMaxSpeed", UpstreamCurrentMaxSpeed)

        End With

    End Function

    Public Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalPacketsSent
        Return TR064Start(ServiceFile, "GetTotalBytesSent", ServiceID, Nothing).TryGetValueEx("NewTotalBytesSent", TotalBytesSent)
    End Function

    Public Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalBytesReceived
        Return TR064Start(ServiceFile, "GetTotalBytesReceived", ServiceID, Nothing).TryGetValueEx("NewTotalBytesReceived", TotalBytesReceived)
    End Function

    Public Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalBytesSent
        Return TR064Start(ServiceFile, "GetTotalPacketsSent", ServiceID, Nothing).TryGetValueEx("NewTotalPacketsSent", TotalPacketsSent)
    End Function

    Public Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalPacketsReceived
        Return TR064Start(ServiceFile, "GetTotalPacketsReceived", ServiceID, Nothing).TryGetValueEx("NewTotalPacketsReceived", TotalPacketsReceived)
    End Function

    Public Function SetWANAccessType(AccessType As String) As Boolean Implements IWANCommonInterfaceConfigSCPD.SetWANAccessType
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWANAccessType", ServiceID, New Dictionary(Of String, String) From {{"NewAccessType", AccessType}}).ContainsKey("Error")
    End Function

    Function GetActiveProvider(ByRef Provider As String) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetActiveProvider
        Return Not TR064Start(ServiceFile, "X_AVM-DE_GetActiveProvider", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_Provider", Provider}}).ContainsKey("Error")
    End Function

    Public Function GetOnlineMonitor(ByRef OnlineMonitor As OnlineMonitor, SyncGroupIndex As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetOnlineMonitor
        If OnlineMonitor Is Nothing Then OnlineMonitor = New OnlineMonitor

        With TR064Start(ServiceFile, "X_AVM-DE_GetOnlineMonitor", ServiceID, New Dictionary(Of String, String) From {{"NewSyncGroupIndex", SyncGroupIndex.ToString}})

            OnlineMonitor.SyncGroupIndex = SyncGroupIndex

            Return .TryGetValueEx("NewTotalNumberSyncGroups", OnlineMonitor.TotalNumberSyncGroups) And
                   .TryGetValueEx("NewSyncGroupName", OnlineMonitor.SyncGroupName) And
                   .TryGetValueEx("NewSyncGroupMode", OnlineMonitor.SyncGroupMode) And
                   .TryGetValueEx("Newmax_ds", OnlineMonitor.Max_ds) And
                   .TryGetValueEx("Newmax_us", OnlineMonitor.Max_us) And
                   .TryGetValueEx("Newds_current_bps", OnlineMonitor.Ds_current_bps) And
                   .TryGetValueEx("Newmc_current_bps", OnlineMonitor.Mc_current_bps) And
                   .TryGetValueEx("Newus_current_bps", OnlineMonitor.Us_current_bps) And
                   .TryGetValueEx("Newprio_realtime_bps", OnlineMonitor.Prio_realtime_bps) And
                   .TryGetValueEx("Newprio_high_bps", OnlineMonitor.Prio_high_bps) And
                   .TryGetValueEx("Newprio_default_bps", OnlineMonitor.Prio_default_bps) And
                   .TryGetValueEx("Newprio_low_bps", OnlineMonitor.Prio_low_bps)
        End With
    End Function
End Class
