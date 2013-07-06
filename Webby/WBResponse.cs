using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Webby.Common;

namespace Webby
{
	public class WBResponse
	{
		public string URL { get; set; }
		public bool FromCache { get; set; }
		public object Other { get; set; }

		public virtual byte[] RawData
		{
			get
			{
				return _rawData;
			}
			set
			{
				_rawData = value;
			}
		}
		private byte[] _rawData;

		public virtual bool IsNull
		{
			get
			{
				return _rawData == null;
			}
		}

		public string Data
		{
			get
			{
				return IsNull ? null : Encoding.ASCII.GetString(RawData);
			}
			set
			{
				RawData = (value == null) ? null : Encoding.ASCII.GetBytes(value);
			}
		}

		public WBError Error { get; set; }

		protected WBResponse()
		{
			RawData = null;
			URL = null;
			FromCache = false;
		}

		public static WBResponse LoadResponse(HttpWebRequest req)
		{
			WBResponse wbResp = new WBResponse();
			wbResp.URL = req.RequestUri.OriginalString;
			wbResp.Error = new WBError(WBError.ErrorCodes.NONE);

			try
			{
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

				byte[] data;
				using (Stream input = resp.GetResponseStream())
				{
					MemoryStream memStream = new MemoryStream();
					input.CopyTo(memStream);
					data = memStream.ToArray();
				}

				wbResp.RawData = data;
			}
			catch (WebException we)
			{
				wbResp.Error = new WBError(WBError.ErrorCodes.WEB_ERROR, we);
			}

			return wbResp;
		}
	}
}