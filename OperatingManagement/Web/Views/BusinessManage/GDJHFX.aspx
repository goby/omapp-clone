<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GDJHFX.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GDJHFX" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 轨道分析 - 交会分析
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<table class="edit" style="width:800px;">
    <tr>
        <th style="width:100px;">交会分析主星数据文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuSubFile" ClientIDMode="Static" runat="server" />
        </td>
    </tr>
    <tr>
        <th style="width:100px;">交会分析目标星数据文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuTgtFile" ClientIDMode="Static" runat="server" />
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="“数据文件”内容必须严格按照格式要求编写。"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnCalculate" runat="server" Text="开始分析" CssClass="button" 
                onclick="btnCalculate_Click" />
        </td>
    </tr>
</table>
<div id="dialog-form" style="display:none" title="提示信息">
	<p class="content">
        <b>角度：</b><asp:Literal ID="ltAngle" runat="server"></asp:Literal><br />
    </p>
</div>
</asp:Content>
