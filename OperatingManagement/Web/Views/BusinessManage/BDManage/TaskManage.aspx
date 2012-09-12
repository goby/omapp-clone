<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaskManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.BDManage.TaskManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    基础数据管理 &gt; 查看任务
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Repeater ID="rpTasks" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:200px;">任务名称</th>
                    <th>任务代号</th>
                    <th style="width:150px;">对象标识</th>
                    <th style="width:70px;">卫星编码</th>
                    <th style="width:70px;">当前任务</th>
                    <th style="width:70px;">开始时间</th>
                    <th style="width:70px;">结束时间</th>
                    <th style="width:70px;">操作</th>
                </tr>  
                <tbody id="tbTasks">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("TaskName") %></td>
                <td><%# Eval("TaskNo") %></td>
                <td><%# Eval("ObjectFlag") %></td>
                <td><%# Eval("SatID") %></td>
                <td><%# Eval("ISCURTASK").ToString() == "1" ? "是" : "否"%></td>
                <td><%# Eval("BeginTime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("EndTime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td>
                    <button class="button" onclick="window.location.href = '/views/BusinessManage/BDManage/TaskEdit.aspx?id=<%# Eval("Id") %>';return false;">编辑</button>
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
