using SharperUniverse.Core;

namespace GuessingGame.Component.Input
{
    public class ExploreInputComponent : BaseSharperComponent
    {
        public string Area { get; set; }

        public ExploreInputComponent(SharperEntity entity, string area)
        {
            Entity = entity;
            Area = area;
        }
    }
}