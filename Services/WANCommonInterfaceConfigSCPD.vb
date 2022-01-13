''' <summary>
''' TR-064 Support – WANCommonInterfaceConfig
''' Date: 2018-09-05
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf</see>
''' </summary>
Friend Class WANCommonInterfaceConfigSCPD
    Implements IWANCommonInterfaceConfigSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANCommonInterfaceConfigSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IWANCommonInterfaceConfigSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IWANCommonInterfaceConfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.wancommonifconfigSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetCommonLinkProperties(ByRef WANAccessType As AccessTypeEnum,
                                            ByRef Layer1UpstreamMaxBitRate As Integer,
                                            ByRef Layer1DownstreamMaxBitRate As Integer,
                                            ByRef PhysicalLinkStatus As PhysicalLinkStatusEnum) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetCommonLinkProperties

        With TR064Start(ServiceFile, "GetCommonLinkProperties", Nothing)

            If .ContainsKey("NewWANAccessType") Then

                WANAccessType = CType(.Item("NewWANAccessType"), AccessTypeEnum)
                Layer1UpstreamMaxBitRate = CInt(.Item("NewLayer1UpstreamMaxBitRate"))
                Layer1DownstreamMaxBitRate = CInt(.Item("NewLayer1DownstreamMaxBitRate"))
                PhysicalLinkStatus = CType(.Item("NewPhysicalLinkStatus"), PhysicalLinkStatusEnum)

                PushStatus.Invoke(LogLevel.Debug, $"GetCommonLinkProperties aus der Fritz!Box ausgelesen: '{WANAccessType}', '{Layer1UpstreamMaxBitRate}','{Layer1DownstreamMaxBitRate}','{PhysicalLinkStatus}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetCommonLinkProperties konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetTotalBytesSent(ByRef TotalBytesSent As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalPacketsSent

        With TR064Start(ServiceFile, "GetTotalBytesSent", Nothing)

            If .ContainsKey("NewTotalBytesSent") Then

                TotalBytesSent = CInt(.Item("NewTotalBytesSent"))

                PushStatus.Invoke(LogLevel.Debug, $"GetTotalBytesSent aus der Fritz!Box ausgelesen: '{TotalBytesSent}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTotalBytesSent konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetTotalBytesReceived(ByRef TotalBytesReceived As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalBytesReceived

        With TR064Start(ServiceFile, "GetTotalBytesReceived", Nothing)

            If .ContainsKey("NewTotalBytesReceived") Then

                TotalBytesReceived = CInt(.Item("NewTotalBytesReceived"))

                PushStatus.Invoke(LogLevel.Debug, $"GetTotalBytesReceived aus der Fritz!Box ausgelesen: '{TotalBytesReceived}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTotalBytesReceived konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetTotalPacketsSent(ByRef TotalPacketsSent As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalBytesSent

        With TR064Start(ServiceFile, "GetTotalPacketsSent", Nothing)

            If .ContainsKey("NewTotalPacketsSent") Then

                TotalPacketsSent = CInt(.Item("NewTotalPacketsSent"))

                PushStatus.Invoke(LogLevel.Debug, $"GetTotalPacketsSent aus der Fritz!Box ausgelesen: '{TotalPacketsSent}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTotalPacketsSent konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetTotalPacketsReceived(ByRef TotalPacketsReceived As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetTotalPacketsReceived

        With TR064Start(ServiceFile, "GetTotalPacketsReceived", Nothing)

            If .ContainsKey("NewTotalPacketsReceived") Then

                TotalPacketsReceived = CInt(.Item("NewTotalPacketsReceived"))

                PushStatus.Invoke(LogLevel.Debug, $"GetTotalPacketsReceived aus der Fritz!Box ausgelesen: '{TotalPacketsReceived}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTotalPacketsReceived konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function SetWANAccessType(AccessType As AccessTypeEnum) As Boolean Implements IWANCommonInterfaceConfigSCPD.SetWANAccessType

        With TR064Start(ServiceFile, "X_AVM-DE_SetWANAccessType", New Dictionary(Of String, String) From {{"NewAccessType", AccessType.ToString}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function GetOnlineMonitor(ByRef OnlineMonitor As OnlineMonitor, SyncGroupIndex As Integer) As Boolean Implements IWANCommonInterfaceConfigSCPD.GetOnlineMonitor
        If OnlineMonitor Is Nothing Then OnlineMonitor = New OnlineMonitor

        With TR064Start(ServiceFile, "X_AVM-DE_GetOnlineMonitor", New Dictionary(Of String, String) From {{"NewSyncGroupIndex", SyncGroupIndex}})

            If .ContainsKey("NewVoIPRegistrar") Then
                OnlineMonitor.SyncGroupIndex = SyncGroupIndex

                OnlineMonitor.TotalNumberSyncGroups = CInt(.Item("NewTotalNumberSyncGroups"))
                OnlineMonitor.SyncGroupName = .Item("NewSyncGroupName").ToString
                OnlineMonitor.SyncGroupMode = .Item("NewSyncGroupMode").ToString
                OnlineMonitor.Max_ds = CInt(.Item("Newmax_ds"))
                OnlineMonitor.Max_us = CInt(.Item("Newmax_us"))
                OnlineMonitor.Ds_current_bps = .Item("Newds_current_bps").ToString
                OnlineMonitor.Mc_current_bps = .Item("Newmc_current_bps").ToString
                OnlineMonitor.Us_current_bps = .Item("Newus_current_bps").ToString
                OnlineMonitor.Prio_realtime_bps = .Item("Newprio_realtime_bps").ToString
                OnlineMonitor.Prio_high_bps = .Item("Newprio_high_bps").ToString
                OnlineMonitor.Prio_default_bps = .Item("Newprio_default_bps").ToString
                OnlineMonitor.Prio_low_bps = .Item("Newprio_low_bps").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-GetOnlineMonitor konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
End Class
