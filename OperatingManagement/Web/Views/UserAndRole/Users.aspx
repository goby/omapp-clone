<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="OperatingManagement.Web.Views.UserAndRole.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="usernrole" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuUserNRole" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    用户管理 &gt; 用户列表
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="listTitle">
        <tr>
            <td class="listTitle-c1">
                <button class="button" onclick="return selectAll();">全选</button>&nbsp;&nbsp;
                <button class="button" onclick="return deleteUsers();">删除</button> 
            </td>
            <td class="listTitle-c2">
                <div class="load" id="submitIndicator" style="display:none">提交中，请稍候。。。</div>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rpUsers" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>
                    <th style="width:200px;">登录名</th>
                    <th style="width:200px;">显示名</th>
                    <th>创建时间</th>
                    <th style="width:70px;">操作</th>
                </tr>  
                <tbody id="tbUsers">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>
                <td><%# Eval("LoginName") %></td>
                <td><%# Eval("DisplayName") %></td>
                <td><%# Eval("CreatedTime","{0:"+this.SiteSetting.DateTimeFormat+"}") %></td>
                <td>
                    <button class="button" onclick="return editUser('<%# Eval("Id") %>')"
                    <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %>>编辑</button>
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
            <td class="listTitle-c1">
                <button class="button" onclick="return selectAll();">全选</button>&nbsp;&nbsp;
                <button class="button" onclick="return deleteUsers();">删除</button>          
            </td>
            <td class="listTitle-c2">
                <om:CollectionPager ID="cpPager" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
</asp:Content>
