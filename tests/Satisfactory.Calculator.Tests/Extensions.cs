using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satisfactory.Calculator.Tests
{
    public static class Extensions
    {
        public static string GetCode(this StackItem item)
        {
            var parentCode = item.GetParentCode();
            if (string.IsNullOrEmpty(parentCode))
                return $"{item.Code}";
            else
                return $"{parentCode}.{item.Code}";
        }

        public static string GetParentCode(this StackItem item)
        {
            var chain = new List<StackItem>();
            var current = item.parent;
            while(current != null)
            {
                chain.Add(current);
                current = current.parent;
            }

            chain.Reverse();

            return string.Join(".", chain
                .Select(r => r.Recipe));
        }
    }
}
