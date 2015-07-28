using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.InvoicePositions to entity <see cref="InvoicePositions"/>
    /// </summary>
    internal sealed class InvoicePositionsMapping: EntityTypeConfiguration<InvoicePositions>
    {
        
        public static readonly InvoicePositionsMapping Instance = new InvoicePositionsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvoicePositionsMapping" /> class.
        /// </summary>
        private InvoicePositionsMapping()
        {

            ToTable("InvoicePositions", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(InvoicePositions.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.InvoiceId)
                .HasColumnName(InvoicePositions.Fields.InvoiceId)
                .IsRequired();

            Property(t => t.PositionId)
                .HasColumnName(InvoicePositions.Fields.PositionId)
                .IsRequired();

            Property(t => t.Price)
                .HasColumnName(InvoicePositions.Fields.Price)
                .IsRequired();

            Property(t => t.FromDate)
                .HasColumnName(InvoicePositions.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(InvoicePositions.Fields.ToDate)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(InvoicePositions.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(InvoicePositions.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(InvoicePositions.Fields.DeleteDate);

            Property(t => t.Amount)
                .HasColumnName(InvoicePositions.Fields.Amount)
                .IsRequired();

            Property(t => t.PaymentType)
                .HasColumnName(InvoicePositions.Fields.PaymentType)
                .IsRequired();


            //Relationships
            HasRequired(i => i.Invoices)
                .WithMany(i => i.InvoicePositions)
                .HasForeignKey(t => t.InvoiceId);
            HasRequired(i => i.Positions)
                .WithMany(p => p.InvoicePositions)
                .HasForeignKey(t => t.PositionId);
        }
    }
}
