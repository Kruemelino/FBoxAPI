Imports System.Xml.Serialization
<Serializable(), XmlType("alert")> Public Class AHAAlert
    ''' <summary>
    ''' 0/1 - letzter übermittelter Alarmzustand (leer bei unbekannt oder Fehler)
    ''' <para>Beim Rollladen als Bitmaske auszuwerten.</para>
    ''' <list type="table">
    ''' <item><term>0000 0000</term><description>Es liegt kein Fehler vor.</description></item>
    ''' <item><term>0000 0001</term><description>Hindernisalarm, der Rollladen wird gestoppt und ein kleines Stück in entgegengesetzte Richtung bewegt.</description></item>
    ''' <item><term>0000 0010</term><description>Temperaturalarm, Motor überhitzt.</description></item>
    ''' </list>
    ''' </summary>
    <XmlElement("state")> Public Property State As String

    ''' <summary>
    ''' Zeitpunkt der letzten Alarmzustandsänderung, timestamp in Sekunden seit 1970, 0 oder leer bei unbekannt
    ''' </summary>
    <XmlElement("lastalertchgtimestamp")> Public Property LastAlertTimeStamp As String
End Class
