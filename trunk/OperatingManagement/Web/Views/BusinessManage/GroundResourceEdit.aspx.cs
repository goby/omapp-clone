﻿#region
//------------------------------------------------------
//Assembly:OperatingManagement.Web
//FileName:GroundResourceEdit.cs
//Remark:地面站资源编辑类
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
    public partial class GroundResourceEdit : AspNetPage
    {
        #region 属性
        /// <summary>
        /// 地面站资源ID
        /// </summary>
        protected int GRID
        {
            get
            {
                int grID = 0;
                if (Request.QueryString["grid"] != null)
                {
                    int.TryParse(Request.QueryString["grid"], out grID);
                }
                return grID;
            }
        }
        /// <summary>
        /// 资源属性键值对列表
        /// </summary>
        protected Dictionary<int, string> ZYSXIDPValueDic
        {
            get
            {
                if (ViewState["ZYSXIDPValueDic"] == null)
                {
                    return new Dictionary<int, string>();
                }
                else
                {
                    return (ViewState["ZYSXIDPValueDic"] as Dictionary<int, string>);
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
                    BindControls();
                    BindZYSXList();
                }
                BindRepeaterItems();
                cpZYSXPager.PostBackPage += new EventHandler(cpZYSXPager_PostBackPage);
            }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面初始化出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 提交更新地面站资源记录
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

                if (!LoopRepeaterItems())
                {
                    return;
                }

                Framework.FieldVerifyResult result;
                GroundResource groundResource = new GroundResource();
                groundResource.Id = GRID;
                groundResource = groundResource.SelectByID();
                if (groundResource == null)
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "修改的地面站资源不存在";
                    return;
                }
                groundResource.RID = rid;
                groundResource.EquipmentName = txtEquipmentName.Text.Trim();
                groundResource.EquipmentCode = txtEquipmentCode.Text.Trim();
                groundResource.FunctionType = functionType;
                //groundResource.Status = 1;
                //groundResource.CreatedTime = DateTime.Now;
                //groundResource.CreatedUserID = LoginUserInfo.Id;
                groundResource.UpdatedTime = DateTime.Now;
                groundResource.UpdatedUserID = LoginUserInfo.Id;

                //Dictionary<int,string>不支持序列化，采用其他方法
                //if (ZYSXIDPValueDic != null && ZYSXIDPValueDic.Count > 0)
                //{
                //    string extProperties = string.Empty;
                //    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Dictionary<int, string>));
                //    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                //    {
                //        xmlSerializer.Serialize(ms, ZYSXIDPValueDic);
                //        extProperties = System.Text.Encoding.UTF8.GetString(ms.ToArray());
                //    }
                //    groundResource.ExtProperties = extProperties;
                //}

                if (ZYSXIDPValueDic != null && ZYSXIDPValueDic.Count > 0)
                {
                    string extProperties = string.Empty;
                    foreach (int key in ZYSXIDPValueDic.Keys)
                    {
                        if (!string.IsNullOrEmpty(extProperties))
                            extProperties += "|$|";

                        extProperties += key + "|#|" + ZYSXIDPValueDic[key];
                    }

                    groundResource.ExtProperties = extProperties;
                }

                if (groundResource.HaveActiveEquipmentCode())
                {
                    trMessage.Visible = true;
                    lblMessage.Text = "设备编号已经存在";
                    return;
                }

                result = groundResource.Update();
                switch (result)
                {
                    case Framework.FieldVerifyResult.Error:
                        msg = "发生了数据错误，无法完成请求的操作。";
                        break;
                    case Framework.FieldVerifyResult.Success:
                        msg = "修改地面站资源成功。";
                        BindControls();
                        BindZYSXList();
                        BindRepeaterItems();
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
                throw (new AspNetException("编辑地面站资源页面btnSubmit_Click方法出现异常，异常原因", ex));
            }
        }
        /// <summary>
        /// 重置当前控件的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                BindControls();
                BindZYSXList();
                BindRepeaterItems();
            }
            catch (System.Threading.ThreadAbortException ex1)
            { }
            catch (Exception ex)
            {
                trMessage.Visible = true;
                lblMessage.Text = "发生未知错误，操作失败。";
                throw (new AspNetException("编辑地面站资源页面btnReset_Click方法出现异常，异常原因", ex));
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
                throw (new AspNetException("编辑地面站资源页面btnReturn_Click方法出现异常，异常原因", ex));
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
                BindZYSXList();
            }
            catch (Exception ex)
            {
                throw (new AspNetException("编辑地面站资源页面cpZYSXPager_PostBackPage方法出现异常，异常原因", ex));
            }
        }
        public override void OnPageLoaded()
        {
            this.ShortTitle = "编辑地面站资源";
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
            //dplGroundStation.Items.Insert(0, new ListItem("请选择", ""));

            cblFunctionType.Items.Clear();
            cblFunctionType.DataSource = SystemParameters.GetSystemParameters(SystemParametersType.GroundResourceFunctionType);
            cblFunctionType.DataTextField = "key";
            cblFunctionType.DataValueField = "value";
            cblFunctionType.DataBind();
        }
        /// <summary>
        /// 绑定控件值
        /// </summary>
        private void BindControls()
        {
            GroundResource groundResource = new GroundResource();
            groundResource.Id = GRID;
            groundResource = groundResource.SelectByID();
            if (groundResource != null)
            {
                dplGroundStation.SelectedValue = groundResource.RID.ToString();
                txtEquipmentName.Text = groundResource.EquipmentName;
                txtEquipmentCode.Text = groundResource.EquipmentCode;
                lblCreatedTime.Text = groundResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss");
                lblUpdatedTime.Text = groundResource.UpdatedTime == DateTime.MinValue ? groundResource.CreatedTime.ToString("yyyy-MM-dd HH:mm:ss") : groundResource.UpdatedTime.ToString("yyyy-MM-dd HH:mm:ss");

                string[] functionTypeArray = groundResource.FunctionType.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (ListItem item in cblFunctionType.Items)
                {
                    item.Selected = functionTypeArray.Contains(item.Value);
                }
                ////反序列化，Dictionary<int,string>不支持序列化，采用其他方法
                //if (!string.IsNullOrEmpty(groundResource.ExtProperties.Trim()))
                //{
                //    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(Dictionary<int,string>));
                //    using (System.IO.Stream xmlStream = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(groundResource.ExtProperties)))
                //    {
                //        using (System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(xmlStream))
                //        {
                //            ViewState["ZYSXIDPValueDic"] = (xmlSerializer.Deserialize(xmlReader) as List<ZYSXExt>);
                //        }
                //    }
                //}

                if (!string.IsNullOrEmpty(groundResource.ExtProperties))
                {
                    string[] keyValueArray = groundResource.ExtProperties.Split(new string[] { "|$|" }, StringSplitOptions.RemoveEmptyEntries);
                    if (keyValueArray != null)
                    {
                        Dictionary<int, string> zysxIDPValueDic = new Dictionary<int, string>();
                        foreach (string keyValue in keyValueArray)
                        {
                            string[] array = keyValue.Split(new string[] { "|#|" }, StringSplitOptions.RemoveEmptyEntries);
                            int key = 0;
                            if (array != null && array.Length == 2 && int.TryParse(array[0], out key))
                            {
                                if (zysxIDPValueDic.ContainsKey(key))
                                {
                                    zysxIDPValueDic[key] = array[1];
                                }
                                else
                                {
                                    zysxIDPValueDic.Add(key, array[1]);
                                }
                            }
                        }
                        ViewState["ZYSXIDPValueDic"] = zysxIDPValueDic;
                    }
                }
            }
        }
        /// <summary>
        /// 绑定资源属性列表
        /// </summary>
        private void BindZYSXList()
        {
            List<ZYSX> zysxList = new ZYSX().GetGroundStationZYSXList();
            cpZYSXPager.Visible = false;
            cpZYSXPager.DataSource = zysxList;
            cpZYSXPager.PageSize = zysxList.Count + 1;//扩展属性不分页
            cpZYSXPager.BindToControl = rpZYSXList;
            rpZYSXList.DataSource = cpZYSXPager.DataSourcePaged;
            rpZYSXList.DataBind();
        }
        /// <summary>
        /// 生成属性对应控件
        /// </summary>
        protected void BindRepeaterItems()
        {
            foreach (RepeaterItem item in rpZYSXList.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    //ZYSX zysx = (item.DataItem as ZYSX);
                    ZYSX zysx = null;
                    PlaceHolder phPValueControls = (item.FindControl("phPValueControls") as PlaceHolder);
                    HiddenField hfPID = (item.FindControl("hfPID") as HiddenField);
                    int id = 0;
                    if (hfPID != null && int.TryParse(hfPID.Value, out id))
                    {
                        zysx = new ZYSX();
                        zysx.Id = id;
                        zysx = zysx.SelectByID();
                    }
                    if (zysx != null && phPValueControls != null)
                    {
                        if (ZYSXIDPValueDic.ContainsKey(zysx.Id))
                        {
                            zysx.PValue = ZYSXIDPValueDic[zysx.Id];
                        }
                        List<Control> controlsList = zysx.GenerateControls();
                        foreach (Control ctl in controlsList)
                        {
                            phPValueControls.Controls.Add(ctl);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 获得并校验属性值
        /// </summary>
        private bool LoopRepeaterItems()
        {
            bool result = true;
            foreach (RepeaterItem item in rpZYSXList.Items)
            {
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    //ZYSX zysx = (item.DataItem as ZYSX);
                    ZYSX zysx = null;
                    PlaceHolder phPValueControls = (item.FindControl("phPValueControls") as PlaceHolder);
                    HiddenField hfPID = (item.FindControl("hfPID") as HiddenField);
                    int id = 0;
                    if (hfPID != null && int.TryParse(hfPID.Value, out id))
                    {
                        zysx = new ZYSX();
                        zysx.Id = id;
                        zysx = zysx.SelectByID();
                    }
                    if (zysx != null && phPValueControls != null)
                    {
                        zysx.GetPValueFromControl(phPValueControls);
                        if (!zysx.ValidatePValue())
                        {
                            result = false;
                            trMessage.Visible = true;
                            lblMessage.Text = string.Format("属性名称为“{0}”的属性值填写错误，请修改。", zysx.PName);
                            break;
                        }
                        Dictionary<int, string> zysxIDPValueDic = ZYSXIDPValueDic;
                        if (zysxIDPValueDic.ContainsKey(zysx.Id))
                        {
                            zysxIDPValueDic[zysx.Id] = zysx.PValue;
                        }
                        else
                        {
                            zysxIDPValueDic.Add(zysx.Id, zysx.PValue);
                        }
                        ViewState["ZYSXIDPValueDic"] = zysxIDPValueDic;
                    }
                }
            }
            return result;
        }
        #endregion
    }
}