Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("List")> Public Class FileLinkList
    <XmlElement("Item")> Public Property Messages As List(Of FileLinkEntry)

    Public Sub New()
        Messages = New List(Of FileLinkEntry)
    End Sub
End Class
