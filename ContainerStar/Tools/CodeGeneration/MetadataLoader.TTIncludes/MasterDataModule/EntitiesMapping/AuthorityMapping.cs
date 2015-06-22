using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_AUTHORITY to entity <see cref="Authority"/>
    /// </summary>
    internal sealed class AuthorityMapping: EntityTypeConfiguration<Authority>
    {
        
        public static readonly AuthorityMapping Instance = new AuthorityMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="AuthorityMapping" /> class.
        /// </summary>
        private AuthorityMapping()
        {

            ToTable("DRL_AUTHORITY", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Authority.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.AuthorityNumber)
                .HasColumnName(Authority.Fields.AuthorityNumber)
                .IsRequired()
                .HasMaxLength(13)
                .IsFixedLength();

            Property(t => t.Name)
                .HasColumnName(Authority.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.Description)
                .HasColumnName(Authority.Fields.Description)
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.IsCertificateRequired)
                .HasColumnName(Authority.Fields.IsCertificateRequired)
                .IsRequired();

            Property(t => t.ReturnType)
                .HasColumnName(Authority.Fields.ReturnType)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(Authority.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Authority.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Authority.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(Authority.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(Authority.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(Authority.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(Authority.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(Authority.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(Authority.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(Authority.Fields.ToDate)
                .IsRequired();

            Property(t => t.RowVersion)
                .HasColumnName(Authority.Fields.RowVersion)
                .IsRequired();

            Property(t => t.Name2)
                .HasColumnName(Authority.Fields.Name2)
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.StreetHouseNumber)
                .HasColumnName(Authority.Fields.StreetHouseNumber)
                .IsUnicode()
                .HasMaxLength(220);

            Property(t => t.ZipCode)
                .HasColumnName(Authority.Fields.ZipCode)
                .HasMaxLength(90);

            Property(t => t.City)
                .HasColumnName(Authority.Fields.City)
                .IsUnicode()
                .HasMaxLength(160);

            Property(t => t.SysCountryId)
                .HasColumnName(Authority.Fields.SysCountryId);

            Property(t => t.Phone1)
                .HasColumnName(Authority.Fields.Phone1)
                .HasMaxLength(90);

            Property(t => t.Phone2)
                .HasColumnName(Authority.Fields.Phone2)
                .HasMaxLength(90);

            Property(t => t.Fax)
                .HasColumnName(Authority.Fields.Fax)
                .HasMaxLength(90);


            //Relationships
        }
    }
}
