using System.Collections.Generic;
using Common.Enums;
using Common.Static;

namespace Common.Structure.Base
{
	public abstract class BaseResult
	{
		public abstract int ResultCode { get; set; }
        public abstract string Information { get; set; }
	}
}
