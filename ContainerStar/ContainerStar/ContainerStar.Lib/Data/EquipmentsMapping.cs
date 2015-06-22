using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Equipments to entity <see cref="Equipments"/>
    /// </summary>
    internal sealed class EquipmentsMapping: EntityTypeConfiguration<Equipments>
    {
        
        public static readonly EquipmentsMapping Instance = new EquipmentsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="EquipmentsMapping" /> class.
        /// </summary>
        private EquipmentsMapping()
        {

            ToTable("Equipments", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Equipments.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Description)
                .HasColumnName(Equipments.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.CreateDate)
                .HasColumnName(Equipments.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Equipments.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Equipments.Fields.DeleteDate);


            //Relationships
        }
    }
}
