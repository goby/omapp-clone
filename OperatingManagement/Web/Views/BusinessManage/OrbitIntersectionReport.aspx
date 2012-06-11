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
            $("#txtCutMainReportBeginDate").datepicker();
            $("#txtCutMainLYDate").datepicker();
            $("#txtCutSubLYDate").datepicker();
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
                      <asp:FileUpload ID="fuCutMainFile" ClientIDMode="Static" runat="server" ViewStateMode="Enabled" />
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
                      <asp:RequiredFieldValidator ID="rfvCutMainReportBeginDate" runat="server" Display="Dynamic"
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
                      <asp:TextBox ID="txtCutMainReportBeginTimeMilliSecond" runat="server" Width="40px" MaxLength="6" CssClass="norText"></asp:TextBox>毫秒
                      <asp:RequiredFieldValidator ID="rfvCutMainReportBeginTimeMilliSecond" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainReportBeginTimeMilliSecond" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                      预报时长(<span class="red">*</span>)
                  </th>
                  <td colspan="3">
                      <asp:TextBox ID="txtCutMainReportTime" runat="server" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvCutMainReportTime" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainReportTime" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                      历元日期(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainLYDate" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="rfvCutMainLYDate" runat="server" Display="Dynamic"
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
                      <asp:TextBox ID="txtCutMainLYTimeMilliSecond" runat="server" Width="40px" MaxLength="6" CssClass="norText"></asp:TextBox>毫秒
                      <asp:RequiredFieldValidator ID="rfvCutMainLYTimeMilliSecond" runat="server" Display="Dynamic"
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
                      <asp:TextBox ID="txtCutMainSatelliteSm" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>米/千克
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
                      <asp:RequiredFieldValidator ID="rfvCutMainD1" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD1" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D2(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD2" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD2Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="rfvCutMainD2" runat="server" Display="Dynamic"
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
                      <asp:RequiredFieldValidator ID="rfvCutMainD3" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD3" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D4(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD4" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD4Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="rfvCutMainD4" runat="server" Display="Dynamic"
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
                      <asp:RequiredFieldValidator ID="rfvCutMainD5" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMainD5" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D6(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMainD6" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMainD6Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="rfvCutMainD6" runat="server" Display="Dynamic"
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
                      <asp:RequiredFieldValidator ID="rfvCutMaindR" runat="server" Display="Dynamic"
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
                      <asp:RequiredFieldValidator ID="rfvCutMaindA" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMaindA" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     dE(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutMaindE" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutMaindEUnit" runat="server" ClientIDMode="Static" Text="deg"></asp:Label>
                      <asp:RequiredFieldValidator ID="rfvCutMaindE" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutMaindE" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
          </table>
      </asp:View>
      <asp:View ID="cutSub" runat="server">
       <table class="edit" style="width: 800px; margin:10px 0px;">
              <tr>
                  <th style="width: 140px;">
                      CutSub文件选项(<span class="red">*</span>)
                  </th>
                  <td>
                     <asp:RadioButtonList ID="rblCutSubFileOption" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblCutSubFileOption_SelectedIndexChanged">
                          <asp:ListItem Text="手工录入" Value="1" Selected="True"></asp:ListItem>
                          <asp:ListItem Text="文件上传" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
         </table>
         <table id="tbCutSubUpload" runat="server" visible="false" class="edit" style="width: 800px;">
              <tr>
                  <th style="width: 140px;">
                      CutSub文件(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:FileUpload ID="fuCutSubFile" ClientIDMode="Static" runat="server" />
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="fuCutSubFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
          </table>
          <table id="tbCutSubFillIn" runat="server" visible="true" class="edit" style="width: 800px;">
              <tr>
                  <th style="width: 140px;">
                      历元日期(<span class="red">*</span>)
                  </th>
                  <td style="width: 260px;">
                      <asp:TextBox ID="txtCutSubLYDate" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubLYDate" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th style="width: 120px;">
                     历元时刻(<span class="red">*</span>)
                  </th>
                  <td style="width: 280px;">
                     <asp:DropDownList ID="dplCutSubLYTimeHour" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutSubLYTimeMinute" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:DropDownList ID="dplCutSubLYTimeSecond" runat="server" CssClass="norDpl" Width="60px">
                      </asp:DropDownList>
                      <asp:TextBox ID="txtCutSubLYTimeMilliSecond" runat="server" Width="40px" MaxLength="6" CssClass="norText"></asp:TextBox>毫秒
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubLYTimeMilliSecond" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
              <tr>
                  <th>
                     主星(<span class="red">*</span>)
                  </th>
                  <td colspan="3">
                      <asp:DropDownList ID="dplCutSubSatellite" runat="server" CssClass="norDpl" AutoPostBack="true" OnSelectedIndexChanged="dplCutSubSatellite_SelectedIndexChanged">
                      </asp:DropDownList>
                  </td>
              </tr>
              <tr>
                  <th>
                     NO(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubSatelliteNO" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
                  <th>
                     KK(<span class="red">*</span>)
                  </th>
                  <td>
                       <asp:TextBox ID="txtCutSubSatelliteKK" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
              </tr>
              <tr>
                  <th>
                     Sm(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubSatelliteSm" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>米/千克
                  </td>
                   <th>
                     Ref(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubSatelliteRef" runat="server" Enabled="false" CssClass="norText"></asp:TextBox>
                  </td>
              </tr>
               <tr>
                  <th>
                     D1(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD1" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD1Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD1" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D2(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD2" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD2Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD2" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
               <tr>
                  <th>
                     D3(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD3" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD3Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD3" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D4(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD4" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD4Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD4" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
               <tr>
                  <th>
                     D5(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD5" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD5Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD5" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
                  <th>
                     D6(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:TextBox ID="txtCutSubD6" runat="server" ClientIDMode="Static" CssClass="norText"></asp:TextBox>
                      <asp:Label ID="lblCutSubD6Unit" runat="server" ClientIDMode="Static" Text="米"></asp:Label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutSubD6" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                  </td>
              </tr>
          </table>
      </asp:View>
        <asp:View ID="cutOptional" runat="server">
            <table class="edit" style="width: 800px; margin: 10px 0px;">
                <tr>
                    <th style="width: 140px;">
                        CutOptional文件选项(<span class="red">*</span>)
                    </th>
                    <td>
                        <asp:RadioButtonList ID="rblCutOptionalFileOption" runat="server" RepeatDirection="Horizontal"
                            AutoPostBack="true" OnSelectedIndexChanged="rblCutOptionalFileOption_SelectedIndexChanged">
                            <asp:ListItem Text="手工录入" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="文件上传" Value="0"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
            <table id="tbCutOptionalUpload" runat="server" visible="false" class="edit" style="width: 800px;">
                <tr>
                    <th style="width: 140px;">
                        CutOptional文件(<span class="red">*</span>)
                    </th>
                    <td>
                        <asp:FileUpload ID="fuCutOptionalFile" ClientIDMode="Static" runat="server" ViewStateMode="Enabled" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" Display="Dynamic"
                            ForeColor="Red" ControlToValidate="fuCutOptionalFile" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
            <table id="tbCutOptionalFillIn" runat="server" visible="true" class="edit" style="width: 800px;">
              <tr>
                  <th style="width: 140px;">
                     预报数据时间间隔(<span class="red">*</span>)
                  </th>
                  <td style="width: 260px;">
                      <asp:TextBox ID="txtCutOptionalTimeInterval" runat="server" ClientIDMode="Static" CssClass="norText" Width="60px"></asp:TextBox>秒
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                          ForeColor="Red" ControlToValidate="txtCutOptionalTimeInterval" ErrorMessage="（必填）"></asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCutOptionalTimeInterval" ForeColor="Red" Type="Integer" MinimumValue="1" MaximumValue="30" ErrorMessage="（1-30整数）"></asp:RangeValidator>
                  </td>
                  <th style="width: 120px;">
                     第三体引力(<span class="red">*</span>)
                  </th>
                  <td style="width: 280px;">
                     <asp:RadioButtonList ID="rblCutOptionalGravitation" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
              <tr>
                  <th>
                     潮汐摄动(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:RadioButtonList ID="rblCutOptionalTide" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
                  <th>
                     光压摄动(<span class="red">*</span>)
                  </th>
                  <td>
                     <asp:RadioButtonList ID="rblCutOptionalLight" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
              <tr>
                  <th>
                     大气阻尼摄动(<span class="red">*</span>)
                  </th>
                  <td>
                      <asp:RadioButtonList ID="rblCutOptionalEther" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
                  <th>
                     后牛顿项(<span class="red">*</span>)
                  </th>
                  <td>
                     <asp:RadioButtonList ID="rblCutOptionalNewton" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="考虑" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="不考虑" Value="0"></asp:ListItem>
                      </asp:RadioButtonList>
                  </td>
              </tr>
              </table>
        </asp:View>
    </asp:MultiView>
    <table class="edit" style="width: 800px; margin:10px 0px;">
      <tr id="trMessage" runat="server" visible="false">
        <th> &nbsp;</th>
        <td>
            <asp:Label ID="lblMessage" runat="server" CssClass="error" Text="“相关文件”内容必须严格按照格式要求编写。"></asp:Label>
        </td>
    </tr>
    <div id="divCalResult" runat="server" visible="false">
    <tr>
        <th>计算结果文件路径</th>
        <td>
            <asp:Label ID="lblResultFilePath" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
       <th>计算结果</th>
       <td>
          <asp:Label ID="lblCalResult" runat="server" Text="计算成功" ForeColor="Red"></asp:Label>
          <asp:LinkButton ID="lbtnViewResult" runat="server" OnClientClick="javascript:_resultDialog.dialog('open');return false;">查看</asp:LinkButton>
          <asp:LinkButton ID="lbtnResultFileDownload" runat="server" OnClick="lbtnResultFileDownload_Click" CausesValidation="false">下载</asp:LinkButton>
       </td>    
    </tr>
    </div>
    <tr>
        <th style="width: 140px;">&nbsp;</th>
        <td>
            <asp:Button ID="btnCalculate" runat="server" Text="开始计算" CssClass="button" 
                onclick="btnCalculate_Click" />
        </td>
    </tr>
</table>
<div id="dialog-form" style="display:none" title="提示信息">
	<p class="content">
        <b>星历数据文件路径：</b><asp:Literal ID="ltXLDataFilePath" runat="server"></asp:Literal><br />
        <b>差值计算时间文件路径：</b><asp:Literal ID="ltDifCalTimeFilePath" runat="server"></asp:Literal><br />
        <b>星历数据文件内容：</b><asp:Literal ID="ltXLDataFile" runat="server"></asp:Literal><br />
        <b>差值计算时间文件内容：</b><asp:Literal ID="ltDifCalTimeFile" runat="server"></asp:Literal><br />
    </p>
</div>
<div id="result-form" style="display:none" title="结果文件内容">
	<p class="content">
        <b>结果文件内容：</b><asp:Literal ID="ltResultFile" runat="server"></asp:Literal><br />
    </p>
</div>
</asp:Content>
