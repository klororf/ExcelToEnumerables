using System;
using Klororf.ExcelReader.Enumerator.Exceptions;

namespace Klororf.ExcelReader.Enumerator.Extensions
{
    public static class ConversionsExtensions
    {

        public static object? CastObject(this object? obj, Type t)
        {
            if (typeof(int) == t)
                return int.Parse(obj.ToString() ?? "0");
            if (typeof(float) == t)
                return float.Parse(obj.ToString() ?? "0.0");
            if (typeof(double) == t)
                return double.Parse(obj.ToString() ?? "0");
            if (typeof(string) == t)
                return obj.ToString() ?? string.Empty;
            if (typeof(bool) == t)
                return bool.Parse(obj.ToString() ?? "0");
            else
                throw new TypeNotImplementedException(t);
        }
    }
}

