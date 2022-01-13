Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("List")> Public Class HostList

    <XmlElement("Item")> Public Property SIPClients As List(Of HostEntry)

    Public Sub New()
        SIPClients = New List(Of HostEntry)
    End Sub

End Class