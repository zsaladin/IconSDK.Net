using System;
using System.Reflection;
using System.Collections.Generic;

namespace IconSDK.RPCs
{
    public class RPCException : Exception
    {
        static Dictionary<int, Type> _exceptionTypes;
        static RPCException()
        {
            _exceptionTypes = new Dictionary<int, Type>
            {
                [-32700] = typeof(RPCParseErrorException),
                [-32600] = typeof(RPCInvalidRequestException),
                [-32601] = typeof(RPCMethodNotFoundException),
                [-32602] = typeof(RPCInvalidParamsException),
                [-32603] = typeof(RPCInternalErrorException),
                [-32000] = typeof(RPCServerErrorException),
                [-32100] = typeof(RPCScoreErrorException),
            };
        }

        public static RPCException Create(int code, string message)
        {
            if (_exceptionTypes.TryGetValue(code, out Type type))
            {
                RPCException exception = (RPCException)Activator.CreateInstance(type, code, message);
                return exception;
            }

            return new RPCException(code, message);
        }

        public readonly int Code;
        public RPCException(int code, string message)
            : base(message)
        {
            Code = code;
        }

    }

    public class RPCParseErrorException : RPCException
    {
        public RPCParseErrorException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCInvalidRequestException : RPCException
    {
        public RPCInvalidRequestException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCMethodNotFoundException : RPCException
    {
        public RPCMethodNotFoundException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCInvalidParamsException : RPCException
    {
        public RPCInvalidParamsException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCInternalErrorException : RPCException
    {
        public RPCInternalErrorException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCServerErrorException : RPCException
    {
        public RPCServerErrorException(int code, string message)
            : base(code, message)
        {
        }
    }

    public class RPCScoreErrorException : RPCException
    {
        public RPCScoreErrorException(int code, string message)
            : base(code, message)
        {
        }
    }
}