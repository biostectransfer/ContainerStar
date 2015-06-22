namespace ContainerStar.API.SystemLog
{
	public enum EntityGender
	{
		Masculine,
		Feminine,
		Neuter
	}

	public static class EntityLoggerTemplates
	{
		public static string UpdateMasculine = @"{0} ""{1}"" обновлен.";
		public static string UpdateFeminine = @"{0} ""{1}"" обновлена.";
		public static string UpdateNeuter = @"{0} ""{1}"" обновлено.";

		public static string AddMasculine = @"{0} ""{1}"" добавлен.";
		public static string AddFeminine = @"{0} ""{1}"" добавлена.";
		public static string AddNeuter = @"{0} ""{1}"" добавлено.";

		public static string DeleteMasculine = @"{0} ""{1}"" удален.";
		public static string DeleteFeminine = @"{0} ""{1}"" удалена.";
		public static string DeleteNeuter = @"{0} ""{1}"" удалено.";
	}

	public interface IEntityLogger<TEntity>
	{
		void AddEntity(TEntity entity);
		void UpdateEntity(TEntity entity);
		void DeleteEntity(TEntity entity);
		void AddInformation(string description, params object[] args);
		//ISystemLogManager SystemLogManager { get; }
	}

	public class EntityLogger<TEntity> : IEntityLogger<TEntity>
		where TEntity : class//TODO IHasID
	{
		public EntityLogger(string login, /*SystemLogRecordTypes systemLogRecordType,*/ EntityGender entityGender, string entityName)
		{
			EntityGender = entityGender;
			//SystemLogRecordType = systemLogRecordType;
			Login = login;
			EntityName = entityName;
		}

		private string EntityName { get; set; }
		//private SystemLogRecordTypes SystemLogRecordType { get; set; }
		private string Login { get; set; }
		private EntityGender EntityGender { get; set; }
		//public ISystemLogManager SystemLogManager { get { return ServiceLocator.GetInstance<ISystemLogManager>(); } }

		public void AddInformation(string description, params object[] args)
		{
			//SystemLogManager.AddInformation(Login, SystemLogRecordType, description, args);
			//SystemLogManager.SaveChanges();
		}

		public void AddEntity(TEntity entity)
		{
            //var value = entity.ID.ToString();
            //if (entity is IHasName)
            //{
            //    var hasNameEntity = entity as IHasName;
            //    value = hasNameEntity.Name;
            //}

            //var template = EntityLoggerTemplates.AddMasculine;
            //if (EntityGender == SystemLog.EntityGender.Feminine)
            //    template = EntityLoggerTemplates.AddFeminine;
            //else if (EntityGender == SystemLog.EntityGender.Neuter)
            //    template = EntityLoggerTemplates.AddNeuter;

            //var description = string.Format(template, EntityName, value);

            //SystemLogManager.AddInformation(Login, SystemLogRecordType, description);
            //SystemLogManager.SaveChanges();
		}

		public void UpdateEntity(TEntity entity)
		{
        //    var value = entity.ID.ToString();
        //    if (entity is IHasName)
        //    {
        //        var hasNameEntity = entity as IHasName;
        //        value = hasNameEntity.Name;
        //    }

        //    var template = EntityLoggerTemplates.UpdateMasculine;
        //    if (EntityGender == SystemLog.EntityGender.Feminine)
        //        template = EntityLoggerTemplates.UpdateFeminine;
        //    else if (EntityGender == SystemLog.EntityGender.Neuter)
        //        template = EntityLoggerTemplates.UpdateNeuter;

        //    var description = string.Format(template, EntityName, value);

        //    SystemLogManager.AddInformation(Login, SystemLogRecordType, description);
        //    SystemLogManager.SaveChanges();
        }

        public void DeleteEntity(TEntity entity)
        {
        //    var value = entity.ID.ToString();
        //    if (entity is IHasName)
        //    {
        //        var hasNameEntity = entity as IHasName;
        //        value = hasNameEntity.Name;
        //    }

        //    var template = EntityLoggerTemplates.DeleteMasculine;
        //    if (EntityGender == SystemLog.EntityGender.Feminine)
        //        template = EntityLoggerTemplates.DeleteFeminine;
        //    else if (EntityGender == SystemLog.EntityGender.Neuter)
        //        template = EntityLoggerTemplates.DeleteNeuter;

        //    var description = string.Format(template, EntityName, value);

        //    SystemLogManager.AddInformation(Login, SystemLogRecordType, description);
        //    SystemLogManager.SaveChanges();
		}
	}
}
