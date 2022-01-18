Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("root"), XmlType("root")> Public Class CallList
    <XmlElement("timestamp")> Public Property Timestamp As String
    <XmlElement("Call")> Public Property Calls As List(Of [Call])

    Public Sub New()
        Calls = New List(Of [Call])
    End Sub
End Class

