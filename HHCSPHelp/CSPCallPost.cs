﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Text.RegularExpressions;
using HHCSPHelp.AboutCallInfo;
using System.IO;

namespace HHCSPHelp
{
    internal class CSPCallPost
    {
        #region 構造函數 字段
        public CSPCallPost()
        {
            if (File.Exists(CSPLoginSet.HKHolidayFile))
            {
                _hkHolidayFileExists = true;
            }
            else
            {
                _hkHolidayFileExists = false;
            }
        }
        //start date format: dd/mm/y
        public string StartDate { get; set; }
        //end date format: dd/mm/y
        public string EndDate { get; set; }
        public string DayCalls { get; set; }
        private bool _hkHolidayFileExists = false;

        public CallInfoList AddCallInfoList { get; set; }

        private List<CloseCallInfo> _closecallinfolist;
        public List<CloseCallInfo> CloseCallInfoList
        {
            get
            {
                try
                {
                    if (_closecallinfolist.Count() <= 0)
                    {
                        throw new Exception("CloseCallInfo List is null, no call need close.");
                    }
                    
                    return _closecallinfolist;
                }
                catch (Exception)
                {
                    throw;
                }
            }
            set => _closecallinfolist = value;
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

        #region Public Add Calls
        public void AddCalls(string cspCookie)
        {
            AddCallFromExcel(cspCookie);
        } 
        #endregion

        #region post call source from Excel file
        private void AddCallFromExcel(string cspCookie)
        {
            string postdata;
            _closecallinfolist = new List<CloseCallInfo>();         
            try
            {
                //AddCallInfoList = excel.GetCallList(CSPLoginSet.ExcelFile); //從 excel 文件獲取 CallInfo List 給 實列字段

                //從開始日期 結束日期 獲得外循環值
                int end = int.Parse(EndDate.Split('/')[0]);
                int st = int.Parse(StartDate.Split('/')[0]);
                int month = int.Parse(StartDate.Split('/')[1]);
                int year = int.Parse(StartDate.Split('/')[2]);

                CSPRandom random = new CSPRandom(AddCallInfoList.Count());
                for (int i = st; i <= end; i++)
                {
                    string requestDate = i.ToString() + "/" + month.ToString() + "/" + year.ToString(); // day/month/year

                    // 是周末就跳過這次循環
                    if (IsWeekends(i, month,year)) continue;
                    // HK公眾假期跳過這次循環
                    if (_hkHolidayFileExists)
                    {
                        if (IsHKHolidays(i, month, year)) continue;
                    }

                    //一天循環call的數目                   
                    for (int j = 0; j < int.Parse(DayCalls); j++)
                    {
                        int ranInt = random.Next();
                        CloseCallInfo closeCall = new CloseCallInfo();
                        
                        AddCallInfoList.SymptomInfo[ranInt].RequestDate = HttpUtility.UrlEncode(requestDate);

                        postdata = _postBody + GetPostData(AddCallInfoList,ranInt,random); // post call data

                        //開始 post add call httpRequest
                        //Post1(cspCookie, postdate);
                        HttpResult result = Post2(cspCookie, postdata);
                        result = Post3(cspCookie, result.RedirectUrl);
                        //開始 post add call httpRequest\\

                        //從跳轉地址取 requestNO, 取不到就跳到下次循環
                        if (IsGetJobRequestNO(result.RedirectUrl, requestDate, out string requestNO))
                        {
                            //給 CloseCallInfo 填充值
                            closeCall.RequestNO = requestNO; //從 跳轉Url 獲取 JosRequestNo
                            closeCall.RequestDate = HttpUtility.UrlEncode(requestDate);
                            closeCall.ServiceDescription = AddCallInfoList.GetServiceDescriptionAt(ranInt);
                            _closecallinfolist.Add(closeCall);
                            //給 CloseCallInfo 填充值\\
                        }
                        else continue;                      

                        AddCallInfoList.SymptomInfo[ranInt].RequestNO = requestNO;
                        PostLogOutput(AddCallInfoList,ranInt);                 
                        Thread.Sleep(500);
                    }                   
                }               
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 填充 PostData 數據
        /// </summary>
        /// <param name="list"></param>
        /// <param name="rindex"></param>
        /// <returns></returns>
        private string GetPostData(CallInfoList list, int rindex, CSPRandom ran)
        {
            string postdata = "&sle_date=" + list.GetRequestDateAt(rindex)
                                    + "&sle_request=" + list.GetRequestTypeAt(rindex)
                                    + "&sle_contact=" + list.GetContactPersonAt(ran.Next())
                                    + "&sle_company=" + list.GetCompanyAt(ran.Next())
                                    + "&sle_location=" + list.GetLocationAt(ran.Next())
                                    + "&sle_symptom=" + list.GetSymptomAt(rindex)
                                    + "&sle_assignto=" + CSPLoginSet.Assignto;
            return postdata;
        }

        /// <summary>
        /// 檢查 日期是否是 假期
        /// </summary>
        /// <param name="day"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        private bool IsWeekends(int day, int month, int year)
        {
            try
            {
                DateTime dateTime = new DateTime(year, month, day);
                if (dateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    CSPLogger.Output(dateTime.ToShortDateString() + " is Saturday.");
                    return true;
                }
                if (dateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    CSPLogger.Output(dateTime.ToShortDateString() + " is Sunday.");
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool IsHKHolidays(int day, int month, int year)
        {
            List<DateTime> dates = new List<DateTime>();
            StreamReader reader = new StreamReader(CSPLoginSet.HKHolidayFile);
            string r;
            while (!reader.EndOfStream)
            {
                r = reader.ReadLine().Trim();
                if (string.IsNullOrWhiteSpace(r)) continue;
                if (DateTime.TryParse(r, out DateTime d))
                {
                    dates.Add(d);
                }
                else
                {
                    CSPLogger.Output($"Error: {r}: HKHoliday date format wrong.");
                }
            }
            if (dates.Count <= 0) return false;
            DateTime dateTime = new DateTime(year, month, day);
            foreach (DateTime d in dates)
            {
                if (dateTime.Date.CompareTo(d.Date) == 0)
                {
                    CSPLogger.Output($"{dateTime.ToShortDateString()} is HKHoliday.");
                    return true;
                }
            }
            return false;
        }

        private string _matchstring = "<B>.*</B>";
        /// <summary>
        /// 匹配 location _matchstring,獲取 JobRequestNo
        /// </summary>
        /// <param name="redirecturl"></param>
        /// <returns></returns>
        private bool IsGetJobRequestNO(string redirecturl, string requestdate, out string requestno)
        {
            try
            {
                Match match = Regex.Match(Regex.Match(redirecturl.ToUpper(), _matchstring).Value, ">.*<");
                if (!match.Success)
                {
                    //throw new Exception($"Error: Can't get JobRequestNo, add call fail.\r\n Assignto:{CSPLoginSet.Assignto} wrong ?");
                    CSPLogger.Output($"Error: {requestdate}\tCan't get JobRequestNo, add a call fail.");
                    requestno = null;
                    return false;
                }
                else
                {
                    requestno = match.Value.Trim('>', '<');
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 寫Log
        /// </summary>
        /// <param name="job"></param>
        private void PostLogOutput(CallInfoList list, int index)
        {
            string put = $"Add:\t{HttpUtility.UrlDecode(list.GetRequestNOAt(index))}\t{HttpUtility.UrlDecode(list.GetRequestDateAt(index))}" +
                         $"\t{list.GetCompanyAt(index)}\t{HttpUtility.UrlDecode(list.GetContactPersonAt(index))}\t{HttpUtility.UrlDecode(list.GetLocationAt(index))}" +
                         $"\t{list.GetRequestTypeAt(index)}\t{HttpUtility.UrlDecode(list.GetSymptomAt(index))}";
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
