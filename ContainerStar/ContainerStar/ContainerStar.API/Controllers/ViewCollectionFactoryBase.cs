using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Managers.Base;


namespace ContainerStar.API.Controllers
{    
    public abstract class ViewCollectionControllerFactoryBase
    {
        protected IEnumerable<IdNameModel<TId>> GetReadOnlyViewCollection<TEntity, TId, TManager>(TManager manager)
            where TEntity : class, IHasId<TId>
            where TId : struct, IEquatable<TId>
            where TManager : IReadOnlyEntityManager<TEntity, TId>
        {
            var result = manager.GetEntities().Select(o => { return ToCollectionItem<TId>(o); });
            return result.ToList();
        }

        protected IEnumerable<IdNameModel<TId>> GetViewCollection<TEntity, TId, TManager>(TManager manager)
            where TEntity : class, IHasId<TId>, IRemovable
            where TId : struct, IEquatable<TId>
            where TManager : IEntityManager<TEntity, TId>
        {
            var result = manager.GetEntities().Where(o => !o.DeleteDate.HasValue);

            if (typeof(IHasTitle<TId>).IsAssignableFrom(typeof(TEntity)))
                return result.ToList().Cast<IHasTitle<TId>>().OrderBy(o => o.EntityTitle)
                    .Select(o => new IdNameModel<TId> { id = o.Id, name = o.EntityTitle });
            else
                return result.Select(o => { return ToCollectionItem<TId>(o); }).ToList();
        }

        protected IdNameModel<TId> ToCollectionItem<TId>(IHasId<TId> item)
            where TId : struct, IEquatable<TId>
        {
            if (item is IHasTitle<TId>)
                return new IdNameModel<TId> { id = item.Id, name = ((IHasTitle<TId>)item).EntityTitle };

            return new IdNameModel<TId> { id = item.Id, name = item.Id.ToString() };
        }
    }
}
