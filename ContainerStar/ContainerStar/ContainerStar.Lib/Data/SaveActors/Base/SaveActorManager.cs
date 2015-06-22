using System.Collections.Generic;
using System.Data.Entity;
using ContainerStar.Contracts.SaveActors.Base;

namespace ContainerStar.Lib.Data.SaveActors.Base
{
    public abstract class SaveActorManager<TSaveActor> where TSaveActor: ISaveActorBase
    {
        private readonly List<TSaveActor> actors = new List<TSaveActor>();

        protected SaveActorManager(IEnumerable<TSaveActor> actors)
        {
            this.actors = new List<TSaveActor>(actors);
        }

        public void DoBeforeSaveAction(object entity, EntityState state)
        {
            foreach (var saveActor in actors)
            {
                if (saveActor.NeedDoAction(entity, state))
                {
                    saveActor.DoAction(entity, state);
                }
            }
        }
    }
}
