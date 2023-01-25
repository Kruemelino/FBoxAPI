Imports System.Xml.Serialization
<XmlRoot("X_AVM-DE_VoIPAccountList"), XmlType("X_AVM-DE_VoIPAccountList")> Public Class VoIPAccountList
    <XmlArray("AccountList")> <XmlArrayItem("Account")> Public Property AccountList As List(Of VoIPAccount)
End Class