using System;

namespace Common.Static
{
	public static class Identify
	{
		internal static int M = 0;

		public static int NewId()
		{
			//M = M > 255 ? 0 : M + 1;
			//return UnixTimeStamp.ToUnixTimaStamp(DateTime.Now) << 8 + M;
			byte[] buffer = Guid.NewGuid().ToByteArray();
			 return BitConverter.ToInt32(buffer, 0);

		}
	}
}
