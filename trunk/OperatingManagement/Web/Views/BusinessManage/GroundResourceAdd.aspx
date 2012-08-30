<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="GroundResourceAdd.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GroundResourceAdd" %>
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
    业务管理 &gt; 新增地面站资源
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 200px;">
                设备名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtEquipmentName" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic" ValidationGroup="GroundResource"
                    ForeColor="Red" ControlToValidate="txtEquipmentName" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                设备编号(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtEquipmentCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic" ValidationGroup="GroundResource"
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
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic" ValidationGroup="GroundResource"
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
                <table width="100%">
                    <tr>
                        <th width="30%">
                            已添加扩展属性
                        </th>
                        <td width="70%">
                            <asp:Repeater ID="rpZYSXList" runat="server">
                                <HeaderTemplate>
                                    <table class="list">
                                        <tr>
                                            <th style="width: 10%; text-align: center;">
                                                属性名称
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                属性编码
                                            </th>
                                             <th style="width: 10%; text-align: center;">
                                                属性类型
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                属性范围
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                属性数值
                                            </th>
                                            <th style="width: 10%; text-align: center;">
                                                删除属性
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
                                        <td style="text-align: center;">
                                            <%# Eval("PValue")%>
                                        </td>
                                        <td style="text-align: center;">
                                            <asp:LinkButton ID="lbtnDeleteZYSX" runat="server" OnClick="lbtnDeleteZYSX_Click"
                                                OnClientClick="javascript:return confirm('是否删除资源属性？')" CausesValidation="false"
                                                CommandName="delete" CommandArgument='<%# Eval("PValueID").ToString()%>'>删除属性</asp:LinkButton>
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
                            属性名称(<span class="red">*</span>)
                        </th>
                        <td>
                            <asp:DropDownList ID="dplZYSX" runat="server" CssClass="norDpl" AutoPostBack="true" OnSelectedIndexChanged="dplZYSX_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvZYSX" runat="server" Display="Dynamic" ValidationGroup="ZYSX"
                                ForeColor="Red" ControlToValidate="dplZYSX" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            属性类型
                        </th>
                        <td>
                            <asp:Label ID="lblZYSXType" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            属性值范围
                        </th>
                        <td>
                            <asp:Label ID="lblZYSXScope" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            属性值(<span class="red">*</span>)
                        </th>
                        <td>
                            <asp:TextBox ID="txtPValue" runat="server" CssClass="norText"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ValidationGroup="ZYSX"
                                ForeColor="Red" ControlToValidate="txtPValue" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="btnAddZYSX" runat="server" CssClass="button" Text="添 加"
                                ValidationGroup="ZYSX" OnClick="btnAddZYSX_Click" />
                        </td>
                    </tr>
                </table>
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" ValidationGroup="GroundResource" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="清 除" OnClick="btnReset_Click"
                    CausesValidation="false" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
