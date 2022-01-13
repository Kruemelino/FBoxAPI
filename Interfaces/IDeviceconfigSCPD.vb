''' <summary>
''' TR-064 Support – DeviceConfig
''' Date: 2021-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf</see>
''' </summary>
Public Interface IDeviceconfigSCPD
    Inherits IServiceBase

    Function GetPersistentData(ByRef PersistentData As String) As Boolean
    Function SetPersistentData(PersistentData As String) As Boolean
    Function ConfigurationStarted(SessionID As String) As Boolean
    Function ConfigurationFinished(ByRef Status As String) As Boolean
    Function FactoryReset() As Boolean
    Function Reboot() As Boolean

    ''' <summary>
    ''' X_GenerateUUID
    ''' </summary>
    Function GenerateUUID(ByRef UUID As String) As Boolean

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
    ''' <param name="ConfigFileUrl">X_AVM-DE_ConfigFileUrl</param> 
    Function GetConfigFile(Password As String, ByRef ConfigFileUrl As String) As Boolean

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
    Function SetConfigFile(Password As String, ConfigFileUrl As String) As Boolean

    ''' <summary>
    ''' Generate a temporary URL session ID. The session ID is need for accessing URLs like phone book, call list, FAX message, answering machine messages Or phone book images.
    ''' </summary>
    ''' <param name="SessionID">Represents the temporary URL session ID.</param>
    Function GetSessionID(ByRef SessionID As String) As Boolean

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
    ''' <param name="SupportDataTimestamp">0001-01-01T00:00:00</param>
    ''' <param name="SupportDataStatus">"unknown", "ok", "preparing", "error", "creating"</param>
    Function GetSupportDataInfo(ByRef SupportDataMode As SupportDataModeEnum, ByRef SupportDataID As String, ByRef SupportDataTimestamp As Date, ByRef SupportDataStatus As SupportDataStatusEnum) As Boolean

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
    Function SendSupportData(SupportDataMode As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Funktion zum Test von Username und Passwort.
    ''' </summary>
    Function LoginTest() As Boolean

End Interface
