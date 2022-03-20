Imports System.Xml.Serialization
<Serializable(), XmlType("simpleonoff")> Public Class AHASimpleOnOff
    ''' <summary>
    ''' aktueller Schaltzutand, 0:aus, 1:an
    ''' </summary>
    <XmlElement("state")> Public Property State As String
End Class
