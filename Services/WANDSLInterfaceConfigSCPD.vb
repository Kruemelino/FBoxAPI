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

    Public Function GetInfo(ByRef Info As DSLLinkConfigInfo) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetInfo
        If Info Is Nothing Then Info = New DSLLinkConfigInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewEnable", Info.Enable) And
                   .TryGetValueEx("NewStatus", Info.Status) And
                   .TryGetValueEx("NewDataPath", Info.DataPath) And
                   .TryGetValueEx("NewUpstreamCurrRate", Info.UpstreamCurrRate) And
                   .TryGetValueEx("NewDownstreamCurrRate", Info.DownstreamCurrRate) And
                   .TryGetValueEx("NewUpstreamMaxRate", Info.UpstreamMaxRate) And
                   .TryGetValueEx("NewDownstreamMaxRate", Info.DownstreamMaxRate) And
                   .TryGetValueEx("NewUpstreamNoiseMargin", Info.UpstreamNoiseMargin) And
                   .TryGetValueEx("NewDownstreamNoiseMargin", Info.DownstreamNoiseMargin) And
                   .TryGetValueEx("NewUpstreamAttenuation", Info.UpstreamAttenuation) And
                   .TryGetValueEx("NewDownstreamAttenuation", Info.DownstreamAttenuation) And
                   .TryGetValueEx("NewATURVendor", Info.ATURVendor) And
                   .TryGetValueEx("NewATURCountry", Info.ATURCountry) And
                   .TryGetValueEx("NewUpstreamPower", Info.UpstreamPower) And
                   .TryGetValueEx("NewDownstreamPower", Info.DownstreamPower)
        End With
    End Function

    Public Function GetStatisticsTotal(ByRef StatisticsTotal As DSLLinkStatTotal) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetStatisticsTotal
        If StatisticsTotal Is Nothing Then StatisticsTotal = New DSLLinkStatTotal

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewReceiveBlocks", StatisticsTotal.ReceiveBlocks) And
                   .TryGetValueEx("NewTransmitBlocks", StatisticsTotal.TransmitBlocks) And
                   .TryGetValueEx("NewCellDelin", StatisticsTotal.CellDelin) And
                   .TryGetValueEx("NewLinkRetrain", StatisticsTotal.LinkRetrain) And
                   .TryGetValueEx("NewInitErrors", StatisticsTotal.InitErrors) And
                   .TryGetValueEx("NewInitTimeouts", StatisticsTotal.InitTimeouts) And
                   .TryGetValueEx("NewLossOfFraming", StatisticsTotal.LossOfFraming) And
                   .TryGetValueEx("NewErroredSecs", StatisticsTotal.ErroredSecs) And
                   .TryGetValueEx("NewSeverelyErroredSecs", StatisticsTotal.SeverelyErroredSecs) And
                   .TryGetValueEx("NewFECErrors", StatisticsTotal.FECErrors) And
                   .TryGetValueEx("NewATUCFECErrors", StatisticsTotal.ATUCFECErrors) And
                   .TryGetValueEx("NewHECErrors", StatisticsTotal.HECErrors) And
                   .TryGetValueEx("NewATUCHECErrors", StatisticsTotal.ATUCHECErrors) And
                   .TryGetValueEx("NewATUCCRCErrors", StatisticsTotal.ATUCCRCErrors) And
                   .TryGetValueEx("NewCRCErrors", StatisticsTotal.CRCErrors)
        End With
    End Function

    Public Function GetDSLDiagnoseInfo(ByRef Info As DSLDiagnoseInfo) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetDSLDiagnoseInfo
        If Info Is Nothing Then Info = New DSLDiagnoseInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_DSLDigagnoseState", Info.DSLDigagnoseState) And
                   .TryGetValueEx("NewX_AVM-DE_CableNokDistance", Info.CableNokDistance) And
                   .TryGetValueEx("NewX_AVM-DE_DSLLastDiagnoseTime", Info.DSLLastDiagnoseTime) And
                   .TryGetValueEx("NewX_AVM-DE_DSLSignalLossTime", Info.DSLSignalLossTime) And
                   .TryGetValueEx("NewX_AVM-DE_DSLActive", Info.DSLActive) And
                   .TryGetValueEx("NewX_AVM-DE_DSLSync", Info.DSLSync)
        End With
    End Function

    Public Function GetDSLInfo(ByRef Info As DSLInfo) As Boolean Implements IWANDSLInterfaceConfigSCPD.GetDSLInfo
        If Info Is Nothing Then Info = New DSLInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewSNRGds", Info.SNRGds) And
                   .TryGetValueEx("NewSNRGus", Info.SNRGus) And
                   .TryGetValueEx("NewSNRpsds", Info.SNRpsds) And
                   .TryGetValueEx("NewSNRpsus", Info.SNRpsus) And
                   .TryGetValueEx("NewSNRMTds", Info.SNRMTds) And
                   .TryGetValueEx("NewSNRMTus", Info.SNRMTus) And
                   .TryGetValueEx("NewLATNds", Info.LATNds) And
                   .TryGetValueEx("NewLATNus", Info.LATNus) And
                   .TryGetValueEx("NewFECErrors", Info.FECErrors) And
                   .TryGetValueEx("NewCRCErrors", Info.CRCErrors) And
                   .TryGetValueEx("NewLinkStatus", Info.LinkStatus) And
                   .TryGetValueEx("NewModulationType", Info.ModulationType) And
                   .TryGetValueEx("NewCurrentProfile", Info.CurrentProfile) And
                   .TryGetValueEx("NewUpstreamCurrRate", Info.UpstreamCurrRate) And
                   .TryGetValueEx("NewDownstreamCurrRate", Info.DownstreamCurrRate) And
                   .TryGetValueEx("NewUpstreamMaxRate", Info.UpstreamMaxRate) And
                   .TryGetValueEx("NewDownstreamMaxRate", Info.DownstreamMaxRate) And
                   .TryGetValueEx("NewUpstreamNoiseMargin", Info.UpstreamNoiseMargin) And
                   .TryGetValueEx("NewDownstreamNoiseMargin", Info.DownstreamNoiseMargin) And
                   .TryGetValueEx("NewUpstreamAttenuation", Info.UpstreamAttenuation) And
                   .TryGetValueEx("NewDownstreamAttenuation", Info.DownstreamAttenuation) And
                   .TryGetValueEx("NewATURVendor", Info.ATURVendor) And
                   .TryGetValueEx("NewATURCountry", Info.ATURCountry) And
                   .TryGetValueEx("NewUpstreamPower", Info.UpstreamPower) And
                   .TryGetValueEx("NewDownstreamPower", Info.DownstreamPower)

        End With
    End Function
End Class
