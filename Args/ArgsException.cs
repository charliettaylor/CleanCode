using System;

namespace Args
{
    public enum ErrorCode
    {
        InvalidFlag, InvalidFlagType, RedundantFlag, InvalidFormat, NoArgValue, InvalidType, InvalidHandlerType
    }

    public class ArgsException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public ArgsException(ErrorCode code)
        {
            ErrorCode = code;
        }

        public ArgsException(string message)
            : base(message)
        {
        }
        
        public ArgsException(ErrorCode code, string message)
            : base(message)
        {
            ErrorCode = code;
        }
    }
}