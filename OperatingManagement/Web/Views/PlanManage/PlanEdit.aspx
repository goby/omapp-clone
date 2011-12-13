<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlanEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.PlanEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .text
        {}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="pnlYJJH" runat="server">
            <table class="edit" style="width: 800px;">
                <tr>
                    <th style="width: 100px;">
                        信息类别</th>
                    <td>
                        <asp:Literal ID="ltPlanType" runat="server"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        开始时间</th>
                    <td>
                        <asp:TextBox ID="txtStartTimeYJJH" runat="server" CssClass="text" 
                            MaxLength="10" Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        结束时间</th>
                    <td>
                        <asp:TextBox ID="txtEndTimeYJJH" runat="server" CssClass="text" MaxLength="10" 
                            Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信源
                    </th>
                    <td>
                        <asp:TextBox ID="txtSourceYJJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信宿
                    </th>
                    <td>
                        <asp:TextBox ID="txtDesYJJH" runat="server"  Width="300px" CssClass="text"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        任务代码</th>
                    <td>
                        <asp:TextBox ID="txtTaskIDYJJH" runat="server" Width="300px"
                            CssClass="text" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据区行数
                    </th>
                    <td>
                        <asp:TextBox ID="txtLinecountYJJH" runat="server" CssClass="text" 
                            MaxLength="15" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式</th>
                    <td>
                        <asp:TextBox ID="txtFormatYJJH" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <th style="width: 100px;">
                        数据</th>
                    <td>
                        <asp:TextBox ID="txtDataYJJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="20" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        备注</th>
                    <td>
                        <asp:TextBox ID="txtNoteYJJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        <asp:Button ID="btnSubmitYJJH" runat="server" CssClass="button" Text="提交" 
                            OnClick="btnSubmit_Click" />
                        &nbsp;
                        <asp:Button ID="txtSaveToYJJH"  CssClass="button"  runat="server" Text="另存" 
                            onclick="txtSaveToYJJH_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlXXXQ" runat="server">
                    <table class="edit" style="width: 800px;">
                <tr>
                    <th style="width: 100px;">
                        信息类别</th>
                    <td>
                        <asp:Literal ID="ltinfoTypeXXXQ" runat="server"></asp:Literal>
                    </td>
                </tr>
                                <tr>
                    <th style="width: 100px;">
                        开始时间</th>
                    <td>
                        <asp:TextBox ID="txtStartTimeXXXQ" runat="server" CssClass="text" 
                            MaxLength="10" Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        结束时间</th>
                    <td>
                        <asp:TextBox ID="txtEndTimeXXXQ" runat="server" CssClass="text" MaxLength="10" 
                            Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信源
                    </th>
                    <td>
                        <asp:TextBox ID="txtSourceXXXQ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信宿
                    </th>
                    <td>
                        <asp:TextBox ID="txtDesXXXQ" runat="server" Width="300px" CssClass="text"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        任务代码</th>
                    <td>
                        <asp:TextBox ID="txtTaskIDXXXQ" runat="server" Width="300px"
                            CssClass="text" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据区行数
                    </th>
                    <td>
                        <asp:TextBox ID="txtLineCountXXXQ" runat="server" CssClass="text" 
                            MaxLength="15" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式1</th>
                    <td>
                        <asp:TextBox ID="txtFormat1XXXQ" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>
                                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式2</th>
                    <td>
                        <asp:TextBox ID="txtFormat2XXXQ" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据</th>
                    <td>
                        <asp:TextBox ID="txtDataXXXQ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="20" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        备注</th>
                    <td>
                        <asp:TextBox ID="txtNoteXXXQ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        <asp:Button ID="btnXXXQ" runat="server" CssClass="button" Text="提交" 
                            onclick="btnXXXQ_Click" />
                        &nbsp;
                        <asp:Button ID="txtSaveToXXXQ" runat="server" CssClass="button" 
                             Text="另存" onclick="txtSaveToXXXQ_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlGZJH" runat="server">
         <table class="edit" style="width: 800px;">
                <tr>
                    <th style="width: 100px;">
                        信息类别</th>
                    <td>
                        <asp:Literal ID="ltinfotypeGZJH" runat="server"></asp:Literal>
                    </td>
                </tr>
                                <tr>
                    <th style="width: 100px;">
                        开始时间</th>
                    <td>
                        <asp:TextBox ID="txtStartTimeGZJH" runat="server" CssClass="text" 
                            MaxLength="10" Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        结束时间</th>
                    <td>
                        <asp:TextBox ID="txtEndTimeGZJH" runat="server" CssClass="text" MaxLength="10" 
                            Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信源
                    </th>
                    <td>
                        <asp:TextBox ID="txtSourceGZJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信宿
                    </th>
                    <td>
                        <asp:TextBox ID="txtDesGZJH" runat="server" Width="300px" CssClass="text"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        任务代码</th>
                    <td>
                        <asp:TextBox ID="txtTaskidGZJH" runat="server" Width="300px"
                            CssClass="text" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据区行数
                    </th>
                    <td>
                        <asp:TextBox ID="txtLineCountGZJH" runat="server" CssClass="text" 
                            MaxLength="15" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式1</th>
                    <td>
                        <asp:TextBox ID="txtFormat1GZJH" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>
                                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式2</th>
                    <td>
                        <asp:TextBox ID="txtFormat2GZJH" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据</th>
                    <td>
                        <asp:TextBox ID="txtDataGZJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="20" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        备注</th>
                    <td>
                        <asp:TextBox ID="txtNoteGZJH" runat="server" Width="300px" CssClass="text" 
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        <asp:Button ID="btnGZJH" runat="server" CssClass="button" Text="提交" 
                            onclick="btnGZJH_Click"  />
                        &nbsp;
                        <asp:Button ID="txtSaveToGZJH" runat="server" CssClass="button" 
                             Text="另存" onclick="txtSaveToGZJH_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlTYSJ" runat="server">
        <table class="edit" style="width: 800px;">
                <tr>
                    <th style="width: 100px;">
                        信息类别</th>
                    <td>
                        <asp:Literal ID="ltinfotypeTYSJ" runat="server"></asp:Literal>
                    </td>
                </tr>
                                <tr>
                    <th style="width: 100px;">
                        开始时间</th>
                    <td>
                        <asp:TextBox ID="txtStartTimeTYSJ" runat="server" CssClass="text" 
                            MaxLength="10" Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        结束时间</th>
                    <td>
                        <asp:TextBox ID="txtEndTimeTYSJ" runat="server" CssClass="text" MaxLength="10" 
                            Width="300px"  onclick="setday(this);"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信源
                    </th>
                    <td>
                        <asp:TextBox ID="txtSourceTYSJ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        信宿
                    </th>
                    <td>
                        <asp:TextBox ID="txtDesTYSJ" runat="server"  Width="300px" CssClass="text"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        任务代码</th>
                    <td>
                        <asp:TextBox ID="txtTaskidTYSJ" runat="server" Width="300px"
                            CssClass="text" MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        数据区行数
                    </th>
                    <td>
                        <asp:TextBox ID="txtLineCountTYSJ" runat="server" CssClass="text" 
                            MaxLength="15" Width="300px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width: 100px;">
                        &nbsp;数据格式</th>
                    <td>
                        <asp:TextBox ID="txtFormat1TYSJ" runat="server" CssClass="text" MaxLength="15" 
                            Width="300px"></asp:TextBox>
                    </td>
                </tr>

                <tr>
                    <th style="width: 100px;">
                        数据</th>
                    <td>
                        <asp:TextBox ID="txtDataTYSJ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="20" Height="70px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        备注</th>
                    <td>
                        <asp:TextBox ID="txtNoteTYSJ" runat="server" Width="300px" CssClass="text" 
                            MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <th>
                        &nbsp;
                    </th>
                    <td>
                        <asp:Button ID="txtTYSJ" runat="server" CssClass="button" Text="提交" 
                            onclick="txtTYSJ_Click"  />
                        &nbsp;
                        <asp:Button ID="txtSaveToTYSJ" runat="server" CssClass="button" 
                            Text="另存" onclick="txtSaveToTYSJ_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnl4" runat="server">
            <asp:TextBox ID="txtContent" runat="server" Height="469px" TextMode="MultiLine" Width="659px"></asp:TextBox>
            </asp:Panel>
    </div>
    <div style="display:none;">
        <asp:HiddenField ID="hfinfotype" runat="server" />
        <asp:HiddenField ID="HfID" runat="server" />
    </div>
    </form>
</body>
</html>
