''' <summary>
''' TR-064 Support – DeviceConfig
''' Date: 2021-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf</see>
''' </summary>
Friend Class DeviceconfigSCPD
    Implements IDeviceconfigSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDeviceconfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.deviceconfigSCPD Implements IDeviceconfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub


#Region "PersistentData"
    Public Function GetPersistentData(ByRef PersistentData As String) As Boolean Implements IDeviceconfigSCPD.GetPersistentData
        Return TR064Start(ServiceFile, "GetPersistentData", Nothing).TryGetValue("NewPersistentData", PersistentData)
    End Function

    ''' <param name="PersistentData">Shall not be empty.</param>
    ''' <returns></returns>
    Public Function SetPersistentData(PersistentData As String) As Boolean Implements IDeviceconfigSCPD.SetPersistentData
        If Not PersistentData.IsStringNothingOrEmpty Then
            Return Not TR064Start(ServiceFile, "SetPersistentData", New Dictionary(Of String, String) From {{"NewPersistentData", PersistentData}}).ContainsKey("Error")
        Else
            Return False
        End If
    End Function
#End Region

#Region "Configuration"
    Public Function ConfigurationStarted(SessionID As String) As Boolean Implements IDeviceconfigSCPD.ConfigurationStarted
        Return Not TR064Start(ServiceFile, "ConfigurationStarted", New Dictionary(Of String, String) From {{"NewSessionID", SessionID}}).ContainsKey("Error")
    End Function

    Public Function ConfigurationFinished(ByRef Status As String) As Boolean Implements IDeviceconfigSCPD.ConfigurationFinished
        Return TR064Start(ServiceFile, "GetPersistentData", Nothing).TryGetValue("NewStatus", Status)
    End Function
#End Region

#Region "Device"
    Public Function FactoryReset() As Boolean Implements IDeviceconfigSCPD.FactoryReset
        Return Not TR064Start(ServiceFile, "FactoryReset", Nothing).ContainsKey("Error")
    End Function

    Public Function Reboot() As Boolean Implements IDeviceconfigSCPD.Reboot
        Return Not TR064Start(ServiceFile, "Reboot", Nothing).ContainsKey("Error")
    End Function
#End Region

#Region "AVM"
    Public Function GenerateUUID(ByRef UUID As String) As Boolean Implements IDeviceconfigSCPD.GenerateUUID
        Return TR064Start(ServiceFile, "X_GenerateUUID", Nothing).TryGetValue("NewUUID", UUID)
    End Function

    Public Function GetConfigFile(Password As String, ByRef ConfigFileUrl As String) As Boolean Implements IDeviceconfigSCPD.GetConfigFile
        Return TR064Start(ServiceFile, "X_AVM-DE_GetConfigFile", New Dictionary(Of String, String) From {{"NewX_AVM-DE_Password", Password}}).TryGetValue("NewX_AVM-DE_ConfigFileUrl", ConfigFileUrl)
    End Function

    Public Function SetConfigFile(Password As String, ConfigFileUrl As String) As Boolean Implements IDeviceconfigSCPD.SetConfigFile
        Return Not TR064Start(ServiceFile, "X_AVM-X_AVM-DE_SetConfigFile ", New Dictionary(Of String, String) From {{"NewX_AVM-DE_Password", Password},
                                                                                                                    {"NewX_AVM-DE_ConfigFileUrl", ConfigFileUrl}}).ContainsKey("Error")
    End Function

    Public Function GetSessionID(ByRef SessionID As String) As Boolean Implements IDeviceconfigSCPD.GetSessionID
        Return TR064Start(ServiceFile, "X_AVM-DE_CreateUrlSID", Nothing).TryGetValue("NewX_AVM-DE_UrlSID", SessionID)
    End Function

    Public Function GetSupportDataInfo(ByRef SupportDataMode As SupportDataModeEnum,
                                       ByRef SupportDataID As String,
                                       ByRef SupportDataTimestamp As Date,
                                       ByRef SupportDataStatus As SupportDataStatusEnum) As Boolean Implements IDeviceconfigSCPD.GetSupportDataInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetSupportDataInfo", Nothing)

            Return .TryGetValue("NewX_AVM-DE_SupportDataMode", SupportDataMode) And
                   .TryGetValue("NewX_AVM-DE_SupportDataID", SupportDataID) And
                   .TryGetValue("NewX_AVM-DE_SupportDataTimestamp", SupportDataTimestamp) And
                   .TryGetValue("NewX_AVM-DE_SupportDataStatus", SupportDataStatus)

        End With

    End Function

    Public Function SendSupportData(SupportDataMode As String) As Boolean Implements IDeviceconfigSCPD.SendSupportData
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SendSupportData", New Dictionary(Of String, String) From {{"NewX_AVM-DE_SupportDataMode", SupportDataMode}}).ContainsKey("Error")
    End Function

    Public Function LoginTest() As Boolean Implements IDeviceconfigSCPD.LoginTest
        With TR064Start(ServiceFile, "X_AVM-DE_CreateUrlSID", Nothing)
            Return .ContainsKey("NewX_AVM-DE_UrlSID") AndAlso Not .Item("NewX_AVM-DE_UrlSID").AreEqual(DfltFritzBoxSessionID)
        End With
    End Function
#End Region

End Class