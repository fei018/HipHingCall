using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;

namespace HHCSPHelp
{
    internal class CSPCallPost_old
    {
        #region 構造函數 字段
        private string _assignto;
        public string AssignTo {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_assignto)) throw new Exception("AssignTo is null");
                    return HttpUtility.UrlEncode(_assignto);
                }
                catch (Exception) { throw; }    
            }
            set => _assignto = value;
        }
       
        //start date format: dd/mm/y
        private string _startdate;
        public string StartDate {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_startdate)) throw new Exception("StartDate is null");
                    return _startdate;
                }
                catch (Exception) { throw; }
            }
            set => _startdate = value;
        }

        //end date format: dd/mm/y
        private string _enddate;
        public string EndDate {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_enddate)) throw new Exception("EndDate is null");
                    return _enddate;
                }
                catch (Exception) { throw; }
            }
            set => _enddate = value;
        }

        private string _daycalls;
        public string DayCalls {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(_daycalls)) throw new Exception("DayCalls is null");
                    return _daycalls;
                }
                catch (Exception) { throw; }
            }
            set => _daycalls = value;
        }

        private List<JobRequest> _jobRequestInfoList;
        internal List<JobRequest> JobRequestInfoList {
            get
            {
                try
                {
                    if (_jobRequestInfoList == null) throw new Exception("JobRequestInfoList is null on Post Call.");
                    if (_jobRequestInfoList.Count <= 0) throw new Exception("JobRequestInfoList count is 0 on Send Call.");
                    return _jobRequestInfoList;
                }
                catch (Exception) { throw; }
            }
            set => _jobRequestInfoList = value;
        }
        #endregion

        #region 變量 Link
        private const string _postBody = "formpage=1&savepage=servlet%2Fcom.hiphing.csp.servlets.cspm0001.JobRequestSave&savebutton=Y" +
                            "&httplocation=servlet%2Fcom.hiphing.csp.servlets.cspm0001.JobRequestSave&inputAction=new&followjobstatus=&languageId=E" +
                            "&sle_requestno=&sle_jobstatus=N&sle_time=08%3A00&sle_source=P&sle_servtype=PC&sle_contactcode=&sle_department=SITE" +
                            "&sle_tel=&sle_classification=&sle_mobile=&sle_email=&sle_supp_id=&sle_handle=G&sle_engineer=Y&sle_indicator=I" +
                            "&sle_docsts=N&sle_vdrfllw_1=&sle_servnum_1=&sle_hotline_1=&sle_vdrfllw_2=&sle_servnum_2=&sle_hotline_2=&sle_vdrfllw_3=&sle_servnum_3=&sle_hotline_3=";

        private const string _userAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729)";
        private const string _httpAccept = @"image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/xaml+xml, application/x-ms-xbap, */*";
        private const string _host = "hiphingweb03.hiphing.com.hk";
        private const string _postLink1 = "http://hiphingweb03.hiphing.com.hk/hhcsp/CSPM0001a.jsp";
        private const string _postLink2 = "http://hiphingweb03.hiphing.com.hk/hhcsp/servlet/com.hiphing.csp.servlets.cspm0001.JobRequestPage1";
        #endregion

        #region Public Send Calls
        public void AddCalls(string cspCookie)
        {
            AddCallFromExcel(cspCookie);
        } 
        #endregion

        #region Add call source from Excel file
        private void AddCallFromExcel(string cspCookie)
        {
            string postdate;
            CSPJobFromExcel excel = new CSPJobFromExcel();
            _jobRequestInfoList = excel.GetCallList(CSPLoginSet.ExcelFile); //從 excel 文件獲取 JobrequestInfo List 給 實列字段
            FillUserId2JobList(); //給 joblist元素的AssignTo 填充UserId
            try
            {
                //從開始日期 結束日期 獲得外循環值
                int end = int.Parse(EndDate.Split('/')[0]);
                int st = int.Parse(StartDate.Split('/')[0]);

                for (int i = st; i <= end; i++)
                {
                    //一天循環call的數目
                    for (int j = 0; j < int.Parse(DayCalls); j++)
                    {
                        JobRequest t = _jobRequestInfoList[0]; //取出 job list 第一個元素,再插入list結尾
                        _jobRequestInfoList.RemoveAt(0);

                        t.RequestDate = i.ToString() + "/" + StartDate.Split('/')[1] + "/" + StartDate.Split('/')[2]; //Request date
                        postdate = "&sle_date=" + t.RequestDate
                                    + "&sle_request=" + t.RequestType
                                    + "&sle_contact=" + t.ContactPerson
                                    + "&sle_company=" + t.Company
                                    + "&sle_location=" + t.Location
                                    + "&sle_symptom=" + t.Symptom
                                    + "&sle_assignto=" + AssignTo;
                        postdate = _postBody + postdate;

                        Post1(cspCookie, postdate);

                        HttpResult result = Post2(cspCookie, postdate);

                        result = Post3(cspCookie, result.RedirectUrl);

                        string location = result.RedirectUrl;
                        t.RequestNO = GetJobRequestNO(location);

                        _jobRequestInfoList.Add(t);
                        PostLogOutput(t);                        
                        Thread.Sleep(500);
                    }
                }                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GetJobRequestNO(string location)
        {
            Match match = Regex.Match(Regex.Match(location, "<B>.*</B>").Value, ">.*<");
            return match.Value.Trim('>', '<');
        }

        private void FillUserId2JobList()
        {
            foreach (JobRequest j in _jobRequestInfoList)
            {
                j.AssignTo = this.AssignTo;
            }
        }

        private void PostLogOutput(JobRequest job)
        {
            string put = $"Send:\t{HttpUtility.UrlDecode(job.RequestDate)}\t{job.Company}\t{HttpUtility.UrlDecode(job.ContactPerson)}\t{job.Location}\t{job.RequestType}\tAssignto:{this.AssignTo}\t{HttpUtility.UrlDecode(job.Symptom)}";
            CSPLogger.Output(put);
        }

        #endregion

        // Fiddler 抓包用
        private string _proxyIP = null; //"127.0.0.1:8888";

        #region Post Call request
        private HttpResult Post1(string cspCookie, string data)
        {
            HttpHelper http = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                URL = _postLink1,
                Method = "post",
                KeepAlive = true,
                ContentType = "application/x-www-form-urlencoded",
                Referer = _postLink1,
                Allowautoredirect = false,
                Accept = _httpAccept,
                UserAgent = _userAgent,
                Host = _host,
                Cookie = cspCookie,
                ProxyIp = _proxyIP
            };
            item.Header.Add("Accept-Language: zh-HK");
            item.Header.Add("Accept-Encoding: gzip, deflate");
            item.Header.Add("Pragma: no-cache");
            item.Postdata = data;
            try
            {
                return http.GetHtml(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HttpResult Post2(string cspCookie, string data)
        {
            HttpHelper http = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                URL = _postLink2,
                Method = "post",
                KeepAlive = true,
                ContentType = "application/x-www-form-urlencoded",
                Referer = _postLink1,
                Allowautoredirect = false,
                Accept = _httpAccept,
                UserAgent = _userAgent,
                Host = _host,
                Cookie = cspCookie,
                ProxyIp = _proxyIP
            };
            item.Header.Add("Accept-Language: zh-HK");
            item.Header.Add("Accept-Encoding: gzip, deflate");
            item.Header.Add("Pragma: no-cache");
            item.Postdata = data;
            try
            {
                return http.GetHtml(item);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private HttpResult Post3(string cspCookie, string location)
        {
            HttpHelper http = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                URL = location,
                Method = "get",
                KeepAlive = true,
                //ContentType = "application/x-www-form-urlencoded",
                Referer = _postLink1,
                Allowautoredirect = false,
                Accept = _httpAccept,
                UserAgent = _userAgent,
                Host = _host,
                Cookie = cspCookie,
                ProxyIp = _proxyIP
            };
            item.Header.Add("Accept-Language: zh-HK");
            item.Header.Add("Accept-Encoding: gzip, deflate");
            item.Header.Add("Pragma: no-cache");
            try
            {
                return http.GetHtml(item);
            }
            catch (Exception)
            {
                throw;
            }
        } 
        #endregion

    }
}
