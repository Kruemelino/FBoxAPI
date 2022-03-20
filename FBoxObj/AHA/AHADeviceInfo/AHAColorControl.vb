Imports System.Xml.Serialization
<Serializable(), XmlType("colorcontrol")> Public Class AHAColorControl

    ''' <summary>
    ''' Bitmaske -- 0x01 = HueSaturation-Mode, 0x04 = Farbtemperatur-Mode
    ''' </summary>
    <XmlAttribute("supported_modes")> Public Property SupportedModes As String

    ''' <summary>
    ''' 1(HueSaturation), 4 (Farbtemperatur) oder ""(leer → unbekannt)
    ''' </summary>
    <XmlAttribute("current_mode")> Public Property CurrentMode As String

    ''' <summary>
    ''' Hue-Wert in Grad, 0 bis 359 (0° bis 359°)
    ''' </summary>
    ''' <remarks>Achtung nur, wenn current_mode == 1(HueSaturation) ansonsten leer/undefiniert</remarks>
    <XmlElement("hue")> Public Property Hue As String

    ''' <summary>
    ''' Saturation-Wert von 0(0%) bis 255(100%)
    ''' </summary>
    ''' <remarks>Achtung nur, wenn current_mode == 1(HueSaturation) ansonsten leer/undefiniert</remarks>
    <XmlElement("saturation")> Public Property Saturation As String

    ''' <summary>
    ''' Wert in Kelvin, ein typischer Wertebereich geht von etwa 2700° bis 6500°
    ''' </summary>
    <XmlElement("temperature")> Public Property Temperature As String
End Class
