Imports System.Xml.Serialization

<Serializable(), XmlType("number")> Public Class Number

    <XmlAttribute("type")> Public Property Type As TelNrTyp


    <XmlAttribute("vanity")> Public Property Vanity As String


    <XmlAttribute("prio")> Public Property Prio As String


    <XmlAttribute("quickdial")> Public Property QuickDial As String


    <XmlText()> Public Property Number As String

End Class
