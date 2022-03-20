Imports System.Xml.Serialization
<Serializable(), XmlRoot("devicelist")> Public Class AHADeviceList
    <XmlAttribute("version")> Public Property Version As String
    <XmlAttribute("fwversion")> Public Property FWVersion As String
    <XmlElement("device")> Public Property Devices As List(Of AHADevice)
    <XmlElement("group")> Public Property Groups As List(Of AHAGroup)
End Class
