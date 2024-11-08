Imports System.Net
Imports System.Threading

Public Class FritzBoxTR64
    Inherits APIConnectorBase
    Implements IDisposable

    Public Property Ready As Boolean = False
    Private Property UseAura As Boolean = False
    Friend Shared Property FBoxIPAdresse As String
    Private Property FBTR64Desc As TR64Desc
    Friend Property XML As Serializer
    Friend Property Client As WebFunctions
    Private Property Services As List(Of Service)

    Protected Property AuthToken As String = String.Empty

    Friend Shared _FBAPIConnector As IFBoxAPIConnector
    Public Shared WriteOnly Property FBAPIConnector As IFBoxAPIConnector
        Set
            _FBAPIConnector = Value
        End Set
    End Property

#Region "Services"
    Public Property AURA As IAuraSCPD
    Public Property DECT As IDECT_SCPD
    Public Property Deviceconfig As IDeviceconfigSCPD
    Public Property Deviceinfo As IDeviceinfoSCPD
    Public Property IGD1conn As IIGD1connSCPD
    Public Property IGD2conn As IIGD2connSCPD
    Public Property IGD2ipv6fwc As IIGD2ipv6fwcSCPD
    Public Property IGDI1cfg As IIGDI1cfgSCPD
    Public Property IGDI2cfg As IIGDI2cfgSCPD
    Public Property IGDI1dsl As IIGDI1dslSCPD
    Public Property IGDI2dsl As IIGDI2dslSCPD
    Public Property Hosts As IHostsSCPD
    Public Property LANConfigSecurity As ILANConfigSecuritySCPD
    Public Property LANhostconfigmgm As ILANhostconfigmgmSCPD
    Public Property LANifconfig As ILANifconfigSCPD
    Public Property Layer3Forwarding As ILayer3ForwardingSCPD
    Public Property Mgmsrv As IMgmsrvSCPD
    Public Property Time As ITimeSCPD
    Public Property UserInterface As IUserInterfaceSCPD
    Public Property WANCommonInterfaceConfig As IWANCommonInterfaceConfigSCPD
    Public Property WANDSLInterfaceConfig As IWANDSLInterfaceConfigSCPD
    Public Property WANDSLLinkConfig As IWANDSLLinkConfigSCPD
    Public Property WANEthernetLinkConfig As IWANEthernetLinkConfigSCPD
    Public Property WANIPConnection As IWANIPConnectionSCPD
    Public Property WANPPPConnection As IWANPPPConnectionSCPD
    Public Property Wlanconfig1 As IWlanconfigSCPD
    Public Property Wlanconfig2 As IWlanconfigSCPD
    Public Property Wlanconfig3 As IWlanconfigSCPD
    Public Property Wlanconfig4 As IWlanconfigSCPD
    Public Property X_appsetup As IX_appsetupSCPD
    Public Property X_auth As IX_authSCPD
    Public Property X_contact As IX_contactSCPD
    Public Property X_filelinks As IX_filelinksSCPD
    Public Property X_HomeAuto As IX_homeautoSCPD
    Public Property X_HomePlug As IX_homeplugSCPD
    Public Property X_HostFilter As IX_hostfilterSCPD
    Public Property X_Media As IX_MediaSCPD
    Public Property X_MyFritz As IX_myfritzSCPD
    Public Property X_Speedtest As IX_speedtestSCPD
    Public Property X_remote As IX_remoteSCPD
    Public Property X_storage As IX_storageSCPD
    Public Property X_tam As IX_tamSCPD
    Public Property X_upnp As IX_upnpSCPD
    Public Property X_USPController As IX_USPControllerSCPD
    Public Property X_voip As IX_voipSCPD
    Public Property X_WANFiber As IX_WANFiberSCPD
    Public Property X_WANMobileConnection As IX_WANMobileConnectionSCPD
    Public Property X_webdav As IX_webdavSCPD
    Public Property UserMode As UserModeSCPD
    Public Property HttpService As IFBoxHttpTransfer
    Public Property AHAService As IFBoxAHA
#End Region

#Region "Konstruktor"

    ''' <summary>
    ''' Initiiert eine neue TR064 Schnittstelle zur Fritz!Box. Die <see cref="NetworkCredential"/> werden hier übergeben.<br/>
    ''' Falls die auzuführende Funktion keine Anmeldung erfordert, kann <paramref name="Anmeldeinformationen"/> Nothing sein.
    ''' </summary>
    ''' <param name="FritzBoxAdresse">Die IP Adresse der Fritz!Box.</param>
    ''' <param name="Anmeldeinformationen">Die Anmeldeinformationen (Benutzername und Passwort) als <see cref="NetworkCredential"/>.</param>
    ''' <param name="APIConnector">Eine Klasse, die die Schnittstelle <see cref="IFBoxAPIConnector"/> implementiert und das Logging sowie die 2FA realisiert.</param>
    Public Sub New(FritzBoxAdresse As String, Anmeldeinformationen As NetworkCredential, Optional AuraService As Boolean = False, Optional APIConnector As IFBoxAPIConnector = Nothing)
        SetData(FritzBoxAdresse, Anmeldeinformationen, AuraService, APIConnector)
    End Sub

    Public Sub New(Settings As Settings)
        If Settings IsNot Nothing Then
            With Settings
                SetData(.FritzBoxAdresse, .Anmeldeinformationen, .AuraService, .FBAPIConnector)
            End With
        End If
    End Sub
#End Region

#Region "Initialisierung"
    Private Sub SetData(FritzBoxAdresse As String, Anmeldeinformationen As NetworkCredential, Optional AuraService As Boolean = False, Optional APIConnector As IFBoxAPIConnector = Nothing)
        ' Setze die Verknüpfung zum FBAPIConnector
        _FBAPIConnector = APIConnector

        ' IP Adresse der Fritz!Box setzen
        _FBoxIPAdresse = FritzBoxAdresse

        ' Lade die Klasse für die http-Funktionalitäten.
        _Client = New WebFunctions(Anmeldeinformationen)

        ' XML initialisieren
        _XML = New Serializer(Client)

        _UseAura = AuraService

        ' Lade die TR064 Services, LUA und UserMode
        InitServices()

        ' Lade alle relevanten Daten von der Fritz!Box und initialisiere die Services
        Ready = ConnectTR064()
    End Sub

    ''' <summary>
    ''' Lädt alle Description herunter und initialisiert die Services
    ''' </summary>
    Private Async Function ConnectTR064Async() As Task(Of Boolean)
        Dim Description As String
        Dim Desc As TR64Desc
        Services = New List(Of Service)

        If Client.Ping(FBoxIPAdresse) Then

            For Each DESCFile As DESCFiles In [Enum].GetValues(GetType(DESCFiles))

                ' AURA nur laden, wenn es gewünscht ist
                If Not DESCFile = DESCFiles.auradesc Or UseAura Then
                    ' Herunterladen
                    Description = Await Client.GetStringWebClientAsync(New Uri($"{Uri.UriSchemeHttp}://{FBoxIPAdresse}:{49000}{DESCFile.Description}"))

                    If Description.IsNotStringNothingOrEmpty Then
                        ' Deserialisieren
                        Desc = Await XML.DeserializeAsyncData(Of TR64Desc)(Description)

                        If Desc IsNot Nothing Then
                            ' Ermittle alle vorhandenen Services
                            Services.AddRange(Desc.Device.GetAllServices())

                            SendLog(LogLevel.Trace, $"Description {DESCFile} verarbeitet")
                        Else
                            SendLog(LogLevel.Error, $"Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Deserialisieren der {DESCFile}.")
                        End If
                    Else
                        SendLog(LogLevel.Error, $"Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Herunterladen der {DESCFile}.")
                        Return False
                    End If
                End If
            Next
            SendLog(LogLevel.Info, $"Fritz!Box TR064 API mit {Services.Count} Services erfolgreich initialisiert.")
            Return True
        Else
            SendLog(LogLevel.Error, $"Fritz!Box TR064 API kann nicht initialisiert werden: Fritz!Box unter {FBoxIPAdresse} nicht verfügbar.")
        End If

        Return False
    End Function

    Private Function ConnectTR064() As Boolean
        Dim T As Task(Of Boolean) = Task.Run(Function() ConnectTR064Async())
        T.Wait()
        Return T.Result
    End Function

    ''' <summary>
    ''' Lade die AVM Services
    ''' </summary>
    Private Sub InitAVMServices()

        AURA = New AuraSCPD(AddressOf TR064Start)
        DECT = New DECT_SCPD(AddressOf TR064Start)
        Deviceconfig = New DeviceconfigSCPD(AddressOf TR064Start)
        Deviceinfo = New DeviceinfoSCPD(AddressOf TR064Start, XML)
        Hosts = New HostsSCPD(AddressOf TR064Start, XML)
        IGD1conn = New IGD1connSCPD(AddressOf TR064Start)
        IGD2conn = New IGD2connSCPD(AddressOf TR064Start)
        IGD2ipv6fwc = New IGD2ipv6fwcSCPD(AddressOf TR064Start)
        IGDI1cfg = New IGDI1cfgSCPD(AddressOf TR064Start)
        IGDI2cfg = New IGDI2cfgSCPD(AddressOf TR064Start)
        IGDI1dsl = New IGDI1dslSCPD(AddressOf TR064Start)
        IGDI2dsl = New IGDI2dslSCPD(AddressOf TR064Start)
        LANConfigSecurity = New LANConfigSecuritySCPD(AddressOf TR064Start)
        LANhostconfigmgm = New LANhostconfigmgmSCPD(AddressOf TR064Start)
        LANifconfig = New LANifconfigSCPD(AddressOf TR064Start)
        Layer3Forwarding = New Layer3ForwardingSCPD(AddressOf TR064Start)
        Mgmsrv = New MgmsrvSCPD(AddressOf TR064Start)
        Time = New TimeSCPD(AddressOf TR064Start)
        UserInterface = New UserInterfaceSCPD(AddressOf TR064Start)
        WANCommonInterfaceConfig = New WANCommonInterfaceConfigSCPD(AddressOf TR064Start)
        WANDSLInterfaceConfig = New WANDSLInterfaceConfigSCPD(AddressOf TR064Start)
        WANDSLLinkConfig = New WANDSLLinkConfigSCPD(AddressOf TR064Start)
        WANEthernetLinkConfig = New WANEthernetLinkConfigSCPD(AddressOf TR064Start)
        WANIPConnection = New WANIPConnectionSCPD(AddressOf TR064Start)
        WANPPPConnection = New WANPPPConnectionSCPD(AddressOf TR064Start)
        Wlanconfig1 = New WlanconfigSCPD(AddressOf TR064Start, 1, XML)
        Wlanconfig2 = New WlanconfigSCPD(AddressOf TR064Start, 2, XML)
        Wlanconfig3 = New WlanconfigSCPD(AddressOf TR064Start, 3, XML)
        Wlanconfig4 = New WlanconfigSCPD(AddressOf TR064Start, 4, XML)
        X_appsetup = New X_appsetupSCPD(AddressOf TR064Start)
        X_auth = New X_authSCPD(AddressOf TR064Start)
        X_contact = New X_contactSCPD(AddressOf TR064Start, XML)
        X_filelinks = New X_filelinksSCPD(AddressOf TR064Start, XML)
        X_HomeAuto = New X_homeautoSCPD(AddressOf TR064Start)
        X_HomePlug = New X_homePlugSCPD(AddressOf TR064Start)
        X_HostFilter = New X_hostfilterSCPD(AddressOf TR064Start)
        X_MyFritz = New X_myfritzSCPD(AddressOf TR064Start)
        X_Media = New X_MediaSCPD(AddressOf TR064Start)
        X_remote = New X_remoteSCPD(AddressOf TR064Start, XML)
        X_Speedtest = New X_SpeedtestSCPD(AddressOf TR064Start)
        X_storage = New X_storageSCPD(AddressOf TR064Start)
        X_tam = New X_tamSCPD(AddressOf TR064Start, XML)
        X_upnp = New X_upnpSCPD(AddressOf TR064Start)
        X_USPController = New X_USPControllerSCPD(AddressOf TR064Start)
        X_voip = New X_voipSCPD(AddressOf TR064Start, XML)
        X_WANFiber = New X_WANFiberSCPD(AddressOf TR064Start)
        X_WANMobileConnection = New X_WANMobileConnectionSCPD(AddressOf TR064Start)
        X_webdav = New X_webdavSCPD(AddressOf TR064Start)

    End Sub

    Private Sub InitServices()
        ' Lade die AVM Services unabhängig davon, ob die Verbindung geklappt hat
        InitAVMServices()

        ' Lade den UserModus
        UserMode = New UserModeSCPD(AddressOf TR064Start)

        ' Lade den LuaService
        HttpService = New FBoxHttpTransfer(Client)

        ' Lade das AVM Home Automation HTTP Interface 
        AHAService = New FBoxAHA(Me)
    End Sub

    ''' <summary>
    ''' Aktualisiert die Anmeldeinformationen zur Fritz!Box. (Passwortwechsel)
    ''' </summary>
    ''' <param name="Anmeldeinformationen">Neue Anmeldeinformationen</param>
    Public Sub UpdateCredential(Anmeldeinformationen As NetworkCredential)

        If Anmeldeinformationen.SecurePassword.Length.IsNotZero Then
            ' Aktualisiere die Klasse für die http-Funktionalitäten.
            Client.UpdateCredential(Anmeldeinformationen)

            SendLog(LogLevel.Info, $"Anmeldeinformationen aktualisiert. Login für {Anmeldeinformationen.UserName}:{If(Deviceconfig.LoginTest, String.Empty, " nicht")} erfolgreich")
        End If

    End Sub
#End Region

    Private Function TR064Start(SCPDURL As SCPDFiles,
                                ActionName As String,
                                ServiceID As Integer,
                                Optional InputArguments As Dictionary(Of String, String) = Nothing) As Dictionary(Of String, String)

        Dim ReturnValues As New Dictionary(Of String, String) From {{"Error", $"Service für {SCPDURL} und {ActionName} nicht vorhanden!"}}

        ' Versuche einen Start, falls noch nicht geschehen
        If Not Ready Then ConnectTR064()

        ' Prüfe, ob die Schnittstelle grundsätzlich verbunden ist.
        If Ready Then
            ' Ermittle den Service, welcher zu der übergebenen SCPD gehört
            Dim Service As Service = GetService(SCPDURL, ServiceID)

            If Service IsNot Nothing Then
                ReturnValues = Service.StartAction(ActionName, InputArguments, AuthToken)

                ' Falls ein Fehler aufgetreten ist: prüfe, ob AuthenticationRequired
                If ReturnValues.ContainsKey("errorCode") AndAlso CType(ReturnValues("errorCode"), AVMErrorCodes) = AVMErrorCodes.AuthenticationRequired Then

                    SendLog(LogLevel.Info, $"Second Factor Authentication Required")


                    Dim T As Task(Of Boolean) = Task.Run(Function() WaitForAuth())

                    With T
                        ' Starte den Task
                        '.Run(Function() WaitForAuth())
                        ' Warte bis der Auth Prozess abgeschlossen ist
                        .Wait()
                        ' Fahre mit der Ausführung fort
                        If .Result Then
                            ' Signalisiere, dass die Authentifikation erfolreich war
                            AuthenticationSuccesful = True

                            ' Führe den Aufruf mit dem Token nochmal durch
                            SendLog(LogLevel.Debug, $"Start Action {ActionName} with token {AuthToken}")

                            ReturnValues = Service.StartAction(ActionName, InputArguments, AuthToken)
                        End If

                    End With

                End If

            End If

        End If
        Return ReturnValues
    End Function


    Private Function GetService(SCPDURL As SCPDFiles, ServiceID As Integer) As Service

        If Services IsNot Nothing AndAlso Services.Any Then

            With Services.Where(Function(Service)

                                    Select Case SCPDURL
                                        ' Sonderfall IGD1 und IGD2: Über die SCPDURL kann der Service nicht eineindeutig bestimmt werden
                                        Case SCPDFiles.igd1icfgSCPD, SCPDFiles.igd1dslSCPD
                                            Return Service.SCPDURL.AreEqual(SCPDURL.Description) And Service.ControlURL.StartsWith("/igdupnp/")

                                        Case SCPDFiles.igd2icfgSCPD, SCPDFiles.igd2dslSCPD
                                            Return Service.SCPDURL.AreEqual(SCPDURL.Description) And Service.ControlURL.StartsWith("/igd2upnp/")

                                        ' Sonderfall WLAN: Es kann mehrere geben. Hier ist die ServiceID relevant.
                                        Case SCPDFiles.wlanconfigSCPD
                                            Return Service.SCPDURL.AreEqual(SCPDURL.Description) And Service.ServiceType.EndsWith(ServiceID.ToString)

                                        Case Else
                                            Return Service.SCPDURL.AreEqual(SCPDURL.Description)

                                    End Select
                                End Function)
                If .Any Then

                    ' Prüfe, ob der Service bereits initialisiert wurde
                    If Not .First.Initialized Then .First.Init(XML, Client)

                    Return .First
                Else
                    SendLog(LogLevel.Error, $"Service für {SCPDURL} nicht vorhanden.")
                End If
            End With

        Else
            SendLog(LogLevel.Error, $"Keine Services geladen.")
            Return Nothing
        End If
        Return Nothing
    End Function

#Region "Abfragen"

    ''' <summary>
    ''' Gibt die Firmware der Fritz!Box aus der TR-064 Description zurück.
    ''' </summary>
    ''' <returns>Fritz!Box Firmware Version</returns>
    Public ReadOnly Property DisplayVersion As String
        Get
            Return If(FBTR64Desc IsNot Nothing AndAlso FBTR64Desc.SystemVersion IsNot Nothing, FBTR64Desc.SystemVersion.Display, String.Empty)
        End Get
    End Property

    ''' <summary>
    ''' Gibt die Hardware-Version der Fritz!Box zurück.
    ''' </summary>
    Public ReadOnly Property HardwareVersion As Integer
        Get
            SendLog(LogLevel.Trace, $"Fritz!Box Hardware: {FBTR64Desc.SystemVersion.HW}")
            Return FBTR64Desc.SystemVersion.HW
        End Get
    End Property

    ''' <summary>
    ''' Gibt die Major-Firmwareversion der Fritz!Box zurück.
    ''' </summary>
    Public ReadOnly Property Major As Integer
        Get
            SendLog(LogLevel.Trace, $"Fritz!Box Major: {FBTR64Desc.SystemVersion.Major}")
            Return FBTR64Desc.SystemVersion.Major
        End Get
    End Property

    ''' <summary>
    ''' Gibt die Minor-Firmwareversion der Fritz!Box zurück.
    ''' </summary>
    Public ReadOnly Property Minor As Integer
        Get
            SendLog(LogLevel.Trace, $"Fritz!Box Minor: {FBTR64Desc.SystemVersion.Minor}")
            Return FBTR64Desc.SystemVersion.Minor
        End Get
    End Property

    ''' <summary>
    ''' Gibt den FriendlyName der Fritz!Box zurück.
    ''' </summary>
    Public ReadOnly Property FriendlyName As String
        Get
            Return If(FBTR64Desc IsNot Nothing AndAlso FBTR64Desc.Device IsNot Nothing, FBTR64Desc.Device.FriendlyName, "Keine Verbindung zu einer Fritz!Box hergestellt.")
        End Get
    End Property

#End Region

#Region "Second Factor Authentication"
    Private Function WaitForAuth() As Boolean
        Dim Token As String = String.Empty
        Dim Methods As String = String.Empty
        Dim RetState As AuthStateEnum

        If X_auth.SetConfig(AuthActionEnum.start, Token, RetState, Methods) Then
            If RetState = AuthStateEnum.waitingforauth Then
                ' Die Fritz!Box wartet auf eine Authentifizierung

                ' Setze den übermittelten Auth-Token ind die Variable
                AuthToken = Token

                ' Signalisiere die notwendige Zweifaktor-Authentifizierung
                Signal2FA(Methods)

                ' Warte auf den Abschluss der Authentifizierung
                While Not AbortAuthentication And (X_auth.GetState(RetState) AndAlso RetState = AuthStateEnum.waitingforauth)
                    ' Nicht schön, aber selten
                    Thread.Sleep(1000)
                End While

                ' Brich die Authentifikation ab, falls dies signalisiert wurde
                If AbortAuthentication Then
                    X_auth.SetConfig(AuthActionEnum.stop, Token, RetState, Methods)
                    ' Setze den Token zurück
                    AuthToken = Token
                End If

                ' Wenn der Vorgang abgeschlossen wurde, gib das als Boolean zurück: True = Authenticated
                SendLog(LogLevel.Info, $"Second Factor Authentication Result: {RetState}")

                Return RetState = AuthStateEnum.authenticated
            Else
                SendLog(LogLevel.Warning, $"Second Factor Authentication not ready: {RetState}")
            End If

        End If

        Return False
    End Function

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            Client.Dispose()
        End If
        disposedValue = True

    End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

End Class
