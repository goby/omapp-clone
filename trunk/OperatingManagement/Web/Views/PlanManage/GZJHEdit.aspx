﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GZJHEdit.aspx.cs"
    Inherits="OperatingManagement.Web.Views.PlanManage.GZJHEdit" %>

<%@ Register Src="../../ucs/ucTask.ascx" TagName="ucTask" TagPrefix="uc1" %>
<%@ Register Src="../../ucs/ucSatellite.ascx" TagName="ucSatellite" TagPrefix="uc2" %>
<%@ Register Src="../../ucs/ucTimer.ascx" TagName="ucTimer" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .text
        {
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
    计划管理 &gt; ZC地面站工作计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table cellpadding="0" class="edit1" style="width: 900px;">
        <tr>
            <th style="width: 150px;">
                任务代号(<span class="red">*</span>)
            </th>
            <td style="width: 300px;">
                <uc1:ucTask ID="ucTask1" runat="server" AllowBlankItem="False" />
            </td>
            <th style="width: 150px;">
                卫星(<span class="red">*</span>)
            </th>
            <td style="width: 300px;">
                <uc2:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                计划开始时间
            </th>
            <td>
                <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" MaxLength="10"
                    ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPlanStartTime"
                    ErrorMessage="开始时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <th style="width: 150px;">
                计划结束时间
            </th>
            <td style="width: 300px;">
                <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" MaxLength="10" ClientIDMode="Static"
                    onfocus="WdatePicker({dateFmt:'yyyy-MM-dd HH:mm:ss'})"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPlanEndTime"
                    ErrorMessage="结束时间不能为空" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPlanStartTime"
                    ControlToValidate="txtPlanEndTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间"
                    ForeColor="Red" Operator="GreaterThan"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                备注
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="100" Width="350px"
                    Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table id="detailtable" cellpadding="0" class="edit1" style="width: 900px;">
        <tr>
            <th style="width: 150px;">
                计划序号
            </th>
            <td style="width: 300px;">
                <asp:TextBox ID="txtJXH" CssClass="text" runat="server" Text='<%# Eval("JXH")%>' Enabled="False"></asp:TextBox>
                &nbsp;<span style="color:#3399FF;">自动生成</span>
            </td>
            <th style="width: 150px;">
                信息分类
            </th>
            <td style="width: 300px;">
                <asp:DropDownList ID="ddlXXFL" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                工作单位
            </th>
            <td>
                <asp:TextBox ID="txtDW" MaxLength="2" CssClass="text" runat="server" Text='<%# Eval("SL")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" Style="ime-mode: Disabled"></asp:TextBox>
                &nbsp;
            </td>
            <th style="width: 150px;">
                设备代号
            </th>
            <td>
                <asp:TextBox ID="txtSB" MaxLength="2" CssClass="text" runat="server" Text='<%# Eval("SL")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" Style="ime-mode: Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                总圈数
            </th>
            <td>
                <asp:TextBox ID="txtQS" MaxLength="4" CssClass="text" runat="server" Text='<%# Eval("SL")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" Style="ime-mode: Disabled"></asp:TextBox>
                &nbsp;
            </td>
            <th style="width: 150px;">
                本行计划飞行圈次
            </th>
            <td>
                <asp:TextBox ID="txtQH" MaxLength="4" CssClass="text" runat="server" Text='<%# Eval("SL")%>'
                    onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46" onpaste="return !clipboardData.getData('text').match(/\D/)"
                    ondragenter="return false" Style="ime-mode: Disabled"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                任务代号
            </th>
            <td>
                <asp:DropDownList ID="ddlDH" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
            <th style="width: 150px;">
                工作方式
            </th>
            <td>
                <asp:DropDownList ID="ddlFS" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                计划性质
            </th>
            <td>
                <asp:DropDownList ID="ddlJXZ" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
            <th style="width: 150px;">
                设备工作模式
            </th>
            <td>
                <asp:DropDownList ID="ddlMS" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                本帧计划的圈标识
            </th>
            <td>
                <asp:DropDownList ID="ddlQB" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
            <th style="width: 150px;">
                工作性质
            </th>
            <td>
                <asp:DropDownList ID="ddlGXZ" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                任务准备开始时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtPreStartTime" CssClass="text" runat="server" Text='<%# Eval("ZHB")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                &nbsp;
            </th>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                跟踪开始时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtTrackStartTime" CssClass="text" runat="server"
                    Text='<%# Eval("GZK")%>' onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                跟踪结束时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtTrackEndTime" CssClass="text" runat="server" Text='<%# Eval("GZJ")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator11" runat="server" ControlToCompare="txtTrackStartTime"
                    ControlToValidate="txtTrackEndTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间"
                    ForeColor="Red" Operator="GreaterThan" Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                开上行载波时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtWaveOnStartTime" CssClass="text" runat="server"
                    Text='<%# Eval("KSHX")%>' onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                关上行载波时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtWaveOffStartTime" CssClass="text" runat="server"
                    Text='<%# Eval("GSHX")%>' onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator12" runat="server" ControlToCompare="txtWaveOnStartTime"
                    ControlToValidate="txtWaveOffStartTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间"
                    ForeColor="Red" Operator="GreaterThan" Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                任务开始时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtStartTime" CssClass="text" runat="server" Text='<%# Eval("RK")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                任务结束时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtEndTime" CssClass="text" runat="server" Text='<%# Eval("JS")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator13" runat="server" ControlToCompare="txtStartTime"
                    ControlToValidate="txtEndTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red"
                    Operator="GreaterThan" Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                信息类别标志
            </th>
            <td>
                <asp:DropDownList ID="ddlBID" runat="server" DataTextField="Text" DataValueField="Value"
                    Width="154px">
                </asp:DropDownList>
            </td>
            <th style="width: 150px;">
                实时传送数据标志
            </th>
            <td>
                <asp:TextBox ID="txtJSBZ" CssClass="text" runat="server" Text='<%# Eval("SBZ")%>'></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                数据传输开始时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtTransStartTime" CssClass="text" runat="server"
                    Text='<%# Eval("RTs")%>' onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                数据传输结束时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="txtTransEndTime" CssClass="text" runat="server" Text='<%# Eval("RTe")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator14" runat="server" ControlToCompare="txtTransStartTime"
                    ControlToValidate="txtTransEndTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间"
                    ForeColor="Red" Operator="GreaterThan" Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                数据传输速率
            </th>
            <td>
                <asp:TextBox ID="txtTransSpeedRate" MaxLength="4" CssClass="text" runat="server"
                    Text='<%# Eval("SL")%>' onkeypress="return event.keyCode>=48&&event.keyCode<=57||event.keyCode==46"
                    onpaste="return !clipboardData.getData('text').match(/\D/)" ondragenter="return false"
                    Style="ime-mode: Disabled"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                事后回放传送数据标志
            </th>
            <td>
                <asp:TextBox ID="txtHBZ" CssClass="text" runat="server" Text='<%# Eval("HBZ")%>'></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                数据起始时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="DataStartTime" CssClass="text" runat="server" Text='<%# Eval("Ts")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
            </td>
            <th style="width: 150px;">
                数据结束时间
            </th>
            <td>
                <asp:TextBox MaxLength="14" ID="DataEndTime" CssClass="text" runat="server" Text='<%# Eval("Te")%>'
                    onfocus="WdatePicker({dateFmt:'yyyyMMddHHmmss'})"></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator15" runat="server" ControlToCompare="DataStartTime"
                    ControlToValidate="DataEndTime" Display="Dynamic" ErrorMessage="结束时间应大于开始时间"
                    ForeColor="Red" Operator="GreaterThan" Type="Double"></asp:CompareValidator>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th style="width: 150px;">
            </th>
            <td colspan="3">
                <asp:Label ID="ltMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th style="width: 150px;">
                &nbsp;
            </th>
            <td colspan="3">
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" OnClick="btnSubmit_Click"
                     OnClientClick="return CheckClientValidate();" Text="保存计划" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSaveTo" runat="server" CssClass="button" OnClick="btnSaveTo_Click"
                    OnClientClick="return CheckClientValidate();"  Text="另存计划" />
                &nbsp;&nbsp;
                <asp:Button ID="btnReset" runat="server" CausesValidation="False" class="button"
                    OnClick="btnReset_Click" Text="重置" Width="65px" />
                &nbsp;&nbsp;
                <asp:Button ID="btnReturn" runat="server" CausesValidation="False" class="button"
                    OnClick="btnReturn_Click" Text="返回" Width="65px" />
                <asp:HiddenField ID="HfID" runat="server" />
                <asp:HiddenField ID="HfFileIndex" runat="server" />
                <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfStatus" runat="server" />
                <asp:HiddenField ID="hfURL" runat="server" />
            </td>
        </tr>
    </table>
    <div id="dialog-form" style="display: none" title="提示信息">
        <p class="content">
        </p>
    </div>
</asp:Content>
