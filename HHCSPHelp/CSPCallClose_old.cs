using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;

namespace HHCSPHelp
{
    internal class CSPCallClose_old
    {
        #region 變量 Link
        private const string _bodyClose = "formpage=1&sle_seqno=1&sle_indicator=I&sle_indicator_follow=I&sle_solution=&sle_assignremark=&pagename=&sle_status=C&sle_docsts=N";

        private const string _userAgent = @"Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 10.0; WOW64; Trident/7.0; .NET4.0C; .NET4.0E; .NET CLR 2.0.50727; .NET CLR 3.0.30729; .NET CLR 3.5.30729)";
        private const string _httpAccept = @"image/gif, image/jpeg, image/pjpeg, application/x-ms-application, application/xaml+xml, application/x-ms-xbap, */*";
        private const string _host = "hiphingweb03.hiphing.com.hk";
        private string _closeCallLink1 = @"http://hiphingweb03.hiphing.com.hk/hhcsp/servlet/com.hiphing.csp.servlets.cspm0006.ServiceSave";
        #endregion

        #region Public Cloes Call
        public void CloseCall(string cspCookie, List<JobRequest> joblist)
        {
            try
            {
                List<JobRequest> jList = FilterJobRequestList(joblist);
                string postdata;
                foreach (JobRequest job in jList)
                {
                    postdata = "&sle_requestno=" + job.RequestNO
                             + "&sle_requestdate=" + job.RequestDate
                             + "&sle_scheduledate=" + job.RequestDate
                             + "&sle_scheduletime=" + job.ScheduleTime
                             + "&sle_servedate=" + job.RequestDate
                             + "&sle_servetime1=" + job.ServeTime1
                             + "&sle_servetime2=" + job.ServeTime2
                             + "&sle_description=" + job.ServiceDescription
                             + "&sle_staff=" + job.AssignTo;
                    postdata = _bodyClose + postdata;
                    Close1(cspCookie, postdata);
                    PostLogOutput(job);
                    Thread.Sleep(500);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PostLogOutput(JobRequest job)
        {
            string put = $"Close:\t{HttpUtility.UrlDecode(job.RequestDate)}\t{job.RequestNO}";
            CSPLogger.Output(put);
        }
        #endregion


        // Fiddler 抓包用
        private string _proxyIP = null; // "127.0.0.1:8888";
        #region Close call
        private List<JobRequest> FilterJobRequestList(List<JobRequest> joblist)
        {
            List<JobRequest> newlist = new List<JobRequest>();
            try
            {
                for (int i = 0; i < joblist.Count; i++)
                {
                    if (!string.IsNullOrWhiteSpace(joblist[i].RequestNO))
                    {                        
                        newlist.Add(joblist[i]);
                    }
                }
                return newlist;
            }
            catch (Exception) { throw; }
        }

        private void Close1(string cspCookie, string data)
        {
            HttpHelper http = new HttpHelper();

            HttpItem item = new HttpItem()
            {
                URL = _closeCallLink1,
                Method = "post",
                KeepAlive = true,
                ContentType = "application/x-www-form-urlencoded",
                Referer = "http://hiphingweb03.hiphing.com.hk/hhcsp/CSPM0006c.jsp",
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
                http.GetHtml(item);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
