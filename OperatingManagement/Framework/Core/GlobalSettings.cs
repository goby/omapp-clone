using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Configuration.Provider;
using System.Collections.Specialized;
using OperatingManagement.Framework.Helper;

namespace OperatingManagement.Framework.Core
{
    /// <summary>
    /// Provides global settings
    /// </summary>
    public class GlobalSettings
    {
        #region -Properties-
        public static readonly string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly string encryptKey = AspNetConfig.Config["encryptKey"].ToString();
        private static char[] splitChars = new char[] { ';', ',', '.', '?', ' ', '，', '；', '。', '？' };
        #endregion

        #region -Format KeyWords-
        /// <summary>
        /// Formats the keywords
        /// <remarks>such as replace ';','；' into ',' </remarks>
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public static string FormatKeywords(string keywords)
        {
            if (string.IsNullOrEmpty(keywords))
                return string.Empty;
            string[] wordList = keywords.Split(splitChars);
            keywords = string.Join(";", wordList);
            while (keywords.IndexOf(";;") >= 0)
                keywords = keywords.Replace(";;", ";");
            return keywords;
        }
        #endregion

        #region -Html Encode-
        /// <summary>
        /// Encode the html.
        /// </summary>
        /// <param name="text">Encoding text.</param>
        /// <returns></returns>
        public static string EnsureHtmlEncoded(string text)
        {
            return WebHelper.EnsureHtmlEncoded(text);
        }
        #endregion

        #region -Web Path-
        /// <summary>
        /// Convert the vitual path to server URI
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            HttpContext context = HttpContext.Current;

            if (context != null)
            {
                return context.Server.MapPath(path);
            }
            else
            {
                return PhysicalPath(path.Replace("/", Path.DirectorySeparatorChar.ToString()).Replace("~", ""));
            }
        }
        /// <summary>
        /// Convert the vitual path to server physical path
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns></returns>
        public static string PhysicalPath(string path)
        {
            string rootPath = AppDomain.CurrentDomain.BaseDirectory;
            char dirSep = Path.DirectorySeparatorChar;
            rootPath = rootPath.Replace("/", dirSep.ToString());
            return string.Concat(rootPath.TrimEnd(dirSep), dirSep, path.TrimStart(dirSep));
        }

        private static string _RelativeWebRoot;
        /// <summary>
        /// The URL relative to web root.
        /// </summary>
        public static string RelativeWebRoot
        {
            get
            {
                if (string.IsNullOrEmpty(_RelativeWebRoot))
                    _RelativeWebRoot = VirtualPathUtility.ToAbsolute("~/");
                return _RelativeWebRoot;
            }
        }
        /// <summary>
        /// The URI abosolute to web root.
        /// </summary>
        public static Uri AbsoluteWebRoot
        {
            get
            {
                HttpContext context = HttpContext.Current;
                if (context == null)
                    throw new System.Net.WebException("The current HttpContext is null");

                if (context.Items["absoluteurl"] == null)
                    context.Items["absoluteurl"] = new Uri(context.Request.Url.GetLeftPart(UriPartial.Authority) + RelativeWebRoot);

                return context.Items["absoluteurl"] as Uri;
            }
        }
        #endregion

        #region -Compress-
        /// <summary>
        /// 是否压缩javascript
        /// </summary>
        public static bool CompressJs
        {
            get
            {
                return bool.Parse(AspNetConfig.Config["compressJs"].ToString());
            }
        }
        /// <summary>
        /// 是否压缩Css
        /// </summary>
        public static bool CompressCss
        {
            get
            {
                return bool.Parse(AspNetConfig.Config["compressCss"].ToString());
            }
        }
        #endregion

        #region -DateTime-
        /// <summary>
        /// Minimize value of DateTime
        /// </summary>
        public static DateTime MinValue
        {
            get
            {
                return new DateTime(2000, 1, 1);
            }
        }
        /// <summary>
        /// Fetch previous week's start datetime and end datetime.
        /// </summary>
        /// <param name="current">Current datetime</param>
        /// <param name="start">Start datetime</param>
        /// <param name="end">End datetime</param>
        public static void FetchPreviousWeekDate(DateTime current, out DateTime start, out DateTime end)
        {
            int minusDay = (int)current.DayOfWeek;
            if (minusDay == 0)
                minusDay = 7;
            end = current.AddDays(-minusDay);
            start = end.AddDays(-6);
        }

        /// <summary>
        /// Fetch current week's start datetime and end datetime.
        /// </summary>
        /// <param name="current">Current datetime</param>
        /// <param name="start">Start datetime</param>
        /// <param name="end">End datetime</param>
        public static void FetchCurrentWeekDate(DateTime current, out DateTime start, out DateTime end)
        {
            int minusDay = (int)current.DayOfWeek;
            if (minusDay == 0)
                minusDay = 7;
            end = current;
            start = current.AddDays(-minusDay + 1);
        }
        /// <summary>
        /// Fetch previous month's start datetime and end datetime.
        /// </summary>
        /// <param name="current">Current datetime</param>
        /// <param name="start">Start datetime</param>
        /// <param name="end">End datetime</param>
        public static void FetchPreviousMonthDate(DateTime current, out DateTime start, out DateTime end)
        {
            start = current.AddMonths(-1).AddDays(-current.Day + 1);
            end = current.AddDays(-current.Day);
        }
        /// <summary>
        /// Fetch current month's start datetime and end datetime.
        /// </summary>
        /// <param name="current">Current datetime</param>
        /// <param name="start">Start datetime</param>
        /// <param name="end">End datetime</param>
        public static void FetchCurrentMonthDate(DateTime current, out DateTime start, out DateTime end)
        {
            start = current.AddDays(-current.Day+1);
            end = current;
        }
        /// <summary>
        /// Maxisize value of DateTime
        /// </summary>
        public static DateTime MaxValue
        {
            get
            {
                return new DateTime(2999, 12, 31, 23, 59, 59);
            }
        }
        #endregion

        #region -Encrypt-
        static readonly string plusReplacer = "______IMP_________________";
        /// <summary>
        /// 加密字符串 MD5+TRIPLE DES
        /// </summary>
        /// <param name="planText"></param>
        /// <returns></returns>
        public static string Encrypt(string planText)
        {
            return Encrypt(planText, true);
        }
        /// <summary>
        /// Encrypt the text with MD5+TRIPLE DES.
        /// </summary>
        /// <param name="planText">Text</param>
        /// <param name="filterPlus">Whether replace the special word such as '+'，in order to avoid the mistake in Url transfer.</param>
        /// <returns></returns>
        public static string Encrypt(string planText, bool filterPlus)
        {
            try
            {
                byte[] inputs = ASCIIEncoding.ASCII.GetBytes(planText);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = buffer;
                des.Mode = CipherMode.ECB;

                string result = Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(inputs, 0, inputs.Length));
                if (filterPlus)
                {
                    result = result.Replace("+", plusReplacer);
                }
                return HttpUtility.UrlEncode(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// Decrypt the text with MD5+TRIPLE DES.
        /// </summary>
        /// <param name="planText">Text</param>
        /// <returns></returns>
        public static string Decrypt(string planText)
        {
            try
            {
                planText = HttpUtility.UrlDecode(planText);
                planText = planText.Replace(plusReplacer, "+");
                byte[] inputs = Convert.FromBase64String(planText);
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] buffer = md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(encryptKey));
                TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
                des.Key = buffer;
                des.Mode = CipherMode.ECB;
                return ASCIIEncoding.ASCII.GetString(des.CreateDecryptor().TransformFinalBlock(inputs, 0, inputs.Length));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region -Extension-
        /// <summary>
        /// Gets the extention from fileName.
        /// </summary>
        /// <param name="fileName">File Name</param>
        /// <returns></returns>
        public static string GetExtension(string fileName)
        {
            Regex regex = new Regex(@"^.+\.([^\.]*)$", RegexOptions.None);

            Match m = regex.Match(fileName);
            if (m != null && m.Success)
                return m.Groups[1].Value;
            else
                return "";
        }
        #endregion

        #region -SubString-
        /// <summary>
        /// Gets the sub string.
        /// <remarks>The string '...' will be append to the RawString while the Length is less then the Length of RawString.</remarks>
        /// </summary>
        /// <param name="RawString">Raw String</param>
        /// <param name="Length">Sub Length</param>
        /// <returns></returns>
        public static string SubString(string RawString, Int32 Length)
        {
            if (string.IsNullOrEmpty(RawString)) { return string.Empty; }
            if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(RawString) <= Length*2)
            {
                return RawString;
            }
            else
            {
                for (Int32 i = RawString.Length - 1; i >= 0; i--)
                {
                    if (System.Text.Encoding.GetEncoding("GB2312").GetByteCount(RawString.Substring(0, i)) <= Length*2)
                    {
                        return RawString.Substring(0, i) + "...";
                    }
                }
                return "...";
            }
        }
        #endregion
    }
}
