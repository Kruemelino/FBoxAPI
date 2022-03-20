Imports System.Xml.Serialization
<Serializable(), XmlType("nextchange")> Public Class AHAHKRNextChange
    ''' <summary>
    ''' timestamp in Sekunden seit 1970, 0 bei unbekannt
    ''' </summary>
    <XmlElement("endperiod")> Public Property EndPeriod As String

    ''' <summary>
    ''' Zieltemperatur, Wertebereich siehe tsoll(255/0xff ist unbekannt/undefiniert)
    ''' </summary>
    <XmlElement("tchange")> Public Property Tchange As String
End Class
