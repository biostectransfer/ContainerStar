using ContainerStar.Contracts.SaveActors;
using ContainerStar.Lib.DuplicateCheckers.Interfaces;
using CoreBase.DuplicateCheckers;

namespace ContainerStar.Lib.DuplicateCheckers
{
    public sealed class ContainerStarDuplicateCheckerSaveActor : BaseDuplicateCheckerSaveActor<IContainerStarDuplicateChecker>, IContainerStarSaveActor
    {
        public ContainerStarDuplicateCheckerSaveActor(IContainerStarDuplicateChecker[] checkers)
            : base(checkers)
        {
        }
    }
}
