using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Webby.Common
{
	public class WBError
	{
		public enum ErrorCodes
		{
			NONE,
			WEB_ERROR,
		};

		public ErrorCodes ErrorCode = ErrorCodes.NONE;
		public Exception Error = null;

		public WBError(ErrorCodes ec)
		{
			ErrorCode = ec;
		}

		public WBError(ErrorCodes ec, Exception e)
		{
			ErrorCode = ec;
			Error = e;
		}
	}
}
