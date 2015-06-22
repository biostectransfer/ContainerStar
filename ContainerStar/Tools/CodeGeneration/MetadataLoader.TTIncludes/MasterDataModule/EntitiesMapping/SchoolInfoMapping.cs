using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_SCHOOL_INFO to entity <see cref="SchoolInfo"/>
    /// </summary>
    internal sealed class SchoolInfoMapping: EntityTypeConfiguration<SchoolInfo>
    {
        
        public static readonly SchoolInfoMapping Instance = new SchoolInfoMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="SchoolInfoMapping" /> class.
        /// </summary>
        private SchoolInfoMapping()
        {

            ToTable("DRL_SCHOOL_INFO", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(SchoolInfo.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Description)
                .HasColumnName(SchoolInfo.Fields.Description)
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.Text)
                .HasColumnName(SchoolInfo.Fields.Text)
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.CreateDate)
                .HasColumnName(SchoolInfo.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(SchoolInfo.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(SchoolInfo.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(SchoolInfo.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(SchoolInfo.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(SchoolInfo.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(SchoolInfo.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(SchoolInfo.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(SchoolInfo.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(SchoolInfo.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
