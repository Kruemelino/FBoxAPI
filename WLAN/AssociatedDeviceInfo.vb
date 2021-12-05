Imports System.Xml.Serialization
<Serializable()> Public Class AssociatedDevice
    <XmlElement("AssociatedDeviceIndex")> Public Property AssociatedDeviceIndex As String
    <XmlElement("AssociatedDeviceMACAddress")> Public Property AssociatedDeviceMACAddress As String
    <XmlElement("AssociatedDeviceIPAddress")> Public Property AssociatedDeviceIPAddress As String
    <XmlElement("AssociatedDeviceAuthState")> Public Property AssociatedDeviceAuthState As Boolean
    <XmlElement("X_AVM-DE_Speed")> Public Property Speed As Integer
    <XmlElement("X_AVM-DE_SignalStrength")> Public Property SignalStrength As Integer
    <XmlElement("AssociatedDeviceChannel")> Public Property AssociatedDeviceChannel As Integer
    <XmlElement("AssociatedDeviceGuest")> Public Property AssociatedDeviceGuest As Integer
End Class
