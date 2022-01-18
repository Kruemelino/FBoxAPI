''' <summary>
''' TR-064 Support – X_UPnP 
''' Date: 2009-09-22 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_upnp.pdf</see>
''' </summary>
Public Interface IX_upnpSCPD
    Inherits IServiceBase
    Function GetInfo(ByRef Enable As Boolean, ByRef UPnPMediaServer As Boolean) As Boolean
    Function SetConfig(Enable As Boolean, UPnPMediaServer As Boolean) As Boolean

End Interface
