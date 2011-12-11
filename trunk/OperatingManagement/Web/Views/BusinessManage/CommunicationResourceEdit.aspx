﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CommunicationResourceEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.CommunicationResourceEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .norText
        {
            width: 155px;
            margin: 0px;
            padding: 0px;
        }
        .norDpl
        {
            width: 160px;
            margin: 0px;
            padding: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuIndex" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理&gt;通信资源编辑
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 200px;">
                线路名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtRouteName" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtRouteName" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                线路编码(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtRouteCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtRouteCode" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                方向(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplDirection" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplDirection" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                带宽(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtBandWidth" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtBandWidth" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                创建时间
            </th>
            <td>
                <asp:Label ID="lblCreatedTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                最后修改时间
            </th>
            <td>
                <asp:Label ID="lblUpdatedTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
