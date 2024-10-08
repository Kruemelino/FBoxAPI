﻿''' <summary>
''' TR-064 Support – Hosts
''' Date: 2024-07-02
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf</see>
''' </summary>
Public Interface IHostsSCPD
    Inherits IServiceBase

    Function GetHostNumberOfEntries(ByRef HostNumberOfEntries As Integer) As Boolean
    Function GetSpecificHostEntry(MACAddress As String, ByRef Host As HostEntry) As Boolean
    Function GetGenericHostEntry(HostNumberOfEntries As Integer, ByRef Host As HostEntry) As Boolean
    Function GetInfo(ByRef Info As HostsInfo) As Boolean
    Function GetChangeCounter(ByRef ChangeCounter As Integer) As Boolean
    Function GetAutoWakeOnLANByMACAddress(MACAddress As String, ByRef AutoWOLEnabled As Boolean) As Boolean
    Function SetAutoWakeOnLANByMACAddress(MACAddress As String, AutoWOLEnabled As Boolean) As Boolean
    Function SetHostNameByMACAddress(MACAddress As String, HostName As String) As Boolean
    Function WakeOnLANByMACAddress(MACAddress As String) As Boolean
    Function GetSpecificHostEntryByIP(IPAddress As String, ByRef Host As HostEntry) As Boolean
    Function HostsCheckUpdate() As Boolean
    Function HostDoUpdate(MACAddress As String) As Boolean

    ''' <summary>
    ''' Activate or deactivate the realtime priority for the host with the given IP address
    ''' </summary>
    ''' <remarks>Required rights: App or Phone</remarks>
    Function SetPrioritizationByIP(IPAddress As String, Priority As Boolean) As Boolean

    ''' <summary>
    ''' Gets a path to a lua script file, which generates an XML structured list of hosts.
    ''' </summary>
    ''' <param name="HostListPath">Related path to lua script which generates a formatted list
    ''' <br/>
    ''' e. g. /devicehostlist.lua?sid=97ebd547080a032a
    ''' </param>
    ''' <remarks>Required rights : App or Phone</remarks>
    Function GetHostListPath(ByRef HostListPath As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: X_AVM-DE_GetHostListPath wird als <see cref="HostList"/> deserialisiert zurückgegeben.
    ''' </summary>
    <Obsolete("This function is obsolete and will be removed in a future version. Use the Function GetHostList() As Task(Of HostList) instead.")>
    Function GetHostList(ByRef Hosts As HostList) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: X_AVM-DE_GetHostListPath wird als <see cref="HostList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetHostList() As Task(Of HostList)

    ''' <summary>
    ''' Gets a path to a lua script file, which generates an json structured list with mesh topology information
    ''' </summary>
    ''' <param name="HostListPath">Related path to lua script which generates a formatted list</param>
    ''' <remarks>Required rights : PhoneRight, AppRight<br/>
    ''' For details on how to interpret the mesh topology information please refer to the JSON schema matching the corresponding FRITZ!OS version.
    ''' <list type="table">
    ''' <listheader>
    ''' <term>FRITZ!OS version</term>
    ''' <description>JSON schema URL</description>
    ''' </listheader>
    ''' <item>
    ''' <term>07.20</term>
    ''' <description><see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mesh_topology/mesh_topology_schema_v1.9.json</see></description>
    ''' </item>
    ''' <item>
    ''' <term>07.15, 07.14, 07.13, 07.12, 07.11</term>
    ''' <description><see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mesh_topology/mesh_topology_schema_v1.6.json</see></description>
    ''' </item>
    ''' <item>
    ''' <term>07.10, 07.08</term>
    ''' <description><see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mesh_topology/mesh_topology_schema_v1.5.json</see></description>
    ''' </item>
    ''' </list>
    ''' </remarks>
    Function GetMeshListPath(ByRef MeshListPath As String) As Boolean

    ''' <summary>
    ''' Gets the own friendly name of the requesting client
    ''' </summary>
    Function GetFriendlyName(ByRef FriendlyName As String) As Boolean

    ''' <summary>
    ''' Sets the own friendly name only if the current friendly name was auto generated (e.g. PC-192.168.178.20) or from type android-123456789abcdef.
    ''' </summary>
    ''' <remarks>Required rights : App, Phone, NAS or HomeAutomation</remarks>
    Function SetFriendlyName(FriendlyName As String) As Boolean

    ''' <summary>
    ''' Sets the friendly name for device with given IP
    ''' </summary>
    ''' <returns>Required rights : App</returns>
    Function SetFriendlyNameByIP(IPAddress As String, FriendlyName As String) As Boolean

    ''' <summary>
    ''' Sets the friendly name for device with given MAC
    ''' </summary>
    ''' <returns>Required rights : App</returns>
    Function SetFriendlyNameByMAC(MACAddress As String, FriendlyName As String) As Boolean
End Interface
