Imports System.Xml.Serialization
<Serializable(), XmlType("color")> Public Class AHAHueColor
    <XmlAttribute("sat_index")> Public Property Index As Integer

    ''' <summary>
    '''  Der hue-Wertebereich kann 0 bis 359 Grad sein.
    ''' </summary>
    <XmlAttribute("hue")> Public Property Hue As Integer

    ''' <summary>
    ''' Der Saturation-Wertebereich geht von 0(0%) bis 255(100%). 
    ''' </summary>
    <XmlAttribute("sat")> Public Property Saturation As Integer

    ''' <summary>
    ''' Der Value-Wertebereich geht von 0 (0%) bis 255 (100%)
    ''' </summary>
    ''' <returns>Der Wert ist nur für die Anzeige der Farben interessant und wird beim Setzen nicht an die FRITZ!Box übermittelt</returns>
    <XmlAttribute("val")> Public Property Value As Integer

End Class
