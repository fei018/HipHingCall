using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HHCSPHelp.AboutCallInfo;

namespace HHCSPHelp
{
    public class CSPTest
    {
        public void Run()
        {
            CSPCallInfoFromExcel csp = new CSPCallInfoFromExcel();
            CallInfoList listInfo = csp.GetCallList(CSPLoginSet.ExcelFile);
            CSPRandom r = new CSPRandom(listInfo.Count());
            for (int i = 0; i < listInfo.Count(); i++)
            {
                int j = r.Next();
                Console.WriteLine(listInfo.ContactPerson.ElementAt(j));
            }   

        }
    }
}
