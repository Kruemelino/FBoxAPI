Imports System.Xml.Serialization

<Serializable(), XmlType("email")> Public Class Email

    <XmlText()> Public Property EMail As String
    <XmlAttribute("classifier")> Public Property Classifier As EMailTyp

End Class

