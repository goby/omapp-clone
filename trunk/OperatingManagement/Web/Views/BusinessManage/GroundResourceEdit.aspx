﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroundResourceEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GroundResourceEdit" %>
<%@ Import Namespace="OperatingManagement.DataAccessLayer.BusinessManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="resmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuRes" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 编辑地面站资源
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 200px;">
                设备名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtEquipmentName" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtEquipmentName" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                设备编号(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtEquipmentCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtEquipmentCode" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                地面站(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplGroundStation" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplGroundStation" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                功能类型(<span class="red">*</span>)
            </th>
            <td>
                <asp:CheckBoxList ID="cblFunctionType" runat="server" BorderWidth="0" RepeatColumns="4" BorderStyle="None">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr>
            <th>
                扩展属性
            </th>
            <td>
                <asp:Repeater ID="rpZYSXList" runat="server">
                    <HeaderTemplate>
                        <table class="list">
                            <tr>
                                <th style="width: 15%; text-align: center;">
                                    属性名称
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    属性编码
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    属性类型
                                </th>
                                <th style="width: 15%; text-align: center;">
                                    属性范围
                                </th>
                                <th style="width: 40%; text-align: center;">
                                    属性数值
                                </th>
                            </tr>
                            <tbody id="tbZYSXList">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center;">
                                <%# Eval("PName")%>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("PCode")%>
                            </td>
                            <td style="text-align: center;">
                                <%# SystemParameters.GetSystemParameterText(SystemParametersType.ZYSXType, Eval("Type").ToString())%>
                            </td>
                            <td style="text-align: center;">
                                <%# Eval("Scope")%>
                            </td>
                            <td style="text-align: left;">
                               <asp:PlaceHolder ID="phPValueControls" runat="server"></asp:PlaceHolder>
                               <asp:HiddenField ID="hfPID" runat="server" Value='<%#Eval("ID")%>' />
                            </td> 
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody></table>
                    </FooterTemplate>
                </asp:Repeater>
                <table class="listTitle">
                    <tr>
                        <td class="listTitle-c1">
                        </td>
                        <td class="listTitle-c2">
                            <om:CollectionPager ID="cpZYSXPager" runat="server">
                            </om:CollectionPager>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <th>
                创建时间
            </th>
            <td>
                <asp:Label ID="lblCreatedTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                最后修改时间
            </th>
            <td>
                <asp:Label ID="lblUpdatedTime" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="重 置" OnClick="btnReset_Click"
                    CausesValidation="false" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td> 
        </tr>
    </table>
</asp:Content>
