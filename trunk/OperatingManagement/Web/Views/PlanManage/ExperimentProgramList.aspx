<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ExperimentProgramList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.ExperimentProgramList" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 查看试验程序
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
 <div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
               <th>
                  起始时间：
               </th>
               <td>
                <asp:TextBox ID="txtStartDate" ClientIDMode="Static"  CssClass="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
               </td>
               <th>
                  结束时间：
               </th>
               <td>
                
                <asp:TextBox ID="txtEndDate" ClientIDMode="Static"  CssClass="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                
               </td>
               <td>
               <asp:Button class="button" ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" 
                    Width="69px" />
&nbsp;<%--<asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />--%>
                    <button class="button" onclick="return clearField();" style="width:65px;">清空</button>
                   &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                       ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" 
                       Display="Dynamic" ErrorMessage="结束时间应大于起始时间" ForeColor="Red" 
                       Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                   </td>
            </tr>
        </table>
 </div>
 <div id="divResourceStatus" class="index_content_view">
 <asp:Repeater ID="rpDatas" runat="server">
        <HeaderTemplate>
            <table class="list">
                <tr>
                    <%--<th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>--%>
                    <th style="width:150px;">项目名称</th>
                    <th style="width:150px;">类型</th>
                    <th style="width:150px;">编号</th>
                    <th style="width:150px;">开始时间</th>
                    <th style="width:150px;">结束时间</th>
                    <th style="width:70px;">明细</th>
                    <th style="width:70px;">生成计划</th>
                </tr>  
                <tbody id="tbUsers">        
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <%--<td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>--%>
                <td><%# Eval("pname")%></td>
                <td><%# Eval("ptype")%></td>
                <td><%# Eval("pnid")%></td>
                <td><%# Eval("starttime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td><%# Eval("endtime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%></td>
                <td>
                    <button class="button" onclick="return showDetail('<%# Eval("ID") %>')">明细</button>
                </td>
                <td>
                    <button class="button" onclick="return generatePlans('<%# Eval("ID") %>')">生成计划</button>
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
                          
            </td>
            <td class="listTitle-c2">
                <om:CollectionPager ID="cpPager" runat="server" ></om:CollectionPager>
            </td>
        </tr>
    </table>
 </div>
</asp:Content>
