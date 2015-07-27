using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.ContainerTypes to entity <see cref="ContainerTypes"/>
    /// </summary>
    internal sealed class ContainerTypesMapping: EntityTypeConfiguration<ContainerTypes>
    {
        
        public static readonly ContainerTypesMapping Instance = new ContainerTypesMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerTypesMapping" /> class.
        /// </summary>
        private ContainerTypesMapping()
        {

            ToTable("ContainerTypes", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ContainerTypes.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ContainerTypes.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Comment)
                .HasColumnName(ContainerTypes.Fields.Comment)
                .IsUnicode()
                .HasMaxLength(256);

            Property(t => t.CreateDate)
                .HasColumnName(ContainerTypes.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ContainerTypes.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ContainerTypes.Fields.DeleteDate);

            Property(t => t.DispositionRelevant)
                .HasColumnName(ContainerTypes.Fields.DispositionRelevant)
                .IsRequired();


            //Relationships
        }
    }
}
