using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServicesKernel.GDFX
{
    public class ResultLoader
    {
        private Dictionary<string, string> resultFileNames;

        private Dictionary<string, string> FileNames
        {
            get 
            {
                if (resultFileNames == null)
                    LoadFileNames();
                return resultFileNames;
            }
        }

        /// <summary>
        /// 载入轨道计算结果文件指定数据
        /// </summary>
        /// <param name="filePath">结果文件路径</param>
        /// <param name="resultType">结果类型</param>
        /// <param name="dataType">数据列名</param>
        /// <param name="parseDate">是否解析日期</param>
        /// <param name="dates">日期</param>
        /// <param name="dblDatas">double型数据列</param>
        /// <param name="intDatas">int型数据列</param>
        /// <returns></returns>
        public string LoadResultFile(string filePath, string resultType, string dataType
            , bool parseDate, out List<DateTime> dates, out List<double> dblDatas, out List<int> intDatas
            , out double maxValue, out double minValue, out int totalCount)
        {
            dates = new List<DateTime>();
            dblDatas = new List<double>();
            intDatas = new List<int>();
            maxValue = double.MinValue;
            minValue = double.MaxValue;
            totalCount = 0;

            string strResult = string.Empty;
            bool blResult;
            string strLine = string.Empty;
            int iLine = 1;
            int iTick = 1;
            double dbTmp = 0;
            //int iTmp = 0;
            string[] strDatas;
            DateTime date;
            ResultType oRType = FormatXMLConfig.GetTypeByName(resultType);

            #region Load Config Info
            if (oRType == null)
            {
                strResult = string.Format("读取轨道计算配置信息{0}失败", resultType);
                return strResult;
            }

            ResultType oCSType;
            if (resultType.ToLower() == "cutpre_stw" || resultType.ToLower() == "cutpre_unw" 
                || resultType.ToLower() == "cutana_stw" || resultType.ToLower() == "cutana_unw")
            {
                oCSType = FormatXMLConfig.GetTypeByName("CutPre_STW");
                if (oCSType != null)
                    oRType.Results = oCSType.Results;
                else
                {
                    strResult = "读取轨道计算配置信息CutPre_STW失败";
                    return strResult;
                }
            }

            ResultData oRData = oRType.GetDataByName(dataType);
            if (oRData == null)
            {
                strResult = "读取轨道计算配置信息Data失败";
                return strResult;
            }
            #endregion

            string strFileName = oRType.FileName;
            string strFileFullName = Path.Combine(filePath, strFileName);
            string[] files = Directory.GetFiles(filePath, oRType.FileName, SearchOption.TopDirectoryOnly);
            if (files.Length == 0)
            {
                strResult = string.Format("在文件目录中找不到结果文件",oRType.FileName);
                return strResult;
            }
            strFileFullName = files[0];
            strResult = string.Format("读取{0}文件【{1}】出错，", resultType, strFileName);
            StreamReader oReader = new StreamReader(strFileFullName);
            oReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oReader.ReadLine();
            strLine = oReader.ReadLine();

            while (strLine != null && !strLine.Equals(string.Empty))
            {
                if ((oRType.IsBigFile && iTick == 1) || !oRType.IsBigFile)
                {
                    strDatas = DataValidator.SplitRowDatas(strLine);
                    if (parseDate)
                    {
                        blResult = DataValidator.ValidateDateColon(strDatas, 0, out date);
                        if (!blResult)
                        {
                            strResult += string.Format("行{0}日期格式错误", iLine);
                            oReader.Close();
                            return strResult;
                        }
                        dates.Add(date);
                    }

                    //if (oRData.Type == DataType.doubletype)
                        blResult = DataValidator.ValidateFloat(strDatas[oRData.Position], oRData.IntLen + oRData.DecLen, oRData.DecLen, out dbTmp);
                    //else
                    //    blResult = DataValidator.ValidateInt(strDatas[oRData.Position], oRData.IntLen, out iTmp);
                    if (!blResult)
                    {
                        strResult += string.Format("行{0}数据格式错误", iLine);
                        oReader.Close();
                        return strResult;
                    }

                    //if (oRData.Type == DataType.doubletype)
                        dblDatas.Add(dbTmp);
                    //else
                    //    intDatas.Add(iTmp);
                    if (dbTmp < minValue)
                        minValue = dbTmp;
                    if (dbTmp > maxValue)
                        maxValue = dbTmp;
                }
                strLine = oReader.ReadLine();
                iLine++;
                iTick++;
                if (oRType.IsBigFile && iTick == 301)//对于大文件，每n分钟显示一个点
                    iTick = 1;
            }
            oReader.Close();
            totalCount = iLine;
            return strResult;
        }

        /// <summary>
        /// 载入文件名集合
        /// </summary>
        private void LoadFileNames()
        {
            if (resultFileNames == null)
            {
                resultFileNames = new Dictionary<string,string>();
                //交会预报，STW结果
                resultFileNames.Add("CutPre_STW", "CutSTW.dat");
                //交会预报，UNW结果
                resultFileNames.Add("CutPre_UNW", "CutUNW.dat");
                //交会分析，STW结果
                resultFileNames.Add("CutAna_STW", "CutAnalyze_STW.dat");
                //交会分析，UNW结果
                resultFileNames.Add("CutAna_UNW", "CutAnalyze_UNW.dat");
                //轨道预报，空间位置预报，J2000系
                resultFileNames.Add("GDYB_MapJ", "MapJ.dat");
                //轨道预报，空间位置预报，WGS-84坐标系
                resultFileNames.Add("GDYB_MapW", "MapW.dat");
                //轨道预报，星下点预报
                resultFileNames.Add("GDYB_SubSatPoint", "SubSatPoint.dat");
                //轨道预报，测站观测量
                resultFileNames.Add("GDYB_StaObsPre", "StaObsPre.dat");
                //轨道预报，观测引导文件
                resultFileNames.Add("GDYB_ObsGuiding", "ObsGuiding.dat");
                //轨道预报，太阳角度
                resultFileNames.Add("GDYB_SunAH", "SunAH.dat");
                //轨道预报，测站可见性统计文件
                resultFileNames.Add("GDYB_VisibleStatistics", "VisibleStatistics.dat");
                //轨道预报，进出站及航捷数据统计文件
                resultFileNames.Add("GDYB_StationInOut", "StationInOut.dat");
                //轨道预报，日凌历元文件
                resultFileNames.Add("GDYB_SunTransit", "SunTransit.dat");
                //轨道预报，月凌历元文件
                resultFileNames.Add("GDYB_MoonTransit", "MoonTransit.dat");
                //轨道预报，地影历元文件
                resultFileNames.Add("GDYB_EarthShadow", "EarthShadow.dat");  
            }
        }
    }
    public enum ResultDataType
    {
        /// <summary>
        /// CutPre、CutAna
        /// </summary>
        dR,
        /// <summary>
        /// CutPre、CutAna、GDYB_StaObsPre、GDYB_ObsGuiding、GDYB_SunAH
        /// </summary>
        A,
        /// <summary>
        /// CutPre、CutAna、GDYB_StaObsPre、GDYB_ObsGuiding、GDYB_SunAH
        /// </summary>
        E,
        /// <summary>
        /// CutPre、CutAna
        /// </summary>
        ddR,
        /// <summary>
        /// CutPre、CutAna
        /// </summary>
        dA,
        /// <summary>
        /// CutPre、CutAna
        /// </summary>
        dE,
        /// <summary>
        /// CutPre、CutAna
        /// </summary>
        ANG,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        X,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        Y,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        Z,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        vX,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        vY,
        /// <summary>
        /// GDYB：MapJ、MapW
        /// </summary>
        vZ,
        /// <summary>
        /// GDYB：SubSatPoint
        /// </summary>
        L,
        /// <summary>
        /// GDYB：SubSatPoint
        /// </summary>
        B,
        /// <summary>
        /// GDYB：SubSatPoint
        /// </summary>
        H,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        R,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        RR,
        /// <summary>
        /// GDYB_StaObsPre、GDYB_ObsGuiding
        /// </summary>
        AA,
        /// <summary>
        /// GDYB_StaObsPre、GDYB_ObsGuiding
        /// </summary>
        EE,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        Th,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        TTh,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        K,
        /// <summary>
        /// GDYB_StaObsPre
        /// </summary>
        M
    }
}
