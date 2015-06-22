using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using ContainerStar.Contracts.SaveActors.Base;
using ContainerStar.Lib.DuplicateCheckers.Interfaces;
using ContainerStar.Contracts.Exceptions;

namespace ContainerStar.Lib.DuplicateCheckers.Base
{
    public abstract class BaseDuplicateCheckerSaveActor<TDuplicateChecker> : ISaveActorBase where TDuplicateChecker: IDuplicateChecker
    {
        readonly Dictionary<string, TDuplicateChecker> checkers = new Dictionary<string, TDuplicateChecker>();

        protected BaseDuplicateCheckerSaveActor(TDuplicateChecker[] checkers)
        {
            foreach (var checker in checkers)
            {
                this.checkers.Add(checker.GetWorkingTypeName(), checker);
            }
        }

        public bool NeedDoAction(object entity, EntityState state)
        {

            return (state == EntityState.Added || state == EntityState.Modified) && checkers.ContainsKey(GetEntityTypeName(entity));
        }

        private string GetEntityTypeName(object entity)
        {
            Type type = entity.GetType();
            if (type.Namespace == "System.Data.Entity.DynamicProxies")
            {
                type = type.BaseType;
            }
            return type.Name;
        }

        public void DoAction(object entity, EntityState state)
        {
            var checker = checkers[GetEntityTypeName(entity)];
            if (checker.HasDuplicate(entity))
            {
                throw new DuplicateEntityException(checker.BusinessKeys, string.Format("Database contains entity of type {0} with the same business keys.", GetEntityTypeName(entity)));
            }
        }
    }
}
