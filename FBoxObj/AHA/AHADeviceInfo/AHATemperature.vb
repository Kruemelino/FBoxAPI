Imports System.Xml.Serialization
<Serializable(), XmlType("temperature")> Public Class AHATemperature
    ''' <summary>
    ''' Wert in 0,1 °C, negative und positive Werte möglich
    ''' </summary>
    <XmlElement("celsius")> Public Property Celsius As String

    ''' <summary>
    ''' Wert in 0,1 °C, negative und positive Werte möglich
    ''' </summary>
    <XmlElement("offset")> Public Property Offset As String
End Class
