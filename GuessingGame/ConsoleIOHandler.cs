using SharperUniverse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GuessingGame
{
    public class ConsoleIOHandler : IIOHandler
    {
        public Task SendOutputAsync(string outputText)
        {
            Console.Out.WriteLine(outputText);
            return Task.CompletedTask;
        }

        public async Task<(string CommandName, List<string> Args, IUniverseCommandSource CommandSource)> GetInputAsync()
        {
            var inputStr = await Console.In.ReadLineAsync();
            var inputSplit = inputStr.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string command = string.Empty;
            List<string> args = new List<string>();

            if (inputSplit.Any())
            {
                command = inputSplit[0];
            }

            if (inputSplit.Length > 1)
            {
                args = inputSplit.Skip(1).ToList();
            }

            return (command, args, new CommandSource(0));
        }
    }
}
