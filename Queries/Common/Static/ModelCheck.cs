using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Structure;

namespace Common.Static
{
    public static class ModelCheck
    {
        public static bool Check(ItemModel itemModel)
        {
            if (string.IsNullOrEmpty(itemModel.Name) && string.IsNullOrEmpty(itemModel.Model) &&
                string.IsNullOrEmpty(itemModel.Spec))
            {
                return false;
            }
            return true;
        }
    }
}
