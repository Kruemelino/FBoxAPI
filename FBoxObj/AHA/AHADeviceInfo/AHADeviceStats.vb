Imports System.Xml.Serialization
<Serializable(), XmlRoot("devicestats")> Public Class AHADeviceStats

    ''' <summary>
    ''' Die Genauigkeit/Einheit der Werte ist 0,1°C.
    ''' </summary>
    <XmlElement("temperature")> Public Property Temperature As AHAStats

    ''' <summary>
    ''' Die Genauigkeit/Einheit der Werte ist 0,001V. 
    ''' </summary>
    <XmlElement("voltage")> Public Property Voltage As AHAStats

    ''' <summary>
    ''' Die Genauigkeit/Einheit der Werte ist 0,01W.
    ''' </summary>
    <XmlElement("power")> Public Property Power As AHAStats

    ''' <summary>
    ''' Die Genauigkeit/Einheit der Werte ist 1 Wh.
    ''' </summary>
    <XmlElement("energy")> Public Property Energy As AHAStats

    ''' <summary>
    ''' Die Genauigkeit/Einheit der Werte ist Prozent.
    ''' </summary>
    <XmlElement("humidity")> Public Property Humidity As AHAStats
End Class
