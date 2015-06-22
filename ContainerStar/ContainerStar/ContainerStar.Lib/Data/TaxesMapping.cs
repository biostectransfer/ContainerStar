using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Taxes to entity <see cref="Taxes"/>
    /// </summary>
    internal sealed class TaxesMapping: EntityTypeConfiguration<Taxes>
    {
        
        public static readonly TaxesMapping Instance = new TaxesMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="TaxesMapping" /> class.
        /// </summary>
        private TaxesMapping()
        {

            ToTable("Taxes", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Taxes.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Value)
                .HasColumnName(Taxes.Fields.Value)
                .IsRequired();

            Property(t => t.FromDate)
                .HasColumnName(Taxes.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(Taxes.Fields.ToDate)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(Taxes.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Taxes.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Taxes.Fields.DeleteDate);


            //Relationships
        }
    }
}
