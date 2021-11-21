Imports System.Xml.Serialization

<Serializable()>
    <XmlRoot("List")> Public Class DeflectionList
    <XmlElement("Item")> Public Property Deflections As List(Of Deflection)

    Public Sub New()
        Deflections = New List(Of Deflection)
    End Sub
End Class

