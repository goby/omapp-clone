#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:GroundResourceAdd.cs
//Remark:地面站资源添加类
//------------------------------------------------------
//VERSION       AUTHOR      DATE        CONTENT
//1.0           liutao      20111015    Create     
//------------------------------------------------------
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using OperatingManagement.Framework.Core;
using OperatingManagement.DataAccessLayer.BusinessManage;
using OperatingManagement.WebKernel.Basic;

namespace OperatingManagement.Web.Views.BusinessManage
{
    public partial class GroundResourceAdd : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 资源属性列表
        /// </summary>
        protected List<ZYSXExt> ZYSXExtList
        {
            get
            {
                if (ViewState["ZYSXExt"] == null)
                {
                    return new List<ZYSXExt>();
                }
                else
                {
                    return (ViewState["ZYSXExt"] as List<ZYSXExt>);
                }
            }
        }
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                trMessage.Visible = false;
                lblMessage.Text = string.Empty;
                if (!IsPostBack)
                {
                    BindDataSource();
                    BindZYSXControls();
                    BindZYSXExtList();
                }
                cpZYSXPager.PostBackPage += new EventHandler(cpZYSXPager_PostBackPage);
            }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面初始化出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 提交添加地面站资源记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string msg = string.Empty;
                if (string.IsNullOrEmpty(dplGroundStation.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站不能为空";
                    return;
                }

                int rid = 0;
                if (!int.TryParse(dplGroundStation.SelectedValue, out rid))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "地面站序号格式错误";
                    return;
                }

                if (string.IsNullOrEmpty(txtEquipmentName.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备名称不能为空";
                    return;
                }

                if (string.IsNullOrEmpty(txtEquipmentCode.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号不能为空";
                    return;
                }

                if (cblFunctionType.SelectedItem == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "请选择功能类型";
                    return;
                }
                string functionType = string.Empty;
                foreach (ListItem item in cblFunctionType.Items)
                {
                    if (item.Selected)
                    {
                        functionType += string.IsNullOrEmpty(functionType) ? item.Value : ";" + item.Value;
                    }
                }

                Framework.FieldVerifyResult result;
                GroundResource groundResource = new GroundResource();
                groundResource.RID = rid;
                groundResource.EquipmentName = txtEquipmentName.Text.Trim();
                groundResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                groundResource.FunctionType = functionType;
                groundResource.Status = 1;//正常
                groundResource.CreatedTime = DateTime.Now;
                groundResource.CreatedUserID = LoginUserInfo.Id;

                if (ZYSXExtList != null && ZYSXExtList.Count > 0)
                {
                    string extProperties = string.Empty;
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<ZYSXExt>));
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        xmlSerializer.Serialize(ms, ZYSXExtList);
                        extProperties = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                    }
                    groundResource.ExtProperties = extProperties;
                }

                if (groundResource.HaveActiveEquipmentCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号已经存在";
                    return;
                }

                result = groundResource.Add();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "添加地面站资源成功。";
                        ResetControls();
                        break;
                    default:
                        msg = "发生未知错误，操作失败。";
                        break;
                }
                trMessage.Visible = true;
                lblMessage.Text = msg;
            }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面btnSubmit_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 清除当前控件的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ResetControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面btnReset_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                string url = @"~/Views/BusinessManage/ResourceManage.aspx?resourcetype=" + Server.UrlEncode("1");
                Response.Redirect(url);
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("新增地面站资源页面btnReturn_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 当资源属性发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dplZYSX_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                BindZYSXControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                throw (new AspNetException("新增地面站资源页面dplZYSX_SelectedIndexChanged方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 添加资源属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddZYSX_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dplZYSX.SelectedValue))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "属性名称不能为空";
                    return;
                }
                if (string.IsNullOrEmpty(txtPValue.Text.Trim()))
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "属性值不能为空";
                    return;
                }
                //TO DO:按照类型、范围校验属性值


                int id = 0;
                int.TryParse(dplZYSX.SelectedValue, out id);
                ZYSX zysx = new ZYSX();
                zysx.Id = id;
                zysx = zysx.SelectByID();
                if (zysx != null)
                {
                    if (!zysx.ValidateValueRegular(txtPValue.Text.Trim()))
                    {
                        trMessage.Visible = true;
                        lblMessage.Text = "属性值类型不符合规范";
                        return;
                    }
                    if (!zysx.ValidateValueRange(txtPValue.Text.Trim()))
                    {
                        trMessage.Visible = true;
                        lblMessage.Text = "属性值范围不符合规范";
                        return;
                    }
                    ZYSXExt zysxExt = new ZYSXExt(zysx);
                    zysxExt.PValueID = Guid.NewGuid().ToString();
                    zysxExt.PValue = txtPValue.Text.Trim();
                    List<ZYSXExt> zysxExtList = ZYSXExtList;
                    zysxExtList.Add(zysxExt);
                    ViewState["ZYSXExt"] = zysxExtList;
                    BindZYSXExtList();
                    ResetZYSXControls();
                }   
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                throw (new AspNetException("新增地面站资源页面btnAddZYSX_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 删除资源属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDeleteZYSX_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lbtnDeleteZYSX = (sender as LinkButton);
                string pValueID = lbtnDeleteZYSX.CommandArgument;
                List<ZYSXExt> zysxExtList = ZYSXExtList;
                int index = zysxExtList.FindIndex(a => a.PValueID.ToLower() == pValueID.ToLower());
                if (index >= 0)
                    zysxExtList.RemoveAt(index);

                ViewState["ZYSXExt"] = zysxExtList;
                BindZYSXExtList();
                ResetZYSXControls();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                throw (new AspNetException("新增地面站资源页面lbtnDeleteZYSX_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cpZYSXPager_PostBackPage(object sender, EventArgs e)
        {
            try
            {
                BindZYSXExtList();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("新增地面站资源页面cpZYSXPager_PostBackPage方法出现异常，异常原因", ex));
            }
        }
        public override void OnPageLoaded()
        {
            this.PagePermission = "OMB_GRes.Add";
            this.ShortTitle = "新增地面站资源";
            this.SetTitle();
        }

        #region Method
        /// <summary>
        /// 绑定控件数据源
        /// </summary>
        private void BindDataSource()
        {
            dplGroundStation.Items.Clear();
            dplGroundStation.DataSource = new XYXSInfo().GetGrountStationList();
            dplGroundStation.DataTextField = "AddrName";
            dplGroundStation.DataValueField = "Id";
            dplGroundStation.DataBind();
            //dplGrountStation.Items.Insert(0, new ListItem("请选择", ""));

            dplZYSX.Items.Clear();
            dplZYSX.DataSource = new ZYSX().GetGroundStationZYSXList();
            dplZYSX.DataTextField = "PName";
            dplZYSX.DataValueField = "Id";
            dplZYSX.DataBind();
            //dplZYSX.Items.Insert(0, new ListItem("请选择", ""));

            cblFunctionType.Items.Clear();
            cblFunctionType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceFunctionType);
            cblFunctionType.DataTextField = "key";
            cblFunctionType.DataValueField = "value";
            cblFunctionType.DataBind();
        }
        /// <summary>
        /// 绑定资源属性相关控件
        /// </summary>
        private void BindZYSXControls()
        {
            int id = 0;
            int.TryParse(dplZYSX.SelectedValue, out id);
            ZYSX zysx = new ZYSX();
            zysx.Id = id;
            zysx = zysx.SelectByID();
            if (zysx != null)
            {
                lblZYSXType.Text = SystemParameters.GetSystemParameterText(SystemParametersType.ZYSXType, zysx.Type.ToString());
                lblZYSXScope.Text = zysx.Scope;
            }
            txtPValue.Text = string.Empty;
        }
        /// <summary>
        /// 重置资源属性控件
        /// </summary>
        private void ResetZYSXControls()
        {
            dplZYSX.SelectedIndex = 0;
            txtPValue.Text = string.Empty;

            BindZYSXControls();
        }
        /// <summary>
        /// 绑定资源属性列表
        /// </summary>
        private void BindZYSXExtList()
        {
            if (ZYSXExtList.Count > this.SiteSetting.PageSize)
                cpZYSXPager.Visible = true;
            cpZYSXPager.DataSource = ZYSXExtList;
            cpZYSXPager.PageSize = this.SiteSetting.PageSize;
            cpZYSXPager.BindToControl = rpZYSXList;
            rpZYSXList.DataSource = cpZYSXPager.DataSourcePaged;
            rpZYSXList.DataBind();
        }
        /// <summary>
        /// 添加完成后将控件设置为初始状态
        /// </summary>
        private void ResetControls()
        {
            dplGroundStation.SelectedIndex = 0;

            txtEquipmentName.Text = string.Empty;
            txtEquipmentCode.Text = string.Empty;

            foreach (ListItem item in cblFunctionType.Items)
            {
                item.Selected = false;
            }

            ViewState["ZYSXExt"] = null;
            ResetZYSXControls();
            BindZYSXExtList();
        }

        #endregion
    }
}