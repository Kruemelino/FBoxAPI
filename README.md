# FBoxAPI

Dieses Projekt ist ein .NET Bibliothek für die TR-064 Schnittstelle der AVM Fritz!Box. 
Das Projekt ist eine Ausgliederung aus meinem Addin für Microsoft Outlook: [Fritz!Box Telefon-dingsbums V5](https://github.com/Kruemelino/FritzBoxTelefon-dingsbums) 

Dieses Addin ist in meiner Freizeit entstanden. Ich erwarte keine Gegenleistung. Ein Danke ist ausreichend. Wer mir dennoch etwas Gutes zukommen lassen möchte kann dies gerne tun:

[![Donate](https://img.shields.io/badge/Spenden-PayPal-green.svg)](https://www.paypal.com/paypalme/gertmichael)

### Grundlagen
Die Schnittstelle basiert auf der [AVM-Dokumentation](https://avm.de/service/schnittstellen). 

### Nutzung
Die Verwendung ist recht einfach angedacht. Es muss eine neue `FBoxAPI.FritzBoxTR64`-Klasse instanziiert werden. Hierfür sind zwei Parameter erforderlich: IP-Adresse der Fritz!Box und die 
Anmeldeinformationen. Nutzername und Passwort werden in einer neuen Instanz der `System.Net.NetworkCredential`-Klasse hinterlegt und übergeben.
Im folgenen ist ein kleines Beispiel aufgeführt, wie die SessionID der Fritz!Box abgefragt werden kann. 

```vbnet
Private Function GetSessionID() As String
    ' Bereitstellung der Variable(n), in die das Ergebnis gesetzt werden sollen.
    Dim SessionID As String = "0000000000000000"

    ' Erstelle Anmeldeinformationen für die Fritz!Box bereit
    Dim Nutzername As String = "Fritz"
    Dim Passwort As String = "Box"

    ' ANmeldeinformationen können Nothing sein, falls nur Actions ausgeführt werden, die keine Anmeldung erfordern.
    Dim Anmeldeinformationen As New Net.NetworkCredential(Nutzername, Passwort)

    ' Starte die TR-064 Schnittstelle zur Fritz!Box
    Using FBoxTR064 As New FBoxAPI.FritzBoxTR64("192.168.178.1", Anmeldeinformationen)

        ' Auwahl des Service
        With FBoxTR064.Deviceconfig

            ' Action ausführen
            If .GetSessionID(SessionID) Then
                ' Alles OK
            Else
                ' Ein Fehler ist aufgetreten
            End If
        End With

    End Using

    Return SessionID

End Function
```

Sobald eine neue `FBoxAPI.FritzBoxTR64`-Klasse erstellt wurde, kann auch auf das Event `Status` abgefragt werden. 
Die FBoxAPI.LogMessage beinhaltet diverse relevante Eigenschaften:

* Level (`System.Enum`) für das LogLevel (`Trace` bis `Fatal`)
* Message (`System.String`)
* Exception (`System.Exception`)
* CallerMemberName (`System.String`)
* CallerFilePath (`System.String`)
* CallerClassName (`System.String`)
* CallerLineNumber (`System.String`)

Beispiel für [NLog](https://nlog-project.org/):
```vbnet
    Friend Sub FBoxAPIMessage(sender As Object, e As FBoxAPI.NotifyEventArgs(Of FBoxAPI.LogMessage))

        With e.Value
            Dim LogEvent As New LogEventInfo(LogLevel.FromOrdinal(.Level),
                                             .CallerClassName,
                                             .Message)

            LogEvent.SetCallerInfo(.CallerClassName, .CallerMemberName, .CallerFilePath, .CallerLineNumber)
            
            NLogger.Log(LogEvent)
        End With

    End Sub
```
### Umsetzung

folgende angehakte Services werden derzeit unterstützt. Die Fehlenden werden nach und nach ergänzt.

* [x] [DeviceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceconfigSCPD.pdf)
* [x] [DeviceInfo](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf)
* [x] [Hosts](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf)
* [x] [LANConfigSecurity](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanconfigsecuritySCPD.pdf)
* [ ] [LANEthernetInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanifconfigSCPD.pdf)
* [ ] [LANHostConfigManagement](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanhostconfigmgmSCPD.pdf)
* [ ] [Layer3Forwarding](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/layer3forwardingSCPD.pdf)
* [ ] [ManagementService](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mgmsrvSCPD.pdf)
* [ ] [Time](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/timeSCPD.pdf)
* [x] [UserInterface](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/userifSCPD.pdf)
* [x] [WANCommonInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wancommonifconfigSCPD.pdf)
* [x] [WANEthernetLinkConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanethlinkconfigSCPD.pdf)
* [x] [WANDSLInterfaceConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandslifconfigSCPD.pdf)
* [x] [WANDSLLinkConfig](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandsllinkconfigSCPD.pdf)
* [x] [WANIPConnection](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanipconnSCPD.pdf)
* [x] [WANPPPConnection](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanpppconnSCPD.pdf)
* [x] [WLANConfiguration](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf)
* [ ] [X_AVM-DE_AppSetup](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_appsetup.pdf)
* [ ] [X_AVM-DE_Auth](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf)
* [x] [X_AVM-DE_Dect](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf)
* [x] [X_AVM-DE_Filelinks](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf)
* [x] [X_AVM-DE_HostFilter](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_hostfilterSCPD.pdf)
* [x] [X_AVM-DE_MyFritz](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf)	
* [x] [X_AVM-DE_OnTel](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf)	
* [ ] [RemoteAccess](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_remoteSCPD.pdf)
* [x] [X_AVM-DE_Speedtest](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf)	
* [ ] [X_AVM-DE_Storage](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_storageSCPD.pdf)
* [x] [X_AVM-DE_TAM](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf)
* [ ] [X_AVM-DE_UPnP](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_upnp.pdf)
* [x] [X_HomeAuto](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf)
* [x] [X_HomePlug](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf)
* [x] [X_VoIP](https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf)			

### Markenrecht
Dieses Software wird vom Autor privat in der Freizeit als Hobby gepflegt. Mit der Bereitstellung der Software werden keine gewerblichen Interessen verfolgt. Es wird aus rein ideellen Gründen zum Gemeinwohl aller Nutzer einer Fritz!Box betrieben. 
Die Erstellung dieser Software erfolgt nicht im Auftrag oder mit Wissen der Firmen AVM GmbH. Diese Software wurde unabhängig erstellt. Der Autor pflegt im Zusammenhang mit dieser Software keine Beziehungen zur Firma AVM GmbH.