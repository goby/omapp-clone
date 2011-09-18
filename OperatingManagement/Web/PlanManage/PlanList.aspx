<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PlanList.aspx.cs" Inherits="OperatingManagement.Web.PlanManage.PlanList" %>
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
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuIndex" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    首页
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel ID="pnlData" runat="server">
    <table cellpadding="0" class="style1">
        <tr>
            <td align="right" class="style2" rowspan="3">
                计划类型：</td>
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
            <td align="right" class="style4">
                计划时效：</td>
            <td>
                <asp:DropDownList ID="ddlAging" runat="server" Height="16px" Width="146px">
                    <asp:ListItem Value="1">周计划</asp:ListItem>
                    <asp:ListItem Value="2">日计划</asp:ListItem>
                    <asp:ListItem Value="3">应急计划</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="style4">
                开始日期：</td>
            <td>
                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style4">
                结束日期：</td>
            <td>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style5">
            </td>
            <td class="style6" colspan="3">
                <asp:Button ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" 
                    Width="69px" />
&nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" Text="重置" Width="65px" />
            </td>
        </tr>
        <tr>
            <td class="style2">
                &nbsp;</td>
            <td class="style3">
                &nbsp;</td>
            <td class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td class="style2" colspan="4">
                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" GridLines="Horizontal" Width="100%" 
                    onrowcommand="gvList_RowCommand">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:BoundField DataField="jhid" HeaderText="计划ID" />
                        <asp:BoundField DataField="plantype" HeaderText="类型" />
                        <asp:BoundField DataField="planid" HeaderText="编号" />
                        <asp:BoundField DataField="starttime" HeaderText="开始时间" />
                        <asp:BoundField HeaderText="结束时间" DataField="endtime" />
                        <asp:ButtonField CommandName="EditPlan" Text="编辑" />
                        <asp:ButtonField Text="明细" ValidationGroup="ShowDetail" />
                    </Columns>
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td  colspan="4" align="center">
                <asp:Button ID="btnSend" runat="server" Text="发送计划" onclick="btnSend_Click" />
            </td>
        </tr>
    </table>
   </asp:Panel>

    <asp:Panel ID="pnlDestination" runat="server">
        <table class="style7">
            <tr>
                <td align="center">
                    <asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="取消" />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
