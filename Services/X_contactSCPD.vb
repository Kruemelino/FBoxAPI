﻿Imports System.Reflection
Imports System.Xml
''' <summary>
''' TR-064 Support – X_AVM-DE_OnTel
''' Date: 2021-02-09
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf</see>
''' </summary>
Friend Class X_contactSCPD
    Implements IX_contactSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_contactSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_contactSCPD Implements IX_contactSCPD.Servicefile
    Private Property XML As Serializer
    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)),
                   XMLSerializer As Serializer)

        TR064Start = Start
        XML = XMLSerializer
    End Sub

    <Obsolete>
    Public Function GetInfoByIndex(Index As Integer,
                                   Optional ByRef Enable As Boolean = False,
                                   Optional ByRef Status As String = "",
                                   Optional ByRef LastConnect As String = "",
                                   Optional ByRef Url As String = "",
                                   Optional ByRef ServiceId As String = "",
                                   Optional ByRef Username As String = "",
                                   Optional ByRef Name As String = "") As Boolean Implements IX_contactSCPD.GetInfoByIndex

        With TR064Start(ServiceFile, "GetInfoByIndex", New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})


            Return .TryGetValueEx("NewEnable", Enable) And
                   .TryGetValueEx("NewStatus", Status) And
                   .TryGetValueEx("NewLastConnect", LastConnect) And
                   .TryGetValueEx("NewUrl", Url) And
                   .TryGetValueEx("NewServiceId", ServiceId) And
                   .TryGetValueEx("NewUsername", Username) And
                   .TryGetValueEx("NewName", Name)

        End With

    End Function

    Public Function GetInfoByIndex(Index As Integer, ByRef Info As OnTelInfo) As Boolean Implements IX_contactSCPD.GetInfoByIndex
        If Info Is Nothing Then Info = New OnTelInfo With {.Index = Index}

        With TR064Start(ServiceFile, "GetInfoByIndex", New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

            Return .TryGetValueEx("NewEnable", Info.Enable) And
                   .TryGetValueEx("NewStatus", Info.Status) And
                   .TryGetValueEx("NewLastConnect", Info.LastConnect) And
                   .TryGetValueEx("NewUrl", Info.Url) And
                   .TryGetValueEx("NewServiceId", Info.ServiceId) And
                   .TryGetValueEx("NewUsername", Info.Username) And
                   .TryGetValueEx("NewName", Info.Name)
        End With
    End Function

    Public Function SetEnableByIndex(Index As Integer, Enable As Boolean) As Boolean Implements IX_contactSCPD.SetEnableByIndex
        Return Not TR064Start(ServiceFile, "SetEnableByIndex",
                              New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                      {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetConfigByIndex(Index As Integer, Enable As Boolean, Url As String, ServiceId As String, Username As String, Password As String, Name As String) As Boolean Implements IX_contactSCPD.SetConfigByIndex
        Return Not TR064Start(ServiceFile, "SetConfigByIndex",
                              New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                      {"NewEnable", Enable.ToBoolStr},
                                                                      {"NewUrl", Url},
                                                                      {"NewServiceId", ServiceId},
                                                                      {"NewUsername", Username},
                                                                      {"NewPassword", Password},
                                                                      {"NewName", Name}}).ContainsKey("Error")
    End Function

    Public Function GetNumberOfEntries(ByRef OntelNumberOfEntries As Integer) As Boolean Implements IX_contactSCPD.GetNumberOfEntries
        Return TR064Start(ServiceFile, "GetNumberOfEntries", Nothing).TryGetValueEx("NewOntelNumberOfEntries", OntelNumberOfEntries)
    End Function

    Public Function DeleteByIndex(Index As Integer) As Boolean Implements IX_contactSCPD.DeleteByIndex
        Return Not TR064Start(ServiceFile, "DeleteByIndex",
                              New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetCallList(ByRef CallListURL As String) As Boolean Implements IX_contactSCPD.GetCallList
        Return TR064Start(ServiceFile, "GetCallList", Nothing).TryGetValueEx("NewCallListURL", CallListURL)
    End Function

    Public Async Function GetCallList() As Task(Of CallList) Implements IX_contactSCPD.GetCallList
        Return Await XML.DeserializeAsyncFromPath(Of CallList)((TR064Start(ServiceFile,
                                                                           "GetCallList",
                                                                           Nothing)).TryGetValueEx(Of String)("NewCallListURL"))
    End Function

#Region "Phonebook"
    Public Function GetPhonebookList(ByRef PhonebookList As Integer()) As Boolean Implements IX_contactSCPD.GetPhonebookList

        With TR064Start(ServiceFile, "GetPhonebookList", Nothing)

            If .ContainsKey("NewPhonebookList") Then
                ' Comma separated list of PhonebookID 
                PhonebookList = Array.ConvertAll(.Item("NewPhonebookList").Split(","c), New Converter(Of String, Integer)(AddressOf Integer.Parse))

                Return True
            Else
                PhonebookList = Array.Empty(Of Integer)

                Return False
            End If
        End With

    End Function

    Public Function GetPhonebook(PhonebookID As Integer,
                                 ByRef PhonebookURL As String,
                                 Optional ByRef PhonebookName As String = "",
                                 Optional ByRef PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.GetPhonebook

        With TR064Start(ServiceFile, "GetPhonebook", New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString}})

            Return .TryGetValueEx("NewPhonebookURL", PhonebookURL) And
                   .TryGetValueEx("NewPhonebookName", PhonebookName) And
                   .TryGetValueEx("NewPhonebookExtraID", PhonebookExtraID)
        End With

    End Function

    Public Async Function GetPhonebook(PhonebookID As Integer) As Task(Of PhonebooksType) Implements IX_contactSCPD.GetPhonebook

        ' Lade die xslt Transformationsdatei
        Dim xslt As New Xsl.XslCompiledTransform
        xslt.Load(XmlReader.Create(Assembly.GetExecutingAssembly.GetManifestResourceStream("FBoxAPI.ToLower.xslt")))

        'Ermittle den Pfad zum Telefonbuch und deserialisiere die Daten
        Return Await XML.DeserializeAsyncFromPath(Of PhonebooksType)((TR064Start(ServiceFile,
                                                                                 "GetPhonebook",
                                                                                 New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString}})).TryGetValueEx(Of String)("NewPhonebookURL"),
                                                                     xslt)
    End Function

    Public Function AddPhonebook(PhonebookName As String, Optional PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.AddPhonebook
        Return Not TR064Start(ServiceFile,
                                   "AddPhonebook",
                                   New Dictionary(Of String, String) From {{"NewPhonebookName", PhonebookName},
                                                                           {"NewPhonebookExtraID", PhonebookExtraID}}).ContainsKey("Error")
    End Function

    Public Function DeletePhonebook(NewPhonebookID As Integer, Optional PhonebookExtraID As String = "") As Boolean Implements IX_contactSCPD.DeletePhonebook
        Return Not TR064Start(ServiceFile,
                                   "DeletePhonebook",
                                   New Dictionary(Of String, String) From {{"NewPhonebookID", NewPhonebookID.ToString},
                                                                           {"NewPhonebookExtraID", PhonebookExtraID}}).ContainsKey("Error")
    End Function
#End Region

#Region "PhonebookEntry"
    Public Function GetPhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetPhonebookEntry
        Return TR064Start(ServiceFile, "GetPhonebookEntry",
                          New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString},
                                                                  {"NewPhonebookEntryID", PhonebookEntryID.ToString}}).
                          TryGetValueEx("NewPhonebookEntryData", PhonebookEntryData)
    End Function

    Public Function GetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryUniqueID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetPhonebookEntryUID
        Return TR064Start(ServiceFile, "GetPhonebookEntryUID",
                          New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString},
                                                                  {"NewPhonebookEntryUniqueID", PhonebookEntryUniqueID.ToString}}).
                          TryGetValueEx("NewPhonebookEntryData", PhonebookEntryData)
    End Function

    Public Function SetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryData As String, ByRef PhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.SetPhonebookEntryUID
        Return TR064Start(ServiceFile, "SetPhonebookEntryUID",
                          New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString},
                                                                  {"NewPhonebookEntryData", PhonebookEntryData}}).
                          TryGetValueEx("NewPhonebookEntryUniqueID", PhonebookEntryUniqueID)
    End Function

    Public Function DeletePhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer) As Boolean Implements IX_contactSCPD.DeletePhonebookEntry
        Return Not TR064Start(ServiceFile, "DeletePhonebookEntry", New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString},
                                                                                                           {"NewPhonebookEntryID", PhonebookEntryID.ToString}}).ContainsKey("Error")

    End Function

    Public Function DeletePhonebookEntryUID(PhonebookID As Integer, NewPhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.DeletePhonebookEntryUID
        Return Not TR064Start(ServiceFile, "DeletePhonebookEntryUID", New Dictionary(Of String, String) From {{"NewPhonebookID", PhonebookID.ToString},
                                                                                                              {"NewPhonebookEntryUniqueID", NewPhonebookEntryUniqueID.ToString}}).ContainsKey("Error")
    End Function
#End Region

#Region "CallBarring"
    Public Function GetCallBarringEntry(PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetCallBarringEntry
        Return TR064Start(ServiceFile,
                          "GetCallBarringEntry",
                          New Dictionary(Of String, String) From {{"NewPhonebookEntryID", PhonebookEntryID.ToString}}).
                          TryGetValueEx("NewPhonebookEntryData", PhonebookEntryData)
    End Function

    Public Async Function GetCallBarringEntry(PhonebookEntryID As Integer) As Task(Of Contact) Implements IX_contactSCPD.GetCallBarringEntry
        ' Ermittle den Pfad zu Rufsperre und deserialisiere die Daten
        Return Await XML.DeserializeAsyncData(Of Contact)((TR064Start(ServiceFile,
                                                                      "GetCallBarringEntry",
                                                                      New Dictionary(Of String, String) From {{"NewPhonebookEntryID", PhonebookEntryID.ToString}})).
                                                                      TryGetValueEx(Of String)("NewPhonebookEntryData"))
    End Function

    Public Function GetCallBarringEntryByNum(Number As String, ByRef PhonebookEntryData As String) As Boolean Implements IX_contactSCPD.GetCallBarringEntryByNum
        Return TR064Start(ServiceFile, "GetCallBarringEntryByNum",
                          New Dictionary(Of String, String) From {{"NewNumber", Number}}).
                          TryGetValueEx("NewPhonebookEntryData", PhonebookEntryData)
    End Function

    Public Async Function GetCallBarringEntryByNum(Number As String) As Task(Of Contact) Implements IX_contactSCPD.GetCallBarringEntryByNum
        ' Ermittle den Pfad zu Rufsperre und deserialisiere die Daten
        Return Await XML.DeserializeAsyncData(Of Contact)((TR064Start(ServiceFile,
                                                                      "GetCallBarringList",
                                                                      New Dictionary(Of String, String) From {{"NewNumber", Number}})).
                                                                      TryGetValueEx(Of String)("NewPhonebookEntryData"))
    End Function


    Public Function GetCallBarringList(ByRef PhonebookURL As String) As Boolean Implements IX_contactSCPD.GetCallBarringList
        Return TR064Start(ServiceFile, "GetCallBarringList", Nothing).TryGetValueEx("NewPhonebookURL", PhonebookURL)
    End Function

    Public Async Function GetCallBarringList() As Task(Of PhonebooksType) Implements IX_contactSCPD.GetCallBarringList
        ' Lade die xslt Transformationsdatei
        Dim xslt As New Xsl.XslCompiledTransform
        xslt.Load(XmlReader.Create(Assembly.GetExecutingAssembly.GetManifestResourceStream("FBoxAPI.ToLower.xslt")))

        ' Ermittle den Pfad zu Rufsperre und deserialisiere die Daten
        Return Await XML.DeserializeAsyncFromPath(Of PhonebooksType)((TR064Start(ServiceFile,
                                                                                 "GetCallBarringList",
                                                                                 Nothing)).TryGetValueEx(Of String)("NewPhonebookURL"),
                                                                     xslt)
    End Function

    Public Function SetCallBarringEntry(PhonebookEntryData As String, Optional ByRef PhonebookEntryUniqueID As Integer = 0) As Boolean Implements IX_contactSCPD.SetCallBarringEntry
        Return TR064Start(ServiceFile, "SetCallBarringEntry",
                          New Dictionary(Of String, String) From {{"NewPhonebookEntryData", PhonebookEntryData}}).
                          TryGetValueEx("NewPhonebookEntryUniqueID", PhonebookEntryUniqueID)
    End Function

    Public Function DeleteCallBarringEntryUID(NewPhonebookEntryUniqueID As Integer) As Boolean Implements IX_contactSCPD.DeleteCallBarringEntryUID
        Return Not TR064Start(ServiceFile, "DeleteCallBarringEntryUID",
                              New Dictionary(Of String, String) From {{"NewPhonebookEntryUniqueID", NewPhonebookEntryUniqueID.ToString}}).ContainsKey("Error")
    End Function

#End Region

#Region "DECTHandset"
    Public Function GetDECTHandsetList(ByRef DectIDList As String()) As Boolean Implements IX_contactSCPD.GetDECTHandsetList

        With TR064Start(ServiceFile, "GetDECTHandsetList", Nothing)

            If .ContainsKey("NewDectIDList") Then
                ' Comma separated list of DectID 
                DectIDList = .Item("NewDectIDList").Split(","c)

                Return True
            Else
                DectIDList = Array.Empty(Of String)

                Return False
            End If
        End With

    End Function

    Public Function GetDECTHandsetInfo(DectID As Integer,
                                       ByRef HandsetName As String,
                                       ByRef PhonebookID As String) As Boolean Implements IX_contactSCPD.GetDECTHandsetInfo

        With TR064Start(ServiceFile, "GetDECTHandsetInfo", New Dictionary(Of String, String) From {{"NewDectID", DectID.ToString}})

            Return .TryGetValueEx("NewHandsetName", HandsetName) And
                   .TryGetValueEx("NewPhonebookID", PhonebookID)
        End With

    End Function


    Public Function SetDECTHandsetPhonebook(DectID As Integer, PhonebookID As Integer) As Boolean Implements IX_contactSCPD.SetDECTHandsetPhonebook
        Return Not TR064Start(ServiceFile, "SetDECTHandsetPhonebook", New Dictionary(Of String, String) From {{"NewDectID", DectID.ToString},
                                                                                                              {"NewPhonebookID", PhonebookID.ToString}}).ContainsKey("Error")
    End Function
#End Region

#Region "Deflections"
    Public Function GetNumberOfDeflections(ByRef NumberOfDeflections As String) As Boolean Implements IX_contactSCPD.GetNumberOfDeflections
        Return TR064Start(ServiceFile, "GetNumberOfDeflections", Nothing).TryGetValueEx("NewNumberOfDeflections", NumberOfDeflections)
    End Function

    Public Function GetDeflection(ByRef DeflectionInfo As Deflection, DeflectionId As Integer) As Boolean Implements IX_contactSCPD.GetDeflection

        If DeflectionInfo Is Nothing Then DeflectionInfo = New Deflection

        With TR064Start(ServiceFile, "GetInfo", New Dictionary(Of String, String) From {{"NewDeflectionId", DeflectionId.ToString}})

            Return .TryGetValueEx("NewEnable", DeflectionInfo.Enable) And
                   .TryGetValueEx("NewType", DeflectionInfo.Type) And
                   .TryGetValueEx("NewNumber", DeflectionInfo.Number) And
                   .TryGetValueEx("NewDeflectionToNumber", DeflectionInfo.DeflectionToNumber) And
                   .TryGetValueEx("NewType", DeflectionInfo.DeflectionToNumber) And
                   .TryGetValueEx("NewMode", DeflectionInfo.Outgoing) And
                   .TryGetValueEx("NewPhonebookID", DeflectionInfo.PhonebookID)
        End With

    End Function

    Public Function GetDeflections(ByRef DeflectionList As String) As Boolean Implements IX_contactSCPD.GetDeflections
        Return TR064Start(ServiceFile, "GetDeflections", Nothing).TryGetValueEx("NewDeflectionList", DeflectionList)
    End Function

    Public Function GetDeflections(ByRef DeflectionList As DeflectionList) As Boolean Implements IX_contactSCPD.GetDeflections

        Dim Deflections As String = String.Empty
        If GetDeflections(Deflections) Then
            XML.Deserialize(Deflections, False, DeflectionList)
            Return True
        Else
            DeflectionList = New DeflectionList
            Return False
        End If

    End Function

    Public Async Function GetDeflections() As Task(Of DeflectionList) Implements IX_contactSCPD.GetDeflections
        'Ermittle Die Rufbehandlungen und deserialisiere die Daten
        Return Await XML.DeserializeAsyncData(Of DeflectionList)((TR064Start(ServiceFile,
                                                                             "GetDeflections",
                                                                             Nothing)).TryGetValueEx(Of String)("NewDeflectionList"))
    End Function

    Public Function SetDeflectionEnable(DeflectionId As Integer, Enable As Boolean) As Boolean Implements IX_contactSCPD.SetDeflectionEnable
        Return Not TR064Start(ServiceFile, "SetDeflectionEnable",
                              New Dictionary(Of String, String) From {{"NewDeflectionId", DeflectionId.ToString},
                                                                      {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function
#End Region
End Class
