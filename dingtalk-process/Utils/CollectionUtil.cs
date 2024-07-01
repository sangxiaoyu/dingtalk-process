using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dingtalk_process
{
    public abstract class CollectionUtil
    {
        public CollectionUtil() { }

        public static bool isEmpty(ICollection collection)
        {
            if (collection is null || collection.Count == 0)
                return false;
            else return true;
        }
    }
}
