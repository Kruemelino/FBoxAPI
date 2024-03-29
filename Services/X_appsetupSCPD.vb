﻿''' <summary>
''' TR-064 Support – X_AVM-DE_WebDAVClient  
''' Date: 2022-10-17
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_appsetup.pdf</see>
''' </summary>
Friend Class X_appsetupSCPD
    Implements IX_appsetupSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 17) Implements IX_appsetupSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_appsetupSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_appsetupSCPD Implements IX_appsetupSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_appsetupSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetAppMessageFilter(AppId As String, ByRef FilterList As String) As Boolean Implements IX_appsetupSCPD.GetAppMessageFilter
        Return TR064Start(ServiceFile, "GetAppMessageFilter", ServiceID,
                          New Dictionary(Of String, String) From {{"NewAppId", AppId}}).TryGetValueEx("NewFilterList", FilterList)
    End Function

    Public Function GetAppRemoteInfo(ByRef Info As RemoteInfo) As Boolean Implements IX_appsetupSCPD.GetAppRemoteInfo
        If Info Is Nothing Then Info = New RemoteInfo

        With TR064Start(ServiceFile, "GetAppRemoteInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewSubnetMask", Info.SubnetMask) And
                   .TryGetValueEx("NewIPAddress", Info.IPAddress) And
                   .TryGetValueEx("NewExternalIPAddress", Info.ExternalIPAddress) And
                   .TryGetValueEx("NewExternalIPv6Address", Info.ExternalIPv6Address) And
                   .TryGetValueEx("NewRemoteAccessDDNSEnabled", Info.RemoteAccessDDNSEnabled) And
                   .TryGetValueEx("NewRemoteAccessDDNSDomain", Info.RemoteAccessDDNSDomain) And
                   .TryGetValueEx("NewMyFritzEnabled", Info.MyFritzEnabled) And
                   .TryGetValueEx("NewMyFritzDynDNSName", Info.MyFritzDynDNSName)
        End With
    End Function

    Public Function GetConfig(ByRef Rights As ConfigRights) As Boolean Implements IX_appsetupSCPD.GetConfig
        If Rights Is Nothing Then Rights = New ConfigRights

        With TR064Start(ServiceFile, "GetConfig", ServiceID, Nothing)

            Return .TryGetValueEx("NewConfigRight", Rights.ConfigRight) And
                   .TryGetValueEx("NewAppRight", Rights.AppRight) And
                   .TryGetValueEx("NewNasRight", Rights.NasRight) And
                   .TryGetValueEx("NewPhoneRight", Rights.PhoneRight) And
                   .TryGetValueEx("NewDialRight", Rights.DialRight) And
                   .TryGetValueEx("NewHomeautoRight", Rights.HomeautoRight) And
                   .TryGetValueEx("NewInternetRights", Rights.InternetRights) And
                   .TryGetValueEx("NewAccessFromInternet", Rights.AccessFromInternet)
        End With
    End Function

    Public Function GetInfo(ByRef Info As AppInfo) As Boolean Implements IX_appsetupSCPD.GetInfo
        If Info Is Nothing Then Info = New AppInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewMinCharsAppId", Info.MinCharsAppId) And
                   .TryGetValueEx("NewMaxCharsAppId", Info.MaxCharsAppId) And
                   .TryGetValueEx("NewAllowedCharsAppId", Info.AllowedCharsAppId) And
                   .TryGetValueEx("NewMinCharsAppDisplayName", Info.MinCharsAppDisplayName) And
                   .TryGetValueEx("NewMaxCharsAppDisplayName", Info.MaxCharsAppDisplayName) And
                   .TryGetValueEx("NewMinCharsAppUsername", Info.MinCharsAppUsername) And
                   .TryGetValueEx("NewMaxCharsAppUsername", Info.MaxCharsAppUsername) And
                   .TryGetValueEx("NewAllowedCharsAppUsername", Info.AllowedCharsAppUsername) And
                   .TryGetValueEx("NewMinCharsAppPassword", Info.MinCharsAppPassword) And
                   .TryGetValueEx("NewMaxCharsAppPassword", Info.MaxCharsAppPassword) And
                   .TryGetValueEx("NewAllowedCharsAppPassword", Info.AllowedCharsAppPassword) And
                   .TryGetValueEx("NewMinCharsIPSecIdentifier", Info.MinCharsIPSecIdentifier) And
                   .TryGetValueEx("NewMaxCharsIPSecIdentifier", Info.MaxCharsIPSecIdentifier) And
                   .TryGetValueEx("NewAllowedCharsIPSecIdentifier", Info.AllowedCharsIPSecIdentifier) And
                   .TryGetValueEx("NewMinCharsIPSecPreSharedKey", Info.MinCharsIPSecPreSharedKey) And
                   .TryGetValueEx("NewMaxCharsIPSecPreSharedKey", Info.MaxCharsIPSecPreSharedKey) And
                   .TryGetValueEx("NewAllowedCharsIPSecPreSharedKey", Info.AllowedCharsIPSecPreSharedKey) And
                   .TryGetValueEx("NewMinCharsIPSecXauthUsername", Info.MinCharsIPSecXauthUsername) And
                   .TryGetValueEx("NewMaxCharsIPSecXauthUsername", Info.MaxCharsIPSecXauthUsername) And
                   .TryGetValueEx("NewAllowedCharsIPSecXauthUsername", Info.AllowedCharsIPSecXauthUsername) And
                   .TryGetValueEx("NewMinCharsIPSecXauthPassword", Info.MinCharsIPSecXauthPassword) And
                   .TryGetValueEx("NewMaxCharsIPSecXauthPassword", Info.MaxCharsIPSecXauthPassword) And
                   .TryGetValueEx("NewAllowedCharsIPSecXauthPassword", Info.AllowedCharsIPSecXauthPassword) And
                   .TryGetValueEx("NewAllowedCharsCryptAlgos", Info.AllowedCharsCryptAlgos) And
                   .TryGetValueEx("NewAllowedCharsAppAVMAddress", Info.AllowedCharsAppAVMAddress) And
                   .TryGetValueEx("NewMinCharsFilter", Info.MinCharsFilter) And
                   .TryGetValueEx("NewMaxCharsFilter", Info.MaxCharsFilter) And
                   .TryGetValueEx("NewAllowedCharsFilter", Info.AllowedCharsFilter)
        End With
    End Function

    Public Function RegisterApp(App As AppData) As Boolean Implements IX_appsetupSCPD.RegisterApp
        Return App IsNot Nothing AndAlso
            Not TR064Start(ServiceFile, "RegisterApp", ServiceID,
                           New Dictionary(Of String, String) From {{"NewAppId", App.AppId},
                                                                   {"NewAppDisplayName", App.AppDisplayName},
                                                                   {"NewAppDeviceMAC", App.AppDeviceMAC},
                                                                   {"NewAppUsername", App.AppUsername},
                                                                   {"NewAppPassword", App.AppPassword},
                                                                   {"NewAppRight", App.AppRight.ToString},
                                                                   {"NewNasRight", App.NasRight.ToString},
                                                                   {"NewPhoneRight", App.PhoneRight.ToString},
                                                                   {"NewHomeautoRight", App.HomeautoRight.ToString},
                                                                   {"NewAppInternetRights", App.AppInternetRights.ToBoolStr}}).ContainsKey("Error")

    End Function

    Public Function ResetEvent(EventId As Integer) As Boolean Implements IX_appsetupSCPD.ResetEvent
        Return Not TR064Start(ServiceFile, "ResetEvent", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEventId", EventId.ToString}}).ContainsKey("Error")
    End Function

    Public Function SetAppMessageFilter(AppId As String, Type As String, Filter As String) As Boolean Implements IX_appsetupSCPD.SetAppMessageFilter
        Return Not TR064Start(ServiceFile, "SetAppMessageFilter", ServiceID,
                              New Dictionary(Of String, String) From {{"NewAppId", AppId},
                                                                      {"NewType", Type},
                                                                      {"NewFilter", Filter}}).ContainsKey("Error")
    End Function

    Public Function SetAppMessageReceiver(AppId As String, CryptAlgos As String, AppAVMAddress As String, AppAVMPasswordHash As String, ByRef EncryptionSecret As String, ByRef BoxSenderId As String) As Boolean Implements IX_appsetupSCPD.SetAppMessageReceiver
        With TR064Start(ServiceFile, "SetAppMessageReceiver", ServiceID,
                        New Dictionary(Of String, String) From {{"NewAppId", AppId},
                                                                {"NewCryptAlgos", CryptAlgos},
                                                                {"NewAppAVMAddress", AppAVMAddress},
                                                                {"NewAppAVMPasswordHash", AppAVMPasswordHash}})

            Return .TryGetValueEx("NewEncryptionSecret", EncryptionSecret) And
                   .TryGetValueEx("NewBoxSenderId", BoxSenderId)
        End With
    End Function

    Public Function SetAppVPN(AppId As String, IPSecIdentifier As String, IPSecPreSharedKey As String, IPSecXauthUsername As String, IPSecXauthPassword As String) As Boolean Implements IX_appsetupSCPD.SetAppVPN
        Return Not TR064Start(ServiceFile, "SetAppVPN", ServiceID,
                              New Dictionary(Of String, String) From {{"NewAppId", AppId},
                                                                      {"NewIPSecIdentifier", IPSecIdentifier},
                                                                      {"NewIPSecPreSharedKey", IPSecPreSharedKey},
                                                                      {"NewIPSecXauthUsername", IPSecXauthUsername},
                                                                      {"NewIPSecXauthPassword", IPSecXauthPassword}}).ContainsKey("Error")
    End Function

    Public Function SetAppVPNwithPFS(AppId As String, IPSecIdentifier As String, IPSecPreSharedKey As String, IPSecXauthUsername As String, IPSecXauthPassword As String) As Boolean Implements IX_appsetupSCPD.SetAppVPNwithPFS
        Return Not TR064Start(ServiceFile, "SetAppVPNwithPFS", ServiceID,
                              New Dictionary(Of String, String) From {{"NewAppId", AppId},
                                                                      {"NewIPSecIdentifier", IPSecIdentifier},
                                                                      {"NewIPSecPreSharedKey", IPSecPreSharedKey},
                                                                      {"NewIPSecXauthUsername", IPSecXauthUsername},
                                                                      {"NewIPSecXauthPassword", IPSecXauthPassword}}).ContainsKey("Error")
    End Function

    Public Function GetBoxSenderId(AppId As String, ByRef BoxSenderId As String) As Boolean Implements IX_appsetupSCPD.GetBoxSenderId
        Return TR064Start(ServiceFile, "GetBoxSenderId", ServiceID,
                          New Dictionary(Of String, String) From {{"NewAppId", AppId}}).TryGetValueEx("NewBoxSenderId", BoxSenderId)

    End Function
End Class