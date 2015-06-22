using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_COMMUNITY to entity <see cref="Community"/>
    /// </summary>
    internal sealed class CommunityMapping: EntityTypeConfiguration<Community>
    {
        
        public static readonly CommunityMapping Instance = new CommunityMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="CommunityMapping" /> class.
        /// </summary>
        private CommunityMapping()
        {

            ToTable("DRL_COMMUNITY", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Community.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.DriverSchoolId)
                .HasColumnName(Community.Fields.DriverSchoolId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(Community.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Community.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Community.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(Community.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(Community.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(Community.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(Community.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(Community.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(Community.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(Community.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
