﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="TYSJEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.TYSJEdit" %>
<%@ Register src="../../ucs/ucTask.ascx" tagname="ucTask" tagprefix="uc1" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc2" %>
<%@ Register src="../../ucs/ucTimer.ascx" tagname="ucTimer" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .text
        {}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 仿真推演试验数据
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">

<div>
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
            <th>计划序号</th>
            <td>
                <asp:TextBox ID="txtJXH" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20" Enabled="False" ReadOnly="True"></asp:TextBox>
                    &nbsp;<span style="color:#3399FF;">自动生成，不可编辑</span>
            </td>
        </tr>
        <tr>
            <th class="style1">卫星名称</th>
            <td>
                <asp:DropDownList ID="ddlSatName" runat="server" AutoPostBack="True" 
                    Height="20px" onselectedindexchanged="ddlSatName_SelectedIndexChanged" 
                    Width="150px">
                    <asp:ListItem>探索三号卫星</asp:ListItem>
                    <asp:ListItem>探索四号卫星</asp:ListItem>
                    <asp:ListItem>探索五号卫星</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th class="style1">试验类别</th>
            <td>
                <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" Height="20px" 
                    onselectedindexchanged="ddlType_SelectedIndexChanged" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th class="style1">试验项目</th>
            <td>
                <asp:DropDownList ID="ddlTestItem" runat="server" Height="20px" Width="150px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th class="style1">试验开始时间</th>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" Width="150px" CssClass="text" 
                    MaxLength="14" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
            &nbsp;<uc3:uctimer ID="ucStartTimer" runat="server" Seperator="empty" 
                    ShowSecond="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtStartTime"
                        ErrorMessage="开始时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
        </tr>
        <tr>
            <th class="style1">试验结束时间</th>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" Width="150px" CssClass="text" 
                    MaxLength="14" ClientIDMode="Static" ReadOnly="True"></asp:TextBox>
            &nbsp;<span style="color:#3399FF;"><uc3:uctimer ID="ucEndTimer" runat="server" 
                    Seperator="empty" ShowSecond="True" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEndTime"
                        ErrorMessage="结束时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
                </span></td>
        </tr>
        <tr>
            <th class="style1">试验条件</th>
            <td>
                <asp:TextBox ID="txtCondition" runat="server" Width="300px"  CssClass="text" 
                  Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>备注</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="100" 
                    Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" 
                    onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSaveTo_Click" />
                     &nbsp;&nbsp; <asp:Button ID="btnReset" class="button" runat="server" 
                    Text="重置" Width="65px" 
                    onclick="btnReset_Click" CausesValidation="False" />
                     <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                    <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfStatus" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>

