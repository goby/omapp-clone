﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrbitDifferenceAnalysis.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.OrbitDifferenceAnalysis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
 <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
 <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理 &gt; 轨道分析 - 差值分析
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<table class="edit" style="width:800px;">
    <tr>
        <th style="width:160px;">星历数据文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuXLDataFile" ClientIDMode="Static" runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="fuXLDataFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <th style="width:160px;">差值计算时间文件(<span class="red">*</span>)</th>
        <td>
            <asp:FileUpload ID="fuDifCalTimeFile" ClientIDMode="Static"  runat="server" />
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="fuDifCalTimeFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr id="trMessage" runat="server" visible="false">
        <th>&nbsp;</th>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="error" Text="“相关文件”内容必须严格按照格式要求编写。"></asp:Label>
        </td>
    </tr>
    <div id="divCalResult" runat="server" visible="false">
    <tr>
        <th>计算结果文件路径</th>
        <td>
            <asp:Label ID="lblResultFilePath" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
       <th>计算结果</th>
       <td>
          <asp:Label ID="lblCalResult" runat="server" Text="计算成功" ForeColor="Red"></asp:Label>
          <asp:LinkButton ID="lbtnViewResult" runat="server" OnClientClick="javascript:_resultDialog.dialog('open');return false;">查看</asp:LinkButton>
          <asp:LinkButton ID="lbtnResultFileDownload" runat="server" OnClick="lbtnResultFileDownload_Click" CausesValidation="false">下载</asp:LinkButton>
       </td>    
    </tr>
    </div>
    <tr>
        <th>&nbsp;</th>
        <td>
            <asp:Button ID="btnCalculate" runat="server" Text="开始计算" CssClass="button" 
                onclick="btnCalculate_Click" />
        </td>
    </tr>
</table>
<div id="dialog-form" style="display:none" title="提示信息">
	<p class="content">
        <b>星历数据文件路径：</b><asp:Literal ID="ltXLDataFilePath" runat="server"></asp:Literal><br />
        <b>差值计算时间文件路径：</b><asp:Literal ID="ltDifCalTimeFilePath" runat="server"></asp:Literal><br />
        <b>星历数据文件内容：</b><asp:Literal ID="ltXLDataFile" runat="server"></asp:Literal><br />
        <b>差值计算时间文件内容：</b><asp:Literal ID="ltDifCalTimeFile" runat="server"></asp:Literal><br />
    </p>
</div>
<div id="result-form" style="display:none" title="结果文件内容">
	<p class="content">
        <b>结果文件内容：</b><asp:Literal ID="ltResultFile" runat="server"></asp:Literal><br />
    </p>
</div>
</asp:Content>
