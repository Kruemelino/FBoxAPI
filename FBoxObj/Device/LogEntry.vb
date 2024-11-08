Imports System.Xml.Serialization

<Serializable()> Public Class LogEntry
    <XmlElement("id")> Public Property ID As Integer
    <XmlElement("group")> Public Property Group As DeviceLogFilter
    <XmlElement("date")> Public Property DateStr As String
    <XmlElement("time")> Public Property Time As String
    <XmlElement("msg")> Public Property Message As String
End Class
