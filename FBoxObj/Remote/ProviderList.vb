Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("List")> Public Class ProviderList
    <XmlElement("Item")> Public Property Provider As List(Of Provider)

    Public Sub New()
        Provider = New List(Of Provider)
    End Sub
End Class
