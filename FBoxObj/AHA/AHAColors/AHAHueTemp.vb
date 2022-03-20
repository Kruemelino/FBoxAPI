Imports System.Xml.Serialization
<Serializable(), XmlType("temp")> Public Class AHAHueTemp
    ''' <summary>
    ''' Der value-Attribut Wert ist die Farbtemperatur In Kelvin. 
    ''' Ein typischer Wertebereich geht von etwa 2700K bis 6500K.
    ''' </summary>
    <XmlAttribute("value")> Public Property Value As Integer
End Class
