Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("root"), XmlType("root")> Public Class CallList
    ''' <summary>
    ''' Timestamp of call list creation (unique ID per call list).
    ''' </summary>
    <XmlElement("timestamp")> Public Property TimeStamp As Integer
    <XmlElement("Call")> Public Property Calls As List(Of [Call])

    Public Sub New()
        Calls = New List(Of [Call])
    End Sub
End Class

