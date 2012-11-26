<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="PlanList.aspx.cs" Inherits="OperatingManagement.Web.Views.PlanManage.PlanList" %>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="planmanage" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuPlan" />
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="MapPathContent" runat="server">
    计划管理 &gt; 查看计划
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="BodyContent" runat="server">
        <asp:Panel ID="pnlData" runat="server">
        <div class="index_content_search">
        <table cellspacing="0" cellpadding="0" class="searchTable">
            <tr>
               <th>
                  计划起始时间：
               </th>
               <td>
                <asp:TextBox ID="txtStartDate" ClientIDMode="Static" Width="90px"  CssClass="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
               </td>
               <th>
                  计划结束时间：
               </th>
               <td>
                
                <asp:TextBox ID="txtEndDate" ClientIDMode="Static" Width="90px"  CssClass="text" runat="server" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd'})"></asp:TextBox>
                
               </td>
               <th>
                    计划类型：
                </th>
                <td>
                        <asp:DropDownList ID="ddlType" ClientIDMode="Static" runat="server" Width="150px" Height="20px">
                        <%--<asp:ListItem Value="0">==全部==</asp:ListItem>--%>
                        <asp:ListItem Value="YJJH">应用研究工作计划</asp:ListItem>
                        <asp:ListItem Value="XXXQ">空间信息需求</asp:ListItem>
                        <asp:ListItem Value="DJZYSQ">测控资源使用申请</asp:ListItem>
                        <asp:ListItem Value="GZJH">地面站工作计划</asp:ListItem>
                        <%--<asp:ListItem Value="ZZGZJH">ZZ地面站工作计划</asp:ListItem>--%>
                        <asp:ListItem Value="ZXJH">中心运行计划</asp:ListItem>
                        <asp:ListItem Value="TYSJ">仿真推演试验数据</asp:ListItem>
                        <asp:ListItem Value="DJZYJH">测控资源使用计划</asp:ListItem>
                        </asp:DropDownList>
                </td>
               <td>
               <asp:Button class="button" ID="btnSearch" runat="server" onclick="btnSearch_Click" Text="查询" 
                    Width="65px" />
&nbsp;<%--<asp:Button ID="btnReset" class="button" runat="server" Text="重置" Width="65px" 
                    onclick="btnReset_Click" />--%>
                    <button class="button" style="width:65px" onclick="return clearField();">清空</button>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                       ControlToCompare="txtStartDate" ControlToValidate="txtEndDate" 
                       Display="Dynamic" ErrorMessage="结束时间应大于起始时间" ForeColor="Red" 
                       Operator="GreaterThanEqual" Type="Date"></asp:CompareValidator>
                    <div style="display:none;">
                        <asp:TextBox ID="txtId" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ID="txtPlanID" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:TextBox ID="txtPlanType" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:Button ID="btnHidden" runat="server" ClientIDMode="Static" Text="btnHidden" 
                                OnClick="btnHidden_Click" />
                                <div>
                    <asp:Button ClientIDMode="Static"  ID="btnSubmit" class="button" runat="server" OnClick="btnSubmit_Click"
                        Text="发送" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" class="button" runat="server" OnClick="btnCancel_Click"
                        Text="取消" />
                        </div>
                        </div>
                   </td>
            </tr>
        </table>
        </div>
        <div id="divResourceStatus" class="index_content_view">
                <asp:Panel ID ="pnlAll1" runat="server">
                    <div id="selectAll1" >
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendPlan();">
                                    发送所选计划</button>
                            </td>
                            <td class="listTitle-c2">
                             <asp:Label ID="Label1" runat="server" Text="Label"  CssClass="error">发送计划时如选择多个计划，生成的外发文件后面的计划会覆盖前面的计划</asp:Label>
                                <div class="load" id="submitIndicator" style="display: none">
                                    提交中，请稍候。。。</div>
                            </td>
                        </tr>
                    </table>
                    </div>
         </asp:Panel>
                <asp:Repeater ID="rpDatas" runat="server">
                        <HeaderTemplate>
                            <table class="list">
                                <tr>
                                    <th style="width:20px;"><input type="checkbox" onclick="checkAll(this)" /></th>
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
                                        编辑
                                    </th>
                                        <%--                                    <th style="width: 70px;">
                                        明细
                                    </th>--%>
                                       <%-- <th style="width: 70px;">
                                            发送
                                        </th>--%>
                                    </tr>
                           <tbody id="tbPlans">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                    <input type="checkbox" name="chkDelete" value="<%# Eval("Id") %>" />
                                    </td>
                                    <td>
                                        <%# Eval("planid")%>
                                    </td>
                                    <td>
                                        <%# Eval("taskname")%>
                                    </td>
                                    <td>
                                        <%# Eval("PlanTypeName")%>
                                    </td>
                                    <td>
                                        <%# Eval("starttime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                    </td>
                                    <td>
                                        <%# Eval("endtime", "{0:" + this.SiteSetting.DateTimeFormat + "}")%>
                                    </td>
                                    <td>
                                        <button class="button"  name="htmbtnEdit"  onclick="return showEdit('<%# Eval("ID") %>','<%# Eval("PLANTYPE") %>')">
                                            编辑</button>
                                    </td>
                                    <%--                                <td>
                                    <button class="button" onclick="return showDetail('<%# Eval("ID") %>','<%# Eval("PLANTYPE") %>')">
                                        明细</button>
                                </td>--%>
                                <%--<td>
                                    <button class="button" onclick="return showSend('<%# Eval("ID") %>','<%# Eval("PLANID") %>','<%# Eval("PLANTYPE") %>')">
                                        发送计划</button>
                                </td>--%>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody> </table>
                        </FooterTemplate>
                    </asp:Repeater>
                <asp:Panel ID ="pnlAll2" runat="server">
                    <div id="selectAll2" >
                    <table class="listTitle">
                        <tr>
                            <td class="listTitle-c1">
                                <button class="button" onclick="return selectAll();">
                                    全选</button>&nbsp;&nbsp;
                                <button class="button" onclick="return sendPlan();">
                                    发送所选计划</button>
                            </td>
                            <td class="listTitle-c2">
                                <om:CollectionPager ID="cpPager" runat="server" PageSize="1">
                                </om:CollectionPager>
                            </td>
                        </tr>
                    </table>
                    </div>
                    </asp:Panel>
        </div>
       </asp:Panel>
        <asp:Panel ID="pnlDestination" runat="server">
        
        </asp:Panel>
        <div id="tartgetPanel" style="display:none">
        <table style = " text-align:center;">
            <tr>
        <td>
        <b>请选择要使用的发送协议：</b>
                <br />
                <asp:RadioButtonList ID="rbtProtocl" runat="server" 
                RepeatDirection="Horizontal">
                        <asp:ListItem Value="2" Selected="True">Fep with Tcp</asp:ListItem>
                        <asp:ListItem Value="1">Fep with Udp</asp:ListItem>
                        <asp:ListItem Value="0">Ftp</asp:ListItem>
                    </asp:RadioButtonList>
           </td>
        </tr>
            <tr>
                <td align="center"  style="text-align: center">
                <br />
                <b>请选择计划待发送的目标系统，可以多选</b>
                <br />
                    <%--<asp:RadioButtonList ID="rbtDestination" runat="server">
                    </asp:RadioButtonList>--%>
                    <asp:CheckBoxList ID="ckbDestination" runat="server">
                    </asp:CheckBoxList>
                    
                    <br />
                   <asp:Label ClientIDMode="Static" CssClass="error" ID="lblTargetMessage" runat="server" ForeColor="Red" style="display:none;">请选择要发送的目标系统</asp:Label>
                </td>
            </tr>
        </table>
    </div>
        <div id="divMessage"  title="消息">
            <asp:Label ID="lblMessage" CssClass="error" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div id="dialog-form" style="display:none" title="提示信息">
	    <p class="content"></p>
    </div>
</asp:Content>