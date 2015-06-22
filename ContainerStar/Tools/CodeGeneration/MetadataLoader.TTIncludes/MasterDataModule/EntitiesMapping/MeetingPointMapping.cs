using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_MEETING_POINT to entity <see cref="MeetingPoint"/>
    /// </summary>
    internal sealed class MeetingPointMapping: EntityTypeConfiguration<MeetingPoint>
    {
        
        public static readonly MeetingPointMapping Instance = new MeetingPointMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MeetingPointMapping" /> class.
        /// </summary>
        private MeetingPointMapping()
        {

            ToTable("DRL_MEETING_POINT", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(MeetingPoint.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(MeetingPoint.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.Description)
                .HasColumnName(MeetingPoint.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.CreateDate)
                .HasColumnName(MeetingPoint.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(MeetingPoint.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(MeetingPoint.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(MeetingPoint.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(MeetingPoint.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(MeetingPoint.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(MeetingPoint.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(MeetingPoint.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(MeetingPoint.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(MeetingPoint.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
