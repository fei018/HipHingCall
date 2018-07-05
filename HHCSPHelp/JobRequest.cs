using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HHCSPHelp
{
    internal class JobRequest
    {
        public string RequestNO { get; set; }

        //public string ScheduleDate { get; set; }
        private string _requestDate;
        public string RequestDate
        {
            get => _requestDate;
            set => _requestDate = HttpUtility.UrlEncode(value);
        }

        private string _assignTo;
        public string AssignTo
        {
            get => _assignTo;
            set => _assignTo = HttpUtility.UrlEncode(value);
        }

        public string Company { get; set; }

        private string _location;
        public string Location
        {
            get => _location;
            set => _location = HttpUtility.UrlEncode(value);
        }

        private string _contactPerson;
        public string ContactPerson
        {
            get => _contactPerson;
            set => _contactPerson = HttpUtility.UrlEncode(value);
        }

        private string _symptom;
        public string Symptom
        {
            get => _symptom;
            set => _symptom = HttpUtility.UrlEncode(value);
        }

        private string _requestType;
        public string RequestType
        {
            get => _requestType;
            set => _requestType = value;
        }

        private string _scheduleTime;
        public string ScheduleTime
        {
            get => _scheduleTime;
            set => _scheduleTime = HttpUtility.UrlEncode(value);
        }

        private string _serveTime1;
        public string ServeTime1
        {
            get => _serveTime1;
            set => _serveTime1 = HttpUtility.UrlEncode(value);
        }

        private string _serveTime2;
        public string ServeTime2
        {
            get => _serveTime2;
            set => _serveTime2 = HttpUtility.UrlEncode(value);
        }

        private string _serviceDescription;
        public string ServiceDescription
        {
            get => _serviceDescription;
            set => _serviceDescription = HttpUtility.UrlEncode(value);
        }

    }
}