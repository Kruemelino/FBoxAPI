Imports System.Xml.Serialization

<Serializable(), XmlType("telephony")> Public Class Telephony
    <XmlArray("services"), XmlArrayItem("email")> Public Property Emails As List(Of Email)
    <XmlElement("number")> Public Property Numbers As List(Of NumberType)
    <XmlAttribute("nid")> Public Property ID As Integer

    Public Sub New()
        Emails = New List(Of Email)
        Numbers = New List(Of NumberType)
    End Sub
End Class
