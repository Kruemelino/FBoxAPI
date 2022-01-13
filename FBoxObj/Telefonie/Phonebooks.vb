Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("phonebooks"), XmlType("phonebooks")> Public Class Phonebooks
    <XmlElement("phonebook")> Public Property Phonebooks As List(Of Phonebook)
End Class


