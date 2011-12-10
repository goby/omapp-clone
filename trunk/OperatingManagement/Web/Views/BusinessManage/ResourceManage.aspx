<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResourceManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.ResourceManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style type="text/css">
        .norText
        {
            width: 155px;
            margin: 0px;
            padding: 0px;
        }
        .norDpl
        {
            width: 160px;
            margin: 0px;
            padding: 0px;
        }
        .index_content_search
        {
            margin-top: 10px;
        }
        
        .index_content_search table
        {
            border: 1px solid #eeeeee;
            border-collapse: collapse;
            width: 100%;
        }
        
        .index_content_search table td
        {
            border: 1px solid #eeeeee;
            line-height: 26px;
            color: #333333;
            text-align: left;
            height: 26px;
        }
        .index_content_search table th
        {
            border: 1px solid #eeeeee;
            font-weight: bold;
            line-height: 26px;
            color: #333333;
            text-align: right;
            height: 26px;
        }
        .index_content_view
        {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
<om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
 业务管理&gt;资源管理
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
    <div id="divCommunicationResource" runat="server" class="index_content_view">
        <asp:Repeater ID="rpCOPList" runat="server">
            <HeaderTemplate>
                <table class="list">
                    <tr>
                        <th style="width: 10%;">
                            线路名称
                        </th>
                        <th style="width: 20%;">
                            线路编号
                        </th>
                        <th style="width: 10%;">
                            方向
                        </th>
                        <th style="width: 10%;">
                            带宽
                        </th>
                        <th style="width: 10%;">
                            创建时间
                        </th>
                        <th style="width: 10%;">
                            修改时间
                        </th>
                        <th style="width: 10%;">
                            查看状态1
                        </th>
                        <th style="width: 10%;">
                            查看状态2
                        </th>
                        <th style="width: 10%;">
                            编辑
                        </th>
                    </tr>
                    <tbody id="tbCOPList">
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
                        <%# Eval("Direction")%>
                    </td>
                    <td>
                        <%# Eval("BandWidth")%>
                    </td>
                    <td>
                        <%# Eval("Ddestination")%>
                    </td>
                    <td>
                        <%# Eval("CreatedTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                    <td>
                        <%# Eval("UpdatedTime", "{0:" + this.SiteSetting.DateFormat + "}")%>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("Id")%>'>查看状态1</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument='<%# Eval("Id")%>'>查看状态2</asp:LinkButton>
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnEdit" runat="server" OnClick="lbtnEdit_Click" CommandArgument='<%# Eval("Id")%>'>编辑</asp:LinkButton>
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
                    <om:CollectionPager ID="cpPager" runat="server">
                    </om:CollectionPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
