using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using HHCSPHelp.AboutCallInfo;

namespace HHCSPHelp
{
    public class CSP2Run
    {
        public void OnStart()
        {
            try
            {
                CSPLoginSet.CheckLoginSet();

                CSPCallLogin login = new CSPCallLogin(CSPLoginSet.LoginId, CSPLoginSet.Password);
                string cookie = login.Login();

                CSPCallInfoFromExcel excel = new CSPCallInfoFromExcel();
                CallInfoList addcalllist = excel.GetCallList(CSPLoginSet.ExcelFile);

                CSPCallPost post = new CSPCallPost()
                {
                    StartDate = CSPLoginSet.StartDate,
                    EndDate = CSPLoginSet.EndDate,
                    DayCalls = CSPLoginSet.DayCalls,
                    AddCallInfoList = addcalllist
                };

                DelLogFile();

                post.AddCalls(cookie);
                CSPLogger.Output("\r\n= = = = = = = = = = = = =\r\n");


                CSPCallClose callclose = new CSPCallClose()
                {
                    CloseCallList = post.CloseCallInfoList,
                    CallInfoTimeList = addcalllist.TimeInfo
                };

                callclose.CloseCall(cookie);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void DelLogFile()
        {
            try
            {
                if (File.Exists(CSPLoginSet.LogTxtFile))
                {
                    File.Delete(CSPLoginSet.LogTxtFile);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
