<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CenterOutputPolicyManage.aspx.cs" Inherits="OperatingManagement.Web.BusinessManage.CenterOutputPolicyManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuIndex" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
首页&gt;中心输出策略
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
  <table class="edit" style="width:800px;">
        <tr>
            <th style="width:100px;">信源(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtInfoFrom" runat="server" Width="300px" CssClass="text" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtInfoFrom" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">信息类别(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtInfoType" runat="server" Width="300px" CssClass="text" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtInfoType" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">信宿(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtInfoTo" runat="server" Width="300px" CssClass="text" MaxLength="4"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtInfoTo" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">描述</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="300px" CssClass="text" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">创建时间</th>
            <td>
                <asp:Label ID="lblCreatedTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">最后修改时间</th>
            <td>
               <asp:Label ID="lblUpdatedTime" runat="server" Text=""></asp:Label>
               <asp:HiddenField ID="hfCOPID" runat="server" Value="" />
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th>&nbsp;</th>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" onclick="btnSubmit_Click" />
                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="取消" 
                    onclick="btnCancel_Click"/>
            </td>
        </tr>
    </table>
</asp:Content>
