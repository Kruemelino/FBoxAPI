''' <summary>
''' TR-064 Support – WANDSLLinkConfig
''' Date: 2015-11-20 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandsllinkconfigSCPD.pdf</see>
''' </summary>
Public Class WANDSLLinkConfigSCPD
    Implements IWANDSLLinkConfigSCPD


    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANDSLLinkConfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wandsllinkconfigSCPD Implements IWANDSLLinkConfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As DSLLinkInfo) As Boolean Implements IWANDSLLinkConfigSCPD.GetInfo
        If Info Is Nothing Then Info = New DSLLinkInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewEnable", Info.Enable) And
                   .TryGetValueEx("NewLinkStatus", Info.LinkStatus) And
                   .TryGetValueEx("NewLinkType", Info.LinkType) And
                   .TryGetValueEx("NewDestinationAddress", Info.DestinationAddress) And
                   .TryGetValueEx("NewATMEncapsulation", Info.ATMEncapsulation) And
                   .TryGetValueEx("NewAutoConfig", Info.AutoConfig) And
                   .TryGetValueEx("NewATMQoS", Info.ATMQoS) And
                   .TryGetValueEx("NewATMPeakCellRate", Info.ATMPeakCellRate) And
                   .TryGetValueEx("NewATMSustainableCellRate", Info.ATMSustainableCellRate)
        End With
    End Function

    Public Function SetEnable(Enable As Boolean) As Boolean Implements IWANDSLLinkConfigSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetDSLLinkType(LinkType As LinkTypeEnum) As Boolean Implements IWANDSLLinkConfigSCPD.SetDSLLinkType
        Return Not TR064Start(ServiceFile, "SetDSLLinkType", New Dictionary(Of String, String) From {{"NewLinkType", LinkType}}).ContainsKey("Error")
    End Function

    Public Function GetDSLLinkInfo(ByRef LinkType As LinkTypeEnum, ByRef LinkStatus As LinkStatusEnum) As Boolean Implements IWANDSLLinkConfigSCPD.GetDSLLinkInfo
        With TR064Start(ServiceFile, "GetDSLLinkInfo", Nothing)

            Return .TryGetValueEx("NewLinkType", LinkType) And
                   .TryGetValueEx("NewLinkStatus", LinkStatus)
        End With
    End Function

    Public Function SetDestinationAddress(DestinationAddress As String) As Boolean Implements IWANDSLLinkConfigSCPD.SetDestinationAddress
        Return Not TR064Start(ServiceFile, "SetDestinationAddress", New Dictionary(Of String, String) From {{"NewDestinationAddress", DestinationAddress}}).ContainsKey("Error")
    End Function

    Public Function GetDestinationAddress(ByRef DestinationAddress As String) As Boolean Implements IWANDSLLinkConfigSCPD.GetDestinationAddress
        Return TR064Start(ServiceFile, "GetDestinationAddress", Nothing).TryGetValueEx("NewDestinationAddress", DestinationAddress)
    End Function

    Public Function SetATMEncapsulation(ATMEncapsulation As String) As Boolean Implements IWANDSLLinkConfigSCPD.SetATMEncapsulation
        Return Not TR064Start(ServiceFile, "SetATMEncapsulation", New Dictionary(Of String, String) From {{"NewATMEncapsulation", ATMEncapsulation}}).ContainsKey("Error")
    End Function

    Public Function GetATMEncapsulation(ByRef ATMEncapsulation As String) As Boolean Implements IWANDSLLinkConfigSCPD.GetATMEncapsulation
        Return TR064Start(ServiceFile, "GetATMEncapsulation", Nothing).TryGetValueEx("NewATMEncapsulation", ATMEncapsulation)
    End Function

    Public Function GetAutoConfig(ByRef AutoConfig As Boolean) As Boolean Implements IWANDSLLinkConfigSCPD.GetAutoConfig
        Return TR064Start(ServiceFile, "GetAutoConfig", Nothing).TryGetValueEx("NewAutoConfig", AutoConfig)
    End Function

    Public Function GetStatistics(ByRef ATMTransmittedBlocks As Integer, ByRef ATMReceivedBlocks As Integer, ByRef AAL5CRCErrors As Integer, ByRef ATMCRCErrors As Integer) As Boolean Implements IWANDSLLinkConfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetDSLLinkInfo", Nothing)

            Return .TryGetValueEx("NewATMTransmittedBlocks", ATMTransmittedBlocks) And
                   .TryGetValueEx("NewATMReceivedBlocks", ATMReceivedBlocks) And
                   .TryGetValueEx("NewAAL5CRCErrors", AAL5CRCErrors) And
                   .TryGetValueEx("NewATMCRCErrors", ATMCRCErrors)
        End With
    End Function
End Class
