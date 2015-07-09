using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Invoices to entity <see cref="Invoices"/>
    /// </summary>
    internal sealed class InvoicesMapping: EntityTypeConfiguration<Invoices>
    {
        
        public static readonly InvoicesMapping Instance = new InvoicesMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvoicesMapping" /> class.
        /// </summary>
        private InvoicesMapping()
        {

            ToTable("Invoices", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Invoices.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.OrderId)
                .HasColumnName(Invoices.Fields.OrderId)
                .IsRequired();

            Property(t => t.InvoiceNumber)
                .HasColumnName(Invoices.Fields.InvoiceNumber)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.PayDate)
                .HasColumnName(Invoices.Fields.PayDate);

            Property(t => t.WithTaxes)
                .HasColumnName(Invoices.Fields.WithTaxes)
                .IsRequired();

            Property(t => t.ManualPrice)
                .HasColumnName(Invoices.Fields.ManualPrice);

            Property(t => t.TaxValue)
                .HasColumnName(Invoices.Fields.TaxValue)
                .IsRequired();

            Property(t => t.Discount)
                .HasColumnName(Invoices.Fields.Discount)
                .IsRequired();

            Property(t => t.BillTillDate)
                .HasColumnName(Invoices.Fields.BillTillDate);

            Property(t => t.CreateDate)
                .HasColumnName(Invoices.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Invoices.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Invoices.Fields.DeleteDate);

            Property(t => t.IsSellInvoice)
                .HasColumnName(Invoices.Fields.IsSellInvoice)
                .IsRequired();

            Property(t => t.ReminderCount)
                .HasColumnName(Invoices.Fields.ReminderCount)
                .IsRequired();

            Property(t => t.DateVExportDate)
                .HasColumnName(Invoices.Fields.DateVExportDate);

            Property(t => t.DateVExportFile)
                .HasColumnName(Invoices.Fields.DateVExportFile)
                .IsUnicode()
                .HasMaxLength(128);


            //Relationships
            HasRequired(i => i.Orders)
                .WithMany(o => o.Invoices)
                .HasForeignKey(t => t.OrderId);
        }
    }
}
