using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Text;
using Webby.Common;
using Webby.Utilities;

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
			httpRequest.Method = wbReq.Method;
			httpRequest.Pipelined = false;
			httpRequest.Proxy = Options.Proxy;
			httpRequest.ReadWriteTimeout = Options.ReadWriteTimeout;
			httpRequest.UserAgent = Options.UserAgent.Agent;

			if (Options.SendCookies)
			{
				httpRequest.CookieContainer = Cookies.DeepClone();
			}

			if (wbReq.Method != "GET")
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