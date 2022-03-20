Imports System.Xml.Serialization
<Serializable(), XmlType("stats")> Public Class AHAStatValues
    ''' <summary>
    ''' Anzahl der Werte
    ''' </summary>
    <XmlAttribute("count")> Public Property Count As Integer

    ''' <summary>
    ''' Zeitliche Abstand/Auflösung in Sekunden
    ''' </summary>
    <XmlAttribute("grid")> Public Property Grid As Integer

    ''' <summary>
    ''' Der Inhalt von <see cref="AHAStatValues"/> ist eine count-Anzahl kommaseparierte Liste von Werten. Werte mit "-" sind unbekannt/undefiniert.</summary>
    ''' <returns></returns>
    <XmlText> Public Property Values As String

    <XmlIgnore> Public ReadOnly Property SeperatedValues As String()
        Get
            Return Values?.Split(","c)
        End Get
    End Property

End Class
