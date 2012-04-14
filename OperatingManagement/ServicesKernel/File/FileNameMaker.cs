using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ServicesKernel.File
{
    public class FileNameMaker
    {
        private static string seperator = "_";

        #region 外部文件命名
        /// <summary>
        /// 只有外发才需要：版本号_对象标识_信源标识_模式标识_信息类型标识_日期_编号. xml
        /// </summary>
        /// <param name="infotype"></param>
        /// <param name="dateType">日期类型U:UTC日期;B:北京日期</param>
        /// <returns></returns>
        public static string GenarateFileNameTypeOne(string infotype,string dateType)
        {
            //版本号用2个字符表示，本版本命名方法固定为“01”，程序中配置为“01”；
            string ver = System.Configuration.ConfigurationManager.AppSettings["Version"];
            //对象标识用4个字符表示，采用可读性ASCII码字符，本任务固定为“7000”，程序中配置为“7000”；
            string flag = System.Configuration.ConfigurationManager.AppSettings["ObjectCode"];
            //	模式标识用2个字符表示，用来标识文件内信息所对应的运行模式。“OP”代表实战，“TS”代表联试；
            string mode = System.Configuration.ConfigurationManager.AppSettings["RunningMode"];

            if (ver == null || flag == null ||  mode == null)
                return null;

            string DateFlag = "";
            if (dateType == "U")
            {
                DateFlag = "U"+DateTime.UtcNow.ToString("yyyyMMdd");
            }
            else if (dateType == "B")
            {
                DateFlag = "B" + DateTime.Now.ToString("yyyyMMdd");
            }
            return GetFileName(infotype, "", DateFlag, 1, ver, flag, mode);
        }

        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="infoCode"></param>
        /// <param name="fromMark"></param>
        /// <param name="toMark"></param>
        /// <param name="time"></param>
        /// <param name="nameWay">1,2,3分别代表命名方式一二三</param>
        /// <param name="version"></param>
        /// <param name="objectCode"></param>
        /// <param name="runningMode"></param>
        /// <returns></returns>
        private static string GetFileName(string infoCode, string toMark, string time
            , int nameWay, string version, string objectCode, string runningMode)
        {
            //中心编码
            string fromMark = System.Configuration.ConfigurationManager.AppSettings["ZXBM"];
            //扩展名
            string sSuffix = FileExchangeConfig.GetSuffixForSending(infoCode, fromMark, toMark);
            if (sSuffix == null)
                return null;
            else
            {
                switch (nameWay)
                {
                    case 1:
                        //版本号_对象标识_信源标识_模式标识_信息类型标识_日期_编号. xml
                        string sequence = GetSequenceID(infoCode);
                        return version + seperator + objectCode + seperator + fromMark + seperator + runningMode +
                            seperator + infoCode + seperator + time + sequence + ".xml";
                    case 2:
                        return "";
                    case 3:
                        //信息名称_信息时间_发送方标识_接收方标识.扩展名
                        return infoCode + seperator + time + seperator + fromMark + seperator + toMark + "." + sSuffix;
                    default:
                        return null;
                }
            }
        }

        public static string GenarateFileNameTypeTwo(string type, string source, string target, string coordinate, int sequence)
        {
            string fileName = "";
            string extName = "";
            if (type.ToUpper()=="CODE")
            {
                extName = ".COD"; 
                fileName = source+DateTime.Now.ToString("yyyyMMdd")+sequence.ToString("00")+extName;
            }
            else if (type.ToUpper()=="JMXL")
            {
                extName = ".EPH";
                fileName = source+target+DateTime.Now.ToString("yyyyMMdd")+sequence.ToString("00")+coordinate+extName;
            }
            
            return fileName;
        }

        /// <summary>
        /// 外发时才需要，信息名称_信息时间_发送方标识_接收方标识.扩展名
        /// </summary>
        /// <param name="infotype"></param>
        /// <param name="source"></param>
        /// <param name="des"></param>
        /// <returns></returns>
        public static string GenarateFileNameTypeThree(string infotype, string des)
        {
            string DateFlag = DateTime.Now.ToString("yyyyMMddHHmm");
            return GetFileName(infotype, des, DateFlag, 3, "", "", "");
        }

        /// <summary>
        /// 获取当日某类信息的流水号0000-9999
        /// sequence file: <sequence><info><name></name><value></value></info><info><name></name><value></value></info></sequence>>
        /// </summary>
        /// <param name="infoCode"></param>
        /// <returns></returns>
        private static string GetSequenceID(string infoCode)
        {
            string strSeqPath = @"../app_data/sequence.xml";
            XDocument doc = XDocument.Load(strSeqPath);
            var infos = doc.Elements("info");
            return "";
        }
        #endregion

        #region 内部文件
        public string GenarateInternalFileNameTypeOne(string infotype,string taskid,string satid)
        {
            string filename = "";
            filename = "GL_" + infotype.ToUpper() + "_" + taskid.ToUpper() + "_" + satid.ToUpper() + "_" + DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";
            return filename;
        }
        #endregion

    }
}
