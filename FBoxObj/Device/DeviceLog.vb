Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("DeviceLog")> Public Class DeviceLog
    <XmlElement("Event")> Public Property DeviceLogEvent As List(Of LogEntry)

    Public Sub New()
        DeviceLogEvent = New List(Of LogEntry)
    End Sub
End Class
