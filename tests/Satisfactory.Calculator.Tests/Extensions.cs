using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Satisfactory.Calculator.Tests
{
    public static class Extensions
    {
        public static string GetCode(this Stack<StackItem> RecipeStack)
        {
            return string.Join(".", RecipeStack.Reverse().Select(r => r.Recipe));
        }

        public static string GetParentCode(this Stack<StackItem> RecipeStack)
        {
            return string.Join(".", RecipeStack.Skip(1).Reverse().Select(r => r.Recipe));
        }
    }
}
