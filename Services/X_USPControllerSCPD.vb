''' <summary>
''' TR-064 Support – X_AVM-DE_USPController 
''' Date: 2022-10-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_uspcontrollerSCPD.pdf</see>
''' </summary>
Friend Class X_USPControllerSCPD
    Implements IX_USPControllerSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 11, 7) Implements IX_USPControllerSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_USPControllerSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_uspcontroller Implements IX_USPControllerSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_USPControllerSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As USPInfo) As Boolean Implements IX_USPControllerSCPD.GetInfo
        If Info Is Nothing Then Info = New USPInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            If .ContainsKey("NewMinCharsEndpointID") Then

                Return .TryGetValueEx("NewMinCharsEndpointID", Info.MinCharsEndpointID) And
                       .TryGetValueEx("NewMaxCharsEndpointID", Info.MaxCharsEndpointID) And
                       .TryGetValueEx("NewAllowedCharsEndpointID", Info.AllowedCharsEndpointID) And
                       .TryGetValueEx("NewMinCharsHostname", Info.MinCharsHostname) And
                       .TryGetValueEx("NewMaxCharsHostname", Info.MaxCharsHostname) And
                       .TryGetValueEx("NewMinCharsPath", Info.MinCharsPath) And
                       .TryGetValueEx("NewMaxCharsPath", Info.MaxCharsPath) And
                       .TryGetValueEx("NewMinCharsUsername", Info.MinCharsUsername) And
                       .TryGetValueEx("NewMaxCharsUsername", Info.MaxCharsUsername) And
                       .TryGetValueEx("NewMinCharsPassword", Info.MinCharsPassword) And
                       .TryGetValueEx("NewMaxCharsPassword", Info.MaxCharsPassword) And
                       .TryGetValueEx("NewUSPMyFRITZEnabled", Info.USPMyFRITZEnabled)
            Else
                Return False
            End If
        End With
    End Function

    Public Function GetUSPControllerByIndex(ByRef USPCntrlr As USPController, Index As Integer) As Boolean Implements IX_USPControllerSCPD.GetUSPControllerByIndex
        If USPCntrlr Is Nothing Then USPCntrlr = New USPController

        With TR064Start(ServiceFile, "GetUSPControllerByIndex", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

            If .ContainsKey("NewEnable") Then

                USPCntrlr.Index = Index

                Return .TryGetValueEx("NewEnable", USPCntrlr.Enable) And
                       .TryGetValueEx("NewEndpointID", USPCntrlr.EndpointID) And
                       .TryGetValueEx("NewMTP", USPCntrlr.MTP) And
                       .TryGetValueEx("NewHostname", USPCntrlr.Hostname) And
                       .TryGetValueEx("NewPath", USPCntrlr.Path) And
                       .TryGetValueEx("NewPort", USPCntrlr.Port) And
                       .TryGetValueEx("NewUseTLS", USPCntrlr.UseTLS) And
                       .TryGetValueEx("NewAccessRightSmarthome", USPCntrlr.AccessRightSmarthome) And
                       .TryGetValueEx("NewAccessRightMesh", USPCntrlr.AccessRightMesh) And
                       .TryGetValueEx("NewAccessRightInternet", USPCntrlr.AccessRightInternet) And
                       .TryGetValueEx("NewAccessRightSystem", USPCntrlr.AccessRightSystem) And
                       .TryGetValueEx("NewAccessRightController", USPCntrlr.AccessRightController) And
                       .TryGetValueEx("NewAccessRightWiFi", USPCntrlr.AccessRightWiFi) And
                       .TryGetValueEx("NewAccessRightVoIP", USPCntrlr.AccessRightVoIP) And
                       .TryGetValueEx("NewUsername", USPCntrlr.Username)
            Else
                Return False
            End If
        End With
    End Function

    Public Function GetUSPControllerNumberOfEntries(ByRef USPControllerNumberOfEntries As Integer) As Boolean Implements IX_USPControllerSCPD.GetUSPControllerNumberOfEntries
        Return Not TR064Start(ServiceFile, "GetUSPControllerNumberOfEntries", ServiceID, Nothing).TryGetValueEx("NewUSPControllerNumberOfEntries", USPControllerNumberOfEntries)
    End Function

    Public Function AddUSPController(USPCntrlr As USPController, Password As String, ByRef Index As Integer) As Boolean Implements IX_USPControllerSCPD.AddUSPController
        Return Not TR064Start(ServiceFile,
                              "AddUSPController", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnable", USPCntrlr.Enable.ToBoolStr},
                                                                      {"NewEndpointID", USPCntrlr.EndpointID},
                                                                      {"NewMTP", USPCntrlr.MTP},
                                                                      {"NewHostname", USPCntrlr.Hostname},
                                                                      {"NewPath", USPCntrlr.Path},
                                                                      {"NewPort", USPCntrlr.Port.ToString},
                                                                      {"NewUseTLS", USPCntrlr.UseTLS.ToBoolStr},
                                                                      {"NewAccessRightSmarthome", USPCntrlr.AccessRightSmarthome.ToBoolStr},
                                                                      {"NewAccessRightMesh", USPCntrlr.AccessRightMesh.ToBoolStr},
                                                                      {"NewAccessRightInternet", USPCntrlr.AccessRightInternet.ToBoolStr},
                                                                      {"NewAccessRightSystem", USPCntrlr.AccessRightSystem.ToBoolStr},
                                                                      {"NewAccessRightController", USPCntrlr.AccessRightController.ToBoolStr},
                                                                      {"NewAccessRightWiFi", USPCntrlr.AccessRightWiFi.ToBoolStr},
                                                                      {"NewAccessRightVoIP", USPCntrlr.AccessRightVoIP.ToBoolStr},
                                                                      {"NewUsername", USPCntrlr.Username},
                                                                      {"NewPassword", Password}}).TryGetValueEx("NewIndex", Index)


    End Function

    Public Function DeleteUSPControllerByIndex(Index As Integer) As Boolean Implements IX_USPControllerSCPD.DeleteUSPControllerByIndex
        Return Not TR064Start(ServiceFile, "DeleteUSPControllerByIndex", ServiceID,
                              New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}}).ContainsKey("Error")
    End Function

    Public Function SetUSPControllerEnableByIndex(Index As Integer, Enable As Boolean) As Boolean Implements IX_USPControllerSCPD.SetUSPControllerEnableByIndex
        Return Not TR064Start(ServiceFile, "SetUSPControllerEnableByIndex", ServiceID,
                              New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                      {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetUSPMyFRITZEnable(ByRef USPMyFRITZEnabled As Boolean) As Boolean Implements IX_USPControllerSCPD.GetUSPMyFRITZEnable
        Return TR064Start(ServiceFile, "GetUSPMyFRITZEnable", ServiceID, Nothing).TryGetValueEx("NewUSPMyFRITZEnabled", USPMyFRITZEnabled.ToBoolStr)
    End Function

    Public Function SetUSPMyFRITZEnable(USPMyFRITZEnabled As Boolean) As Boolean Implements IX_USPControllerSCPD.SetUSPMyFRITZEnable
        Return Not TR064Start(ServiceFile, "SetUSPMyFRITZEnable", ServiceID, New Dictionary(Of String, String) From {{"NewUSPMyFRITZEnabled", USPMyFRITZEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function
End Class
