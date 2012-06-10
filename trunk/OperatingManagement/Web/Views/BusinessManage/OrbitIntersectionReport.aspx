<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrbitIntersectionReport.aspx.cs" Inherits="OperatingManagement.Web.Views.BusinessManage.OrbitIntersectionReport" %>
<%@ Register src="../../ucs/ucSatellite.ascx" tagname="ucSatellite" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigatorContent" runat="server">
<om:PageNavigator ID="navMain" runat="server" CssName="menu-top" SelectedId="bizmanage" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
<om:PageMenu ID="PageMenu1" runat="Server" XmlFileName="menuBusiness" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MapPathContent" runat="server">
业务管理 &gt; 轨道分析 - 交会预报
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyContent" runat="server">
    <script type="text/javascript">
        $(function () {
            $("#txtReportBeginDate").datepicker();
            $("#txtLYDate").datepicker();
        });
    </script>
<asp:Menu ID="menuCut" runat="server" Orientation="Horizontal" 
        onmenuitemclick="menuCut_MenuItemClick">
<Items>
 <asp:MenuItem Text="CutMain" Value="0"></asp:MenuItem>
 <asp:MenuItem Text="CutSub" Value="1"></asp:MenuItem>
 <asp:MenuItem Text="CutOptional" Value="2"></asp:MenuItem>
</Items>
</asp:Menu>
    <asp:MultiView ID="mvCut" runat="server" ActiveViewIndex="0">
      <asp:View ID="cutMain" runat="server">
         <table class="edit" style="width: 800px; margin:10px 0px;">
              <tr>
                  <th style="width: 140px;">
                      CutMain文件选项(<span class="red">*</span>)
                  </th>
                  <td>
                     <asp:RadioButtonList ID="rblCutMainFileOption" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblCutMainFileOption_SelectedIndexChanged">
                          <asp:ListItem Text="手工录入" Value="1" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="文件上传" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
         </table>
         <table id="tbCutMainUpload" runat="server" visible="false" class="edit" style="width: 800px;">
              <tr>
                  <th style="width: 140px;">
                      CutMain文件(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:FileUpload ID="fuCutMainFile" ClientIDMode="Static" runat="server" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="fuCutMainFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
          </table>
          <table id="tbCutMainFillIn" runat="server" visible="true" class="edit" style="width: 800px;">
              <tr>
                  <th style="width: 140px;">
                      预报起始历元日期(<span class="red">*</span>)
                  </th>
                  <td style="width: 260px;">
                      <asp:TextBox ID="txtCutMainReportBeginDate" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainReportBeginDate" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th style="width: 120px;">
                      预报起始历元时刻(<span class="red">*</span>)
                  </th>
                  <td style="width: 280px;"> 
                      <asp:DropDownList ID="dplCutMainReportBeginTimeHour" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutMainReportBeginTimeMinute" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutMainReportBeginTimeSecond" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:TextBox ID="txtCutMainReportBeginTimeMilliSecond" runat="server" Width="40px" MaxLength="6" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainReportBeginTimeMilliSecond" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                      预报时长(<span class="red">*</span>)
                  </th>
                  <td colspan="3">
                      <asp:TextBox ID="txtCutMainReportTime" runat="server" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainReportTime" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                      历元日期(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainLYDate" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainLYDate" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     历元时刻(<span class="red">*</span>)
                  </th>
                  <td>
                     <asp:DropDownList ID="dplCutMainLYTimeHour" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutMainLYTimeMinute" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutMainLYTimeSecond" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:TextBox ID="txtCutMainLYTimeMilliSecond" runat="server" Width="40px" MaxLength="6" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainLYTimeMilliSecond" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                     主星(<span class="red">*</span>)
                  </th>
                  <td colspan="3">
                      <asp:DropDownList ID="dplCutMainSatellite" runat="server" CssClass="norDpl" AutoPostBack="true" OnSelectedIndexChanged="dplCutMainSatellite_SelectedIndexChanged">
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                  <th>
                     NO(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainSatelliteNO" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
                  <th>
                     KK(<span class="red">*</span>)
                  </th>
                  <td>
                       <asp:TextBox ID="txtCutMainSatelliteKK" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <th>
                     Sm(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainSatelliteSm" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
                   <th>
                     Ref(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainSatelliteRef" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
              </tr>
               <tr>
                  <th>
                     D1(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD1" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD1Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD1" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D2(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD2" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD2Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD2" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
               <tr>
                  <th>
                     D3(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD3" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD3Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD3" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D4(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD4" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD4Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD4" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
               <tr>
                  <th>
                     D5(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD5" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD5Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD5" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D6(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD6" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD6Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD6" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                     dR(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMaindR" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMaindRUnit" runat="server" ClientIDMode="Static" Text="Km"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMaindR" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     KAE(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:RadioButtonList ID="rblCutMainKAE" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblCutMainKAE_SelectedIndexChanged">
                          <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
              <tr>
                  <th>
                     dA(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMaindA" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMaindAUnit" runat="server" ClientIDMode="Static" Text="deg"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMaindA" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     dE(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMaindE" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMaindEUnit" runat="server" ClientIDMode="Static" Text="deg"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMaindE" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
          </table>
      </asp:View>
      <asp:View ID="cutSub" runat="server">
      </asp:View>
      <asp:View ID="cutOptional" runat="server">
      </asp:View>
    </asp:MultiView>
</asp:Content>
