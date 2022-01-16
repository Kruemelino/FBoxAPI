''' <summary>
''' TR-064 Support – WANDSLInterfaceConfig
''' Date: 2019-11-01 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandslifconfigSCPD.pdf</see>
''' </summary>
Public Class WANDSLInterfaceConfigSCPD
    Implements IWANDSLInterfaceConfigSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANDSLInterfaceConfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wandslifconfigSCPD Implements IWANDSLInterfaceConfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As DSLLinkInfo) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetInfo
        If Info Is Nothing Then Info = New DSLLinkInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewEnable", Info.Enable) And
                   .TryGetValue("NewLinkStatus", Info.LinkStatus) And
                   .TryGetValue("NewLinkType", Info.LinkType) And
                   .TryGetValue("NewDestinationAddress", Info.DestinationAddress) And
                   .TryGetValue("NewATMEncapsulation", Info.ATMEncapsulation) And
                   .TryGetValue("NewAutoConfig", Info.AutoConfig) And
                   .TryGetValue("NewATMQoS", Info.ATMQoS) And
                   .TryGetValue("NewATMPeakCellRate", Info.ATMPeakCellRate) And
                   .TryGetValue("NewATMSustainableCellRate", Info.ATMSustainableCellRate)
        End With
    End Function

    Public Function SetEnable(Enable As Boolean) As Boolean Implements IWANDSLInterfaceConfigSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetDSLLinkType(LinkType As LinkTypeEnum) As Boolean Implements IWANDSLInterfaceConfigSCPD.SetDSLLinkType
        Return Not TR064Start(ServiceFile, "SetDSLLinkType", New Dictionary(Of String, String) From {{"NewLinkType", LinkType}}).ContainsKey("Error")
    End Function

    Public Function GetDSLLinkInfo(ByRef LinkType As LinkTypeEnum, ByRef LinkStatus As LinkStatusEnum) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetDSLLinkInfo
        With TR064Start(ServiceFile, "GetDSLLinkInfo", Nothing)

            Return .TryGetValue("NewLinkType", LinkType) And
                   .TryGetValue("NewLinkStatus", LinkStatus)
        End With
    End Function

    Public Function SetDestinationAddress(DestinationAddress As String) As Boolean Implements IWANDSLInterfaceConfigSCPD.SetDestinationAddress
        Return Not TR064Start(ServiceFile, "SetDestinationAddress", New Dictionary(Of String, String) From {{"NewDestinationAddress", DestinationAddress}}).ContainsKey("Error")
    End Function

    Public Function GetDestinationAddress(ByRef DestinationAddress As String) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetDestinationAddress
        Return TR064Start(ServiceFile, "GetDestinationAddress", Nothing).TryGetValue("NewDestinationAddress", DestinationAddress)
    End Function

    Public Function SetATMEncapsulation(ATMEncapsulation As String) As Boolean Implements IWANDSLInterfaceConfigSCPD.SetATMEncapsulation
        Return Not TR064Start(ServiceFile, "SetATMEncapsulation", New Dictionary(Of String, String) From {{"NewATMEncapsulation", ATMEncapsulation}}).ContainsKey("Error")
    End Function

    Public Function GetATMEncapsulation(ByRef ATMEncapsulation As String) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetATMEncapsulation
        Return TR064Start(ServiceFile, "GetATMEncapsulation", Nothing).TryGetValue("NewATMEncapsulation", ATMEncapsulation)
    End Function

    Public Function GetAutoConfig(ByRef AutoConfig As Boolean) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetAutoConfig
        Return TR064Start(ServiceFile, "GetAutoConfig", Nothing).TryGetValue("NewAutoConfig", AutoConfig)
    End Function

    Public Function GetStatistics(ByRef ATMTransmittedBlocks As Integer, ByRef ATMReceivedBlocks As Integer, ByRef AAL5CRCErrors As Integer, ByRef ATMCRCErrors As Integer) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetDSLLinkInfo", Nothing)

            Return .TryGetValue("NewATMTransmittedBlocks", ATMTransmittedBlocks) And
                   .TryGetValue("NewATMReceivedBlocks", ATMReceivedBlocks) And
                   .TryGetValue("NewAAL5CRCErrors", AAL5CRCErrors) And
                   .TryGetValue("NewATMCRCErrors", ATMCRCErrors)
        End With
    End Function
End Class
