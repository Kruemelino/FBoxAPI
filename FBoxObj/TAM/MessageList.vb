Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("Root"), XmlType("Root")> Public Class MessageList
    <XmlElement("Message")> Public Property Messages As List(Of Message)

    Public Sub New()
        Messages = New List(Of Message)
    End Sub
End Class
