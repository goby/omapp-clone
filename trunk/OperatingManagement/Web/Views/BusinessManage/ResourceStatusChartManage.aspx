﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResourceStatusChartManage.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.ResourceStatusChartManage" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .norText
        {
            width: 155px;
            margin: 0px;
            padding: 0px;
        }
        .norDpl
        {
            width: 160px;
            margin: 0px;
            padding: 0px;
        }
        .index_content_search
        {
            margin-top: 10px;
        }
        
        .index_content_search table
        {
            border: 1px solid #eeeeee;
            border-collapse: collapse;
            width: 100%;
        }
        
        .index_content_search table td
        {
            border: 1px solid #eeeeee;
            line-height: 26px;
            color: #333333;
            text-align: left;
            height: 26px;
        }
        .index_content_search table th
        {
            border: 1px solid #eeeeee;
            font-weight: bold;
            line-height: 26px;
            color: #333333;
            text-align: right;
            height: 26px;
        }
        .index_content_view
        {
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
<om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理&gt;资源状态分布图
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
                <th width="15%">
                    资源类型：
                </th>
                <td width="25%">
                    <asp:DropDownList ID="dplResourceType" runat="server" CssClass="norDpl">
                    </asp:DropDownList>
                </td>
                <th width="15%">
                    资源编号：
                </th>
                <td width="25%">
                    <asp:TextBox ID="txtResourceCode" runat="server" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtResourceCode" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
                </td>
                <td width="20%">
                </td>
            </tr>
            <tr>
               <th>
                  起始时间：
               </th>
               <td>
                  <asp:TextBox ID="txtBeginTime" runat="server" CssClass="norText" onclick="setday(this);"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtBeginTime" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
               </td>
               <th>
                  结束时间：
               </th>
               <td>
                 <asp:TextBox ID="txtEndTime" runat="server" CssClass="norText" onclick="setday(this);"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtEndTime" ErrorMessage="（必填）" ValidationGroup="SearchStatus"></asp:RequiredFieldValidator>
               </td>
               <td>
                    <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" CssClass="button" ValidationGroup="SearchStatus" Text="查 询"/>
                    <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="button" Text="添 加"/>
               </td>
            </tr>
        </table>
    </div>
     <div id="divResourceStatus" class="index_content_view">
        <asp:Chart ID="chartResourceStatus" runat="server" Width="900px" OnPreRender="chartResourceStatus_PreRender">
        <Legends>
            <asp:Legend Name="图例"></asp:Legend>
        </Legends>
        <Series>
            <asp:Series Name="seriesHealthStatus" ChartType="RangeBar" YValueType="Date" Legend="图例" LegendText="健康状态">
            </asp:Series>
            <asp:Series Name="seriesUseStatus" ChartType="RangeBar" YValueType="Date" Legend="图例" LegendText="占用状态">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
              <AxisY IntervalType="Days"  Interval="1"></AxisY>
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
    </div>
   
</asp:Content>
