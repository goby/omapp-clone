using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesKernel.File
{
    public class FileNameMaker
    {
        #region 外部文件命名
        public string GenarateFileNameTypeOne(string infotype,string dateType,int sequence)
        {
            //版本号_对象标识_信源标识_模式标识_信息类型标识_日期_编号. xml
            string fileName="";
            string extName = ".xml";
            string ver = "01";
            string flag = "7000";
            string source = GetSourceForTypeOne(infotype);
            string mode = ""; //TS  ,OP
            mode = System.Configuration.ConfigurationManager.AppSettings["FileNameMode"];
            string Infotype = infotype;
            string DateFlag = "";
            if (dateType == "U")
            {
                DateFlag = "U"+DateTime.UtcNow.ToString("yyyyMMdd");
            }
            else if (dateType == "B")
            {
                DateFlag = "B" + DateTime.Now.ToString("yyyyMMdd");
            }
            string sequencenumber = sequence.ToString("0000");

            fileName = ver + flag + source + mode + Infotype + DateFlag + sequencenumber+extName;
            return fileName;
        }

        private string GetSourceForTypeOne(string infotype)
        {
            string result = "";
            switch (infotype)
            { 
                case "SYJH":
                case "GZJH":
                case "YJJH":
                    result = "YKZX";
                    break;
                case "SBJH":
                    result = "XSCC";
                    break;
            }
            return result;
        }

        public string GenarateFileNameTypeTwo(string type,string source,string target,string coordinate,int sequence)
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

        public string GenarateFileNameTypeThree(string infotype,string sou,string des,string ext)
        {
            //信息名称_信息时间_发送方标识_接收方标识.扩展名
            string fileName = "";
            string extName = ext;
            string DateFlag = DateTime.Now.ToString("yyyyMMddHHmm");
            string source = sou;
            string destination = des;

            fileName = infotype + DateFlag + source + destination + ext;
            return fileName;
        }

        private string GetSourceForTypeThree(string infotype)
        {
            string result = "";
            switch (infotype)
            {
                case "GDGS":
                case "MBXQ":
                case "HJXQ":
                case "TYSJ":
                case "GCSJ":
                case "GCZT":
                case "PGEO":
                case "GLEO":
                case "PLEO":
                case "JDSJ":
                case "JDZT":
                case "JDCL": 
                    result = "YKZX";
                    break;
                case "GCJG":
                case "TWDW":
                case "XLSJ":
                case "TXSJ":
                //case "PLEO":
                    result = "GCYJ";
                    break;
                case "CZJG":
                case "MXCS":
                    result = "CZYJ";
                    break;
                case "JDJG":
                case "YDSJ":
                //case "JDZT":
                //case "JDCL":
                //case "MXCS":
                //case "TXSJ":
                    result = "JDYJ";
                    break;
                case "TYJG":
                //case "GCZT":
                    //case "JDZT":
                    //case "JDCL":
                    //case "MXCS":
                    //case "TXSJ":
                    result = "FZTY";
                    break;
                case "HJXX":
                case "YJBG":
                case "JHBG":
                    result = "XXZX";
                    break;

            }
            return result;
        }

        private string GetDestinationForTypeThree(string infotype)
        {
            string result = "";
            switch (infotype)
            {
                case "SYXQ":
                case "GCJG":
                case "TWDW":
                case "XLSJ":
                case "TXSJ":
                case "CZJG":
                case "MXCS":
                case "JDJG":
                case "YDSJ":
                case "TYJG":
                case "HJXX":
                case "YJBG":
                case "JHBG":
                    result = "YKZX";
                    break;
                case "MBXQ":
                case "HJXQ":
                    result = "XXZX";
                    break;
                case "TYSJ":
                    result = "XXZX";
                    break;
                case "GCSJ":
                case "GCZT":
                case "PGEO":
                case "GLEO":
                    result = "GCYJ";
                    break;
            }
            return result;
        }

        private string GetExtNameForTypeThree(string infotype)
        {
            string result = "";
            switch (infotype)
            {
                case "SYXQ":
                case "MBXQ":
                case "HJXQ":
                    result = "REQ";
                    break;
                case "GDGS":
                    result = "ORB";
                    break;
               // case "TYSJ":
                case "GCSJ":
                case "JDSJ":
                case "GCJG":
                case "CZJG":
                case "JDJG":
                case "TYJG":
                case "HJXX":
                    result = "DAT";
                    break;
                case "GCZT":
                case "JDZT":
                    result = "TM";
                    break;
                case "PLEO":
                case "PGEO":
                case "GLEO":
                //case "TXSJ":
                    result = "RAW";
                    break;
                case "JDCL": 
                    result = "RNG";
                    break;
                case "TWDW":
                    result = "GTW";
                    break;
                case "XLSJ":
                    result = "EPH";
                    break;
                case "TXSJ":
                    result = "SPE";
                    break;
                case "MXCS":
                    result = "MOD";
                    break;
                case "YDSJ":
                    result = "YDSJ";
                    break;
                case "YJBG":
                case "JHBG":
                    result = "REP";
                    break;
            }
            return result;
        }
        #endregion

        #region 内部文件
        public string GenarateInternalFileNameTypeOne(string infotype,string taskid,string satid)
        {
            string filename = "";
            filename = "GL_" + infotype.ToUpper() + taskid.ToUpper() + satid.ToUpper() + DateTime.Now.ToString("yyyyMMddHHmm") + ".xml";
            return filename;
        }
        #endregion

    }
}
