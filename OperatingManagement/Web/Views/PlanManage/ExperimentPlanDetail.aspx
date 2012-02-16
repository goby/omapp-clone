﻿<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ExperimentPlanDetail.aspx.cs"
    Inherits="OperatingManagement.Web.Views.PlanManage.ExperimentPlanDetail" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 800px;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    试验计划明细
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
    <table cellpadding="0" class="style1 edit1">
        <tr>
            <th style="width: 180px;">
                时间
            </th>
            <td>
                <asp:TextBox ID="txtSYTime" runat="server" CssClass="text" MaxLength="10" onclick="setday(this);"
                    Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 180px;">
                试验个数
            </th>
            <td>
                <asp:TextBox ID="txtSYCount" runat="server" CssClass="text" MaxLength="10" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 180px;">
                &nbsp;
            </th>
            <td>
                     <asp:HiddenField ID="HfID" runat="server" />
                     <asp:HiddenField ID="HfFileIndex" runat="server" />
            </td>
        </tr>
    </table>
    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"
        OnItemCommand="Repeater1_ItemCommand">
        <ItemTemplate>
            <table class="edit1" style="width: 800px;">
                <tr>
                    <th style="width: 180px;" rowspan="7">
                        试验
                    </th>
                    <th style="width: 140px;">
                        卫星名称
                    </th>
                    <td>
                        <asp:TextBox ID="txtSYSatName" CssClass="text" runat="server" Text='<%# Eval("SYSatName")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        试验类别
                    </th>
                    <td>
                        <asp:TextBox ID="txtSYType" CssClass="text" runat="server" Text='<%# Eval("SYType")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        试验项目
                    </th>
                    <td>
                        <asp:TextBox ID="txtSYItem" CssClass="text" runat="server" Text='<%# Eval("SYItem")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        开始时间
                    </th>
                    <td>
                        <asp:TextBox ID="txtStartTime" CssClass="text" runat="server" Text='<%# Eval("SYStartTime")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        结束时间
                    </th>
                    <td>
                        <asp:TextBox ID="txtEndTime" CssClass="text" runat="server" Text='<%# Eval("SYEndTime")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        系统名称
                    </th>
                    <td>
                        <asp:TextBox ID="txtSYSysName" CssClass="text" runat="server" Text='<%# Eval("SYSysName")%>'></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 140px;">
                        系统任务
                    </th>
                    <td>
                        <asp:TextBox ID="txtSYSysTask" CssClass="text" runat="server" Text='<%# Eval("SYSysTask")%>'></asp:TextBox>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:Repeater>
</asp:Content>
