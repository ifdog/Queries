using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Static
{
	public static class Log
	{
		private static readonly TraceSwitch MyTraceSwitch = new TraceSwitch("traceSwitch", string.Empty);
		static Log()
		{
			Trace.Listeners.Add(new TextWriterTraceListener("Log.txt"));
			Trace.AutoFlush = true;
			MyTraceSwitch.Level = TraceLevel.Verbose;

		}


		public static void Here(Exception e)
		{
			if (!MyTraceSwitch.TraceError) return;
			Trace.IndentLevel = 0;
			Trace.TraceError($"\r\n{DateTime.Now}>=====Error=====<");
			Trace.IndentLevel = 1;
			Trace.TraceError($"Source: {e.Source}");
			Trace.TraceError($"Message: {e.Message}");
			Trace.TraceError($"InnerSource: {e.InnerException?.Source}");
			Trace.TraceError($"InnerMessage: {e.InnerException?.Message}");
		}

		public static void Here(string msg)
		{
			if (!MyTraceSwitch.TraceInfo) return;
			Trace.IndentLevel = 0;
			Trace.TraceInformation($"\r\n{DateTime.Now}>=====Info=====<");
			Trace.IndentLevel = 1;
			Trace.TraceInformation(msg);
		}

	}
}
