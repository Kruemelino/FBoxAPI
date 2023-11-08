Imports System.Reflection
''' <summary>
''' TR-064 Support – X AVM Media 
''' Date: 2022-02-25
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_mediaSCPD.pdf</see>
''' </summary>
Friend Class X_MediaSCPD
    Implements IX_MediaSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 25) Implements IX_MediaSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_MediaSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_mediaSCPD Implements IX_MediaSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_MediaSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef DVBCEnabled As Boolean, ByRef StationSearchStatus As StationSearchStatusEnum, ByRef SearchProgress As Integer) As Boolean Implements IX_MediaSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewDVBCEnabled", DVBCEnabled) And
                   .TryGetValueEx("NewStationSearchStatus", StationSearchStatus) And
                   .TryGetValueEx("NewSearchProgress", SearchProgress)
        End With
    End Function

    Public Function GetDVBCEnable(ByRef DVBCEnabled As Boolean) As Boolean Implements IX_MediaSCPD.GetDVBCEnable
        Return TR064Start(ServiceFile, "GetDVBCEnable", ServiceID, Nothing).TryGetValueEx("NewDVBCEnabled", DVBCEnabled)
    End Function

    Public Function SetDVBCEnable(DVBCEnabled As Boolean) As Boolean Implements IX_MediaSCPD.SetDVBCEnable
        Return Not TR064Start(ServiceFile, "SetDVBCEnable", ServiceID, New Dictionary(Of String, String) From {{"NewDVBCEnabled", DVBCEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function StationSearch(StationSearchMode As StationSearchModeEnum, ByRef StationSearchStatus As StationSearchStatusEnum) As Boolean Implements IX_MediaSCPD.StationSearch
        Return Not TR064Start(ServiceFile, "StationSearch", ServiceID, New Dictionary(Of String, String) From {{"NewStationSearchMode", StationSearchMode.ToString}}).TryGetValueEx("NewStationSearchStatus", StationSearchStatus)
    End Function

    Public Function GetSearchProgress(ByRef StationSearchStatus As StationSearchStatusEnum, ByRef SearchProgress As Integer) As Boolean Implements IX_MediaSCPD.GetSearchProgress
        With TR064Start(ServiceFile, "GetSearchProgress", ServiceID, Nothing)
            Return .TryGetValueEx("NewStationSearchStatus", StationSearchStatus) And
                   .TryGetValueEx("NewSearchProgress", SearchProgress)
        End With
    End Function
End Class
