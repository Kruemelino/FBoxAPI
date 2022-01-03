''' <summary>
''' TR-064 Support – X_AVM-DE_Homeplug
''' Date: 2017-01-06 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf</see>
''' </summary>
Public Interface IX_homeplugSCPD
    Inherits IServiceBase

    Function GetNumberOfDeviceEntries(NumberOfEntries As Integer) As Boolean
    Function GetNumberOfDeviceEntries(Index As Integer, ByRef Device As HomePlugDevice) As Boolean
    Function GetSpecificDeviceEntry(MACAddress As String, ByRef Device As HomePlugDevice) As Boolean
    Function DeviceDoUpdate(MACAddress As String) As Boolean

End Interface
