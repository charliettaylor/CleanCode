using System;

namespace Args
{
    public interface IHandler
    {
        public void Set(string arg);
    }

    public class BooleanHandler : IHandler
    {
        private bool _value;
    
        public void Set(string arg)
        {
            _value = arg.Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
        }

        public static bool Get(IHandler handler)
        {
            if (handler is BooleanHandler booleanHandler)
            {
                return booleanHandler._value;
            }

            return false;
        }
    }

    public class IntHandler : IHandler
    {
        private int _value;
    
        public void Set(string arg)
        {
            int.TryParse(arg, out var parsed);
            _value = parsed;
        }
    
        public static int Get(IHandler handler)
        {
            if (handler is IntHandler intHandler)
            {
                return intHandler._value;
            }

            return -1;
        }
    }

    public class DecimalHandler : IHandler
    {
        private decimal _value;

        public void Set(string arg)
        {
            decimal.TryParse(arg, out var parsed);
            _value = parsed;
        }
    
        public static decimal Get(IHandler handler)
        {
            if (handler is DecimalHandler decimalHandler)
            {
                return decimalHandler._value;
            }

            return -1.0m;
        }
    }

    public class StringHandler : IHandler
    {
        private string _value = string.Empty;
    
        public void Set(string arg)
        {
            _value = arg;
        }
    
        public static string Get(IHandler handler)
        {
            if (handler is StringHandler stringHandler)
            {
                return stringHandler._value;
            }

            return "";
        }
    }
}