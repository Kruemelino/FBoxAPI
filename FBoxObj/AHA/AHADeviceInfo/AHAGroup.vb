Imports System.Xml.Serialization
''' <summary>
''' Im Aufbau sind bei <see cref="AHADevice"/> und <see cref="AHAGroup"/> identisch, nur das <see cref="AHAGroup"/> noch den <see cref="AHAGroupInfo"/> Knoten enthält.
''' </summary>
<Serializable(), XmlType("group")> Public Class AHAGroup
    Inherits AHADevice
    <XmlAttribute("synchronized")> Public Property Synchronized As String
    <XmlElement("groupinfo")> Public Property GroupInfo As AHAGroupInfo
End Class
