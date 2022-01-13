''' <summary>
''' TR-064 Support – DeviceConfig
''' Date: 2021-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf</see>
''' </summary>
Friend Class DeviceconfigSCPD
    Implements IDeviceconfigSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDeviceconfigSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IDeviceconfigSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IDeviceconfigSCPD.Servicefile
    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.deviceconfigSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

#Region "PersistentData"
    Public Function GetPersistentData(ByRef PersistentData As String) As Boolean Implements IDeviceconfigSCPD.GetPersistentData
        With TR064Start(ServiceFile, "GetPersistentData", Nothing)
            If .ContainsKey("NewPersistentData") Then

                PersistentData = .Item("NewPersistentData").ToString

                PushStatus.Invoke(LogLevel.Debug, $"PersistentData der Fritz!Box: {PersistentData}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"PersistentData der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    ''' <param name="PersistentData">Shall not be empty.</param>
    ''' <returns></returns>
    Public Function SetPersistentData(PersistentData As String) As Boolean Implements IDeviceconfigSCPD.SetPersistentData
        If Not PersistentData.IsStringNothingOrEmpty Then
            With TR064Start(ServiceFile, "SetPersistentData", New Dictionary(Of String, String) From {{"NewPersistentData", PersistentData}})
                Return Not .ContainsKey("Error")
            End With
        Else
            PushStatus.Invoke(LogLevel.Warn, $"PersistentData shall not be empty.")
            Return False
        End If
    End Function
#End Region

#Region "Configuration"
    Public Function ConfigurationStarted(SessionID As String) As Boolean Implements IDeviceconfigSCPD.ConfigurationStarted

        With TR064Start(ServiceFile, "ConfigurationStarted", New Dictionary(Of String, String) From {{"NewSessionID", SessionID}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function ConfigurationFinished(ByRef Status As String) As Boolean Implements IDeviceconfigSCPD.ConfigurationFinished
        With TR064Start(ServiceFile, "GetPersistentData", Nothing)
            If .ContainsKey("NewStatus") Then

                Status = .Item("NewStatus").ToString

                PushStatus.Invoke(LogLevel.Debug, $"ConfigurationFinished der Fritz!Box: {Status}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"ConfigurationFinished der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
#End Region

#Region "Device"
    Public Function FactoryReset() As Boolean Implements IDeviceconfigSCPD.FactoryReset
        With TR064Start(ServiceFile, "FactoryReset", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function Reboot() As Boolean Implements IDeviceconfigSCPD.Reboot
        With TR064Start(ServiceFile, "Reboot", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "AVM"

    Public Function GenerateUUID(ByRef UUID As String) As Boolean Implements IDeviceconfigSCPD.GenerateUUID
        With TR064Start(ServiceFile, "X_GenerateUUID", Nothing)
            If .ContainsKey("NewUUID") Then

                UUID = .Item("NewUUID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"UUID der Fritz!Box: {UUID}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"UUID der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetConfigFile(Password As String, ByRef ConfigFileUrl As String) As Boolean Implements IDeviceconfigSCPD.GetConfigFile

        With TR064Start(ServiceFile, "X_AVM-DE_GetConfigFile", New Dictionary(Of String, String) From {{"NewX_AVM-DE_Password", Password}})

            If .ContainsKey("NewX_AVM-DE_ConfigFileUrl") Then

                ConfigFileUrl = .Item("NewX_AVM-DE_ConfigFileUrl").ToString

                PushStatus.Invoke(LogLevel.Debug, $"ConfigFileUrl der Fritz!Box: {ConfigFileUrl}")

                Return True
            Else

                PushStatus.Invoke(LogLevel.Warn, $"Keine ConfigFileUrl der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function SetConfigFile(Password As String, ConfigFileUrl As String) As Boolean Implements IDeviceconfigSCPD.SetConfigFile

        With TR064Start(ServiceFile, "X_AVM-X_AVM-DE_SetConfigFile ", New Dictionary(Of String, String) From {{"NewX_AVM-DE_Password", Password},
                                                                                          {"NewX_AVM-DE_ConfigFileUrl", ConfigFileUrl}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function GetSessionID(ByRef SessionID As String) As Boolean Implements IDeviceconfigSCPD.GetSessionID

        With TR064Start(ServiceFile, "X_AVM-DE_CreateUrlSID", Nothing)

            If .ContainsKey("NewX_AVM-DE_UrlSID") Then

                SessionID = .Item("NewX_AVM-DE_UrlSID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Aktuelle SessionID der Fritz!Box: {SessionID}")

                Return True
            Else
                SessionID = DfltFritzBoxSessionID

                PushStatus.Invoke(LogLevel.Warn, $"Keine SessionID der Fritz!Box erhalten. Rückgabewert: '{SessionID}' '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetSupportDataInfo(ByRef SupportDataMode As SupportDataModeEnum,
                                       ByRef SupportDataID As String,
                                       ByRef SupportDataTimestamp As Date,
                                       ByRef SupportDataStatus As SupportDataStatusEnum) As Boolean Implements IDeviceconfigSCPD.GetSupportDataInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetSupportDataInfo", Nothing)

            If .ContainsKey("NewX_AVM-DE_SupportDataMode") Then

                SupportDataMode = CType(.Item("NewX_AVM-DE_SupportDataMode"), SupportDataModeEnum)
                SupportDataID = .Item("NewX_AVM-DE_SupportDataID").ToString
                SupportDataTimestamp = CDate(.Item("NewX_AVM-DE_SupportDataTimestamp"))
                SupportDataStatus = CType(.Item("X_AVM-DE_SupportDataStatus"), SupportDataStatusEnum)

                PushStatus.Invoke(LogLevel.Debug, $"SupportDataInfo der Fritz!Box: ID: {SupportDataID}, Modus: {SupportDataMode}, Zeitstempel: {SupportDataTimestamp}, Status: {SupportDataStatus}")

                Return True
            Else

                PushStatus.Invoke(LogLevel.Warn, $"SupportDataInfo der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function SendSupportData(SupportDataMode As String) As Boolean Implements IDeviceconfigSCPD.SendSupportData

        With TR064Start(ServiceFile, "X_AVM-DE_SendSupportData", New Dictionary(Of String, String) From {{"NewX_AVM-DE_SupportDataMode", SupportDataMode}})
            Return Not .ContainsKey("Error")

        End With

    End Function

    Public Function LoginTest() As Boolean Implements IDeviceconfigSCPD.LoginTest
        With TR064Start(ServiceFile, "X_AVM-DE_CreateUrlSID", Nothing)
            Return .ContainsKey("NewX_AVM-DE_UrlSID") AndAlso Not .Item("NewX_AVM-DE_UrlSID").ToString.AreEqual(DfltFritzBoxSessionID)
        End With
    End Function
#End Region

End Class