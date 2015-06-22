using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.AdditionalCosts to entity <see cref="AdditionalCosts"/>
    /// </summary>
    internal sealed class AdditionalCostsMapping: EntityTypeConfiguration<AdditionalCosts>
    {
        
        public static readonly AdditionalCostsMapping Instance = new AdditionalCostsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="AdditionalCostsMapping" /> class.
        /// </summary>
        private AdditionalCostsMapping()
        {

            ToTable("AdditionalCosts", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(AdditionalCosts.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(AdditionalCosts.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Description)
                .HasColumnName(AdditionalCosts.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Price)
                .HasColumnName(AdditionalCosts.Fields.Price)
                .IsRequired();

            Property(t => t.Automatic)
                .HasColumnName(AdditionalCosts.Fields.Automatic)
                .IsRequired();

            Property(t => t.IncludeInFirstBill)
                .HasColumnName(AdditionalCosts.Fields.IncludeInFirstBill)
                .IsRequired();

            Property(t => t.ProceedsAccount)
                .HasColumnName(AdditionalCosts.Fields.ProceedsAccount)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(AdditionalCosts.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(AdditionalCosts.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(AdditionalCosts.Fields.DeleteDate);


            //Relationships
        }
    }
}
