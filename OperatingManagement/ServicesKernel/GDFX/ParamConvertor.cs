using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Globalization;
using ServicesKernel.File;
using OperatingManagement.Framework.Core;

namespace ServicesKernel.GDFX
{
    /// <summary>
    /// 轨道分析-参数转换
    /// </summary>
    public class ParamConvertor
    {
        private const string dllPath = @"\GDDLL\Convert\";
        private const string outputPath = @"output\";
        private string strDllPath;
        //
        //private IntPtr handle;
        private static ParamConvertor _instance = null;
        private static object _locker = new object();
        private bool isCaculating = false;

        /// <summary>
        /// 使用唯一实例是为了控制同一时间只能有一个计算在进行
        /// </summary>
        public static ParamConvertor Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_locker)
                    {
                        if (_instance == null)
                        {
                            _instance = new ParamConvertor();
                        }
                    }
                }
                return _instance;
            }
        }

        public ParamConvertor()
        {
            strDllPath = string.Empty;

        }

        private string DllPath
        {
            get
            {
                if (strDllPath.Equals(string.Empty))
                    strDllPath = GlobalSettings.MapPath("~" + dllPath);
                return strDllPath;
            }
        }

        /// <summary>
        /// 检查发射系文件合法性
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CheckEmitFileData(string fileName)
        {
            bool blResult = false;
            int iLine = 1;
            string strLine = string.Empty;
            StreamReader oSReader = new StreamReader(fileName);
            oSReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oSReader.ReadLine();
            while (strLine != null)
            {
                blResult = CheckEmitRowData(strLine);
                if (!blResult)
                {
                    oSReader.Close();
                    return string.Format("发射系文件数据行{0}格式不合法", iLine);
                }
                iLine++;
                strLine = oSReader.ReadLine();
            }
            oSReader.Close();
            return string.Empty;
        }

        /// <summary>
        /// 检查发射系文件单行数据合法性
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private bool CheckEmitRowData(string rowData)
        {
            rowData = rowData.Trim();
            if (rowData.Equals(string.Empty))
                return false;

            string sTmp = string.Empty;
            int iIdx = 0;
            string[] datas = new string[0];
            bool blResult = false;
            DateTime date;
            datas = DataValidator.SplitRowDatas(rowData);//H  Lamda  Fai  A  YYYY  MM  DD  HH  MM  SS.SSSSSS
            if (datas.Length != 10)
                return blResult;
            iIdx = 4;
            for (int i = 0; i < iIdx; i++)
            {
                blResult = DataValidator.ValidateFloat(datas[i], 16, 6);
                if (!blResult)
                    return blResult;
            }

            datas[iIdx + 5] = datas[iIdx + 5].Substring(0, datas[iIdx + 5].IndexOf('.') + 4);
            if (datas[iIdx + 5].Length != 6)
                datas[iIdx + 5] = "0" + datas[iIdx + 5];
            sTmp = string.Format("{0}{1}{2} {3}{4}{5}", datas[iIdx], datas[iIdx + 1].PadLeft(2, '0'), datas[iIdx + 2].PadLeft(2, '0')
                    , datas[iIdx + 3].PadLeft(2, '0'), datas[iIdx + 4].PadLeft(2, '0'), datas[iIdx + 5]);
            blResult = DataValidator.ValidateDate(sTmp, out date);
            return blResult;
        }

        /// <summary>
        /// 检查待转换文件数据合法性
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string CheckConvertFileData(string fileName)
        {
            bool blResult = false;
            int iLine = 1;
            string strLine = string.Empty;
            StreamReader oSReader = new StreamReader(fileName);
            oSReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oSReader.ReadLine();
            while (strLine != null)
            {
                blResult = CheckConvertRowData(strLine);
                if (!blResult)
                {
                    oSReader.Close();
                    return string.Format("待转换文件数据行{0}格式不合法", iLine);
                }
                iLine++;
                strLine = oSReader.ReadLine();
            }
            oSReader.Close();
            return string.Empty;
        }

        /// <summary>
        /// 检查待转换文件单行数据合法性
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        private bool CheckConvertRowData(string rowData)
        {
            rowData = rowData.Trim();
            if (rowData.Equals(string.Empty))
                return false;

            string sTmp = string.Empty;
            int iIdx = 0;
            string[] datas = new string[0];
            bool blResult = false;
            DateTime date;
            datas = DataValidator.SplitRowDatas(rowData);//YYYY  MM  DD  HH  MM  SS.SSSSSS  D1 D2 D3 D4 D5 D6
            if (datas.Length != 12)
                return blResult;
            iIdx = 0;

            datas[iIdx + 5] = datas[iIdx + 5].Substring(0, datas[iIdx + 5].IndexOf('.') + 4);
            if (datas[iIdx + 5].Length != 6)
                datas[iIdx + 5] = "0" + datas[iIdx + 5];
            sTmp = string.Format("{0}{1}{2} {3}{4}{5}", datas[iIdx], datas[iIdx + 1].PadLeft(2, '0'), datas[iIdx + 2].PadLeft(2, '0')
                    , datas[iIdx + 3].PadLeft(2, '0'), datas[iIdx + 4].PadLeft(2, '0'), datas[iIdx + 5]);
            blResult = DataValidator.ValidateDate(sTmp, out date);
            if (!blResult)
                return blResult;
            iIdx = 6;
            for (int i = iIdx; i < datas.Length; i++)
            {
                blResult = DataValidator.ValidateFloat(datas[i], 16, 6);
                if (!blResult)
                    break;
            }

            return blResult;
        }

        /// <summary>
        /// 开始计算
        /// </summary>
        /// <param name="deg"></param>
        /// <param name="km"></param>
        /// <param name="convertType"></param>
        /// <param name="convertFileName"></param>
        /// <param name="emitFileName"></param>
        /// <param name="resultFile"></param>
        /// <returns></returns>
        public string DoConvert(bool deg, bool km, string convertType, string convertFileFullName, string emitFileFullName, out string resultFile)
        {
            resultFile = string.Empty;
            string strResult = string.Empty;

            if (isCaculating)
            {
                strResult = "同一时间只能只能执行单次计算任务，请稍候再计算。";
                return strResult;
            }

            isCaculating = true;
            bool blResult = false;
            int iCvtType = System.Convert.ToInt32(convertType);
            int iLine = 1;
            string strLine = string.Empty;
            string strTmp = string.Empty;
            string strResults = string.Empty;
            double[] result = new double[0];
            //取得发射系文件路径的int类型数组
            int[] iPath = new int[0];
            int[] iName = new int[0];
            if (convertType.IndexOf("8") >= 0 || convertType.IndexOf("9") >= 0)
            {
                iLine = emitFileFullName.LastIndexOf(@"\") + 1;
                iPath = DataValidator.GetIntPath(emitFileFullName.Substring(0, iLine));
                iName = DataValidator.GetIntPath(emitFileFullName.Substring(iLine));
            }

            resultFile = this.DllPath + outputPath + "CvtResult_" + DateTime.Now.ToString("yyyyMMdd HHmmss.dat");
            StreamWriter oSWriter = new StreamWriter(resultFile);
            StreamReader oSReader = new StreamReader(emitFileFullName);
            oSReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oSReader.ReadLine();
            while (strLine != null)
            {
                strLine = strLine.Trim();
                if (!strLine.Equals(string.Empty))
                {
                    strResult = ConvertRow(deg, km, iCvtType, iName, iPath, strLine, out result);
                    if (!strResult.Equals(string.Empty))
                    {
                        strResult = string.Format("第{0}行，{1}", iLine, strResult);
                        break;
                    }
                    strTmp = strLine.Substring(0, strLine.IndexOf('.') + 7);
                    blResult = WriteToResult(oSWriter, strTmp, result);
                    if (!blResult)
                    {
                        strResult = string.Format("保存第{0}行计算结果出错", iLine);
                        break;
                    }
                }
                iLine++;
                strLine = oSReader.ReadLine();
            }
            oSReader.Close();
            oSWriter.Close();
            isCaculating = false;
            return strResult;
        }


        /// <summary>
        /// 从行中取数据
        /// </summary>
        /// <param name="rowData"></param>
        /// <param name="ym">年月日时分，int[5]</param>
        /// <param name="s">秒，double</param>
        /// <param name="orb">double[6]</param>
        /// <returns></returns>
        private bool GetRowData(string rowData, out int[] ym, out double s, out double[] da)    
        {
            ym = new int[0];
            s = 0;
            da = new double[0];

            string[] datas = new string[0];
            int iIdx = 0;
            //YYYY  MM  DD  HH  MM  SS.SSSSSS d1 d2 d3 d4 d5 d6
            //格式: 2空格，I4，空格，2（I2，空格），2（I2，‘：’），F4.1，2空格，6(F16.6,2空格)
            try
            {
                datas = DataValidator.SplitRowDatas(rowData);
                ym = new int[5];
                for (int i = 0; i < 5; i++)
                {
                    ym[i] = System.Convert.ToInt32(datas[i]);
                }
                iIdx = 5;
                s = System.Convert.ToDouble(datas[iIdx]);

                //后6个数为double
                iIdx++;
                da = new double[6];
                for (int i = iIdx; i < datas.Length; i++)
                {
                    da[i - 6] = System.Convert.ToDouble(datas[i]);
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
        /// 单行数据进行转换
        /// </summary>
        /// <param name="deg"></param>
        /// <param name="km"></param>
        /// <param name="convertType"></param>
        /// <param name="emitFilePath"></param>
        /// <param name="line"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public string ConvertRow(bool deg, bool km, int convertType, int[] emitFileName, int[] emitFilePath, string line, out double[] result)
        {
            result = new double[0];
            string strResult = string.Empty;
            int[] ym;
            double s;
            double[] da;
            bool blResult = false;
            blResult = GetRowData(line, out ym, out s, out da);
            if (!blResult)
            {
                strResult = string.Format("获取行数据{0}出错", line) ;
                return strResult;
            }
            blResult = ParamConvert(deg, km, convertType, ym, s, da, emitFileName, emitFilePath, out result);
            if (!blResult)
                strResult = string.Format("计算行数据{0}出错", line);
            return strResult;
        }

        public bool ParamConvert(bool deg, bool km, int convertType, int[] YM, double s, double[] da, int[] emitFileName, int[] emitFilePath, out double[] result)
        {
            result = new double[0];
            bool blResult = true;
            int kz = 0;
            Convert(deg, km, convertType, kz, YM, s, da, result, ref emitFileName[0], emitFileName.Length, ref emitFilePath[0], emitFilePath.Length);
            return blResult;
        }

        /// <summary>
        /// 把计算结果写入文件
        /// </summary>
        /// <param name="oSTWResult"></param>
        /// <param name="YM1"></param>
        /// <param name="S1"></param>
        /// <param name="Orb1"></param>
        /// <param name="YM2"></param>
        /// <param name="S2"></param>
        /// <param name="Orb2"></param>
        /// <returns></returns>
        private bool WriteToResult(StreamWriter oSTWResult, string yms, double[] da)
        {
            bool blResult = true;
            StringBuilder sb = new StringBuilder();
            sb.Append("  ");
            sb.Append(yms);

            for (int i = 0; i < da.Length; i++)
            {
                sb.Append("  ");
                sb.Append(da[i].ToString("0000000000.000000").TrimStart(new char[]{'0'}));
            }
            try
            {
                oSTWResult.WriteLine(sb.ToString());
            }
            catch (Exception ex)
            {
                blResult = false;
            }
            return blResult;
        }

        /// <summary>
        /// Fortran 调用
        /// </summary>
        /// <param name="dirIn"></param>
        /// <param name="Ndir"></param>
        [DllImport(dllPath + @"EleCvtDLL.dll",
            SetLastError = true, CharSet = CharSet.Ansi,
            CallingConvention = CallingConvention.StdCall)]
        public static extern void Convert(bool deg, bool km, int KCvt, int Kz, int[] ym, double s, double[] da, double[] result
            , ref int emitFile, int nfile, ref int dir, int ndir);
    }
}
