using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Gean.Utility
{
    public static class UtilityCompression 
    {
        public static string RAR_Executable_File { get; private set; }

        public static void Initialization(string file_RAR_Executable)
        {
            UtilityCompression.RAR_Executable_File = file_RAR_Executable;
        }

        /// <summary>
        /// 用RAR命令行执行程序解压指定的压缩文件
        /// </summary>
        /// <param name="compressionFile">指定的压缩文件</param>
        /// <returns>返回解压后的目录及文件数等相关信息</returns>
        public static string UnRar(string compressionFile)
        {
            //要解压的文件的路径，请自行设置 
            string rarFilePath = compressionFile;
            //确定要解压到的目录，是系统临时文件夹下，与原压缩文件同名的目录里 
            string unRarDestPath = Path.Combine(
                                            Path.GetDirectoryName(compressionFile), 
                                            Path.GetFileNameWithoutExtension(rarFilePath));
            //组合出需要shell的完整格式 
            string shellArguments = string.Format("x -o+ \"{0}\" \"{1}\\\"", rarFilePath, unRarDestPath);
            
            //用Process调用 
            using (Process unrar = new Process())
            {
                unrar.StartInfo.FileName = UtilityCompression.RAR_Executable_File;
                unrar.StartInfo.Arguments = shellArguments;
                unrar.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//隐藏rar本身的窗口 
                unrar.Start();
                unrar.WaitForExit();//等待解压完成  
                unrar.Close();
            }

            //统计解压后的目录和文件数 
            DirectoryInfo di = new DirectoryInfo(unRarDestPath);
            string info = string.Format("解压完成，共解压出：{0}个目录，{1}个文件", 
                                    di.GetDirectories().Length, 
                                    di.GetFiles().Length);
            return info;
        }
    }
}
