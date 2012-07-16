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

        private string LoadCutPreResult(string filePath, ResultType resultType, ResultDataType dataType
            , int postion, bool parseDate, out List<DateTime> dates, out List<double> datas)
        {
            dates = new List<DateTime>();
            datas = new List<double>();

            string strResult = string.Empty;
            bool blResult;
            string strLine = string.Empty;
            string strTmp = string.Empty;
            int iLine = 1;
            string[] strDatas;
            DateTime date;
            string strFileName = FileNames[System.Enum.GetName(typeof(ResultType), resultType)];
            string strFileFullName = Path.Combine(filePath, strFileName);

            strResult = string.Format("读取文件【{0}】出错，", strFileName);
            StreamReader oReader = new StreamReader(strFileFullName);
            oReader.BaseStream.Seek(0, SeekOrigin.Begin);
            strLine = oReader.ReadLine();

            while (strLine != null && !strLine.Equals(string.Empty))
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
                //blResult = DataValidator.ValidateFloat(
                strLine = oReader.ReadLine();
                iLine++;
            }
            return strResult;
        }

        private void LoadCutPreResult(string filePath, ResultType resultType, ResultDataType dataType, int postion, out List<DateTime> dates, out List<int> datas)
        {
            dates = new List<DateTime>();
            datas = new List<int>();
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

    public enum ResultType
    {
        /// <summary>
        /// 交会预报，STW结果
        /// </summary>
        CutPre_STW = 0,
        /// <summary>
        /// 交会预报，UNW结果
        /// </summary>
        CutPre_UNW = 1,
        /// <summary>
        /// 交会分析，STW结果
        /// </summary>
        CutAna_STW = 2,
        /// <summary>
        /// 交会分析，UNW结果
        /// </summary>
        CutAna_UNW = 3,
        /// <summary>
        /// 轨道预报，空间位置预报，J2000系
        /// </summary>
        GDYB_MapJ = 4,
        /// <summary>
        /// 轨道预报，空间位置预报，WGS-84坐标系
        /// </summary>
        GDYB_MapW = 5,
        /// <summary>
        /// 轨道预报，星下点预报
        /// </summary>
        GDYB_SubSatPoint = 6,
        /// <summary>
        /// 轨道预报，测站观测量
        /// </summary>
        GDYB_StaObsPre = 7,
        /// <summary>
        /// 轨道预报，观测引导文件
        /// </summary>
        GDYB_ObsGuiding = 8,
        /// <summary>
        /// 轨道预报，太阳角度
        /// </summary>
        GDYB_SunAH = 9,
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
