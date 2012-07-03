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
            return DateTime.TryParseExact(data, "yyyy-MM-dd HH:mm:ss.f", CultureInfo.InvariantCulture, DateTimeStyles.None, out date);
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
    }
}
