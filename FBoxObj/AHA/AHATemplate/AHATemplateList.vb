Imports System.Xml.Serialization
<Serializable(), XmlRoot("templatelist")> Public Class AHATemplateList
    <XmlAttribute("version")> Public Property Version As String
    <XmlElement("template")> Public Property Templates As List(Of AHATemplate)
End Class
