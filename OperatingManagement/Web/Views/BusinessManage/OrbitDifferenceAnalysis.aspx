<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrbitDifferenceAnalysis.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.OrbitDifferenceAnalysis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
 <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
 <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理 &gt; 轨道分析 - 差值分析
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<table class="edit" style="width:800px;">
    <tr>
        <th style="width:120px;">星历数据文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuXLDataFile" ClientIDMode="Static" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="fuXLDataFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width:120px;">差值计算时间文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuDifCalTimeFile" ClientIDMode="Static"  runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="fuDifCalTimeFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <th>&nbsp;</th>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="error" Text="“相关文件”内容必须严格按照格式要求编写。"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnCalculate" runat="server" Text="开始计算" CssClass="button" 
                onclick="btnCalculate_Click" />
        </td>
    </tr>
</table>
<div id="dialog-form" style="display:none" title="提示信息">
	<p class="content">
        <b>角度：</b><asp:Literal ID="ltAngle" runat="server"></asp:Literal><br />
        <b>长度：</b><asp:Literal ID="ltDist" runat="server"></asp:Literal><br />
        <b>类型：</b><asp:Literal ID="ltOrbit" runat="server"></asp:Literal><br />
        <b>参数：</b><asp:Literal ID="ltPara" runat="server"></asp:Literal><br />
        <b>计算：</b><asp:Literal ID="ltCal" runat="server"></asp:Literal><br />
    </p>
</div>
</asp:Content>
