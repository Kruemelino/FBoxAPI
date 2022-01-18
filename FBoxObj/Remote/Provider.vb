Imports System.Xml.Serialization
<Serializable()> Public Class Provider
    <XmlElement("ProviderName")> Public Property ProviderName As String
    <XmlElement("InfoURL")> Public Property InfoURL As String
End Class
