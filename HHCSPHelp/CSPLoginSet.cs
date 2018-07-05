using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace HHCSPHelp
{
    public static class CSPLoginSet
    {
        public static string LoginId { get; set; }

        public static string Password { get; set; }

        public static string Assignto { get; set; }

        public static string StartDate { get; set; }

        public static string EndDate { get; set; }

        public static string DayCalls { get; set; }

        public static string ExcelFile { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CallList.xlsx");

        public static string HKHolidayFile { get; set; } = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HKHoliday.txt");

        public static string LogTxtFile { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log.txt"); }

        public static string ErrorTxtFile { get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "error.txt"); }

        public static bool AppExit { get; set; } = false;

        public static void CheckLoginSet()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(CSPLoginSet.StartDate))
                {
                    throw new Exception("Error: StartDate null.");
                }
                if (string.IsNullOrWhiteSpace(CSPLoginSet.EndDate))
                {
                    throw new Exception("Error: EndDate null.");
                }
                if (DateTime.Today.CompareTo(DateTime.Parse(CSPLoginSet.EndDate)) < 0)
                {
                    throw new Exception("Error: EndDate wrong.");
                }

                if (DateTime.Parse(CSPLoginSet.EndDate).CompareTo(DateTime.Parse(CSPLoginSet.StartDate)) < 0)
                {
                    throw new Exception("Error: StartDate wrong.");
                }

                if (string.IsNullOrWhiteSpace(CSPLoginSet.LoginId))
                {
                    throw new Exception("Error: LoginName null.");
                }
                if (string.IsNullOrWhiteSpace(CSPLoginSet.Password))
                {
                    throw new Exception("Error: Password null.");
                }
                if (string.IsNullOrWhiteSpace(CSPLoginSet.Assignto))
                {
                    throw new Exception("Error: Assignto null.");
                }
                if (string.IsNullOrWhiteSpace(CSPLoginSet.DayCalls) || !Regex.IsMatch(CSPLoginSet.DayCalls,@"^[0-9]+$"))
                {
                    throw new Exception("Error: DayCalls null or not a number.");
                }
                if (!File.Exists(ExcelFile))
                {
                    throw new Exception("Error: CallList.xlsx not existing.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
