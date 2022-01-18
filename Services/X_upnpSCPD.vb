''' <summary>
''' TR-064 Support – X_UPnP 
''' Date: 2009-09-22 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_upnp.pdf</see>
''' </summary>
Friend Class X_upnpSCPD
    Implements IX_upnpSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_upnpSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_upnpSCPD Implements IX_upnpSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Enable As Boolean, ByRef UPnPMediaServer As Boolean) As Boolean Implements IX_upnpSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)
            Return .TryGetValueEx("NewEnable", Enable) And
                   .TryGetValueEx("NewUPnPMediaServer", UPnPMediaServer)
        End With
    End Function

    Public Function SetConfig(Enable As Boolean, UPnPMediaServer As Boolean) As Boolean Implements IX_upnpSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig",
                              New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr},
                                                                      {"NewUPnPMediaServer", UPnPMediaServer.ToBoolStr}}).ContainsKey("Error")

    End Function
End Class
