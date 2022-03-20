''' <summary>
''' AVM Home Automation HTTP Interface<br/>
''' Date: 2020-09-16<br/>
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/AHA-HTTP-Interface.pdf</see>
''' </summary>
Public Interface IFBoxAHA
#Region "Switch/Schalter"
    ''' <summary>
    ''' Liefert die kommaseparierte AIN/MAC Liste aller bekannten Steckdosen
    ''' </summary>
    ''' <returns>kommaseparierte AIN/MAC-Liste, leer wenn keine Steckdose bekannt</returns>
    Function GetSwitchList() As Task(Of String())

    ''' <summary>
    ''' Schaltet Steckdose ein
    ''' </summary>
    ''' <param name="AIN">Zu schaltende Steckdose</param>
    ''' <returns>"1"</returns>
    Function SetSwitchON(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Schaltet Steckdose aus
    ''' </summary>
    ''' <param name="AIN">Zu schaltende Steckdose</param>
    ''' <returns>"0"</returns>
    Function SetSwitchOFF(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Toggeln der Steckdose ein/aus
    ''' </summary>
    ''' <param name="AIN">Zu schaltende Steckdose</param>
    ''' <returns>"0" oder "1" (Steckdose aus oder an)</returns>
    Function SetSwitchToggle(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Ermittelt Schaltzustand der Steckdose
    ''' </summary>
    ''' <param name="AIN">Abzufragende Steckdose</param>
    ''' <returns>"0" oder "1" (Steckdose aus oder an), "inval" wenn unbekannt</returns>
    Function GetSwitchState(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Ermittelt Verbindungsstatus des Aktors
    ''' </summary>
    ''' <param name="AIN">Abzufragende Steckdose</param>
    ''' <returns>"0" oder "1" für Gerät nicht verbunden bzw. verbunden. Bei Verbindungsverlust wechselt der Zustand erst mit einigen Minuten Verzögerung zu "0".</returns>
    Function GetSwitchPresent(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Ermittelt aktuell über die Steckdose entnommene Leistung
    ''' </summary>
    ''' <param name="AIN">Abzufragende Steckdose</param>
    ''' <returns>Leistung in mW, "inval" wenn unbekannt</returns>
    Function GetSwitchPower(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Liefert die über die Steckdose entnommene Ernergiemenge seit Erstinbetriebnahme oder Zurücksetzen der Energiestatistik
    ''' </summary>
    ''' <param name="AIN">Abzufragende Steckdose</param>
    ''' <returns>Energie in Wh, "inval" wenn unbekannt</returns>
    Function GetSwitchEnergy(AIN As String) As Task(Of String)

    ''' <summary>
    ''' Liefert Bezeichner des Aktors
    ''' </summary>
    ''' <param name="AIN">Abzufragende Steckdose</param>
    ''' <returns>Name der Steckdose</returns>
    Function GetSwitchName(AIN As String) As Task(Of String)
#End Region
    ''' <summary>
    ''' Liefert die grundlegenden Informationen aller SmartHome-Geräte
    ''' </summary>
    ''' <returns>XML-Format mit grundlegenden und funktionsspezifischen Informationen</returns>
    Function GetDeviceListInfos() As Task(Of AHADeviceList)

    ''' <summary>
    ''' Liefert die grundlegenden Informationen dieses SmartHome-Geräte
    ''' </summary>
    ''' <param name="AIN">Abzufragendes Gerät</param>
    ''' <returns>XML-Format mit grundlegenden und funktionsspezifischen Informationen</returns>
    Function GetDeviceInfos(AIN As String) As Task(Of AHAGroup)

    Function GetBasicDeviceStats(AIN As String) As Task(Of AHADeviceStats)

#Region "Heizkörperregler"
    ''' <summary>
    ''' Letzte Temperaturinformation des Aktors
    ''' </summary>
    ''' <param name="AIN">Abzufragender Aktor</param>
    ''' <returns>Temperatur-Wert in 0,1 °C, negative und positive Werte möglich Bsp. "200" bedeutet 20°C</returns>
    Function GetTemperature(AIN As String) As Task(Of Double)

    ''' <summary>
    ''' Für HKR aktuell eingestellte Solltemperatur
    ''' </summary>
    ''' <param name="AIN">Abzufragender Heizkörperregler</param>
    ''' <returns>Temperatur-Wert in 0,5 °C, Wertebereich: 16 – 56 8 bis 28°C, 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C 254 = ON , 253 = OFF</returns>
    Function GetHKRTsoll(AIN As String) As Task(Of Double)

    ''' <summary>
    ''' Für HKR aktuell eingestellte Komforttemperatur
    ''' </summary>
    ''' <param name="AIN">Abzufragender Heizkörperregler</param>
    ''' <returns>Temperatur-Wert in 0,5 °C, Wertebereich: 16 – 56 8 bis 28°C, 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C 254 = ON , 253 = OFF</returns>
    Function GetHKRkomfort(AIN As String) As Task(Of Double)

    ''' <summary>
    ''' Für HKR aktuell eingestellte Spartemperatur
    ''' </summary>
    ''' <param name="AIN">Abzufragender Heizkörperregler</param>
    ''' <returns>Temperatur-Wert in 0,5 °C, Wertebereich: 16 – 56 8 bis 28°C, 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C 254 = ON , 253 = OFF</returns>

    Function GetHKRabsenk(AIN As String) As Task(Of Double)

    ''' <summary>
    ''' HKR Solltemperatur einstellen.
    ''' </summary>
    ''' <param name="AIN">Heizkörperregler</param>
    ''' <param name="HKRTsoll">Mit dem „param“ GetParameter wird die Solltemperatur übergeben. Temperatur-Wert in 0,5 °C<br/>
    ''' Wertebereich: 16 – 56 8 bis 28°C, 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C 254 = ON , 253 = OFF</param>
    Sub SetHKRTsoll(AIN As String, HKRTsoll As Double)

    ''' <summary>
    ''' HKR Boost aktivieren mit End-Zeit 
    ''' </summary>
    ''' <param name="AIN">Heizkörperregler</param>
    ''' <param name="EndTimeStamp">Zeit in Sekunden seit 1970, zum Deaktivieren: endtimestamp=0</param>
    ''' <returns>endtimestamp wenn erfolgreich</returns>
    ''' <remarks>Die End-Zeit darf maximal bis zu 24 Stunden in der Zukunft liegen.</remarks>
    Function SetHKRBoost(AIN As String, EndTimeStamp As Integer) As Task(Of Integer)

    ''' <summary>
    ''' HKR Fenster-offen Modus aktivieren mit End-Zeit 
    ''' </summary>
    ''' <param name="AIN">Heizkörperregler</param>
    ''' <param name="EndTimeStamp">Zeit in Sekunden seit 1970, zum Deaktivieren: endtimestamp=0</param>
    ''' <returns>endtimestamp wenn erfolgreich</returns>
    ''' <remarks>Die End-Zeit darf maximal bis zu 24 Stunden in der Zukunft liegen.</remarks>
    Function SetHKRWindowOpen(AIN As String, EndTimeStamp As Integer) As Task(Of Integer)
#End Region

#Region "Vorlagen"
    ''' <summary>
    ''' Liefert die grundlegenden Informationen aller Vorlagen/Templates
    ''' </summary>
    Function GetTemplateListInfos() As Task(Of AHATemplateList)

    ''' <summary>
    ''' Vorlage anwenden, der ain Parameter wird ausgewertet
    ''' </summary>
    ''' <param name="AIN">Anzuwendende Vorlage</param>
    Sub ApplyTemplate(AIN As String)
#End Region

#Region "Diverse Aktionen"
    ''' <summary>
    ''' Gerät/Aktor/Lampe an-/ausschalten oder toggeln
    ''' </summary>
    ''' <param name="AIN">Zu schaltender Aktor</param>
    ''' <param name="OnOff">0(aus), 1(an) oder 2(toggle)</param>
    Sub SetSimpleOnOff(AIN As String, OnOff As Boolean)

    ''' <summary>
    ''' Dimm-, Höhen-, Helligkeit bzw. Niveau-Level einstellen
    ''' </summary>
    ''' <param name="AIN">Zu schaltender Aktor</param>
    ''' <param name="Level">0(0%) bis 255(100%)</param>
    Sub SetLevel(AIN As String, Level As Integer)

    ''' <summary>
    ''' Dimm-, Höhen-, Helligkeit bzw. Niveau-Level in Prozent einstellen
    ''' </summary>
    ''' <param name="AIN">Zu schaltender Aktor</param>
    ''' <param name="Level">0(0%) bis 100(100%)</param>
    Sub SetLevelPercentage(AIN As String, Level As Integer)

    ''' <summary>
    ''' HueSaturation-Farbe einstellen 
    ''' Der HSV-Farbraum wird mit dem HueSaturation-Mode unterstützt. 
    ''' Der Hellwert(Value) kann über <see cref="SetLevel(String, Integer)"/>/<see cref="SetLevelPercentage(String, Integer)"/> konfiguriert werden.
    ''' Die Hue und Saturation-Werte sind hier konfigurierbar.
    ''' </summary>
    ''' <param name="AIN">Zu schaltender Aktor</param>
    ''' <param name="HUE">in Grad, 0 bis 359 (0° bis 359°)</param>
    ''' <param name="Saturation">0(0%) bis 255(100%)</param>
    ''' <param name="Duration">Schnelligkeit der Änderung in 100ms. 0 sofort</param>
    Sub SetColor(AIN As String, Hue As Integer, Saturation As Integer, Duration As Integer)

    ''' <summary>
    ''' Farbtemperatur einstellen
    ''' </summary>
    ''' <param name="AIN">Zu schaltender Aktor</param>
    ''' <param name="Temperature">in Kelvin, typisch im Bereich 2700 bis 6500</param>
    ''' <param name="Duration">Schnelligkeit der Änderung in 100ms. 0 sofort</param>
    Sub SetColorTemperature(AIN As String, Temperature As Integer, Duration As Integer)

    ''' <summary>
    ''' Rollo schliessen(close), öffnen(open) oder stoppen(stop)
    ''' </summary>
    ''' <param name="AIN">Zu schaltendes Rollow</param>
    ''' <param name="Target">"open", "close" oder "stop"</param>
    ''' <remarks>Rollos haben den HANFUNUnittyp Blind(281)</remarks>
    Sub Setblind(AIN As String, Target As String)

    ''' <summary>
    ''' Gerät- oder Gruppenname ändern 
    ''' </summary>
    ''' <param name="AIN">Zu änderndes Gerät</param>
    ''' <param name="Name">in UTF8, maximal 40 Zeichen</param>
    ''' <remarks>Achtung: die Benutzer-Session muss das Smarthome- UND App-Recht haben<br/>
    ''' Hinweis: benötigt die "Eingeschränkte FRITZ!Box Einstellungen für Apps"-Berechtigung</remarks>
    Sub SetName(AIN As String, Name As String)

    ''' <summary>
    ''' DECT-ULE-Geräteanmeldung starten
    ''' </summary>
    ''' <remarks>Achtung: die Benutzer-Session muss das Smarthome- UND App-Recht haben<br/>
    ''' Hinweis: benötigt die "Eingeschränkte FRITZ!Box Einstellungen für Apps"-Berechtigung</remarks>
    Sub StartULESubscription()

    ''' <summary>
    ''' DECT-ULEGeräteanmeldestatus abfragen
    ''' </summary>
    Function GetSubscriptionState() As Task(Of AHASubscriptionState)

#End Region

    Function GetColorDefaults() As Task(Of AHAColorDefaults)

End Interface
