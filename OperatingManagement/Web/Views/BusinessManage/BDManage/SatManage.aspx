<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="SatManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.SatManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 查看卫星
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Repeater ID="rpTasks" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:200px;">卫星名称</th>
                    <th>卫星编码</th>
                    <th style="width:150px;">卫星标识</th>
                    <th style="width:70px;">状态</th>
                    <th style="width:70px;">面质比</th>
                    <th style="width:70px;">表面反射系数</th>
                    <th style="width:70px;">创建时间</th>
                    <th style="width:70px;">操作</th>
                </tr>  
                <tbody id="tbTasks">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("WXMC") %></td>
                <td><%# Eval("WXBM") %></td>
                <td><%# Eval("WXBS") %></td>
                <td><%# Eval("State").ToString() == "0" ? "可用" : "不可用"%></td>
                <td><%# Eval("MZB") %></td>
                <td><%# Eval("BMFSXS") %></td>
                <td><%# Eval("CTime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td>
                    <button class="button" onclick="window.location.href = '/views/BusinessManage/BDManage/SatEdit.aspx?id=<%# Eval("WXBM") %>';return false;">编辑</button>
                </td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>   
                </tbody>           
            </table>            
        </FooterTemplate>
    </asp:Repeater>
    <table class="listTitle">
        <tr>
            <td class="listTitle-c2" align="right">
                <om:CollectionPager ID="cpPager" runat="server"></om:CollectionPager>
            </td>
        </tr>
    </table>
</asp:Content>
