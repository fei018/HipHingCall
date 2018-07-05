using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HHCSPHelp.AboutCallInfo
{
    internal class CallInfoSymptom
    {
        
        public string RequestType { get; set; }

        public string RequestNO { get; set; }

        //ScheduleDate
        private string _requestdate;
        public string RequestDate
        {
            get => _requestdate;
            set => _requestdate = value;
        }

        private string _symptom;
        public string Symptom
        {
            get => _symptom;
            set => _symptom = value;
        }

        private string _description;
        public string ServiceDescription
        {
            get => _description;
            set => _description = value;
        }
    }
}
