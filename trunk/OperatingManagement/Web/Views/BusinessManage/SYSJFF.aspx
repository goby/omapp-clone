<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SYSJFF.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.SYSJFF" %>
<%@ Register src="../../ucs/ucTask.ascx" tagname="ucTask" tagprefix="uc1" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .button
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理 &gt; 试验数据分发
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="listTitle">
        <tr>
            <td valign="middle"  width="10%">任务<uc1:ucTask ID="ucTask1" runat="server" /></td>
            <td valign="middle" width="10%">卫星<uc2:ucSatellite ID="ucSatellite1" runat="server" /></td>
            <td valign="middle" width="20%">数据类型<asp:DropDownList ID="ddlDataType" runat="server" ClientIDMode="Static">
                <asp:ListItem Value="0">天基目标观测试验数据</asp:ListItem>
                <asp:ListItem Value="1">空间机动试验数据</asp:ListItem>
                <asp:ListItem Value="2">仿真推演试验数据</asp:ListItem>
                </asp:DropDownList></td>
            <td valign="middle" width="20%">开始日期<asp:TextBox ID="txtFrom" ClientIDMode="Static" CssClass="text" 
                    runat="server"></asp:TextBox></td>
            <td valign="middle"width="20%">结束日期<asp:TextBox ID="txtTo" ClientIDMode="Static" CssClass="text" 
                    runat="server"></asp:TextBox></td>
            <td valign="middle" width="10%"><asp:Button CssClass="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                        Text="查询" />&nbsp;&nbsp;<asp:Button CssClass="button" ID="btnSend" 
                    runat="server" Text="发送数据" OnClientClick="return sendFiles();" 
                    Height="21px" onclick="btnSend_Click" /></td>
        </tr>
    </table>
    <div runat="server" id="vYCData">
    <asp:Repeater ID="rpYCData" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:5%;"><input type="checkbox" /></th>
                    <th style="width:10%;">创建时间</th>
                    <th style="width:10%;">任务代号</th>
                    <th style="width:10%;">卫星编号</th>
                    <th style="width:10%;">数据类型</th>
                    <th style="width:25%;">开始星上时间</th>
                    <th style="width:25%;">结束星上时间</th>
                    <th style="width:5%;">备注</th>
                </tr>  
                <tbody id="tbYCData">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><input type="checkbox" name="chkSelect" value="<%# Eval("Id") %>" /></td>
                <td><%# Eval("CTime", "{0:" + this.SiteSetting.DateTimeFormat + "}") %></td>
                <td><%# Eval("TaskID") %></td>
                <td><%# Eval("SatID") %></td>
                <td><%# Eval("Stype") %></td>
                <td><%# Eval("STTIMESTART", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("STTIMEEND", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("Reserve") %></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>   
                </tbody>           
            </table>            
        </FooterTemplate>
    </asp:Repeater>
    <table>
        <tr>
            <td class="listTitle-c2">
                <om:CollectionPager ID="cpYCData" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
    </div>
    <div runat="server" id="vUFData">
    <asp:Repeater ID="rpUFData" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:3%;"><input type="checkbox" onclick="checkAll(this)" /></th>
                    <th style="width:7%;">创建时间</th>
                    <th style="width:5%;">任务代号</th>
                    <th style="width:5%;">卫星编号</th>
                    <th style="width:10%;">数据接收开始时间</th>
                    <th style="width:10%;">数据接收结束时间</th>
                    <th style="width:5%;">用户帧代号</th>
                    <th style="width:3%;">延时标志</th>
                    <th style="width:7%;">数据开始时间</th>
                    <th style="width:7%;">数据结束时间</th>
                    <th style="width:10%;">目录</th>
                    <th style="width:10%;">数据文件名</th>
                    <th style="width:4%;">备注</th>
                </tr>  
                <tbody id="tbUFData">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" /></td>
                <td><%# Eval("CTime", "{0:" + this.SiteSetting.DateTimeFormat + "}") %></td>
                <td><%# Eval("TaskID") %></td>
                <td><%# Eval("SatID") %></td>
                <td><%# Eval("RECEIVEB", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("RECEIVEE", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("UserID") %></td>
                <td><%# Eval("DelaySI") %></td>
                <td><%# Eval("DATATIMEB", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("DATATIMEE", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("DIRECTORY")%></td>
                <td><%# Eval("FILENAME")%></td>
                <td><%# Eval("Reserve") %></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>   
                </tbody>           
            </table>            
        </FooterTemplate>
    </asp:Repeater>
    <table>
        <tr>
            <td class="listTitle-c2">
                <om:CollectionPager ID="cpUFData" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
    </div>
    <div runat="server" id="vFZData">
    <asp:Repeater ID="rpFZData" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <th style="width:5px;"><input type="checkbox" onclick="checkAll(this)" /></th>
                    <th style="width:15%;">创建时间</th>
                    <th style="width:10%;">任务代号</th>
                    <th style="width:15%;">开始时间</th>
                    <th style="width:15%;">结束时间</th>
                    <th style="width:30%;">文件路径</th>
                    <th style="width:10%;">备注</th>
                </tr>  
                <tbody id="tbFZData">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td><input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" /></td>
                <td><%# Eval("CTime","{0:"+this.SiteSetting.DateTimeFormat+"}") %></td>
                <td><%# Eval("TaskID") %></td>
                <td><%# Eval("STARTTIME", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("EndTIME", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("FileIndex")%></td>
                <td><%# Eval("Reserve")%></td>
            </tr>            
        </ItemTemplate>
        <FooterTemplate>   
                </tbody>           
            </table>            
        </FooterTemplate>
    </asp:Repeater>
    <table>
        <tr>
            <td class="listTitle-c2">
                <om:CollectionPager ID="cpFZData" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
    </div>
    <table class="listTitle">
        <tr id="trMessage" runat="server" visible="false">
            <td><asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label></td>
        </tr>
        <tr><td>
                <asp:HiddenField ID="hfycids" runat="server" ClientIDMode="Static"/>
                <asp:HiddenField ID="Hfufids" runat="server" ClientIDMode="Static"/>
                <asp:HiddenField ID="hffzids" runat="server" ClientIDMode="Static"/>
                <asp:HiddenField ID="hfSendWay" runat="server" ClientIDMode="Static"/></td></tr>
    </table>
    <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>
