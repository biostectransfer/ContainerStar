using System.Data.Entity;

namespace ContainerStar.Contracts.SaveActors.Base
{
    public interface ISaveActorManagerBase
    {
        void DoBeforeSaveAction(object entity, EntityState state);
    }
}