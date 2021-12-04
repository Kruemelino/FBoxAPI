''' <summary>
''' TR-064 Support – X_AVM-DE_OnTel
''' Date: 2021-02-09
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf"/>
''' </summary>
Public Class X_contactSCPD
    Implements IX_contactSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_contactSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_contactSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_contactSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_contactSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetInfoByIndex(Index As Integer,
                                   Optional ByRef Enable As Boolean = False,
                                   Optional ByRef Status As String = "",
                                   Optional ByRef LastConnect As String = "",
                                   Optional ByRef Url As String = "",
                                   Optional ByRef ServiceId As String = "",
                                   Optional ByRef Username As String = "",
                                   Optional ByRef Name As String = "") As Boolean Implements IX_contactSCPD.GetInfoByIndex

        With TR064Start(ServiceFile, "GetInfoByIndex", New Hashtable From {{"NewIndex", Index}})

            If .ContainsKey("NewName") Then

                Enable = CBool(.Item("NewEnable"))
                Status = .Item("NewStatus").ToString
                LastConnect = .Item("NewLastConnect").ToString
                Url = .Item("NewUrl").ToString
                ServiceId = .Item("NewServiceId").ToString
                Username = .Item("NewUsername").ToString
                Name = .Item("NewName").ToString


                PushStatus.Invoke(LogLevel.Debug, $"GetInfoByIndex des Telefonbuches {Index} von der Fritz!Box abgerufen: {Name}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfoByIndex des Telefonbuches {Index} von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function SetEnableByIndex(Index As Integer, Enable As Boolean) As Boolean Implements IX_contactSCPD.SetEnableByIndex

        With TR064Start(ServiceFile, "SetEnableByIndex", New Hashtable From {{"NewIndex", Index},
                                                                             {"NewEnable", Enable.ToInt}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function SetConfigByIndex(Index As Integer, Enable As Boolean, Url As String, ServiceId As String, Username As String, Password As String, Name As String) As Boolean Implements IX_contactSCPD.SetConfigByIndex

        With TR064Start(ServiceFile, "SetConfigByIndex", New Hashtable From {{"NewIndex", Index},
                                                                             {"NewEnable", Enable.ToInt},
                                                                             {"NewUrl", Url},
                                                                             {"NewServiceId", ServiceId},
                                                                             {"NewUsername", Username},
                                                                             {"NewPassword", Password},
                                                                             {"NewName", Name}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function GetNumberOfEntries(ByRef OntelNumberOfEntries As Integer) As Boolean Implements IX_contactSCPD.GetNumberOfEntries

        With TR064Start(ServiceFile, "GetNumberOfEntries", Nothing)
            If .ContainsKey("NewOntelNumberOfEntries") Then

                OntelNumberOfEntries = CInt(.Item("NewOntelNumberOfEntries"))

                PushStatus.Invoke(LogLevel.Debug, $"GetNumberOfEntries: {OntelNumberOfEntries}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfEntries von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function DeleteByIndex(Index As Integer) As Boolean Implements IX_contactSCPD.DeleteByIndex

        With TR064Start(ServiceFile, "DeleteByIndex", New Hashtable From {{"NewIndex", Index}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function GetCallList(ByRef CallListURL As String) As Boolean Implements IX_contactSCPD.GetCallList

        With TR064Start(ServiceFile, "GetCallList", Nothing)

            If .ContainsKey("NewCallListURL") Then

                CallListURL = .Item("NewCallListURL").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Pfad zur Anrufliste der Fritz!Box: {CallListURL} ")

                Return True
            Else
                CallListURL = String.Empty

                PushStatus.Invoke(LogLevel.Warn, $"Pfad zur Anrufliste der Fritz!Box konnte nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

#Region "Phonebook"
    Public Function GetPhonebookList(ByRef PhonebookList As Integer()) As Boolean Implements IX_contactSCPD.GetPhonebookList

        With TR064Start(ServiceFile, "GetPhonebookList", Nothing)

            If .ContainsKey("NewPhonebookList") Then
                ' Comma separated list of PhonebookID 
                PhonebookList = Array.ConvertAll(.Item("NewPhonebookList").ToString.Split(","),
                                                         New Converter(Of String, Integer)(AddressOf Integer.Parse))

                PushStatus.Invoke(LogLevel.Debug, $"Telefonbuchliste der Fritz!Box: '{String.Join(", ", PhonebookList)}'")

                Return True
            Else
                PhonebookList = {}

                PushStatus.Invoke(LogLevel.Warn, $"Telefonbuchliste der Fritz!Box konnte nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetPhonebook(PhonebookID As Integer,
                                 ByRef PhonebookURL As String,
                                 Optional ByRef PhonebookName As String = "",
                                 Optional ByRef PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.GetPhonebook

        With TR064Start(ServiceFile, "GetPhonebook", New Hashtable From {{"NewPhonebookID", PhonebookID}})

            If .ContainsKey("NewPhonebookURL") Then
                ' Phonebook URL auslesen
                PhonebookURL = .Item("NewPhonebookURL").ToString
                ' Phonebook Name auslesen
                If .ContainsKey("NewPhonebookName") Then PhonebookName = .Item("NewPhonebookName").ToString
                ' Phonebook ExtraID auslesen
                If .ContainsKey("NewPhonebookExtraID") Then PhonebookExtraID = .Item("NewPhonebookExtraID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Pfad zum Telefonbuch '{PhonebookName}' der Fritz!Box: {PhonebookURL} ")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetPhonebook konnte für das Telefonbuch {PhonebookID} nicht aufgelößt werden. '{ .Item("Error")}'")
                PhonebookURL = String.Empty

                Return False
            End If
        End With

    End Function

    Public Function AddPhonebook(PhonebookName As String, Optional PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.AddPhonebook

        With TR064Start(ServiceFile, "AddPhonebook", New Hashtable From {{"NewPhonebookName", PhonebookName},
                                                                                              {"NewPhonebookExtraID", PhonebookExtraID}})

            Return Not .ContainsKey("Error")

        End With

    End Function

    Public Function DeletePhonebook(NewPhonebookID As Integer, Optional PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.DeletePhonebook

        With TR064Start(ServiceFile, "DeletePhonebook", New Hashtable From {{"NewPhonebookID", NewPhonebookID},
                                                                                                 {"NewPhonebookExtraID", PhonebookExtraID}})

            Return Not .ContainsKey("Error")

        End With

    End Function
#End Region

#Region "PhonebookEntry"
    Public Function GetPhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetPhonebookEntry

        With TR064Start(ServiceFile, "GetPhonebookEntry", New Hashtable From {{"NewPhonebookID", PhonebookID},
                                                                                                   {"NewPhonebookEntryID", PhonebookEntryID}})

            If .ContainsKey("NewPhonebookEntryData") Then
                ' Phonebook URL auslesen
                PhonebookEntryData = .Item("NewPhonebookEntryData").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Telefonbucheintrag '{PhonebookEntryID}' aus Telefonbuch {PhonebookID} der Fritz!Box ausgelesen: '{PhonebookEntryData}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetPhonebookEntry für konnte für den Telefonbucheintrag '{PhonebookEntryID}' aus Telefonbuch {PhonebookID} nicht aufgelößt werden. '{ .Item("Error")}'")
                PhonebookEntryData = String.Empty

                Return False
            End If

        End With

    End Function

    Public Function GetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryUniqueID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetPhonebookEntryUID

        With TR064Start(ServiceFile, "GetPhonebookEntryUID", New Hashtable From {{"NewPhonebookID", PhonebookID},
                                                                                                      {"NewPhonebookEntryUniqueID", PhonebookEntryUniqueID}})

            If .ContainsKey("NewPhonebookEntryData") Then
                ' Phonebook URL auslesen
                PhonebookEntryData = .Item("NewPhonebookEntryData").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Telefonbucheintrag '{PhonebookEntryUniqueID}' aus Telefonbuch {PhonebookID} der Fritz!Box ausgelesen: '{PhonebookEntryData}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetPhonebookEntry für konnte für den Telefonbucheintrag '{PhonebookEntryUniqueID}' aus Telefonbuch '{PhonebookID}' nicht aufgelößt werden. '{ .Item("Error")}'")
                PhonebookEntryData = String.Empty

                Return False
            End If

        End With

    End Function

    Public Function SetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryData As String, ByRef PhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.SetPhonebookEntryUID

        With TR064Start(ServiceFile, "SetPhonebookEntryUID", New Hashtable From {{"NewPhonebookID", PhonebookID},
                                                                                                      {"NewPhonebookEntryData", PhonebookEntryData}})

            If .ContainsKey("NewPhonebookEntryUniqueID") Then
                ' Phonebook URL auslesen
                PhonebookEntryUniqueID = CInt(.Item("NewPhonebookEntryUniqueID"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"SetPhonebookEntryUID konnte nicht aufgelöst werden. '{ .Item("Error")}'")
                PhonebookEntryUniqueID = -1

                Return False
            End If
        End With

    End Function

    Public Function DeletePhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer) As Boolean Implements IX_contactSCPD.DeletePhonebookEntry

        With TR064Start(ServiceFile, "DeletePhonebookEntry", New Hashtable From {{"NewPhonebookID", PhonebookID},
                                                                                                      {"NewPhonebookEntryID", PhonebookEntryID}})
            Return Not .ContainsKey("Error")

        End With
    End Function

    Public Function DeletePhonebookEntryUID(PhonebookID As Integer, NewPhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.DeletePhonebookEntryUID

        With TR064Start(ServiceFile, "DeletePhonebookEntryUID", New Hashtable From {{"NewPhonebookID", PhonebookID},
                                                                                                         {"NewPhonebookEntryUniqueID", NewPhonebookEntryUniqueID}})
            Return Not .ContainsKey("Error")

        End With

    End Function
#End Region

#Region "CallBarring"
    Public Function GetCallBarringEntry(PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetCallBarringEntry

        With TR064Start(ServiceFile, "GetCallBarringEntry", New Hashtable From {{"NewPhonebookEntryID", PhonebookEntryID}})

            If .ContainsKey("NewPhonebookEntryData") Then
                ' Phonebook URL auslesen
                PhonebookEntryData = .Item("NewPhonebookEntryData").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Rufsperre aus Telefonbuch {PhonebookEntryID} der Fritz!Box ausgelesen: '{PhonebookEntryData}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetCallBarringEntry konnte für die ID {PhonebookEntryID} nicht aufgelöst werden. '{ .Item("Error")}'")

                PhonebookEntryData = String.Empty

                Return False
            End If
        End With

    End Function


    Public Function GetCallBarringEntryByNum(Number As String, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetCallBarringEntryByNum

        With TR064Start(ServiceFile, "GetCallBarringEntryByNum", New Hashtable From {{"NewNumber", Number}})

            If .ContainsKey("NewPhonebookEntryData") Then
                ' Phonebook URL auslesen
                PhonebookEntryData = .Item("NewPhonebookEntryData").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Rufsperre für die Nummer {Number} der Fritz!Box ausgelesen: '{PhonebookEntryData}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetCallBarringEntryByNum konnte für die Nummer {Number} nicht aufgelöst werden. '{ .Item("Error")}'")

                PhonebookEntryData = String.Empty

                Return False
            End If
        End With

    End Function

    Public Function GetCallBarringList(ByRef PhonebookURL As String) As Boolean Implements IX_contactSCPD.GetCallBarringList

        With TR064Start(ServiceFile, "GetCallBarringList", Nothing)

            If .ContainsKey("NewPhonebookURL") Then
                ' Phonebook URL auslesen
                PhonebookURL = .Item("NewPhonebookURL").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Pfad zur Rufsperre der Fritz!Box: {PhonebookURL} ")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetCallBarringList konnte für die Rufsperre nicht aufgelöst werden. '{ .Item("Error")}'")
                PhonebookURL = String.Empty

                Return False
            End If
        End With

    End Function

    Public Function SetCallBarringEntry(PhonebookEntryData As String, Optional ByRef PhonebookEntryUniqueID As Integer = 0) As Boolean Implements IX_contactSCPD.SetCallBarringEntry

        With TR064Start(ServiceFile, "SetCallBarringEntry", New Hashtable From {{"NewPhonebookEntryData", PhonebookEntryData}})

            If .ContainsKey("NewPhonebookEntryUniqueID") Then
                ' Phonebook URL auslesen
                PhonebookEntryUniqueID = CInt(.Item("NewPhonebookEntryUniqueID"))

                PushStatus.Invoke(LogLevel.Debug, $"Rufsperre in der Fritz!Box angelegt: '{PhonebookEntryUniqueID}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"SetCallBarringEntry konnte keinen Eintrag anlegen: '{PhonebookEntryData}' '{ .Item("Error")}'")

                PhonebookEntryUniqueID = -1

                Return False
            End If
        End With

    End Function

    Public Function DeleteCallBarringEntryUID(NewPhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.DeleteCallBarringEntryUID

        With TR064Start(ServiceFile, "DeleteCallBarringEntryUID", New Hashtable From {{"NewPhonebookEntryUniqueID", NewPhonebookEntryUniqueID}})
            Return Not .ContainsKey("Error")

        End With

    End Function

#End Region

#Region "DECTHandset"
    Public Function GetDECTHandsetList(ByRef DectIDList As Integer()) As Boolean Implements IX_contactSCPD.GetDECTHandsetList

        With TR064Start(ServiceFile, "GetDECTHandsetList", Nothing)

            If .ContainsKey("NewDectIDList") Then
                ' Comma separated list of DectID 
                DectIDList = Array.ConvertAll(.Item("NewDectIDList").ToString.Split(","), New Converter(Of String, Integer)(AddressOf Integer.Parse))

                PushStatus.Invoke(LogLevel.Debug, $"Liste der DECT IDs der Fritz!Box: '{String.Join(", ", DectIDList)}'")

                Return True
            Else
                DectIDList = {}

                PushStatus.Invoke(LogLevel.Warn, $"Liste der DECT IDs der Fritz!Box konnte nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetDECTHandsetInfo(DectID As Integer,
                                       ByRef HandsetName As String,
                                       ByRef PhonebookID As String) As Boolean Implements IX_contactSCPD.GetDECTHandsetInfo

        With TR064Start(ServiceFile, "GetDECTHandsetInfo", New Hashtable From {{"NewDectID", DectID}})

            If .ContainsKey("NewHandsetName") And .ContainsKey("NewPhonebookID") Then

                HandsetName = .Item("NewHandsetName").ToString
                PhonebookID = .Item("NewPhonebookID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"DECTHandsetInfo {DectID}: Name: {HandsetName} PhonebookID: {PhonebookID}")


                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"DECTHandsetInfo konnte für das DECTHandset {DectID} nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function


    Public Function SetDECTHandsetPhonebook(DectID As Integer, PhonebookID As Integer) As Boolean Implements IX_contactSCPD.SetDECTHandsetPhonebook

        With TR064Start(ServiceFile, "SetDECTHandsetPhonebook", New Hashtable From {{"NewDectID", DectID}, {"NewPhonebookID", PhonebookID}})
            Return Not .ContainsKey("Error")
        End With

    End Function
#End Region

#Region "Deflections"
    Public Function GetNumberOfDeflections(ByRef NumberOfDeflections As String) As Boolean Implements IX_contactSCPD.GetNumberOfDeflections

        With TR064Start(ServiceFile, "GetNumberOfDeflections", Nothing)

            If .ContainsKey("NewNumberOfDeflections") Then
                ' Phonebook URL auslesen
                NumberOfDeflections = .Item("NewNumberOfDeflections").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Anzahl der Rufweiterleitungen aus der Fritz!Box ausgelesen: '{NumberOfDeflections}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfDeflections konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                NumberOfDeflections = String.Empty

                Return False
            End If
        End With

    End Function

    Public Function GetDeflection(ByRef DeflectionInfo As Deflection, DeflectionId As Integer) As Boolean Implements IX_contactSCPD.GetDeflection

        If DeflectionInfo Is Nothing Then DeflectionInfo = New Deflection

        With TR064Start(ServiceFile, "GetInfo", New Hashtable From {{"NewDeflectionId", DeflectionId}})

            If .ContainsKey("NewEnable") Then

                DeflectionInfo.Enable = CBool(.Item("NewEnable"))
                DeflectionInfo.Type = CType(.Item("NewType"), DeflectionType)
                DeflectionInfo.Number = .Item("NewNumber").ToString
                DeflectionInfo.DeflectionToNumber = .Item("NewDeflectionToNumber").ToString
                DeflectionInfo.Mode = CType(.Item("NewMode"), DeflectionMode)
                DeflectionInfo.Outgoing = .Item("NewOutgoing").ToString
                DeflectionInfo.PhonebookID = .Item("NewPhonebookID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetDeflection ({DeflectionId}): {DeflectionInfo.Mode}; {DeflectionInfo.Enable}")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetDeflection konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetDeflections(ByRef DeflectionList As DeflectionList) As Boolean Implements IX_contactSCPD.GetDeflections

        With TR064Start(ServiceFile, "GetDeflections", Nothing)

            If .ContainsKey("NewDeflectionList") Then

                If Not DeserializeXML(.Item("NewDeflectionList").ToString(), False, DeflectionList) Then
                    PushStatus.Invoke(LogLevel.Warn, $"GetDeflections konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine Umleitung angeschlossen wurden, gib eine leere Klasse zurück
                If DeflectionList Is Nothing Then DeflectionList = New DeflectionList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetDeflections konnte nicht aufgelöst werden. '{ .Item("Error")}'")

                DeflectionList = Nothing

                Return False
            End If
        End With

    End Function

    Public Function SetDeflectionEnable(DeflectionId As Integer, Enable As Boolean) As Boolean Implements IX_contactSCPD.SetDeflectionEnable

        With TR064Start(ServiceFile, "SetDeflectionEnable", New Hashtable From {{"NewDeflectionId", DeflectionId}, {"NewEnable", Enable.ToString}})

            Return Not .ContainsKey("Error")

        End With

    End Function
#End Region
End Class
