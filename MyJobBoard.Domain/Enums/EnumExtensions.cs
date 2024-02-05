using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyJobBoard.Domain.Enums
{
    public static class EnumExtensions
    {

        public static bool Equal<TEnum>(this TEnum enumValue, TEnum otherValue) where TEnum : Enum
        {
            return enumValue.Equals(otherValue);
        }
       
    }
}
