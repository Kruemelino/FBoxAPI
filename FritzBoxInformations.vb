Imports System.ComponentModel
Imports System.Xml.Schema
Public Module FritzBoxInformations

#Region "Properties"
#Region "Fritz!Box SOAP/TR64"
    Friend Const PropertyDfltFritzBoxAdress As String = "192.168.178.1"
    Friend Const DfltFritzBoxHostName As String = "fritz.box"
    Friend Const DfltFritzBoxSessionID As String = "0000000000000000"
    Friend Const DfltCodePageFritzBox As Integer = 65001
    Friend Const TR064ContentType As String = "text/xml; charset=""utf-8"""
    Friend Const TR064UserAgent As String = "AVM UPnP/1.0 Client 1.0"
    Friend Const DfltTR064Port As Integer = 49000
    Friend Const DfltTR064PortSSL As Integer = 49443
    Friend Const DfltTR064RequestNameSpaceEnvelope As String = "http://schemas.xmlsoap.org/soap/envelope/"
    Friend Const DfltTR064RequestNameSpaceEncoding As String = "http://schemas.xmlsoap.org/soap/encoding/"
    Friend ReadOnly Property DfltSOAPRequestSchema As XmlSchema
        Get
            Dim XMLSOAPSchema As New XmlSchema

            With XMLSOAPSchema.Namespaces
                .Add("s", DfltTR064RequestNameSpaceEnvelope)
                .Add("u", DfltTR064RequestNameSpaceEncoding)
            End With

            Return XMLSOAPSchema
        End Get
    End Property
#End Region

#End Region

#Region "Fritz!Box UPnP/TR-064 Files"

    ''' <summary>
    ''' ServiceControlProtocolDefinitions 
    ''' </summary>
    Public Enum SCPDFiles

        <Description("/any.xml")> any

        <Description("/deviceconfigSCPD.xml")> deviceconfigSCPD

        <Description("/deviceinfoSCPD.xml")> deviceinfoSCPD

        <Description("/ethifconfigSCPD.xml")> ethifconfigSCPD

        <Description("/hostsSCPD.xml")> hostsSCPD

        <Description("/igdconnSCPD.xml")> igdconnSCPD

        <Description("/igddesc.xml")> igddesc

        <Description("/igddslSCPD.xml")> igddslSCPD

        <Description("/igdicfgSCPD.xml")> igdicfgSCPD

        <Description("/lanconfigsecuritySCPD.xml")> lanconfigsecuritySCPD

        <Description("/lanhostconfigmgmSCPD.xml")> lanhostconfigmgmSCPD

        <Description("/lanifconfigSCPD.xml")> lanifconfigSCPD

        <Description("/layer3forwardingSCPD.xml")> layer3forwardingSCPD

        <Description("/mgmsrvSCPD.xml")> mgmsrvSCPD

        <Description("/timeSCPD.xml")> timeSCPD

        <Description("/tr64desc.xml")> tr64desc

        <Description("/userifSCPD.xml")> userifSCPD

        <Description("/wancommonifconfigSCPD.xml")> wancommonifconfigSCPD

        <Description("/wanethlinkconfigSCPD.xml")> wanethlinkconfigSCPD

        <Description("/wandslifconfigSCPD.xml")> wandslifconfigSCPD

        <Description("/wandsllinkconfigSCPD.xml")> wandsllinkconfigSCPD

        <Description("/wanipconnSCPD.xml")> wanipconnSCPD

        <Description("/wanpppconnSCPD.xml")> wanpppconnSCPD

        <Description("/wlanconfigSCPD.xml")> wlanconfigSCPD

        <Description("/x_appsetupSCPD.xml")> x_appsetupSCPD

        <Description("/x_authSCPD.xml")> x_authSCPD

        ''' <summary>
        ''' X_AVM-DE_OnTel
        ''' </summary>
        <Description("/x_contactSCPD.xml")> x_contactSCPD

        <Description("/x_dectSCPD.xml")> x_dectSCPD

        <Description("/x_filelinksSCPD.xml")> x_filelinksSCPD

        <Description("/x_homeautoSCPD.xml")> x_homeautoSCPD

        <Description("/x_homeplugSCPD.xml")> x_homeplugSCPD

        <Description("/x_hostfilterSCPD.xml")> x_hostfilterSCPD

        <Description("/x_myfritzSCPD.xml")> x_myfritzSCPD

        <Description("/x_remoteSCPD.xml")> x_remoteSCPD

        <Description("/x_storageSCPD.xml")> x_storageSCPD

        <Description("/x_speedtestSCPD.xml")> x_speedtestSCPD

        <Description("/x_tamSCPD.xml")> x_tamSCPD

        <Description("/x_upnpSCPD.xml")> x_upnpSCPD

        <Description("/x_voipSCPD.xml")> x_voipSCPD

        <Description("/x_webdavSCPD.xml")> x_webdavSCPD

    End Enum

#End Region
End Module
