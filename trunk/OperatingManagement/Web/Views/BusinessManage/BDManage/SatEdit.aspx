<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SatEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.SatEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 编辑卫星
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
        <tr>
            <th style="width:100px;">卫星名称(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtWXMC" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtWXMC" ErrorMessage="必须填写“卫星名称”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">卫星编码(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtWXBM" runat="server" Width="300px" CssClass="text" MaxLength="25" ReadOnly=true></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtWXBM" ErrorMessage="必须填写“卫星编码”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">卫星标识(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtWXBS" runat="server" Width="300px" CssClass="text" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtWXBS" ErrorMessage="必须填写“卫星标识”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">状态(<span class="red">*</span>)</th>
            <td>
                <asp:RadioButtonList ID="rblState" runat="server" BorderColor="White" 
                    BorderStyle="Double" BorderWidth="2px" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0">可用</asp:ListItem>
                    <asp:ListItem Value="1">不可用</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">面质比(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtMZB" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtMZB" ErrorMessage="必须填写“面质比”。"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev3" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtMZB" ErrorMessage="“面质比”必须为8位内整型数值。"
                     ValidationExpression="^[1-9][0-9]{0,8}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">表面反射系数(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtBMFSXS" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtBMFSXS" ErrorMessage="必须填写“表面反射系数”。"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtMZB" ErrorMessage="“表面反射系数”必须为8位内整型数值。"
                     ValidationExpression="^[1-9][0-9]{0,8}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="“卫星名称”、“卫星编码”、“卫星标识”必须唯一。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnEmpty" runat="server" CssClass="button" Text="清空" CausesValidation="False"
                    onclick="btnEmpty_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
