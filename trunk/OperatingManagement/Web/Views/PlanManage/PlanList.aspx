<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
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
    <div id="divData">
            <table cellpadding="0" class="edit1" width="850px">
            <tr>
                    <th>
                        开始日期：</th>
                    <td>
                        <asp:TextBox ID="txtStartDate" ClientIDMode="Static"   runat="server" CssClass="text" Width="300px"></asp:TextBox>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        结束日期：</th>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" ClientIDMode="Static" 
                            CssClass="text" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                    计划类型：
                </th>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" Width="305px" Height="20px">
                        <asp:ListItem Value="YJJH">应用研究工作计划</asp:ListItem>
                            <asp:ListItem Value="XXXQ">空间信息需求</asp:ListItem>
                        <asp:ListItem Value="DMJH">地面站工作计划</asp:ListItem>
                        <asp:ListItem Value="ZXJH">中心运行计划</asp:ListItem>
                        <asp:ListItem Value="TYSJ">仿真推演试验数据</asp:ListItem>
                        <asp:ListItem Value="SBJH">设备工作计划</asp:ListItem>
                        </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                    <td>
                    <asp:Button CssClass="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                        Text="查询" Width="69px" />
                    &nbsp;&nbsp;
                        <asp:Button ID="btnReset" CssClass="button" runat="server" Text="重置" Width="65px"
                            OnClick="btnReset_Click" />
                        <%--<button class="button" onclick="return reset();" style="width: 65px;">
                        重置</button>--%>
                        <div style="display:none;">
                        <asp:TextBox ID="txtId" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtPlanID" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:TextBox ID="txtPlanType" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <asp:Button ID="btnHidden" runat="server" ClientIDMode="Static" Text="btnHidden" 
                                OnClick="btnHidden_Click" />
                        </div>
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="2">
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
                </td>
            </tr>
        </table>
    </div>
       </asp:Panel>
        <asp:Panel ID="pnlDestination" runat="server">
    <div id="tartgetPanel">
        <table style = " text-align:center;">
            <tr>
                <td align="center"  style="text-align: center">
                    <asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>
                    <br />
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnSubmit" class="button" runat="server" OnClick="btnSubmit_Click"
                        Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="button" runat="server" OnClick="btnCancel_Click"
                        Text="取消" />
                </td>
            </tr>
        </table>
    </div>
        </asp:Panel>
            <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>
