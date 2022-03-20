Imports System.Xml.Serialization
<Serializable(), XmlType("avmbutton")> Public Class AHAAVMButton

    ''' <summary>
    ''' relative Luftfeuchtigkeit in Prozent von 0 bis 100, Spezialwert: -9999 bei unbekannt
    ''' </summary>
    <XmlElement("humidity")> Public Property Humidity As String

    ''' <summary>
    ''' Ein <see cref="AHAAVMButton"/> kann gegebenenfalls mehrere <see cref="AHAButton"/>-Knoten haben.
    ''' </summary>
    ''' <remarks>Der FRITZ!DECT 400 hat 2 <see cref="AHAButton"/>-Knoten.</remarks>
    <XmlElement("button")> Public Property Buttons As List(Of AHAButton)

End Class
