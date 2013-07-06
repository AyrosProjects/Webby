using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webby
{
	public class WBRequest
	{
		public string URL { get; set; }
		public string Method { get; set; }
		public Dictionary<string, string> Parameters { get; set; }
		public string Referer { get; set; }
		public object Other { get; set; }

		public WBRequest(string url)
		{
			URL = url;
			Method = "GET";
			Parameters = new Dictionary<string, string>();
			Referer = null;
		}

		public WBRequest(string url, string method, Dictionary<string, string> parameters)
		{
			URL = url;
			Method = method;
			Parameters = parameters;
		}
	}
}
