using System.Collections.Generic;
using System.Threading.Tasks;
using SharperUniverse.Core;

namespace GuessingGame
{
    public class ExploreCommandInfo : IUniverseCommandInfo
    {
        public string InputArea { get; private set; }

        public Task ProcessArgsAsync(List<string> args)
        {
            if (args.Count == 1)
            {
                InputArea = args[0];
            }
            return Task.CompletedTask;
        }
    }
}