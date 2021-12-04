Imports System.Xml.Serialization
<Serializable(), XmlType("Call")> Public Class [Call]

    ''' <summary>
    ''' Unique ID per call. 
    ''' </summary>
    <XmlElement("Id", GetType(Integer))> Public Property ID As Integer

    ''' <summary>
    ''' 1 incoming,
    ''' 2 missed,
    ''' 3 outgoing,
    ''' 9 active incoming,
    ''' 10 rejected incoming,
    ''' 11 active outgoing 
    ''' </summary>
    <XmlElement("Type", GetType(Integer))> Public Property Type As Integer

    ''' <summary>
    ''' Number or name of called party 
    ''' </summary>
    <XmlElement("Called")> Public Property Called As String

    ''' <summary>
    ''' Number of calling party 
    ''' </summary>
    <XmlElement("Caller")> Public Property Caller As String

    ''' <summary>
    ''' Own Number of called party (incoming call)
    ''' </summary>
    <XmlElement("CalledNumber")> Public Property CalledNumber As String

    ''' <summary>
    ''' Own Number of called party (outgoing call) 
    ''' </summary>
    <XmlElement("CallerNumber")> Public Property CallerNumber As String

    ''' <summary>
    ''' Name of called/ called party (outgoing/ incoming call) 
    ''' </summary>
    <XmlElement("Name")> Public Property Name As String

    ''' <summary>
    ''' pots, isdn, sip, umts, '' 
    ''' </summary>
    <XmlElement("Numbertype")> Public Property Numbertype As String

    ''' <summary>
    ''' Name of used telephone port. 
    ''' </summary>
    <XmlElement("Device")> Public Property Device As String

    ''' <summary>
    ''' Number of telephone port. 
    ''' </summary>
    ''' <remarks>    
    ''' To differ between voice calls, fax calls and TAM calls use the Port value.
    ''' E.g. if port equals 5 it Is a fax call. If port equals 6 Or port in in the rage of 40 to 49 it Is a TAM call.
    ''' </remarks>
    <XmlElement("Port")> Public Property Port As Integer

    ''' <summary>
    ''' 31.07.12 12:03
    ''' </summary>
    <XmlElement("Date")> Public Property [Date] As String

    ''' <summary>
    ''' hh:mm (minutes rounded up) 
    ''' </summary>
    <XmlElement("Duration")> Public Property Duration As String

    <XmlElement("Count")> Public Property Count As String

    ''' <summary>
    '''  A call list may contain URLs for telephone answering machine messages or fax messages.
    '''  The content can be downloaded ising the protocol, hostname and port with the path URL.<br/>
    '''  An example is described here:<br/>
    '''  Protocol: https
    '''  Hostname: fritz.box
    '''  Port: 49443
    '''  path URL :  /download.lua?path=/var/media/ftp/USB/FRITZ/voicebox/rec/rec.0.000
    '''  The combination of<br/>
    '''  Protocoll + :// + Hostname + : + Port + path URL<br/>
    '''  will be the complete URL<br/>
    '''  https://fritz.box:49443/download.lua?path=/var/media/ftp/USB/FRITZ/voicebox/rec/rec.0.000<br/>
    '''  Please note, that this URL might require authentication. 
    ''' </summary>
    ''' <returns>URL path to TAM or FAX file.</returns>
    <XmlElement("Path")> Public Property Path As String

End Class
