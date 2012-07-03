using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using ServicesKernel.File;
using OperatingManagement.Framework.Core;

namespace ServicesKernel.GDFX
{
    /// <summary>
    /// 轨道分析-交会分析
    /// </summary>
    public class CutAnalyzer
    {
        private string[] fileNames = new string[] { "JPLEPH", "TESTRECL", "WGS84.GEO", "eopc04_IAU2000.dat" };
        private const string dllPath = @"～\GDDLL\CutAna\";
        private string strDllPath;
        //
        //private IntPtr handle;
        private static CutAnalyzer _instance = null;
        private static object _locker = new object();
        private bool isCaculating = false;

        /// <summary>
        /// Gets the instance of <see cref="DFSenderManager"/> class.
        /// </summary>
        public static CutAnalyzer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new CutAnalyzer();
                        }
                    }
                }
                return _instance;
            }
        }

        public bool GetFileBeginDate(string fileFullName, out DateTime beginDate, out int interval)
        {
            beginDate = DateTime.MinValue;
            interval = 0;
            bool blResult = true;
            string strLine = string.Empty;
            DateTime date;

            StreamReader oSReader = new StreamReader(fileFullName);
            oSReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oSReader.ReadLine();
            if (strLine != null)
            {
                blResult = CheckRowData(strLine.Trim(), out date);
                if (!blResult)
                    return blResult;
                beginDate = date;
            }
            else
                return false;//第一行为空返回false

            strLine = oSReader.ReadLine();
            if (strLine != null)
            {
                blResult = CheckRowData(strLine.Trim(), out date);
                if (!blResult)
                    return blResult;
                interval = (date - beginDate).Seconds;
            }
            oSReader.Close();
            return true;
        }

        /// <summary>
        /// 检查文件中数据的合法性
        /// </summary>
        /// <param name="fileFullName">文件全名</param>
        /// <param name="minDate">最小时间</param>
        /// <param name="maxDate">最大时间</param>
        /// <param name="interval">单位毫秒</param>
        /// <returns></returns>
        public string CheckFileData(string fileFullName, out DateTime minDate, out DateTime maxDate, out int interval)
        {
            /*
             * 先读两行，以获取时间间隔和最小时间
             * 校验每行数据合法性
             * 最大时间等于最后一行的时间
             */
            minDate = DateTime.MinValue;
            maxDate = DateTime.MaxValue;
            interval = 0;

            bool blResult = true;
            int iLine = 0;
            string msg = "Row:{0}为空";
            string strLine = string.Empty;
            DateTime date;

            StreamReader oSReader = new StreamReader(fileFullName);
            oSReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oSReader.ReadLine();
            iLine++;
            if (strLine != null)
            {
                blResult = CheckRowData(strLine.Trim(), out date);
                if (!blResult)
                    return string.Format(msg, iLine);
                minDate = date;
            }
            else
                return string.Format("Row:{0}为空", iLine);//第一行为空返回false

            strLine = oSReader.ReadLine();
            iLine++;
            if (strLine != null)
            {
                blResult = CheckRowData(strLine.Trim(), out date);
                if (!blResult)
                    return string.Format(msg, iLine);
                interval = (date - minDate).Milliseconds;
            }
            else
            {
                maxDate = minDate;
                return string.Empty;//第二行为空返回true
            }

            strLine = oSReader.ReadLine();
            while (strLine != null)
            {
                iLine++;
                if (strLine.Trim().Equals(string.Empty))
                    break;

                blResult = CheckRowData(strLine.Trim(), out date);
                if (!blResult)
                    break;
                strLine = oSReader.ReadLine();
            }
            oSReader.Close();
            maxDate = date;
            return string.Format(msg, iLine);
        }

        /// <summary>
        /// 检查每行数据
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private bool CheckRowData(string rowData, out DateTime date)
        {
            date = DateTime.MinValue;
            bool blResult = true;
            string[] datas = new string[0];
            string sTmp = string.Empty;
            //YYYY  MM  DD  HH  MM  SS.SSSSSS x1 y1 z1 vx1 vy1 vz1
            //格式: 2空格，I4，空格，2（I2，空格），2（I2，‘：’），F4.1，2空格，6(F16.6,2空格)
            try
            {
                datas = GetDatas(rowData);
                if (datas.Length != 12)
                    return false;

                //前六个数组成日期型
                sTmp = string.Format("{0}-{1}-{2} {3}:{4}:{5}", datas[0], datas[1], datas[2], datas[3], datas[4], datas[5]);
                blResult = DataValidator.ValidateDate(sTmp, out date);
                if (!blResult)
                    return blResult;

                //后6个数为double
                for (int i = 6; i < datas.Length; i++)
                {
                    blResult = DataValidator.ValidateFloat(datas[i], 16, 6);
                    if (!blResult)
                        return blResult;
                }
            }
            catch (Exception ex)
            {
                date = DateTime.MaxValue;
                return false;
            }
            finally { }
            return blResult;
        }

        /// <summary>
        /// 从行中取数据
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="ym">年月日时分，int[5]</param>
        /// <param name="s">秒，double</param>
        /// <param name="orb">double[6]</param>
        /// <returns></returns>
        private bool GetRowData(string rowData, out int[] ym, out double s, out double[] orb)
        {
            ym = new int[0];
            s = 0;
            orb = new double[0];

            string[] datas = new string[0];
            int iIdx = 0;
            //YYYY  MM  DD  HH  MM  SS.SSSSSS x1 y1 z1 vx1 vy1 vz1
            //格式: 2空格，I4，空格，2（I2，空格），2（I2，‘：’），F4.1，2空格，6(F16.6,2空格)
            try
            {
                datas = GetDatas(rowData);
                ym = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    ym[i] = Convert.ToInt32(datas[i]);
                }
                iIdx = 5;
                s = Convert.ToDouble(datas[iIdx]);

                //后6个数为double
                orb = new double[6];
                for (int i = iIdx; i < datas.Length; i++)
                {
                    orb[i - 6] = Convert.ToDouble(datas[i]);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
            return true;
        }

        /// <summary>
        /// 从行中取日期
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="ym">年月日时分，int[5]</param>
        /// <param name="s">秒，double</param>
        /// <param name="orb">double[6]</param>
        /// <returns></returns>
        private bool GetRowDate(string rowData, out DateTime date)
        {
            date = DateTime.MinValue;

            string[] datas = new string[0];
            string sTmp = string.Empty;
            //YYYY  MM  DD  HH  MM  SS.SSSSSS x1 y1 z1 vx1 vy1 vz1
            //格式: 2空格，I4，空格，2（I2，空格），2（I2，‘：’），F4.1，2空格，6(F16.6,2空格)
            try
            {
                datas = GetDatas(rowData);
                sTmp = string.Format("{0}-{1}-{2} {3}:{4}:{5}", datas[0], datas[1], datas[2], datas[3], datas[4], datas[5]);
                date = DateTime.ParseExact(sTmp, "yyyy-MM-dd HH:mm:ss.f", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                return false;
            }
            finally { }
            return true;
        }

        /// <summary>
        /// 复制数据文件到交会分析程序路径下
        /// </summary>
        /// <param name="srcFileFullName"></param>
        /// <returns></returns>
        public bool CopyDataFile(string srcFileFullName)
        {
            bool blResult = true;
            string strResult = DataFileHandle.CopyFile(srcFileFullName, DllPath + @"Input\", "CutAnalyzeData.dat");
            if (strResult.Equals(string.Empty))
                blResult = true;
            else
                blResult = false;
            return blResult;
        }

        /// <summary>
        /// 判断所有文件是否都存在
        /// </summary>
        /// <returns></returns>
        public string IsAllFileExist()
        {
            string strResult = string.Empty;
            for (int i = 0; i < fileNames.Length; i++)
            {
                if (!DataFileHandle.Exists(DllPath + fileNames[i]))
                {
                    strResult = string.Format("文件{0}不存在。", fileNames[i]);
                    break;
                }
            }
            return strResult;
        }

        public string DoCaculate(string subFileFullName, string targetFileFullName, DateTime beginDate, DateTime endDate, int minInterval
            , bool isSubInterval, bool isSubBeginDate, bool isSubEndDate, out string resultFileName)
        {
            resultFileName = "CutAnyalyze_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            string strResult = string.Empty;
            if (isCaculating)
            {
                strResult = "同一时间只能只能执行单次计算任务，请稍候再计算。"; 
                return strResult;
            }
            isCaculating = true;

            #region 变量声明
            StreamReader oSubSR = new StreamReader(subFileFullName);
            StreamReader oTgtSR = new StreamReader(targetFileFullName);
            StreamWriter oSTWResult = new StreamWriter(this.DllPath + @"Output\" + resultFileName + "_STW.dat");
            StreamWriter oUNWResult = new StreamWriter(this.DllPath + @"Output\" + resultFileName + "_UNW.dat");
            #endregion

            oSubSR.BaseStream.Seek(0, SeekOrigin.Begin);
            oTgtSR.BaseStream.Seek(0, SeekOrigin.Begin);
            if (isSubInterval)
            {
                if (isSubBeginDate)
                    strResult = CaculateTotalFile(oSubSR, oTgtSR, oSTWResult, oUNWResult, minInterval, DateTime.MinValue, true);
                else
                    strResult = CaculateTotalFile(oSubSR, oTgtSR, oSTWResult, oUNWResult, minInterval, beginDate, false);

            }
            else
            {
                if (isSubBeginDate)
                    strResult = CaculateTotalFile(oTgtSR, oSubSR, oSTWResult, oUNWResult, minInterval, beginDate, false);
                else
                    strResult = CaculateTotalFile(oTgtSR, oSubSR, oSTWResult, oUNWResult, minInterval, DateTime.MinValue, true);
            }

            oSubSR.Close();
            oTgtSR.Close();
            oSTWResult.Close();
            oUNWResult.Close();

            isCaculating = false;
            return strResult;
        }
        
        private bool Caculate(int[] YM1, double S1, double[] Orb1, int[] YM2, double S2, double[] Orb2, out int[] YM3, out double S3, out double[] Orb3)
        {
            YM3 = new int[0];
            S3 = 0;
            Orb3 = new double[0];
            if (isCaculating)
                return false;
            isCaculating = true;
            /*
             * do caculate here
            */
            isCaculating = false;
            return true;
        }

        private bool WriteToResult(StreamWriter oSTWResult, StreamWriter oUNWResult, int[] YM3, double S3, double[] Orb3)
        {
            bool blResult = true;
            return blResult;
        }
        /// <summary>
        /// 从行数据获取分割后的结果
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private string[] GetDatas(string rowData)
        {
            string[] datas;
            int iIdx = 0;
            rowData = rowData.Trim();
            iIdx = rowData.IndexOf("  ");
            while (iIdx >= 0)
            {
                //提供的样本数据空格数不一致，干脆把多余的空格换成一个
                rowData = rowData.Replace("  ", " ");
                iIdx = rowData.IndexOf("  ");
            }
            datas = rowData.Split(new char[] { ' ' });
            return datas;
        }

        private string DllPath 
        {
            get 
            {
                if (strDllPath.Equals(string.Empty))
                    strDllPath = GlobalSettings.MapPath(dllPath);
                return strDllPath;
            }
        }

        private string CaculateTotalFile(StreamReader oSrcSR, StreamReader oTgtSR, StreamWriter oSTWResult, StreamWriter oUNWResult
            , int minInterval, DateTime beginDate, bool isRightTurn)
        {
            string strResult = string.Empty;
            int[] YM1;
            double S1;
            double[] Orb1;
            int[] YM2;
            double S2;
            double[] Orb2;
            int[] YM3;
            double S3;
            double[] Orb3;
            string strSubLine = string.Empty;
            string strTgtLine = string.Empty;
            DateTime subDate = DateTime.MinValue;
            DateTime tgtDate = DateTime.MinValue;
            bool blResult = false;
            bool isEnd = false;
            bool isBegin = false;
            string strLastSrcLine = string.Empty;

            strSubLine = oSrcSR.ReadLine();
            if (beginDate == DateTime.MinValue)
                isBegin = true;
            while (strSubLine != null)
            {
                if (strSubLine.Trim().Equals(string.Empty))
                    break;

                strSubLine = strSubLine.Trim();

                //得出源日期
                blResult = GetRowDate(strSubLine, out subDate);
                if (!blResult)
                {
                    strResult = string.Format("获取文件行{0}中的数据日期错误", strSubLine);
                    break;
                }

                if (!isBegin)
                {
                    if (beginDate != DateTime.MinValue)
                    {
                        if (Math.Abs((subDate - beginDate).Milliseconds) < minInterval)
                            isBegin = true;
                    }
                }

                if (isBegin)
                {
                    //得出源数据
                    blResult = GetRowData(strSubLine, out YM1, out S1, out Orb1);
                    if (!blResult)
                    {
                        strResult = string.Format("获取文件行{0}中的数据错误", strSubLine);
                        break;
                    }

                    //得出计算目标的数据
                    strResult = GetToCaculateParam(oTgtSR, minInterval, subDate, out YM2, out S2, out Orb2, out strTgtLine, out isEnd);
                    if (!strResult.Equals(string.Empty))
                        break;

                    //计算
                    if (isRightTurn)
                        blResult = Caculate(YM1, S1, Orb1, YM2, S2, Orb2, out YM3, out S3, out Orb3);
                    else
                        blResult = Caculate(YM2, S2, Orb2, YM1, S1, Orb1, out YM3, out S3, out Orb3);

                    if (!blResult)
                    {
                        strResult = string.Format("计算数据行{0}，{1}错误", strSubLine, strTgtLine);
                        break;
                    }

                    //存储计算结果到文件
                    blResult = WriteToResult(oSTWResult, oUNWResult, YM3, S3, Orb3);
                    if (!blResult)
                    {
                        strResult = "存储计算结果出错";
                        break;
                    }

                    //如果目标是最后一行，终止
                    if (isEnd)
                        break;
                }
                strSubLine = oSrcSR.ReadLine();
            }
            return strResult;
        }

        private string GetToCaculateParam(StreamReader oSR, int minInterval, DateTime compareDate
            , out int[] ym, out double s, out double[] orb, out string line, out bool isEnd)
        {
            ym = new int[0];
            s = 0;
            orb = new double[0];
            line = string.Empty;
            isEnd = false;
            string strLine = string.Empty;
            string strLastLine = string.Empty;
            bool blResult = false;
            string strResult = string.Empty;
            DateTime date;
            TimeSpan ts1 = new TimeSpan();
            TimeSpan ts2;

            strLine = oSR.ReadLine();
            while (strLine != null)
            {
                blResult = GetRowDate(strLine, out date);
                if (!blResult)
                {
                    strResult = string.Format("获取文件行{0}中的数据日期错误", strLine);
                    break;
                }
                ts1 = date - compareDate;
                if (Math.Abs(ts1.Milliseconds) < minInterval)
                {
                    strLastLine = strLine;
                    break;
                }
                strLine = oSR.ReadLine();
            }
            if (!strResult.Equals(string.Empty))
                return strResult;

            if (!strLastLine.Equals(string.Empty))
            {
                strLine = oSR.ReadLine();//再读一行，和上一行的Timespan比较
                if (strLine != null || strLine.Trim().Equals(string.Empty))
                {
                    blResult = GetRowDate(strLine, out date);
                    if (!blResult)//Error
                    {
                        strResult = string.Format("获取文件行{0}中的数据日期错误", strLine);
                        return strResult;
                    }
                    ts2 = date - compareDate;
                    if (Math.Abs(ts2.Milliseconds) < minInterval)
                    {
                        if (ts1.Milliseconds < ts2.Milliseconds)
                            strLine = strLastLine;
                    }
                    else
                        strLine = strLastLine;
                }
                else
                {
                    isEnd = true;
                    strLine = strLastLine;
                }

                blResult = GetRowData(strLine, out ym, out s, out orb);
                if (!blResult)//Error
                {
                    strResult = string.Format("获取文件行{0}中的数据错误", strLine);
                    return strResult;
                }
            }
            line = strLine;
            return strResult;
        }
    }
}
