using SharperUniverse.Core;

namespace GuessingGame
{
    public class CommandSource : IUniverseCommandSource
    {
        private int ID;

        public CommandSource(int id)
        {
            ID = id;
        }

        public bool SourceIsSameAsBindingSource(IUniverseCommandSource bindingSource)
        {
            var source = (CommandSource)bindingSource;

            return ID == source.ID;
        }
    }
}