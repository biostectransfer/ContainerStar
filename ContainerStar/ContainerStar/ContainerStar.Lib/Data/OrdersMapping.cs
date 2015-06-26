using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Orders to entity <see cref="Orders"/>
    /// </summary>
    internal sealed class OrdersMapping: EntityTypeConfiguration<Orders>
    {
        
        public static readonly OrdersMapping Instance = new OrdersMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="OrdersMapping" /> class.
        /// </summary>
        private OrdersMapping()
        {

            ToTable("Orders", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Orders.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.CustomerId)
                .HasColumnName(Orders.Fields.CustomerId)
                .IsRequired();

            Property(t => t.CommunicationPartnerId)
                .HasColumnName(Orders.Fields.CommunicationPartnerId);

            Property(t => t.IsOffer)
                .HasColumnName(Orders.Fields.IsOffer)
                .IsRequired();

            Property(t => t.DeliveryPlace)
                .HasColumnName(Orders.Fields.DeliveryPlace)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Street)
                .HasColumnName(Orders.Fields.Street)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Zip)
                .HasColumnName(Orders.Fields.Zip)
                .IsRequired();

            Property(t => t.City)
                .HasColumnName(Orders.Fields.City)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Comment)
                .HasColumnName(Orders.Fields.Comment)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.OrderDate)
                .HasColumnName(Orders.Fields.OrderDate);

            Property(t => t.OrderedFrom)
                .HasColumnName(Orders.Fields.OrderedFrom)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.OrderNumber)
                .HasColumnName(Orders.Fields.OrderNumber)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.RentOrderNumber)
                .HasColumnName(Orders.Fields.RentOrderNumber)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.RentFromDate)
                .HasColumnName(Orders.Fields.RentFromDate);

            Property(t => t.RentToDate)
                .HasColumnName(Orders.Fields.RentToDate);

            Property(t => t.AutoBill)
                .HasColumnName(Orders.Fields.AutoBill)
                .IsRequired();

            Property(t => t.Discount)
                .HasColumnName(Orders.Fields.Discount);

            Property(t => t.BillTillDate)
                .HasColumnName(Orders.Fields.BillTillDate);

            Property(t => t.CreateDate)
                .HasColumnName(Orders.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Orders.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Orders.Fields.DeleteDate);

            Property(t => t.CustomerOrderNumber)
                .HasColumnName(Orders.Fields.CustomerOrderNumber)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.AutoProlongation)
                .HasColumnName(Orders.Fields.AutoProlongation)
                .IsRequired();


            //Relationships
            HasRequired(o => o.Customers)
                .WithMany(c => c.Orders)
                .HasForeignKey(t => t.CustomerId);
            HasOptional(o => o.CommunicationPartners)
                .WithMany(c => c.Orders)
                .HasForeignKey(t => t.CommunicationPartnerId);
        }
    }
}
