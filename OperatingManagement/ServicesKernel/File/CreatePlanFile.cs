using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ServicesKernel.File
{
    public class CreatePlanFile
    {
        #region -Properties-
        StreamReader sr;
        StreamWriter sw;

        public int ID { get; set; }
        public DateTime CTime { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public string TaskID { get; set; }
        public string InfoType { get; set; }
        public int LineCount { get; set; }
        public string Format1 { get; set; }
        public string Format2 { get; set; }
        public string DataSection { get; set; }
        public string FileIndex { get; set; }
        public string Reserve { get; set; }

        public string FilePath { get; set; }
        public string SavePath { get; set; }

        #endregion

        public void NewFile()
        {
            sw= new StreamWriter(FilePath);
            sw.WriteLine("<说明区>");
            sw.WriteLine("[生成时间]：" + this.CTime.ToString("yyyy-MM-dd-HH-mm"));
                sw.WriteLine("[信源S]："+this.Source);
                sw.WriteLine("[信宿D]："+this.Destination);
                sw.WriteLine("[任务代码M]："+this.TaskID);
                sw.WriteLine("[信息类别B]："+this.InfoType);
                sw.WriteLine("[数据区行数L]："+this.LineCount.ToString("0000"));
            sw.WriteLine("<符号区>");
                sw.WriteLine("[格式标识1]："+this.Format1);
                if (!string.IsNullOrEmpty(this.Format2))
                {
                    sw.WriteLine("[格式标识2]：" + this.Format2);    //没有这个字段时不输出
                }
            sw.WriteLine("[数据区]：");
                sw.WriteLine(this.DataSection);
            sw.WriteLine("<辅助区>");
                sw.WriteLine("[备注]：");
                sw.WriteLine("[结束]：END");

                sw.Close();
        }
    }
}
