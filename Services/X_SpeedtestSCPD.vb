''' <summary>
''' TR-064 Support – Speedtest
''' Date: 2022-01-10
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf</see>
''' </summary>
Friend Class X_SpeedtestSCPD
    Implements IX_speedtestSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 1, 10) Implements IX_speedtestSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_speedtestSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_speedtestSCPD Implements IX_speedtestSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_speedtestSCPD.ServiceID
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Info As SpeedtestInfo) As Boolean Implements IX_speedtestSCPD.GetInfo
        If Info Is Nothing Then Info = New SpeedtestInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewEnableTcp", Info.EnableTcp) And
                   .TryGetValueEx("NewEnableUdp", Info.EnableUdp) And
                   .TryGetValueEx("NewEnableUdpBidirect", Info.EnableUdpBidirect) And
                   .TryGetValueEx("NewWANEnableTcp", Info.WANEnableTcp) And
                   .TryGetValueEx("NewWANEnableUdp", Info.WANEnableUdp) And
                   .TryGetValueEx("NewPortTcp", Info.PortTcp) And
                   .TryGetValueEx("NewPortUdp", Info.PortUdp) And
                   .TryGetValueEx("NewPortUdpBidirect", Info.PortUdpBidirect)

        End With
    End Function

    Public Function SetConfig(EnableTcp As Boolean, EnableUdp As Boolean, EnableUdpBidirect As Boolean, WANEnableTcp As Boolean, WANEnableUdp As Boolean) As Boolean Implements IX_speedtestSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnableTcp", EnableTcp.ToBoolStr},
                                                                      {"NewEnableUdp", EnableUdp.ToBoolStr},
                                                                      {"NewEnableUdpBidirect", EnableUdpBidirect.ToBoolStr},
                                                                      {"NewWANEnableTcp", WANEnableTcp.ToBoolStr},
                                                                      {"NewWANEnableUdp", WANEnableUdp.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetStatistics(ByRef Statistics As SpeedtestStatistics) As Boolean Implements IX_speedtestSCPD.GetStatistics
        If Statistics Is Nothing Then Statistics = New SpeedtestStatistics

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewByteCount", Statistics.ByteCount) And
                   .TryGetValueEx("NewKbitsCurrent", Statistics.KbitsCurrent) And
                   .TryGetValueEx("NewKbitsAvg", Statistics.KbitsAvg) And
                   .TryGetValueEx("NewPacketCount", Statistics.PacketCount) And
                   .TryGetValueEx("NewPPSCurrent", Statistics.PPSCurrent) And
                   .TryGetValueEx("NewPPSAvg", Statistics.PPSAvg)

        End With
    End Function

    Public Function ResetStatistics() As Boolean Implements IX_speedtestSCPD.ResetStatistics
        Return Not TR064Start(ServiceFile, "ResetStatistics", ServiceID, Nothing).ContainsKey("Error")
    End Function
End Class
