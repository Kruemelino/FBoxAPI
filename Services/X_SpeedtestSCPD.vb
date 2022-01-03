''' <summary>
''' TR-064 Support – Speedtest
''' Date: 2015-02-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf</see>
''' </summary>
Friend Class X_SpeedtestSCPD
    Implements IX_speedtestSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_speedtestSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_speedtestSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_speedtestSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_speedtestSCPD

        TR064Start = Start

        PushStatus = Status

    End Sub

    Public Function GetInfo(ByRef Info As SpeedtestInfo) As Boolean Implements IX_speedtestSCPD.GetInfo
        If Info Is Nothing Then Info = New SpeedtestInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewEnableTcp") And .ContainsKey("NewPortTcp") Then

                Info.EnableTcp = CBool(.Item("NewEnableTcp"))
                Info.EnableUdp = CBool(.Item("NewEnableUdp"))
                Info.EnableUdpBidirect = CBool(.Item("NewEnableUdpBidirect"))
                Info.WANEnableTcp = CBool(.Item("NewWANEnableTcp"))
                Info.WANEnableUdp = CBool(.Item("NewWANEnableUdp"))
                Info.PortTcp = CInt(.Item("NewPortTcp"))
                Info.PortUdp = CInt(.Item("NewPortUdp"))
                Info.PortUdpBidirect = CInt(.Item("NewPortUdpBidirect"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo (Speedtest) konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetConfig(EnableTcp As Boolean, EnableUdp As Boolean, EnableUdpBidirect As Boolean, WANEnableTcp As Boolean, WANEnableUdp As Boolean) As Boolean Implements IX_speedtestSCPD.SetConfig
        With TR064Start(ServiceFile, "SetConfig", New Hashtable From {{"NewEnableTcp", EnableTcp.ToInt},
                                                                      {"NewEnableUdp", EnableUdp.ToInt},
                                                                      {"NewEnableUdpBidirect", EnableUdpBidirect.ToInt},
                                                                      {"NewWANEnableTcp", WANEnableTcp.ToInt},
                                                                      {"NewWANEnableUdp", WANEnableUdp.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function
End Class
