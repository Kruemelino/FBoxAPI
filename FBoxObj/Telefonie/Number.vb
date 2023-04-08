Imports System.Xml.Serialization

<Serializable(), XmlType("number")> Public Class NumberType
    Implements IEquatable(Of NumberType)

    <XmlAttribute("type")> Public Property Type As TelNrTypEnum
    <XmlAttribute("vanity")> Public Property Vanity As String
    <XmlAttribute("prio")> Public Property Prio As String
    <XmlAttribute("quickdial")> Public Property QuickDial As String
    <XmlAttribute("id")> Public Property ID As Integer
    <XmlText()> Public Property Number As String

    Public Overloads Function Equals(other As NumberType) As Boolean Implements IEquatable(Of NumberType).Equals
        ' Vergleich auf ID, Vanity und Prio wird übergangen
        With other
            Return Type = .Type AndAlso
                   QuickDial.AreEqual(.QuickDial) AndAlso
                   Number.AreEqual(.Number)
        End With

    End Function
End Class
