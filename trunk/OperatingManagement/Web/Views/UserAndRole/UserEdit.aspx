<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.UserAndRole.UserEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="usernrole" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuUserNRole" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    用户管理 &gt; 新增用户
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
        <tr>
            <th style="width:100px;">登录名(<span class="red">*</span>)</th>
            <td>
                <asp:Literal ID="ltLoginName" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">显示名(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtDisplayName" runat="server" Width="300px" CssClass="text" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtDisplayName" ErrorMessage="必须填写“显示名称”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">密码(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="300px" CssClass="text" MaxLength="15"></asp:TextBox>
                <asp:RegularExpressionValidator ID="rev2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtPassword" ErrorMessage="只能输入字母和数字，且首字符必须为字母，最小长度为6个字符。"
                     ValidationExpression="^[a-zA-Z]\w{6,15}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">确认密码(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtPasswordConfirm" runat="server" TextMode="Password" Width="300px" CssClass="text" MaxLength="15"></asp:TextBox>
                <asp:CompareValidator ID="cv1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToCompare="txtPassword" ControlToValidate="txtPasswordConfirm" ErrorMessage="“确认密码”与“密码”不一致。"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">类型(<span class="red">*</span>)</th>
            <td>
                <asp:RadioButtonList ID="rdlTypes" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="普通用户" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="系统管理员" Value="1"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">状态(<span class="red">*</span>)</th>
            <td>
                <asp:RadioButtonList ID="rdlStatus" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Text="活动帐户" Value="0" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="锁定帐户" Value="2"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">联系电话</th>
            <td>
                <asp:TextBox ID="txtMobile" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>描述</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="300px"  CssClass="text" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Literal ID="ltHref" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="“显示名称”必须唯一，“密码”为空时将保持原密码不变。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>

