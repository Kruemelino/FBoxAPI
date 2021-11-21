Imports System.Xml.Serialization

<Serializable(), XmlType("person")> Public Class Person
    <XmlElement("realName")> Public Property RealName As String


    ''' <summary>
    ''' A telephone book may contain URLs with an image for the contact. 
    ''' The content can be downloaded using the protocol, hostname and port with the image URL.
    ''' An example is described here:<br/>
    ''' Protocol: https<br/>
    ''' Hostname: fritz.box<br/>
    ''' Port: 49443<br/>
    ''' image URL : /download.lua?path=/var/media/ftp/JetFlash-Transcend4GB-01/FRITZ/fonpix/1316705057-0.jpg<br/>
    ''' The combination of Protocoll + :// + Hostname + : + Port + image URL will be the complete URL<br/>
    ''' https://fritz.box:49443/download.lua?path=/var/media/ftp/JetFlash-Transcend4GB01/FRITZ/fonpix/1316705057-0.jpg<br/>
    ''' Please note, that this URL might require authentication. 
    ''' </summary>
    ''' <returns>HTTP URL to image for this contact</returns>
    <XmlElement("imageURL")> Public Property ImageURL As String

End Class
