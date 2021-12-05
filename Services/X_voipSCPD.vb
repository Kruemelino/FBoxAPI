''' <summary>
''' TR-064 Support – X_VoIP
''' Date: 2019-08-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf</see>
''' </summary>
Friend Class X_voipSCPD
    Implements IX_voipSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_voipSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_voipSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_voipSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String), XMLSerializer As Serializer)
        ServiceFile = SCPDFiles.x_voipSCPD

        TR064Start = Start

        PushStatus = Status

        XML = XMLSerializer
    End Sub

#Region "GetInfo"
    Public Function GetInfo(ByRef FaxT38Enable As Boolean, ByRef VoiceCoding As VoiceCoding) As Boolean Implements IX_voipSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewFaxT38Enable") And .ContainsKey("NewVoiceCoding") Then
                FaxT38Enable = CBool(.Item("NewFaxT38Enable"))
                VoiceCoding = CType(.Item("NewVoiceCoding"), VoiceCoding)

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetConfig(T38FaxEnable As Boolean, VoiceCoding As VoiceCoding) As Boolean Implements IX_voipSCPD.SetConfig
        With TR064Start(ServiceFile, "SetConfig", New Hashtable From {{"NewT38FaxEnable", T38FaxEnable}, {"NewVoiceCoding", VoiceCoding}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetInfoEx(ByRef InfoEx As VoIPInfoEx) As Boolean Implements IX_voipSCPD.GetInfoEx

        If InfoEx Is Nothing Then InfoEx = New VoIPInfoEx

        With TR064Start(ServiceFile, "GetInfoEx", Nothing)

            If .ContainsKey("NewVoIPNumberMinChars") Then

                InfoEx.VoIPNumberMinChars = CInt(.Item("NewVoIPNumberMinChars"))
                InfoEx.VoIPNumberMaxChars = CInt(.Item("NewVoIPNumberMaxChars"))
                InfoEx.VoIPNumberAllowedChars = CStr(.Item("NewVoIPNumberAllowedChars"))

                InfoEx.VoIPUsernameMinChars = CInt(.Item("NewVoIPUsernameMinChars"))
                InfoEx.VoIPUsernameMaxChars = CInt(.Item("NewVoIPUsernameMaxChars"))
                InfoEx.VoIPUsernameAllowedChars = CStr(.Item("NewVoIPUsernameAllowedChars"))

                InfoEx.VoIPPasswordMinChars = CInt(.Item("NewVoIPPasswordMinChars"))
                InfoEx.VoIPPasswordMaxChars = CInt(.Item("NewVoIPPasswordMaxChars"))
                InfoEx.VoIPPasswordAllowedChars = CStr(.Item("NewVoIPPasswordAllowedChars"))

                InfoEx.VoIPRegistrarMinChars = CInt(.Item("NewVoIPRegistrarMinChars"))
                InfoEx.VoIPRegistrarMaxChars = CInt(.Item("NewVoIPRegistrarMaxChars"))
                InfoEx.VoIPRegistrarAllowedChars = CStr(.Item("NewVoIPRegistrarAllowedChars"))

                InfoEx.VoIPSTUNServerMinChars = CInt(.Item("NewVoIPSTUNServerMinChars"))
                InfoEx.VoIPSTUNServerMaxChars = CInt(.Item("NewVoIPSTUNServerMaxChars"))
                InfoEx.VoIPSTUNServerAllowedChars = CStr(.Item("NewVoIPSTUNServerAllowedChars"))

                InfoEx.ClientUsernameMinChars = CInt(.Item("NewX_AVM-DE_ClientUsernameMinChars"))
                InfoEx.ClientUsernameMaxChars = CInt(.Item("NewX_AVM-DE_ClientUsernameMaxChars"))
                InfoEx.ClientUsernameAllowedChars = CStr(.Item("NewX_AVM-DE_ClientUsernameAllowedChars"))

                InfoEx.ClientPasswordMinChars = CInt(.Item("NewX_AVM-DE_ClientPasswordMinChars "))
                InfoEx.ClientPasswordMaxChars = CInt(.Item("NewX_AVM-DE_ClientPasswordMaxChars"))
                InfoEx.ClientPasswordAllowedChars = CStr(.Item("NewX_AVM-DE_ClientPasswordAllowedChars"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfoEx konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
#End Region

#Region "VoIPNumbers"
    Public Function GetExistingVoIPNumbers(ByRef ExistingVoIPNumbers As Integer) As Boolean Implements IX_voipSCPD.GetExistingVoIPNumbers
        With TR064Start(ServiceFile, "GetExistingVoIPNumbers", Nothing)

            If .ContainsKey("NewExistingVoIPNumbers") Then
                ExistingVoIPNumbers = CInt(.Item("NewExistingVoIPNumbers"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetExistingVoIPNumbers konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                ExistingVoIPNumbers = 0

                Return False
            End If
        End With
    End Function

    Public Function GetMaxVoIPNumbers(ByRef MaxVoIPNumbers As Integer) As Boolean Implements IX_voipSCPD.GetMaxVoIPNumbers
        With TR064Start(ServiceFile, "GetMaxVoIPNumbers", Nothing)

            If .ContainsKey("NewMaxVoIPNumbers") Then
                MaxVoIPNumbers = CInt(.Item("NewMaxVoIPNumbers"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetMaxVoIPNumbers konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                MaxVoIPNumbers = 0

                Return False
            End If
        End With
    End Function
#End Region

#Region "AreaCode / CountryCode"
    Public Function GetVoIPEnableAreaCode(ByRef VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPEnableAreaCode
        With TR064Start(ServiceFile, "GetVoIPEnableAreaCode", New Hashtable From {{"NewVoIPAccountIndex", VoIPAccountIndex}})

            If .ContainsKey("NewVoIPEnableAreaCode") Then
                VoIPEnableAreaCode = CBool(.Item("NewVoIPEnableAreaCode"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetVoIPEnableAreaCode konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                VoIPEnableAreaCode = False

                Return False
            End If
        End With
    End Function

    Public Function SetVoIPEnableAreaCode(VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.SetVoIPEnableAreaCode
        With TR064Start(ServiceFile, "SetVoIPEnableAreaCode", New Hashtable From {{"NewVoIPAccountIndex", VoIPAccountIndex},
                                                                                  {"NewVoIPEnableAreaCode", VoIPEnableAreaCode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPEnableCountryCode(ByRef VoIPEnableCountryCode As Boolean, VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPEnableCountryCode
        With TR064Start(ServiceFile, "GetVoIPEnableCountryCode", New Hashtable From {{"NewVoIPAccountIndex", VoIPAccountIndex}})

            If .ContainsKey("NewVoIPEnableCountryCode") Then
                VoIPEnableCountryCode = CBool(.Item("NewVoIPEnableCountryCode"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetVoIPEnableCountryCode konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                VoIPEnableCountryCode = False

                Return False
            End If
        End With
    End Function

    Public Function SetVoIPCommonCountryCode(LKZ As String, LKZPrefix As String) As Boolean Implements IX_voipSCPD.SetVoIPCommonCountryCode
        With TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonCountryCode", New Hashtable From {{"NewX_AVM-DE_LKZ", LKZ},
                                                                                           {"NewX_AVM-DE_LKZPrefix", LKZPrefix}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPCommonCountryCode(ByRef LKZ As String, Optional ByRef LKZPrefix As String = "") As Boolean Implements IX_voipSCPD.GetVoIPCommonCountryCode

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPCommonCountryCode", Nothing)

            If .ContainsKey("NewX_AVM-DE_LKZ") And .ContainsKey("NewX_AVM-DE_LKZPrefix") Then
                LKZ = .Item("NewX_AVM-DE_LKZ").ToString
                LKZPrefix = .Item("NewX_AVM-DE_LKZPrefix").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"LKZ und LKZPrefix konnten nicht ermittelt werden. '{ .Item("Error")}'")
                LKZ = If(LKZ.IsStringNothingOrEmpty, String.Empty, LKZ)
                LKZPrefix = If(LKZPrefix.IsStringNothingOrEmpty, String.Empty, LKZPrefix)

                Return False
            End If
        End With

    End Function

    Public Function SetVoIPCommonAreaCode(OKZ As String, OKZPrefix As String) As Boolean Implements IX_voipSCPD.SetVoIPCommonAreaCode
        With TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonAreaCode", New Hashtable From {{"NewX_AVM-DE_OKZ", OKZ},
                                                                                           {"NewX_AVM-DE_OKZPrefix", OKZPrefix}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPCommonAreaCode(ByRef OKZ As String, Optional ByRef OKZPrefix As String = "") As Boolean Implements IX_voipSCPD.GetVoIPCommonAreaCode

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPCommonAreaCode", Nothing)

            If .ContainsKey("NewX_AVM-DE_OKZ") And .ContainsKey("NewX_AVM-DE_OKZPrefix") Then
                OKZ = .Item("NewX_AVM-DE_OKZ").ToString
                OKZPrefix = .Item("NewX_AVM-DE_OKZPrefix").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"OKZ und OKZPrefix konnten nicht ermittelt werden. '{ .Item("Error")}'")
                OKZ = If(OKZ.IsStringNothingOrEmpty, String.Empty, OKZ)
                OKZPrefix = If(OKZPrefix.IsStringNothingOrEmpty, String.Empty, OKZPrefix)

                Return False
            End If
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

        With TR064Start(ServiceFile, "X_AVM-DE_AddVoIPAccount", New Hashtable From {{"NewVoIPAccountIndex", VoIPAccountIndex},
                                                                                    {"NewVoIPRegistrar", VoIPRegistrar},
                                                                                    {"NewVoIPNumber", VoIPNumber},
                                                                                    {"NewVoIPUsername", VoIPUsername},
                                                                                    {"NewVoIPPassword", VoIPPassword},
                                                                                    {"NewVoIPOutboundProxy", VoIPOutboundProxy},
                                                                                    {"NewVoIPSTUNServer", VoIPSTUNServer}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DelVoIPAccount(VoIPAccountIndex As Integer) As Boolean Implements IX_voipSCPD.DelVoIPAccount

        With TR064Start(ServiceFile, "X_AVM-DE_DelVoIPAccount", New Hashtable From {{"NewVoIPAccountIndex ", VoIPAccountIndex}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPAccount(ByRef Account As VoIPAccount, AccountIndex As Integer) As Boolean Implements IX_voipSCPD.GetVoIPAccount
        If Account Is Nothing Then Account = New VoIPAccount

        With TR064Start(ServiceFile, "X_AVM-DE_GetVoIPAccount", New Hashtable From {{"NewVoIPAccountIndex", AccountIndex}})

            If .ContainsKey("NewVoIPRegistrar") Then
                Account.VoIPAccountIndex = AccountIndex
                Account.VoIPRegistrar = .Item("NewVoIPRegistrar").ToString
                Account.VoIPNumber = .Item("NewVoIPNumber").ToString
                Account.VoIPUsername = .Item("NewVoIPUsername").ToString
                Account.VoIPOutboundProxy = .Item("NewVoIPOutboundProxy").ToString
                Account.VoIPSTUNServer = .Item("NewVoIPSTUNServer").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetVoIPAccount konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
#End Region

#Region "Client"
    Public Function DeleteClient(ClientIndex As Integer) As Boolean Implements IX_voipSCPD.DeleteClient
        With TR064Start(ServiceFile, "X_AVM-DE_DeleteClient", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetNumberOfClients(ByRef NumberOfClients As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfClients
        With TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfClients", Nothing)

            If .ContainsKey("NewX_AVM-DE_NumberOfClients") Then
                NumberOfClients = CInt(.Item("NewX_AVM-DE_NumberOfClients"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfClients konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                NumberOfClients = 0

                Return False
            End If
        End With
    End Function

    Public Function GetClient2(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean Implements IX_voipSCPD.GetClient2
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClient2", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})

            If .ContainsKey("NewX_AVM-DE_ClientUsername") Then
                Client.ClientIndex = ClientIndex
                Client.ClientUsername = .Item("NewX_AVM-DE_ClientUsername").ToString
                Client.ClientRegistrar = .Item("NewX_AVM-DE_ClientRegistrar").ToString
                Client.ClientRegistrarPort = CInt(.Item("NewX_AVM-DE_ClientRegistrarPort"))
                Client.PhoneName = .Item("NewX_AVM-DE_PhoneName").ToString
                Client.ClientId = .Item("NewX_AVM-DE_ClientId").ToString
                Client.OutGoingNumber = .Item("NewX_AVM-DE_OutGoingNumber").ToString
                Client.InternalNumber = CInt(.Item("NewX_AVM-DE_InternalNumber"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetClient2 konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetClient3(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean Implements IX_voipSCPD.GetClient3
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClient3", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})

            If .ContainsKey("NewX_AVM-DE_ClientUsername") Then
                Client.ClientIndex = ClientIndex
                Client.ClientUsername = .Item("NewX_AVM-DE_ClientUsername").ToString
                Client.ClientRegistrar = .Item("NewX_AVM-DE_ClientRegistrar").ToString
                Client.ClientRegistrarPort = CInt(.Item("NewX_AVM-DE_ClientRegistrarPort"))
                Client.PhoneName = .Item("NewX_AVM-DE_PhoneName").ToString
                Client.ClientId = .Item("NewX_AVM-DE_ClientId").ToString
                Client.OutGoingNumber = .Item("NewX_AVM-DE_OutGoingNumber").ToString
                If Not XML.Deserialize(.Item("NewX_AVM-DE_InComingNumbers").ToString(), False, Client.InComingNumbers) Then
                    PushStatus.Invoke(LogLevel.Warn, $"NewX_AVM-DE_InComingNumbers konnte für nicht deserialisiert werden. '{ .Item("Error")}'")
                End If
                Client.ExternalRegistration = CBool(.Item("NewX_AVM-DE_ExternalRegistration"))
                Client.InternalNumber = CInt(.Item("NewX_AVM-DE_InternalNumber"))
                Client.DelayedCallNotification = CBool(.Item("NewX_AVM-DE_DelayedCallNotification"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetClient3 konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetClientByClientId(ByRef Client As SIPClient, ClientId As String) As Boolean Implements IX_voipSCPD.GetClientByClientId
        If Client Is Nothing Then Client = New SIPClient

        With TR064Start(ServiceFile, "X_AVM-DE_GetClientByClientId", New Hashtable From {{"NewX_AVM-DE_ClientId", ClientId}})

            If .ContainsKey("NewX_AVM-DE_ClientIndex") Then
                Client.ClientIndex = CInt(.Item("NewX_AVM-DE_ClientIndex"))
                Client.ClientUsername = .Item("NewX_AVM-DE_ClientUsername").ToString
                Client.ClientRegistrar = .Item("NewX_AVM-DE_ClientRegistrar").ToString
                Client.ClientRegistrarPort = CInt(.Item("NewX_AVM-DE_ClientRegistrarPort"))
                Client.PhoneName = .Item("NewX_AVM-DE_PhoneName").ToString
                Client.ClientId = .Item("NewX_AVM-DE_ClientId").ToString
                Client.OutGoingNumber = .Item("NewX_AVM-DE_OutGoingNumber").ToString
                If Not XML.Deserialize(.Item("NewX_AVM-DE_InComingNumbers").ToString(), False, Client.InComingNumbers) Then
                    PushStatus.Invoke(LogLevel.Warn, $"NewX_AVM-DE_InComingNumbers konnte für nicht deserialisiert werden. '{ .Item("Error")}'")
                End If
                Client.ExternalRegistration = CBool(.Item("NewX_AVM-DE_ExternalRegistration"))
                Client.InternalNumber = CInt(.Item("NewX_AVM-DE_InternalNumber"))
                Client.DelayedCallNotification = CBool(.Item("NewX_AVM-DE_DelayedCallNotification"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetClient3 konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetClients(ByRef ClientList As SIPClientList) As Boolean Implements IX_voipSCPD.GetClients

        With TR064Start(ServiceFile, "X_AVM-DE_GetClients", Nothing)

            If .ContainsKey("NewX_AVM-DE_ClientList") Then

                If Not XML.Deserialize(.Item("NewX_AVM-DE_ClientList").ToString(), False, ClientList) Then
                    PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_ClientList konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine SIP-Clients angeschlossen wurden, gib eine leere Klasse zurück
                If ClientList Is Nothing Then ClientList = New SIPClientList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetClients konnte für nicht aufgelößt werden. '{ .Item("Error")}'")
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

        With TR064Start(ServiceFile, "X_AVM-DE_SetClient2", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                {"NewX_AVM-DE_ClientId", ClientId},
                                                                                {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               ExternalRegistration As String) As Boolean Implements IX_voipSCPD.SetClient3

        With TR064Start(ServiceFile, "X_AVM-DE_SetClient3", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                {"NewX_AVM-DE_ClientId", ClientId},
                                                                                {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber},
                                                                                {"NewX_AVM-DE_InComingNumbers", InComingNumbers},
                                                                                {"NewX_AVM-DE_ExternalRegistration", ExternalRegistration}})
            Return Not .ContainsKey("Error")
        End With
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

        With TR064Start(ServiceFile, "X_AVM-DE_SetClient4", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                {"NewX_AVM-DE_ClientId", ClientId},
                                                                                {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber},
                                                                                {"NewX_AVM-DE_InComingNumbers", InComingNumbers},
                                                                                {"NewX_AVM-DE_InternalNumber", InternalNumber}})
            Return Not .ContainsKey("Error")
        End With
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

        With TR064Start(ServiceFile, "X_AVM-DE_SetDelayedCallNotification", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                {"NewX_AVM-DE_DelayedCallNotification", DelayedCallNotification.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "Numbers"
    Public Function GetNumberOfNumbers(ByRef NumberOfNumbers As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfNumbers
        With TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfNumbers", Nothing)

            If .ContainsKey("NewNumberOfNumbers") Then
                NumberOfNumbers = CInt(.Item("NewNumberOfNumbers"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNumberOfNumbers konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                NumberOfNumbers = 0

                Return False
            End If
        End With
    End Function

    Public Function GetNumbers(ByRef NumberList As SIPTelNrList) As Boolean Implements IX_voipSCPD.GetNumbers

        With TR064Start(ServiceFile, "X_AVM-DE_GetNumbers", Nothing)

            If .ContainsKey("NewNumberList") Then

                If Not XML.Deserialize(.Item("NewNumberList").ToString(), False, NumberList) Then
                    PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNumbers konnte für nicht deserialisiert werden. '{ .Item("Error")}'")
                End If

                ' Wenn keine Nummern angeschlossen wurden, gib eine leere Klasse zurück
                If NumberList Is Nothing Then NumberList = New SIPTelNrList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNumbers konnte für nicht aufgelößt werden.")
                NumberList = Nothing

                Return False
            End If
        End With

    End Function
#End Region

#Region "Dialing"
    Public Function GetPhonePort(ByRef PhoneName As String, i As Integer) As Boolean Implements IX_voipSCPD.GetPhonePort

        With TR064Start(ServiceFile, "X_AVM-DE_GetPhonePort", New Hashtable From {{"NewIndex", i}})

            If .ContainsKey("NewX_AVM-DE_PhoneName") Then
                PhoneName = .Item("NewX_AVM-DE_PhoneName").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetPhonePort konnte für id {i} nicht aufgelößt werden. '{ .Item("Error")}'")
                PhoneName = String.Empty

                Return False
            End If
        End With

    End Function

    Public Function DialGetConfig(ByRef PhoneName As String) As Boolean Implements IX_voipSCPD.DialGetConfig
        With TR064Start(ServiceFile, "X_AVM-DE_DialGetConfig", Nothing)

            If .ContainsKey("NewX_AVM-DE_PhoneName") Then
                PhoneName = .Item("NewX_AVM-DE_PhoneName").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Eingestellter: Phoneport '{PhoneName}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_DialGetConfig konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                PhoneName = String.Empty

                Return False
            End If
        End With
    End Function

    Public Function DialHangup() As Boolean Implements IX_voipSCPD.DialHangup
        With TR064Start(ServiceFile, "X_AVM-DE_DialHangup", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DialNumber(PhoneNumber As String) As Boolean Implements IX_voipSCPD.DialNumber
        With TR064Start(ServiceFile, "X_AVM-DE_DialNumber", New Hashtable From {{"NewX_AVM-DE_PhoneNumber", PhoneNumber}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DialSetConfig(PhoneName As String) As Boolean Implements IX_voipSCPD.DialSetConfig
        With TR064Start(ServiceFile, "X_AVM-DE_DialSetConfig", New Hashtable From {{"NewX_AVM-DE_PhoneName", PhoneName}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "AlarmClock"
    Public Function GetAlarmClock(ByRef AlarmClock As AlarmClock, Index As Integer) As Boolean Implements IX_voipSCPD.GetAlarmClock
        If AlarmClock Is Nothing Then AlarmClock = New AlarmClock

        With TR064Start(ServiceFile, "X_AVM-DE_GetAlarmClock", New Hashtable From {{"NewIndex", Index}})

            If .ContainsKey("NewX_AVM-DE_AlarmClockEnable") Then
                AlarmClock.AlarmClockEnable = CBool(.Item("NewX_AVM-DE_AlarmClockEnable"))
                AlarmClock.AlarmClockName = .Item("NewX_AVM-DE_AlarmClockName").ToString
                AlarmClock.AlarmClockTime = .Item("NewX_AVM-DE_AlarmClockTime").ToString
                AlarmClock.AlarmClockWeekdays = .Item("NewX_AVM-DE_AlarmClockWeekdays").ToString.Split(",")
                AlarmClock.AlarmClockPhoneName = .Item("NewX_AVM-DE_AlarmClockPhoneName").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetAlarmClock konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetNumberOfAlarmClocks(ByRef NumberOfAlarmClocks As Integer) As Boolean Implements IX_voipSCPD.GetNumberOfAlarmClocks
        With TR064Start(ServiceFile, "X_AVM-DE_GetNumberOfAlarmClocks", Nothing)

            If .ContainsKey("NewX_AVM-DE_NumberOfAlarmClocks") Then
                NumberOfAlarmClocks = CInt(.Item("NewX_AVM-DE_NumberOfAlarmClocks"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNumberOfAlarmClocks konnte nicht aufgelößt werden. '{ .Item("Error")}'")
                NumberOfAlarmClocks = -1

                Return False
            End If
        End With
    End Function

    Public Function SetAlarmClockEnable(Index As Integer, AlarmClockEnable As Boolean) As Boolean Implements IX_voipSCPD.SetAlarmClockEnable
        With TR064Start(ServiceFile, "X_AVM-DE_SetAlarmClockEnable", New Hashtable From {{"NewIndex", Index},
                                                                                         {"NewX_AVM-DE_AlarmClockEnable", AlarmClockEnable.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region
End Class
