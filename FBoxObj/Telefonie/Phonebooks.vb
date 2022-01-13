Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("phonebooks"), XmlType("phonebooks")> Public Class PhonebooksType
    <XmlElement("phonebook")> Public Property Phonebooks As List(Of Phonebook)
End Class


