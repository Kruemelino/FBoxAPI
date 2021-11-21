Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("Root"), XmlType("Root")> Public Class MessageList
    <XmlElement("Message")> Public Property Messages As List(Of Message)
End Class
