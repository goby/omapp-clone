<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ExperimentPlanList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.ExperimentPlanList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 100%;
            border-collapse: collapse;
            border-style: solid;
            border-width: 1px;
        }
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
    </style>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 查看试验计划
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
    <table  class="edit"  cellpadding="0">
        <tr>
            <th align="right" class="style2">
                开始日期：
            </th>
            <td class="style3">
                <asp:TextBox ID="txtStartDate" CssClass="text" runat="server" onclick="setday(this);"></asp:TextBox>
            </td>
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
                <asp:Button class="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                    Text="查询" Width="65px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />
                <%--<button class="button" onclick="return resetAll();" style="width: 65px;">
                    重置</button>--%>
            </td>
        </tr>
        <tr>
            <td colspan="4">
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
                                    信源
                                </th>
                                <th style="width: 150px;">
                                    信宿
                                </th>
                                <th style="width: 150px;">
                                    任务代码
                                </th>
                                <th style="width: 150px;">
                                    生成时间
                                </th>
                                <th style="width: 70px;">
                                    明细
                                </th>
                            </tr>
                            <tbody id="tbUsers">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <%--<td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>--%>
                            <td>
                                <%# Eval("source")%>
                            </td>
                            <td>
                                <%# Eval("destination")%>
                            </td>
                            <td>
                                <%# Eval("taskid")%>
                            </td>
                            <td>
                                <%# Eval("ctime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                            </td>
                            <td>
                                <button class="button" onclick="return showDetail('<%# Eval("JHID") %>')">
                                    明细</button>
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
</asp:Content>
