Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("List")> Public Class FileLinkList
    <XmlElement("Item")> Public Property Messages As List(Of FileLinkEntry)
End Class
