using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using OperatingManagement.DataAccessLayer.PlanManage;
using OperatingManagement.DataAccessLayer.BusinessManage;

namespace ServicesKernel.File
{
    public class PlanFileCreator
    {
        #region -Properties-
        StreamReader sr;
        StreamWriter sw;
        string filename = "";
        XmlTextWriter xmlWriter;
        XmlTextReader xmlReader;
        XmlDocument xmldoc;
        XmlDeclaration xmldecl;
        XmlNode xmlnode;
        XmlElement xmlelem;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        /// <summary>
        /// 发送源名称
        /// </summary>
        public string Source 
        {
            get
            {
                return Param.SourceName;
            }
            set{}
        }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public string Format1 { get; set; }
        public string Format2 { get; set; }
        public string DataSection { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }

        private string strOutputPath = null;
        private string strSavePath = null;
        private string _filepath=null;
        /// <summary>
        /// 内部文件完整路径
        /// </summary>
        public string FilePath 
        {
            get
            {
                if (string.IsNullOrEmpty(_filepath))
                {
                    return SavePath + filename;
                }
                else
                {
                    return _filepath;
                }
            }
            set
            {
                _filepath = value;
            }
        }
        /// <summary>
        /// 外发文件完整路径
        /// </summary>
        public string SendingPath
        {
            get
            {
                if (string.IsNullOrEmpty(_filepath))
                {
                    return OutPutPath + filename;
                }
                else
                {
                    return _filepath;
                }
            }
            set
            {
                _filepath = value;
            }
        }
        /// <summary>
        /// 内部文件保存目录
        /// </summary>
        public string SavePath 
        {
            get
            {
                if (string.IsNullOrEmpty(strSavePath))
                    strSavePath = Param.SavePath;
                return strSavePath;
            }
        }
        /// <summary>
        /// 外发文件保存目录
        /// </summary>
        public string OutPutPath
        {
            get
            {
                if (string.IsNullOrEmpty(strOutputPath))
                    strOutputPath = Param.OutPutPath;
                return strOutputPath;
            }
        }

        #endregion

        public void NewFile()
        {
            sw= new StreamWriter(FilePath);
            sw.WriteLine("<说明区>");
            sw.WriteLine("[生成时间]：" + this.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]："+this.Source);
                sw.WriteLine("[信宿D]："+this.Destination);
                sw.WriteLine("[任务代码M]："+this.TaskID);
                sw.WriteLine("[信息类别B]："+this.InfoType);
                sw.WriteLine("[数据区行数L]："+this.LineCount.ToString("0000"));
            sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]："+this.Format1);
                if (!string.IsNullOrEmpty(this.Format2))
                {
                    sw.WriteLine("[格式标识2]：" + this.Format2);    //没有这个字段时不输出
                }
            sw.WriteLine("[数据区]：");
                sw.WriteLine(this.DataSection);
            sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
        }

        #region <Internal file>

        /// <summary>
        /// 应用研究
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">0:Add;1:Edit</param>
        /// <returns></returns>
        public string CreateYJJHFile(YJJH obj,int type)
        {
            if (type == 0)
            {
                filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("YJJH", obj.TaskID, obj.SatID);
            }
            xmlWriter = new XmlTextWriter(FilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("应用研究工作计划");

            xmlWriter.WriteStartElement("XXFL");
            xmlWriter.WriteString(obj.XXFL);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("JXH");
            xmlWriter.WriteString(obj.JXH);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("SysName");
            xmlWriter.WriteString(obj.SysName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("StartTime");
            xmlWriter.WriteString(obj.StartTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EndTime");
            xmlWriter.WriteString(obj.EndTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Task");
            xmlWriter.WriteString(obj.Task);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            xmlWriter.Close();

            return FilePath;
        }

        /// <summary>
        /// 空间信息需求计划
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">0:新增; 1:修改</param>
        /// <returns></returns>
        public string CreateXXXQFile(XXXQ obj, int type)
        {
            if (type == 0)
            {
                filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("XXXQ", obj.TaskID, obj.SatID);
            }
            xmlWriter = new XmlTextWriter(FilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("空间信息需求");

            #region MBXQ
            xmlWriter.WriteStartElement("空间目标信息需求");

            xmlWriter.WriteStartElement("User");
            xmlWriter.WriteString(obj.objMBXQ.User);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Time");
            xmlWriter.WriteString(obj.objMBXQ.Time);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TargetInfo");
            xmlWriter.WriteString(obj.objMBXQ.TargetInfo);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TimeSection1");
            xmlWriter.WriteString(obj.objMBXQ.TimeSection1);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TimeSection2");
            xmlWriter.WriteString(obj.objMBXQ.TimeSection2);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Sum");
            xmlWriter.WriteString(obj.objMBXQ.Sum);
            xmlWriter.WriteEndElement();

            
            for (int i = 1; i <= obj.objMBXQ.SatInfos.Count; i++)
            {
                xmlWriter.WriteStartElement("卫星");

                xmlWriter.WriteStartElement("SatName");
                xmlWriter.WriteString(obj.objMBXQ.SatInfos[i - 1].SatName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("InfoName");
                xmlWriter.WriteString(obj.objMBXQ.SatInfos[i - 1].InfoName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("InfoTime");
                xmlWriter.WriteString(obj.objMBXQ.SatInfos[i - 1].InfoTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
           


            xmlWriter.WriteEndElement();
            #endregion

            #region HJXQ
            xmlWriter.WriteStartElement("空间环境信息需求");

            xmlWriter.WriteStartElement("User");
            xmlWriter.WriteString(obj.objHJXQ.User);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Time");
            xmlWriter.WriteString(obj.objHJXQ.Time);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EnvironInfo");
            xmlWriter.WriteString(obj.objHJXQ.EnvironInfo);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TimeSection1");
            xmlWriter.WriteString(obj.objHJXQ.TimeSection1);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TimeSection2");
            xmlWriter.WriteString(obj.objHJXQ.TimeSection2);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Sum");
            xmlWriter.WriteString(obj.objHJXQ.Sum);
            xmlWriter.WriteEndElement();

            
            for (int i = 1; i <= obj.objHJXQ.SatInfos.Count; i++)
            {
                xmlWriter.WriteStartElement("卫星");

                xmlWriter.WriteStartElement("SatName");
                xmlWriter.WriteString(obj.objHJXQ.SatInfos[i - 1].SatName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("InfoName");
                xmlWriter.WriteString(obj.objHJXQ.SatInfos[i - 1].InfoName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("InfoArea");
                xmlWriter.WriteString(obj.objHJXQ.SatInfos[i - 1].InfoArea);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("InfoTime");
                xmlWriter.WriteString(obj.objHJXQ.SatInfos[i - 1].InfoTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
            

            xmlWriter.WriteEndElement();
            #endregion

            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            return FilePath;
        }

        /// <summary>
        /// 内部存储地面站工作计划
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="type">0:新增; 1:修改</param>
        /// <returns></returns>
        public string CreateDMJHFile(DMJH obj,int type)
        {
            if (type == 0)
            {
                filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("DMJH", obj.TaskID, obj.SatID);
                FilePath = SavePath + filename;
            }

            xmlWriter = new XmlTextWriter(FilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("地面站工作计划");
            #region basicinfo
            xmlWriter.WriteStartElement("编号");
            xmlWriter.WriteString(obj.Sequence);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("时间");
            xmlWriter.WriteString(obj.DateTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("工作单位");
            xmlWriter.WriteString(obj.StationName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("设备代号");
            xmlWriter.WriteString(obj.EquipmentID);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("任务个数");
            xmlWriter.WriteString(obj.TaskCount);
            xmlWriter.WriteEndElement();
            #endregion

            #region 任务
            for (int i = 0; i < obj.DMJHTasks.Count; i++)
            {
                xmlWriter.WriteStartElement("任务");
                #region BasicInfo
                xmlWriter.WriteStartElement("任务标志");
                xmlWriter.WriteString(obj.DMJHTasks[i].TaskFlag);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("工作方式");
                xmlWriter.WriteString(obj.DMJHTasks[i].WorkWay);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("计划性质");
                xmlWriter.WriteString(obj.DMJHTasks[i].PlanPropertiy);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("工作模式");
                xmlWriter.WriteString(obj.DMJHTasks[i].WorkMode);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("任务准备开始时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].PreStartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("任务开始时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].StartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("跟踪开始时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].TrackStartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("开上行载波时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].WaveOnStartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("关上行载波时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].WaveOffStartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("跟踪结束时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].TrackEndTime);
                xmlWriter.WriteEndElement();
                #endregion

                #region 实时传输
                for (int j = 0; j < obj.DMJHTasks[i].ReakTimeTransfors.Count; j++)
                {
                    xmlWriter.WriteStartElement("实时传输");

                    xmlWriter.WriteStartElement("格式标志");
                    xmlWriter.WriteString(obj.DMJHTasks[i].ReakTimeTransfors[j].FormatFlag);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("信息流标志");
                    xmlWriter.WriteString(obj.DMJHTasks[i].ReakTimeTransfors[j].InfoFlowFlag);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据传输开始时间");
                    xmlWriter.WriteString(obj.DMJHTasks[i].ReakTimeTransfors[j].TransStartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据传输结束时间");
                    xmlWriter.WriteString(obj.DMJHTasks[i].ReakTimeTransfors[j].TransEndTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据传输速率");
                    xmlWriter.WriteString(obj.DMJHTasks[i].ReakTimeTransfors[j].TransSpeedRate);
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteEndElement();
                }
                #endregion

                #region 事后回放
                for (int j = 0; j < obj.DMJHTasks[i].AfterFeedBacks.Count; j++)
                {
                    xmlWriter.WriteStartElement("事后回放");

                    xmlWriter.WriteStartElement("格式标志");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].FormatFlag);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("信息流标志");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].InfoFlowFlag);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据起始时间");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].DataStartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据结束时间");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].DataEndTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据传输开始时间");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].TransStartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("数据传输速率");
                    xmlWriter.WriteString(obj.DMJHTasks[i].AfterFeedBacks[j].TransSpeedRate);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteEndElement();
                }
                #endregion
                xmlWriter.WriteStartElement("任务结束时间");
                xmlWriter.WriteString(obj.DMJHTasks[i].EndTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }
            #endregion
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            return FilePath;
        }
        /// <summary>
        /// 中心计划
        /// </summary>
        public string CreateZXJHFile(ZXJH obj,int type)
        {
            if (type == 0)
            {
                filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("ZXJH", obj.TaskID, obj.SatID);
            }
            xmlWriter = new XmlTextWriter(FilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("中心运行计划");
            xmlWriter.WriteStartElement("日期");
            xmlWriter.WriteString(obj.Date);
            xmlWriter.WriteEndElement();

            #region 试验计划
            xmlWriter.WriteStartElement("试验计划");

            xmlWriter.WriteStartElement("对应日期的试验个数");
            xmlWriter.WriteString(obj.SYCount);
            xmlWriter.WriteEndElement();
            #region 试验内容
            xmlWriter.WriteStartElement("试验内容");
            xmlWriter.WriteStartElement("在当日计划中的ID");
            xmlWriter.WriteString(obj.SYID);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("试验项目名称");
            xmlWriter.WriteString(obj.SYName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("试验开始日期及时间");
            xmlWriter.WriteString(obj.SYDateTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("试验运行的天数");
            xmlWriter.WriteString(obj.SYDays);
            xmlWriter.WriteEndElement();
            #region 载荷
            xmlWriter.WriteStartElement("载荷");
                xmlWriter.WriteStartElement("载荷名称");
                xmlWriter.WriteString(obj.SYLoadName);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("开始时间");
                xmlWriter.WriteString(obj.SYLoadStartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("结束时间");
                xmlWriter.WriteString(obj.SYLoadEndTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("动作内容");
                xmlWriter.WriteString(obj.SYLoadContent);
                xmlWriter.WriteEndElement();
            xmlWriter.WriteEndElement();
            #endregion
            #region 数传
            xmlWriter.WriteStartElement("数传");
            xmlWriter.WriteStartElement("站编号");
            xmlWriter.WriteString(obj.SY_SCStationNO);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("设备编号");
            xmlWriter.WriteString(obj.SY_SCEquipmentNO);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("频段");
            xmlWriter.WriteString(obj.SY_SCFrequencyBand);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("圈次");
            xmlWriter.WriteString(obj.SY_SCLaps);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("开始时间");
            xmlWriter.WriteString(obj.SY_SCStartTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("结束时间");
            xmlWriter.WriteString(obj.SY_SCEndTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            #endregion
            #region 测控
            xmlWriter.WriteStartElement("测控");
            xmlWriter.WriteStartElement("站编号");
            xmlWriter.WriteString(obj.SY_CKStationNO);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("设备编号");
            xmlWriter.WriteString(obj.SY_CKEquipmentNO);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("圈次");
            xmlWriter.WriteString(obj.SY_CKLaps);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("开始时间");
            xmlWriter.WriteString(obj.SY_CKStartTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("结束时间");
            xmlWriter.WriteString(obj.SY_CKEndTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            #endregion
            #region 注数
            xmlWriter.WriteStartElement("注数");
            xmlWriter.WriteStartElement("最早时间要求");
            xmlWriter.WriteString(obj.SY_ZSFirst);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("最晚时间要求");
            xmlWriter.WriteString(obj.SY_ZSLast);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("主要内容");
            xmlWriter.WriteString(obj.SY_ZSContent);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            #endregion
            xmlWriter.WriteEndElement();
            #endregion
            xmlWriter.WriteEndElement();
            #endregion

            #region 工作计划
            xmlWriter.WriteStartElement("工作计划");
            #region 任务管理
            xmlWriter.WriteStartElement("任务管理");
            for (int i = 1; i <= obj.WorkContents.Count; i++)
            {
                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                    xmlWriter.WriteStartElement("工作");
                    xmlWriter.WriteString(obj.WorkContents[i - 1].Work);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("对应试验ID");
                    xmlWriter.WriteString(obj.WorkContents[i - 1].SYID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("开始时间");
                    xmlWriter.WriteString(obj.WorkContents[i - 1].StartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("最短持续时间");
                    xmlWriter.WriteString(obj.WorkContents[i - 1].MinTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("最长持续时间");
                    xmlWriter.WriteString(obj.WorkContents[i - 1].MaxTime);
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            #endregion
            #region 载荷管理
            xmlWriter.WriteStartElement("载荷管理");
                  #region 载荷管理
            xmlWriter.WriteStartElement("载荷管理");
                xmlWriter.WriteStartElement("对应试验ID");
                xmlWriter.WriteString(obj.Work_Load_SYID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("卫星代号");
                xmlWriter.WriteString(obj.Work_Load_SatID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("载荷名称");
                xmlWriter.WriteString(obj.Work_Load_Name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("进程");
                xmlWriter.WriteString(obj.Work_Load_Process);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("事件");
                xmlWriter.WriteString(obj.Work_Load_Event);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("动作");
                xmlWriter.WriteString(obj.Work_Load_Action);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("开始时间");
                xmlWriter.WriteString(obj.Work_Load_StartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("结束时间");
                xmlWriter.WriteString(obj.Work_Load_EndTime);
                xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            #endregion
                  #region 指令制作
            xmlWriter.WriteStartElement("指令制作");
                xmlWriter.WriteStartElement("对应试验ID");
                xmlWriter.WriteString(obj.Work_Command_SYID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("试验项目");
                xmlWriter.WriteString(obj.Work_Command_SYItem);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("卫星代号");
                xmlWriter.WriteString(obj.Work_Command_SatID);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("作业");
                xmlWriter.WriteString(obj.Work_Command_Content);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("上注要求");
                xmlWriter.WriteString(obj.Work_Command_UpRequire);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("指令发送方向");
                xmlWriter.WriteString(obj.Work_Command_Direction);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("开始时间");
                xmlWriter.WriteString(obj.Work_Command_StartTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("结束时间");
                xmlWriter.WriteString(obj.Work_Command_EndTime);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("特殊需求");
                xmlWriter.WriteString(obj.Work_Command_SpecialRequire);
                xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();
            #endregion
            xmlWriter.WriteEndElement();
            #endregion
            #region 试验数据处理
            xmlWriter.WriteStartElement("试验数据处理");
                            for (int i = 1; i <= obj.SYDataHandles.Count; i++)
                            {
                                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                                    xmlWriter.WriteStartElement("对应试验ID");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].SYID);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("卫星代号");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].SatID);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("圈次");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].Laps);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("主站名称");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].MainStationName);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("主站设备");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].MainStationEquipment);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("备站名称");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].BakStationName);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("备站设备");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].BakStationEquipment);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("工作内容");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].Content);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("实时开始处理时间");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].StartTime);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("实时结束处理时间");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].EndTime);
                                    xmlWriter.WriteEndElement();

                                    xmlWriter.WriteStartElement("事后数据处理");
                                    xmlWriter.WriteString(obj.SYDataHandles[i - 1].AfterWardsDataHandle);
                                    xmlWriter.WriteEndElement();
                                xmlWriter.WriteEndElement();
                            }
            xmlWriter.WriteEndElement();
            #endregion
            #region 指挥与监视
            xmlWriter.WriteStartElement("指挥与监视");
            for (int i = 1; i <= obj.DirectAndMonitors.Count; i++)
            {
                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                    xmlWriter.WriteStartElement("对应试验ID");
                    xmlWriter.WriteString(obj.DirectAndMonitors[i - 1].SYID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("时间段");
                    xmlWriter.WriteString(obj.DirectAndMonitors[i - 1].DateSection);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("指挥与监视任务");
                    xmlWriter.WriteString(obj.DirectAndMonitors[i - 1].Task);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("实时显示任务");
                    xmlWriter.WriteString(obj.DirectAndMonitors[i - 1].RealTimeShowTask);
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            #endregion
            #region 实时控制
            xmlWriter.WriteStartElement("实时控制");
            for (int i = 1; i <= obj.RealTimeControls.Count; i++)
            {
                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                    xmlWriter.WriteStartElement("工作");
                    xmlWriter.WriteString(obj.RealTimeControls[i - 1].Work);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("对应试验ID");
                    xmlWriter.WriteString(obj.RealTimeControls[i - 1].SYID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("开始时间");
                    xmlWriter.WriteString(obj.RealTimeControls[i - 1].StartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("结束时间");
                    xmlWriter.WriteString(obj.RealTimeControls[i - 1].EndTime);
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            #endregion
            #region 试验评估
            xmlWriter.WriteStartElement("试验评估");
            for (int i = 1; i <= obj.SYEstimates.Count; i++)
            {
                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                    xmlWriter.WriteStartElement("对应试验ID");
                    xmlWriter.WriteString(obj.SYEstimates[i - 1].SYID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("开始时间");
                    xmlWriter.WriteString(obj.SYEstimates[i - 1].StartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("结束时间");
                    xmlWriter.WriteString(obj.SYEstimates[i - 1].EndTime);
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            #endregion
            #region 数据管理
            xmlWriter.WriteStartElement("数据管理");
            for (int i = 1; i <= obj.DataManages.Count; i++)
            {
                xmlWriter.WriteStartElement("工作内容"+i.ToString());

                    xmlWriter.WriteStartElement("对应试验ID");
                    xmlWriter.WriteString(obj.DataManages[i - 1].SYID);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("工作");
                    xmlWriter.WriteString(obj.DataManages[i - 1].Work);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("对应数据描述");
                    xmlWriter.WriteString(obj.DataManages[i - 1].Description);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("开始时间");
                    xmlWriter.WriteString(obj.DataManages[i - 1].StartTime);
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteStartElement("结束时间");
                    xmlWriter.WriteString(obj.DataManages[i - 1].EndTime);
                    xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }
            xmlWriter.WriteEndElement();
            #endregion
            xmlWriter.WriteEndElement();
            #endregion
            xmlWriter.WriteEndElement();
            xmlWriter.Close();
            return FilePath;
        }
        /// <summary>
        /// 仿真推演
        /// </summary>
        public string CreateTYSJFile(TYSJ obj,int type)
        {
            if (type == 0)
            {
                filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("TYSJ", obj.TaskID, obj.SatID);
            }
            if (TestFileName())
            {
                return "EXIST";
            }
            xmlWriter = new XmlTextWriter(FilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;
            xmlWriter.WriteStartDocument();

            xmlWriter.WriteStartElement("仿真推演试验数据");

            xmlWriter.WriteStartElement("SatName");
            xmlWriter.WriteString(obj.SatName);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Type");
            xmlWriter.WriteString(obj.Type);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("TestItem");
            xmlWriter.WriteString(obj.TestItem);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("StartTime");
            xmlWriter.WriteString(obj.StartTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("EndTime");
            xmlWriter.WriteString(obj.EndTime);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("Condition");
            xmlWriter.WriteString(obj.Condition);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndElement();

            xmlWriter.Close();
            return FilePath;
        }
        /// <summary>
        /// 测试要生成的文件文件名是否已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TestYJJHFileName(YJJH obj)
        {
            filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("YJJH", obj.TaskID, obj.SatID);
            return TestFileName();
        }
        /// <summary>
        /// 测试要生成的文件文件名是否已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TestXXXQFileName(XXXQ obj)
        {
            filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("XXXQ", obj.TaskID, obj.SatID);
             return TestFileName();
        }
        /// <summary>
        /// 测试要生成的文件文件名是否已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TestDMJHFileName(DMJH obj)
        { 
            filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("DMJH", obj.TaskID, obj.SatID);
             return TestFileName();
        }
        /// <summary>
        /// 测试要生成的文件文件名是否已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TestZXJHFileName(ZXJH obj)
        { 
            filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("ZXJH", obj.TaskID, obj.SatID);
             return TestFileName();
        }
        /// <summary>
        /// 测试要生成的文件文件名是否已存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool TestTYSJFileName(TYSJ obj)
        { 
             filename = (new FileNameMaker()).GenarateInternalFileNameTypeOne("TYSJ", obj.TaskID, obj.SatID);
             return TestFileName();
        }
        #endregion

        #region <External File>
        /// <summary>
        /// 创建外部应用计划文件
        /// </summary>
        /// <param name="ids">计划ID串</param>
        /// <param name="desValue">信宿地址值</param>
        /// <param name="destinationName">信宿地址名称</param>
        /// <returns>生成的外发文件完整路径名串</returns>
        public string CreateSendingYJJHFile(string ids,string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" +info.EXCODE+ ")";
            OperatingManagement.DataAccessLayer.BusinessManage.InfoType itype = new OperatingManagement.DataAccessLayer.BusinessManage.InfoType();

            string SendFileNames  ="";
            List<JH> listJH = (new JH()).SelectByIDS(ids);
            YJJH obj;
            foreach (JH jh in listJH)
            {
                #region 读取计划XML文件，变量赋值
                obj = new YJJH();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(jh.FileIndex);

                obj.CTime = DateTime.Now;
                XmlNode root = xmlDoc.SelectSingleNode("应用研究工作计划/XXFL");
                obj.XXFL = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/JXH");
                obj.JXH= root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/SysName");
                obj.SysName = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/StartTime");
                obj.StartTime = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/EndTime");
                obj.EndTime = root.InnerText;
                root = xmlDoc.SelectSingleNode("应用研究工作计划/Task");
                obj.Task = root.InnerText;
                #endregion

                #region 写入文件
                filename = FileNameMaker.GenarateFileNameTypeThree("YJJH", desValue);
                SendFileNames = SendFileNames+SendingPath+",";
                itype = itype.GetByExMark("YJJH");

                sw = new StreamWriter(SendingPath);
                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(obj.TaskID));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME + "(" + itype.EXCODE + ")");
                sw.WriteLine("[数据区行数L]：0001");
                sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]：XXFL  JXH  SysName  StartTime  EndTime  Task");
                sw.WriteLine("[数据区]：");
                sw.WriteLine(obj.XXFL + "  " + obj.JXH + "  " + obj.SysName + "  " + obj.StartTime + "  " + obj.EndTime + "  "+obj.Task);
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
                #endregion

            }

            if (SendFileNames[SendFileNames.Length - 1] == ',')
            {
                SendFileNames = SendFileNames.Substring(0,SendFileNames.Length-1);
            }
            return SendFileNames;
        }
        /// <summary>
        /// 生成外发信息需求文件
        /// </summary>
        /// <param name="ids">计划ID串</param>
        /// <param name="desValue">信宿地址值</param>
        /// <param name="destinationName">信宿地址名称</param>
        /// <returns>生成的外发文件完整路径名串</returns>
        public string CreateSendingXXXQFile(string ids, string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" +info.EXCODE+ ")";

            OperatingManagement.DataAccessLayer.BusinessManage.InfoType itype = new OperatingManagement.DataAccessLayer.BusinessManage.InfoType();
           
            string SendFileNames  ="";
            string filenameMBXQ = "";
            string filenameHJXQ = "";
            List<JH> listJH = (new JH()).SelectByIDS(ids);
            XXXQ obj;
            foreach (JH jh in listJH)
            {
                #region 读取计划XML文件，变量赋值
                obj = new XXXQ();
                obj.objMBXQ = new MBXQ();
                obj.objHJXQ = new HJXQ();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(jh.FileIndex);

                obj.CTime = DateTime.Now; //jh.CTime;
                #region 空间目标信息需求
                XmlNode root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/User");
                obj.objMBXQ.User = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/Time");
                obj.objMBXQ.Time = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TargetInfo");
                obj.objMBXQ.TargetInfo = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TimeSection1");
                obj.objMBXQ.TimeSection1 = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/TimeSection2");
                obj.objMBXQ.TimeSection2 = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求/Sum");
                obj.objMBXQ.Sum = root.InnerText;

                root = xmlDoc.SelectSingleNode("空间信息需求/空间目标信息需求");
                List<MBXQSatInfo> list = new List<MBXQSatInfo>();
                MBXQSatInfo sat;
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.Name == "卫星")
                    {
                        sat = new MBXQSatInfo();
                        sat.SatName = n["SatName"].InnerText;
                        sat.InfoName = n["InfoName"].InnerText;
                        sat.InfoTime = n["InfoTime"].InnerText;
                        list.Add(sat);
                    }
                }
                obj.objMBXQ.SatInfos = list;

                #endregion
                #region 空间环境信息需求
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/User");
                obj.objHJXQ.User = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/Time");
                obj.objHJXQ.Time = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/EnvironInfo");
                obj.objHJXQ.EnvironInfo = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/TimeSection1");
                obj.objHJXQ.TimeSection1 = root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/TimeSection2");
                obj.objHJXQ.TimeSection2= root.InnerText;
                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求/Sum");
                obj.objHJXQ.Sum = root.InnerText;

                root = xmlDoc.SelectSingleNode("空间信息需求/空间环境信息需求");
                List<HJXQSatInfo> listhj = new List<HJXQSatInfo>();
                HJXQSatInfo sathj;
                foreach (XmlNode n in root.ChildNodes)
                {
                    if (n.Name == "卫星")
                    {
                        sathj = new HJXQSatInfo();
                        sathj.SatName = n["SatName"].InnerText;
                        sathj.InfoName = n["InfoName"].InnerText;
                        sathj.InfoArea = n["InfoArea"].InnerText;
                        sathj.InfoTime = n["InfoTime"].InnerText;
                        listhj.Add(sathj);
                    }
                }
                obj.objHJXQ.SatInfos = listhj;
                #endregion
                #endregion

                #region 写入文件

                #region MBXQ
                filename = FileNameMaker.GenarateFileNameTypeThree("MBXQ", desValue);
                SendFileNames = SendFileNames + SendingPath + ",";
                filenameMBXQ = filename;    //用于在XXXQ文件里的数据行记录
                itype = itype.GetByExMark("MBXX");

                sw = new StreamWriter(SendingPath);
                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(obj.TaskID));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME+"("+itype.EXCODE+")");
                sw.WriteLine("[数据区行数L]：" + (obj.objMBXQ.SatInfos.Count+1).ToString("0000"));
                sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]：User  Time  TargetInfo  TimeSection1  TimeSection2  Sum");
                sw.WriteLine("[格式标识2]：SatName  InfoName  InfoTime");
                sw.WriteLine("[数据区]：");
                sw.WriteLine(obj.objMBXQ.User + "  " + obj.objMBXQ.Time + "  " + obj.objMBXQ.TargetInfo + "  " + obj.objMBXQ.TimeSection1 + "  " + obj.objMBXQ.TimeSection2 + "  " + obj.objMBXQ.Sum);
                for (int i = 0; i < obj.objMBXQ.SatInfos.Count; i++)
                {
                    sw.WriteLine(obj.objMBXQ.SatInfos[i].SatName + "  " + obj.objMBXQ.SatInfos[i].InfoName + "  " + obj.objMBXQ.SatInfos[i].InfoTime);
                }
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
                #endregion
                #region HJXQ
                filename = FileNameMaker.GenarateFileNameTypeThree("HJXQ", desValue);
                SendFileNames = SendFileNames + SendingPath + ",";
                filenameHJXQ = filename;    //用于在XXXQ文件里的数据行记录
                itype = itype.GetByExMark("HJXX");

                sw = new StreamWriter(SendingPath);
                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(obj.TaskID));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME + "(" + itype.EXCODE + ")");
                sw.WriteLine("[数据区行数L]：" + (obj.objHJXQ.SatInfos.Count + 1).ToString("0000"));
                sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]：User  Time  EnvironInfo  TimeSection1  TimeSection2  Sum");
                sw.WriteLine("[格式标识2]：SatName  InfoName  InfoArea  InfoTime");
                sw.WriteLine("[数据区]：");
                sw.WriteLine(obj.objHJXQ.User + "  " + obj.objHJXQ.Time + "  " + obj.objHJXQ.EnvironInfo + "  " + obj.objHJXQ.TimeSection1 + "  " + obj.objHJXQ.TimeSection2 + "  " + obj.objHJXQ.Sum);
                for (int i = 0; i < obj.objHJXQ.SatInfos.Count; i++)
                {
                    sw.WriteLine(obj.objHJXQ.SatInfos[i].SatName + "  " + obj.objHJXQ.SatInfos[i].InfoName + "  " + obj.objHJXQ.SatInfos[i].InfoArea + "  " + obj.objHJXQ.SatInfos[i].InfoTime);
                }
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
                #endregion
                #region XXXQ
                filename = FileNameMaker.GenarateFileNameTypeThree("XXXQ", desValue);
                SendFileNames = SendFileNames + SendingPath + ",";
                itype = itype.GetByExMark("XXXQ");

                sw = new StreamWriter(SendingPath);
                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(obj.TaskID));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME + "(" + itype.EXCODE + ")");
                sw.WriteLine("[数据区行数L]：0002");
                sw.WriteLine("<符号区>");
                sw.WriteLine("[数据区]：");
                sw.WriteLine(filenameMBXQ);
                sw.WriteLine(filenameHJXQ);
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
                #endregion
                #endregion
            }

            if (SendFileNames[SendFileNames.Length - 1] == ',')
            {
                SendFileNames = SendFileNames.Substring(0, SendFileNames.Length - 1);
            }
            return SendFileNames;
        }
        /// <summary>
        /// 生成外发地面工作计划文件
        /// </summary>
        /// <param name="ids">计划ID串</param>
        /// <param name="desValue">信宿地址值</param>
        /// <param name="destinationName">信宿地址名称</param>
        /// <returns>生成的外发文件完整路径名串</returns>
        public string CreateSendingGZJHFile(string ids, string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" +info.EXCODE+ ")";
           
            string SendFileNames = "";
            List<JH> listJH = (new JH()).SelectByIDS(ids);
            DMJH obj;
            foreach (JH jh in listJH)
            {
                //if (desValue == "XSCC")
                //{
                //    filename = FileNameMaker.GenarateFileNameTypeOne("GZJH", "B");
                //    RenamePlanFile(jh.FileIndex, SendingPath);  //移动并重命名文件到外发目录
                //}
                //else
                //{
                    #region 有问题，暂时空着
                    /*
                    #region 读取计划XML文件，变量赋值
                    obj = new DMJH();
                    List<DMJH_Task> listTask = new List<DMJH_Task>();
                    DMJH_Task task;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(jh.FileIndex);

                    obj.CTime = jh.CTime;
                    XmlNode root = xmlDoc.SelectSingleNode("地面站工作计划/编号");
                    obj.Sequence = root.InnerText;
                    root = xmlDoc.SelectSingleNode("地面站工作计划/时间");
                    obj.DateTime = root.InnerText;
                    root = xmlDoc.SelectSingleNode("地面站工作计划/工作单位");
                    obj.StationName = root.InnerText;
                    root = xmlDoc.SelectSingleNode("地面站工作计划/设备代号");
                    obj.EquipmentID = root.InnerText;
                    root = xmlDoc.SelectSingleNode("地面站工作计划/任务个数");
                    obj.TaskCount = root.InnerText;

                    root = xmlDoc.SelectSingleNode("地面站工作计划");
                    foreach (XmlNode n in root.ChildNodes)
                    {
                        if (n.Name == "任务")
                        {
                            task = new DMJH_Task();
                            task.TaskFlag = n["任务标志"].InnerText;
                            task.WorkWay = n["工作方式"].InnerText;
                            task.PlanPropertiy = n["计划性质"].InnerText;
                            task.WorkMode = n["工作模式"].InnerText;
                            task.PreStartTime = n["任务准备开始时间"].InnerText;
                            task.StartTime = n["任务开始时间"].InnerText;
                            task.TrackStartTime = n["跟踪开始时间"].InnerText;
                            task.WaveOnStartTime = n["开上行载波时间"].InnerText;
                            task.WaveOffStartTime = n["关上行载波时间"].InnerText;
                            task.TrackEndTime = n["跟踪结束时间"].InnerText;
                            task.EndTime = n["任务结束时间"].InnerText;
                            listTask.Add(task);
                        }
                    }
                    #endregion

                    #region 写入文件
                    filename = FileNameMaker.GenarateFileNameTypeThree("GZJH", desValue);

                    sw = new StreamWriter(SendingPath);
                    sw.WriteLine("<说明区>");
                    sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                    sw.WriteLine("[信源S]：" + this.Source);
                    sw.WriteLine("[信宿D]：" + destinationName);
                    sw.WriteLine("[任务代码M]：700任务(0501)");
                    sw.WriteLine("[信息类别B]：地面站工作计划");
                    sw.WriteLine("[数据区行数L]：" + (Convert.ToInt32( obj.TaskCount) +1).ToString("0000") );
                    sw.WriteLine("<符号区>");
                    sw.WriteLine("[格式标识1]：JXH  XXFL  DW  SB	QS");
                    sw.WriteLine("[格式标识2]：QH  DH  FS  JXZ  MS  QB  GXZ  ZHB  RK  GZK  KSHX  GSHX  GZJ  JS  BID  SBZ  RTs  RTe  SL  BID  HBZ  Ts  Te  RTs  SL");
                    sw.WriteLine("[数据区]：");
                    sw.WriteLine(obj.Sequence + "  " + "ZJ" + "  " + obj.StationName + "  " + obj.EquipmentID + "  " + "0001");
                    foreach (DMJH_Task t in obj.DMJHTasks)
                    {
                        sw.WriteLine("0001"+ "  " + t.TaskFlag + "  " + t.WorkWay + "  " + t.PlanPropertiy + "  " + t.WorkMode + "  "
                            + "Q1" + "  " + "M" + "  " + t.PreStartTime + "  " + t.StartTime + "  " +t.TrackStartTime + "  " +t.WaveOnStartTime
                             + "  " +t.WaveOffStartTime + "  " +t.TrackEndTime + "  " +t.EndTime + "  ");
                    }
                    sw.WriteLine("<辅助区>");
                    sw.WriteLine("[备注]：");
                    sw.WriteLine("[结束]：END");

                    sw.Close();
                    #endregion
 */
                    #endregion
                //}

                filename = FileNameMaker.GenarateFileNameTypeOne("GZJH", "B");
                RenamePlanFile(jh.FileIndex, SendingPath);  //移动并重命名文件到外发目录
                SendFileNames = SendFileNames + SendingPath + ",";
            }

            if (SendFileNames[SendFileNames.Length - 1] == ',')
            {
                SendFileNames = SendFileNames.Substring(0,SendFileNames.Length-1);
            }
            return SendFileNames;
        }

        public string CreateSendingZXJHFile(string ids, string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" +info.EXCODE+ ")";
           
            //string SendFileNames = "";
            //List<JH> listJH = (new JH()).SelectByIDS(ids);
            //ZXJH obj;
            //foreach (JH jh in listJH)
            //{
            //    #region 读取计划XML文件，变量赋值
            //    #endregion

            //    #region 写入文件
            //    filename = FileNameMaker.GenarateFileNameTypeThree("ZXJH", desValue);
            //    SendFileNames += SendingPath;

            //    #endregion
            //}

            //return SendFileNames;
            return "";
        }
        /// <summary>
        /// 生成外发推演数据文件
        /// </summary>
        /// <param name="ids">计划ID串</param>
        /// <param name="desValue">信宿地址值</param>
        /// <param name="destinationName">信宿地址名称</param>
        /// <returns>生成的外发文件完整路径名串</returns>
        public string CreateSendingTYSJFile(string ids, string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" +info.EXCODE+ ")";
            OperatingManagement.DataAccessLayer.BusinessManage.InfoType itype = new OperatingManagement.DataAccessLayer.BusinessManage.InfoType();

            string SendFileNames = "";
            List<JH> listJH = (new JH()).SelectByIDS(ids);
            TYSJ obj;
            foreach (JH jh in listJH)
            {
                #region 读取计划XML文件，变量赋值
                obj = new TYSJ();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(jh.FileIndex);

                obj.CTime = DateTime.Now; //jh.CTime;
                XmlNode root = xmlDoc.SelectSingleNode("仿真推演试验数据/SatName");
                obj.SatName = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/Type");
                obj.Type = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/TestItem");
                obj.TestItem = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/StartTime");
                obj.StartTime = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/EndTime");
                obj.EndTime = root.InnerText;
                root = xmlDoc.SelectSingleNode("仿真推演试验数据/Condition");
                obj.Condition = root.InnerText;
                #endregion

                #region 写入文件
                filename = FileNameMaker.GenarateFileNameTypeThree("TYSJ", desValue);
                SendFileNames = SendFileNames + SendingPath + ",";
                itype = itype.GetByExMark("TYSJ");

                sw = new StreamWriter(SendingPath);
                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + obj.CTime.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(obj.TaskID));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME + "(" + itype.EXCODE + ")");
                sw.WriteLine("[数据区行数L]：0001");
                sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]：SatName  Type  TestItem  StartTime  EndTime  Condition");
                sw.WriteLine("[数据区]：");
                sw.WriteLine(obj.SatName + "  " + obj.Type + "  " + obj.TestItem + "  " + obj.StartTime + "  " + obj.EndTime + "  " + obj.Condition);
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
                #endregion
                
            }

            if (SendFileNames[SendFileNames.Length - 1] == ',')
            {
                SendFileNames = SendFileNames.Substring(0, SendFileNames.Length - 1);
            }
            return SendFileNames;
        }

        public string CreateSendingGDGSFile(string ids, string desValue)
        {
            XYXSInfo info = new XYXSInfo();
            info = info.GetByAddrMark(desValue);
            string destinationName = info.ADDRName + info.ADDRMARK + "(" + info.EXCODE + ")";
            OperatingManagement.DataAccessLayer.BusinessManage.InfoType itype = new OperatingManagement.DataAccessLayer.BusinessManage.InfoType();

            string SendFileNames = "";
            List<GD> listJH = (new GD()).SelectByIDS(ids);
            Dictionary<string, int> dicTaskID = new Dictionary<string, int>();
            foreach (GD obj in listJH)
            {
                if (dicTaskID.ContainsKey(obj.TaskID))
                {
                    dicTaskID[obj.TaskID] = dicTaskID[obj.TaskID] + 1;
                }
                else
                {
                    dicTaskID.Add(obj.TaskID, 1);
                    
                }
            }
            //相同任务的轨道数据写进一个文件里
            foreach (string key in dicTaskID.Keys)
            {
                #region 写入文件
                filename = FileNameMaker.GenarateFileNameTypeThree("GDGS", desValue);
                SendFileNames = SendFileNames + SendingPath + ",";
                itype = itype.GetByExMark("GD");
                sw = new StreamWriter(SendingPath);

                sw.WriteLine("<说明区>");
                sw.WriteLine("[生成时间]：" + DateTime.Now.ToString("yyyy-MM-dd-HH:mm"));
                sw.WriteLine("[信源S]：" + this.Source);
                sw.WriteLine("[信宿D]：" + destinationName);
                sw.WriteLine("[任务代码M]：" + (new Task()).GetTaskName(key));
                sw.WriteLine("[信息类别B]：" + itype.DATANAME + "(" + itype.EXCODE + ")");
                sw.WriteLine("[数据区行数L]："+dicTaskID[key].ToString("0000") );
                sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]：T01  T02  a  e  i  Ω  w  M");
                sw.WriteLine("[数据区]：");
                foreach (GD obj in listJH)
                {
                    if (obj.TaskID == key)
                    {
                        sw.WriteLine(obj.Times.ToString("yyyyMMdd") + "  " + obj.Times.ToString("HHmmssffff") + "  "
                            + obj.A + "  " + obj.E + "  " + obj.I + "  " + obj.Q + "  " + obj.W + "  " + obj.M);
                    }
                }
                sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                
                #endregion
            }
            sw.Close();

            if (SendFileNames[SendFileNames.Length - 1] == ',')
            {
                SendFileNames = SendFileNames.Substring(0, SendFileNames.Length - 1);
            }
            return SendFileNames;
        }
        #endregion

        /// <summary>
        /// 重命名计划文件
        /// </summary>
        /// <param name="oldfile"></param>
        /// <param name="newfile"></param>
        public void RenamePlanFile(string oldfile,string newfile)
        {
            FileInfo fi = new FileInfo(oldfile);
            fi.MoveTo(newfile);
        }

        /// <summary>
        /// 判断要新生成要的文件，文件名是否已经存在
        /// </summary>
        /// <returns></returns>
        private bool TestFileName()
        {
            return System.IO.File.Exists(FilePath);
        }

    }
}
