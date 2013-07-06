using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using Webby.Common;
using Webby.Utilities;
using System.IO;

namespace Webby
{
	public class WebBot
	{
		public CookieContainer Cookies { get; set; }
		public WBOptions Options { get; set; }
		public string LastURL { get; set; }

		public WebBot()
		{
			Options = WBOptions.DEFAULT.DeepClone();
			Cookies = new CookieContainer();
			LastURL = null;
		}

		public static WBResponse LoadPage(string url)
		{
			WebBot wb = new WebBot();
			return wb.Load(url);
		}

		public WBResponse Load(string url)
		{
			WBRequest wbReq = new WBRequest(url);

			return Load(wbReq);
		}

		public WBResponse Load(WBRequest wbReq)
		{
			LastURL = wbReq.URL;

			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(wbReq.URL);
			httpRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			httpRequest.KeepAlive = false;
			httpRequest.Pipelined = false;
			httpRequest.Proxy = Options.Proxy;
			httpRequest.ReadWriteTimeout = Options.ReadWriteTimeout;
			httpRequest.UserAgent = Options.UserAgent.Agent;

			if (Options.SendCookies)
			{
				httpRequest.CookieContainer = Cookies.DeepClone();
			}

			if (wbReq.Method == "GET")
			{
				httpRequest.Method = wbReq.Method;
			}
			else if (wbReq.Method == "POST")
			{
				httpRequest.Method = wbReq.Method;
				httpRequest.ContentType = "application/x-www-form-urlencoded";
				using (StreamWriter writer = new StreamWriter(httpRequest.GetRequestStream()))
				{
					foreach (KeyValuePair<string, string> pair in wbReq.Parameters)
					{
						writer.Write(pair.Key + "=" + pair.Value + "&");
					}
				}
			}
			else
			{
				throw new NotImplementedException();
			}

			if (wbReq.Referer == null)
			{
				httpRequest.Referer = null;
			}
			else if (wbReq.Referer == WBOptions.AUTO_REFERER)
			{
				httpRequest.Referer = (LastURL == null) ? null : LastURL;
			}
			else
			{
				httpRequest.Referer = wbReq.Referer;
			}

			WBResponse wbResp = WBResponse.LoadResponse(httpRequest);
			wbResp.Other = wbReq.Other;

			if (Options.SaveCookies)
			{
				Cookies = httpRequest.CookieContainer;
			}

			return wbResp;
		}
	}
}