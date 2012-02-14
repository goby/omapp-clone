<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HJXQEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.HJXQEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

    .style3
    {
        color: #FF0000;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    空间环境信息需求
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
<table class="edit" style="width:800px;">
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
            <th class="style1">用户名称</th>
            <td>
                <asp:TextBox ID="txtHJUser" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">需求制定时间</th>
            <td>
                <asp:TextBox ID="txtHJTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="12"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDDHHMM</span>
            </td>
        </tr>
        <tr>
            <th class="style1">环境信息标志</th>
            <td>
                <asp:TextBox ID="txtHJEnvironInfo" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">开始时间</th>
            <td>
                <asp:TextBox ID="txtHJTimeSection1" runat="server" ClientIDMode="Static" Width="300px" CssClass="text" 
                    MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
            </td>
        </tr>
        <tr>
            <th class="style1">结束时间</th>
            <td>
                <asp:TextBox ID="txtHJTimeSection2" runat="server" ClientIDMode="Static" Width="300px" CssClass="text" 
                    MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
            </td>
        </tr>
        <tr>
            <th class="style1">信息条数</th>
            <td>
                <asp:TextBox ID="txtHJSum" runat="server" Width="300px"  CssClass="text" 
                    MaxLength="50" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                    <asp:Repeater ID="rpHJ" runat="server" 
                        onitemcommand="rpHJ_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                       卫星名称</td>
                                    <td>
                                        <asp:TextBox ID="txtHJSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称</td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                       区域范围</td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoArea" runat="server" Text='<%# Eval("InfoArea")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        提供时间</td>
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
                                         <asp:Button ID="btn11" CssClass="button"  runat="server" CommandName="Add" 
                                             Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn12" CssClass="button"  runat="server" CommandName="Del" 
                                            Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" 
                    onclick="btnSubmit_Click" />
                     <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                    <asp:HiddenField ID="hfTaskID" runat="server" />
                <asp:HiddenField ID="hfSatID" runat="server" />
                <asp:HiddenField ID="hfOverDate" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
