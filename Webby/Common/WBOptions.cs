using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Webby.Common
{
	[Serializable]
	public class WBOptions
	{
		public static WBOptions DEFAULT = new WBOptions();

		public const string AUTO_REFERER = "";

		public WBUserAgent UserAgent { get; set; }
		public WebProxy Proxy { get; set; }

		public int ReadWriteTimeout { get; set; }

		public bool SaveCookies { get; set; }
		public bool SendCookies { get; set; }

		static WBOptions()
		{
			DEFAULT.UserAgent = WBUserAgent.FIREFOX;
			DEFAULT.Proxy = null; // No proxy, overrides potential global proxy
			
			DEFAULT.ReadWriteTimeout = 1000 * 60;

			DEFAULT.SaveCookies = true;
			DEFAULT.SendCookies = true;
		}
	}
}