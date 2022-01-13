''' <summary>
''' TR-064 Support – UserInterface
''' Date: 2019-01-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/userifSCPD.pdf</see>
''' </summary>
Public Class UserInterfaceSCPD
    Implements IUserInterfaceSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IUserInterfaceSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IUserInterfaceSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IUserInterfaceSCPD.Servicefile
    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.userifSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetInfo(ByRef Info As DeviceUIInfo) As Boolean Implements IUserInterfaceSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceUIInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewUpgradeAvailable") Then

                Info.UpgradeAvailable = CBool(.Item("NewUpgradeAvailable"))
                Info.PasswordRequired = CBool(.Item("NewPasswordRequired"))
                Info.PasswordUserSelectable = CBool(.Item("NewPasswordUserSelectable"))
                Info.WarrantyDate = .Item("NewWarrantyDate").ToString
                Info.Version = .Item("NewX_AVM-DE_Version").ToString
                Info.DownloadURL = .Item("NewX_AVM-DE_DownloadURL").ToString
                Info.InfoURL = .Item("NewX_AVM-DE_InfoURL").ToString
                Info.UpdateState = .Item("NewX_AVM-DE_UpdateState").ToString
                Info.LaborVersion = .Item("NewX_AVM-DE_LaborVersion").ToString


                PushStatus.Invoke(LogLevel.Debug, $"Geräteinformationen (GetInfo UserInterfaceSCPD) der Fritz!Box: {Info.UpgradeAvailable}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Keine Geräteinformationen (GetInfo UserInterfaceSCPD) der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function CheckUpdate(LaborVersion As String) As Boolean Implements IUserInterfaceSCPD.CheckUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_CheckUpdate", New Hashtable From {{"NewX_AVM-DE_LaborVersion", LaborVersion}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DoPrepareCGI(ByRef CGI As String, ByRef SessionID As String) As Boolean Implements IUserInterfaceSCPD.DoPrepareCGI
        With TR064Start(ServiceFile, "X_AVM-DE_DoPrepareCGI", Nothing)

            If .ContainsKey("NewX_AVM-DE_CGI") And .ContainsKey("NewX_AVM-DE_SesssionID") Then

                CGI = .Item("NewX_AVM-DE_CGI").ToString
                SessionID = .Item("NewX_AVM-DE_SesssionID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"CGI der Fritz!Box: {CGI}, SessionID: {SessionID}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Fehler bei X_AVM-DE_DoPrepareCGI: '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function DoUpdate(ByRef UpgradeAvailable As Boolean, ByRef UpdateState As UpdateStateEnum) As Boolean Implements IUserInterfaceSCPD.DoUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_DoUpdate", Nothing)

            If .ContainsKey("NewUpgradeAvailable") And .ContainsKey("NewX_AVM-DE_UpdateState") Then

                UpgradeAvailable = CBool(.Item("NewUpgradeAvailable"))
                UpdateState = CType(.Item("NewX_AVM-DE_UpdateState"), UpdateStateEnum)

                PushStatus.Invoke(LogLevel.Debug, $"Update verfügbar: {UpgradeAvailable}, UpdateState: {UpdateState}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Fehler bei X_AVM-DE_DoUpdate: '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function DoManualUpdate(DownloadURL As String, AllowDowngrade As Boolean) As Boolean Implements IUserInterfaceSCPD.DoManualUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_DoManualUpdate", New Hashtable From {{"NewX_AVM-DE_DownloadURL", DownloadURL},
                                                                                    {"NewX_AVM-DE_AllowDowngrade", AllowDowngrade.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetInternationalConfig(ByRef Language As String, ByRef Country As String, ByRef Annex As String, ByRef LanguageList As String, ByRef CountryList As String, ByRef AnnexList As String) As Boolean Implements IUserInterfaceSCPD.GetInternationalConfig
        With TR064Start(ServiceFile, "X_AVM-DE_GetInternationalConfig", Nothing)

            If .ContainsKey("X_AVM-DE_Language") And .ContainsKey("NewX_AVM-DE_LanguageList") Then

                Language = .Item("NewX_AVM-DE_Language").ToString
                Country = .Item("NewX_AVM-DE_Country").ToString
                Annex = .Item("NewX_AVM-DE_Annex").ToString
                Language = .Item("NewX_AVM-DE_LanguageList").ToString
                CountryList = .Item("NewX_AVM-DE_CountryList").ToString
                AnnexList = .Item("NewX_AVM-DE_AnnexList").ToString


                PushStatus.Invoke(LogLevel.Debug, $"InternationalConfig erfolgreich")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Fehler bei X_AVM-DE_GetInternationalConfig: '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetInternationalConfig(Language As String, Country As String, Annex As String) As Boolean Implements IUserInterfaceSCPD.SetInternationalConfig
        With TR064Start(ServiceFile, "X_AVM-DE_SetInternationalConfig", New Hashtable From {{"NewX_AVM-DE_Language", Language},
                                                                                            {"NewX_AVM-DE_Country", Country},
                                                                                            {"NewX_AVM-DE_Annex", Annex}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetInfo(ByRef Info As DeviceUIAVMInfo) As Boolean Implements IUserInterfaceSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceUIAVMInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetInfo", Nothing)

            If .ContainsKey("NewUpgradeAvailable") Then

                Info.AutoUpdateMode = CType(.Item("NewX_AVM-DE_AutoUpdateMode"), AutoUpdateModeEnum)
                Info.UpdateTime = .Item("DE_UpdateTime").ToString
                Info.LastFwVersion = .Item("NewX_AVM-DE_LastFwVersion").ToString
                Info.LastInfoUrl = .Item("NewX_AVM-DE_LastInfoUrl").ToString
                Info.CurrentFwVersion = .Item("NewX_AVM-DE_CurrentFwVersion").ToString
                Info.UpdateSuccessful = CType(.Item("NewX_AVM-DE_UpdateSuccessful"), UpdateEnum)

                PushStatus.Invoke(LogLevel.Debug, $"Geräteinformationen (X_AVM-DE_GetInfo UserInterfaceSCPD) der Fritz!Box: {Info.LastFwVersion}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Fehler bei: X_AVM-DE_GetInfo (UserInterfaceSCPD) '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetConfig(AutoUpdateMode As AutoUpdateModeEnum) As Boolean Implements IUserInterfaceSCPD.SetConfig
        With TR064Start(ServiceFile, "X_AVM-DE_SetConfig", New Hashtable From {{"NewX_AVM-DE_AutoUpdateMode", AutoUpdateMode.ToString}})
            Return Not .ContainsKey("Error")
        End With
    End Function

End Class
