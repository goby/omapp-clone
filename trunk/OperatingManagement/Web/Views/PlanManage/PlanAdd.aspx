<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlanAdd.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.PlanAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .text
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
新建计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
        <tr>
            <th style="width:100px;">
                任务代号(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtTaskID" runat="server" CssClass="text" MaxLength="25" 
                    Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" 
                    ControlToValidate="txtTaskID" Display="Dynamic" ErrorMessage="必须填写“任务代号”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                计划类型(<span class="red">*</span>)</th>
            <td>
                <asp:DropDownList ID="ddlPlanType" runat="server" Width="304px" Height="24px">
                    <asp:ListItem>应用研究工作计划</asp:ListItem>
                    <asp:ListItem>空间信息需求</asp:ListItem>
                    <asp:ListItem>地面站工作计划</asp:ListItem>
                    <asp:ListItem>中心运行计划</asp:ListItem>
                    <asp:ListItem>仿真推演试验数据</asp:ListItem>
                    <asp:ListItem>设备工作计划</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                计划编号(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtPlanID" runat="server" CssClass="text" MaxLength="10" 
                    Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" 
                    ControlToValidate="txtPlanID" Display="Dynamic" ErrorMessage="必须填写“计划编号”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtPlanID" ErrorMessage="只能输入数字"
                     ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                开始时间(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" CssClass="text" 
                     Width="300px" onclick="setday(this);"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" 
                    ControlToValidate="txtStartTime" Display="Dynamic" ErrorMessage="必须填写“开始时间”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                结束时间(<span class="red">*</span>)</th>
            <td>

                <asp:TextBox ID="txtEndTime" runat="server" CssClass="text" 
                     Width="300px"  onclick="setday(this);"></asp:TextBox>

                <asp:RequiredFieldValidator ID="rfv5" runat="server" 
                    ControlToValidate="txtEndTime" Display="Dynamic" ErrorMessage="必须填写“结束时间”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <th>
                备注</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="50" 
                    Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:HiddenField ID="hfUserId" runat="server" />
                <asp:Literal ID="ltHref" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" 
                    Text="“任务代号”和“计划编号”必须唯一。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" 
                    onclick="btnSubmit_Click" Text="提交" />
            &nbsp;&nbsp;
                <asp:Button ID="txtGetPlanInfo" runat="server" onclick="txtGetPlanInfo_Click" 
                    Text="从设备计划获取信息" />
            </td>
        </tr>
    </table>
</asp:Content>
