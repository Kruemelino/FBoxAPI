Imports System.Xml.Serialization

''' <summary>
''' TR-064 Support – Hosts
''' Date: 2022-10-13
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf</see>
''' </summary>
<Serializable()> Public Class HostEntry
    ''' <summary>
    ''' '1' If host is active, '0' if host is inactive (currently not connected)
    ''' </summary>
    <XmlElement("Active")> Public Property Active As Boolean

    ''' <summary>
    ''' Name of the host device
    ''' </summary>
    <XmlElement("HostName")> Public Property HostName As String

    ''' <summary>
    ''' Sequential number for each host
    ''' </summary>
    <XmlElement("Index")> Public Property Index As Integer

    ''' <summary>
    ''' The interface with which the host accesses the F!Box ("Ethernet", "802.11", "HomePlug", "")
    ''' </summary>
    <XmlElement("InterfaceType")> Public Property InterfaceType As String

    ''' <summary>
    ''' The host's ip address
    ''' </summary>
    <XmlElement("IPAddress")> Public Property IPAddress As String

    ''' <summary>
    ''' The host's MAC address
    ''' </summary>
    <XmlElement("MACAddress")> Public Property MACAddress As String

    ''' <summary>
    ''' Flag wich represent the WAN access allowed sate
    ''' </summary>
    <XmlElement("X_AVM-DE_Disallow")> Public Property Disallow As Boolean

    ''' <summary>
    ''' '1' if the host is connected with guest network, '0' if connected with home network
    ''' </summary>
    <XmlElement("X_AVM-DE_Guest")> Public Property Guest As Boolean

    ''' <summary>
    ''' Link to a text file which contains the changelog of the last firmware update
    ''' </summary>
    <XmlElement("X_AVM-DE_InfoURL")> Public Property InfoURL As String

    ''' <summary>
    ''' Flag which shows if the landevice can be part of a 'Mesh'
    ''' </summary>
    <XmlElement("X_AVM-DE_IsMeshable")> Public Property IsMeshable As Boolean

    ''' <summary>
    ''' Comma separated list with device MAC Addresses e.g. different interfaces.
    ''' </summary>
    ''' <remarks>Only AVM devices have this list, other devices have an empty tag.</remarks>
    <XmlElement("X_AVM-DE_MACAddressList")> Public Property MACAddressList As String

    ''' <summary>
    ''' Model name or number of the F!device
    ''' </summary>
    <XmlElement("X_AVM-DE_Model")> Public Property Model As String

    ''' <summary>
    ''' If host is connected via ethernet, it shows the port number <br/>
    ''' Port#, beginning with 1; only for InterfaceType 'Ethernet'
    ''' </summary>
    <XmlElement("X_AVM-DE_Port")> Public Property Port As String

    ''' <summary>
    ''' Flag which shows if the landevice has realtime priority
    ''' </summary>
    <XmlElement("X_AVM-DE_Priority")> Public Property Priority As Boolean

    ''' <summary>
    ''' Flag which shows if this landevice was sending the request
    ''' </summary>
    <XmlElement("X_AVM-DE_RequestClient")> Public Property RequestClient As Boolean

    ''' <summary>
    ''' Shows the speed in Mbit/s
    ''' </summary>
    <XmlElement("X_AVM-DE_Speed")> Public Property Speed As Integer

    ''' <summary>
    ''' '1' if update is available, '0' if no new update is available
    ''' </summary>
    <XmlElement("X_AVM-DE_UpdateAvailable")> Public Property UpdateAvailable As Boolean

    ''' <summary>
    ''' Shows the state of the last firmware update process
    ''' </summary>
    <XmlElement("X_AVM-DE_UpdateSuccessful")> Public Property UpdateSuccessful As String

    <XmlElement("X_AVM-DE_URL")> Public Property URL As String

    ''' <summary>
    ''' ‘1’ if host is a vpn connection else ‘0’.
    ''' </summary>
    <XmlElement("X_AVM-DE_VPN")> Public Property VPN As Boolean

    ''' <summary>
    ''' Shows if the landevice has WAN access "granted", "denied", "error"
    ''' </summary>
    <XmlElement("X_AVM-DE_WANAccess")> Public Property WANAccess As String

    ''' <summary>
    ''' The host’s friendly name
    ''' </summary>
    <XmlElement("X_AVM-DE_FriendlyName")> Public Property FriendlyName As String

    ''' <summary>
    ''' '1' if the friendly name can be changed, '0' if not
    ''' </summary>
    <XmlElement("X_AVM-DE_FriendlyNameIsWriteable")> Public Property FriendlyNameIsWriteable As Boolean

    <XmlIgnore> Public Property AddressSource As String

    <XmlIgnore> Public Property LeaseTimeRemaining As Integer
End Class
