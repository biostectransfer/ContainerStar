using ContainerStar.Contracts.SaveActors.Base;
using System.Data.Entity;
using System.Linq;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     ContainerStar Entities
    /// </summary>
    [DbConfigurationType(typeof(ContainerStarConfiguration))]
    public partial class ContainerStarEntities: EntitiesBase
    {
        /// <summary>
        /// </summary>
        /// <param name="saveActorManager"></param>
        /// <param name="connectionString"></param>
        public ContainerStarEntities(ISaveActorManagerBase saveActorManager, string connectionString)
            : base(saveActorManager, connectionString)
        {
        }

        static ContainerStarEntities()
        {
            Database.SetInitializer<ContainerStarEntities>(null);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerStarEntities" /> class.
        /// </summary>
        public ContainerStarEntities(ISaveActorManagerBase saveActorManager)
            : base(saveActorManager, "name=ContainerStarEntities")
        {
        }


        protected override void RegisterCustomMappings(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(NumbersMapping.Instance);
        }
    }
}