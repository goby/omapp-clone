<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="TaskEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.TaskEdit" %>
<%@ Register src="../../../ucs/ucCBLSat.ascx" tagname="ucCBLSat" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 编辑任务
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
        <tr>
            <th style="width:100px;">任务名称(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtTaskName" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtTaskName" ErrorMessage="必须填写“任务名称”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">任务代号(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtTaskNo" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtTaskNo" ErrorMessage="必须填写“任务代号”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">对象标识(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtObjectFlag" runat="server" Width="300px" CssClass="text" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtObjectFlag" ErrorMessage="必须填写“对象标识”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">卫星(<span class="red">*</span>)</th>
            <td>
                <uc1:ucCBLSat ID="ucCBLSats" runat="server" />
            </td>
        </tr>
        <tr>
            <th style="width:100px;">当前任务(<span class="red">*</span>)</th>
            <td>
                <asp:RadioButtonList ID="rblCurTask" runat="server" BorderColor="White" 
                    BorderStyle="Double" BorderWidth="2px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                    <asp:ListItem Value="0">否</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">开始时间(<span class="red">*</span>)</th>
            <td><asp:TextBox ID="txtFrom" ClientIDMode="Static" CssClass="text"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  
                    runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator ID="rv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtFrom" ErrorMessage="必须填写“开始日期”。"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <th style="width:100px;">结束时间</th>
            <td>
                <asp:TextBox ID="txtTo" ClientIDMode="Static" CssClass="text"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"  
                    runat="server" Width="300px"></asp:TextBox><asp:RequiredFieldValidator ID="rv3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtTo" ErrorMessage="必须填写“结束日期”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:HiddenField ID="hfUserId" runat="server" />
                <asp:Literal ID="ltHref" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="“登录名”和“显示名称”必须唯一。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" 
                    CssClass="button" Text="重置"  CausesValidation="False" onclick="btnReset_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReturn" class="button" runat="server" 
                    Text="返回" onclick="btnReturn_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
</asp:Content>