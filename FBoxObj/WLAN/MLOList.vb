Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("MLOList")> Public Class MLOList
    <XmlElement("MLONode")> Public Property MLONodes As List(Of MLONode)

    Public Sub New()
        MLONodes = New List(Of MLONode)
    End Sub
End Class