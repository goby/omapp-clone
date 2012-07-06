using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ServicesKernel.GDFX
{
    /// <summary>
    /// 用来校验数据
    /// </summary>
    public class DataValidator
    {
        /// <summary>
        /// 验证字符串是否为日期格式
        /// </summary>
        /// <param name="data"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool ValidateDate(string data, out DateTime date)
        {
            return DateTime.TryParseExact(data, "yyyyMMdd HHmmss.fff", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
        }

        /// <summary>
        /// 验证是否为正确的浮点型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalLength"></param>
        /// <param name="decimalLength"></param>
        /// <returns></returns>
        public static bool ValidateFloat(string data, int maxLength, int decimalLength)
        {
            bool blResult = true;
            int iIdx = data.IndexOf('.');
            if (iIdx > 0)
            {
                if (data.Length - iIdx - 1 > decimalLength)//小数位置超限
                    blResult = false;
                else
                {
                    if (data.Length - 1 > maxLength)//总长度超限
                        blResult = false;
                }
            }
            else
            {
                if (data.Length > maxLength - decimalLength)//没有小数位，总长度超限
                    blResult = false;
            }

            if (blResult)
            {
                double dbResult;
                return double.TryParse(data, out dbResult);
            }
            else
                return blResult;
        }


        /// <summary>
        /// 验证是否为正确的浮点型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="totalLength"></param>
        /// <param name="decimalLength"></param>
        /// <returns></returns>
        public static bool ValidateFloat(string data, int maxLength, int decimalLength, out double result)
        {
            result = 0;
            bool blResult = true;
            int iIdx = data.IndexOf('.');
            if (iIdx > 0)
            {
                if (data.Length - iIdx - 1 > decimalLength)//小数位置超限
                    blResult = false;
                else
                {
                    if (data.Length - 1 > maxLength)//总长度超限
                        blResult = false;
                }
            }
            else
            {
                if (data.Length > maxLength - decimalLength)//没有小数位，总长度超限
                    blResult = false;
            }

            if (blResult)
            {
                return double.TryParse(data, out result);
            }
            else
                return blResult;
        }
        
        /// <summary>
        /// 从行数据获取分割后的结果(空格分隔数据)
        /// </summary>
        /// <param name="rowData"></param>
        /// <returns></returns>
        public static string[] SplitRowDatas(string rowData)
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

        /// <summary>
        /// 验证是否为正确格式的整型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool ValidateInt(string data, int maxLength)
        {
            if (maxLength != 0)
            {
                if (data.Length > maxLength)
                    return false;
            }
            int iResult;
            return int.TryParse(data, out iResult);
        }

        /// <summary>
        /// 验证是否为正确格式的整型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static bool ValidateInt(string data, int maxLength, out int result)
        {
            result = 0;
            if (maxLength != 0)
            {
                if (data.Length > maxLength)
                    return false;
            }
            int iResult;
            return int.TryParse(data, out result);
        }

        /// <summary>
        /// 获取路径的ascii int数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static int[] GetIntPath(string path)
        {
            int[] iPath = new int[path.Length];
            byte[] bPath;
            ASCIIEncoding ascii = new ASCIIEncoding();
            bPath = ascii.GetBytes(path);
            for (int i = 0; i < iPath.Length; i++)
            {
                iPath[i] = (int)bPath[i];
            }
            return iPath;
        }
    }
}
