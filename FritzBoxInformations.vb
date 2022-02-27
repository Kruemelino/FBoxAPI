Imports System.ComponentModel
Public Module FritzBoxInformations

#Region "Fritz!Box SOAP/TR-064"
    Friend Const DfltTR064Port As Integer = 49000
    Friend Const DfltTR064PortSSL As Integer = 49443
#End Region

#Region "Fritz!Box UPnP/TR-064 Files"

    ''' <summary>
    ''' ServiceControlProtocolDefinitions 
    ''' </summary>
    Public Enum SCPDFiles

        '<Description("/any.xml")> any

        <Description("/aura.xml")> auradesc

        <Description("/aura-scpd.xml")> auraSCPD

        <Description("/deviceconfigSCPD.xml")> deviceconfigSCPD

        <Description("/deviceinfoSCPD.xml")> deviceinfoSCPD

        <Description("/ethifconfigSCPD.xml")> ethifconfigSCPD

        <Description("/hostsSCPD.xml")> hostsSCPD

        '<Description("/igdconnSCPD.xml")> igdconnSCPD

        '<Description("/igddesc.xml")> igddesc

        '<Description("/igddslSCPD.xml")> igddslSCPD

        '<Description("/igdicfgSCPD.xml")> igdicfgSCPD

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
