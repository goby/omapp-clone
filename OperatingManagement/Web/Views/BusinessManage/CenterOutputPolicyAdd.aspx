<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="CenterOutputPolicyAdd.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.CenterOutputPolicyAdd" %>

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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
    <om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="index" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuIndex" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
    业务管理&gt;中心输出策略添加
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <table class="edit" style="width: 800px;">
        <tr>
            <th style="width: 150px;">
                任务代号(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplTask" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplTask" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                卫星名称(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplSatName" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="dplSatName" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                信源(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplInfoSource" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv1" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="dplInfoSource" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                信息类别(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplInfoType" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv2" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="dplInfoType" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                信宿(<span class="red">*</span>)
            </th>
            <td>
                <asp:DropDownList ID="dplDdestination" runat="server" CssClass="norDpl">
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv3" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="dplDdestination" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                生效时间(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtEffectTime" runat="server" onclick="setday(this);" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtEffectTime" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <th>
                失效时间(<span class="red">*</span>)
            </th>
            <td>
                <asp:TextBox ID="txtDefectTime" runat="server" onclick="setday(this);" CssClass="norText"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                    ForeColor="Red" ControlToValidate="txtDefectTime" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" Display="Dynamic" ForeColor="Red"
                    ControlToValidate="txtDefectTime" ControlToCompare="txtEffectTime" Type="Date"
                    Operator="GreaterThanEqual" ErrorMessage="失效时间应大于生效时间"></asp:CompareValidator>
            </td>
        </tr>
        <tr>
            <th>
                描述
            </th>
            <td>
                <asp:TextBox ID="txtNote" runat="server" Width="320px" Height="120px" CssClass="text"
                    MaxLength="200" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr id="trMessage" runat="server" visible="false">
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                &nbsp;
            </th>
            <td>
                <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="提 交" OnClick="btnSubmit_Click" />
                <asp:Button ID="btnReturn" runat="server" CssClass="button" Text="返 回" OnClick="btnReturn_Click"
                    CausesValidation="false" />
            </td>
        </tr>
    </table>
</asp:Content>
