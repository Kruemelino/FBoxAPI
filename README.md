# FBoxAPI

Dieses Projekt ist ein .NET Bibliothek für die Schnittstellen TR-064 und AHA der AVM Fritz!Box. 
Das Projekt ist eine Ausgliederung aus meinem Addin für Microsoft Outlook: [Fritz!Box Telefon-dingsbums V5](https://github.com/Kruemelino/FritzBoxTelefon-dingsbums) 

Dieses Addin ist in meiner Freizeit entstanden. Ich erwarte keine Gegenleistung. Ein Danke ist ausreichend. Wer mir dennoch etwas Gutes zukommen lassen möchte kann dies gerne tun:

[![Donate](https://img.shields.io/badge/Spenden-green.svg?logo=paypal)](https://www.paypal.com/paypalme/gertmichael) ![Nuget](https://img.shields.io/nuget/v/FBoxAPI)

### Grundlagen
Die Schnittstelle basiert auf der [AVM-Dokumentation](https://avm.de/service/schnittstellen). 

### Nutzung
#### Initialisierung
Die Verwendung ist recht einfach angedacht. Es muss eine neue `FBoxAPI.FritzBoxTR64`-Klasse instanziiert werden (auch für AHA). Hierfür sind zwei Parameter erforderlich: 
IP-Adresse der Fritz!Box und die Anmeldeinformationen. Nutzername und Passwort werden in einer neuen Instanz der `System.Net.NetworkCredential`-Klasse hinterlegt und übergeben.
Es gibt mehrere Möglichkeiten die Schnittstelle zu initiieren.

- Übergabe der benötigten Daten als Einzel-Parameter
- Dimensionierung einer `FBoxAPI.Settings`-Klasse, die alle notwendigen Daten enthält

Im Folgenen ist ein kleines Beispiel aufgeführt, wie die SessionID der Fritz!Box abgefragt werden kann. 

```vbnet
Private Function GetSessionID() As String
    ' Bereitstellung der Variable(n), in die das Ergebnis gesetzt werden sollen.
    Dim SessionID As String = "0000000000000000"

    ' Erstelle Anmeldeinformationen für die Fritz!Box bereit
    Dim Nutzername As String = "Fritz"
    Dim Passwort As String = "Box"

    ' Anmeldeinformationen können Nothing sein, falls nur Actions ausgeführt werden, die keine Anmeldung erfordern.
    Dim Anmeldeinformationen As New Net.NetworkCredential(Nutzername, Passwort)

    ' Starte die TR-064 Schnittstelle zur Fritz!Box
    Using FBoxTR064 As New FBoxAPI.FritzBoxTR64("192.168.178.1", Anmeldeinformationen)

        ' Auswahl des Service
        With FBoxTR064.Deviceconfig

            ' Action ausführen
            If .GetSessionID(SessionID) Then
                ' Alles OK: SessionID enthält eine gültige SessionID
            Else
                ' Ein Fehler ist aufgetreten
            End If
        End With

    End Using

    Return SessionID

End Function
```

Alternativ:
```vbnet
FBoxTR064 = New FBoxAPI.FritzBoxTR64(New FBoxAPI.Settings With {.Anmeldeinformationen = Anmeldeinformationen,
                                                                .FritzBoxAdresse = "192.168.178.1",
                                                                .FBAPIConnector = New FBoxAPIConnector,
                                                                .AuraService = True})
```

Hinweis: Wenn der AURA-Service (AVM USB Remote Access) verwendet werden soll, muss dies bei der Initialisierung der Schnittstelle übergegeben werden. 
Dies ist über die Eigenschaft `AuraService` der `FBoxAPI.Settings`-Klasse möglich.

#### Logging
Mit Hilfe des `FBAPIConnector`-Schnittstelle kann eine eigene Routine verknüpft werden, die das Logging übernimmt.
Die Schnittstelle gibt folgende relevante Daten in der Containerklasse `FBoxAPI.LogMessage` für das Logging aus:

* Level (`System.Enum`) für das LogLevel (`Trace` bis `Fatal`)
* Message (`System.String`)
* Exception (`System.Exception`)
* CallerMemberName (`System.String`)
* CallerFilePath (`System.String`)
* CallerClassName (`System.String`)
* CallerLineNumber (`System.String`)

Beispiel für [NLog](https://nlog-project.org/):
```vbnet
Imports FBoxAPI

Friend Class FBoxAPILog
    Implements IFBoxAPIConnector

    Private Property NLogger As Logger = LogManager.GetCurrentClassLogger

    Public Sub LogMessage(MessageContainer As LogMessage) Implements IFBoxAPIConnector.LogMessage
        With MessageContainer
            Dim LogEvent As New LogEventInfo() With {.Level = NLog.LogLevel.FromOrdinal(MessageContainer.Level),
                                                     .LoggerName = MessageContainer.CallerClassName,
                                                     .Exception = MessageContainer.Ex,
                                                     .Message = MessageContainer.Message}

            LogEvent.SetCallerInfo(.CallerClassName, .CallerMemberName, .CallerFilePath, .CallerLineNumber)

            NLogger.Log(LogEvent)
        End With
    End Sub

    Public Sub Signal2FAuthentication(Methods As String) Implements IFBoxAPIConnector.Signal2FAuthentication
        ' ...
    End Sub
End Class
```

#### Zwei-Faktor-Authentifizierung
Die Nutzung der Zwei-Faktor-Authentifizierung kann ab Fritz!OS 7.39 nicht mehr deaktiviert werden. Das Setzen verschiedener Einstellungen bedarf nun einer zusätzlichen Bestätigung durch den Nutzer. 
Der Ablauf des Authentifizierungsprozesses ist in [X_AVM-DE_Auth](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf) beschrieben. 
Sobald für eine Action eine eine Zwei-Faktor-Authentifizierung erforderlich ist, signalisiert diese API dies über die `FBoxAPIConnector`-Schnittstelle, welche hierfür mit der Routine `Signal2FAuthentication` ergänzt wurde. 
Der Parameter `Methods` enhält die erlaubten Methoden, z. B. `button,dtmf;*14048`. Sobald der Nutzer die Authentifizierung durchgeführt hat, wird die ursprüngliche Action erneut ausgeführt. 
Die Ergebnisse des Authentifizierungsprozesses werden über die `LogMessage` ausgegeben. 
* Über die Eigenschaft `AbortAuthentication` kann der API signalisiert werden, dass der Authentifizierungsporzess abgebrochen werden soll.
* Über die Eigenschaft `AuthenticationSuccesful` signalisiert die API, dass der Authentifizierungsporzess abgeschlossen wurde.

```vbnet
Imports FBoxAPI

Friend Class FBoxAPILog
    Implements IFBoxAPIConnector

    Public Sub LogMessage(MessageContainer As LogMessage) Implements IFBoxAPIConnector.LogMessage
        ' ...
    End Sub

    Public Property AbortAuthentication As Boolean Implements IFBoxAPIConnector.AbortAuthentication

    Public Property AuthenticationSuccesful As Boolean Implements IFBoxAPIConnector.AuthenticationSuccesful

    Public Sub Signal2FAuthentication(Methods As String) Implements IFBoxAPIConnector.Signal2FAuthentication
        MsgBox(String.Format($"Zwei-Faktor-Authentifizierung: {Methods}"), MsgBoxStyle.Information, "Zwei-Faktor-Authentifizierung")
    End Sub
End Class
```

### Bekannte Probleme und Hinweise
* Die Dokumentation von AVM ist nicht immer korrekt. Z. B. wird in der Dokumentation [X_AVM-DE_AppSetup](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_appsetup.pdf)
  der Parameter `MyFritzDynDNSEnabled` der Action GetAppRemoteInfo aufgelistet. Dieser Parameter lautet aber `NewMyFritzEnabled`. 
  Es kann nicht ausgeschlossen werden, dann auch anderer Stelle ähnliche Probleme auftreten.
* Die Services und Actions wurden per Copy&Paste aus den vorliegenden Dokumentationen zusammengestellt. Bitte habt Verständnis, dass ich nicht alles testen kann.  
* Während einer laufenden Zwei-Faktor-Authentifizierung wird der aufrufende Thread blockiert. 

### Umsetzung
folgende angehakte TR-064 Services werden derzeit unterstützt. Falls etwas fehlen sollte, oder etwas nicht funktioniert, dann gebt bitte Bescheid.

* [x] [AURA](https://github.com/blacksenator/fritzsoap/blob/master/docs/auraSCPD.pdf) (Inoffizielle Dokumentation von Black Senator aus dem IPPF)
* [x] [DeviceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf)
* [x] [DeviceInfo](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf)
* [x] [Hosts](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf)
* [x] [LANConfigSecurity](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanconfigsecuritySCPD.pdf)
* [x] [LANEthernetInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanifconfigSCPD.pdf)
* [x] [LANHostConfigManagement](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanhostconfigmgmSCPD.pdf)
* [x] [Layer3Forwarding](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/layer3forwardingSCPD.pdf)
* [x] [ManagementService](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mgmsrvSCPD.pdf)
* [x] [Time](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/timeSCPD.pdf)
* [x] [UserInterface](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/userifSCPD.pdf)
* [x] [WANCommonInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf)
* [x] [WANEthernetLinkConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanethlinkconfigSCPD.pdf)
* [x] [WANDSLInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandslifconfigSCPD.pdf)
* [x] [WANDSLLinkConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandsllinkconfigSCPD.pdf)
* [x] [WANIPConnection](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanipconnSCPD.pdf)
* [x] [WANPPPConnection](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanpppconnSCPD.pdf)
* [x] [WLANConfiguration](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf)
* [x] [X_AVM-DE_AppSetup](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_appsetup.pdf) (Hinweis: Alle Argumente sind in der Dokumentation falsch angegeben. Der übliche Präfix `New` wurde nicht dargestellt.)
* [x] [X_AVM-DE_Auth](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf)
* [x] [X_AVM-DE_Dect](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf)
* [x] [X_AVM-DE_Filelinks](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf)
* [x] [X_AVM-DE_HostFilter](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_hostfilterSCPD.pdf)
* [x] [X_AVM-DE_Media](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_mediaSCPD.pdf)	
* [x] [X_AVM-DE_MyFritz](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf)	
* [x] [X_AVM-DE_OnTel](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf)	
* [x] [X_AVM-DE_RemoteAccess](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_remoteSCPD.pdf)
* [x] [X_AVM-DE_Speedtest](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf)	
* [x] [X_AVM-DE_Storage](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_storageSCPD.pdf)
* [x] [X_AVM-DE_TAM](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf)
* [x] [X_AVM-DE_UPnP](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_upnp.pdf)
* [x] [X_AVM-DE_USPController](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanmobileconnSCPD.pdf)
* [x] [X_AVM-DE_WANMobileConnection](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_uspcontrollerSCPD.pdf)
* [x] [X_AVM-DE_WebDAVClient](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_webdavSCPD.pdf)
* [x] [X_HomeAuto](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf)
* [x] [X_HomePlug](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf)
* [x] [X_VoIP](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf)	
      
Des Weiteren wird das [AVM Home Automation](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/AHA-HTTP-Interface.pdf)-Interface unterstützt. 

### Markenrecht
Dieses Software wird vom Autor privat in der Freizeit als Hobby gepflegt. Mit der Bereitstellung der Software werden keine gewerblichen Interessen verfolgt. Es wird aus rein ideellen Gründen zum Gemeinwohl aller Nutzer einer Fritz!Box betrieben. 
Die Erstellung dieser Software erfolgt nicht im Auftrag oder mit Wissen der Firmen AVM GmbH. Diese Software wurde unabhängig erstellt. Der Autor pflegt im Zusammenhang mit dieser Software keine Beziehungen zur Firma AVM GmbH.
