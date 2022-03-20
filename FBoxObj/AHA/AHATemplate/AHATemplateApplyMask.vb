Imports System.Xml
Imports System.Xml.Serialization
<Serializable(), XmlType("applymask")> Public Class AHATemplateApplyMask
    ''' <summary>
    ''' Unterknoten je nachdem welche Konfiguration gesetzt wird
    ''' </summary>
    <XmlAnyElement> Public Property AllElements() As XmlElement
End Class
