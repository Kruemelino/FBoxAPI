Imports System.Xml.Serialization
<Serializable(), XmlType("hs")> Public Class AHAHueSaturation
    <XmlAttribute("hue_index")> Public Property HueIndex As Integer
    <XmlElement("name")> Public Property Name As AHAHueName
    <XmlElement("color")> Public Property Color As List(Of AHAHueColor)
End Class
