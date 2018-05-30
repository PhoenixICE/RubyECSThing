﻿using SharperUniverse.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class ConsoleIOHandler : IIOHandler
    {
        public Task<(string commandName, List<string> args)> GetInputAsync()
        {
            var input = Console.In.ReadLine();
            var split = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (split.Length >= 2)
            {
                return Task.FromResult((split[0], new List<string> { split[1] }));
            }

            return Task.FromResult((string.Empty, new List<string>()));
        }

        public Task SendOutputAsync(string outputText)
        {
            Console.Out.WriteLine(outputText);
            return Task.CompletedTask;
        }
    }
}
