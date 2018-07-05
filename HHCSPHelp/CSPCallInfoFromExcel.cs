using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ClosedXML.Excel;
using HHCSPHelp.AboutCallInfo;

namespace HHCSPHelp
{
    internal class CSPCallInfoFromExcel
    {
        public CallInfoList GetCallList(string excelPath)
        {
            CallInfoList listInfo = new CallInfoList();
            try
            {
                using (XLWorkbook workbook = new XLWorkbook(excelPath))
                {
                    using (IXLWorksheet sheet = workbook.Worksheet(1))
                    {
                        using (IXLRows rows = sheet.RowsUsed())
                        {
                            FillCallInfoList(rows, ref listInfo);
                        }
                    }
                }
                return listInfo;
            }           
            catch (Exception)
            {
                throw;
            }
        }

        #region Fill call info list data
        private void FillCallInfoList(IXLRows rows, ref CallInfoList listInfo)
        {
            try
            {
                foreach (IXLRow r in rows)
                {
                    if (r.RowNumber() == 1) continue;

                    CallInfoSymptom symptom = new CallInfoSymptom();
                    CallInfoTime time = new CallInfoTime();
                    foreach (IXLCell cell in r.CellsUsed())
                    {                       
                        MatchCallInfoListName(cell, ref listInfo,ref symptom, ref time);                        
                    }
                    listInfo.SymptomInfo.Add(symptom);
                    listInfo.TimeInfo.Add(time);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void MatchCallInfoListName(IXLCell cell, ref CallInfoList listInfo, ref CallInfoSymptom symptom, ref CallInfoTime time)
        {
            try
            {               
                switch (cell.WorksheetColumn().FirstCell().GetString().Trim().ToLower())
                {
                    case "contactperson":
                        listInfo.ContactPerson.Add(HttpUtility.UrlEncode(cell.GetString()));
                        break;

                    case "location":
                        listInfo.Location.Add(HttpUtility.UrlEncode(cell.GetString()));
                        break;

                    case "company":
                        listInfo.Company.Add(HttpUtility.UrlEncode(cell.GetString()));
                        break;

                    case "requesttype":
                        symptom.RequestType = HttpUtility.UrlEncode(cell.GetString());
                        break;

                    case "symptom":
                        symptom.Symptom = HttpUtility.UrlEncode(cell.GetString());
                        break;

                    case "scheduletime":
                        time.ScheduleTime = HttpUtility.UrlEncode(cell.GetString());
                        break;

                    case "servetime1":
                        time.ServeTime1 = HttpUtility.UrlEncode(cell.GetString());
                        break;

                    case "servetime2":
                        time.ServeTime2 = HttpUtility.UrlEncode(cell.GetString());
                        break;

                    case "servicedescription":
                        symptom.ServiceDescription = HttpUtility.UrlEncode(cell.GetString());
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion

    }
}
