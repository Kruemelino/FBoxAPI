Imports System.Xml.Serialization
<Serializable()> Public Class MLONode
    <XmlElement("Index")> Public Property Index As Integer
    <XmlElement("X_AVM-DE_ML_AssociatedDeviceMACAddress")> Public Property AssociatedDeviceMACAddress As String
    <XmlElement("X_AVM-DE_ML_Channel")> Public Property Channel As Integer
    <XmlElement("X_AVM-DE_ML_ChannelWidth")> Public Property ChannelWidth As Integer
    <XmlElement("X_AVM-DE_ML_FrequencyBand")> Public Property FrequencyBand As String
    <XmlElement("X_AVM-DE_ML_SignalStrength")> Public Property SignalStrength As Integer
    <XmlElement("X_AVM-DE_ML_Speed")> Public Property Speed As Integer
    <XmlElement("X_AVM-DE_ML_SpeedRX")> Public Property SpeedRX As Integer
    <XmlElement("X_AVM-DE_ML_SpeedMax")> Public Property SpeedMax As Integer
    <XmlElement("X_AVM-DE_ML_SpeedRXMax")> Public Property SpeedRXMax As Integer
End Class
