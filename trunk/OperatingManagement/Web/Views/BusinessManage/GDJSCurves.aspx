<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GDJSCurves.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GDJSCurves" %>
<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 轨道分析 - 曲线图
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<div class="index_content_search">
<table style="width:800px;" cellspacing="0" cellpadding="0" class="searchTable">
    <tr>
        <th style="width:20%;">结果类型(<span class="red">*</span>)</th>
        <td style="width:35%;"><asp:DropDownList ID="ddlResultType" runat="server" 
                onselectedindexchanged="ddlResultType_SelectedIndexChanged">
        </asp:DropDownList></td>
        <th style="width:10%;">数据名称(<span class="red">*</span>)</th>
        <td style="width:35%;"><asp:DropDownList ID="ddlDataType" runat="server">
        </asp:DropDownList></td>
    </tr>
    <tr>
        <th>轨道分析结果数据路径</th>
        <td>
            <asp:Label ID="lbPath" runat="server" Text=""></asp:Label></td>
        <th>&nbsp;</th>
        <td align="right">
            <asp:Button ID="btnCurve2" runat="server" Text="曲线图" CssClass="button" 
                onclick="btnCurve2_Click" />
        </td>
    </tr>
    <tr>
        <th>选择其他结果数据文件</th>
        <td><asp:FileUpload ID="fuPath" ClientIDMode="Static" runat="server" /></td>
        <th>&nbsp;</th>
        <td align="right">
            <asp:Button ID="btnCurve" runat="server" Text="曲线图" CssClass="button" 
                onclick="btnCurve_Click" />
        </td>
    </tr>
</table>
</div>
<div id="divOneResourceStatus" runat="server" visible="true" class="index_content_view">
    <asp:CHART id="ChartCurve" runat="server" Palette="BrightPastel" 
        BackColor="AliceBlue" ImageType="Png" 
        ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" Width="1100px" Height="600px" 
        BorderlineDashStyle="Solid" BackGradientStyle="TopBottom" BorderWidth="2" 
        BorderColor="181, 64, 1" BackSecondaryColor="LightBlue">
		<legends>
			<asp:Legend Enabled="False" IsTextAutoFit="False" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold"></asp:Legend>
		</legends>
		<borderskin SkinStyle="Emboss"></borderskin>
		<series>
			<asp:Series ChartArea="ChartArea1" MarkerSize="8" BorderWidth="3" XValueType="DateTime" YValueType="Double" Name="Series1" ChartType="FastLine" MarkerStyle="Circle" ShadowColor="Black" BorderColor="180, 26, 59, 105" Color="220, 65, 140, 240" ShadowOffset="2"></asp:Series>
		</series>
		<chartareas>
			<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" 
                BorderDashStyle="Solid" BackSecondaryColor="White" BackColor="White" 
                ShadowColor="Transparent" BackGradientStyle="TopBottom">
				<axisy LineColor="64, 64, 64, 64">
					<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
					<MajorGrid LineColor="64, 64, 64, 64" />
				</axisy>
				<axisx IntervalType="Minutes" Interval="10" LineColor="64, 64, 64, 64">
					<LabelStyle Font="Trebuchet MS, 8.25pt, style=Bold" />
					<MajorGrid LineColor="64, 64, 64, 64" />
				</axisx>
			</asp:ChartArea>
		</chartareas>
	</asp:CHART>
</div>
</asp:Content>
