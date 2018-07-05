using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HHCSPHelp
{
    public class CSPLogger
    {
        public static void Output(string text)
        {
            text += Environment.NewLine;
            Out2Console(text);
            WriteLog2Txt(text);            
        }

        private static void Out2Console(string s)
        {
            Console.WriteLine(s);
        }

        private static void WriteLog2Txt(string s)
        {
            File.AppendAllText(CSPLoginSet.LogTxtFile, s);
        }

        public static void Error2Txt(string s)
        {
            s += "\r\n\r\n";
            File.AppendAllText(CSPLoginSet.ErrorTxtFile, s);
        }
    }
}
