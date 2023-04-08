Imports System.Xml.Serialization

<Serializable(), XmlType("email")> Public Class Email
    Implements IEquatable(Of Email)

    <XmlText()> Public Property EMail As String
    <XmlAttribute("classifier")> Public Property Classifier As EMailTypEnum

    Public Overloads Function Equals(other As Email) As Boolean Implements IEquatable(Of Email).Equals
        Return EMail.AreEqual(other.EMail) AndAlso Classifier = other.Classifier
    End Function
End Class

