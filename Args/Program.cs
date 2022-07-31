using System;

var test = new Args.Args("ab*c#", Environment.GetCommandLineArgs());

Console.WriteLine($"a to {test.GetBool('a')}");
Console.WriteLine($"b to {test.GetString('b')}");
Console.WriteLine($"c to {test.GetInt('c')}");
Console.WriteLine($"c to {test.GetString('c')}");