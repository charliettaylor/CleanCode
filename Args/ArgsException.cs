using System;

namespace Args
{
    public enum ErrorCode
    {
        InvalidFlag, RedundantFlag
    }

    public class ArgsException : Exception
    {
        public ArgsException(string message)
        {
            
        }

        public ArgsException(ErrorCode code)
        {
            
        }

        public ArgsException(string message, ErrorCode code)
        {
            
        }
    }
}