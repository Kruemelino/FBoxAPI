Imports System.Xml.Serialization
<Serializable(), XmlType("button")> Public Class AHAButton

    ''' <summary>
    ''' eindeutige ID, AIN
    ''' </summary>
    <XmlAttribute("identifier")> Public Property Identifier As String

    ''' <summary>
    ''' interne Geräteid
    ''' </summary>
    <XmlAttribute("id")> Public Property ID As String

    ''' <summary>
    ''' Zeitpunkt des letzten Tastendrucks, timestamp in Sekunden seit 1970, 0 oder leer bei unbekannt
    ''' </summary>
    <XmlElement("lastpressedtimestamp")> Public Property LastPressedTimestamp As String

    ''' <summary>
    ''' Optinal: Name
    ''' </summary>
    <XmlElement("name")> Public Property Name As String
End Class
