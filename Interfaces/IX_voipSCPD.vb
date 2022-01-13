''' <summary>
''' TR-064 Support – X_VoIP
''' Date: 2019-08-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_voip-avm.pdf</see>
''' </summary>
Public Interface IX_voipSCPD
    Inherits IServiceBase

#Region "GetInfo"
    Function GetInfo(ByRef FaxT38Enable As Boolean, ByRef VoiceCoding As VoiceCodingEnum) As Boolean

    Function SetConfig(T38FaxEnable As Boolean, VoiceCoding As VoiceCodingEnum) As Boolean

    Function GetInfoEx(ByRef InfoEx As VoIPInfoEx) As Boolean
#End Region

#Region "VoIPNumbers"
    Function GetExistingVoIPNumbers(ByRef ExistingVoIPNumbers As Integer) As Boolean

    Function GetMaxVoIPNumbers(ByRef MaxVoIPNumbers As Integer) As Boolean
#End Region

#Region "AreaCode / CountryCode"
    Function GetVoIPEnableAreaCode(ByRef VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean

    Function SetVoIPEnableAreaCode(VoIPEnableAreaCode As Boolean, VoIPAccountIndex As Integer) As Boolean

    Function GetVoIPEnableCountryCode(ByRef VoIPEnableCountryCode As Boolean, VoIPAccountIndex As Integer) As Boolean

    ''' <summary>
    ''' Set the common country code where the <paramref name="LKZ"/> represents the actual country code and the <paramref name="LKZPrefix"/> is the international call prefix.<br/>
    ''' e.g. +49 = 0049 where 00 is the <paramref name="LKZPrefix"/> and 49 the <paramref name="LKZ"/>.
    ''' </summary>
    ''' <param name="LKZ">Represents the actual country code.</param>
    ''' <param name="LKZPrefix">Represents the international call prefix.</param>
    Function SetVoIPCommonCountryCode(LKZ As String, LKZPrefix As String) As Boolean

    ''' <summary>
    ''' Get the configured common country code where the <paramref name="LKZ"/> represents the actual country code and the <paramref name="LKZPrefix"/> is the international call prefix.
    ''' </summary>
    ''' <param name="LKZ">Represents the actual country code.</param>
    ''' <param name="LKZPrefix">Represents the international call prefix.</param>
    Function GetVoIPCommonCountryCode(ByRef LKZ As String, Optional ByRef LKZPrefix As String = "") As Boolean

    ''' <summary>
    ''' Set the common area code where the <paramref name="OKZ"/> represents the actual area code and the <paramref name="OKZPrefix"/> is the national call prefix.<br/> 
    ''' e.g. 030 where 0 is the <paramref name="OKZPrefix"/> and 30 the <paramref name="OKZ"/>.
    ''' </summary>
    ''' <param name="OKZ">Represents the actual area code.</param>
    ''' <param name="OKZPrefix">Represents the national Call prefix.</param>
    Function SetVoIPCommonAreaCode(OKZ As String, OKZPrefix As String) As Boolean

    ''' <summary>
    ''' Get the configured common area code where the <paramref name="OKZ"/> represents the actual area code and the <paramref name="OKZPrefix"/> is the national Call prefix.
    ''' </summary>
    ''' <param name="OKZ">Represents the actual area code.</param>
    ''' <param name="OKZPrefix">Represents the national Call prefix.</param>
    Function GetVoIPCommonAreaCode(ByRef OKZ As String, Optional ByRef OKZPrefix As String = "") As Boolean
#End Region

#Region "VoIP Account"
    Function AddVoIPAccount(VoIPAccountIndex As Integer,
                                   VoIPRegistrar As String,
                                   VoIPNumber As String,
                                   VoIPUsername As String,
                                   VoIPPassword As String,
                                   VoIPOutboundProxy As String,
                                   VoIPSTUNServer As String) As Boolean

    ''' <summary>
    ''' The action can be used to delete an existing VoIP entry.
    ''' </summary>
    Function DelVoIPAccount(VoIPAccountIndex As Integer) As Boolean

    Function GetVoIPAccount(ByRef Account As VoIPAccount, AccountIndex As Integer) As Boolean
#End Region

#Region "Client"
    Function DeleteClient(ClientIndex As Integer) As Boolean

    Function GetNumberOfClients(ByRef NumberOfClients As Integer) As Boolean

    Function GetClient2(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean

    ''' <summary>
    ''' Return SIP Client account with incoming numbers and allow registration from outside flag.
    ''' </summary>
    ''' <remarks>The format of the state variable X_AVM-DE_IncomingNumbers is similar to the state variable X_AVMDE_Numbers described in the paragraph X_AVM-DE_GetNumbers (below).
    ''' If the SIP client shall react on all possible numbers the Type is set to eAllCalls.</remarks>
    Function GetClient3(ByRef Client As SIPClient, ClientIndex As Integer) As Boolean

    ''' <summary>
    ''' The input parameter ClientId has to be at least 1 character long and a substring of the actual ClientId (case
    ''' sensitive). The response contains the information about the client, whose ClientId string contains the input 
    ''' parameter. Even when it is a substring. E.g. the string “le” returns “apple” from the following ClientId List: 
    ''' [0] : "melon" ; [1] "apple" ; [2] "lemon".
    ''' </summary>
    ''' <returns>Return SIP Client account with incoming numbers and allow registration from outside flag.</returns>
    Function GetClientByClientId(ByRef Client As SIPClient, ClientId As String) As Boolean

    ''' <summary>
    ''' Return a list of all SIP client accounts. 
    ''' </summary>
    ''' <param name="ClientList">Represents the list of all SIP client accounts.</param>
    ''' <remarks>The list contains all configured SIP client accounts a XML list.</remarks>
    Function GetClients(ByRef ClientList As SIPClientList) As Boolean

    ''' <summary>
    ''' Create a SIP client account or overwrite it when the X_AVM-DE_ClientIndex is already in use.
    ''' </summary>
    ''' <remarks>When the action is called with app instance credentials and the parameter NewX_AVM-DE_ClientId is set,
    ''' an internal link between the created SIP client and app instance is created. Therefore when the app instance
    ''' is deleted, the SIP client is deleted too.
    ''' </remarks>
    Function SetClient2(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String) As Boolean

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
    Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               ExternalRegistration As String) As Boolean

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
    Function SetClient3(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               ExternalRegistration As String) As Boolean

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and client username or overwrites it when the X_AVMDE_ClientIndex is already in use. When the action is called with app instance credentials and the parameter
    ''' NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app instance is created.
    ''' Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As String,
                               InternalNumber As String) As Boolean

    ''' <summary>
    ''' Create a SIP client account with incoming numbers and client username or overwrites it when the X_AVMDE_ClientIndex is already in use. When the action is called with app instance credentials and the parameter
    ''' NewX_AVM-DE_ClientId is set, an internal link between the created SIP client and app instance is created.
    ''' Therefore when the app instance is deleted, the SIP client is deleted too.
    ''' </summary>
    Function SetClient4(ClientIndex As Integer,
                               ClientPassword As String,
                               PhoneName As String,
                               ClientId As String,
                               OutGoingNumber As String,
                               InComingNumbers As SIPTelNrList,
                               InternalNumber As String) As Boolean

    ''' <summary>
    ''' Set the flag for a SIP client account. Some SIP clients need some seconds time to wake up before a SIP call can be answered. 
    ''' The FRITZ!OS SIP server will delay SIP calls if at least one SIP client has the flag enabled.
    ''' </summary>
    Function SetDelayedCallNotification(ClientIndex As Integer, DelayedCallNotification As Boolean) As Boolean
#End Region

#Region "Numbers"
    ''' <summary>
    ''' Return amount of telephone numbers usable as incoming number. 
    ''' </summary>
    ''' <param name="NumberOfNumbers"></param>
    Function GetNumberOfNumbers(ByRef NumberOfNumbers As Integer) As Boolean

    ''' <summary>
    ''' Return a list of all telephone numbers. 
    ''' </summary>
    ''' <param name="NumberList">Represents the list of all telephone numbers.</param>
    ''' <remarks>The list contains all configured numbers for all number types. The index can be used to see how many numbers are configured For one type. </remarks>
    Function GetNumbers(ByRef NumberList As SIPTelNrList) As Boolean
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
    Function GetPhonePort(ByRef PhoneName As String, i As Integer) As Boolean


    ''' <summary>
    ''' Ermittelt das aktuell ausgewählte Telefon der Fritz!Box Wählhilfe
    ''' </summary>
    ''' <param name="PhoneName">Phoneport des ausgewählten Telefones.</param>
    Function DialGetConfig(ByRef PhoneName As String) As Boolean

    ''' <summary>
    ''' Disconnect the dialling process. 
    ''' </summary>
    ''' <returns>True</returns>
    Function DialHangup() As Boolean

    ''' <summary>
    ''' Startet den Wählvorgang mit der übergebenen Telefonnummer.
    ''' </summary>
    ''' <param name="PhoneNumber">Die zu wählende Telefonnummer.</param>
    Function DialNumber(PhoneNumber As String) As Boolean

    ''' <summary>
    ''' Stellt die Wählhilfe der Fritz!Box auf das gewünschte Telefon um.
    ''' </summary>
    ''' <param name="PhoneName">Phoneport des Telefones.</param>
    Function DialSetConfig(PhoneName As String) As Boolean
#End Region

#Region "AlarmClock"
    Function GetAlarmClock(ByRef AlarmClock As AlarmClock, Index As Integer) As Boolean

    ''' <summary>
    ''' Returns the amount of alarm clocks.
    ''' </summary>
    Function GetNumberOfAlarmClocks(ByRef NumberOfAlarmClocks As Integer) As Boolean

    ''' <summary>
    ''' Enables or disables the alarm clock.
    ''' </summary>
    Function SetAlarmClockEnable(Index As Integer, AlarmClockEnable As Boolean) As Boolean
#End Region

End Interface
