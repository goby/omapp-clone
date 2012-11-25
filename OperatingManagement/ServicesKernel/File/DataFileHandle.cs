using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace ServicesKernel.File
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


        /// <summary>
        /// 创建文件格式一的文件
        /// </summary>
        /// <param name="oFileInfo"></param>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public FileCreateResult CreateFormat1File(FileBaseInfo oFileInfo, string xmlData)
        {
            if (oFileInfo == null || oFileInfo.FullName == string.Empty)
                return FileCreateResult.LackFileInfo;

            if (System.IO.File.Exists(oFileInfo.FullName))
                return FileCreateResult.FileExisted;

            StreamWriter oSW = new StreamWriter(oFileInfo.FullName);
            oSW.Write(xmlData);
            oSW.Close();
            return FileCreateResult.CreateSuccess;
        }
        /// <summary>
        /// 创建文件格式三的文件
        /// </summary>
        /// <param name="oFileInfo"></param>
        /// <param name="fields"></param>
        /// <param name="datas">数组中的每个表示每行数据</param>
        /// <returns></returns>
        public FileCreateResult CreateFormat3File(FileBaseInfo oFileInfo, string[] fields, string[] datas)
        {
            if (oFileInfo == null || oFileInfo.FullName == string.Empty)
                return FileCreateResult.LackFileInfo;

            if (System.IO.File.Exists(oFileInfo.FullName))
                DeleteFile(oFileInfo.FullName);

            StreamWriter oSW = new StreamWriter(oFileInfo.FullName);
            oSW.WriteLine("<说明区>");
            oSW.WriteLine("[生成时间]：" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm"));
            oSW.WriteLine("[信源S]：" + oFileInfo.From);
            oSW.WriteLine("[信宿D]：" + oFileInfo.To);
            oSW.WriteLine("[任务代码M]：" + oFileInfo.TaskID);
            oSW.WriteLine("[信息类别B]：" + oFileInfo.InfoTypeName);
            oSW.WriteLine("[数据区行数L]：" + oFileInfo.LineCount);
            oSW.WriteLine("<符号区>");
            for (int i = 0; i < fields.Length; i++)
            {
                oSW.WriteLine("[格式标识" + i.ToString() + "]：" + fields[i]);
            }
            oSW.WriteLine("[数据区]：");
            for (int j = 0; j < datas.Length; j++)
            {
                oSW.WriteLine(datas[j]);
            }
            oSW.WriteLine("<辅助区>");
            oSW.WriteLine("[备注]：");
            oSW.WriteLine("[结束]：END");

            oSW.Close();
            return FileCreateResult.CreateSuccess;
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

        /// <summary>
        /// 归档文件，将文件移动至归档目录
        /// </summary>
        /// <param name="srcFile">返回空是成功</param>
        /// <param name="targetPath"></param>
        public static string MoveFile(FileInfo srcFile, string targetPath)
        {
            string strTgtFile = targetPath + @"\" + srcFile.Name;
            string strLog = "归档";
            if (!srcFile.Exists)
            {
                strLog = strLog + string.Format("失败，源文件不存在{0}", srcFile.FullName);
                return strLog;
            }
            try
            {
                //归档路径不存在，创建
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                //归档文件不存在，移动，若存在应该怎么办？
                if (!Directory.Exists(strTgtFile))
                {
                    srcFile.MoveTo(strTgtFile);
                    strLog = "";
                }
                else
                    strLog = strLog + "失败，目标文件已存在";
            }
            catch (Exception ex)
            {
                strLog = strLog + "异常：" + "SourceObject--" + ex.Source + ";Error--" + ex.Message;
            }
            finally
            {
            }
            return strLog;
        }

        /// <summary>
        /// 从源路径复制到目标路径
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="targetPath"></param>
        /// <param name="fileName">为空则保留源文件名，否则替换为此名</param>
        /// <returns></returns>
        public static string CopyFile(string fileFullName, string targetPath, string fileName)
        {
            string strLog = "复制";
            FileInfo oFile = new FileInfo(fileFullName);
            if (!oFile.Exists)
            {
                strLog = strLog + string.Format("失败，源文件不存在{0}", fileFullName);
                return strLog;
            }
            string strTgtFile;
            if (!fileName.Equals(string.Empty))
                strTgtFile = targetPath + fileName;
            else
                strTgtFile = targetPath + @"\" + oFile.Name;
            try
            {
                //复制路径不存在，创建
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);

                //制文件不存在，移动，若存在应该怎么办？
                if (!System.IO.File.Exists(strTgtFile))
                {
                    oFile.CopyTo(strTgtFile);
                    strLog = "";
                }
                else
                    strLog = string.Format("{0}失败，目标文件已存在，源路径{1}，目标路径{2}", strLog, fileFullName, strTgtFile);
            }
            catch (Exception ex)
            {
                strLog = strLog + "异常：" + "SourceObject--" + ex.Source + ";Error--" + ex.Message;
            }
            finally
            {
            }
            return strLog;
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="srcPath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        public static string MoveFile(string srcPath, string targetPath)
        {
            FileInfo oFile = new FileInfo(srcPath);
            if (oFile != null)
                return MoveFile(oFile, targetPath);
            else
                return "源路径不存在";
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string DeleteFile(string fileFullName)
        {
            string strLog = "删除";
            FileInfo oFile = new FileInfo(fileFullName);
            if (!oFile.Exists)
                strLog = strLog + string.Format("失败，源文件不存在{0}", fileFullName);
            else
            {
                oFile.Delete();
                strLog = "";
            }
            return strLog;
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static bool Exists(string fileFullName)
        {
            if (System.IO.File.Exists(fileFullName))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 获取文件名称
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFileName(string fileFullName)
        {
            if (!string.IsNullOrEmpty(fileFullName))
                return fileFullName.Substring(fileFullName.LastIndexOf('\\') + 1);
            else
                return null;
        }

        /// <summary>
        /// 获取文件路径
        /// </summary>
        /// <param name="fileFullName"></param>
        /// <returns></returns>
        public static string GetFilePath(string fileFullName)
        {
            if (!string.IsNullOrEmpty(fileFullName))
                return fileFullName.Substring(0, fileFullName.LastIndexOf('\\'));
            else
                return null;
        }

        /// <summary>
        /// 写数据到文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        /// <param name="dataLen"></param>
        public static void WriteDataToFile(string filename, byte[] data, int dataLen)
        {
            ReaderWriterLock rwl = new ReaderWriterLock();
            FileStream fs = null;
            try
            {
                rwl.AcquireWriterLock(1000);
                try
                {
                    if (System.IO.File.Exists(filename))
                    {
                        fs = new FileStream(filename, FileMode.Append, FileAccess.Write);
                    }
                    else
                    {
                        fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
                    }
                    fs.Write(data, 0, dataLen);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    if (fs != null)
                    {
                        fs.Dispose();
                        fs.Close();
                    }
                    rwl.ReleaseWriterLock();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
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


    public enum FileCreateResult
    {
        CreateSuccess = 0,
        FileExisted = 1,
        LackFileInfo = 2,
        SomethingError = 3
    }

    public class FileBaseInfo
    {
        /// <summary>
        /// File GenerateTime
        /// </summary>
        public DateTime CTime { get; set; }
        /// <summary>
        /// Format:tb_xyxsinfo.AddrName+AddrMark+(Excode)
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// Format:tb_xyxsinfo.AddrName+AddrMark+(Excode)
        /// </summary>
        public string To { get; set; }
        /// <summary>
        /// Format:Name(Code)
        /// </summary>
        public string TaskID { get; set; }
        /// <summary>
        /// Format:tb_InfoType.DataName+(Excode)
        /// </summary>
        public string InfoTypeName { get; set; }
        public int LineCount { get; set; }
        /// <summary>
        /// File Path + File Name
        /// </summary>
        public string FullName { get; set; }

        public FileBaseInfo()
        {
        }
    }
}
