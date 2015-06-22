using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.TransportProducts to entity <see cref="TransportProducts"/>
    /// </summary>
    internal sealed class TransportProductsMapping: EntityTypeConfiguration<TransportProducts>
    {
        
        public static readonly TransportProductsMapping Instance = new TransportProductsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportProductsMapping" /> class.
        /// </summary>
        private TransportProductsMapping()
        {

            ToTable("TransportProducts", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(TransportProducts.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(TransportProducts.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Description)
                .HasColumnName(TransportProducts.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Price)
                .HasColumnName(TransportProducts.Fields.Price)
                .IsRequired();

            Property(t => t.ProceedsAccount)
                .HasColumnName(TransportProducts.Fields.ProceedsAccount)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(TransportProducts.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(TransportProducts.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(TransportProducts.Fields.DeleteDate);


            //Relationships
        }
    }
}
