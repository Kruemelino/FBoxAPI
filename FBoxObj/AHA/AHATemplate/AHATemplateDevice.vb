Imports System.Xml.Serialization
<Serializable(), XmlType("device")> Public Class AHATemplateDevice

    ''' <summary>
    ''' identifier: eindeutige string ID
    ''' </summary>
    <XmlAttribute("identifier")> Public Property Identifier As String
End Class
