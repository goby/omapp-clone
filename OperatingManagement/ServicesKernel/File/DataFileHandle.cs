using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OperatingManagement.ServicesKernel.File
{
    public class DataFileHandle
    {
        #region -Properties-
        StreamReader sr;
        StreamWriter sw;
        string _filepath = null;
        string _savepath = null;

        /// <summary>
        /// 文件读取路径
        /// </summary>
        public string FilePath
        {
            get { return _filepath; }
            set { _filepath = value; }
        }

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string SavePath
        {
            get { return _savepath; }
            set { _savepath = value; }
        }
        #endregion


        #region -Public Methods-
        public DataFileHandle(string filepath)
        {
            FilePath = filepath;
        }

        /// <summary>
        /// 检测文件是否满足格式要求
        /// </summary>
        public void ReadFile()
        {
            if (String.IsNullOrEmpty(_filepath))
            { return; }

            sr = new StreamReader(FilePath);
            string nextline = null;
            int  validatePartResult = 0; //验证结果,四部分(说明区，符号区，数据区，辅助区)组成，结果为1111，说明完整
            bool validateContentResult = true; //每部分内容的验证结果

            sr.BaseStream.Seek(0,SeekOrigin.Begin); 
            nextline = sr.ReadLine();
            while (nextline != null)
            {
                if (validateContentResult == false)
                {
                    break; //有错误直接退出循环
                }

                string strlineText = nextline.Trim();
                switch (strlineText)
                {
                    case "<说明区>":
                        validatePartResult = validatePartResult +1;
                        validateContentResult = validateDate(1, sr);
                        break;
                    case "<符号区>":
                        validatePartResult = validatePartResult + 10;
                        validateContentResult = validateDate(2, sr);
                        break;
                    case "<数据区>":
                        validatePartResult = validatePartResult + 100;
                        validateContentResult = validateDate(3, sr);
                        break;
                    case "<辅助区>":
                        validatePartResult = validatePartResult + 1000;
                        validateContentResult = validateDate(4, sr);
                        break;
                }
                nextline = sr.ReadLine();
            }
            sr.Close();
        }

        /// <summary>
        /// 生成计划文件:应用研究工作计划、空间信息需求、地面站工作计划、中心运行计划、仿真推演试验数据
        /// </summary>
        public void WriteFile(string type)
        {
            if (String.IsNullOrEmpty(_savepath))
            { return; }

            //switch (type)
            //{ 
            //    case
            //}

        }

        public void GetDataFileBaseInfo(out string ctime,out string source, out string destination,out string taskid,
            out string infotype,out string linecount)
        {
            ctime = ""; source = ""; destination = ""; taskid = ""; infotype = ""; linecount = "";
            if (String.IsNullOrEmpty(_filepath))
            { return; }

            sr = new StreamReader(FilePath);
            string nextline = null;

            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            nextline = sr.ReadLine();
            while (nextline != null)
            {
                if (nextline.Trim() == "<说明区>")
                {
                    nextline = sr.ReadLine();
                    ctime = nextline.Replace("[生成时间]：", "").Trim();
                    nextline = sr.ReadLine();
                    source = nextline.Replace("[信源S]：", "").Trim();
                    nextline = sr.ReadLine();
                    destination = nextline.Replace("[信宿D]：", "").Trim();
                    nextline = sr.ReadLine();
                    taskid = nextline.Replace("[任务代码M]：", "").Trim();
                    nextline = sr.ReadLine();
                    infotype = nextline.Replace("[信息类别B]：", "").Trim();
                    nextline = sr.ReadLine();
                    linecount = nextline.Replace("[数据区行数L]：", "").Trim();
                    break;
                }
            }

            sr.Close();
        }

        #endregion

        #region -Private Methods-
        /// <summary>
        /// type: 1,说明区;2,符号区;3,数据区;4,辅助区;
        /// </summary>
        /// <param name="type"></param>
        bool validateDate(int type, StreamReader reader)
        {
            string strLine = null;
            string strLineText = null;
            bool result = true;
            switch (type)
            {
                case 1:
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[生成时间]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[信源S]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[信宿D]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[任务代码M]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[信息类别B]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[数据区行数L]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    break;
                case 2:
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[格式标识1]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[格式标识2]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    break;
                case 3:
                    break;
                case 4:
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[备注]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    strLine = reader.ReadLine();
                    strLineText = strLine.Replace("[结束]", "");
                    if (strLine.Length == strLineText.Length)
                    { result = false; }
                    break;
            }

            return result;
        }

        #endregion

    }
}
