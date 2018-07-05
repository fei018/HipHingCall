using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ClosedXML.Excel;

namespace HHCSPHelp
{
    internal class CSPJobFromExcel
    {
        public List<JobRequest> GetCallList(string filepath)
        {
            List<JobRequest> jobList = new List<JobRequest>();
            try
            {
                XLWorkbook workbook = new XLWorkbook(filepath);
                IXLWorksheet worksheet = workbook.Worksheet(1);
                IXLRows rows = worksheet.RowsUsed();
                foreach (IXLRow r in rows)
                {
                    if (r.RowNumber() == 1) continue;

                    JobRequest job = new JobRequest();
                    foreach (IXLCell cell in r.CellsUsed())
                    {
                        FillJobRequestInfo(cell, ref job);
                    }
                    jobList.Add(job);
                }
                if (jobList.Count <= 0) throw new Exception("JobRequestInfo List Count is 0");
                workbook.Dispose();
                return jobList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 獲取cell所在列的第一個cell的值,和列名進行match and fill JobRequestInfo的屬性值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="job"></param>
        private void FillJobRequestInfo(IXLCell cell, ref JobRequest job)
        {
            try
            {
                switch (cell.WorksheetColumn().FirstCell().GetValue<string>().Trim())
                {
                    case "ContactPerson":
                        job.ContactPerson = cell.GetValue<string>();
                        break;

                    case "Location":
                        job.Location = cell.GetValue<string>();
                        break;

                    case "Company":
                        job.Company = cell.GetValue<string>();
                        break;

                    case "RequestType":
                        job.RequestType = cell.GetValue<string>();
                        break;

                    case "Symptom":
                        job.Symptom = cell.GetValue<string>();
                        break;

                    case "ScheduleTime":
                        job.ScheduleTime = cell.GetValue<string>();
                        break;

                    case "ServeTime1":
                        job.ServeTime1 = cell.GetValue<string>();
                        break;

                    case "ServeTime2":
                        job.ServeTime2 = cell.GetValue<string>();
                        break;

                    case "ServiceDescription":
                        job.ServiceDescription = cell.GetValue<string>();
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }           
        }

        #region For testing
        //public void ShowList()
        //{
        //    List<JobRequestInfo> list =  GetCallList();
        //    foreach (JobRequestInfo job in list)
        //    {
        //        string s = job.ContactPerson + " " + job.Location + " " + job.Company + " " + job.RequestType + " " + job.Symptom + " " 
        //                  + job.ScheduleTime + " " + job.ServeTime1 + " " + job.ServeTime2 + " " + job.ServiceDescription;
        //        Console.WriteLine(s);
        //    }
        //}
        #endregion
    }
}
