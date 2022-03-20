Imports System.Xml.Serialization
<Serializable(), XmlRoot("groupinfo")> Public Class AHAGroupInfo
    ''' <summary>
    ''' interne id des Master/Chef-Schalters, 0 bei "keiner gesetzt"
    ''' </summary>
    <XmlElement("masterdeviceid")> Public Property MasterDeviceID As String

    ''' <summary>
    ''' interne ids der Gruppenmitglieder, kommasepariert
    ''' </summary>
    <XmlElement("members")> Public Property Members As String
End Class
