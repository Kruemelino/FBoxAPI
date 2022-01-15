''' <summary>
''' TR-064 Support – Speedtest
''' Date: 2015-02-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf</see>
''' </summary>
Friend Class X_SpeedtestSCPD
    Implements IX_speedtestSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_speedtestSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_speedtestSCPD Implements IX_speedtestSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Info As SpeedtestInfo) As Boolean Implements IX_speedtestSCPD.GetInfo
        If Info Is Nothing Then Info = New SpeedtestInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewEnableTcp", Info.EnableTcp) And
                   .TryGetValue("NewEnableUdp", Info.EnableUdp) And
                   .TryGetValue("NewEnableUdpBidirect", Info.EnableUdpBidirect) And
                   .TryGetValue("NewWANEnableTcp", Info.WANEnableTcp) And
                   .TryGetValue("NewWANEnableUdp", Info.WANEnableUdp) And
                   .TryGetValue("NewPortTcp", Info.PortTcp) And
                   .TryGetValue("NewPortUdp", Info.PortUdp) And
                   .TryGetValue("NewPortUdpBidirect", Info.PortUdpBidirect)

        End With
    End Function

    Public Function SetConfig(EnableTcp As Boolean, EnableUdp As Boolean, EnableUdpBidirect As Boolean, WANEnableTcp As Boolean, WANEnableUdp As Boolean) As Boolean Implements IX_speedtestSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", New Dictionary(Of String, String) From {{"NewEnableTcp", EnableTcp.ToBoolStr},
                                                                                                {"NewEnableUdp", EnableUdp.ToBoolStr},
                                                                                                {"NewEnableUdpBidirect", EnableUdpBidirect.ToBoolStr},
                                                                                                {"NewWANEnableTcp", WANEnableTcp.ToBoolStr},
                                                                                                {"NewWANEnableUdp", WANEnableUdp.ToBoolStr}}).ContainsKey("Error")
    End Function
End Class
