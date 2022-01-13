Imports System.Xml.Serialization
<Serializable()> Public Class User
    <XmlAttribute("last_user")> Public Property LastUser As Integer
    <XmlText> Public Property UserName As String
End Class
