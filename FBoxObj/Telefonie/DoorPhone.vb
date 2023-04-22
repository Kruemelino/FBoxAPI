Imports System.Xml.Serialization

<Serializable(), XmlType("doorphone")> Public Class DoorPhone
    Implements IEquatable(Of DoorPhone)

    <XmlElement("videoURL")> Public Property VideoURL As String
    <XmlElement("openkey")> Public Property Openkey As String

    Public Overloads Function Equals(other As DoorPhone) As Boolean Implements IEquatable(Of DoorPhone).Equals
        Return _VideoURL.AreEqual(other._VideoURL) AndAlso _Openkey.Equals(other._Openkey)
    End Function
End Class
