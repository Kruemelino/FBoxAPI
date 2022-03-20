Imports System.Xml.Serialization
<Serializable(), XmlType("hkr")> Public Class AHAHKR

    ''' <summary>
    ''' Isttemperatur in 0,5 °C, Wertebereich: 0x0 – 0x64 
    ''' </summary>
    ''' <remarks>0 &lt;= 0°C, 1 = 0,5°C...... 120 = 60°C, 254 = ON , 253 = OFF</remarks>
    <XmlElement("tist")> Public Property Tist As String

    ''' <summary>
    ''' Solltemperatur in 0,5 °C, Wertebereich: 0x10 – 0x38
    ''' </summary>
    ''' <remarks>16 – 56 (8 bis 28°C), 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C, 254 = ON , 253 = OFF</remarks>
    <XmlElement("tsoll")> Public Property Tsoll As String

    ''' <summary>
    ''' Komforttemperatur in 0,5 °C, Wertebereich: 0x10 – 0x38
    ''' </summary>
    ''' <remarks>16 – 56 (8 bis 28°C), 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C, 254 = ON , 253 = OFF</remarks>
    <XmlElement("komfort")> Public Property Komfort As String

    ''' <summary>
    ''' Absenktemperatur in 0,5 °C, Wertebereich: 0x10 – 0x38
    ''' </summary>
    ''' <remarks>16 – 56 (8 bis 28°C), 16 &lt;= 8°C, 17 = 8,5°C...... 56 &gt;= 28°C, 254 = ON , 253 = OFF</remarks>
    <XmlElement("absenk")> Public Property Absenk As String

    ''' <summary>
    ''' 0 oder 1: Batterieladezustand niedrig - bitte Batterie wechseln
    ''' </summary>
    ''' <remarks>optional, wenn vom Gerät unterstützt</remarks>
    <XmlElement("batterylow")> Public Property BatteryLow As String

    ''' <summary>
    ''' Batterieladezustand in Prozent
    ''' </summary>
    ''' <remarks>optional, wenn vom Gerät unterstützt</remarks>
    <XmlElement("battery")> Public Property Battery As String

    ''' <summary>
    ''' Fenster-offen Modus aktiviert: 0 oder 1
    ''' </summary>
    <XmlElement("windowopenactiv")> Public Property WindowOpenActiv As String

    ''' <summary>
    ''' Fenster-offen End-Zeit, in Sekunden seit 1970
    ''' </summary>
    <XmlElement("windowopenactiveendtime")> Public Property WindowOpenActiveEndTime As String

    ''' <summary>
    ''' Boost Modus aktiviert: 0 oder 1
    ''' </summary>
    <XmlElement("boostactive")> Public Property BoostActive As String

    ''' <summary>
    ''' Boost End-Zeit, in Sekunden seit 1970
    ''' </summary>
    <XmlElement("boostactiveendtime")> Public Property BoostActiveEndTime As String

    ''' <summary>
    ''' befindet sich der HKR aktuell in einem Urlaubszeitraum, 0 oder 1
    ''' </summary>
    <XmlElement("holidayactive")> Public Property HolidayActive As String

    ''' <summary>
    ''' befindet sich der HKR aktuell im „Heizung aus“ Zeitraum, 0 oder 1
    ''' </summary>
    <XmlElement("summeractive")> Public Property SummerActive As String

    ''' <summary>
    ''' 0/1 - Schaltsperre über UI/API ein nein/ja(leer bei unbekannt oder Fehler)
    ''' </summary>
    ''' <remarks>Achtung die Tastensperre wird automatisch bei <see cref="SummerActive"/>==1 oder <see cref="HolidayActive"/>==1 aktiviert</remarks>
    <XmlElement("lock")> Public Property Lock As String

    ''' <summary>
    ''' 0/1 - Schaltsperre direkt am Gerät ein nein/ja(leer bei unbekannt oder Fehler)
    ''' </summary>
    <XmlElement("devicelock")> Public Property DeviceLock As String

    ''' <summary>
    ''' nächste Temperaturänderung
    ''' </summary>
    <XmlElement("nextchange")> Public Property NextChange As AHAHKRNextChange

    ''' <summary>
    ''' Fehlercodes die der HKR liefert (bspw. wenn es bei der Installation des HKRs Problem gab)
    ''' <list type="table">
    ''' <item><term>0</term><description>kein Fehler</description></item>
    ''' <item><term>1</term><description>Keine Adaptierung möglich. Gerät korrekt am Heizkörper montiert?</description></item>
    ''' <item><term>2</term><description>Ventilhub zu kurz oder Batterieleistung zu schwach. Ventilstößel per Hand mehrmals öffnen und schließen oder neue Batterien einsetzen.</description></item>
    ''' <item><term>3</term><description>Keine Ventilbewegung möglich. Ventilstößel frei?</description></item>
    ''' <item><term>4</term><description>Die Installation wird gerade vorbereitet.</description></item>
    ''' <item><term>5</term><description>Der Heizkörperregler ist im Installationsmodus und kann auf das Heizungsventil montiert werden.</description></item>
    ''' <item><term>6</term><description>Der Heizkörperregler passt sich nun an den Hub des Heizungsventils an.</description></item>
    ''' </list>
    ''' </summary>
    <XmlElement("errorcode")> Public Property ErrorCode As String
End Class
