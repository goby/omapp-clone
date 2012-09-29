<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="ZYGNEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.ZYGNEdit" %>

<%@ Register Src="../../../ucs/ucSatellite.ascx" TagName="ucSatellite" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 修改资源功能
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 100px;">
                名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtName" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtName" ErrorMessage="必须填写“名称”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                编码(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtCode" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtCode" ErrorMessage="必须填写“编码”。"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 100px;">
                属性(<span class="red">*</span>)
            </th>
            <td>
                <br />
                <table class="edit">
                    <tr>
                        <th style="width: 150px;">
                            卫星
                        </th>
                        <th style="width: 100px;">
                            匹配准则
                        </th>
                        <th style="width: 150px;">
                            地面站资源
                        </th>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <uc1:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
                            <br />
                            <asp:ListBox ID="lbSat" runat="server" Height="110px" Width="113px">
                            </asp:ListBox>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rblOwn" runat="server" BorderColor="White" BorderStyle="Double"
                                BorderWidth="2px">
                                <asp:ListItem Value="&lt;" Selected="True">&lt;</asp:ListItem>
                                <asp:ListItem Value="&lt;=">&lt;=</asp:ListItem>
                                <asp:ListItem Value="=">=</asp:ListItem>
                                <asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
                                <asp:ListItem Value="&gt;">&gt;</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td style="vertical-align: top;">
                            <asp:DropDownList ID="ddlDMZ" runat="server" Width="113px">
                            </asp:DropDownList>
                            <br />
                            <asp:ListBox ID="lbDMZ" runat="server" Height="110px" Width="113px">
                            </asp:ListBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="lbSat" Display="Dynamic" ErrorMessage="未选择属性" 
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td style="vertical-align: top;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="lbDMZ" Display="Dynamic" ErrorMessage="未选择属性" 
                                ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="“名称”、“编码”必须唯一。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnEmpty" runat="server" CssClass="button" Text="重置" CausesValidation="False"
                    OnClick="btnEmpty_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" class="button" runat="server" 
                    Text="返回" onclick="btnReturn_Click" CausesValidation="False" />
                    <asp:HiddenField ID="hfID" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>