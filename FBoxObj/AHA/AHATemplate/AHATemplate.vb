Imports System.Xml.Serialization
<Serializable(), XmlType("template")> Public Class AHATemplate
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

    <XmlAttribute("applymask")> Public Property Applymask As String
#End Region

#Region "Unterknoten von <template>"
    ''' <summary>
    ''' Template/Vorlagen Name
    ''' </summary>
    <XmlElement("name")> Public Property Name As String

    ''' <summary>
    ''' List der zugehörigen Geräte
    ''' </summary>
    <XmlArray("devices"), XmlArrayItem("device")> Public Property Devices As List(Of AHATemplateDevice)

    <XmlElement("applymask")> Public Property Applymasks As AHATemplateApplyMask
#End Region
End Class
