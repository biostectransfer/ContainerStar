using System.Data.Entity;

namespace ContainerStar.Contracts.SaveActors.Base
{
    public interface ISaveActorBase
    {
        bool NeedDoAction(object entity, EntityState state);
        void DoAction(object entity, EntityState state);
    }
}