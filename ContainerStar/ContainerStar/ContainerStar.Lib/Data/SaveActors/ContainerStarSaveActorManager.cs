using System.Collections.Generic;
using ContainerStar.Contracts.SaveActors;
using CoreBase.SaveActors;

namespace ContainerStar.Lib.Data.SaveActors
{
    public sealed class ContainerStarSaveActorManager : SaveActorManager<IContainerStarSaveActor>, IContainerStarSaveActorManager
    {
        public ContainerStarSaveActorManager(IContainerStarSaveActor[] actors)
            : base(actors)
        {
        }
    }
}
