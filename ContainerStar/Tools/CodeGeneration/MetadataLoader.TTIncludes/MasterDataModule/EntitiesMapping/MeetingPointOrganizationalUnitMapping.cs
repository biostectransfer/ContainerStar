using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_MEETING_POINT_ORGANIZATIONAL_UNIT_RSP to entity <see cref="MeetingPointOrganizationalUnit"/>
    /// </summary>
    internal sealed class MeetingPointOrganizationalUnitMapping: EntityTypeConfiguration<MeetingPointOrganizationalUnit>
    {
        
        public static readonly MeetingPointOrganizationalUnitMapping Instance = new MeetingPointOrganizationalUnitMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MeetingPointOrganizationalUnitMapping" /> class.
        /// </summary>
        private MeetingPointOrganizationalUnitMapping()
        {

            ToTable("DRL_MEETING_POINT_ORGANIZATIONAL_UNIT_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.MeetingPointId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.MeetingPointId)
                .IsRequired();

            Property(t => t.OrgOrganizationalUnitId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.OrgOrganizationalUnitId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(MeetingPointOrganizationalUnit.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(m => m.MeetingPoint)
                .WithMany(m => m.MeetingPointOrganizationalUnits)
                .HasForeignKey(t => t.MeetingPointId);
        }
    }
}
