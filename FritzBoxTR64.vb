Imports System.Net

Public Class FritzBoxTR64
    Inherits LogBase
    Implements IDisposable

    Public Event Status As EventHandler(Of NotifyEventArgs(Of LogMessage))
    Public Property Ready As Boolean = False
    Private Property FBTR64Desc As TR64Desc
    Private Property XML As Serializer
    Private Property Http As TR064HttpBasics
    Private Property Credential As NetworkCredential
    Private Property FBoxIPAdresse As String

    Private Property Services As List(Of Service)
#Region "Services"
    Public Property DECT As IDECT_SCPD
    Public Property Deviceconfig As IDeviceconfigSCPD
    Public Property Deviceinfo As IDeviceinfoSCPD
    Public Property Hosts As IHostsSCPD
    Public Property LANConfigSecurity As ILANConfigSecuritySCPD
    Public Property UserInterface As IUserInterfaceSCPD
    Public Property WANCommonInterfaceConfig As IWANCommonInterfaceConfigSCPD
    Public Property WANDSLInterfaceConfig As IWANDSLInterfaceConfigSCPD
    Public Property WANDSLLinkConfig As IWANDSLLinkConfigSCPD
    Public Property WANEthernetLinkConfig As IWANEthernetLinkConfigSCPD
    Public Property WANIPConnection As IWANIPConnectionSCPD
    Public Property WANPPPConnection As IWANPPPConnectionSCPD
    Public Property Wlanconfig As IWlanconfigSCPD
    Public Property X_appsetup As IX_appsetupSCPD
    Public Property X_contact As IX_contactSCPD
    Public Property X_filelinks As IX_filelinksSCPD
    Public Property X_HomeAuto As IX_homeautoSCPD
    Public Property X_HomePlug As IX_homeplugSCPD
    Public Property X_HostFilter As IX_hostfilterSCPD
    Public Property X_MyFritz As IX_myfritzSCPD
    Public Property X_Speedtest As IX_speedtestSCPD
    Public Property X_remote As IX_remoteSCPD
    Public Property X_storage As IX_storageSCPD
    Public Property X_tam As IX_tamSCPD
    Public Property X_upnp As IX_upnpSCPD
    Public Property X_voip As IX_voipSCPD
    Public Property X_webdav As IX_webdavSCPD

    Public Property UserMode As UserModeSCPD
#End Region

#Region "Kontruktor"
    ''' <summary>
    ''' Initiiert eine neue TR064 Schnittstelle zur Fritz!Box. Achtung! Die Routine <see cref="Init(String, NetworkCredential)"/> muss separat ausgeführt werden./>
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Initiiert eine neue TR064 Schnittstelle zur Fritz!Box. Die <see cref="NetworkCredential"/> werden hier übergeben.<br/>
    ''' Falls die auzuführende Funktion keine Anmeldung erfordert, kann <paramref name="Anmeldeinformationen"/> Nothing sein.
    ''' </summary>
    ''' <param name="FritzBoxAdresse">Die IP Adresse der Fritz!Box.</param>
    ''' <param name="Anmeldeinformationen">Die Anmeldeinformationen (Benutzername und Passwort) als <see cref="NetworkCredential"/>.</param>
    Public Sub New(FritzBoxAdresse As String, timeout As Integer, Anmeldeinformationen As NetworkCredential)
        Init(FritzBoxAdresse, timeout, Anmeldeinformationen)
    End Sub
#End Region

#Region "Initialisierung"
    ''' <summary>
    ''' Initiiert eine neue TR064 Schnittstelle zur Fritz!Box. Die <see cref="NetworkCredential"/> werden hier übergeben.<br/>
    ''' Falls die auzuführende Funktion keine Anmeldung erfordert, kann <paramref name="Anmeldeinformationen"/> Nothing sein.
    ''' </summary>
    ''' <param name="FritzBoxAdresse">Die IP Adresse der Fritz!Box.</param>
    ''' <param name="Anmeldeinformationen">Die Anmeldeinformationen (Benutzername und Passwort) als <see cref="NetworkCredential"/>.</param>
    Public Sub Init(FritzBoxAdresse As String, timeout As Integer, Anmeldeinformationen As NetworkCredential)
        ' ByPass SSL Certificate Validation Checking
        ServicePointManager.ServerCertificateValidationCallback = Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslerror As Security.SslPolicyErrors) True

        ' IP Adresse der Fritz!Box setzen
        FBoxIPAdresse = FritzBoxAdresse

        ' Netzwerkanmeldeinformationen zuweisen
        Credential = Anmeldeinformationen

        ' Lade die Klasse für die http-Funktionalitäten.
        Http = New TR064HttpBasics(AddressOf PushStatus, timeout)

        ' Verbindung zur Fritz!Box aufbauen
        Ready = ConnectTR064()

        ' Lade die AVM Services (auch unabhängig davon, ob die Verbindung geklappt hat)
        InitAVMServices()

        ' Lade den UserModus
        UserMode = New UserModeSCPD(AddressOf TR064Start)

        If Credential Is Nothing Then
            PushStatus(CreateLog(LogLevel.Info, $"Init abgeschlossen: {FBoxIPAdresse} für eingeschränkten anonymen Zugriff."))
        Else
            ' Führe einen Logintest durch: Ermittle die Informationen zur Fritz!Box
            Ready = Deviceconfig.LoginTest()
            PushStatus(CreateLog(LogLevel.Info, $"Init abgeschlossen: {FBoxIPAdresse} für User: {Credential.UserName}: Passwort {If(Ready, "gültig", "ungültig")}"))
        End If
    End Sub

    Private Function ConnectTR064() As Boolean
        ' Funktioniert nicht: ByPass SSL Certificate Validation Checking wird ignoriert. Es kommt zu unerklärlichen System.Net.WebException in FritzBoxPOST
        ' FBTR64Desc = DeserializeObject(Of TR64Desc)($"http://{FBoxIPAdresse}:{FritzBoxDefault.PDfltFBSOAP}{Tr064Files.tr64desc}")

        ' Workaround: XML-Datei als String herunterladen und separat deserialisieren
        If Http.Ping(FBoxIPAdresse) Then
            Dim Response As String = String.Empty

            ' Herunterladen
            If Http.DownloadString(New UriBuilder(Uri.UriSchemeHttps, FBoxIPAdresse, DfltTR064PortSSL, SCPDFiles.tr64desc.Description).Uri, Response) Then

                ' XML initialisieren
                XML = New Serializer(AddressOf PushStatus)

                ' Deserialisieren
                If XML.Deserialize(Response, False, FBTR64Desc) Then

                    ' Ermittle alle vorhandenen Services
                    Services = FBTR64Desc.Device.GetAllServices()

                    PushStatus(CreateLog(LogLevel.Info, $"Fritz!Box TR064 API mit {Services.Count} Services erfolgreich initialisiert."))

                    ' Füge das Flag hinzu, dass die TR064-Schnittstelle bereit ist.
                    Return True
                Else
                    PushStatus(CreateLog(LogLevel.Error, "Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Deserialisieren der FBTR64Desc."))
                End If
            Else
                PushStatus(CreateLog(LogLevel.Error, "Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Herunterladen der FBTR64Desc."))
            End If
        Else
            PushStatus(CreateLog(LogLevel.Error, $"Fritz!Box TR064 API kann nicht initialisiert werden: Fritz!Box unter {FBoxIPAdresse} nicht verfügbar."))
        End If

        Return False
    End Function

    ''' <summary>
    ''' Lade die AVM Services
    ''' </summary>
    Private Sub InitAVMServices()

        DECT = New DECT_SCPD(AddressOf TR064Start)
        Deviceconfig = New DeviceconfigSCPD(AddressOf TR064Start)
        Deviceinfo = New DeviceinfoSCPD(AddressOf TR064Start)
        Hosts = New HostsSCPD(AddressOf TR064Start, XML)
        LANConfigSecurity = New LANConfigSecuritySCPD(AddressOf TR064Start)
        UserInterface = New UserInterfaceSCPD(AddressOf TR064Start)
        WANCommonInterfaceConfig = New WANCommonInterfaceConfigSCPD(AddressOf TR064Start)
        WANDSLInterfaceConfig = New WANDSLInterfaceConfigSCPD(AddressOf TR064Start)
        WANDSLLinkConfig = New WANDSLLinkConfigSCPD(AddressOf TR064Start)
        WANEthernetLinkConfig = New WANEthernetLinkConfigSCPD(AddressOf TR064Start)
        WANIPConnection = New WANIPConnectionSCPD(AddressOf TR064Start)
        WANPPPConnection = New WANPPPConnectionSCPD(AddressOf TR064Start)
        Wlanconfig = New WlanconfigSCPD(AddressOf TR064Start, XML)
        X_appsetup = New X_appsetupSCPD(AddressOf TR064Start)
        X_contact = New X_contactSCPD(AddressOf TR064Start, XML)
        X_filelinks = New X_filelinksSCPD(AddressOf TR064Start, XML)
        X_HomeAuto = New X_homeautoSCPD(AddressOf TR064Start)
        X_HomePlug = New X_homePlugSCPD(AddressOf TR064Start)
        X_HostFilter = New X_hostfilterSCPD(AddressOf TR064Start)
        X_MyFritz = New X_myfritzSCPD(AddressOf TR064Start)
        X_remote = New X_remoteSCPD(AddressOf TR064Start, XML)
        X_Speedtest = New X_SpeedtestSCPD(AddressOf TR064Start)
        X_storage = New X_storageSCPD(AddressOf TR064Start)
        X_tam = New X_tamSCPD(AddressOf TR064Start, XML)
        X_upnp = New X_upnpSCPD(AddressOf TR064Start)
        X_voip = New X_voipSCPD(AddressOf TR064Start, XML)
        X_webdav = New X_webdavSCPD(AddressOf TR064Start)

    End Sub

#End Region

    Private Sub PushStatus(LMsg As LogMessage)
        RaiseEvent Status(Me, New NotifyEventArgs(Of LogMessage)(LMsg))
    End Sub

    Private Function TR064Start(SCPDURL As SCPDFiles, ActionName As String, Optional InputArguments As Dictionary(Of String, String) = Nothing) As Dictionary(Of String, String)

        If Ready Then
            With GetService(SCPDURL)
                If?.ActionExists(ActionName) Then
                    If .CheckInput(ActionName, InputArguments) Then
                        Return .StartAction(.GetActionByName(ActionName), InputArguments, Http, Credential)
                    Else
                        PushStatus(CreateLog(LogLevel.Error, $"InputData for Action '{ActionName}' not valid!"))
                    End If
                Else
                    PushStatus(CreateLog(LogLevel.Error, $"Action '{ActionName}' does not exist!"))
                End If
            End With
            PushStatus(CreateLog(LogLevel.Error, "Fritz!Box TR064 API nicht gestartet (Init Routine starten!)."))

        End If
        Return New Dictionary(Of String, String) From {{"Error", $"Service für {SCPDURL} nicht vorhanden!"}}
    End Function

    Private Function GetService(SCPDURL As SCPDFiles) As Service

        If Services IsNot Nothing AndAlso Services.Any Then
            ' Suche den angeforderten Service
            Dim FBoxService As Service = Services.Find(Function(Service) Service.SCPDURL.AreEqual(SCPDURL.Description))

            ' Weise die Fritz!Box IP-Adresse zu
            If FBoxService IsNot Nothing Then
                With FBoxService
                    ' IP Adresse der Fritz!Box übergeben
                    .FBoxIPAdresse = FBoxIPAdresse
                    ' XML-Klasse übergeben
                    .XML = XML
                    ' Routine für die Statusmeldungen übergeben
                    .PushStatus = AddressOf PushStatus
                End With
            Else
                PushStatus(CreateLog(LogLevel.Error, $"Service für {SCPDURL} nicht vorhanden."))
            End If

            Return FBoxService
        Else
            PushStatus(CreateLog(LogLevel.Error, $"Keine Sercvices geladen."))
            Return Nothing
        End If

    End Function
#Region "Abfragen"

#Region "TR64Desc"
    ''' <summary>
    ''' Gibt die Firmware der Fritz!Box aus der TR-064 Description zurück.
    ''' </summary>
    ''' <returns>Fritz!Box Firmware Version</returns>
    Public ReadOnly Property DisplayVersion As String
        Get
            Return If(FBTR64Desc IsNot Nothing AndAlso FBTR64Desc.SystemVersion IsNot Nothing, FBTR64Desc.SystemVersion.Display, String.Empty)
        End Get
    End Property

    Public ReadOnly Property HardwareVersion As Integer
        Get
            PushStatus(CreateLog(LogLevel.Trace, $"Fritz!Box Hardware: {FBTR64Desc.SystemVersion.HW}"))
            Return FBTR64Desc.SystemVersion.HW
        End Get
    End Property

    Public ReadOnly Property Major As Integer
        Get
            PushStatus(CreateLog(LogLevel.Trace, $"Fritz!Box Major: {FBTR64Desc.SystemVersion.Major}"))
            Return FBTR64Desc.SystemVersion.Major
        End Get
    End Property

    Public ReadOnly Property Minor As Integer
        Get
            PushStatus(CreateLog(LogLevel.Trace, $"Fritz!Box Minor: {FBTR64Desc.SystemVersion.Minor}"))
            Return FBTR64Desc.SystemVersion.Minor
        End Get
    End Property

    Public ReadOnly Property FriendlyName As String
        Get
            Return If(FBTR64Desc IsNot Nothing AndAlso FBTR64Desc.Device IsNot Nothing, FBTR64Desc.Device.FriendlyName, "Keine Verbindung zu einer Fritz!Box hergestellt.")
        End Get
    End Property

#End Region

#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Dient zur Erkennung redundanter Aufrufe.

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            'Restore SSL Certificate Validation Checking
            ServicePointManager.ServerCertificateValidationCallback = Nothing
        End If
        disposedValue = True

        'Http = Nothing
        'XML = Nothing
    End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
        Dispose(True)
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
#End Region

End Class
