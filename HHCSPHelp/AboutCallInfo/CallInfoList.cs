using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HHCSPHelp.AboutCallInfo
{
    internal class CallInfoList
    {
        #region 屬性
        private List<string> _company;
        public List<string> Company
        {
            get => _company;
            set => _company = value;
        }

        private List<string> _location;
        public List<string> Location
        {
            get => _location;
            set => _location = value;
        }

        private List<string> _contactPerson;
        public List<string> ContactPerson
        {
            get => _contactPerson;
            set => _contactPerson = value;
        }

        public List<CallInfoSymptom> SymptomInfo { get; set; }

        public List<CallInfoTime> TimeInfo { get; set; }

        #endregion

        #region 構造函數 
        internal CallInfoList()
        {
            _company = new List<string>();
            _contactPerson = new List<string>();
            _location = new List<string>();
            SymptomInfo = new List<CallInfoSymptom>();
            TimeInfo = new List<CallInfoTime>();
        }
        #endregion

        public int Count()
        {
            return SymptomInfo.Count;
        }

        #region Get add call info at index
        public string GetRequestTypeAt(int index)
        {
            return SymptomInfo[index].RequestType;
        }

        public string GetSymptomAt(int index)
        {
            return SymptomInfo[index].Symptom;
        }

        public string GetContactPersonAt(int index)
        {
            return ContactPerson[index];
        }

        public string GetCompanyAt(int index)
        {
            return Company[index];
        }

        public string GetLocationAt(int index)
        {
            return Location[index];
        }

        public string GetRequestDateAt(int index)
        {
            return SymptomInfo[index].RequestDate;
        }

        public string GetServiceDescriptionAt(int index)
        {
            return SymptomInfo[index].ServiceDescription;
        }

        public string GetRequestNOAt(int index)
        {
            return SymptomInfo[index].RequestNO;
        }
        #endregion
    }
}
