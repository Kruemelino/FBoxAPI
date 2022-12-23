Public Class Settings
    ''' <summary>
    ''' Angabe, ob der AURA (AVM USB Remote Access) Service geladen werden soll.
    ''' </summary>
    ''' <remarks>Please note: You must activate USB remote access in FRITZ!Box settings to get a access to this service!</remarks>
    Public Property AuraService As Boolean

    ''' <summary>
    ''' Die Anmeldeinformationen (Benutzername und Passwort) als <see cref="NetworkCredential"/>.
    ''' </summary>
    Public Property Anmeldeinformationen As Net.NetworkCredential

    ''' <summary>
    ''' 
    ''' </summary>
    Public Property FritzBoxAdresse As String

    ''' <summary>
    ''' Die Schnittstelle für die Realisierung des Loggings.
    ''' </summary>
    Public Property FBAPIConnector As IFBoxAPIConnector
End Class
