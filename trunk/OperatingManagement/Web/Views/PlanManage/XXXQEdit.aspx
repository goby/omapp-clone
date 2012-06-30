<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="XXXQEdit.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.XXXQEdit" %>

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
    计划管理 &gt; 空间信息需求
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit1" style="width: 1000px;">
       <tr>
            <td colspan="4">
                <strong>计划基本信息</strong>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                任务代号(<span class="red">*</span>)
            </th>
            <td  style="width:312px;">
                <uc1:ucTask ID="ucTask1" runat="server" AllowBlankItem="False" />
            </td>
            <th style="width:100px;">
                卫星(<span class="red">*</span>)
            </th>
            <td>
                <uc2:ucSatellite ID="ucSatellite1" runat="server" AllowBlankItem="False" />
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                计划开始时间
            </th>
            <td>
                <asp:TextBox ID="txtPlanStartTime" runat="server" CssClass="text" MaxLength="10"
                    ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                    &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtPlanStartTime" ErrorMessage="开始时间不能为空" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
            <th style="width:100px;">
                计划结束时间
            </th>
            <td>
                <asp:TextBox ID="txtPlanEndTime" runat="server" CssClass="text" MaxLength="10" 
                    ClientIDMode="Static" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtPlanEndTime" ErrorMessage="结束时间不能为空" 
                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" 
                    ControlToCompare="txtPlanStartTime" ControlToValidate="txtPlanEndTime" 
                    Display="Dynamic" ErrorMessage="结束时间应大于开始时间" ForeColor="Red" 
                    Operator="GreaterThan" Type="Date"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                备注
            </th>
            <td colspan="3">
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="100" Width="585px"
                    Height="40px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="edit1" style="width: 1000px;">
        <tr>
            <td colspan="4">
                <strong>空间目标信息需求</strong>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                用户名称
            </th>
            <td  style="width:312px;">
                <asp:TextBox ID="txtMBUser" runat="server" CssClass="text" MaxLength="32">运控评估中心</asp:TextBox>
            </td>
            <th style="width:100px;">
                需求制定时间
            </th>
            <td>
                <asp:TextBox ID="txtMBTime" runat="server" CssClass="text" MaxLength="12" onfocus="WdatePicker({dateFmt:'yyyyMMddHHmm'})"></asp:TextBox>
                &nbsp;<span style="color:#3399FF;">格式：YYYYMMDDHHMM</span>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                开始时间
            </th>
            <td>
                <asp:TextBox ID="txtMBTimeSection1" runat="server" ClientIDMode="Static" CssClass="text"
                    MaxLength="8" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
            </td>
            <th style="width:100px;">
                结束时间
            </th>
            <td>
                <asp:TextBox ID="txtMBTimeSection2" runat="server" ClientIDMode="Static" CssClass="text"
                    MaxLength="8" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                目标信息标志
            </th>
            <td>
                <asp:TextBox ID="txtMBTargetInfo" runat="server" CssClass="text" MaxLength="10"></asp:TextBox>
            </td>
            <th style="width:100px;">
                <%--信息条数--%>
            </th>
            <td>
            <div style="display:none">
                <asp:TextBox ID="txtMBSum" runat="server" CssClass="text" MaxLength="50" Enabled="false"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                &nbsp;
            </th>
            <td colspan="3">
                <asp:Repeater ID="rpMB" runat="server" OnItemCommand="rpMB_ItemCommand" 
                    onitemdatabound="rpMB_ItemDataBound">
                <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 80px;">
                                        卫星名称 
                                    </th>
                                    <th style="width: 150px;">
                                        产品名称
                                    </th>
                                    <th style="width: 150px;">
                                        提供时间(HHMM)
                                    </th>
                                    <th>
                                     </th>
                                    </tr>
                           <tbody id="tbPlans">
                            </HeaderTemplate>
                    <ItemTemplate>
                            <tr>
                                <td>
                                    <%--<asp:TextBox ID="txtMBSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>--%>
                                    <uc2:ucSatellite ID="ucSatelliteMB" runat="server" AllowBlankItem="False" />
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtMBInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlMBInfoName" runat="server" DataTextField="Text" DataValueField="Value"
                                            Width="154px">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMBInfoTime" MaxLength="4" runat="server" Text='<%# Eval("InfoTime")%>'  onfocus="WdatePicker({dateFmt:'HHmm'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn9" CausesValidation="False" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    <asp:Button ID="btn10" CausesValidation="False" CssClass="button" runat="server" CommandName="Del" Text="删除" />
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
    <table class="edit1" style="width: 1000px;">
        <tr>
            <td colspan="4">
                <strong>空间环境信息需求</strong>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                用户名称
            </th>
            <td  style="width:170px;">
                <asp:TextBox ID="txtHJUser" runat="server" CssClass="text" MaxLength="32">运控评估中心</asp:TextBox>
            </td>
            <th style="width:100px;">
                需求制定时间
            </th>
            <td>
                <asp:TextBox ID="txtHJTime" runat="server" CssClass="text" MaxLength="12" onfocus="WdatePicker({dateFmt:'yyyyMMddHHmm'})"></asp:TextBox>
                &nbsp;<span style="color:#3399FF;">格式：YYYYMMDDHHMM</span>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                开始时间
            </th>
            <td>
                <asp:TextBox ID="txtHJTimeSection1" runat="server" ClientIDMode="Static" CssClass="text"
                    MaxLength="8" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
            </td>
            <th style="width:100px;">
                结束时间
            </th>
            <td>
                <asp:TextBox ID="txtHJTimeSection2" runat="server" ClientIDMode="Static" CssClass="text"
                    MaxLength="8" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                环境信息标志
            </th>
            <td>
                <asp:TextBox ID="txtHJEnvironInfo" runat="server" CssClass="text" MaxLength="11"></asp:TextBox>
            </td>
            <th style="width:100px;">
                <%--信息条数--%>
            </th>
            <td>
            <div style="display:none">
                <asp:TextBox ID="txtHJSum" runat="server" CssClass="text" MaxLength="50" Enabled="false"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                &nbsp;
            </th>
            <td colspan="3">
                <asp:Repeater ID="rpHJ" runat="server" OnItemCommand="rpHJ_ItemCommand" 
                    onitemdatabound="rpHJ_ItemDataBound">
                    <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width: 80px;">
                                        卫星名称 
                                    </th>
                                    <th style="width: 170px;">
                                        产品名称
                                    </th>
                                    <th style="width: 360px;">
                                        区域范围
                                    </th>
                                    <th style="width: 100px;">
                                        提供时间(HHMM)
                                    </th>
                                    <th>
                                     </th>
                                    </tr>
                           <tbody id="tbPlans1">
                            </HeaderTemplate>
                    <ItemTemplate>
                            <tr>
                                <td>
                                    <%--<asp:TextBox ID="txtHJSatName" runat="server" Text='<%# Eval("SatName")%>'></asp:TextBox>--%>
                                    <uc2:ucSatellite ID="ucSatelliteHJ" runat="server" AllowBlankItem="False" />
                                </td>
                                <td>
                                    <%--<asp:TextBox ID="txtHJInfoName" runat="server" Text='<%# Eval("InfoName")%>'></asp:TextBox>--%>
                                    <asp:DropDownList ID="ddlHJInfoName" runat="server" DataTextField="Text" DataValueField="Value"
                                            Width="174px">
                                        </asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHJInfoArea" MaxLength="64" runat="server" Text='<%# Eval("InfoArea")%>' Width="350px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtHJInfoTime" MaxLength="4" runat="server" Text='<%# Eval("InfoTime")%>' Width="90px" onfocus="WdatePicker({dateFmt:'HHmm'})"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btn11" CausesValidation="False" CssClass="button" runat="server" CommandName="Add" Text="添加" />
                                    <asp:Button ID="btn12" CausesValidation="False" CssClass="button" runat="server" CommandName="Del" Text="删除" />
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
    <br />
    <div style="width: 950px; text-align: center">
        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="保存计划" OnClick="btnSubmit_Click" />
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnSaveTo" runat="server" CssClass="button" Text="另存计划" OnClick="btnSaveTo_Click" />
    &nbsp;&nbsp;
        <asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" CausesValidation="False" />
                    &nbsp;&nbsp; 
        <asp:Button ID="btnReturn" class="button" runat="server" 
                    Text="返回" Width="65px" 
                    onclick="btnReturn_Click" CausesValidation="False" />
    </div>
    <div style="display: none">
        <asp:HiddenField ID="HfID" runat="server" />
        <asp:HiddenField ID="HfFileIndex" runat="server" />
        <asp:HiddenField ID="hfTaskID" runat="server" />
        <asp:HiddenField ID="hfSatID" runat="server" />
        <asp:HiddenField ID="hfStatus" runat="server" />
    </div>
    <div id="dialog-form" style="display: none" title="提示信息">
        <p class="content">
        </p>
    </div>
</asp:Content>
