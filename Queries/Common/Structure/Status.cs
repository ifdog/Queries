using System;
using Common.Structure.Base;

namespace Common.Structure
{
    public class Status:BaseStatus
    {
        public override byte[] Id { get; set; }
        public override DateTime LogTime { get; set; }
        public override long ItemCount { get; set; }
        public override long UserCount { get; set; }
        public override long QueryCount { get; set; }
    }
}
