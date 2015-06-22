using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_ARGE_VERSION to entity <see cref="ArgeVersion"/>
    /// </summary>
    internal sealed class ArgeVersionMapping: EntityTypeConfiguration<ArgeVersion>
    {
        
        public static readonly ArgeVersionMapping Instance = new ArgeVersionMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ArgeVersionMapping" /> class.
        /// </summary>
        private ArgeVersionMapping()
        {

            ToTable("DRL_ARGE_VERSION", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ArgeVersion.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ProgrammName)
                .HasColumnName(ArgeVersion.Fields.ProgrammName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(30);

            Property(t => t.VersionSystem)
                .HasColumnName(ArgeVersion.Fields.VersionSystem)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.ExpirationDate)
                .HasColumnName(ArgeVersion.Fields.ExpirationDate)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ArgeVersion.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ArgeVersion.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ArgeVersion.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ArgeVersion.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ArgeVersion.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ArgeVersion.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ArgeVersion.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ArgeVersion.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ArgeVersion.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ArgeVersion.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
