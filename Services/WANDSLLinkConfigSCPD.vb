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

    Public Function GetInfo(ByRef Info As DSLLinkConfigInfo) As Boolean Implements IWANDSLLinkConfigSCPD.GetInfo
        If Info Is Nothing Then Info = New DSLLinkConfigInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewEnable", Info.Enable) And
                   .TryGetValue("NewStatus", Info.Status) And
                   .TryGetValue("NewDataPath", Info.DataPath) And
                   .TryGetValue("NewUpstreamCurrRate", Info.UpstreamCurrRate) And
                   .TryGetValue("NewDownstreamCurrRate", Info.DownstreamCurrRate) And
                   .TryGetValue("NewUpstreamMaxRate", Info.UpstreamMaxRate) And
                   .TryGetValue("NewDownstreamMaxRate", Info.DownstreamMaxRate) And
                   .TryGetValue("NewUpstreamNoiseMargin", Info.UpstreamNoiseMargin) And
                   .TryGetValue("NewDownstreamNoiseMargin", Info.DownstreamNoiseMargin) And
                   .TryGetValue("NewUpstreamAttenuation", Info.UpstreamAttenuation) And
                   .TryGetValue("NewDownstreamAttenuation", Info.DownstreamAttenuation) And
                   .TryGetValue("NewATURVendor", Info.ATURVendor) And
                   .TryGetValue("NewATURCountry", Info.ATURCountry) And
                   .TryGetValue("NewUpstreamPower", Info.UpstreamPower) And
                   .TryGetValue("NewDownstreamPower", Info.DownstreamPower)
        End With
    End Function

    Public Function GetStatisticsTotal(ByRef StatisticsTotal As DSLLinkStatTotal) As Boolean Implements IWANDSLLinkConfigSCPD.GetStatisticsTotal
        If StatisticsTotal Is Nothing Then StatisticsTotal = New DSLLinkStatTotal

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewReceiveBlocks", StatisticsTotal.ReceiveBlocks) And
                   .TryGetValue("NewTransmitBlocks", StatisticsTotal.TransmitBlocks) And
                   .TryGetValue("NewCellDelin", StatisticsTotal.CellDelin) And
                   .TryGetValue("NewLinkRetrain", StatisticsTotal.LinkRetrain) And
                   .TryGetValue("NewInitErrors", StatisticsTotal.InitErrors) And
                   .TryGetValue("NewInitTimeouts", StatisticsTotal.InitTimeouts) And
                   .TryGetValue("NewLossOfFraming", StatisticsTotal.LossOfFraming) And
                   .TryGetValue("NewErroredSecs", StatisticsTotal.ErroredSecs) And
                   .TryGetValue("NewSeverelyErroredSecs", StatisticsTotal.SeverelyErroredSecs) And
                   .TryGetValue("NewFECErrors", StatisticsTotal.FECErrors) And
                   .TryGetValue("NewATUCFECErrors", StatisticsTotal.ATUCFECErrors) And
                   .TryGetValue("NewHECErrors", StatisticsTotal.HECErrors) And
                   .TryGetValue("NewATUCHECErrors", StatisticsTotal.ATUCHECErrors) And
                   .TryGetValue("NewATUCCRCErrors", StatisticsTotal.ATUCCRCErrors) And
                   .TryGetValue("NewCRCErrors", StatisticsTotal.CRCErrors)
        End With
    End Function

    Public Function GetDSLDiagnoseInfo(ByRef Info As DSLDiagnoseInfo) As Boolean Implements IWANDSLLinkConfigSCPD.GetDSLDiagnoseInfo
        If Info Is Nothing Then Info = New DSLDiagnoseInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewX_AVM-DE_DSLDigagnoseState", Info.DSLDigagnoseState) And
                   .TryGetValue("NewX_AVM-DE_CableNokDistance", Info.CableNokDistance) And
                   .TryGetValue("NewX_AVM-DE_DSLLastDiagnoseTime", Info.DSLLastDiagnoseTime) And
                   .TryGetValue("NewX_AVM-DE_DSLSignalLossTime", Info.DSLSignalLossTime) And
                   .TryGetValue("NewX_AVM-DE_DSLActive", Info.DSLActive) And
                   .TryGetValue("NewX_AVM-DE_DSLSync", Info.DSLSync)
        End With
    End Function

    Public Function GetDSLInfo(ByRef Info As DSLInfo) As Boolean Implements IWANDSLLinkConfigSCPD.GetDSLInfo
        If Info Is Nothing Then Info = New DSLInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewSNRGds", Info.SNRGds) And
                   .TryGetValue("NewSNRGus", Info.SNRGus) And
                   .TryGetValue("NewSNRpsds", Info.SNRpsds) And
                   .TryGetValue("NewSNRpsus", Info.SNRpsus) And
                   .TryGetValue("NewSNRMTds", Info.SNRMTds) And
                   .TryGetValue("NewSNRMTus", Info.SNRMTus) And
                   .TryGetValue("NewLATNds", Info.LATNds) And
                   .TryGetValue("NewLATNus", Info.LATNus) And
                   .TryGetValue("NewFECErrors", Info.FECErrors) And
                   .TryGetValue("NewCRCErrors", Info.CRCErrors) And
                   .TryGetValue("NewLinkStatus", Info.LinkStatus) And
                   .TryGetValue("NewModulationType", Info.ModulationType) And
                   .TryGetValue("NewCurrentProfile", Info.CurrentProfile) And
                   .TryGetValue("NewUpstreamCurrRate", Info.UpstreamCurrRate) And
                   .TryGetValue("NewDownstreamCurrRate", Info.DownstreamCurrRate) And
                   .TryGetValue("NewUpstreamMaxRate", Info.UpstreamMaxRate) And
                   .TryGetValue("NewDownstreamMaxRate", Info.DownstreamMaxRate) And
                   .TryGetValue("NewUpstreamNoiseMargin", Info.UpstreamNoiseMargin) And
                   .TryGetValue("NewDownstreamNoiseMargin", Info.DownstreamNoiseMargin) And
                   .TryGetValue("NewUpstreamAttenuation", Info.UpstreamAttenuation) And
                   .TryGetValue("NewDownstreamAttenuation", Info.DownstreamAttenuation) And
                   .TryGetValue("NewATURVendor", Info.ATURVendor) And
                   .TryGetValue("NewATURCountry", Info.ATURCountry) And
                   .TryGetValue("NewUpstreamPower", Info.UpstreamPower) And
                   .TryGetValue("NewDownstreamPower", Info.DownstreamPower)

        End With
    End Function
End Class
