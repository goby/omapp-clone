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
        /// <param name="resultFileName">结果文件名（未加_STW.dat和_UNW.dat）</param>
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
    }
}
