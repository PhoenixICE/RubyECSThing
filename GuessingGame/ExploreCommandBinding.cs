using System.Collections.Generic;
using System.Threading.Tasks;
using SharperUniverse.Core;
using System;
using System.Linq;
using GuessingGame.System.Input;

namespace GuessingGame
{
    public class ExploreCommandBinding : IUniverseCommandBinding
    {
        private IIOHandler _ioHandler;
        private ExploreInputSystem _exploreInputSystem;
        private SharperEntity _controllerEntity;

        public string CommandName { get; }

        public ExploreCommandBinding(string commandName)
        {
            CommandName = commandName;
        }

        [SharperInject]
        private async void InitializeCommandRequirements(IIOHandler ioHandler, ExploreInputSystem exploreInputSystem)
        {
            _ioHandler = ioHandler;
            _controllerEntity = await Program.GameRunner.CreateEntityAsync();
            _exploreInputSystem = exploreInputSystem;
        }

        public async Task ProcessCommandAsync(List<string> args)
        {
            if (args.Any())
            {
                await _exploreInputSystem.RegisterComponentAsync(_controllerEntity, args[0]);
                //Console.WriteLine($"Attempted to move to {args[0]}");
            }
        }
    }
}