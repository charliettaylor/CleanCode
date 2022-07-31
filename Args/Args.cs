using System;
using System.Collections.Generic;
using System.Linq;

namespace Args
{
    public class Args
    {
        private readonly HashSet<char> _usedFlags = new ();
        private readonly Dictionary<char, IHandler> _handlers = new ();

        public Args(string schema, string[]? args)
        {
            if (args is null)
            {
                return;
            }
            
            foreach (var arg in args)
            {
                Console.WriteLine(arg);
            }

            ParseSchema(schema);
            ParseArguments(args.ToList());
        }

        public Dictionary<char, IHandler> GetArgs()
        {
            return _handlers;
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
            else
            {
                switch (flagType)
                {
                    case "#":
                        _handlers.Add(flagName, new IntHandler());
                        break;
                    case "##":
                        _handlers.Add(flagName, new DecimalHandler());
                        break;
                    case "*":
                        _handlers.Add(flagName, new StringHandler());
                        break;
                    default:
                        throw new ArgsException(ErrorCode.InvalidFlagType, "Schema flag not defined correctly");
                }
            }
        }

        private void ValidateFlag(char flag)
        {
            if (!char.IsLetter(flag))
            {
                throw new ArgsException(ErrorCode.InvalidFlag, "Flag is not a letter");
            }
            
            if (_usedFlags.Contains(flag))
            {
                throw new ArgsException(ErrorCode.RedundantFlag, "Flag has already been used");
            }

            _usedFlags.Add(flag);
        }

        private void ParseArguments(List<string> args)
        {
            for (var i = 0; i < args.Count; i++)
            {
                var flagString = args[i];
                if (!ArgIsInSchema(flagString)) continue;
                
                if (++i < args.Count)
                    ParseValue(flagString.Last(), args[i]);
            }
        }

        private bool ArgIsInSchema(string flag)
        {
            var hasDash = flag.First().Equals('-');
            var isInSchema = _usedFlags.Contains(flag.Last());
            return hasDash && isInSchema;
        }

        private void ParseValue(char flag, string arg)
        {
            if (!_handlers.TryGetValue(flag, out var handler))
            {
                throw new ArgsException(ErrorCode.InvalidFlag);
            }
            
            handler.Set(arg);
        }

        public bool GetBool(char flag)
        {
            if (_handlers.TryGetValue(flag, out var result))
            {
                return BooleanHandler.Get(result);
            }

            throw new ArgsException(ErrorCode.InvalidFlag, "Flag is not defined in schema");
        }
        
        public int GetInt(char flag)
        {
            if (_handlers.TryGetValue(flag, out var result))
            {
                return IntHandler.Get(result);
            }

            throw new ArgsException(ErrorCode.InvalidFlag, "Flag is not defined in schema");
        }
        
        public decimal GetDecimal(char flag)
        {
            if (_handlers.TryGetValue(flag, out var result))
            {
                return DecimalHandler.Get(result);
            }

            throw new ArgsException(ErrorCode.InvalidFlag, "Flag is not defined in schema");
        }
        
        public string GetString(char flag)
        {
            if (_handlers.TryGetValue(flag, out var result))
            {
                return StringHandler.Get(result);
            }

            throw new ArgsException(ErrorCode.InvalidFlag, "Flag is not defined in schema");
        }
    }
}