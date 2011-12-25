<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DMJHEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.DMJHEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
                    编号
                </th>
                <td>
                    <asp:TextBox ID="txtSequence" runat="server"></asp:TextBox>
                </td>
                <th>
                    时间
                </th>
                <td>
                    <asp:TextBox ID="txtDatetime" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    工作单位
                </th>
                <td>
                    <asp:TextBox ID="txtStationName" runat="server"></asp:TextBox>
                </td>
                <th>
                    设备代号
                </th>
                <td>
                    <asp:TextBox ID="txtEquipmentID" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    任务个数
                </th>
                <td>
                    <asp:TextBox ID="txtTaskCount" runat="server"></asp:TextBox>
                </td>
                <th>
                     <asp:HiddenField ID="HfID" runat="server" />
                </th>
                <td>
                    <asp:HiddenField ID="HfFileIndex" runat="server" />
                </td>
            </tr>
            
        </table>
        <table class="edit" style="width: 800px;">
            <tr>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound"
                        OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <table class="edit" style="width: 800px;">
                                <tr>
                                    <th>
                                        任务标志
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTaskFlag" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        工作方式
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWorkWay" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        计划性质
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtPlanPropertiy" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        工作模式
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWorkMode" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        任务准备开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtPreStartTime" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        任务开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtStartTime" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        跟踪开始时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTrackStartTime" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        开上行载波时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWaveOnStartTime" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        关上行载波时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtWaveOffStartTime" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        跟踪结束时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtTrackEndTime" CssClass="text" runat="server" Text='<%# Eval("Description")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th>
                                        任务结束时间
                                    </th>
                                    <td>
                                        <asp:TextBox ID="txtEndTime" CssClass="text" runat="server" Text='<%# Eval("Work")%>'></asp:TextBox>
                                    </td>
                                    <th>
                                        
                                    </th>
                                    <td>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4">
                                        实时传输
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Repeater ID="rpReakTimeTransfor" runat="server" OnItemCommand="Repeater2_ItemCommand">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <th>
                                                            格式标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtFormatFlag" CssClass="text" runat="server" Text='<%# Eval("FormatFlag")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            信息流标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtInfoFlowFlag" CssClass="text" runat="server" Text='<%# Eval("txtInfoFlowFlag")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输开始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransStartTime" CssClass="text" runat="server" Text='<%# Eval("TransStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据传输结束时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransEndTime" CssClass="text" runat="server" Text='<%# Eval("TransEndTime")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输速率
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="txtTransSpeedRate" CssClass="text" runat="server" Text='<%# Eval("TransSpeedRate")%>'></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button2" CssClass="button" CommandName="Add" runat="server" Text="删除" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="Button3" CssClass="button" CommandName="Del" runat="server" Text="删除" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <th colspan="4">
                                        事后回放
                                    </th>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Repeater ID="rpAfterFeedBack" runat="server" OnItemCommand="Repeater3_ItemCommand">
                                            <ItemTemplate>
                                                <table>
                                                    <tr>
                                                        <th>
                                                            格式标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="FormatFlag" CssClass="text" runat="server" Text='<%# Eval("FormatFlag")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            信息流标志
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="InfoFlowFlag" CssClass="text" runat="server" Text='<%# Eval("InfoFlowFlag")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据起始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="DataStartTime" CssClass="text" runat="server" Text='<%# Eval("DataStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据结束时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="DataEndTime" CssClass="text" runat="server" Text='<%# Eval("DataEndTime")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            数据传输开始时间
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="TransStartTime" CssClass="text" runat="server" Text='<%# Eval("TransStartTime")%>'></asp:TextBox>
                                                        </td>
                                                        <th>
                                                            数据传输速率
                                                        </th>
                                                        <td>
                                                            <asp:TextBox ID="TransSpeedRate" CssClass="text" runat="server" Text='<%# Eval("TransSpeedRate")%>'></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <th>
                                                            
                                                        </th>
                                                        <td>
                                                            <asp:Button ID="Button5" CssClass="button" CommandName="Add" runat="server" Text="删除" />
                                                        </td>
                                                        <th>
                                                           
                                                        </th>
                                                        <td>
                                                            <asp:Button ID="Button6" CssClass="button" CommandName="Del" runat="server" Text="删除" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="Button1" CssClass="button" CommandName="Add" runat="server" Text="添加任务" />
                                    </td>
                                    <td>
                                    <asp:Button ID="Button4" CssClass="button" CommandName="Del" runat="server" Text="删除任务" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
