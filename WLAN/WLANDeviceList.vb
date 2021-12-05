Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("List")> Public Class WLANDeviceList
    <XmlElement("Item")> Public Property AssociatedDevices As List(Of AssociatedDevice)

    Public Sub New()
        AssociatedDevices = New List(Of AssociatedDevice)
    End Sub
End Class
