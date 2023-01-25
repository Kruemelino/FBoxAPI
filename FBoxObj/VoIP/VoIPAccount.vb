Imports System.Xml.Serialization
<Serializable(), XmlType("Account")> Public Class VoIPAccount
    <XmlElement("VoIPAccountIndex")> Public Property VoIPAccountIndex As Integer
    <XmlElement("VoIPRegistrar")> Public Property VoIPRegistrar As String
    <XmlElement("VoIPNumber")> Public Property VoIPNumber As String
    <XmlElement("VoIPUsername")> Public Property VoIPUsername As String
    <XmlElement("VoIPOutboundProxy")> Public Property VoIPOutboundProxy As String
    <XmlElement("VoIPSTUNServer")> Public Property VoIPSTUNServer As String
    <XmlElement("X_AVM-DE_VoIPStatus")> Public Property VoIPStatus As String

End Class