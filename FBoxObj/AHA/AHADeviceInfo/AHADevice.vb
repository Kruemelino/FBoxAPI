Imports System.Xml.Serialization
''' <summary>
''' Im Aufbau sind bei <see cref="AHADevice"/> und <see cref="AHAGroup"/> identisch, nur das <see cref="AHAGroup"/> noch den <see cref="AHAGroupInfo"/> Knoten enthält.
''' </summary>
<Serializable(), XmlType("device")> Public Class AHADevice
#Region "Attribute von <device/group>"
    ''' <summary>
    ''' eindeutige ID, AIN, MAC-Adresse
    ''' </summary>
    <XmlAttribute("identifier")> Public Property Identifier As String

    ''' <summary>
    ''' interne Geräteid
    ''' </summary>
    <XmlAttribute("id")> Public Property ID As String

    ''' <summary>
    ''' Firmwareversion des Gerätes
    ''' </summary>
    <XmlAttribute("fwversion")> Public Property FWVersion As String

    ''' <summary>
    ''' Herstellerangabe z. B. "AVM"
    ''' </summary>
    <XmlAttribute("manufacturer")> Public Property Manufacturer As String

    ''' <summary>
    ''' Produktname des Gerätes, leer bei unbekanntem/undefiniertem Gerät
    ''' </summary>
    <XmlAttribute("productname")> Public Property ProductName As String

    ''' <summary>
    ''' Bitmaske der Geräte-Funktionsklassen, beginnen mit Bit 0, es können mehrere Bits gesetzt sein
    ''' <list type="table">
    ''' <listheader>
    ''' <term>Bit</term>
    ''' <description>Beschreibung</description>
    ''' </listheader>
    ''' <item><term>0</term><description>HAN-FUN Gerät</description></item>
    ''' <item><term>2</term><description>Licht/Lampe</description></item>
    ''' <item><term>4</term><description>Alarm-Sensor</description></item>
    ''' <item><term>5</term><description>AVM-Button</description></item>
    ''' <item><term>6</term><description>Heizkörperregler</description></item>
    ''' <item><term>7</term><description>Energie Messgerät</description></item>
    ''' <item><term>8</term><description>Temperatursensor</description></item>
    ''' <item><term>9</term><description>Schaltsteckdose</description></item>
    ''' <item><term>10</term><description>AVM DECT Repeater</description></item>
    ''' <item><term>11</term><description>Mikrofon</description></item>
    ''' <item><term>13</term><description>HAN-FUN-Unit</description></item>
    ''' <item><term>15</term><description>an-/ausschaltbares Gerät/Steckdose/Lampe/Aktor</description></item>
    ''' <item><term>16</term><description>Gerät mit einstellbarem Dimm-, Höhen- bzw. Niveau-Level</description></item>
    ''' <item><term>17</term><description>Lampe mit einstellbarer Farbe/Farbtemperatur</description></item>
    ''' <item><term>18</term><description>Rollladen(Blind) - hoch, runter, stop und level 0% bis 100 %</description></item>
    ''' </list>
    ''' </summary>
    ''' <remarks>Beispiel FD300: binär 101000000(320), Bit6(HKR) und Bit8(Temperatursensor) sind gesetzt</remarks>
    <XmlAttribute("functionbitmask")> Public Property FunctionBitMask As String
#End Region

#Region "Unterknoten von <device>/<group>"
    ''' <summary>
    ''' Gerät verbunden nein/ja
    ''' </summary>
    <XmlElement("present")> Public Property Present As String

    ''' <summary>
    ''' Das Senden eines Kommandos(wie Schaltbefehl oder Helligkeit ändern) läuft – ja(1) bzw. nein(0)
    ''' </summary>
    <XmlElement("txbusy")> Public Property Txbusy As String

    ''' <summary>
    ''' Gerätename
    ''' </summary>
    <XmlElement("name")> Public Property Name As String
#End Region

#Region "optionale Unterknoten von <device>/<group> - wenn vom Gerät unterstützt"
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
#End Region

#Region "Gerätespezifische Daten"
    ''' <summary>
    ''' Schaltsteckdose
    ''' </summary>
    <XmlElement("switch")> Public Property Switch As AHASwitch

    ''' <summary>
    ''' Energiemessgerät
    ''' </summary>
    <XmlElement("powermeter")> Public Property PowerMeter As AHAPowerMeter

    ''' <summary>
    ''' Temperatursensor
    ''' </summary>
    <XmlElement("temperature")> Public Property Temperature As AHATemperature

    ''' <summary>
    ''' Alarmsensor
    ''' </summary>
    <XmlElement("alert")> Public Property Alert As AHAAlert

    ''' <summary>
    ''' Taster
    ''' </summary>
    <XmlElement("button")> Public Property Button As AHAButton

    ''' <summary>
    ''' Taster
    ''' </summary>
    <XmlElement("avmbutton")> Public Property AVMButton As AHAAVMButton

    ''' <summary>
    ''' Das HAN-FUN-Gerät taucht in der getdevicelistinfo Auflistung als HAN-FUN(ETSI)-Gerät und zusätzlich mit 
    ''' einem <see cref="AHADevice"/> je HAN-FUN-Unit auf. 
    ''' Die HAN-FUN Gerät ↔ Units Zuordnung erfolgt über die <see cref="AHAHANFUN"/> der HANFUN-Unit. 
    ''' Der Identifier einer HANFUN-Unit endet typischerweise auf "-" und &lt;Nummer&gt;.
    ''' </summary>
    ''' <remarks>Der FRITZ!DECT 500 ist ein HAN-FUN-Gerät mit einer Unit vom Typ "dimmbare Farb-Lampe". Diese Unit kann die Farbe/Farbtemperatur ändern, die Helligkeit einstellen und an-/ausschalten.</remarks>
    <XmlElement("etsiunitinfo")> Public Property HANFUNUnit As AHAHANFUN

    ''' <summary>
    ''' an-/ausschaltbares Gerät/Steckdose/Lampe/Aktor
    ''' </summary>
    <XmlElement("simpleonoff")> Public Property SimpleOnOff As AHASimpleOnOff

    ''' <summary>
    ''' Gerät mit einstellbarem Dimm-, Höhen-, Helligkeit- bzw. Niveau-Level
    ''' </summary>
    <XmlElement("levelcontrol")> Public Property LevelControl As AHALevelControl

    ''' <summary>
    ''' Lampe mit einstellbarer Farbe/Farbtemperatur
    ''' </summary>
    <XmlElement("colorcontrol")> Public Property ColorControl As AHAColorControl

    ''' <summary>
    ''' Heizkörperregler
    ''' </summary>
    <XmlElement("hkr")> Public Property HKR As AHAHKR

#End Region
End Class
