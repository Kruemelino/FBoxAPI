Imports System.Xml.Serialization
<Serializable(), XmlRoot("state")> Public Class AHASubscriptionState
    ''' <summary>Geräteanmeldestatus
    ''' <list type="table">
    ''' <item><term>0</term><description>Anmeldung läuft nicht</description></item>
    ''' <item><term>1</term><description>Anmeldung läuft</description></item>
    ''' <item><term>2</term><description>timeout</description></item>
    ''' <item><term>3</term><description>sonstiger Error</description></item>
    ''' </list>
    ''' </summary>
    <XmlAttribute("code")> Public Property Code As String
    ''' <summary>
    ''' enthält gegebenenfalls die AIN des zuletzt angemeldeten Geräts
    ''' </summary>
    <XmlElement("latestain")> Public Property LatestAIN As String
End Class
