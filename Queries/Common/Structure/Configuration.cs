namespace Common.Structure
{
	public class Configuration
	{
		/// <summary>
		/// 0b(ServerOn)(ClientOn)
		/// 0b01=_C=1
		/// 0b10=S_=2
		/// 0b11=SC=3
		/// </summary>
		public int RunMode { get; set; }
		public string ServerPath { get; set; }
		public int ServerPort { get; set; }
		public string RequestPath { get; set; }
		public int RequestPort { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		/// <summary>
		/// true:Embeded
		/// false:Process.Start
		/// </summary>
		public bool ServiceStartMode { get; set; }
	}
}
