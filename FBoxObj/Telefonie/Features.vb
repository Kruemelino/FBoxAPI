Imports System.Xml.Serialization

<Serializable(), XmlType("features")> Public Class Features
    Implements IEquatable(Of Features)

    <XmlAttribute("doorphone")> Public Property Doorphone As String

    Public Overloads Function Equals(other As Features) As Boolean Implements IEquatable(Of Features).Equals
        Return _Doorphone.AreEqual(other._Doorphone)
    End Function
End Class
