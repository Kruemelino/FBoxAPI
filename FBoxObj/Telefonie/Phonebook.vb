Imports System.Xml.Serialization

<Serializable(), XmlType("phonebook")> Public Class Phonebook

    <XmlAttribute("owner")> Public Property Owner As String
    <XmlAttribute("name")> Public Property Name As String
    <XmlElement("timestamp")> Public Property TimeStamp As String
    <XmlElement("contact")> Public Property Contacts As List(Of Contact)
End Class
