# FBoxAPI

Dieses Projekt ist ein .NET Bibliothek für die Schnittstellen TR-064 und AHA der AVM Fritz!Box. 
Das Projekt ist eine Ausgliederung aus meinem Addin für Microsoft Outlook: [Fritz!Box Telefon-dingsbums V5](https://github.com/Kruemelino/FritzBoxTelefon-dingsbums) 

Dieses Addin ist in meiner Freizeit entstanden. Ich erwarte keine Gegenleistung. Ein Danke ist ausreichend. Wer mir dennoch etwas Gutes zukommen lassen möchte kann dies gerne tun:

[![Donate](https://img.shields.io/badge/Spenden-green.svg?logo=paypal)](https://www.paypal.com/paypalme/gertmichael) [![Nuget](https://img.shields.io/nuget/v/FBoxAPI)](https://www.nuget.org/packages/FBoxAPI)

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
* Über die Eigenschaft `AbortAuthentication` kann der API signalisiert werden, dass der Authentifizierungsprozess abgebrochen werden soll.
* Über die Eigenschaft `AuthenticationSuccesful` signalisiert die API, dass der Authentifizierungsprozess abgeschlossen wurde.

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
* [x] [DeviceConfig](https://fritz.support/resources/TR-064_Device_Config.pdf)
* [x] [DeviceInfo](https://fritz.support/resources/TR-064_Device_Info.pdf)
* [x] [Hosts](https://fritz.support/resources/TR-064_Hosts.pdf)
* [x] [IDGcfg](https://fritz.support/resources/IGD_v1.pdf)
* [x] [IDGdsl](https://fritz.support/resources/IGD_v1.pdf)
* [x] [IDGconn](https://fritz.support/resources/IGD_v1.pdf)
* [x] [IDG2cfg](https://fritz.support/resources/IGD_v2.pdf)
* [x] [IDG2dsl](https://fritz.support/resources/IGD_v2.pdf)
* [x] [IDG2conn](https://fritz.support/resources/IGD_v2.pdf)
* [x] [IDG2ipv6fwc](https://fritz.support/resources/IGD_v2.pdf)
* [x] [LANConfigSecurity](https://fritz.support/resources/TR-064_LAN_Config_Security.pdf)
* [x] [LANEthernetInterfaceConfig](https://fritz.support/resources/TR-064_LAN_Ethernet_Interface_Config.pdf)
* [x] [LANHostConfigManagement](https://fritz.support/resources/TR-064_LAN_Host_Config_Management.pdf)
* [x] [Layer3Forwarding](https://fritz.support/resources/TR-064_Layer_3_Forwarding.pdf)
* [x] [ManagementService](https://fritz.support/resources/TR-064_Management_Server.pdf)
* [x] [Time](https://fritz.support/resources/TR-064_Time.pdf)
* [x] [UserInterface](https://fritz.support/resources/TR-064_User_Interface.pdf)
* [x] [WANCommonInterfaceConfig](https://fritz.support/resources/TR-064_WAN_Common_Interface_Config.pdf)
* [x] [WANEthernetLinkConfig](https://fritz.support/resources/TR-064_WAN_Ethernet_Link_Config.pdf)
* [x] [WANDSLInterfaceConfig](https://fritz.support/resources/TR-064_WAN_DSL_Interface_Config.pdf)
* [x] [WANDSLLinkConfig](https://fritz.support/resources/TR-064_WAN_DSL_Link_Config.pdf)
* [x] [WANIPConnection](https://fritz.support/resources/TR-064_WAN_IP_Connection.pdf)
* [x] [WANPPPConnection](https://fritz.support/resources/TR-064_WAN_PPP_Connection.pdf)
* [x] [WLANConfiguration](https://fritz.support/resources/TR-064_WLAN_Configuration.pdf) (#1, #2, #3, #4)
* [x] [X_AVM-DE_AppSetup](https://fritz.support/resources/TR-064_App_Setup.pdf) (Hinweis: Alle Argumente sind in der Dokumentation falsch angegeben. Der übliche Präfix `New` wurde nicht dargestellt.)
* [x] [X_AVM-DE_Auth](https://fritz.support/resources/TR-064_Authentication.pdf)
* [x] [X_AVM-DE_Dect](https://fritz.support/resources/TR-064_DECT.pdf)
* [x] [X_AVM-DE_Filelinks](https://fritz.support/resources/TR-064_Filelinks.pdf)
* [x] [X_AVM-DE_HostFilter](https://fritz.support/resources/TR-064_Host_Filter.pdf)
* [x] [X_AVM-DE_Media](https://fritz.support/resources/TR-064_Media.pdf)	
* [x] [X_AVM-DE_MyFritz](https://fritz.support/resources/TR-064_MyFRITZ.pdf)	
* [x] [X_AVM-DE_OnTel](https://fritz.support/resources/TR-064_Contact_SCPD.pdf)	
* [x] [X_AVM-DE_RemoteAccess](https://fritz.support/resources/TR-064_Remote_Access.pdf)
* [x] [X_AVM-DE_Speedtest](https://fritz.support/resources/TR-064_Speedtest.pdf)	
* [x] [X_AVM-DE_Storage](https://fritz.support/resources/TR-064_Storage.pdf)
* [x] [X_AVM-DE_TAM](https://fritz.support/resources/TR-064_TAM.pdf)
* [x] [X_AVM-DE_UPnP](https://fritz.support/resources/TR-064_UPnP.pdf)
* [x] [X_AVM-DE_USPController](https://fritz.support/resources/TR-064_USP_Controller.pdf)
* [x] [X_AVM-DE_WANFiber](https://fritz.support/resources/TR-064_WAN_Fiber.pdf)
* [x] [X_AVM-DE_WANMobileConnection](https://fritz.support/resources/TR-064_WAN_Mobile_Connection.pdf)
* [x] [X_AVM-DE_WebDAVClient](https://fritz.support/resources/TR-064_WebDAV.pdf)
* [x] [X_HomeAuto](https://fritz.support/resources/TR-064_HomeAuto.pdf)
* [x] [X_HomePlug](https://fritz.support/resources/TR-064_HomePlug.pdf)
* [x] [X_VoIP](https://fritz.support/resources/TR-064_VoIP.pdf)		
      
Derzeit noch in Bearbeitung: Des Weiteren wird das [AVM Home Automation](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/AHA-HTTP-Interface.pdf)-Interface unterstützt.

### Markenrecht
Dieses Software wird vom Autor privat in der Freizeit als Hobby gepflegt. Mit der Bereitstellung der Software werden keine gewerblichen Interessen verfolgt. Es wird aus rein ideellen Gründen zum Gemeinwohl aller Nutzer einer Fritz!Box betrieben. 
Die Erstellung dieser Software erfolgt nicht im Auftrag oder mit Wissen der Firmen AVM GmbH. Diese Software wurde unabhängig erstellt. Der Autor pflegt im Zusammenhang mit dieser Software keine Beziehungen zur Firma AVM GmbH.
