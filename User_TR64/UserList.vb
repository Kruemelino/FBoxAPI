Imports System.Xml.Serialization
<XmlRoot("List"), XmlType("List")> Public Class UserList
    <XmlElement("Username")> Public Property UserListe As List(Of User)

    <XmlIgnore> Public ReadOnly Property GetLastUsedUser As User
        Get
            Return UserListe.Find(Function(User) User.LastUser.IsNotZero)
        End Get
    End Property
End Class
