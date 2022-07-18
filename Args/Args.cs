using System;
using System.Collections.Generic;
using System.Linq;

namespace Args
{
    public class Args
    {
        private readonly HashSet<char> _usedFlags = new ();
        private readonly Dictionary<char, Handler> _handlers = new ();

        public Args(string schema, IEnumerable<string> args)
        {
            Console.WriteLine(schema);
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }
            ParseSchema(schema);
            ParseArguments(args);
        }

        private void ParseSchema(string schema)
        {
            foreach (var element in schema.Split(","))
            {
                if (element.Length > 0)
                {
                    ParseSchemaElement(element);
                }
            }
        }

        private void ParseSchemaElement(string element)
        {
            var flagName = element[0];
            var flagType = element[1..];
            ValidateFlag(flagName);
            if (flagType.Length == 0)
            {
                _handlers.Add(flagName, new BooleanHandler());
            }
            
            switch (flagType)
            {
                case "#":
                    _handlers.Add(flagName, new IntHandler());
                    break;
                case "##":
                    _handlers.Add(flagName, new DoubleHandler());
                    break;
                case "*":
                    _handlers.Add(flagName, new StringHandler());
                    break;
                default:
                    throw new ArgsException(ErrorCode.InvalidFlag);
            }
        }

        private void ValidateFlag(char flag)
        {
            if (!char.IsLetter(flag))
            {
                throw new ArgsException(ErrorCode.InvalidFlag);
            }
            
            if (_usedFlags.Contains(flag))
            {
                throw new ArgsException(ErrorCode.RedundantFlag);
            }

            _usedFlags.Add(flag);
        }

        private void ParseArguments(IEnumerable<string> args)
        {
            foreach (var arg in args)
            {
                if (arg.StartsWith('-'))
                {
                    
                }
                else
                {
                    
                }
            }
        }
    }
}