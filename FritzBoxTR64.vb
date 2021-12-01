Imports System.Net

Public Class FritzBoxTR64
    Implements IDisposable

    Public Event Status As EventHandler(Of NotifyEventArgs(Of LogMessage))
    Public Property Bereit As Boolean = False
    Private Property FBTR64Desc As TR64Desc
    Private Property Credential As NetworkCredential
    Private Property FBoxIPAdresse As String

#Region "Services"
    Public Property Deviceconfig As DeviceconfigSCPD
    Public Property Deviceinfo As DeviceinfoSCPD
    Public Property LANConfigSecurity As LANConfigSecuritySCPD
    Public Property X_contact As X_contactSCPD
    Public Property X_tam As X_tamSCPD
    Public Property X_voip As X_voipSCPD
    Public Property UserMode As UserModeSCPD
    Public Property WANCommonInterfaceConfig As WANCommonInterfaceConfigSCPD
#End Region


    ''' <summary>
    ''' Initiiert eine neue TR064 Schnittstelle zur Fritz!Box. Die <see cref="NetworkCredential"/> werden hier übergeben.<br/>
    ''' Falls die auzuführende Funktion keine Anmeldung erfordert, kann <paramref name="Anmeldeinformationen"/> Nothing sein.
    ''' </summary>
    ''' <param name="FritzBoxAdresse">Die IP Adresse der Fritz!Box.</param>
    ''' <param name="Anmeldeinformationen">Die Anmeldeinformationen (Benutzername und Passwort) als <see cref="NetworkCredential"/>.</param>
    Public Sub New(FritzBoxAdresse As String, Anmeldeinformationen As NetworkCredential)
        Dim Response As String = String.Empty

        ' IP Adresse der Fritz!Box setzen
        FBoxIPAdresse = FritzBoxAdresse

        ' Netzwerkanmeldeinformationen zuweisen
        Credential = Anmeldeinformationen

        ' ByPass SSL Certificate Validation Checking
        ServicePointManager.ServerCertificateValidationCallback = Function(se As Object, cert As System.Security.Cryptography.X509Certificates.X509Certificate, chain As System.Security.Cryptography.X509Certificates.X509Chain, sslerror As Security.SslPolicyErrors) True

        ' Funktioniert nicht: ByPass SSL Certificate Validation Checking wird ignoriert. Es kommt zu unerklärlichen System.Net.WebException in FritzBoxPOST
        ' FBTR64Desc = DeserializeObject(Of TR64Desc)($"http://{FBoxIPAdresse}:{FritzBoxDefault.PDfltFBSOAP}{Tr064Files.tr64desc}")

        ' Workaround: XML-Datei als String herunterladen und separat deserialisieren
        If Ping(FBoxIPAdresse) Then
            ' Herunterladen
            If DownloadString(New UriBuilder(Uri.UriSchemeHttps, FBoxIPAdresse, DfltTR064PortSSL, SCPDFiles.tr64desc.Description).Uri, Response) Then
                ' Deserialisieren
                If DeserializeXML(Response, False, FBTR64Desc) Then
                    ' Füge das Flag hinzu, dass die TR064-Schnittstelle bereit ist.
                    Bereit = True
                    PushStatus(LogLevel.Debug, "Fritz!Box TR064 API erfolgreich initialisiert.")

                    ' Lade die AVM Services
                    Deviceconfig = New DeviceconfigSCPD(AddressOf TR064Start, AddressOf PushStatus)
                    Deviceinfo = New DeviceinfoSCPD(AddressOf TR064Start, AddressOf PushStatus)
                    LANConfigSecurity = New LANConfigSecuritySCPD(AddressOf TR064Start, AddressOf PushStatus)
                    WANCommonInterfaceConfig = New WANCommonInterfaceConfigSCPD(AddressOf TR064Start, AddressOf PushStatus)
                    X_contact = New X_contactSCPD(AddressOf TR064Start, AddressOf PushStatus)
                    X_tam = New X_tamSCPD(AddressOf TR064Start, AddressOf PushStatus)
                    X_voip = New X_voipSCPD(AddressOf TR064Start, AddressOf PushStatus)

                    ' Lade den UserModus
                    UserMode = New UserModeSCPD(AddressOf TR064Start, AddressOf PushStatus)
                Else
                    PushStatus(LogLevel.Error, "Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Deserialisieren der FBTR64Desc.")
                End If
            Else
                PushStatus(LogLevel.Error, "Fritz!Box TR064 API kann nicht initialisiert werden: Fehler beim Herunterladen der FBTR64Desc.")
            End If
        Else
            PushStatus(LogLevel.Error, $"Fritz!Box TR064 API kann nicht initialisiert werden: Fritz!Box unter {FBoxIPAdresse} nicht verfügbar.")
        End If
    End Sub

    Private Sub PushStatus(Level As LogLevel, Message As String)
        RaiseEvent Status(Me, New NotifyEventArgs(Of LogMessage)(New LogMessage(Level, Message)))
    End Sub

    Private Function TR064Start(SCPDURL As SCPDFiles, ActionName As String, Optional InputHashTable As Hashtable = Nothing) As Hashtable

        If Bereit Then
            With GetService(SCPDURL)
                If?.ActionExists(ActionName) Then
                    If .CheckInput(ActionName, InputHashTable) Then
                        Return .Start(.GetActionByName(ActionName), InputHashTable, Credential)
                    Else
                        PushStatus(LogLevel.Error, $"InputData for Action '{ActionName}' not valid!")
                    End If
                Else
                    PushStatus(LogLevel.Error, $"Action '{ActionName}'does not exist!")
                End If
            End With
        End If

        ' TODO Fehlermeldung konkretisieren
        Return New Hashtable From {{"Error", String.Empty}}
    End Function

    Private Function GetService(SCPDURL As SCPDFiles) As Service

        If FBTR64Desc IsNot Nothing AndAlso FBTR64Desc.Device.ServiceList.Any Then
            ' Suche den angeforderten Service
            Dim FBoxService As Service = FBTR64Desc.Device.ServiceList.Find(Function(Service) Service.SCPDURL.AreEqual(SCPDURL.Description))

            ' Weise die Fritz!Box IP-Adresse zu
            If FBoxService IsNot Nothing Then FBoxService.FBoxIPAdresse = FBoxIPAdresse

            Return FBoxService
        Else

            PushStatus(LogLevel.Error, $"SOAP zur Fritz!Box ist nicht bereit: {FBoxIPAdresse}")
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
            PushStatus(LogLevel.Trace, $"Fritz!Box Hardware: {FBTR64Desc.SystemVersion.HW}")
            Return FBTR64Desc.SystemVersion.HW
        End Get
    End Property

    Public ReadOnly Property Major As Integer
        Get
            PushStatus(LogLevel.Trace, $"Fritz!Box Major: {FBTR64Desc.SystemVersion.Major}")
            Return FBTR64Desc.SystemVersion.Major
        End Get
    End Property

    Public ReadOnly Property Minor As Integer
        Get
            PushStatus(LogLevel.Trace, $"Fritz!Box Minor: {FBTR64Desc.SystemVersion.Minor}")
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
    End Sub

    ' Dieser Code wird von Visual Basic hinzugefügt, um das Dispose-Muster richtig zu implementieren.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(disposing As Boolean) weiter oben ein.
        Dispose(True)
    End Sub
#End Region

End Class
