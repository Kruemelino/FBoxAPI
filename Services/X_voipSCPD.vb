''' <summary>
''' TR-064 Support – X_VoIP
''' Date: 2019-08-14
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf"/>
''' </summary>
Public Class X_voipSCPD
    Implements IService
    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IService.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IService.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IService.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))
        ServiceFile = SCPDFiles.x_voipSCPD
        TR064Start = Start

        PushStatus = Status
    End Sub

#Region "GetInfo"
    Public Function GetInfo(ByRef FaxT38Enable As Boolean, ByRef VoiceCoding As VoiceCoding) As Boolean
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

    Public Function SetConfig(T38FaxEnable As Boolean, VoiceCoding As VoiceCoding) As Boolean
        With TR064Start(ServiceFile, "SetConfig", New Hashtable From {{"NewT38FaxEnable", T38FaxEnable}, {"NewVoiceCoding", VoiceCoding}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetInfoEx(ByRef InfoEx As VoIPInfoEx) As Boolean

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
    Public Function GetExistingVoIPNumbers(ByRef ExistingVoIPNumbers As Integer) As Boolean
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

    Public Function GetMaxVoIPNumbers(ByRef MaxVoIPNumbers As Integer) As Boolean
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
    Public Function GetVoIPEnableAreaCode(ByRef VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean
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

    Public Function SetVoIPEnableAreaCode(VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean
        With TR064Start(ServiceFile, "SetVoIPEnableAreaCode", New Hashtable From {{"NewVoIPAccountIndex", VoIPAccountIndex},
                                                                                  {"NewVoIPEnableAreaCode", VoIPEnableAreaCode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPEnableCountryCode(ByRef VoIPEnableCountryCode As Boolean, VoIPAccountIndex As Integer) As Boolean
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

    ''' <summary>
    ''' Set the common country code where the <paramref name="LKZ"/> represents the actual country code and the <paramref name="LKZPrefix"/> is the international call prefix.<br/>
    ''' e.g. +49 = 0049 where 00 is the <paramref name="LKZPrefix"/> and 49 the <paramref name="LKZ"/>.
    ''' </summary>
    ''' <param name="LKZ">Represents the actual country code.</param>
    ''' <param name="LKZPrefix">Represents the international call prefix.</param>
    ''' <returns>True when success</returns>
    Public Function SetVoIPCommonCountryCode(LKZ As String, LKZPrefix As String) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonCountryCode", New Hashtable From {{"NewX_AVM-DE_LKZ", LKZ},
                                                                                           {"NewX_AVM-DE_LKZPrefix", LKZPrefix}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    ''' <summary>
    ''' Get the configured common country code where the <paramref name="LKZ"/> represents the actual country code and the <paramref name="LKZPrefix"/> is the international call prefix.
    ''' </summary>
    ''' <param name="LKZ">Represents the actual country code.</param>
    ''' <param name="LKZPrefix">Represents the international call prefix.</param>
    ''' <returns>True when success</returns>
    Public Function GetVoIPCommonCountryCode(ByRef LKZ As String, Optional ByRef LKZPrefix As String = "") As Boolean

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

    ''' <summary>
    ''' Set the common area code where the <paramref name="OKZ"/> represents the actual area code and the <paramref name="OKZPrefix"/> is the national call prefix.<br/> 
    ''' e.g. 030 where 0 is the <paramref name="OKZPrefix"/> and 30 the <paramref name="OKZ"/>.
    ''' </summary>
    ''' <param name="OKZ">Represents the actual area code.</param>
    ''' <param name="OKZPrefix">Represents the national Call prefix.</param>
    ''' <returns>True when success</returns>
    Public Function SetVoIPCommonAreaCode(OKZ As String, OKZPrefix As String) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_SetVoIPCommonAreaCode", New Hashtable From {{"NewX_AVM-DE_OKZ", OKZ},
                                                                                           {"NewX_AVM-DE_OKZPrefix", OKZPrefix}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    ''' <summary>
    ''' Get the configured common area code where the <paramref name="OKZ"/> represents the actual area code and the <paramref name="OKZPrefix"/> is the national Call prefix.
    ''' </summary>
    ''' <param name="OKZ">Represents the actual area code.</param>
    ''' <param name="OKZPrefix">Represents the national Call prefix.</param>
    ''' <returns>True when success</returns>
    Public Function GetVoIPCommonAreaCode(ByRef OKZ As String, Optional ByRef OKZPrefix As String = "") As Boolean

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
                                   VoIPSTUNServer As String) As Boolean

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

    ''' <summary>
    ''' The action can be used to delete an existing VoIP entry.
    ''' </summary>
    Public Function DelVoIPAccount(VoIPAccountIndex As Integer) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_DelVoIPAccount", New Hashtable From {{"NewVoIPAccountIndex ", VoIPAccountIndex}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetVoIPAccount(ByRef Account As VoIPAccount, AccountIndex As Integer) As Boolean
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
    Public Function DeleteClient(ClientIndex As Integer) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_DeleteClient", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetNumberOfClients(ByRef NumberOfClients As Integer) As Boolean
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

    Public Function GetClient2(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean
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

    ''' <summary>
    ''' Return SIP Client account with incoming numbers and allow registration from outside flag.
    ''' </summary>
    ''' <remarks>The format of the state variable X_AVM-DE_IncomingNumbers is similar to the state variable X_AVMDE_Numbers described in the paragraph X_AVM-DE_GetNumbers (below).
    ''' If the SIP client shall react on all possible numbers the Type is set to eAllCalls.</remarks>
    Public Function GetClient3(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean
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
                If Not DeserializeXML(.Item("NewX_AVM-DE_InComingNumbers").ToString(), False, Client.InComingNumbers) Then
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

    ''' <summary>
    ''' The input parameter ClientId has to be at least 1 character long and a substring of the actual ClientId (case
    ''' sensitive). The response contains the information about the client, whose ClientId string contains the input 
    ''' parameter. Even when it is a substring. E.g. the string “le” returns “apple” from the following ClientId List: 
    ''' [0] : "melon" ; [1] "apple" ; [2] "lemon".
    ''' </summary>
    ''' <returns>Return SIP Client account with incoming numbers and allow registration from outside flag.</returns>
    Public Function GetClientByClientId(ByRef Client As SIPClient, ClientId As String) As Boolean
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
                If Not DeserializeXML(.Item("NewX_AVM-DE_InComingNumbers").ToString(), False, Client.InComingNumbers) Then
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

    ''' <summary>
    ''' Return a list of all SIP client accounts. 
    ''' </summary>
    ''' <param name="ClientList">Represents the list of all SIP client accounts.</param>
    ''' <returns>True when success</returns>
    ''' <remarks>The list contains all configured SIP client accounts a XML list.</remarks>
    Public Function GetClients(ByRef ClientList As SIPClientList) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_GetClients", Nothing)

            If .ContainsKey("NewX_AVM-DE_ClientList") Then

                If Not DeserializeXML(.Item("NewX_AVM-DE_ClientList").ToString(), False, ClientList) Then
                    PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNumbers konnte für nicht deserialisiert werden.")
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


    ''' <summary>
    ''' Create a SIP client account or overwrite it when the X_AVM-DE_ClientIndex is already in use.
    ''' </summary>
    ''' <remarks>When the action is called with app instance credentials and the parameter NewX_AVM-DE_ClientId is set,
    ''' an internal link between the created SIP client and app instance is created. Therefore when the app instance
    ''' is deleted, the SIP client is deleted too.
    ''' </remarks>
    ''' <returns>True when success</returns>
    Public Function SetClient2(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_SetClient2", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                {"NewX_AVM-DE_ClientPassword", ClientPassword},
                                                                                {"NewX_AVM-DE_PhoneName", PhoneName},
                                                                                {"NewX_AVM-DE_ClientId", ClientId},
                                                                                {"NewX_AVM-DE_OutGoingNumber", OutGoingNumber}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and allow registration from outside flag or overwrites it
    ''' when the X_AVM-DE_ClientIndex is already in use. When the action is called with app instance credentials
    ''' and the parameter NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app
    ''' instance is created. Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    ''' <remarks>
    ''' The format of the state variable X_AVM-DE_IncomingNumbers is similar to the state variable X_AVMDE_Numbers described in the paragraph X_AVM-DE_GetNumbers (above).
    ''' If the value for X_AVM-DE_IncomingNumbers is empty, the SIP client has to ring for all incoming numbers. 
    ''' </remarks>
    ''' <param name="ExternalRegistration">Value ignored 2015-10-22</param>
    ''' <returns>True when success</returns>
    Public Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               ExternalRegistration As String) As Boolean

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

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and allow registration from outside flag or overwrites it
    ''' when the X_AVM-DE_ClientIndex is already in use. When the action is called with app instance credentials
    ''' and the parameter NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app
    ''' instance is created. Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    ''' <remarks>
    ''' The format of the state variable X_AVM-DE_IncomingNumbers is similar to the state variable X_AVMDE_Numbers described in the paragraph X_AVM-DE_GetNumbers (above).
    ''' If the value for X_AVM-DE_IncomingNumbers is empty, the SIP client has to ring for all incoming numbers. 
    ''' </remarks>
    ''' <param name="ExternalRegistration">Value ignored 2015-10-22</param>
    ''' <returns>True when success</returns>
    Public Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               ExternalRegistration As String) As Boolean

        Return SetClient3(ClientIndex, ClientPassword, PhoneName, ClientId, OutGoingNumber, InComingNumbers.ToXMLString, ExternalRegistration)
    End Function

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and client username or overwrites it when the X_AVMDE_ClientIndex is already in use. When the action is called with app instance credentials and the parameter
    ''' NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app instance is created.
    ''' Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    ''' <returns>True when success</returns>
    Public Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               InternalNumber As String) As Boolean

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

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and client username or overwrites it when the X_AVMDE_ClientIndex is already in use. When the action is called with app instance credentials and the parameter
    ''' NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app instance is created.
    ''' Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    ''' <returns>True when success</returns>
    Public Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               InternalNumber As String) As Boolean

        Return SetClient4(ClientIndex, ClientPassword, PhoneName, ClientId, OutGoingNumber, InComingNumbers.ToXMLString, InternalNumber)

    End Function

    ''' <summary>
    ''' Set the flag for a SIP client account. Some SIP clients need some seconds time to wake up before a SIP call can be answered. 
    ''' The FRITZ!OS SIP server will delay SIP calls if at least one SIP client has the flag enabled.
    ''' </summary>
    ''' <returns>True when success</returns>
    Public Function SetDelayedCallNotification(ClientIndex As Integer, DelayedCallNotification As Boolean) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_SetDelayedCallNotification", New Hashtable From {{"NewX_AVM-DE_ClientIndex", ClientIndex},
                                                                                                {"NewX_AVM-DE_DelayedCallNotification", DelayedCallNotification.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "Numbers"
    ''' <summary>
    ''' Return amount of telephone numbers usable as incoming number. 
    ''' </summary>
    ''' <param name="NumberOfNumbers"></param>
    ''' <returns>True when success</returns>
    Public Function GetNumberOfNumbers(ByRef NumberOfNumbers As Integer) As Boolean
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

    ''' <summary>
    ''' Return a list of all telephone numbers. 
    ''' </summary>
    ''' <param name="NumberList">Represents the list of all telephone numbers.</param>
    ''' <returns>True when success</returns>
    ''' <remarks>The list contains all configured numbers for all number types. The index can be used to see how many numbers are configured For one type. </remarks>
    Public Function GetNumbers(ByRef NumberList As SIPTelNrList) As Boolean

        With TR064Start(ServiceFile, "X_AVM-DE_GetNumbers", Nothing)

            If .ContainsKey("NewNumberList") Then

                If Not DeserializeXML(.Item("NewNumberList").ToString(), False, NumberList) Then
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

    ''' <summary>
    ''' Return phone name by <paramref name="i"/> (1 … n) usable for X_AVM-DE_SetDialConfig.
    ''' <list type="bullet">
    ''' <item>FON1: Telefon</item>
    ''' <item>FON2: Telefon</item>
    ''' <item>ISDN: ISDN/DECT Rundruf</item>
    ''' <item>DECT: Mobilteil 1</item>
    ''' </list>
    ''' </summary>
    ''' <param name="PhoneName">Represents the PhoneName of index <paramref name="i"/>.</param>
    ''' <param name="i">Represents the index of all dialable phones.</param>
    ''' <remarks>SIP IP phones are not usable for X_AVM-DE_SetDialConfig.</remarks>
    ''' <returns>True when success</returns>
    Public Function GetPhonePort(ByRef PhoneName As String, i As Integer) As Boolean

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

    ''' <summary>
    ''' Ermittelt das aktuell ausgewählte Telefon der Fritz!Box Wählhilfe
    ''' </summary>
    ''' <param name="PhoneName">Phoneport des ausgewählten Telefones.</param>
    ''' <returns>True when success</returns>
    Public Function DialGetConfig(ByRef PhoneName As String) As Boolean
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

    ''' <summary>
    ''' Disconnect the dialling process. 
    ''' </summary>
    ''' <returns>True</returns>
    Public Function DialHangup() As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_DialHangup", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function

    ''' <summary>
    ''' Startet den Wählvorgang mit der übergebenen Telefonnummer.
    ''' </summary>
    ''' <param name="PhoneNumber">Die zu wählende Telefonnummer.</param>
    Public Function DialNumber(PhoneNumber As String) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_DialNumber", New Hashtable From {{"NewX_AVM-DE_PhoneNumber", PhoneNumber}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    ''' <summary>
    ''' Stellt die Wählhilfe der Fritz!Box auf das gewünschte Telefon um.
    ''' </summary>
    ''' <param name="PhoneName">Phoneport des Telefones.</param>
    Public Function DialSetConfig(PhoneName As String) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_DialSetConfig", New Hashtable From {{"NewX_AVM-DE_PhoneName", PhoneName}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region

#Region "AlarmClock"
    Public Function GetAlarmClock(ByRef AlarmClock As AlarmClock, Index As Integer) As Boolean
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

    ''' <summary>
    ''' Returns the amount of alarm clocks.
    ''' </summary>
    Public Function GetNumberOfAlarmClocks(ByRef NumberOfAlarmClocks As Integer) As Boolean
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

    ''' <summary>
    ''' Enables or disables the alarm clock.
    ''' </summary>
    Public Function SetAlarmClockEnable(Index As Integer, AlarmClockEnable As Boolean) As Boolean
        With TR064Start(ServiceFile, "X_AVM-DE_SetAlarmClockEnable", New Hashtable From {{"NewIndex", Index},
                                                                                         {"NewX_AVM-DE_AlarmClockEnable", AlarmClockEnable.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function
#End Region
End Class
