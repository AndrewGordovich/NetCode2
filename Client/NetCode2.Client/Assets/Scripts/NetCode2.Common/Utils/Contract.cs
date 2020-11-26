using System;

namespace NetCode2.Common.Utils
{
    public static class Contract
    {
        public static void Requires<TException>(bool condition, string format, params object[] args)
            where TException : Exception
        {
            if (!condition)
            {
                var exception = (TException)Activator.CreateInstance(typeof(TException), string.Format(format, args));
                throw exception;
            }
        }
    }
}