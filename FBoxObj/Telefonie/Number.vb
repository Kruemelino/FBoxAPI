Imports System.Xml.Serialization

<Serializable(), XmlType("number")> Public Class NumberType

    <XmlAttribute("type")> Public Property Type As TelNrTypEnum
    <XmlAttribute("vanity")> Public Property Vanity As String
    <XmlAttribute("prio")> Public Property Prio As String
    <XmlAttribute("quickdial")> Public Property QuickDial As String
    <XmlAttribute("id")> Public Property ID As Integer
    <XmlText()> Public Property Number As String

End Class
