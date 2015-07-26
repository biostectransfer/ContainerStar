using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.CommunicationPartners to entity <see cref="CommunicationPartners"/>
    /// </summary>
    internal sealed class CommunicationPartnersMapping: EntityTypeConfiguration<CommunicationPartners>
    {
        
        public static readonly CommunicationPartnersMapping Instance = new CommunicationPartnersMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunicationPartnersMapping" /> class.
        /// </summary>
        private CommunicationPartnersMapping()
        {

            ToTable("CommunicationPartners", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(CommunicationPartners.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(CommunicationPartners.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.FirstName)
                .HasColumnName(CommunicationPartners.Fields.FirstName)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.CustomerId)
                .HasColumnName(CommunicationPartners.Fields.CustomerId)
                .IsRequired();

            Property(t => t.Phone)
                .HasColumnName(CommunicationPartners.Fields.Phone)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Mobile)
                .HasColumnName(CommunicationPartners.Fields.Mobile)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Fax)
                .HasColumnName(CommunicationPartners.Fields.Fax)
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.Email)
                .HasColumnName(CommunicationPartners.Fields.Email)
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.CreateDate)
                .HasColumnName(CommunicationPartners.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(CommunicationPartners.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(CommunicationPartners.Fields.DeleteDate);


            //Relationships
            HasRequired(c => c.Customers)
                .WithMany(c => c.CommunicationPartners)
                .HasForeignKey(t => t.CustomerId);
        }
    }
}
