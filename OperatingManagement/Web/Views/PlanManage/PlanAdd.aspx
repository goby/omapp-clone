<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlanAdd.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.PlanAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 新建计划
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">

    <table class="edit1" style="width:800px;">
        <tr>
            <th style="width:100px;">
                任务代号(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtTaskID" runat="server" CssClass="text" MaxLength="25" 
                    Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" 
                    ControlToValidate="txtTaskID" Display="Dynamic" ErrorMessage="必须填写“任务代号”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                计划类型(<span class="red">*</span>)</th>
            <td>
                <asp:DropDownList ID="ddlPlanType" runat="server" Width="304px" Height="24px">
                    <asp:ListItem Value="YJJH">应用研究工作计划</asp:ListItem>
                    <asp:ListItem Value="XXXQ">空间信息需求</asp:ListItem>
                    <asp:ListItem Value="DMJH">地面站工作计划</asp:ListItem>
                    <asp:ListItem Value="ZXJH">中心运行计划</asp:ListItem>
                    <asp:ListItem Value="TYSJ">仿真推演试验数据</asp:ListItem>
                    <%--<asp:ListItem Value="SBJH">设备工作计划</asp:ListItem>--%>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                卫星(<span class="red">*</span>)</th>
            <td>
                <asp:DropDownList ID="ddlSat" runat="server" Width="304px" Height="24px">
                    <asp:ListItem Value="TS3">TS-3卫星</asp:ListItem>
                    <asp:ListItem Value="TS4A">TS-4-A卫星</asp:ListItem>
                    <asp:ListItem Value="TS4B">TS-4-B卫星</asp:ListItem>
                    <asp:ListItem Value="TS5A">TS-5-A卫星</asp:ListItem>
                    <asp:ListItem Value="TS5B">TS-5-B卫星</asp:ListItem>
                    <asp:ListItem Value="TS2">TS-2卫星</asp:ListItem>
                    <asp:ListItem Value="DXLH">多星联合试验</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
<%--        <tr>
            <th style="width:100px;">
                计划编号(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtPlanID" runat="server" CssClass="text" MaxLength="4" 
                    Width="300px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" 
                    ControlToValidate="txtPlanID" Display="Dynamic" ErrorMessage="必须填写“计划编号”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rev2" runat="server" Display="Dynamic" ForeColor="Red"
                     ControlToValidate="txtPlanID" ErrorMessage="只能输入数字"
                     ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
            </td>
        </tr>--%>
        <tr>
            <th style="width:100px;">
                开始时间(<span class="red">*</span>)</th>
            <td>
                <asp:TextBox ID="txtStartTime" runat="server" CssClass="text" 
                     Width="300px" ClientIDMode="Static"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfv4" runat="server" 
                    ControlToValidate="txtStartTime" Display="Dynamic" ErrorMessage="必须填写“开始时间”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th style="width:100px;">
                结束时间(<span class="red">*</span>)</th>
            <td>

                <asp:TextBox ID="txtEndTime" runat="server" CssClass="text" 
                     Width="300px"  ClientIDMode="Static"></asp:TextBox>

                <asp:RequiredFieldValidator ID="rfv5" runat="server" 
                    ControlToValidate="txtEndTime" Display="Dynamic" ErrorMessage="必须填写“结束时间”。" 
                    ForeColor="Red"></asp:RequiredFieldValidator>

            </td>
        </tr>
        <tr>
            <th>
                备注</th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" CssClass="text" MaxLength="50" 
                    Width="300px" Height="75px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:HiddenField ID="hfPlanType" runat="server" />
                <asp:HiddenField ID="hfID" runat="server" />
                <asp:HiddenField ID="hfSBJHID" runat="server" ClientIDMode="Static" />
                <asp:Literal ID="ltHref" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:Label ID="ltMessage" runat="server" CssClass="error" 
                    Text="“计划编号”必须唯一。"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" 
                    onclick="btnSubmit_Click" Text="生成计划" />
            &nbsp;&nbsp;
                <asp:Button ID="btnGetPlanInfo" runat="server" CssClass="button" onclick="txtGetPlanInfo_Click" 
                    Text="选择设备计划" CausesValidation="False" />
            &nbsp;
                <asp:Button ID="btnEdit" runat="server" CssClass="button" Text="编辑详细" 
                    onclick="btnEdit_Click" CausesValidation="False" />
&nbsp;
                <asp:Button ID="btnContinue" runat="server" CssClass="button" Text="继续新建" 
                    onclick="btnContinue_Click" CausesValidation="False" />
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                <asp:LinkButton ID="btnSBJH" runat="server" ClientIDMode="Static" 
                    onclick="btnSBJH_Click" CausesValidation="False"></asp:LinkButton>
                
            &nbsp;
                <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="设备计划仅对地面计划有效"></asp:Label>
                
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;</th>
            <td>
                    <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <%--<th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>--%>
                                    <th style="width: 150px;">
                                        计划编号
                                    </th>
                                    <th style="width: 150px;">
                                        任务代号
                                    </th>
                                    <th style="width: 150px;">
                                        计划类别
                                    </th>
                                    <th style="width: 150px;">
                                        开始时间
                                    </th>
                                    <th style="width: 150px;">
                                        结束时间
                                    </th>
                                    <th style="width: 70px;">
                                        选择
                                    </th>
                                </tr>
                                <tbody id="tbUsers">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <%--<td><input type="checkbox" <%# Eval("LoginName").ToString().Equals(this.Profile.UserName,StringComparison.InvariantCultureIgnoreCase)?"disabled=\"true\"":"" %> name="chkDelete" value="<%# Eval("Id") %>" /></td>--%>
                                <td>
                                    <%# Eval("planid")%>
                                </td>
                                <td>
                                    <%# Eval("taskid")%>
                                </td>
                                <td>
                                    <%# Eval("plantype")%>
                                </td>
                                <td>
                                    <%# Eval("starttime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                </td>
                                <td>
                                    <%# Eval("endtime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                </td>
                                <td>
                                    <button class="button" 
                                        onclick="return SelectSBJH('<%# Eval("ID") %>',escape('<%# GetFileName(Eval("FileIndex")) %>'))">
                                        选择</button>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">

                            </td>
                            <td class="listTitle-c2">
                                <om:CollectionPager ID="cpPager" runat="server" PageSize="5">
                                </om:CollectionPager>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
    </table>
</asp:Content>
