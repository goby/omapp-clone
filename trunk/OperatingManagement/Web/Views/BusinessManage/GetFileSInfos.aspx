<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetFileSInfos.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.GetFileSInfos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 查看文件发送记录
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="listTitle">
        <tr>
            <td class="listTitle-c1">
                <div style="display:none;">
                    <asp:TextBox ID="txtRID" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtStatus" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:Button ID="btnHidRSendFile" runat="server" ClientIDMode="Static" Text="btnHidRSendFile" 
                                onclick="btnHidRSendFile_Click" />
                </div>
            </td>
        </tr>
    </table>
    <asp:Repeater ID="rpRoles" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:200px;">记录号</th>
                    <th style="width:200px;">文件名</th>
                    <th style="width:200px;">文件标识</th>
                    <th style="width:150px;">文件路径</th>
                    <th style="width:200px;">文件大小</th>
                    <th style="width:200px;">发送方式</th>
                    <th style="width:150px;">发送方</th>
                    <th style="width:200px;">信息类型</th>
                    <th style="width:150px;">接收方</th>
                    <th style="width:150px;">状态</th>
                    <th style="width:200px;">重发次数</th>
                    <th style="width:200px;">备注</th>
                    <th style="width:200px;">提交时间</th>
                    <th style="width:70px;">操作</th>
                </tr>  
                <tbody id="tbRoles">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><%# Eval("RID") %></td>
                <td><%# Eval("FileName") %></td>
                <td><%# Eval("FileCode") %></td>
                <td><%# Eval("FilePath") %></td>
                <td><%# Eval("Filesize") %></td>
                <td><%# Eval("SendWay") %></td>
                <td><%# Eval("Sender") %></td>
                <td><%# Eval("InfoType")%></td>
                <td><%# Eval("Receiver")%></td>
                <td><%# Eval("SendStatus")%></td>
                <td><%# Eval("RetryTimes")%></td>
                <td><%# Eval("Remark")%></td>
                <td><%# Eval("SubmitTime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td>
                    <button class="button" onclick="return reSendFile('<%# Eval("RId") %>', '<%# Eval("SendStatus") %>')">重发</button>
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
                <om:CollectionPager ID="cpPager" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
</asp:Content>
