<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="YJJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.YJJHEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
            <th class="style1">计划开始时间</th>
            <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"   ClientIDMode="Static" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划结束时间</th>
            <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"   ClientIDMode="Static" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">信息分类</th>
            <td>
                <asp:RadioButtonList ID="radBtnXXFL" runat="server" 
                    RepeatDirection="Horizontal">
                    <asp:ListItem Value="ZJ">周计划</asp:ListItem>
                    <asp:ListItem Value="RJ">日计划</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th class="style1">计划序号</th>
            <td>
                <asp:TextBox ID="txtJXH" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">系统名称</th>
            <td>
                <asp:DropDownList ID="ddlSysName" runat="server" Height="16px" Width="298px">
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
                <asp:TextBox ID="txtStartTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            &nbsp;<span style="color:Red;">格式：YYMMDDHHmmss</span></td>
        </tr>
        <tr>
            <th class="style1">试验结束时间</th>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            &nbsp;<span style="color:Red;">格式：YYMMDDHHmmss</span></td>
        </tr>
        <tr>
            <th class="style1">系统任务</th>
            <td>
                <asp:TextBox ID="txtTask" runat="server" Width="300px"  CssClass="text" 
                    MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" 
                    onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSubmit_Click" />
                     <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfOverDate" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

