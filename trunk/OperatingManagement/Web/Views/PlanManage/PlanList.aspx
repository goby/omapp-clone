<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="OperatingManagement.Web.PlanManage.PlanList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2
        {
        }
        .style3
        {
            width: 179px;
        }
        .style4
        {
            width: 125px;
        }
        .style5
        {
            width: 131px;
            height: 18px;
        }
        .style6
        {
            height: 18px;
        }
        .style7
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    查询计划
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
    <%--    <asp:Panel ID="pnlData" runat="server">--%>
    <div id="divData">
        <table cellpadding="0" class="edit">
            <tr>
                <th align="right" class="style2" rowspan="3">
                    计划类型：
                </th>
                <td class="style3" rowspan="3">
                    <asp:RadioButtonList ID="rbtType" runat="server">
                        <asp:ListItem>应用研究工作计划</asp:ListItem>
                        <asp:ListItem>空间信息需求</asp:ListItem>
                        <asp:ListItem>面站工作计划</asp:ListItem>
                        <asp:ListItem>中心运行计划</asp:ListItem>
                        <asp:ListItem>仿真推演试验数据</asp:ListItem>
                        <asp:ListItem>设备工作计划</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <th align="right" class="style4">
                    计划时效：
                </th>
                <td>
                    <asp:DropDownList ID="ddlAging" runat="server" Height="24px" Width="146px">
                        <asp:ListItem Value="1">周计划</asp:ListItem>
                        <asp:ListItem Value="2">日计划</asp:ListItem>
                        <asp:ListItem Value="3">应急计划</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th align="right" class="style4">
                    开始日期：
                </th>
                <td>
                    <asp:TextBox ID="txtStartDate" CssClass="text" runat="server" onclick="setday(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th align="right" class="style4">
                    结束日期：
                </th>
                <td>
                    <asp:TextBox ID="txtEndDate" CssClass="text" runat="server" onclick="setday(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style5">
                </td>
                <td class="style6" colspan="3">
                    <asp:Button CssClass="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                        Text="查询" Width="69px" />
                    &nbsp;&nbsp;
                    <%--<asp:Button ID="btnReset" runat="server" Text="重置" Width="65px" />--%>
                    <button class="button" onclick="return reset();" style="width: 65px;">
                        重置</button>
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" colspan="4">
                    <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <%--<th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>--%>
                                    <th style="width: 150px;">
                                        计划编号
                                    </th>
                                    <th style="width: 150px;">
                                        任务代号
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
                                    <th style="width: 70px;">
                                        明细
                                    </th>
                                    <th style="width: 70px;">
                                        发送
                                    </th>
                                </tr>
                                <tbody id="tbUsers">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <%--<td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>--%>
                                <td>
                                    <%# Eval("planid")%>
                                </td>
                                <td>
                                    <%# Eval("taskid")%>
                                </td>
                                <td>
                                    <%# Eval("starttime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                </td>
                                <td>
                                    <%# Eval("endtime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                </td>
                                <td>
                                    <button class="button" onclick="return showEdit('<%# Eval("ID") %>')">
                                        编辑</button>
                                </td>
                                <td>
                                    <button class="button" onclick="return showDetail('<%# Eval("ID") %>')">
                                        明细</button>
                                </td>
                                <td>
                                    <button class="button" onclick="return showSend('<%# Eval("ID") %>')">
                                        发送计划</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
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
                </td>
            </tr>
        </table>
    </div>
    <%--   </asp:Panel>--%>
    <%--    <asp:Panel ID="pnlDestination" runat="server">--%>
    <div id="tartgetPanel" style="display: none">
        <table class="style7">
            <tr>
                <td align="center">
                    <asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="取消" />
                </td>
            </tr>
        </table>
    </div>
    <%--    </asp:Panel>--%>
</asp:Content>
