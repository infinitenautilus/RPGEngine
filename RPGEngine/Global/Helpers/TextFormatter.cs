using RPGEngine.Global.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGEngine.Global.Helpers
{
    public static class TextFormatter
    {
        
        public static string FormatListWithCommas(List<string> items)
        {
            if(items == null || items.Count == 0)
            {
                return string.Empty;
            }

            if (items.Count == 1)
                return items[0];

            var allButLast = items.Take(items.Count - 1);
            var last = items.Last();

            return string.Join(", ", allButLast) + ", and " + last + ".";

        }
    }
}
