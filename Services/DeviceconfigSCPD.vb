''' <summary>
''' TR-064 Support – DeviceConfig
''' Date: 2021-01-20
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf"/>
''' </summary>
Public Class DeviceconfigSCPD
    Implements IService
    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IService.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IService.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IService.Servicefile
    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.deviceconfigSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

#Region "PersistentData"
    Public Function GetPersistentData(ByRef PersistentData As String) As Boolean
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
    Public Function SetPersistentData(PersistentData As String) As Boolean
        If Not PersistentData.IsStringNothingOrEmpty Then
            With TR064Start(ServiceFile, "SetPersistentData", New Hashtable From {{"NewPersistentData", PersistentData}})
                Return Not .ContainsKey("Error")
            End With
        Else
            PushStatus.Invoke(LogLevel.Warn, $"PersistentData shall not be empty.")
            Return False
        End If
    End Function
#End Region

#Region "Configuration"
    Public Function ConfigurationStarted(SessionID As String) As Boolean

        With TR064Start(ServiceFile, "ConfigurationStarted", New Hashtable From {{"NewSessionID", SessionID}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function ConfigurationFinished(ByRef Status As String) As Boolean
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
    Public Function FactoryReset() As Boolean
        With TR064Start(ServiceFile, "FactoryReset", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function Reboot() As Boolean
        With TR064Start(ServiceFile, "Reboot", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "AVM"
    ''' <summary>
    ''' X_GenerateUUID
    ''' </summary>
    Public Function GenerateUUID(ByRef UUID As String) As Boolean
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

    ''' <summary>
    ''' X_AVM-DE_GetConfigFile<br/>
    ''' The action uses the given password to offer an encrypted password file to be downloaded at the given URL.
    ''' <list type="bullet">
    ''' <item>The URL is secured by SSL (https://) using the TR-064 SSL certificate.</item>
    ''' <item>The URL is secured by Digest authorization using the currently active username and password of the TR-064 service.</item>
    ''' <item>The URL is valid for less than 30 seconds.</item>
    ''' </list>
    ''' </summary>
    ''' <param name="Password">X_AVM-DE_Password</param>
    ''' <param name="ConfigFileUrl">X_AVM-DE_ConfigFileUrl</param>''' 
    Public Function GetConfigFile(Password As String, ByRef ConfigFileUrl As String) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_GetConfigFile", New Hashtable From {{"NewX_AVM-DE_Password", Password}})

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

    ''' <summary>
    ''' X_AVM-DE_SetConfigFile<br/>
    ''' The action needs both arguments. The password can be empty.
    ''' The URL shall use http or https protocol. The URL shall not be secured by Basic or Digest authorization. 
    ''' The URL shall be accessible when the action is called. The URL may have the following format:
    ''' <list type="bullet">
    ''' <item>http[s]://subdomain.domain.country[:port][/resource]</item>
    ''' <item>e.g. http://192.168.178.123:12345/ABCDEF or</item>
    ''' <item>https://192.168.178.123:23456 or</item>
    ''' <item>http://192.168.178.123/cfg.export </item>
    ''' </list>
    ''' </summary>
    ''' <param name="Password">X_AVM-DE_Password</param>
    ''' <param name="ConfigFileUrl">X_AVM-DE_ConfigFileUrl</param>
    ''' <returns></returns>
    Public Function SetConfigFile(Password As String, ConfigFileUrl As String) As Boolean

        With TR064Start(ServiceFile, "X_AVM-X_AVM-DE_SetConfigFile ", New Hashtable From {{"NewX_AVM-DE_Password", Password},
                                                                                          {"NewX_AVM-DE_ConfigFileUrl", ConfigFileUrl}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    ''' <summary>
    ''' Generate a temporary URL session ID. The session ID is need for accessing URLs like phone book, call list, FAX message, answering machine messages Or phone book images.
    ''' </summary>
    ''' <param name="SessionID">Represents the temporary URL session ID.</param>
    ''' <returns>True when success</returns>
    Public Function GetSessionID(ByRef SessionID As String) As Boolean

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

    ''' <summary>
    ''' X_AVM-DE_GetSupportDataInfo<br/>
    ''' Returns information about the current / last support data process. ID, timestamp, mode
    ''' and status always belongs to a process. The ID is required for AVM support to process
    ''' customer inquiries. The mode describes whether the support data is only created for this
    ''' device or the complete mesh system. Status shows in which step the current process is.
    ''' The normal flow "preparing" (2-3 sec.) -> "creating" (2-3min.) -> "ok"/"error".
    ''' It usually takes 2 to 3 minutes. In the worst case, up to 15 minutes.
    ''' </summary>
    ''' <param name="SupportDataMode">"normal", "mesh", "unknown"</param>
    ''' <param name="SupportDataID"></param>
    ''' <param name="SupportDataTimestamp">0001-01-01T00:00:00</param>
    ''' <param name="SupportDataStatus">"unknown", "ok", "preparing", "error", "creating"</param>
    ''' <returns></returns>
    Public Function GetSupportDataInfo(ByRef SupportDataMode As SupportDataMode, ByRef SupportDataID As String, ByRef SupportDataTimestamp As Date, ByRef SupportDataStatus As SupportDataStatus) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_GetSupportDataInfo", Nothing)

            If .ContainsKey("NewX_AVM-DE_SupportDataMode") Then

                SupportDataMode = CType(.Item("NewX_AVM-DE_SupportDataMode"), SupportDataMode)
                SupportDataID = .Item("NewX_AVM-DE_SupportDataID").ToString
                SupportDataTimestamp = CDate(.Item("NewX_AVM-DE_SupportDataTimestamp"))
                SupportDataStatus = CType(.Item("X_AVM-DE_SupportDataStatus"), SupportDataStatus)

                PushStatus.Invoke(LogLevel.Debug, $"SupportDataInfo der Fritz!Box: ID: {SupportDataID}, Modus: {SupportDataMode}, Zeitstempel: {SupportDataTimestamp}, Status: {SupportDataStatus}")

                Return True
            Else

                PushStatus.Invoke(LogLevel.Warn, $"SupportDataInfo der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    ''' <summary>
    ''' X_AVM-DE_SendSupportData<br/>
    ''' Initiate the creation of support data that will be sent to AVM support.
    ''' The X_AVM-DE_SupportDataMode parameter can be set to "normal" in order to send the
    ''' support data from this device. If it's necessary to send the support data from the complete
    ''' mesh system set the mode to "mesh". If the mode "mesh" is not supported by the device
    ''' the "normal" support data will be sent. Only one Support Data process is allowed to run.
    ''' Before request this action check the status value (see <see cref="GetSupportDataInfo"/>). If the status value is "ok", "error" or "unknown" this action can
    ''' be requested. Otherwise TR-064 error code 600 will be returned.
    ''' </summary>
    ''' <param name="SupportDataMode">"normal", "mesh", "unknown"</param>
    ''' <returns></returns>
    Public Function SendSupportData(SupportDataMode As String) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_SendSupportData", New Hashtable From {{"NewX_AVM-DE_SupportDataMode", SupportDataMode}})
            Return Not .ContainsKey("Error")

        End With

    End Function
#End Region

End Class