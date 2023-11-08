''' <summary>
''' TR-064 Support – UserInterface
''' Date: 2022-10-17
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/userifSCPD.pdf</see>
''' </summary>
Friend Class UserInterfaceSCPD
    Implements IUserInterfaceSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 17) Implements IUserInterfaceSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IUserInterfaceSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.userifSCPD Implements IUserInterfaceSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IUserInterfaceSCPD.ServiceID
    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Info As DeviceUIInfo) As Boolean Implements IUserInterfaceSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceUIInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewUpgradeAvailable", Info.UpgradeAvailable) And
                   .TryGetValueEx("NewPasswordRequired", Info.PasswordRequired) And
                   .TryGetValueEx("NewPasswordUserSelectable", Info.PasswordUserSelectable) And
                   .TryGetValueEx("NewWarrantyDate", Info.WarrantyDate) And
                   .TryGetValueEx("NewX_AVM-DE_Version", Info.Version) And
                   .TryGetValueEx("NewX_AVM-DE_DownloadURL", Info.DownloadURL) And
                   .TryGetValueEx("NewX_AVM-DE_InfoURL", Info.InfoURL) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateState", Info.UpdateState) And
                   .TryGetValueEx("NewX_AVM-DE_BuildType", Info.BuildType) And
                   .TryGetValueEx("NewX_AVM-DE_SetupAssistantStatus", Info.SetupAssistantStatus)

        End With
    End Function

    <Obsolete> Public Function CheckUpdate(LaborVersion As String) As Boolean Implements IUserInterfaceSCPD.CheckUpdate
        Return Not TR064Start(ServiceFile, "X_AVM-DE_CheckUpdate", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_LaborVersion", LaborVersion}}).ContainsKey("Error")
    End Function

    Public Function DoPrepareCGI(ByRef CGI As String, ByRef SessionID As String) As Boolean Implements IUserInterfaceSCPD.DoPrepareCGI
        With TR064Start(ServiceFile, "X_AVM-DE_DoPrepareCGI", ServiceID, Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_CGI", CGI) And
                   .TryGetValueEx("NewX_AVM-DE_SesssionID", SessionID)

        End With
    End Function

    Public Function DoUpdate(ByRef UpgradeAvailable As Boolean, ByRef UpdateState As UpdateStateEnum) As Boolean Implements IUserInterfaceSCPD.DoUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_DoUpdate", ServiceID, Nothing)

            Return .TryGetValueEx("NewUpgradeAvailable", UpgradeAvailable) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateState", UpdateState)

        End With
    End Function

    Public Function DoManualUpdate(DownloadURL As String, AllowDowngrade As Boolean) As Boolean Implements IUserInterfaceSCPD.DoManualUpdate
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DoManualUpdate", ServiceID,
                              New Dictionary(Of String, String) From {{"NewX_AVM-DE_DownloadURL", DownloadURL},
                                                                      {"NewX_AVM-DE_AllowDowngrade", AllowDowngrade.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetInternationalConfig(ByRef Language As String, ByRef Country As String, ByRef Annex As String, ByRef LanguageList As String, ByRef CountryList As String, ByRef AnnexList As String) As Boolean Implements IUserInterfaceSCPD.GetInternationalConfig
        With TR064Start(ServiceFile, "X_AVM-DE_GetInternationalConfig", ServiceID, Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_Language", Language) And
                   .TryGetValueEx("NewX_AVM-DE_Country", Country) And
                   .TryGetValueEx("NewX_AVM-DE_Annex", Annex) And
                   .TryGetValueEx("NewX_AVM-DE_LanguageList", LanguageList) And
                   .TryGetValueEx("NewX_AVM-DE_CountryList", CountryList) And
                   .TryGetValueEx("NewX_AVM-DE_AnnexList", AnnexList)

        End With
    End Function

    Public Function SetInternationalConfig(Language As String, Country As String, Annex As String) As Boolean Implements IUserInterfaceSCPD.SetInternationalConfig

        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetInternationalConfig", ServiceID,
                              New Dictionary(Of String, String) From {{"NewX_AVM-DE_Language", Language},
                                                                      {"NewX_AVM-DE_Country", Country},
                                                                      {"NewX_AVM-DE_Annex", Annex}}).ContainsKey("Error")

    End Function

    Public Function GetInfo(ByRef Info As DeviceUIAVMInfo) As Boolean Implements IUserInterfaceSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceUIAVMInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_AutoUpdateMode", Info.AutoUpdateMode) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateTime", Info.UpdateTime) And
                   .TryGetValueEx("NewX_AVM-DE_LastFwVersion", Info.LastFwVersion) And
                   .TryGetValueEx("NewX_AVM-DE_LastInfoUrl", Info.LastInfoUrl) And
                   .TryGetValueEx("NewX_AVM-DE_CurrentFwVersion", Info.CurrentFwVersion) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateSuccessful", Info.UpdateSuccessful)

        End With
    End Function

    Public Function SetConfig(AutoUpdateMode As AutoUpdateModeEnum) As Boolean Implements IUserInterfaceSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetConfig", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_AutoUpdateMode", AutoUpdateMode.ToString}}).ContainsKey("Error")
    End Function

End Class
