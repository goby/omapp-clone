<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SpaceTaskList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.SpaceTaskList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
计划管理 &gt; 空间机动任务
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
        <asp:Panel ID="pnlData" runat="server">
        <div class="index_content_search">
            <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
               <th>
                  起始时间：
               </th>
               <td>
                <asp:TextBox ID="txtStartDate" ClientIDMode="Static"  CssClass="text" runat="server"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
               </td>
               <th>
                  结束时间：
               </th>
               <td>
                
                <asp:TextBox ID="txtEndDate" ClientIDMode="Static"  CssClass="text" runat="server"  onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                
               </td>
               <td>
               <asp:Button class="button" ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" 
                    Width="69px" />
&nbsp;<asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />
                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" 
                       ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" 
                       Display="Dynamic" ErrorMessage="结束时间应大于起始时间" ForeColor="Red" 
                       Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    <div style="display:none;">
                    <asp:TextBox ID="txtId" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:Button ID="btnHidden" runat="server" ClientIDMode="Static" Text="btnHidden"  OnClick="btnHidden_Click" />
                    </div>
                   </td>
            </tr>
        </table>
        </div>
        <div id="divResourceStatus" class="index_content_view">
            <asp:Panel ID ="pnlAll1" runat="server">
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendYDSJ1();">
                                    发送任务</button>
                            </td>
                            <td class="listTitle-c2">
                                <div class="load" id="submitIndicator" style="display: none">
                                    提交中，请稍候。。。</div>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
            <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 20px;">
                                        <input type="checkbox" onclick="checkAll(this)" />
                                    </th>
                                    <th style="width: 150px;">
                                        任务代号
                                    </th>
                                    <th style="width: 150px;">
                                        卫星名称
                                    </th>
                                    <th style="width: 150px;">
                                        历元日期
                                    </th>
                                    <th style="width: 150px;">
                                        历元时刻
                                    </th>
                                    <th>
                                        创建时间
                                    </th>
                                    <th style="width: 110px;">
                                        明细
                                    </th>
                                </tr>
                                <tbody id="tbYDSJs">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" />
                                </td>
                                <td>
                                    <%# Eval("TaskName")%>
                                </td>
                                <td>
                                    <%# Eval("SatName")%>
                                </td>
                                <td>
                                    <%# Eval("D")%>
                                </td>
                                <td>
                                    <%# Eval("T")%>
                                </td>
                                <td>
                                    <%# Eval("CreatedTime","{0:"+this.SiteSetting.DateTimeFormat+"}") %>
                                </td>
                                <td>
                                    <button class="button" onclick="return showDetail('<%# Eval("Id") %>')">
                                        明细</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
            <asp:Panel ID ="pnlAll2" runat="server">
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendYDSJ1();">
                                    发送任务</button>
                            </td>
                            <td class="listTitle-c2">
                                <om:CollectionPager ID="cpPager" runat="server">
                                </om:CollectionPager>
                            </td>
                        </tr>
                    </table>
                    </asp:Panel>
        </div>
        </asp:Panel>
        <asp:Panel ID="pnlDestination" runat="server">
        <div id="tartgetPanel">
        <table>
            <tr>
                <td align="center">
                    <asp:CheckBoxList ID="ckbDestination" runat="server">
                        <asp:ListItem Value="0">863-YZ4701机动站</asp:ListItem>
                        <asp:ListItem Value="1">863-YZ4702机动站</asp:ListItem>
                        <asp:ListItem Value="2">西安卫星测控中心（转发到S、X地面站、东风站）</asp:ListItem>
                        <asp:ListItem Value="3">总参二部信息处理中心（转发到相关地面站）</asp:ListItem>
                        <asp:ListItem Value="4">总参三部技侦中心（转发到相关地面站）</asp:ListItem>
                        <asp:ListItem Value="5">总参气象水文空间天气总站资料处理中心（转发到相关地面站）</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button  class="button" ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button  class="button" ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" />
                </td>
            </tr>
        </table>
    </div>
        </asp:Panel>
        <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>
