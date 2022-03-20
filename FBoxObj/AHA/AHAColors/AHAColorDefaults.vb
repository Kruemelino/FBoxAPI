Imports System.Xml.Serialization
<Serializable(), XmlRoot("colordefaults")> Public Class AHAColorDefaults
    <XmlArray("hsdefaults"), XmlArrayItem("hs")> Public Property HueSaturations As List(Of AHAHueSaturation)
    <XmlArray("temperaturedefaults"), XmlArrayItem("temp")> Public Property Temperaturedefaults As List(Of AHAHueTemp)

End Class
