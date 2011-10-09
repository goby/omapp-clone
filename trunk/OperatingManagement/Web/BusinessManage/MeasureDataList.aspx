<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MeasureDataList.aspx.cs" Inherits="OperatingManagement.Web.BusinessManage.MeasureDataList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <asp:Panel ID="pnlData" runat="server">
    <table cellpadding="0" class="style1">
        <tr>
            <td align="right" class="style2">
                开始日期：</td>
            <td class="style3">
                <asp:TextBox ID="txtStartDate" runat="server"  onclick="new WdatePicker(this);"></asp:TextBox>
            </td>
            <td align="right" class="style4">
                结束日期：</td>
            <td>
                <asp:TextBox ID="txtEndDate" runat="server"  onclick="new WdatePicker(this);"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="style2">
                数据类型：</td>
            <td class="style3">
                <asp:DropDownList ID="ddlType" runat="server" Height="16px" Width="148px">
                    <asp:ListItem Value="tb_ae">测角信息</asp:ListItem>
                    <asp:ListItem Value="tb_r">测距信息</asp:ListItem>
                    <asp:ListItem Value="tb_rr">测速信息</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td align="right" class="style4">
                &nbsp;</td>
            <td>
                &nbsp;</td>
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
                    CellPadding="3" GridLines="Horizontal" Width="100%" DataKeyNames="id" 
                    onrowcommand="gvList_RowCommand">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <Columns>
                        <asp:CheckBoxField />
                        <asp:BoundField DataField="source" HeaderText="信源" />
                        <asp:BoundField DataField="destination" HeaderText="信宿" />
                        <asp:BoundField DataField="taskid" HeaderText="任务代码" />
                        <asp:BoundField DataField="ctime" HeaderText="生成时间" />
                        <asp:ButtonField CommandName="ShowDetail" HeaderText="明细" Text="明细" />
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
                &nbsp;</td>
        </tr>
    </table>
    </asp:Panel>

    </asp:Content>