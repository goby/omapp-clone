<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YJJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.YJJHEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 147px;
        }
        .text
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="edit" style="width:800px;">
        <tr>
            <th class="style1">计划开始时间</th>
            <td>
                    <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划结束时间</th>
            <td>
                    <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" 
                            MaxLength="10"   onclick="setday(this);" Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">信息分类</th>
            <td>
                <asp:TextBox ID="txtXXFL" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划序号</th>
            <td>
                <asp:TextBox ID="txtJXH" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">系统名称</th>
            <td>
                <asp:TextBox ID="txtSysName" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">试验开始时间</th>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">试验结束时间</th>
            <td>
                <asp:TextBox ID="txtEndTime" runat="server" Width="300px" CssClass="text" 
                    MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">系统任务</th>
            <td>
                <asp:TextBox ID="txtTask" runat="server" Width="300px"  CssClass="text" 
                    MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">&nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" 
                    onclick="btnSubmit_Click" />
                     <asp:HiddenField ID="HfID" runat="server" />
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
