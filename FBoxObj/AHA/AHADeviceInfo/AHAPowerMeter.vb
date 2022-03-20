Imports System.Xml.Serialization
<Serializable(), XmlType("powermeter")> Public Class AHAPowerMeter
    ''' <summary>
    ''' Wert in 0,001 W (aktuelle Leistung, wird etwa alle 2 Minuten aktualisiert)
    ''' </summary>
    <XmlElement("power")> Public Property Power As String

    ''' <summary>
    ''' Wert in 1.0 Wh (absoluter Verbrauch seit Inbetriebnahme)
    ''' </summary>
    <XmlElement("energy")> Public Property Energy As String

    ''' <summary>
    ''' Wert in 0,001 V (aktuelle Spannung, wird etwa alle 2 Minuten aktualisiert)
    ''' </summary>
    <XmlElement("voltage")> Public Property Voltage As String

End Class
