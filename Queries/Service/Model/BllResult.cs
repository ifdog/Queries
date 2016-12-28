using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Enums;
using Common.Structure;

namespace Service.Model
{
	public class BllResult
	{
		public ReusltPostBody ReusltPostBody { get; private set; }
		public string Token { get; set; }
		public List<ItemPostBody> ItemPostBodies { get; set; }

		public BllResult(ResultCode resultCode)
		{
			this.ReusltPostBody = CreateInstance(resultCode);
		}

		public BllResult(string result)
		{
			this.ReusltPostBody = CreateInstance(result);
		}

		public BllResult(Exception e)
		{
			this.ReusltPostBody = CreateInstance(e);
		}

		public static ReusltPostBody CreateInstance(ResultCode resultCode)
		{
			return new ReusltPostBody()
			{
				Result = resultCode.ToString(),
				ResultCode = Convert.ToInt32(resultCode)
			};
		}

		public static ReusltPostBody CreateInstance(string result)
		{
			return new ReusltPostBody
			{
				Result = result,
				ResultCode = 999
			};
		}

		public static ReusltPostBody CreateInstance(Exception e)
		{
			return new ReusltPostBody
			{
				Result = $"Server side exception :{e.Message}",
				ResultCode = 999
			};
		}
	}
}
