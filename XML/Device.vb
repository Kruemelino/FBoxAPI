﻿Imports System.Xml.Serialization

<Serializable()> Public Class Device

    <XmlElement("deviceType")> Public Property DeviceType As String
    <XmlElement("friendlyName")> Public Property FriendlyName As String
    <XmlElement("manufacturer")> Public Property Manufacturer As String
    <XmlElement("manufacturerURL")> Public Property ManufacturerURL As String
    <XmlElement("modelDescription")> Public Property ModelDescription As String
    <XmlElement("modelName")> Public Property ModelName As String
    <XmlElement("modelNumber")> Public Property ModelNumber As String
    <XmlElement("modelURL")> Public Property Display As String
    <XmlElement("UDN")> Public Property UDN As String
    <XmlElement("UPC")> Public Property UPC As String
    <XmlElement("serialNumber")> Public Property SerialNumber As String
    <XmlElement("originUDN")> Public Property OriginUDN As String
    <XmlArray("iconList")> <XmlArrayItem("icon")> Public Property IconList As List(Of Icon)
    <XmlArray("serviceList")> <XmlArrayItem("service")> Public Property ServiceList As List(Of Service)
    <XmlArray("deviceList")> <XmlArrayItem("device")> Public Property DeviceList As List(Of Device)
    <XmlElement("presentationURL")> Public Property PresentationURL As String

    ''' <summary>
    ''' Rekursive Funktion, die für jedes Device die eigenen Services und die Services der jeweiligen Child-Devices zurückgibt
    ''' </summary>
    Friend Function GetAllServices() As List(Of Service)
        ' Erstelle eine neue Gesamtliste
        GetAllServices = New List(Of Service)
        ' Füge die Services dieses Devices hinzu
        GetAllServices.AddRange(ServiceList)

        ' rekursive Schleife durch alle Devices
        For Each D In DeviceList
            GetAllServices.AddRange(D.GetAllServices)
        Next

    End Function

End Class


