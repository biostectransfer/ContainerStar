using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.TransportOrders to entity <see cref="TransportOrders"/>
    /// </summary>
    internal sealed class TransportOrdersMapping: EntityTypeConfiguration<TransportOrders>
    {
        
        public static readonly TransportOrdersMapping Instance = new TransportOrdersMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="TransportOrdersMapping" /> class.
        /// </summary>
        private TransportOrdersMapping()
        {

            ToTable("TransportOrders", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(TransportOrders.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.CustomerId)
                .HasColumnName(TransportOrders.Fields.CustomerId)
                .IsRequired();

            Property(t => t.CommunicationPartnerId)
                .HasColumnName(TransportOrders.Fields.CommunicationPartnerId);

            Property(t => t.IsOffer)
                .HasColumnName(TransportOrders.Fields.IsOffer)
                .IsRequired();

            Property(t => t.DeliveryPlace)
                .HasColumnName(TransportOrders.Fields.DeliveryPlace)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Street)
                .HasColumnName(TransportOrders.Fields.Street)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Zip)
                .HasColumnName(TransportOrders.Fields.Zip)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.City)
                .HasColumnName(TransportOrders.Fields.City)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Comment)
                .HasColumnName(TransportOrders.Fields.Comment)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.OrderDate)
                .HasColumnName(TransportOrders.Fields.OrderDate);

            Property(t => t.OrderedFrom)
                .HasColumnName(TransportOrders.Fields.OrderedFrom)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.OrderNumber)
                .HasColumnName(TransportOrders.Fields.OrderNumber)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.CustomerOrderNumber)
                .HasColumnName(TransportOrders.Fields.CustomerOrderNumber)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Discount)
                .HasColumnName(TransportOrders.Fields.Discount);

            Property(t => t.BillTillDate)
                .HasColumnName(TransportOrders.Fields.BillTillDate);

            Property(t => t.Status)
                .HasColumnName(TransportOrders.Fields.Status)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(TransportOrders.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(TransportOrders.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(TransportOrders.Fields.DeleteDate);


            //Relationships
            HasRequired(t => t.Customers)
                .WithMany(c => c.TransportOrders)
                .HasForeignKey(t => t.CustomerId);
            HasOptional(t => t.CommunicationPartners)
                .WithMany(c => c.TransportOrders)
                .HasForeignKey(t => t.CommunicationPartnerId);
        }
    }
}
