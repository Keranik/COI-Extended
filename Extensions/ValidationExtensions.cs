using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COIExtended.Extensions;
public static class ValidationExtensions
{
    public static T CheckNotNull<T>(this T obj, string paramName = null) where T : class
    {
        if (obj == null)
        {
            throw new ArgumentNullException(paramName ?? nameof(obj), "Value cannot be null.");
        }
        return obj;
    }
}
