<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YDSJDetail.aspx.cs" Inherits="OperatingManagement.Web.PlanManage.YDSJDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <style type="text/css">
        .style1
        {
            width: 100%;
            border-collapse: collapse;
        }
        p.MsoNormal
        {
            margin-bottom: .0001pt;
            text-align: justify;
            text-justify: inter-ideograph;
            font-size: 10.5pt;
            font-family: "Times New Roman" , "serif";
            margin-left: 0cm;
            margin-right: 0cm;
            margin-top: 0cm;
        }
        .style2
        {
            width: 133px;
        }
        .style3
        {
            width: 134px;
        }
        .style4
        {
            width: 241px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" class="style1">
            <tr>
                <td class="style2">
                    版本：
                </td>
                <td class="style4">
                    <asp:Label ID="lblVersion" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    标志：
                </td>
                <td>
                    <asp:Label ID="lblFlag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    数据主类别：
                </td>
                <td class="style4">
                    <asp:Label ID="lblMainType" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    数据次类别：
                </td>
                <td>
                    <asp:Label ID="lblDataType" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    信源地址：
                </td>
                <td class="style4">
                    <asp:Label ID="lblSource" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    信宿地址：
                </td>
                <td>
                    <asp:Label ID="lblDestination" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    任务代号：
                </td>
                <td class="style4">
                    <asp:Label ID="lblMissionCode" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    卫星编号：
                </td>
                <td>
                    <asp:Label ID="lblSatelliteCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    数据日期：
                </td>
                <td class="style4">
                    <asp:Label ID="lblDataDate" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    数据时间：
                </td>
                <td>
                    <asp:Label ID="lblDataTime" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    &nbsp;
                </td>
                <td class="style4">
                    &nbsp;
                </td>
                <td class="style3">
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2">
                    历元日期：
                </td>
                <td class="style4">
                    <asp:Label ID="lblD" runat="server"></asp:Label>
                </td>
                <td class="style3">
                    历元时刻：
                </td>
                <td>
                    <asp:Label ID="lblT" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style2">
                    轨道半长轴：
                </td>
                <td class="style4">
                    <asp:Label ID="lblA" runat="server"></asp:Label>
                    &nbsp;0.1m
                </td>
                <td class="style3">
                    升交点赤经：
                </td>
                <td>
                    <asp:Label ID="lblOhm" runat="server"></asp:Label>
                    &nbsp;2<sup>-22</sup>度
                </td>
            </tr>
            <tr>
                <td class="style2">
                    轨道偏心率：
                </td>
                <td class="style4">
                    <asp:Label ID="lblE" runat="server"></asp:Label>
                    &nbsp;2<sup>-31</sup>
                </td>
                <td class="style3">
                    轨道倾角：
                </td>
                <td>
                    <asp:Label ID="lblI" runat="server"></asp:Label>
                    &nbsp;2<sup>-24</sup>度
                </td>
            </tr>
            <tr>
                <td class="style2">
                    近地点幅角：
                </td>
                <td class="style4">
                    <asp:Label ID="lblOmega" runat="server"></asp:Label>
                    &nbsp;2<sup>-22</sup>度
                </td>
                <td class="style3">
                    平近点角：
                </td>
                <td>
                    <asp:Label ID="lblM" runat="server"></asp:Label>
                    &nbsp;2<sup>-22</sup>度
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
