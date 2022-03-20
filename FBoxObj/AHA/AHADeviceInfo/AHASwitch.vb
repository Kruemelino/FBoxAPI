Imports System.Xml.Serialization
<Serializable(), XmlType("switch")> Public Class AHASwitch
    ''' <summary>
    ''' Schaltzustand aus/an (leer bei unbekannt oder Fehler)
    ''' </summary>
    <XmlElement("state")> Public Property State As String

    ''' <summary>
    ''' "auto" oder "manuell" -> automatische Zeitschaltung oder manuell schalten (leer bei unbekannt oder Fehler)
    ''' </summary>
    <XmlElement("mode")> Public Property Mode As String

    ''' <summary>
    ''' 0/1 - Schaltsperre über UI/API ein nein/ja(leer bei unbekannt oder Fehler)
    ''' </summary>
    <XmlElement("lock")> Public Property Lock As String

    ''' <summary>
    ''' 0/1 - Schaltsperre direkt am Gerät ein nein/ja(leer bei unbekannt oder Fehler)
    ''' </summary>
    <XmlElement("devicelock")> Public Property DeviceLock As String
End Class
