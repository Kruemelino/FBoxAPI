''' <summary>
''' TR-064 Support – UserInterface
''' Date: 2019-01-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/userifSCPD.pdf</see>
''' </summary> 
Public Interface IUserInterfaceSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As DeviceUIInfo) As Boolean

    ''' <param name="LaborVersion">"Current" currently used Version, either Release or Labor XXX Release-Firmware: "" Labor-Firmware: content of CONFIG_LABOR_ID_NAME e.g. phone</param>
    Function CheckUpdate(LaborVersion As String) As Boolean

    ''' <summary>
    ''' The CGI has to use the session ID as first parameter. The session ID will be checked at the start of the CGI process.
    ''' The session ID is valid for up to 60 seconds after calling the SOAP action X_AVM-DE_DoPrepareCGI. 
    ''' </summary>
    Function DoPrepareCGI(ByRef CGI As String, ByRef SessionID As String) As Boolean

    ''' <remarks>
    ''' Required rights: depending on configuration either configuration rights or no authentication 
    ''' </remarks>
    Function DoUpdate(ByRef UpgradeAvailable As Boolean, ByRef UpdateState As UpdateStateEnum) As Boolean

    Function DoManualUpdate(DownloadURL As String, AllowDowngrade As Boolean) As Boolean

    Function GetInternationalConfig(ByRef Language As String, ByRef Country As String, ByRef Annex As String, ByRef LanguageList As String, ByRef CountryList As String, ByRef AnnexList As String) As Boolean

    Function SetInternationalConfig(Language As String, Country As String, Annex As String) As Boolean

    Function GetInfo(ByRef Info As DeviceUIAVMInfo) As Boolean

    Function SetConfig(AutoUpdateMode As AutoUpdateModeEnum) As Boolean
End Interface
