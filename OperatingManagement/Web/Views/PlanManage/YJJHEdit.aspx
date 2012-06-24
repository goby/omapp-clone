<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YJJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.YJJHEdit" %>

<%@ Register src="../../ucs/ucTask.ascx" tagname="ucTask" tagprefix="uc1" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc2" %>

<%@ Register src="../../ucs/ucTimer.ascx" tagname="ucTimer" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .text
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 应用研究计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">

<table class="edit1" style="width:800px;">
        <tr>
            <th>任务代号(<span class="red">*</span>)</th>
            <td>
                <uc1:ucTask ID="ucTask1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th>卫星(<span class="red">*</span>)</th>
            <td>
                <uc2:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th>信息分类</th>
            <td>
                <asp:RadioButtonList ID="radBtnXXFL" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="ZJ" Selected>周计划</asp:ListItem>
                    <asp:ListItem Value="RJ">日计划</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th>计划序号</th>
            <td>
                <asp:TextBox ID="txtJXH" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20" Enabled="False" ReadOnly="True"></asp:TextBox>
                    &nbsp;<span style="color:#3399FF;">自动生成，不可编辑</span>
            </td>
        </tr>
        <tr>
            <th class="style1">系统名称</th>
            <td>
                <asp:DropDownList ID="ddlSysName" runat="server" Height="20px" Width="305px">
                    <asp:ListItem>天基目标观测应用研究分系统</asp:ListItem>
                    <asp:ListItem>空间遥操作应用研究分系统</asp:ListItem>
                    <asp:ListItem>空间机动应用研究分系统</asp:ListItem>
                    <asp:ListItem>仿真推演分系统</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th class="style1">试验开始时间</th>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" Width="150px" CssClass="text" 
                    MaxLength="14" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
            &nbsp;<uc3:ucTimer ID="ucStartTimer" runat="server" Seperator="empty" 
                    ShowSecond="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartTime"
                        ErrorMessage="开始时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <th class="style1">试验结束时间</th>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" Width="150px" CssClass="text" 
                    MaxLength="14" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
            &nbsp;<span style="color:#3399FF;"><uc3:ucTimer ID="ucEndTimer" runat="server" 
                    Seperator="empty" ShowSecond="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndTime"
                        ErrorMessage="结束时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </span></td>
        </tr>
        <tr>
            <th class="style1">系统任务</th>
            <td>
                <asp:TextBox ID="txtTask" runat="server" Width="390px"  CssClass="text" 
                    MaxLength="128" Height="47px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>备注</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="50" 
                    Width="390px" Height="75px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" 
                    onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSaveTo_Click" />
                     &nbsp;&nbsp; <asp:Button ID="btnReset" class="button" runat="server" 
                    Text="重置" Width="65px" 
                    onclick="btnReset_Click" CausesValidation="False" />
                    &nbsp;<asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfStatus" runat="server" />
            </td>
        </tr>
    </table>
    <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>

