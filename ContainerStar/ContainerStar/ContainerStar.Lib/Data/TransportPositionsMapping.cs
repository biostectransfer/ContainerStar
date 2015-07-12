using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.TransportPositions to entity <see cref="TransportPositions"/>
    /// </summary>
    internal sealed class TransportPositionsMapping: EntityTypeConfiguration<TransportPositions>
    {
        
        public static readonly TransportPositionsMapping Instance = new TransportPositionsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportPositionsMapping" /> class.
        /// </summary>
        private TransportPositionsMapping()
        {

            ToTable("TransportPositions", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(TransportPositions.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.TransportOrderId)
                .HasColumnName(TransportPositions.Fields.TransportOrderId)
                .IsRequired();

            Property(t => t.TransportProductId)
                .HasColumnName(TransportPositions.Fields.TransportProductId)
                .IsRequired();

            Property(t => t.Price)
                .HasColumnName(TransportPositions.Fields.Price)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(TransportPositions.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(TransportPositions.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(TransportPositions.Fields.DeleteDate);

            Property(t => t.Amount)
                .HasColumnName(TransportPositions.Fields.Amount)
                .IsRequired();


            //Relationships
            HasRequired(t => t.TransportOrders)
                .WithMany(t => t.TransportPositions)
                .HasForeignKey(t => t.TransportOrderId);
            HasRequired(t => t.TransportProducts)
                .WithMany(t => t.TransportPositions)
                .HasForeignKey(t => t.TransportProductId);
        }
    }
}
