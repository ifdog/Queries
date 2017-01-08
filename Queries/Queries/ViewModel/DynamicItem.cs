using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Queries.ViewModel
{
    public class DynamicItem : DynamicObject
    {
        readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            if (!_properties.Keys.Contains(binder.Name))
            {
                _properties.Add(binder.Name, value.ToString());
            }
            return true;
        }
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            return _properties.TryGetValue(binder.Name, out result);
        }
    }
}