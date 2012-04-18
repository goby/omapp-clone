using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace ServicesKernel.File
{
    public class FileExchangeConfig
    {
        private static string filePath = @"../app_data/FileExchangeConfig.xml";
        private static string sSeperator = "-";
        private static Dictionary<string, string> sendList = null;
        private static Dictionary<string, string> recvList = null;
        private static List<string> recvSuffixList = null;

        public static string GetSuffixForSending(string infoCode, string fromMark, string toMark)
        {
            if (sendList == null)
                Load();
            if (sendList == null)
                return null;

            string sKey = infoCode + sSeperator + fromMark + sSeperator + toMark;
            if (sendList.ContainsKey(sKey))
                return sendList[sKey].ToString();
            else
                return null;
        }

        private static void Load()
        {
            XDocument doc = XDocument.Load(filePath);
            XElement root = doc.Root;
            if (doc == null)
                return;

            string sType = "";
            string sSrc = "";
            string sSuffix = "";
            XElement node = null;
            var sInfos = root.Element("toSend").Elements("Info");
            if (sInfos != null && sInfos.Count() > 0)
            {
                sendList = new Dictionary<string, string>();
                for (int i = 0; i < sInfos.Count(); i++)
                {
                    node = sInfos.ElementAt(i);
                    sType = node.Element("Type").Value;
                    sSrc = node.Element("SrcCode").Value;
                    sSuffix = node.Element("Suffix").Value;
                    foreach (XElement tgt in node.Element("TgtList").Elements("TgtCode"))
                    {
                        sendList.Add(sType + sSeperator + sSrc + sSeperator + tgt.Value, sSuffix);
                    }
                }
            }

            var rInfos = root.Element("toReceive").Elements("Info");
            if (rInfos != null && rInfos.Count() > 0)
            {
                recvList = new Dictionary<string, string>();
                recvSuffixList = new List<string>();
                for (int i = 0; i < rInfos.Count(); i++)
                {
                    node = rInfos.ElementAt(i);
                    sType = node.Element("Type").Value;
                    sSrc = node.Element("SrcCode").Value;
                    sSuffix = node.Element("Suffix").Value;
                    if (sSrc.Contains(';'))
                    {
                        string[] strSrcs = sSrc.Split(new char[] { ';' });
                        for (int j = 0; j < strSrcs.Length; j++)
                        {
                            recvList.Add(sType + sSeperator + strSrcs[j], sSuffix);
                        }
                    }
                    else
                        recvList.Add(sType + sSeperator + sSrc, sSuffix);

                    if (!recvSuffixList.Contains(sSuffix))
                        recvSuffixList.Add(sSuffix);
                }
            }
            doc = null;
        }
    }
}
