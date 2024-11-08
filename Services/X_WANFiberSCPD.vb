''' <summary>
''' TR-064 Support – X_AVM-DE_WANFiber
''' Date: 2023-08-30
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanfiberSCPD.pdf</see>
''' </summary>
Public Class X_WANFiberSCPD
    Implements IX_WANFiberSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 8, 30) Implements IX_WANFiberSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_WANFiberSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_wanfiberSCPD Implements IX_WANFiberSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_WANFiberSCPD.ServiceID


    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Info As WANFiberInfo) As Boolean Implements IX_WANFiberSCPD.GetInfo
        If Info Is Nothing Then Info = New WANFiberInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            If .ContainsKey("NewOpticalSignalLevel") Then

                Return .TryGetValueEx("NewOpticalSignalLevel", Info.OpticalSignalLevel) And
                       .TryGetValueEx("NewLowerOpticalThreshold", Info.LowerOpticalThreshold) And
                       .TryGetValueEx("NewUpperOpticalThreshold", Info.UpperOpticalThreshold) And
                       .TryGetValueEx("NewTransmitOpticalLevel", Info.TransmitOpticalLevel) And
                       .TryGetValueEx("NewLowerTransmitPowerThreshold", Info.LowerTransmitPowerThreshold) And
                       .TryGetValueEx("NewUpperTransmitPowerThreshold", Info.UpperTransmitPowerThreshold) And
                       .TryGetValueEx("NewSFPVendor", Info.SFPVendor) And
                       .TryGetValueEx("NewSFPPartNumber", Info.SFPPartNumber) And
                       .TryGetValueEx("NewSFPSerialNumber", Info.SFPSerialNumber) And
                       .TryGetValueEx("NewSFPType", Info.SFPType) And
                       .TryGetValueEx("NewTXWaveLength", Info.TXWaveLength) And
                       .TryGetValueEx("NewFiberMode", Info.FiberMode)
            Else
                Return False
            End If
        End With
    End Function

    Public Function GetInfoGPON(ByRef Info As WANGPONInfo) As Boolean Implements IX_WANFiberSCPD.GetInfoGPON
        If Info Is Nothing Then Info = New WANGPONInfo

        With TR064Start(ServiceFile, "GetInfoGPON", ServiceID, Nothing)

            If .ContainsKey("NewGponSerial") Then

                Return .TryGetValueEx("NewGponSerial", Info.GponSerial) And
                       .TryGetValueEx("NewPONId", Info.PONId) And
                       .TryGetValueEx("NewONUId", Info.ONUId) And
                       .TryGetValueEx("NewUNIType", Info.UNIType) And
                       .TryGetValueEx("NewGEMPortCount", Info.GEMPortCount)
            Else
                Return False
            End If
        End With
    End Function

    Public Function GetStatistics(ByRef Statistics As WANFiberStat) As Boolean Implements IX_WANFiberSCPD.GetStatistics
        If Statistics Is Nothing Then Statistics = New WANFiberStat

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            If .ContainsKey("NewBytesSent") Then

                Return .TryGetValueEx("NewBytesSent", Statistics.BytesSent) And
                       .TryGetValueEx("NewBytesReceived", Statistics.BytesReceived) And
                       .TryGetValueEx("NewPacketsSent", Statistics.PacketsSent) And
                       .TryGetValueEx("NewPacketsReceived", Statistics.PacketsReceived) And
                       .TryGetValueEx("NewPacketErrorsSent", Statistics.PacketErrorsSent) And
                       .TryGetValueEx("NewPacketErrorsReceived", Statistics.PacketErrorsReceived) And
                       .TryGetValueEx("NewPacketsMulticast", Statistics.PacketsMulticast) And
                       .TryGetValueEx("NewConnectionRateDown", Statistics.ConnectionRateDown) And
                       .TryGetValueEx("NewConnectionRateUp", Statistics.ConnectionRateUp) And
                       .TryGetValueEx("NewBestTrainState", Statistics.BestTrainState) And
                       .TryGetValueEx("NewResyncs", Statistics.Resyncs) And
                       .TryGetValueEx("NewMinutesInShowtime", Statistics.MinutesInShowtime)
            Else
                Return False
            End If
        End With
    End Function
End Class
