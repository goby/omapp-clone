<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SatAdd.aspx.cs"
    Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.SatAdd" %>

<%@ Import Namespace="OperatingManagement.DataAccessLayer.BusinessManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 新增卫星
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 200px;">
                卫星名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtWXMC" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtWXMC" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                卫星编码(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtWXBM" runat="server" Width="300px" CssClass="text" MaxLength="25"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtWXBM" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                卫星标识(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtWXBS" runat="server" Width="300px" CssClass="text" MaxLength="10"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtWXBS" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                卫星状态(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplState" runat="server" CssClass="norDpl">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>
                面质比(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtMZB" runat="server" Width="300px" CssClass="text" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtMZB" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev3" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtMZB" ErrorMessage="（请输入8位内正整数）" ValidationExpression="^[1-9][0-9]{0,7}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                表面反射系数(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtBMFSXS" runat="server" Width="300px" CssClass="text" MaxLength="8"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv5" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtBMFSXS" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtBMFSXS" ErrorMessage="（请输入8位内正整数）" ValidationExpression="^[1-9][0-9]{0,7}$"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <th>
                扩展属性
            </th>
            <td>
                <asp:Repeater ID="rpZYSXList" runat="server" ViewStateMode="Enabled">
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
                卫星功能
            </th>
            <td>
                <asp:TextBox ID="txtGN" runat="server" TextMode="MultiLine" Width="300px" Height="100px"
                    CssClass="text" MaxLength="500"></asp:TextBox>
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
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" OnClick="btnSubmit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" CssClass="button" Text="清 除" OnClick="btnReset_Click"
                    CausesValidation="false" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
