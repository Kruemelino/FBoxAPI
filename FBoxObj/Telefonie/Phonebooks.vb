Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("phonebooks"), XmlType("phonebooks")> Public Class PhonebooksType
    <XmlElement("phonebook")> Public Property Phonebooks As List(Of Phonebook)

    Public Sub New()
        Phonebooks = New List(Of Phonebook)
    End Sub
End Class


