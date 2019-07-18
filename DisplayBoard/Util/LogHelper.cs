using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DisplayBoard.Util
{
    public class LogHelper
    {
        public static readonly ILog infoLog = LogManager.GetLogger("info");
        public static readonly ILog errorLog = LogManager.GetLogger("error");

        /// <summary>
        /// 普通日子写入
        /// </summary>
        /// <param name="info"></param>
        public static void WriteInfoLog(string info)
        {
            infoLog.Info(info);
        }

        /// <summary>
        /// 错误日志写入
        /// </summary>
        /// <param name="info"></param>
        public static void WriteErrorLog(string info)
        {
            errorLog.Error(info);
        }

        /// <summary>
        /// 读取错误日志
        /// </summary>
        /// <returns></returns>
        public static string ReadLog(string fileName)
        {
            string readTxt = "";
            string logFile = Path.Combine(Environment.CurrentDirectory, "Logs", fileName);
            if (File.Exists(logFile))
            {
                readTxt = ReadFileInReverse(logFile);
            }

            return readTxt;
        }

        /// <summary>
        /// 读取文件流
        /// </summary>
        /// <param name="logFile"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        private static string ReadFileInReverse(string logFile)
        {
            int n = 0;
            int i = 1;//指的是不停的向后(文件末尾向前移动)按字节往前读的位置
            int b = 1;
            using (FileStream fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.Write))
            {
                if (fs.Length == 0) return string.Empty;
                MemoryStream ms = new MemoryStream();
                while (b > 0)
                {
                    long t = fs.Seek(-i, SeekOrigin.End);
                    i++;
                    if (t == 0) break;//t代表当前正序时的位置最前面是1,如果不判断,当文档没有N行时,会造成移动到-1位置报错,意思就是到了文件头了不能再前移了,防止傻比行为
                    b = fs.ReadByte();
                    ms.WriteByte((byte)b);
                    char ch = Convert.ToChar(b);
                    if (ch == '\n')//按行取,指的是\r\n的行,这里只判断\n
                        n++;
                }
                byte[] data = ms.ToArray();
                Array.Reverse(data);
                ms.Close();
                string txt = Encoding.GetEncoding("GB2312").GetString(data);
                return txt.Trim();
            }
        }

        /// <summary>
        /// 生成自定义异常消息
        /// </summary>
        /// <param name="ex">异常对象</param>
        /// <param name="backStr">备用异常消息：当ex为null时有效</param>
        /// <returns>异常字符串文本</returns>
        public static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************Exception****************************");
            sb.AppendLine("【Time】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【Type】：" + ex.GetType().Name);
                sb.AppendLine("【Info】：" + ex.Message);
                sb.AppendLine("【Stack】：" + ex.StackTrace);
            }
            if (!string.IsNullOrEmpty(backStr))
            {
                sb.AppendLine("【Custom】：" + backStr);
            }
            sb.AppendLine("***************************************************************");
            return sb.ToString();
        }

        /// <summary>
        /// 删除过期日志
        /// </summary>
        /// <param name="expiredDay"></param>
        /// <param name="dirPath"></param>
        public static void DeleteExpiredLogFiles()
        {
            string dirPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            DirectoryInfo folder = new DirectoryInfo(dirPath);
            foreach (FileInfo file in folder.GetFiles())
            {

                if (file.Name == "update.log" || file.Name == "error.log" || file.Name == "info.log" || file.Name == "freetime.json")
                    continue;

                var fileCreateTime = file.LastWriteTime.Date;
                var curTime = DateTime.Now.Date;
                TimeSpan timeSpan = curTime - fileCreateTime;
                if (timeSpan.Days > 7)
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch (Exception ex)
                    {
                        string str = LogHelper.GetExceptionMsg(ex, "DeleteExpiredLogFiles异常");
                        WriteErrorLog(str);
                    }
                }
            }
        }


    }
}
