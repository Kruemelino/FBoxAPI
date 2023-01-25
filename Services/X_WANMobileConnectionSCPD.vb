Imports System.Net.NetworkInformation
''' <summary>
''' TR-064 Support – X_AVM-DE_WANMobileConnection
''' Date: 2022-11-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanmobileconnSCPD.pdf</see>
''' </summary>
Friend Class X_WANMobileConnectionSCPD
    Implements IX_WANMobileConnectionSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 11, 7) Implements IX_WANMobileConnectionSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_WANMobileConnectionSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_wanmobileconn Implements IX_WANMobileConnectionSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub
    Public Function GetInfo(ByRef Info As WANMobileConnectionInfo) As Boolean Implements IX_WANMobileConnectionSCPD.GetInfo
        If Info Is Nothing Then Info = New WANMobileConnectionInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewEnable") Then

                Return .TryGetValueEx("NewEnable", Info.Enable) And
                       .TryGetValueEx("NewStatus", Info.Status) And
                       .TryGetValueEx("NewPINFailureCount", Info.PINFailureCount) And
                       .TryGetValueEx("NewPUKFailureCount", Info.PUKFailureCount)
            Else
                Return False
            End If
        End With

    End Function

    Public Function GetInfoEx(ByRef Info As WANMobileConnectionInfoEx) As Boolean Implements IX_WANMobileConnectionSCPD.GetInfoEx
        If Info Is Nothing Then Info = New WANMobileConnectionInfoEx

        With TR064Start(ServiceFile, "GetInfoEx", Nothing)

            If .ContainsKey("NewSerialNumber") Then

                Return .TryGetValueEx("NewSerialNumber", Info.SerialNumber) And
                       .TryGetValueEx("NewEnableVoIPPDN", Info.EnableVoIPPDN) And
                       .TryGetValueEx("NewPPPUsername", Info.PPPUsername) And
                       .TryGetValueEx("NewPPPUsernameVoIP", Info.PPPUsernameVoIP) And
                       .TryGetValueEx("NewSoftwareVersion", Info.SoftwareVersion) And
                       .TryGetValueEx("NewUptime", Info.Uptime) And
                       .TryGetValueEx("NewPDN1_MTU", Info.PDN1_MTU) And
                       .TryGetValueEx("NewPDN2_MTU", Info.PDN2_MTU) And
                       .TryGetValueEx("NewIMSI", Info.IMSI) And
                       .TryGetValueEx("NewAPN_VoIP", Info.APN_VoIP) And
                       .TryGetValueEx("NewAPN", Info.APN) And
                       .TryGetValueEx("NewRoaming", Info.Roaming) And
                       .TryGetValueEx("NewCurrentAccessTechnology", Info.CurrentAccessTechnology) And
                       .TryGetValueEx("NewSignalRSRP0", Info.SignalRSRP0) And
                       .TryGetValueEx("NewSignalRSRP1", Info.SignalRSRP1) And
                       .TryGetValueEx("NewCellList", Info.CellList)
            Else
                Return False
            End If
        End With
    End Function

    Public Function SetPIN(PIN As String) As Boolean Implements IX_WANMobileConnectionSCPD.SetPIN
        Return Not TR064Start(ServiceFile, "SetPIN", New Dictionary(Of String, String) From {{"NewPIN", PIN}}).ContainsKey("Error")
    End Function

    Public Function SetPUK(PUK As String, PIN As String) As Boolean Implements IX_WANMobileConnectionSCPD.SetPUK
        Return Not TR064Start(ServiceFile, "SetPUK", New Dictionary(Of String, String) From {{"NewPUK", PUK},
                                                                                             {"NewPIN", PIN}}).ContainsKey("Error")
    End Function

    Public Function SetAccessTechnology(AccessTechnology As String) As Boolean Implements IX_WANMobileConnectionSCPD.SetAccessTechnology
        Return Not TR064Start(ServiceFile, "SetAccessTechnology", New Dictionary(Of String, String) From {{"NewAccessTechnology", AccessTechnology}}).ContainsKey("Error")
    End Function

    Public Function GetAccessTechnology(ByRef AccessTechnology As String,
                                        ByRef PossibleAccessTechnology As String,
                                        ByRef CurrentAccessTechnology As String) As Boolean Implements IX_WANMobileConnectionSCPD.GetAccessTechnology

        With TR064Start(ServiceFile, "GetAccessTechnology", Nothing)
            Return .TryGetValueEx("NewAccessTechnology", AccessTechnology) And
                   .TryGetValueEx("NewPossibleAccessTechnology", PossibleAccessTechnology) And
                   .TryGetValueEx("NewCurrentAccessTechnology", CurrentAccessTechnology)

        End With
    End Function

    Public Function SetEnabledBandCapabilities(BandCapabilitiesLTE As String,
                                               BandCapabilities5GNSA As String,
                                               BandCapabilities5GSA As String) As Boolean Implements IX_WANMobileConnectionSCPD.SetEnabledBandCapabilities

        Return Not TR064Start(ServiceFile, "SetEnabledBandCapabilities", New Dictionary(Of String, String) From {{"NewBandCapabilitiesLTE", BandCapabilitiesLTE},
                                                                                                                 {"NewBandCapabilities5GNSA", BandCapabilities5GNSA},
                                                                                                                 {"NewBandCapabilities5GSA", BandCapabilities5GSA}}).ContainsKey("Error")

    End Function

    Public Function GetEnabledBandCapabilities(ByRef BandCapabilitiesLTE As String,
                                               ByRef BandCapabilities5GNSA As String,
                                               ByRef BandCapabilities5GSA As String) As Boolean Implements IX_WANMobileConnectionSCPD.GetEnabledBandCapabilities

        With TR064Start(ServiceFile, "GetEnabledBandCapabilities", Nothing)
            Return .TryGetValueEx("NewBandCapabilitiesLTE", BandCapabilitiesLTE) And
                   .TryGetValueEx("NewBandCapabilities5GNSA", BandCapabilities5GNSA) And
                   .TryGetValueEx("NewBandCapabilities5GSA", BandCapabilities5GSA)

        End With
    End Function

    Public Function GetBandCapabilities(ByRef BandCapabilitiesLTE As String, ByRef BandCapabilities5GNSA As String, ByRef BandCapabilities5GSA As String) As Boolean Implements IX_WANMobileConnectionSCPD.GetBandCapabilities

        With TR064Start(ServiceFile, "GetBandCapabilities", Nothing)
            Return .TryGetValueEx("NewBandCapabilitiesLTE", BandCapabilitiesLTE) And
                   .TryGetValueEx("NewBandCapabilities5GNSA", BandCapabilities5GNSA) And
                   .TryGetValueEx("NewBandCapabilities5GSA", BandCapabilities5GSA)

        End With
    End Function
End Class
