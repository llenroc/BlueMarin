﻿using System;

namespace BlueMarin.Extensions
{
	public static class ExceptionExtensions
	{
		public static string Info (this Exception ex, bool showStack = false)
		{
			if (ex == null) 
				return "exception is NULL !";

			string info = "";

			if (ex.Message.IsNullOrBlank ())
				info += "exception message is empty\n";
			else
				info += ex.Message.Substring (0, Math.Min(ex.Message.Length, 150));

			if (showStack && !ex.StackTrace.IsNullOrBlank ())
				info += ex.StackTrace.Substring (0, Math.Min (ex.StackTrace.Length, 150));

			return info;
		}
	}
}

