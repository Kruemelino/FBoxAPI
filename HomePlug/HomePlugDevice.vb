''' <summary>
''' TR-064 Support – X_AVM-DE_Homeplug
''' Date: 2017-01-06 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf</see>
''' </summary>
Public Class HomePlugDevice
    Public Property Index As Integer
    Public Property MACAddress As String
    Public Property Active As Boolean
    Public Property Name As String
    Public Property Model As String
    Public Property UpdateAvailable As Boolean
    Public Property UpdateSuccessful As UpdateEnum
End Class
