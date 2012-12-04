<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="SpaceTaskList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.SpaceTaskList" %>

<%@ Register src="../../ucs/ucOutTask.ascx" tagname="ucOutTask" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 引导数据-空间机动任务
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
                        <asp:TextBox ID="txtStartDate" ClientIDMode="Static" CssClass="text" runat="server"
                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <th>
                        结束时间：
                    </th>
                    <td>
                        <asp:TextBox ID="txtEndDate" ClientIDMode="Static" CssClass="text" runat="server"
                            onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    </td>
                    <th>
                        任务：
                    </th>
                    <td>
                        <uc1:ucOutTask ID="ucOutTask1" runat="server" AllowBlankItem="False" />
                    </td>
                    <td>
                        <asp:Button class="button" ID="btnSearch" runat="server" OnClick="btnSearch_Click"
                            Text="查询" Width="69px" style="margin-top: 0px" />
                        &nbsp;<%--<asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />--%>
                        <button class="button" onclick="return clearField();" style="width: 65px;">
                            清空</button>
                        &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtStartDate"
                            ControlToValidate="txtEndDate" Display="Dynamic" ErrorMessage="结束时间应大于起始时间" ForeColor="Red"
                            Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                        <div style="display: none;">
                            <asp:TextBox ID="txtId" runat="server" ClientIDMode="Static"></asp:TextBox>
                            <asp:Button ID="btnHidden" runat="server" ClientIDMode="Static" Text="btnHidden"
                                OnClick="btnHidden_Click" />
                            <asp:Button class="button" ID="btnSubmit" runat="server" ClientIDMode="Static" OnClick="btnSubmit_Click"
                                Text="发送" />
                            &nbsp;&nbsp;
                            <asp:Button class="button" ID="btnCancel" runat="server" ClientIDMode="Static" OnClick="btnCancel_Click"
                                Text="取消" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divResourceStatus" class="index_content_view">
            <asp:Panel ID="pnlAll1" runat="server">
                <table class="listTitle">
                    <tr>
                        <td class="listTitle-c1">
                            <button class="button" onclick="return selectAll();">
                                全选</button>&nbsp;&nbsp;
                            <button class="button" onclick="return sendYDSJ1();">
                                发送</button>
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
                            <%# Eval("CTime","{0:"+this.SiteSetting.DateTimeFormat+"}") %>
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
            <asp:Panel ID="pnlAll2" runat="server">
                <table class="listTitle">
                    <tr>
                        <td class="listTitle-c1">
                            <button class="button" onclick="return selectAll();">
                                全选</button>&nbsp;&nbsp;
                            <button class="button" onclick="return sendYDSJ1();">
                                发送</button>
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
    </asp:Panel>
    <div id="tartgetPanel" style="display: none;">
        <table>
            <tr>
                <td>
                    <b>请选择要使用的发送协议：</b>
                    <br />
                    <asp:RadioButtonList ID="rbtProtocl" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="2" Selected="True">Fep with Tcp</asp:ListItem>
                        <asp:ListItem Value="1">Fep with Udp</asp:ListItem>
                        <asp:ListItem Value="0">Ftp</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <b>请选择计划待发送的目标系统，可以多选</b>
                    <br />
                    <asp:CheckBoxList ID="ckbDestination" runat="server">
                    </asp:CheckBoxList>
                    <br />
                    <asp:Label ClientIDMode="Static" CssClass="error" ID="lblTargetMessage" runat="server"
                        ForeColor="Red" Style="display: none;">请选择要发送的目标系统</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divMessage" title="消息">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" CssClass="error"></asp:Label>
    </div>
    <div id="dialog-form" style="display: none" title="提示信息">
        <p class="content">
        </p>
    </div>
</asp:Content>
