using System;

namespace Autransoft.SendAsync.Mock.Lib.Extensions
{
    internal static class ParamsExtension
    {
        internal static object Default(this Type type)
        {
            if (type == typeof(short))
                return default(short);

            if (type == typeof(ushort))
                return default(ushort);

            if (type == typeof(int))
                return default(int);

            if (type == typeof(uint))
                return default(uint);

            if (type == typeof(long))
                return default(long);

            if (type == typeof(ulong))
                return default(ulong);

            if (type == typeof(float))
                return default(float);

            if (type == typeof(double))
                return default(double);

            if (type == typeof(decimal))
                return default(decimal);

            if (type == typeof(string))
                return default(string);

            if (type == typeof(char))
                return default(char);

            return null;
        }
    }
}