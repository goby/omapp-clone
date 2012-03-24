<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResourceManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.ResourceManage" %>
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
 业务管理 &gt; 查询资源
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
 <div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
                <th width="15%">
                    资源类型：
                </th>
                <td width="25%">
                    <asp:DropDownList ID="dplResourceType" runat="server" CssClass="norDpl">
                    </asp:DropDownList>
                </td>
                <th width="15%">
                    资源状态：
                </th>
                <td width="25%">
                    <asp:DropDownList ID="dplResourceStatus" runat="server" CssClass="norDpl">
                    </asp:DropDownList>
                </td>
                <td width="20%">
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="button" Text="查 询" />
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="button" Text="添 加"/>
                </td>
            </tr>
        </table>
    </div>
    <div id="divGroundResource" runat="server" class="index_content_view">
        <asp:Repeater ID="rpGroundResourceList" runat="server" 
            onitemdatabound="rpGroundResourceList_ItemDataBound">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                        <th style="width: 10%;">
                            地面站名称
                        </th>
                        <th style="width: 10%;">
                            地面站编号
                        </th>
                        <th style="width: 10%;">
                            设备名称
                        </th>
                        <th style="width: 10%;">
                            设备编号
                        </th>
                        <th style="width: 10%;">
                            管理单位
                        </th>
                        <th style="width: 10%;">
                            站址坐标
                        </th>
                        <th style="width: 10%;">
                            功能类型
                        </th>
                        <th style="width: 9%;">
                            资源状态
                        </th>
                        <th style="width: 7%;">
                            管理状态
                        </th>
                        <th style="width: 7%;">
                            编辑资源
                        </th>
                         <th style="width: 7%;">
                            删除资源
                        </th>
                    </tr>
                    <tbody id="tbGroundResourceList">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("GRName")%>
                    </td>
                    <td>
                        <%# Eval("GRCode")%>
                    </td>
                    <td>
                        <%# Eval("EquipmentName")%>
                    </td>
                    <td>
                        <%# Eval("EquipmentCode")%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.GroundResourceOwner, Eval("Owner").ToString())%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.GroundResourceCoordinate, Eval("Coordinate").ToString())%>
                    </td>
                    <td>
                        <%# GetGroundResourceFunctionType(Eval("FunctionType").ToString())%>
                    </td>
                    <td>
                       <%# Eval("Status").ToString() == "2" ? "删除" : "正常"%>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnManageResourceStatus" runat="server" OnClick="lbtnManageResourceStatus_Click" CommandName="1"  CommandArgument='<%# Eval("GRCode")%>'>管理状态</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnEditResource" runat="server" OnClick="lbtnEditResource_Click" CommandName="1" CommandArgument='<%# Eval("Id")%>'>编辑资源</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnDeleteResource" runat="server" OnClick="lbtnDeleteResource_Click" OnClientClick="javascript:return confirm('是否删除该资源？')" CommandName="1" CommandArgument='<%# Eval("Id")%>'>删除资源</asp:LinkButton>
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
                    <om:CollectionPager ID="cpGroundResourcePager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
    </div>
    <div id="divCommunicationResource" runat="server" class="index_content_view">
        <asp:Repeater ID="rpCommunicationResourceList" runat="server" 
            onitemdatabound="rpCommunicationResourceList_ItemDataBound">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                        <th style="width: 20%;">
                            线路名称
                        </th>
                        <th style="width: 20%;">
                            线路编号
                        </th>
                        <th style="width: 15%;">
                            方向
                        </th>
                        <th style="width: 15%;">
                            带宽
                        </th>
                        <th style="width: 9%;">
                            资源状态
                        </th>
                         <th style="width: 7%;">
                            管理状态
                        </th>
                        <th style="width: 7%;">
                            编辑资源
                        </th>
                         <th style="width: 7%;">
                            删除资源
                        </th>
                    </tr>
                    <tbody id="tbCommunicationResourceList">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("RouteName")%>
                    </td>
                    <td>
                        <%# Eval("RouteCode")%>
                    </td>
                    <td>
                        <%# SystemParameters.GetSystemParameterText(SystemParametersType.CommunicationResourceDirection, Eval("Direction").ToString())%>
                    </td>
                    <td>
                        <%# Eval("BandWidth")%>
                    </td>
                     <td>
                       <%# Eval("Status").ToString() == "2" ? "删除" : "正常"%>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnManageResourceStatus" runat="server" OnClick="lbtnManageResourceStatus_Click" CommandName="2"  CommandArgument='<%# Eval("RouteCode")%>'>管理状态</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnEditResource" runat="server" OnClick="lbtnEditResource_Click" CommandName="2" CommandArgument='<%# Eval("Id")%>'>编辑资源</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnDeleteResource" runat="server" OnClick="lbtnDeleteResource_Click" OnClientClick="javascript:return confirm('是否删除该资源？')" CommandName="2" CommandArgument='<%# Eval("Id")%>'>删除资源</asp:LinkButton>
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
                    <om:CollectionPager ID="cpCommunicationResourcePager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
    </div>
     <div id="divCenterResource" runat="server" class="index_content_view">
        <asp:Repeater ID="rpCenterResourceList" runat="server" 
             onitemdatabound="rpCenterResourceList_ItemDataBound">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                        <th style="width: 15%;">
                            设备编号
                        </th>
                        <th style="width: 20%;">
                            设备类型
                        </th>
                        <th style="width: 20%;">
                            支持的任务
                        </th>
                        <th style="width: 15%;">
                            最大数据处理量
                        </th>
                       <th style="width: 9%;">
                            资源状态
                        </th>
                         <th style="width: 7%;">
                            管理状态
                        </th>
                        <th style="width: 7%;">
                            编辑资源
                        </th>
                         <th style="width: 7%;">
                            删除资源
                        </th>
                    </tr>
                    <tbody id="tbCenterResourceList">
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("EquipmentCode")%>
                    </td>
                    <td>
                         <%# SystemParameters.GetSystemParameterText(SystemParametersType.CenterResourceEquipmentType, Eval("EquipmentType").ToString())%>
                    </td>
                    <td>
                        <%# Eval("SupportTask")%>
                    </td>
                    <td>
                        <%# Eval("DataProcess")%>
                    </td>
                     <td>
                       <%# Eval("Status").ToString() == "2" ? "删除" : "正常"%>
                    </td>
                   <td>
                        <asp:LinkButton ID="lbtnManageResourceStatus" runat="server" OnClick="lbtnManageResourceStatus_Click" CommandName="3"  CommandArgument='<%# Eval("EquipmentCode")%>'>管理状态</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnEditResource" runat="server" OnClick="lbtnEditResource_Click" CommandName="3" CommandArgument='<%# Eval("Id")%>'>编辑资源</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnDeleteResource" runat="server" OnClick="lbtnDeleteResource_Click" OnClientClick="javascript:return confirm('是否删除该资源？')" CommandName="3" CommandArgument='<%# Eval("Id")%>'>删除资源</asp:LinkButton>
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
                    <om:CollectionPager ID="cpCenterResourcePager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
