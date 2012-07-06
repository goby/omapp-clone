using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesKernel.GDFX
{
    /// <summary>
    /// 轨道分析逻辑处理
    /// </summary>
    public class GDFXProcessor
    {
        /// <summary>
        /// 交会分析
        /// </summary>
        /// <param name="subFileFullName"></param>
        /// <param name="tgtFileFullName"></param>
        /// <param name="resultFileName">结果文件名的一部分（未加_STW.dat和_UNW.dat）</param>
        /// <returns></returns>
        public string CutAnalyze(string subFileFullName, string tgtFileFullName, out string resultFileName)
        {
            /*
             * 1、检查依赖的文件是否都存在
             * 2、检查主星文件和目标星文件数据是否合法
             * 3、开始计算
             */

            resultFileName = string.Empty;
            string strResult = string.Empty;
            #region 检查数据文件存在及合法性
            strResult = CutAnalyzer.Instance.IsAllFileExist();
            if (!strResult.Equals(string.Empty))
                return strResult;

            DateTime subMinDate;
            DateTime subMaxDate;
            int subInterval;
            strResult = CutAnalyzer.Instance.CheckFileData(subFileFullName, out subMinDate, out subMaxDate, out subInterval);
            if (!strResult.Equals(string.Empty))
            {
                strResult = string.Format("主星文件数据不合法{0}", strResult);
                return strResult;
            }

            DateTime tgtMinDate;
            DateTime tgtMaxDate;
            int tgtInterval;
            strResult = CutAnalyzer.Instance.CheckFileData(tgtFileFullName, out tgtMinDate, out tgtMaxDate, out tgtInterval);
            if (!strResult.Equals(string.Empty))
            {
                strResult = string.Format("目标星文件数据不合法{0}", strResult);
                return strResult;
            }
            #endregion

            //检查时间是否有交集
            if (subMinDate > tgtMaxDate || tgtMinDate > subMaxDate)
            {
                strResult = "两个数据文件时间没有交集";
                return strResult;
            }

            #region 比较间隔、开始时间、结束时间
            DateTime beginDate;
            DateTime endDate;
            int maxInterval;
            int minInterval;
            bool IsSubInterval = false;
            bool IsSubBeginDate = false;
            bool IsSubEndDate = false;

            if (subInterval >= tgtInterval)
            {
                IsSubInterval = true;
                maxInterval = subInterval;
                minInterval = tgtInterval;
            }
            else
            {
                maxInterval = tgtInterval;
                minInterval = subInterval;
            }

            if (subMinDate >= tgtMinDate)
            {
                IsSubBeginDate = true;
                beginDate = subMinDate;
            }
            else
                beginDate = tgtMinDate;

            if (subMaxDate >= tgtMaxDate)
                endDate = tgtMaxDate;
            else
            {
                IsSubEndDate = true;
                endDate = subMaxDate;
            }
            #endregion

            strResult = CutAnalyzer.Instance.DoCaculate(subFileFullName, tgtFileFullName, beginDate, endDate, maxInterval, minInterval
                , IsSubInterval, IsSubBeginDate, IsSubEndDate, out resultFileName);
            return strResult;
        }

        /// <summary>
        /// 参数转换
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="length"></param>
        /// <param name="convertType"></param>
        /// <param name="convertFilePath">路径+文件名</param>
        /// <param name="emitFilePath">路径+文件名</param>
        /// <returns></returns>
        public string ParamConvert(bool deg, bool km, string convertType, string convertFileFullName, string emitFileFullName
            , out string resultFileName)
        {
            /*
             * 1、校验发射系文件，时间合法
             * 2、校验待转换文件，时间合法，数据合法
             * 3、进行转换
             */
            resultFileName = string.Empty;
            string strResult = string.Empty;
            strResult = ParamConvertor.Instance.CheckEmitFileData(emitFileFullName);
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = ParamConvertor.Instance.CheckConvertFileData(convertFileFullName);
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = ParamConvertor.Instance.DoConvert(deg, km, convertType, convertFileFullName, emitFileFullName, out resultFileName);
            return strResult;
        }

        /// <summary>
        /// 差值分析，俩文件必须在同一路径中
        /// </summary>
        /// <param name="ephemerisFileFullName">路径+文件名</param>
        /// <param name="timeSeriesFileFullName">路径+文件名</param>
        /// <param name="resultFileFullName">路径+文件名</param>
        /// <returns></returns>
        public string Intepolate(string ephemerisFileFullName, string timeSeriesFileFullName, out string resultFileFullName)
        {
            /*
             * 1、校验差值分析时间文件，时间合法，数据合法
             * 2、校验星历文件，时间合法
             * 3、进行差值分析
             */
            resultFileFullName = string.Empty;
            string strResult = string.Empty;
            DateTime beginDate;
            DateTime endDate;
            strResult = Intepolater.Instance.CheckTimeSeriesFileData(timeSeriesFileFullName, out beginDate, out endDate);
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = Intepolater.Instance.CheckEphemerisFileData(timeSeriesFileFullName, beginDate, endDate);
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = Intepolater.Instance.DoCaculate(ephemerisFileFullName, timeSeriesFileFullName, out resultFileFullName);
            return strResult;
        }

        /// <summary>
        /// 交会预报
        /// </summary>
        /// <param name="filesPath">交会预报的文件路径</param>
        /// <returns></returns>
        public string CutPre(string filesPath, out string resultFileFullName)
        {
            /*
             * 1、检查需要的文件是否都有
             * 2、校验主文件数据合法性
             * 3、校验Sub文件数据合法性
             * 4、校验Optional文件数据合法性
             * 5、进行交会预报计算
             */
            resultFileFullName = string.Empty;
            string strResult = string.Empty;
            strResult = CutPrer.Instance.IsAllFileExist();
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = CutPrer.Instance.CheckMainFileData(filesPath + "CutMain.dat");
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = CutPrer.Instance.CheckSubFileData(filesPath + "CutSub.dat");
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = CutPrer.Instance.CheckOptionalFileData(filesPath + "CutOptional.dat");
            if (!strResult.Equals(string.Empty))
                return strResult;

            strResult = CutPrer.Instance.DoCaculate(filesPath);
            return strResult;
        }
    }
}
