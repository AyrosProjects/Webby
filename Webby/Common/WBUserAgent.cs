using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webby.Common
{
	[Serializable]
	public class WBUserAgent
	{
		public static WBUserAgent FIREFOX
		{
			get
			{
				return new WBUserAgent("Mozilla/5.0 (Windows NT 6.2; WOW64; rv:16.0.1) Gecko/20121011 Firefox/16.0.1");
			}
		}

		public string Agent { get; set; }

		public WBUserAgent()
		{
			Agent = FIREFOX.Agent;
		}

		public WBUserAgent(string agent)
		{
			Agent = agent;
		}
	}
}