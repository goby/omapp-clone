<%@ Page MaintainScrollPositionOnPostback="true" MasterPageFile="~/Site.Master" Language="C#"
    AutoEventWireup="true" CodeBehind="ZXJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.ZXJHEdit" %>

<%@ Register Src="../../ucs/ucTask.ascx" TagName="ucTask" TagPrefix="uc1" %>
<%@ Register Src="../../ucs/ucSatellite.ascx" TagName="ucSatellite" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 中心运行计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <div>
        <table cellpadding="0" class="edit1" style="width: 950px;">
            <tr>
                <th style="width: 120px;">
                    任务代号(<span class="red">*</span>)
                </th>
                <td style="width: 332px;">
                    <uc1:ucTask ID="ucTask1" runat="server" AllowBlankItem="False" />
                </td>
                <th style="width: 120px;">
                    卫星(<span class="red">*</span>)
                </th>
                <td>
                    <uc2:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    计划开始时间
                </th>
                <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" MaxLength="10"
                        ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPlanStartTime"
                        ErrorMessage="开始时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
                <th style="width: 120px;">
                    计划结束时间
                </th>
                <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" MaxLength="10" 
                        ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPlanEndTime"
                        ErrorMessage="结束时间不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtPlanStartTime" ControlToValidate="txtPlanEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th>
                    日期
                </th>
                <td>
                    <asp:TextBox ID="txtDate" CssClass="text" runat="server" MaxLength="10" ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <th style="width: 100px;">
                    备注
                </th>
                <td>
                    <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="100" Width="310px"
                        Height="40px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
           <div  id="detailtable">
        <table  class="edit1" style="width: 950px;">
            <tr>
                <th colspan="4" style="color: Black; text-align: left;">
                    <b>试验内容</b>
                </th>
            </tr>
            <tr>
                <th style="width: 120px;">
                    对应日期的试验个数
                </th>
                <td colspan="3">
                    <asp:TextBox ID="txtSYCount" runat="server" CssClass="text" Text='<%# Eval("SYCount")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57" 
                    onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" style="ime-mode:Disabled"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                        ControlToValidate="txtSYCount" ErrorMessage="只能是数字" ForeColor="Red" 
                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    在当日计划中的ID
                </th>
                <td  style="width: 332px;">
                    <asp:TextBox ID="txtSYID" runat="server" CssClass="text" Text='<%# Eval("SYID")%>'></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    试验项目名称
                </th>
                <td>
                    <asp:TextBox ID="txtSYName" runat="server" CssClass="text" Text='<%# Eval("SYName")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    试验开始日期及时间
                </th>
                <td>
                    <asp:TextBox ID="txtSYDateTime" runat="server" CssClass="text" Text='<%# Eval("SYDateTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH-mm-ss'})"></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    试验运行的天数
                </th>
                <td>
                    <asp:TextBox ID="txtSYDays" runat="server" CssClass="text" Text='<%# Eval("SYDays")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57" 
                    onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" style="ime-mode:Disabled"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtSYDays" ErrorMessage="只能是数字" ForeColor="Red" 
                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    载荷-开始时间
                            </th>
                <td>
                                <asp:TextBox ID="txtLoadStartTime" runat="server" CssClass="text" Text='<%# Eval("SYLoadStartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    载荷-结束时间</th>
                <td>
                                <asp:TextBox ID="txtLoadEndTime" runat="server" CssClass="text" Text='<%# Eval("SYLoadEndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator4" runat="server" 
                        ControlToCompare="txtLoadStartTime" ControlToValidate="txtLoadEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    载荷-载荷名称</th>
                <td>
                                <asp:TextBox ID="txtLoadName" runat="server" CssClass="text" 
                        Text='<%# Eval("SYLoadName")%>' ></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    载荷-动作内容</th>
                <td>
                                <asp:TextBox ID="txtLoadContent" runat="server" CssClass="text" Text='<%# Eval("SYLoadContent")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    数传-站编号</th>
                <td>
                                <asp:TextBox ID="txtSCStationNO" runat="server" CssClass="text" 
                        Text='<%# Eval("SY_SCStationNO")%>'></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    数传-设备编号</th>
                <td>
                                <asp:TextBox ID="txtSCEquipmentNO" runat="server" CssClass="text" 
                        Text='<%# Eval("SY_SCEquipmentNO")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    数传-频段</th>
                <td>
                                <asp:TextBox ID="txtSCFrequencyBand" runat="server" CssClass="text" 
                        Text='<%# Eval("SY_SCFrequencyBand")%>'></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    数传-圈次</th>
                <td>
                                <asp:TextBox ID="txtSCLaps" runat="server" CssClass="text" Text='<%# Eval("SY_SCLaps")%>'></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    数传-开始时间</th>
                <td>
                                <asp:TextBox ID="txtSCStartTime" runat="server" CssClass="text" Text='<%# Eval("SY_SCStartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                </td>
                <th style="width: 120px;">
                    数传-结束时间</th>
                <td>
                                <asp:TextBox ID="txtSCEndTime" runat="server" CssClass="text" Text='<%# Eval("SY_SCEndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToCompare="txtSCStartTime" ControlToValidate="txtSCEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    测控-站编号</th>
                <td>
                                <asp:TextBox ID="txtCKStationNO" runat="server" CssClass="text" 
                        Text='<%# Eval("SY_CKStationNO")%>'></asp:TextBox>
                            </td>
                <th style="width: 120px;">
                    测控-设备编号</th>
                <td>
                                <asp:TextBox ID="txtCKEquipmentNO" runat="server" CssClass="text" 
                        Text='<%# Eval("SY_CKEquipmentNO")%>'></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    测控-开始时间</th>
                <td>
                                <asp:TextBox ID="txtCKStartTime" runat="server" CssClass="text" Text='<%# Eval("SY_CKStartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                <th style="width: 120px;">
                    测控-结束时间</th>
                <td>
                                <asp:TextBox ID="txtCKEndTime" runat="server" CssClass="text" Text='<%# Eval("SY_CKEndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator3" runat="server" 
                        ControlToCompare="txtCKStartTime" ControlToValidate="txtCKEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator></td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    测控-圈次</th>
                <td>
                                <asp:TextBox ID="txtCKLaps" runat="server" CssClass="text" Text='<%# Eval("SY_CKLaps")%>'></asp:TextBox>
                            </td>
                <th style="width: 120px;">
                    注数-主要内容</th>
                <td>
                                <asp:TextBox ID="txtZSContent" runat="server" CssClass="text" Text='<%# Eval("SY_ZSContent")%>'></asp:TextBox>
                            </td>
            </tr>
            <tr>
                <th style="width: 120px;">
                    注数-最早时间要求</th>
                <td>
                                <asp:TextBox ID="txtZSFirst" runat="server" CssClass="text" Text='<%# Eval("SY_ZSFirst")%>'></asp:TextBox>
                            </td>
                <th style="width: 120px;">
                    注数-最晚时间要求</th>
                <td>
                                <asp:TextBox ID="txtZSLast" runat="server" CssClass="text" Text='<%# Eval("SY_ZSLast")%>'></asp:TextBox>
                            </td>
            </tr>
            </table>
        <table class="edit1" style="width: 950px">
            <tr>
                <th colspan="2" style="color: Black; text-align: left;">
                    <b>工作计划</b>
                </th>
            </tr>
            <tr>
                <th  style="width: 120px;">
                    任务管理
                </th>
                <td>
                    <asp:Repeater ID="rpWork" runat="server" OnItemCommand="rpWork_ItemCommand">
                        <HeaderTemplate>
                            <table class="edit1">
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th style="width: 120px;">
                                        工作
                                </th>
                                <td  style="width: 270px;">
                                    <asp:TextBox ID="txtWC_Work" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                </td>
                                 <th style="width: 120px;">
                                        对应试验ID
                                 </th>
                                <td  style="width: 270px;">
                                    <asp:TextBox ID="txtWC_SYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                </td>
                                </tr>
                                <tr>
                                <th style="width: 120px;">
                                        开始时间
                                    </th>
                                <td>
                                    <asp:TextBox ID="txtWC_StartTime" CssClass="text" runat="server" Text='<%# Eval("StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <th style="width: 120px;">
                                        最短持续时间
                                    </th>
                                <td>
                                    <asp:TextBox ID="txtWC_MinTime" CssClass="text" runat="server" Text='<%# Eval("MinTime")%>'></asp:TextBox>
                                </td>
                                </tr>
                                <tr>
                                <th style="width: 120px;">
                                        最长持续时间
                                    </th>
                                <td>
                                    <asp:TextBox ID="txtWC_MaxTime" CssClass="text" runat="server" Text='<%# Eval("MaxTime")%>'></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btn1" CausesValidation="False" CssClass="button" runat="server" CommandName="Add"
                                        Text="添加" />
                                    <asp:Button ID="btn2" CausesValidation="False" CssClass="button" runat="server" CommandName="Del"
                                        Text="删除" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th>
                    载荷管理
                </th>
                <td>
                    <table class="edit1">
                        <tr>
                            <th colspan="4" style="text-align: center">
                                载荷管理
                            </th>
                        </tr>
                        <tr>
                            <th style="width: 120px;">
                                对应试验ID
                            </th>
                            <td style="width: 270px;">
                                <asp:TextBox ID="txtWork_Load_SYID" runat="server" CssClass="text" Text='<%# Eval("Work_Load_SYID")%>'></asp:TextBox>
                            </td>
                            <th style="width: 120px;">
                                卫星代号
                            </th>
                            <td style="width: 270px;">
                                <asp:TextBox ID="txtWork_Load_SatID" runat="server" CssClass="text" Text='<%# Eval("Work_Load_SatID")%>'></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <th>
                                载荷名称</th>
                            <td>
                                <asp:TextBox ID="txtWork_Load_Name" runat="server" CssClass="text" 
                                    Text='<%# Eval("Work_Load_Name")%>'></asp:TextBox>
                            </td>
                            <th>
                                进程</th>
                            <td>
                                <asp:TextBox ID="txtWork_Load_Process" runat="server" CssClass="text" Text='<%# Eval("Work_Load_Process")%>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                事件</th>
                            <td>
                                <asp:TextBox ID="txtWork_Load_Event" runat="server" CssClass="text" Text='<%# Eval("Work_Load_Event")%>'></asp:TextBox>
                            </td>
                            <th>
                                动作</th>
                            <td>
                                <asp:TextBox ID="txtWork_Load_Action" runat="server" CssClass="text" Text='<%# Eval("Work_Load_Action")%>'></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <th>
                                开始时间</th>
                            <td>
                                <asp:TextBox ID="txtWork_Load_StartTime" runat="server" CssClass="text" 
                                Text='<%# Eval("Work_Load_StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <th>
                            
                                结束时间</th>
                            <td colspan="2">
                                <asp:TextBox ID="txtWork_Load_EndTime" runat="server" CssClass="text" 
                                Text='<%# Eval("Work_Load_EndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator5" runat="server" 
                        ControlToCompare="txtWork_Load_StartTime" ControlToValidate="txtWork_Load_EndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <th colspan="4" style="text-align: center">
                                指令制作</th>
                        </tr>
                        <tr>
                            <th style="width: 120px;">
                                对应试验ID
                            </th>
                            <td style="width: 270px;">
                                <asp:TextBox ID="txtWork_Command_SYID" runat="server" CssClass="text" Text='<%# Eval("Work_Command_SYID")%>'></asp:TextBox>
                            </td>
                            <th style="width: 120px;">
                                试验项目
                            </th>
                            <td style="width: 270px;">
                                <asp:TextBox ID="txtWork_Command_SYItem" runat="server" CssClass="text" Text='<%# Eval("Work_Command_SYItem")%>'></asp:TextBox>
                            </td>
                            </tr>
                            <tr>
                            <th>
                                卫星代号
                            </th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_SatID" runat="server" CssClass="text" Text='<%# Eval("Work_Command_SatID")%>'></asp:TextBox>
                            </td>
                            <th>
                                指令内容
                            </th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_Content" runat="server" CssClass="text" Text='<%# Eval("Work_Command_Content")%>'></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                上注要求
                            </th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_UpRequire" runat="server" CssClass="text" Text='<%# Eval("Work_Command_UpRequire")%>'></asp:TextBox>
                            </td>
                            <th>
                                指令发送方向
                            </th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_Direction" runat="server" CssClass="text" Text='<%# Eval("Work_Command_Direction")%>'></asp:TextBox>
                            </td>
                            </tr>
                        <tr>
                            <th>
                                开始时间</th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_StartTime" runat="server" CssClass="text" 
                                Text='<%# Eval("Work_Command_StartTime")%>' 
                                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            </td>
                            <th>
                                结束时间</th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_EndTime" runat="server" CssClass="text" 
                                Text='<%# Eval("Work_Command_EndTime")%>' 
                                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator14" runat="server" 
                        ControlToCompare="txtWork_Command_StartTime" ControlToValidate="txtWork_Command_EndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                            </td>
                            </tr>
                        <tr>
                            <th>
                                特殊需求
                            </th>
                            <td>
                                <asp:TextBox ID="txtWork_Command_SpecialRequire" runat="server" CssClass="text" Text='<%# Eval("Work_Command_SpecialRequire")%>'></asp:TextBox>
                            </td>
                            <th>
                            </th>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <th>
                    试验数据处理
                </th>
                <td>
                    <asp:Repeater ID="rpSYDataHandle" runat="server" OnItemCommand="rpSYDataHandle_ItemCommand">
                        <HeaderTemplate>
                            <table class="edit1">
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th style="width: 120px;">
                                        对应试验ID
                                </th>
                                <td style="width: 270px;">
                                    <asp:TextBox ID="txtSHSYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                </td>
                                <th style="width: 120px;">
                                        卫星代号
                                    </th>
                                <td style="width: 270px;">
                                    <asp:TextBox  ID="txtSHSatID" CssClass="text" runat="server" Text='<%# Eval("SatID")%>'></asp:TextBox>
                                </td>
                                </tr><tr>
                                <th>
                                        圈次
                                </th>
                                <td>
                                    <asp:TextBox  ID="txtSHLaps" CssClass="text" runat="server" Text='<%# Eval("Laps")%>'></asp:TextBox>
                                </td>
                                <th>
                                        工作内容
                                </th>
                                <td>
                                    <asp:TextBox  ID="txtSHContent" CssClass="text" runat="server" Text='<%# Eval("Content")%>'></asp:TextBox>
                                </td>
                                </tr><tr>
                                <th>
                                        主站名称
                                </th>
                                <td>
                                    <asp:TextBox  ID="txtSHMaintStation" CssClass="text" runat="server" Text='<%# Eval("MainStationName")%>'></asp:TextBox>
                                </td>
                                <th>
                                        主站设备
                                </th>
                                <td>
                                    <asp:TextBox  ID="txtSHMainStationEquipment" CssClass="text" runat="server" Text='<%# Eval("MainStationEquipment")%>'></asp:TextBox>
                                </td>
                                </tr><tr>
                                <th>
                                        备站名称
                                    </th>
                                <td>
                                    <asp:TextBox  ID="txtSHBakStation" CssClass="text" runat="server" Text='<%# Eval("BakStationName")%>'></asp:TextBox>
                                </td>
                                <th>
                                        备站设备
                                </th>
                                <td>
                                    <asp:TextBox  ID="txtSHBakStationEquipment" CssClass="text" runat="server" Text='<%# Eval("BakStationEquipment")%>'></asp:TextBox>
                                </td>
                                </tr><tr>
                                <th>
                                        实时开始处理时间
                                    </th>
                                <td>
                                    <asp:TextBox ID="txtSHStartTime" runat="server"  CssClass="text"
                                    Text='<%# Eval("StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <th>
                                        实时结束处理时间
                                    </th>
                                <td>
                                    <asp:TextBox  ID="txtSHEndTime" runat="server"  CssClass="text"
                                    Text='<%# Eval("EndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                </tr><tr>
                                <th>
                                        事后数据处理
                                    </th>
                                <td>
                                    <asp:TextBox  ID="txtSHAfterDH" CssClass="text" runat="server" Text='<%# Eval("AfterWardsDataHandle")%>'></asp:TextBox>
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btn3" CausesValidation="False" CssClass="button" runat="server" CommandName="Add"
                                        Text="添加" />
                                    <asp:Button ID="btn4" CausesValidation="False" CssClass="button" runat="server" CommandName="Del"
                                        Text="删除" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th>
                    数据管理
                </th>
                <td>
                    <asp:Repeater ID="rpDataManage" runat="server" OnItemCommand="rpDataManage_ItemCommand">
                        <HeaderTemplate>
                            <table class="edit1">
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                                <tr>
                                    <th style="width: 120px;">
                                        对应试验ID
                                    </th>
                                    <td style="width: 270px;">
                                        <asp:TextBox ID="txtMSYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                    </td>
                                    <th style="width: 120px;">
                                        工作
                                    </th>
                                    <td style="width: 270px;">
                                        <asp:TextBox ID="txtMWork" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    </tr><tr>
                                    <th>
                                        开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtMStartTime" CssClass="text" runat="server" Text='<%# Eval("StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    </td>
                                    <th>
                                        结束时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtMEndTime" CssClass="text" runat="server" Text='<%# Eval("EndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                    </td>
                                    </tr><tr>
                                    <th>
                                        对应数据描述
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtMDes" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btn11" CausesValidation="False" CssClass="button" runat="server"
                                            CommandName="Add" Text="添加" />
                                        <asp:Button ID="btn12" CausesValidation="False" CssClass="button" runat="server"
                                            CommandName="Del" Text="删除" />
                                            <asp:CompareValidator ID="CompareValidator13" runat="server" 
                        ControlToCompare="txtMStartTime" ControlToValidate="txtMEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                    </td>
                                </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th>
                    指挥与监视
                </th>
                <td>
                    <asp:Repeater ID="rpDirectAndMonitor" runat="server" OnItemCommand="rpDirectAndMonitor_ItemCommand">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 150px;">
                                        对应试验ID
                                    </th>
                                    <th style="width: 150px;">
                                        时间段
                                    </th>
                                    <th style="width: 150px;">
                                        指挥与监视任务
                                    </th>
                                    <th style="width: 150px;">
                                        实时显示任务
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDMSYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDMDateSection" CssClass="text" runat="server" Text='<%# Eval("DateSection")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDMTask" CssClass="text" runat="server" Text='<%# Eval("Task")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDMRTTask" CssClass="text" runat="server" Text='<%# Eval("RealTimeShowTask")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn5" CausesValidation="False" CssClass="button" runat="server" CommandName="Add"
                                        Text="添加" />
                                    <asp:Button ID="btn6" CausesValidation="False" CssClass="button" runat="server" CommandName="Del"
                                        Text="删除" />
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th>
                    实时控制
                </th>
                <td>
                    <asp:Repeater ID="rpRealTimeControl" runat="server" OnItemCommand="rpRealTimeControl_ItemCommand">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 150px;">
                                        工作
                                    </th>
                                    <th style="width: 150px;">
                                        对应试验ID
                                    </th>
                                    <th style="width: 150px;">
                                        开始时间
                                    </th>
                                    <th style="width: 150px;">
                                        结束时间
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtRCWork" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRCSYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRCStartTime" CssClass="text" runat="server" Text='<%# Eval("StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRCEndTime" CssClass="text" runat="server" Text='<%# Eval("EndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn7" CausesValidation="False" CssClass="button" runat="server" CommandName="Add"
                                        Text="添加" />
                                    <asp:Button ID="btn8" CausesValidation="False" CssClass="button" runat="server" CommandName="Del"
                                        Text="删除" />
                                        <asp:CompareValidator ID="CompareValidator11" runat="server" 
                        ControlToCompare="txtRCStartTime" ControlToValidate="txtRCEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th>
                    试验评估
                </th>
                <td>
                    <asp:Repeater ID="rpSYEstimate" runat="server" OnItemCommand="SYEstimate_ItemCommand">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 150px;">
                                        对应试验ID
                                    </th>
                                    <th style="width: 150px;">
                                        开始时间
                                    </th>
                                    <th style="width: 150px;">
                                        完成时间
                                    </th>
                                    <th>
                                    </th>
                                </tr>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtESYID" CssClass="text" runat="server" Text='<%# Eval("SYID")%>'></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEStartTime" CssClass="text" runat="server" Text='<%# Eval("StartTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEEndTime" CssClass="text" runat="server" Text='<%# Eval("EndTime")%>' onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn9" CausesValidation="False" CssClass="button" runat="server" CommandName="Add"
                                        Text="添加" />
                                    <asp:Button ID="btn10" CausesValidation="False" CssClass="button" runat="server"
                                        CommandName="Del" Text="删除" />
                                        <asp:CompareValidator ID="CompareValidator12" runat="server" 
                        ControlToCompare="txtEStartTime" ControlToValidate="txtEEndTime" 
                        Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                        Operator="GreaterThan" Type="Date"></asp:CompareValidator>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            
        </table>
        </div>
        <br />
        <br />
     <div style="width: 750px; text-align: center;">
    <asp:Label ID="ltMessage" runat="server" CssClass="error" Text="实验内容与工作计划，所有字段都必须填写。"></asp:Label>
   </div>
        <div style="width: 750px; text-align: center">
            <asp:Button ID="btnSubmit" CssClass="button" OnClientClick="return CheckClientValidate();" runat="server" Text="保存计划" OnClick="btnSubmit_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSaveTo" runat="server" OnClientClick="return CheckClientValidate();" CssClass="button" Text="另存计划" OnClick="btnSaveTo_Click" />
        &nbsp;&nbsp;
            <asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" CausesValidation="False" />
                    &nbsp;&nbsp; 
            <asp:Button ID="btnReturn" class="button" runat="server" 
                    Text="返回" Width="65px" 
                    onclick="btnReturn_Click" CausesValidation="False" />
        </div>
        <div>
            <asp:HiddenField ID="HfID" runat="server" />
            <asp:HiddenField ID="HfFileIndex" runat="server" />
            <asp:HiddenField ID="hfTaskID" runat="server" />
            <asp:HiddenField ID="hfSatID" runat="server" />
            <asp:HiddenField ID="hfStatus" runat="server" />
            <asp:HiddenField ID="hfURL" runat="server" />
        </div>
    </div>
    <div id="dialog-form" style="display: none" title="提示信息">
        <p class="content">
        </p>
    </div>
</asp:Content>
