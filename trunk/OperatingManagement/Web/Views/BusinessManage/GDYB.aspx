<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GDYB.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GDYB" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="gdfx" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuGD" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 轨道分析 - 轨道预报
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<table class="edit" style="width:800px;">
    <tr>
        <th style="width:150px; text-align:right;">起始日期(<span class="red">*</span>)</th>
        <td>
            <asp:TextBox ID="txtFrom" ClientIDMode="Static" CssClass="text"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  
                    runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="cv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtFrom" ErrorMessage="必须填写“起始日期”。"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">预报时长（天）(<span class="red">*</span>)</th>
        <td>
            <asp:TextBox ID="txtDays" ClientIDMode="Static" CssClass="text"  
                    runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rv1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtDays" ErrorMessage="必须填写“预报时长”。"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="rev3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtDays" ErrorMessage="必须为有效整数。"
                     ValidationExpression="((\d{1,5}))"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <th style="text-align:right;">预报时间间隔（秒）(<span class="red">*</span>)</th>
        <td>
            <asp:TextBox ID="txtTimeSpan" ClientIDMode="Static" CssClass="text"  
                    runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="rv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtTimeSpan" ErrorMessage="必须填写“预报时间间隔”。"></asp:RequiredFieldValidator>
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtTimeSpan" ErrorMessage="必须为有效整数。"
                     ValidationExpression="((\d{1,3}))"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <th style="text-align:right;">卫星名称(<span class="red">*</span>)</th>
        <td>
            <uc1:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">测站编码(<span class="red">*</span>)</th>
        <td>
            <asp:CheckBoxList ID="cblXyxs1" runat="server" BorderColor="White" 
                BorderStyle="Double" BorderWidth="2px" ClientIDMode="Static">
            </asp:CheckBoxList>
            <asp:RadioButtonList ID="rblDMZ" runat="server" BorderColor="White" 
                BorderStyle="Double" BorderWidth="2px" RepeatColumns="2" Visible="False">
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">圈次源(<span class="red">*</span>)</th>
        <td id="tdQcy">
            <asp:RadioButton ID="rb1" runat="server" Checked="True" 
                GroupName="qcy" Text="是" />
            <asp:RadioButton ID="rb2" runat="server" GroupName="qcy" Text="否" />
        </td>
    </tr>
    <tr>
        <th style="text-align:right;">圈次（整数）(<span class="red">*</span>)</th>
        <td>
            <asp:TextBox ID="txtQC" ClientIDMode="Static" CssClass="text"
                    runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtQC" ErrorMessage="必须为有效整数。"
                     ValidationExpression="((\d{1,3}))"></asp:RegularExpressionValidator></td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="请严格按照格式要求填写。" ClientIDMode="Static"></asp:Label>
        </td>
    </tr>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnCalculate" runat="server" Text="开始预报" CssClass="button" CausesValidation="true"
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
