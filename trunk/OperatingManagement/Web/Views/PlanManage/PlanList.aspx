﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.PlanList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 查看计划
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
        <asp:Panel ID="pnlData" runat="server">
        <div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
               <th>
                  起始时间：
               </th>
               <td>
                <asp:TextBox ID="txtStartDate" ClientIDMode="Static"  CssClass="text" runat="server"></asp:TextBox>
               </td>
               <th>
                  结束时间：
               </th>
               <td>
                
                <asp:TextBox ID="txtEndDate" ClientIDMode="Static"  CssClass="text" runat="server"></asp:TextBox>
                
               </td>
               <th>
                    计划类型：
                </th>
                <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="150px" Height="20px">
                        <%--<asp:ListItem Value="0">==全部==</asp:ListItem>--%>
                        <asp:ListItem Value="YJJH">应用研究工作计划</asp:ListItem>
                        <asp:ListItem Value="XXXQ">空间信息需求</asp:ListItem>
                        <asp:ListItem Value="DMJH">地面站工作计划</asp:ListItem>
                        <asp:ListItem Value="ZXJH">中心运行计划</asp:ListItem>
                        <asp:ListItem Value="TYSJ">仿真推演试验数据</asp:ListItem>
                        <asp:ListItem Value="SBJH">设备工作计划</asp:ListItem>
                        </asp:DropDownList>
                </td>
               <td>
               <asp:Button class="button" ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" 
                    Width="69px" />
&nbsp;<asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />
                    <div style="display:none;">
                        <asp:TextBox ID="txtId" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ID="txtPlanID" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ID="txtPlanType" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:Button ID="btnHidden" runat="server" ClientIDMode="Static" Text="btnHidden" 
                                OnClick="btnHidden_Click" />
                        </div>
                   </td>
            </tr>
        </table>
        </div>
        <div id="divResourceStatus" class="index_content_view">
                <asp:Panel ID ="pnlAll1" runat="server">
                    <div id="selectAll1" >
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendPlan();">
                                    发送所选计划</button>
                            </td>
                            <td class="listTitle-c2">
                                <div class="load" id="submitIndicator" style="display: none">
                                    提交中，请稍候。。。</div>
                            </td>
                        </tr>
                    </table>
                    </div>
         </asp:Panel>
                <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>
                                    <th style="width: 150px;">
                                        计划编号
                                    </th>
                                    <th style="width: 150px;">
                                        任务代号
                                    </th>
                                    <th style="width: 150px;">
                                        计划类别
                                    </th>
                                    <th style="width: 150px;">
                                        开始时间
                                    </th>
                                    <th style="width: 150px;">
                                        结束时间
                                    </th>
                                    <th style="width: 70px;">
                                        编辑
                                    </th>
                                        <%--                                    <th style="width: 70px;">
                                        明细
                                    </th>--%>
                                       <%-- <th style="width: 70px;">
                                            发送
                                        </th>--%>
                                    </tr>
                           <tbody id="tbPlans">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                    <input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" />
                                    </td>
                                    <td>
                                        <%# Eval("planid")%>
                                    </td>
                                    <td>
                                        <%# Eval("taskid")%>
                                    </td>
                                    <td>
                                        <%# Eval("plantype")%>
                                    </td>
                                    <td>
                                        <%# Eval("starttime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                    </td>
                                    <td>
                                        <%# Eval("endtime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                    </td>
                                    <td>
                                        <button class="button" onclick="return showEdit('<%# Eval("ID") %>','<%# Eval("PLANTYPE") %>')">
                                            编辑</button>
                                    </td>
                                    <%--                                <td>
                                    <button class="button" onclick="return showDetail('<%# Eval("ID") %>','<%# Eval("PLANTYPE") %>')">
                                        明细</button>
                                </td>--%>
                                <%--<td>
                                    <button class="button" onclick="return showSend('<%# Eval("ID") %>','<%# Eval("PLANID") %>','<%# Eval("PLANTYPE") %>')">
                                        发送计划</button>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                <asp:Panel ID ="pnlAll2" runat="server">
                    <div id="selectAll2" >
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendPlan();">
                                    发送所选计划</button>
                            </td>
                            <td class="listTitle-c2">
                                <om:CollectionPager ID="cpPager" runat="server">
                                </om:CollectionPager>
                            </td>
                        </tr>
                    </table>
                    </div>
                    </asp:Panel>
        </div>
       </asp:Panel>
        <asp:Panel ID="pnlDestination" runat="server">
        
        </asp:Panel>
        <div id="tartgetPanel" style="display:none">
        <table style = " text-align:center;">
            <tr>
                <td align="center"  style="text-align: center">
                <b>请选择计划待发送的目标系统，可以多选</b>
                <br />
                    <asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                <%--<button class="button"  onclick="callSend();">发送</button>--%>
                <div>
                    <asp:Button ClientIDMode="Static" ID="btnSubmit" class="button" runat="server" OnClick="btnSubmit_Click"
                        Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="button" runat="server" OnClick="btnCancel_Click"
                        Text="取消" />
                        </div>
                </td>
            </tr>
        </table>
    </div>
        <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
        </div>
</asp:Content>
