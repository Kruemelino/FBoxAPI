Imports System.Runtime.CompilerServices
''' <summary>
''' AVM Home Automation HTTP Interface<br/>
''' Date: 2020-09-16<br/>
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/AHA-HTTP-Interface.pdf</see>
''' </summary>
Friend Class FBoxAHA
    Inherits APIConnectorBase
    Implements IFBoxAHA

    Private ReadOnly Property FBoXTR064Base As FritzBoxTR64
    'Private ReadOnly Property Client As TR064WebFunctions
    Private Property UrlPort As Integer = 0
    Private Property SessionID As String = String.Empty
    Private Property SessionIDTime As Date = Date.MinValue

    Public Sub New(TR064Base As FritzBoxTR64)
        FBoXTR064Base = TR064Base
    End Sub

    Private Async Function SendAHARequest(Optional Parameter As Dictionary(Of String, String) = Nothing, <CallerMemberName> Optional SwitchCMD As String = Nothing) As Task(Of String)

        Dim RemoteInfo As New XRemoteInfo
        ' Der HTTPS-Port ist auf der FRITZ!Box konfigurierbar. Er kann über den X_AVM-DE_RemoteAccess TR-064-
        ' Service und der dazugehörigen GetInfo-Action mit der "NewPort"-Variable abgefragt werden. 
        If UrlPort.IsZero AndAlso FBoXTR064Base.X_remote.GetInfo(RemoteInfo) Then UrlPort = RemoteInfo.Port

        ' Die SessionID hat nach Vergabe eine Gültigkeit von 60 Minuten.
        If (SessionID.IsStringNothingOrEmpty Or (Date.Now - SessionIDTime).TotalMinutes.IsLarger(59)) Then FBoXTR064Base.Deviceconfig.GetSessionID(SessionID)

        If SessionID.IsNotStringNothingOrEmpty Then

            ' Die Gültigkeitsdauer der SessionID verlängert sich automatisch bei aktivem Zugriff auf die FRITZ!Box. 
            SessionIDTime = Date.Now

            Dim QueryString As IEnumerable(Of String) = {$"switchcmd={SwitchCMD.ToLower}", SessionID}

            If Parameter IsNot Nothing Then QueryString = QueryString.Union(Parameter.Select(Function(kvp) $"{kvp.Key}={kvp.Value}"))

            Dim ub As New UriBuilder With {.Scheme = Uri.UriSchemeHttps,
                                           .Port = If(UrlPort.IsZero, 443, UrlPort),
                                           .Host = FritzBoxTR64.FBoxIPAdresse,
                                           .Path = $"/webservices/homeautoswitch.lua",
                                           .Query = String.Join("&", QueryString)}

            SendLog(LogLevel.Debug, ub.Uri.AbsoluteUri)

            ' Am Ende der Antwort ist immer ein Zeilenumbruch. Dieser wird mittels TrimEnd entfernt.
            Return (Await FBoXTR064Base.Client.GetStringWebClientAsync(ub.Uri)).TrimEnd({Convert.ToChar(13), Convert.ToChar(10)})
        End If

        Return String.Empty

    End Function

#Region "Switch/Schalter"
    Public Async Function GetSwitchList() As Task(Of String()) Implements IFBoxAHA.GetSwitchList
        ' Teile die Antwort
        Return (Await SendAHARequest()).Split(","c)
    End Function

    Public Async Function SetSwitchON(AIN As String) As Task(Of String) Implements IFBoxAHA.SetSwitchON
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function SetSwitchOFF(AIN As String) As Task(Of String) Implements IFBoxAHA.SetSwitchOFF
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function SetSwitchToggle(AIN As String) As Task(Of String) Implements IFBoxAHA.SetSwitchToggle
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function GetSwitchState(AIN As String) As Task(Of String) Implements IFBoxAHA.GetSwitchState
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function GetSwitchPresent(AIN As String) As Task(Of String) Implements IFBoxAHA.GetSwitchPresent
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function GetSwitchPower(AIN As String) As Task(Of String) Implements IFBoxAHA.GetSwitchPower
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function GetSwitchEnergy(AIN As String) As Task(Of String) Implements IFBoxAHA.GetSwitchEnergy
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function

    Public Async Function GetSwitchName(AIN As String) As Task(Of String) Implements IFBoxAHA.GetSwitchName
        Return Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Function
#End Region

    Private Async Function GetDeviceListInfos() As Task(Of AHADeviceList) Implements IFBoxAHA.GetDeviceListInfos
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHADeviceList)(Await SendAHARequest())
    End Function

    Public Async Function GetDeviceInfos(AIN As String) As Task(Of AHAGroup) Implements IFBoxAHA.GetDeviceInfos
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHAGroup)(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

    Public Async Function GetBasicDeviceStats(AIN As String) As Task(Of AHADeviceStats) Implements IFBoxAHA.GetBasicDeviceStats
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHADeviceStats)(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

#Region "Heizkörperregler"
    Public Async Function GetTemperature(AIN As String) As Task(Of Double) Implements IFBoxAHA.GetTemperature
        Return CDbl(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

    Public Async Function GethHKRTsoll(AIN As String) As Task(Of Double) Implements IFBoxAHA.GetHKRTsoll
        Return CDbl(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

    Public Async Function GetHKRkomfort(AIN As String) As Task(Of Double) Implements IFBoxAHA.GetHKRkomfort
        Return CDbl(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

    Public Async Function GetHKRabsenk(AIN As String) As Task(Of Double) Implements IFBoxAHA.GetHKRabsenk
        Return CDbl(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}}))
    End Function

    Public Async Sub SethHKRTsoll(AIN As String, HKRTsoll As Double) Implements IFBoxAHA.SetHKRTsoll
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"param", HKRTsoll.ToString}})
    End Sub

    Public Async Function SetHKRBoost(AIN As String, EndTimeStamp As Integer) As Task(Of Integer) Implements IFBoxAHA.SetHKRBoost
        Return CInt(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                                 {"endtimestamp", EndTimeStamp.ToString}}))
    End Function

    Public Async Function SetHKRWindowOpen(AIN As String, EndTimeStamp As Integer) As Task(Of Integer) Implements IFBoxAHA.SetHKRWindowOpen
        Return CInt(Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                                 {"endtimestamp", EndTimeStamp.ToString}}))

    End Function
#End Region

#Region "Leuchten"
    Public Async Sub SetLevel(AIN As String, Level As Integer) Implements IFBoxAHA.SetLevel
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"level", Level.ToString}})
    End Sub

    Public Async Sub SetLevelPercentage(AIN As String, Level As Integer) Implements IFBoxAHA.SetLevelPercentage
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"level", Level.ToString}})
    End Sub

    Public Async Sub SetColor(AIN As String, Hue As Integer, Saturation As Integer, Duration As Integer) Implements IFBoxAHA.SetColor
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"hue", Hue.ToString},
                                                                     {"saturation", Saturation.ToString},
                                                                     {"duration", Duration.ToString}})
    End Sub
    Public Async Sub SetUnmappedColor(AIN As String, Hue As Integer, Saturation As Integer, Duration As Integer) Implements IFBoxAHA.SetUnmappedColor
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"hue", Hue.ToString},
                                                                     {"saturation", Saturation.ToString},
                                                                     {"duration", Duration.ToString}})
    End Sub

    Public Async Sub SetColorTemperature(AIN As String, Temperature As Integer, Duration As Integer) Implements IFBoxAHA.SetColorTemperature
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"temperature", Temperature.ToString},
                                                                     {"duration", Duration.ToString}})
    End Sub

    Public Async Function GetColorDefaults() As Task(Of AHAColorDefaults) Implements IFBoxAHA.GetColorDefaults
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHAColorDefaults)(Await SendAHARequest())
    End Function

    ' TODO Wie soll das übergeben werden?
    Public Async Sub AddColorLevelTemplate(Name As String, LevelPercentage As Integer, Hue As Integer, Saturation As Integer, Temperature As Integer, AINList As IEnumerable(Of String), Optional ColorPresent As Boolean = False) Implements IFBoxAHA.AddColorLevelTemplate

        Dim ParamDict As New Dictionary(Of String, String) From {{"name", Name},
                                                                 {"hue", Hue.ToString},
                                                                 {"levelPercentage", LevelPercentage.ToString},
                                                                 {"saturation", Saturation.ToString},
                                                                 {"temperature", Temperature.ToString},
                                                                 {"colorpreset", ColorPresent.ToBoolStr}}

        ' ain-Geräteliste in Anzahl n child_<n>-Parametern beginnend mit child_1, child_2..
        For i = 1 To AINList.Count
            ParamDict.Add($"child_{i}", AINList(i))
        Next
        Await SendAHARequest(ParamDict)
    End Sub



#End Region

#Region "Vorlagen"

    Public Async Function GetTemplateListInfos() As Task(Of AHATemplateList) Implements IFBoxAHA.GetTemplateListInfos
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHATemplateList)(Await SendAHARequest())
    End Function

    Public Async Sub ApplyTemplate(AIN As String) Implements IFBoxAHA.ApplyTemplate
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN}})
    End Sub

#End Region

#Region "Diverse Aktionen"

    Public Async Sub SetSimpleOnOff(AIN As String, OnOff As Boolean) Implements IFBoxAHA.SetSimpleOnOff
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"onoff", OnOff.ToBoolStr}})
    End Sub

    Public Async Sub Setblind(AIN As String, Target As String) Implements IFBoxAHA.Setblind
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"levtargetel", Target}})
    End Sub

    Public Async Sub SetName(AIN As String, Name As String) Implements IFBoxAHA.SetName
        Await SendAHARequest(New Dictionary(Of String, String) From {{"ain", AIN},
                                                                     {"name", Name}})
    End Sub

    Public Async Sub StartULESubscription() Implements IFBoxAHA.StartULESubscription
        Await SendAHARequest()
    End Sub

    Public Async Function GetSubscriptionState() As Task(Of AHASubscriptionState) Implements IFBoxAHA.GetSubscriptionState
        Return Await FBoXTR064Base.XML.DeserializeAsyncData(Of AHASubscriptionState)(Await SendAHARequest())
    End Function

#End Region
    ' TODO
    Public Function GetTriggerListInfos() As Task(Of String) Implements IFBoxAHA.GetTriggerListInfos
        Throw New NotImplementedException()
    End Function

    ' TODO
    Public Sub SetTriggerActive(AIN As String, Active As Boolean) Implements IFBoxAHA.SetTriggerActive
        Throw New NotImplementedException()
    End Sub
End Class
