<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="XXXQEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.XXXQEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style3
        {
            color: #FF0000;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    空间信息需求
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width:800px;">
                <tr>
            <th class="style1">计划开始时间</th>
            <td>
                    <asp:TextBox ID="TextBox1" runat="server" CssClass="text" 
                            MaxLength="10"  ClientIDMode="Static"  Width="300px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th class="style1">计划结束时间</th>
            <td>
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="text" 
                            MaxLength="10"  ClientIDMode="Static"  Width="300px"></asp:TextBox>
            </td>
        </tr>
</table>
    <asp:Panel ID="pnlMBXQ" runat="server">
        <table class="edit" style="width: 800px;">
            <tr>
                <th class="style1">
                    用户名称
                </th>
                <td>
                    <asp:TextBox ID="txtMBUser" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    需求制定时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTime" runat="server" Width="300px" CssClass="text" MaxLength="12"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDDHHMM</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    目标信息标志
                </th>
                <td>
                    <asp:TextBox ID="txtMBTargetInfo" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    开始时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTimeSection1" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    结束时间
                </th>
                <td>
                    <asp:TextBox ID="txtMBTimeSection2" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    信息条数
                </th>
                <td>
                    <asp:TextBox ID="txtMBSum" runat="server" Width="300px" CssClass="text" MaxLength="50"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:Repeater ID="rpMB" runat="server" OnItemCommand="rpMB_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        卫星名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        提供时间
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMBInfoTime" runat="server" Text='<%# Eval("InfoTime")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btn9" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn10" CssClass="button" runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:HiddenField ID="HfMBID" runat="server" />
                    <asp:HiddenField ID="HfMBFileIndex" runat="server" />
                    <asp:HiddenField ID="hfMBTaskID" runat="server" />
                    <asp:HiddenField ID="hfMBSatID" runat="server" />
                    <asp:HiddenField ID="hfMBOverDate" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <asp:Panel ID="pnlHJXQ" runat="server">
        <table class="edit" style="width: 800px;">
            <tr>
                <th class="style1">
                    用户名称
                </th>
                <td>
                    <asp:TextBox ID="txtHJUser" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    需求制定时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTime" runat="server" Width="300px" CssClass="text" MaxLength="12"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDDHHMM</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    环境信息标志
                </th>
                <td>
                    <asp:TextBox ID="txtHJEnvironInfo" runat="server" Width="300px" CssClass="text" MaxLength="20"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    开始时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTimeSection1" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    结束时间
                </th>
                <td>
                    <asp:TextBox ID="txtHJTimeSection2" runat="server" ClientIDMode="Static" Width="300px"
                        CssClass="text" MaxLength="8"></asp:TextBox>
                    &nbsp;<span class="style3">格式：YYYYMMDD</span>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    信息条数
                </th>
                <td>
                    <asp:TextBox ID="txtHJSum" runat="server" Width="300px" CssClass="text" MaxLength="50"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:Repeater ID="rpHJ" runat="server" OnItemCommand="rpHJ_ItemCommand">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        卫星名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        产品名称
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        区域范围
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHJInfoArea" runat="server" Text='<%# Eval("InfoArea")%>'></asp:TextBox>
                                    </td>
                                    <td>
                                        提供时间
                                    </td>
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
                                        <asp:Button ID="btn11" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btn12" CssClass="button" runat="server" CommandName="Del" Text="删除" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <th class="style1">
                    &nbsp;
                </th>
                <td>
                    <asp:HiddenField ID="HfHJID" runat="server" />
                    <asp:HiddenField ID="HfHJFileIndex" runat="server" />
                    <asp:HiddenField ID="hfHJTaskID" runat="server" />
                    <asp:HiddenField ID="hfHJSatID" runat="server" />
                    <asp:HiddenField ID="hfHJOverDate" runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <table class="edit" style="width: 800px;">
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提交" OnClick="btnSubmit_Click" />
                &nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
