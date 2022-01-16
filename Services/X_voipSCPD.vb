''' <summary>
''' TR-064 Support – X_VoIP
''' Date: 2019-08-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf</see>
''' </summary>
Friend Class X_voipSCPD
    Implements IX_voipSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_voipSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_voipSCPD Implements IX_voipSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), XMLSerializer As Serializer)

        TR064Start = Start

        XML = XMLSerializer

    End Sub

#Region "GetInfo"
    Public Function GetInfo(ByRef FaxT38Enable As Boolean, ByRef VoiceCoding As VoiceCodingEnum) As Boolean Implements IX_voipSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValue("NewFaxT38Enable", FaxT38Enable) And
                   .TryGetValue("NewVoiceCoding", VoiceCoding)

        End With
    End Function

    Public Function SetConfig(T38FaxEnable As Boolean, VoiceCoding As VoiceCodingEnum) As Boolean Implements IX_voipSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", New Dictionary(Of String, String) From {{"NewT38FaxEnable", T38FaxEnable.ToBoolStr}, {"NewVoiceCoding", VoiceCoding}}).ContainsKey("Error")
    End Function

    Public Function GetInfoEx(ByRef InfoEx As VoIPInfoEx) As Boolean Implements IX_voipSCPD.GetInfoEx

        If InfoEx Is Nothing Then InfoEx = New VoIPInfoEx

        With TR064Start(ServiceFile, "GetInfoEx", Nothing)

            If .ContainsKey("NewVoIPNumberMinChars") Then

                InfoEx.VoIPNumberMinChars = CInt(.Item("NewVoIPNumberMinChars"))
                InfoEx.VoIPNumberMaxChars = CInt(.Item("NewVoIPNumberMaxChars"))
                InfoEx.VoIPNumberAllowedChars = .Item("NewVoIPNumberAllowedChars")

                InfoEx.VoIPUsernameMinChars = CInt(.Item("NewVoIPUsernameMinChars"))
                InfoEx.VoIPUsernameMaxChars = CInt(.Item("NewVoIPUsernameMaxChars"))
                InfoEx.VoIPUsernameAllowedChars = .Item("NewVoIPUsernameAllowedChars")

                InfoEx.VoIPPasswordMinChars = CInt(.Item("NewVoIPPasswordMinChars"))
                InfoEx.VoIPPasswordMaxChars = CInt(.Item("NewVoIPPasswordMaxChars"))
                InfoEx.VoIPPasswordAllowedChars = .Item("NewVoIPPasswordAllowedChars")

                InfoEx.VoIPRegistrarMinChars = CInt(.Item("NewVoIPRegistrarMinChars"))
                InfoEx.VoIPRegistrarMaxChars = CInt(.Item("NewVoIPRegistrarMaxChars"))
                InfoEx.VoIPRegistrarAllowedChars = .Item("NewVoIPRegistrarAllowedChars")

                InfoEx.VoIPSTUNServerMinChars = CInt(.Item("NewVoIPSTUNServerMinChars"))
                InfoEx.VoIPSTUNServerMaxChars = CInt(.Item("NewVoIPSTUNServerMaxChars"))
                InfoEx.VoIPSTUNServerAllowedChars = .Item("NewVoIPSTUNServerAllowedChars")

                InfoEx.ClientUsernameMinChars = CInt(.Item("NewX_AVM-DE_ClientUsernameMinChars"))
                InfoEx.ClientUsernameMaxChars = CInt(.Item("NewX_AVM-DE_ClientUsernameMaxChars"))
                InfoEx.ClientUsernameAllowedChars = .Item("NewX_AVM-DE_ClientUsernameAllowedChars")

                InfoEx.ClientPasswordMinChars = CInt(.Item("NewX_AVM-DE_ClientPasswordMinChars "))
                InfoEx.ClientPasswordMaxChars = CInt(.Item("NewX_AVM-DE_ClientPasswordMaxChars"))
                InfoEx.ClientPasswordAllowedChars = .Item("NewX_AVM-DE_ClientPasswordAllowedChars")

                Return True

            Else

                Return False
            End If
        End With
    End Function
#End Region

#Region "VoIPNumbers"
    Public Function GetExistingVoIPNumbers(ByRef ExistingVoIPNumbers As Integer) As Boolean Implements IX_voipSCPD.GetExistingVoIPNumbers
        Return TR064Start(ServiceFile, "GetExistingVoIPNumbers", Nothing).TryGetValue("NewExistingVoIPNumbers", ExistingVoIPNumbers)
    End Function

    Public Function GetMaxVoIPNumbers(ByRef MaxVoIPNumbers As Integer) As Boolean Implements IX_voipSCPD.GetMaxVoIPNumbers
        Return TR064Start(ServiceFile, "GetMaxVoIPNumbers", Nothing).TryGetValue("NewMaxVoIPNumbers", MaxVoIPNumbers)
    End Function
#End Region

#Region "AreaCode / CountryCode"
    Public Function GetVoIPEnableAreaCode(ByRef VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPEnableAreaCode
        Return TR064Start(ServiceFile, "GetVoIPEnableAreaCode",
                          New Dictionary(Of String, String) From {{"NewVoIPEnableAreaCode", VoIPAccountIndex}}).
                          TryGetValue("NewVoIPEnableAreaCode", VoIPEnableAreaCode)
    End Function

    Public Function SetVoIPEnableAreaCode(VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.SetVoIPEnableAreaCode
        Return Not TR064Start(ServiceFile, "SetVoIPEnableAreaCode", New Dictionary(Of String, String) From {{"NewVoIPAccountIndex", VoIPAccountIndex},
                                                                                                            {"NewVoIPEnableAreaCode", VoIPEnableAreaCode.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetVoIPEnableCountryCode(ByRef VoIPEnableCountryCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPEnableCountryCode
        Return TR064Start(ServiceFile, "GetVoIPEnableCountryCode",
                          New Dictionary(Of String, String) From {{"NewVoIPAccountIndex", VoIPAccountIndex}}).
                          TryGetValue("NewVoIPEnableCountryCode", VoIPEnableCountryCode)
    End Function

    Public Function SetVoIPCommonCountryCode(LKZ As String, LKZPrefix As String) As Boolean Implements IX_voipSCPD.SetVoIPCommonCountryCode
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonCountryCode", New Dictionary(Of String, String) From {{"NewX_AVM-DE_LKZ", LKZ},
                                                                                                                        {"NewX_AVM-DE_LKZPrefix", LKZPrefix}}).ContainsKey("Error")
    End Function

    Public Function GetVoIPCommonCountryCode(ByRef LKZ As String, Optional ByRef LKZPrefix As String = "") As Boolean Implements IX_voipSCPD.GetVoIPCommonCountryCode

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPCommonCountryCode", Nothing)

            Return .TryGetValue("NewX_AVM-DE_LKZ", LKZ) And
                   .TryGetValue("NewX_AVM-DE_LKZPrefix", LKZPrefix)
        End With

    End Function

    Public Function SetVoIPCommonAreaCode(OKZ As String, OKZPrefix As String) As Boolean Implements IX_voipSCPD.SetVoIPCommonAreaCode
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonAreaCode", New Dictionary(Of String, String) From {{"NewX_AVM-DE_OKZ", OKZ},
                                                                                                                     {"NewX_AVM-DE_OKZPrefix", OKZPrefix}}).ContainsKey("Error")
    End Function

    Public Function GetVoIPCommonAreaCode(ByRef OKZ As String, Optional ByRef OKZPrefix As String = "") As Boolean Implements IX_voipSCPD.GetVoIPCommonAreaCode

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPCommonAreaCode", Nothing)

            Return .TryGetValue("NewX_AVM-DE_OKZ", OKZ) And
                   .TryGetValue("NewX_AVM-DE_OKZPrefix", OKZPrefix)

        End With

    End Function
#End Region

#Region "VoIP Account"
    Public Function AddVoIPAccount(VoIPAccountIndex As Integer,
                                   VoIPRegistrar As String,
                                   VoIPNumber As String,
                                   VoIPUsername As String,
                                   VoIPPassword As String,
                                   VoIPOutboundProxy As String,
                                   VoIPSTUNServer As String) As Boolean Implements IX_voipSCPD.AddVoIPAccount


        Return Not TR064Start(ServiceFile, "X_AVM-DE_AddVoIPAccount", New Dictionary(Of String, String) From {{"NewVoIPAccountIndex", VoIPAccountIndex},
                                                                                                              {"NewVoIPRegistrar", VoIPRegistrar},
                                                                                                              {"NewVoIPNumber", VoIPNumber},
                                                                                                              {"NewVoIPUsername", VoIPUsername},
                                                                                                              {"NewVoIPPassword", VoIPPassword},
                                                                                                              {"NewVoIPOutboundProxy", VoIPOutboundProxy},
                                                                                                              {"NewVoIPSTUNServer", VoIPSTUNServer}}).ContainsKey("Error")
    End Function

    Public Function DelVoIPAccount(VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.DelVoIPAccount
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DelVoIPAccount", New Dictionary(Of String, String) From {{"NewVoIPAccountIndex ", VoIPAccountIndex}}).ContainsKey("Error")
    End Function

    Public Function GetVoIPAccount(ByRef Account As VoIPAccount, AccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPAccount
        If Account Is Nothing Then Account = New VoIPAccount

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPAccount", New Dictionary(Of String, String) From {{"NewVoIPAccountIndex", AccountIndex}})

            Account.VoIPAccountIndex = AccountIndex

            Return .TryGetValue("NewVoIPRegistrar", Account.VoIPRegistrar) And
                   .TryGetValue("NewVoIPNumber", Account.VoIPNumber) And
                   .TryGetValue("NewVoIPUsername", Account.VoIPUsername) And
                   .TryGetValue("NewVoIPOutboundProxy", Account.VoIPOutboundProxy) And
                   .TryGetValue("NewVoIPOutboundProxy", Account.VoIPSTUNServer)

        End With
    End Function
#End Region

#Region "Client"
    Public Function DeleteClient(ClientIndex As Integer) As Boolean Implements IX_voipSCPD.DeleteClient
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DeleteClient", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex}}).ContainsKey("Error")
    End Function

    Public Function GetNumberOfClients(ByRef NumberOfClients As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfClients
        Return TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfClients", Nothing).TryGetValue("NewX_AVM-DE_NumberOfClients", NumberOfClients)
    End Function

    Public Function GetClient2(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean Implements IX_voipSCPD.GetClient2
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClient2", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})

            Client.ClientIndex = ClientIndex

            Return .TryGetValue("NewX_AVM-DE_ClientUsername", Client.ClientUsername) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrar", Client.ClientRegistrar) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrarPort", Client.ClientRegistrarPort) And
                   .TryGetValue("NewX_AVM-DE_PhoneName", Client.PhoneName) And
                   .TryGetValue("NewX_AVM-DE_ClientId", Client.ClientId) And
                   .TryGetValue("NewX_AVM-DE_OutGoingNumber", Client.OutGoingNumber) And
                   .TryGetValue("NewX_AVM-DE_InternalNumber", Client.InternalNumber)

        End With
    End Function

    Public Function GetClient3(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean Implements IX_voipSCPD.GetClient3
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClient3", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})

            Client.ClientIndex = ClientIndex

            Return .TryGetValue("NewX_AVM-DE_ClientUsername", Client.ClientUsername) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrar", Client.ClientRegistrar) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrarPort", Client.ClientRegistrarPort) And
                   .TryGetValue("NewX_AVM-DE_PhoneName", Client.PhoneName) And
                   .TryGetValue("NewX_AVM-DE_ClientId", Client.ClientId) And
                   .TryGetValue("NewX_AVM-DE_OutGoingNumber", Client.OutGoingNumber) And
                   .TryGetValue("NewX_AVM-DE_ExternalRegistration", Client.ExternalRegistration) And
                   .TryGetValue("NewX_AVM-DE_InternalNumber", Client.InternalNumber) And
                   .TryGetValue("NewX_AVM-DE_DelayedCallNotification", Client.DelayedCallNotification) And
                   XML.Deserialize(.Item("NewX_AVM-DE_InComingNumbers"), False, Client.InComingNumbers)

        End With
    End Function

    Public Function GetClientByClientId(ByRef Client As SIPClient, ClientId As String) As Boolean Implements IX_voipSCPD.GetClientByClientId
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClientByClientId", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientId", ClientId}})

            Return .TryGetValue("NewX_AVM-DE_ClientIndex", Client.ClientIndex) And
                   .TryGetValue("NewX_AVM-DE_ClientUsername", Client.ClientUsername) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrar", Client.ClientRegistrar) And
                   .TryGetValue("NewX_AVM-DE_ClientRegistrarPort", Client.ClientRegistrarPort) And
                   .TryGetValue("NewX_AVM-DE_PhoneName", Client.PhoneName) And
                   .TryGetValue("NewX_AVM-DE_ClientId", Client.ClientId) And
                   .TryGetValue("NewX_AVM-DE_OutGoingNumber", Client.OutGoingNumber) And
                   .TryGetValue("NewX_AVM-DE_ExternalRegistration", Client.ExternalRegistration) And
                   .TryGetValue("NewX_AVM-DE_InternalNumber", Client.InternalNumber) And
                   .TryGetValue("NewX_AVM-DE_DelayedCallNotification", Client.DelayedCallNotification) And
                   XML.Deserialize(.Item("NewX_AVM-DE_InComingNumbers"), False, Client.InComingNumbers)

        End With
    End Function

    Public Function GetClients(ByRef ClientList As SIPClientList) As Boolean Implements IX_voipSCPD.GetClients

        With TR064Start(ServiceFile, "X_AVM-DE_GetClients", Nothing)

            If .ContainsKey("NewX_AVM-DE_ClientList") Then

                XML.Deserialize(.Item("NewX_AVM-DE_ClientList"), False, ClientList)

                ' Wenn keine SIP-Clients angeschlossen wurden, gib eine leere Klasse zurück
                If ClientList Is Nothing Then ClientList = New SIPClientList

                Return True

            Else
                ClientList = Nothing

                Return False
            End If
        End With

    End Function

    Public Function SetClient2(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String) As Boolean Implements IX_voipSCPD.SetClient2

        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetClient2", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                          {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                                          {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                                          {"NewX_AVM-DE_ClientId", ClientId},
                                                                                                          {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber}}).ContainsKey("Error")
    End Function

    Public Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               ExternalRegistration As String) As Boolean Implements IX_voipSCPD.SetClient3

        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetClient3", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                          {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                                          {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                                          {"NewX_AVM-DE_ClientId", ClientId},
                                                                                                          {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber},
                                                                                                          {"NewX_AVM-DE_InComingNumbers", InComingNumbers},
                                                                                                          {"NewX_AVM-DE_ExternalRegistration", ExternalRegistration}}).ContainsKey("Error")
    End Function

    Public Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               ExternalRegistration As String) As Boolean Implements IX_voipSCPD.SetClient3

        Dim XMLString As String = String.Empty
        If XML.SerializeToString(InComingNumbers, XMLString) Then
            Return SetClient3(ClientIndex, ClientPassword, PhoneName, ClientId, OutGoingNumber, XMLString, ExternalRegistration)
        Else
            Return False
        End If

    End Function

    Public Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               InternalNumber As String) As Boolean Implements IX_voipSCPD.SetClient4
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetClient4", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                          {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                                          {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                                          {"NewX_AVM-DE_ClientId", ClientId},
                                                                                                          {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber},
                                                                                                          {"NewX_AVM-DE_InComingNumbers", InComingNumbers},
                                                                                                          {"NewX_AVM-DE_InternalNumber", InternalNumber}}).ContainsKey("Error")
    End Function

    Public Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               InternalNumber As String) As Boolean Implements IX_voipSCPD.SetClient4


        Dim XMLString As String = String.Empty
        If XML.SerializeToString(InComingNumbers, XMLString) Then
            Return SetClient4(ClientIndex, ClientPassword, PhoneName, ClientId, OutGoingNumber, XMLString, InternalNumber)
        Else
            Return False
        End If

    End Function

    Public Function SetDelayedCallNotification(ClientIndex As Integer, DelayedCallNotification As Boolean) As Boolean Implements IX_voipSCPD.SetDelayedCallNotification
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetDelayedCallNotification", New Dictionary(Of String, String) From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                                          {"NewX_AVM-DE_DelayedCallNotification", DelayedCallNotification.ToBoolStr}}).ContainsKey("Error")
    End Function
#End Region

#Region "Numbers"
    Public Function GetNumberOfNumbers(ByRef NumberOfNumbers As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfNumbers
        Return TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfNumbers", Nothing).TryGetValue("NewNumberOfNumbers", NumberOfNumbers)
    End Function

    Public Function GetNumbers(ByRef NumberList As SIPTelNrList) As Boolean Implements IX_voipSCPD.GetNumbers

        With TR064Start(ServiceFile, "X_AVM-DE_GetNumbers", Nothing)

            If .ContainsKey("NewNumberList") Then

                XML.Deserialize(.Item("NewNumberList"), False, NumberList)

                ' Wenn keine Nummern angeschlossen wurden, gib eine leere Klasse zurück
                If NumberList Is Nothing Then NumberList = New SIPTelNrList

                Return True

            Else
                NumberList = Nothing

                Return False
            End If
        End With

    End Function
#End Region

#Region "Dialing"
    Public Function GetPhonePort(ByRef PhoneName As String, i As Integer) As Boolean Implements IX_voipSCPD.GetPhonePort
        Return TR064Start(ServiceFile, "X_AVM-DE_GetPhonePort", New Dictionary(Of String, String) From {{"NewIndex", i}}).TryGetValue("NewX_AVM-DE_PhoneName", PhoneName)
    End Function

    Public Function DialGetConfig(ByRef PhoneName As String) As Boolean Implements IX_voipSCPD.DialGetConfig
        Return TR064Start(ServiceFile, "X_AVM-DE_DialGetConfig", Nothing).TryGetValue("NewX_AVM-DE_PhoneName", PhoneName)
    End Function

    Public Function DialHangup() As Boolean Implements IX_voipSCPD.DialHangup
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DialHangup", Nothing).ContainsKey("Error")
    End Function

    Public Function DialNumber(PhoneNumber As String) As Boolean Implements IX_voipSCPD.DialNumber
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DialNumber", New Dictionary(Of String, String) From {{"NewX_AVM-DE_PhoneNumber", PhoneNumber}}).ContainsKey("Error")
    End Function

    Public Function DialSetConfig(PhoneName As String) As Boolean Implements IX_voipSCPD.DialSetConfig
        Return Not TR064Start(ServiceFile, "X_AVM-DE_DialSetConfig", New Dictionary(Of String, String) From {{"NewX_AVM-DE_PhoneName", PhoneName}}).ContainsKey("Error")
    End Function
#End Region

#Region "AlarmClock"
    Public Function GetAlarmClock(ByRef AlarmClock As AlarmClock, Index As Integer) As Boolean Implements IX_voipSCPD.GetAlarmClock
        If AlarmClock Is Nothing Then AlarmClock = New AlarmClock

        With TR064Start(ServiceFile, "X_AVM-DE_GetAlarmClock", New Dictionary(Of String, String) From {{"NewIndex", Index}})

            If .ContainsKey("NewX_AVM-DE_AlarmClockWeekdays") Then

                AlarmClock.AlarmClockWeekdays = .Item("NewX_AVM-DE_AlarmClockWeekdays").Split(",")

                Return .TryGetValue("NewX_AVM-DE_AlarmClockEnable", AlarmClock.AlarmClockEnable) And
                       .TryGetValue("NewX_AVM-DE_AlarmClockName", AlarmClock.AlarmClockName) And
                       .TryGetValue("NewX_AVM-DE_AlarmClockTime", AlarmClock.AlarmClockTime) And
                       .TryGetValue("NewX_AVM-DE_AlarmClockPhoneName", AlarmClock.AlarmClockPhoneName)

            Else

                Return False
            End If
        End With
    End Function

    Public Function GetNumberOfAlarmClocks(ByRef NumberOfAlarmClocks As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfAlarmClocks
        Return TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfAlarmClocks", Nothing).TryGetValue("NewX_AVM-DE_NumberOfAlarmClocks", NumberOfAlarmClocks)
    End Function

    Public Function SetAlarmClockEnable(Index As Integer, AlarmClockEnable As Boolean) As Boolean Implements IX_voipSCPD.SetAlarmClockEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetAlarmClockEnable", New Dictionary(Of String, String) From {{"NewIndex", Index},
                                                                                                                   {"NewX_AVM-DE_AlarmClockEnable", AlarmClockEnable.ToBoolStr}}).ContainsKey("Error")
    End Function
#End Region
End Class
