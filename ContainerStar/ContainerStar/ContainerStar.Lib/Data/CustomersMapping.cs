using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Customers to entity <see cref="Customers"/>
    /// </summary>
    internal sealed class CustomersMapping: EntityTypeConfiguration<Customers>
    {
        
        public static readonly CustomersMapping Instance = new CustomersMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomersMapping" /> class.
        /// </summary>
        private CustomersMapping()
        {

            ToTable("Customers", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Customers.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Number)
                .HasColumnName(Customers.Fields.Number)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Name)
                .HasColumnName(Customers.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Street)
                .HasColumnName(Customers.Fields.Street)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Zip)
                .HasColumnName(Customers.Fields.Zip)
                .IsRequired();

            Property(t => t.City)
                .HasColumnName(Customers.Fields.City)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Country)
                .HasColumnName(Customers.Fields.Country)
                .IsUnicode()
                .HasMaxLength(2);

            Property(t => t.Phone)
                .HasColumnName(Customers.Fields.Phone)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Mobile)
                .HasColumnName(Customers.Fields.Mobile)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Fax)
                .HasColumnName(Customers.Fields.Fax)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Email)
                .HasColumnName(Customers.Fields.Email)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Comment)
                .HasColumnName(Customers.Fields.Comment)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Iban)
                .HasColumnName(Customers.Fields.Iban)
                .IsUnicode()
                .HasMaxLength(22);

            Property(t => t.Bic)
                .HasColumnName(Customers.Fields.Bic)
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.WithTaxes)
                .HasColumnName(Customers.Fields.WithTaxes)
                .IsRequired();

            Property(t => t.AutoDebitEntry)
                .HasColumnName(Customers.Fields.AutoDebitEntry)
                .IsRequired();

            Property(t => t.AutoBill)
                .HasColumnName(Customers.Fields.AutoBill)
                .IsRequired();

            Property(t => t.Discount)
                .HasColumnName(Customers.Fields.Discount);

            Property(t => t.UstId)
                .HasColumnName(Customers.Fields.UstId)
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.CreateDate)
                .HasColumnName(Customers.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Customers.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Customers.Fields.DeleteDate);


            //Relationships
        }
    }
}
