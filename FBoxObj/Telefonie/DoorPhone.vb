Imports System.Xml.Serialization

<Serializable(), XmlType("doorphone")> Public Class DoorPhone
    <XmlElement("videoURL")> Public Property VideoURL As String
    <XmlElement("openkey")> Public Property Openkey As String
End Class
