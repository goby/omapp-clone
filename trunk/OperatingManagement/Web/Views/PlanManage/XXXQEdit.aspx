<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="XXXQEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.XXXQEdit" %>

<%@ Register src="../../ucs/ucTask.ascx" tagname="ucTask" tagprefix="uc1" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            color: #FF0000;
        }
        .style4
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 空间信息需求
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
         <tr>
            <th>任务代号(<span class="red">*</span>)</th>
            <td>
                <uc1:ucTask ID="ucTask1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th class="style1">卫星(<span class="red">*</span>)</th>
            <td>
                <uc2:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th class="style1">计划开始时间</th>
            <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"  ClientIDMode="Static"  Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划结束时间</th>
            <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"  ClientIDMode="Static"  Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>备注</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="50" 
                    Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                    <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                    <asp:HiddenField ID="hfTaskID" runat="server" />
                    <asp:HiddenField ID="hfSatID" runat="server" />
                    <asp:HiddenField ID="hfStatus" runat="server" />
                </td>
        </tr>
</table>
    <asp:Panel ID="pnlMBXQ" runat="server">
        <table class="edit" style="width: 800px;">
            <tr>
                <td colspan="2" class="style4">
                    <strong>空间目标信息需求</strong></td>
            </tr>
            <tr>
                <th class="style1">
                    用户名称 
                </th>
                <td>
                    <asp:TextBox ID="txtMBUser" runat="server" CssClass="text" MaxLength="20" 
                        Width="300px">运控评估中心</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    需求制定时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTime" runat="server" Width="300px" CssClass="text" MaxLength="12"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDDHHMM</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    目标信息标志
                </th>
                <td>
                    <asp:TextBox ID="txtMBTargetInfo" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    开始时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTimeSection1" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    结束时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTimeSection2" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    信息条数
                </th>
                <td>
                    <asp:TextBox ID="txtMBSum" runat="server" Width="300px" CssClass="text" MaxLength="50"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:Repeater ID="rpMB" runat="server" OnItemCommand="rpMB_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        卫星名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        提供时间
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoTime" runat="server" Text='<%# Eval("InfoTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn9" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn10" CssClass="button" runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlHJXQ" runat="server">
        <table class="edit" style="width: 800px;">
            <tr>
                <td colspan="2" class="style4">
                    <strong>空间环境信息需求</strong></td>
            </tr>
            <tr>
                <th class="style1">
                    用户名称 
                </th>
                <td>
                    <asp:TextBox ID="txtHJUser" runat="server" CssClass="text" MaxLength="20" 
                        Width="300px">运控评估中心</asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    需求制定时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTime" runat="server" Width="300px" CssClass="text" MaxLength="12"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDDHHMM</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    环境信息标志
                </th>
                <td>
                    <asp:TextBox ID="txtHJEnvironInfo" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    开始时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTimeSection1" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    结束时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTimeSection2" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    信息条数
                </th>
                <td>
                    <asp:TextBox ID="txtHJSum" runat="server" Width="300px" CssClass="text" MaxLength="50"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:Repeater ID="rpHJ" runat="server" OnItemCommand="rpHJ_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        卫星名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        区域范围
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoArea" runat="server" Text='<%# Eval("InfoArea")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        提供时间
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoTime" runat="server" Text='<%# Eval("InfoTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn11" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn12" CssClass="button" runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </asp:Panel>
<div style="width:750px; text-align:center">
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" OnClick="btnSubmit_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    OnClick="btnSaveTo_Click" />
</div>
<div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>
