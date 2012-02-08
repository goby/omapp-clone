<%@ Page MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master"  Language="C#" AutoEventWireup="true" CodeBehind="ZXJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.ZXJHEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .style2
        {
            width: 139px;
        }
        .style3
        {
            width: 50px;
        }
        .style4
        {
            width: 184px;
        }
        .style5
        {
            width: 43px;
        }
        .style6
        {
            width: 100%;
        }
        .style7
        {
            width: 48px;
        }
        .style8
        {
            width: 140px;
        }
        .style9
        {
        }
    </style>
<script src="../../Scripts/calendar.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    中心运行计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">

<div>
        <table cellpadding="0" class="edit" style="width: 800px;">
            <tr>
                <th>
                    计划开始时间</th>
                <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);"></asp:TextBox>
                </td>
                <th>
                    计划结束时间</th>
                <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    日期
                </th>
                <td>
                    <asp:TextBox ID="txtDate" CssClass="text" runat="server"></asp:TextBox>
                </td>
                <th>
                     <asp:HiddenField ID="HfID" runat="server" />
                     <asp:HiddenField ID="HfFileIndex" runat="server" />
                </th>
                <td>
                 <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfOverDate" runat="server" />
                </td>
            </tr>
            
        </table>
        <table class="edit" style="width:800px;">
            <tr>
                <th class="style3" rowspan="17">
                    试验<br />
                    内容</th>
                <th class="style2">
                    对应日期的试验个数</th>
                <td class="style5">
                    <asp:TextBox ID="txtSYCount" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
                <td class="style4">
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <th class="style2" rowspan="16">
                    试验项</th>
                <th colspan="2">
                    在当日计划中的ID</th>
                <td>
                    <asp:TextBox ID="txtSYID" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th colspan="2">
                    试验项目名称</th>
                <td>
                    <asp:TextBox ID="txtSYName" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th colspan="2">
                    试验开始日期及时间</th>
                <td>
                    <asp:TextBox ID="txtSYDateTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th colspan="2">
                    试验运行的天数</th>
                <td>
                    <asp:TextBox ID="txtSYDays" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style5" rowspan="3">
                    载荷</th>
                <th class="style4">
                    开始时间</th>
                <td>
                    <asp:TextBox ID="txtLoadStartTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    结束时间</th>
                <td>
                    <asp:TextBox ID="txtLoadEndTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    动作内容</th>
                <td>
                    <asp:TextBox ID="txtLoadContent" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style5" rowspan="3">
                    数传</th>
                <th class="style4">
                    圈次</th>
                <td>
                    <asp:TextBox ID="txtSCLaps" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    开始时间</th>
                <td>
                    <asp:TextBox ID="txtSCStartTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    结束时间</th>
                <td>
                    <asp:TextBox ID="txtSCEndTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style5" rowspan="3">
                    测控</th>
                <th class="style4">
                    圈次</th>
                <td>
                    <asp:TextBox ID="txtCKLaps" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    开始时间</th>
                <td>
                    <asp:TextBox ID="txtCKStartTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    结束时间</th>
                <td>
                    <asp:TextBox ID="txtCKEndTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style5" rowspan="3">
                    注数</th>
                <th class="style4">
                    最早时间要求</th>
                <td>
                    <asp:TextBox ID="txtZSFirst" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    最晚时间要求</th>
                <td>
                    <asp:TextBox ID="txtZSLast" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style4">
                    主要内容</th>
                <td>
                    <asp:TextBox ID="txtZSContent" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="edit style6" style="width:800px;">
            <tr>
                <th class="style7" rowspan="20">
                    工作<br />
                    计划</th>
                <th class="style8">
                    工作内容</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpWork" runat="server" 
                        onitemcommand="rpWork_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        工作</td>
                                    <td>
                                        <asp:TextBox ID="txtWC_Work" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        对应试验ID</td>
                                    <td>
                                        <asp:TextBox ID="txtWC_SYID" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        开始时间</td>
                                    <td>
                                        <asp:TextBox ID="txtWC_StartTime" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        最短持续时间</td>
                                    <td>
                                        <asp:TextBox ID="txtWC_MinTime" runat="server" Text='<%# Eval("MinTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        最长持续时间</td>
                                    <td>
                                        <asp:TextBox ID="txtWC_MaxTime" runat="server" Text='<%# Eval("MaxTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn1" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn2" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style8" rowspan="14">
                    载荷管理</th>
                <th class="style9" rowspan="7">
                    载荷管理</th>
                <th class="style9">
                    对应试验ID</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_SYID" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    卫星代号</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_SatID" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    进程</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_Process" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    事件</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_Event" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    动作内容</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_Action" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    开始时间</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_StartTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9">
                    结束时间</th>
                <td>
                    <asp:TextBox ID="txtWork_Load_EndTime" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style9" rowspan="7">
                    指令管理</th>
                <th>
                    对应试验ID</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_SYID" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    试验项目</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_SYItem" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    卫星代号</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_SatID" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    指令内容</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_Content" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    上注要求</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_UpRequire" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    指令发送方向</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_Direction" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    特殊需求</th>
                <td>
                    <asp:TextBox ID="txtWork_Command_SpecialRequire" runat="server" CssClass="text" 
                        Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style8">
                    试验数据处理</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpSYDataHandle" runat="server" 
                        onitemcommand="rpSYDataHandle_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                        对应试验ID</th>
                                    <td>
                                        <asp:TextBox ID="txtSHSYID" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        卫星代号</th>
                                    <td>
                                        <asp:TextBox ID="txtSHSatID" runat="server" Text='<%# Eval("SatID")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        圈次</th>
                                    <td>
                                        <asp:TextBox ID="txtSHLaps" runat="server" Text='<%# Eval("Laps")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        主站名称</th>
                                    <td>
                                        <asp:TextBox ID="txtSHMaintStation" runat="server" Text='<%# Eval("MainStationName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        备站名称</th>
                                    <td>
                                        <asp:TextBox ID="txtSHBakStation" runat="server" Text='<%# Eval("BakStationName")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        工作内容</th>
                                    <td>
                                        <asp:TextBox ID="txtSHContent" runat="server" Text='<%# Eval("Content")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        实时开始处理时间</th>
                                    <td>
                                        <asp:TextBox ID="txtSHStartTime" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        实时结束处理时间</th>
                                    <td>
                                        <asp:TextBox ID="txtSHEndTime" runat="server" Text='<%# Eval("EndTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        事后数据处理</th>
                                    <td>
                                        <asp:TextBox ID="txtSHAfterDH" runat="server" Text='<%# Eval("AfterWardsDataHandle")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn3" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn4" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style8">
                    指挥与监视</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpDirectAndMonitor" runat="server" 
                        onitemcommand="rpDirectAndMonitor_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                        对应试验ID</th>
                                    <td>
                                        <asp:TextBox ID="txtDMSYID" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        时间段</th>
                                    <td>
                                        <asp:TextBox ID="txtDMDateSection" runat="server" Text='<%# Eval("DateSection")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        指挥与监视任务</th>
                                    <td>
                                        <asp:TextBox ID="txtDMTask" runat="server" Text='<%# Eval("Task")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        实时显示任务</th>
                                    <td>
                                        <asp:TextBox ID="txtDMRTTask" runat="server" Text='<%# Eval("RealTimeShowTask")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn5" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn6" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style8">
                    实时控制</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpRealTimeControl" runat="server" 
                        onitemcommand="rpRealTimeControl_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                        工作</th>
                                    <td>
                                        <asp:TextBox ID="txtRCWork" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        对应试验ID</th>
                                    <td>
                                        <asp:TextBox ID="txtRCSYID" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        开始时间</th>
                                    <td>
                                        <asp:TextBox ID="txtRCStartTime" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        结束时间</th>
                                    <td>
                                        <asp:TextBox ID="txtRCEndTime" runat="server" Text='<%# Eval("EndTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        </td>
                                    <td>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn7" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn8" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style8">
                    试验评估</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpSYEstimate" runat="server" 
                        onitemcommand="SYEstimate_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                       对应试验ID</th>
                                    <td>
                                        <asp:TextBox ID="txtESYID" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        开始时间</th>
                                    <td>
                                        <asp:TextBox ID="txtEStartTime" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>
                                      完成时间</th>
                                    <td>
                                        <asp:TextBox ID="txtEEndTime" runat="server" Text='<%# Eval("EndTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn9" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn10" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style8">
                    数据管理</th>
                <td class="style9" colspan="3">
                    <asp:Repeater ID="rpDataManage" runat="server" 
                        onitemcommand="rpDataManage_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                        工作</th>
                                    <td>
                                        <asp:TextBox ID="txtMWork" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        对应数据描述</th>
                                    <td>
                                        <asp:TextBox ID="txtMDes" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <th>
                                        开始时间</th>
                                    <td>
                                        <asp:TextBox ID="txtMStartTime" runat="server" Text='<%# Eval("StartTime")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        结束时间</th>
                                    <td>
                                        <asp:TextBox ID="txtMEndTime" runat="server" Text='<%# Eval("EndTime")%>'></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                         <asp:Button ID="btn11" CssClass="button"  runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn12" CssClass="button"  runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <br />
        <br />
<div style="width:750px; text-align:center">
                    <asp:Button ID="btnSubmit" CssClass="button" runat="server" Text="保存计划" 
                        onclick="btnSubmit_Click" />
                        &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSubmit_Click" />
</div>
    </div>
</asp:Content>

