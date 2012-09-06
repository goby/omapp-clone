using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OperatingManagement.DataAccessLayer.PlanManage;

namespace ServicesKernel.File
{
    public class StationInOutFileReader
    {
        /// <summary>
        /// 获得完整路径
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string GetFullFilePath(string filename)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["StationInOutFilePath"];
            if (path != string.Empty)
            {
                if (path[path.Length - 1] != '\\')
                    path = path + @"\";
            }
            else
            {
                path = AppDomain.CurrentDomain.BaseDirectory + @"TempJHSavePath\";
            }
            path += filename;
            return path ;
        }

        /// <summary>
        /// 读取进出站及航捷数据统计文件
        /// </summary>
        /// <param name="filename">文件完整路径</param>
        /// <returns></returns>
        public List<StationInOut> Read(string filename)
        {
            StationInOut station;
            List<StationInOut> list = new List<StationInOut>();
            string fullpath = GetFullFilePath(filename);
            string[] content = System.IO.File.ReadAllLines(fullpath);
            for (int i = 1; i < content.Length; i++)
            {
                station = GetStationInOutFromString(content[i]);
                list.Add(station);
            }

            return list;
        }

        public StationInOut GetStationInOutFromString(string data)
        {
            string[] temp;
            
            data = data.Replace("          ", ",");//10个空格
            data = data.Replace("        ", ",");//8个空格
            data = data.Replace("      ", ",");//6个空格
            data = data.Replace("   ", ",");//3个空格
            data = data.Replace("  ", ",");//2个空格
            temp = data.Split(',');

            int i=1;
            StationInOut sta = new StationInOut();
            sta.ZM = temp[i++];
            sta.N = temp[i++];
            sta.QC = temp[i++];
            sta.SJG = temp[i++];
            sta.SP1 = temp[i++];
            sta.SP2 = temp[i++];
            sta.T1 = temp[i++];
            sta.T2 = temp[i++];
            sta.T3 = temp[i++];
            sta.T4 = temp[i++];
            sta.T5 = temp[i++];
            sta.T6 = temp[i++];
            sta.T7 = temp[i++];
            sta.h = temp[i++];
            return sta;
        }
    }
}
