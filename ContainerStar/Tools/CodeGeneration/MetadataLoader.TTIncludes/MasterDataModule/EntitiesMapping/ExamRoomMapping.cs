using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_ROOM to entity <see cref="ExamRoom"/>
    /// </summary>
    internal sealed class ExamRoomMapping: EntityTypeConfiguration<ExamRoom>
    {
        
        public static readonly ExamRoomMapping Instance = new ExamRoomMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamRoomMapping" /> class.
        /// </summary>
        private ExamRoomMapping()
        {

            ToTable("DRL_EXAM_ROOM", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamRoom.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.RoomNumber)
                .HasColumnName(ExamRoom.Fields.RoomNumber)
                .IsRequired();

            Property(t => t.PlaceAmount)
                .HasColumnName(ExamRoom.Fields.PlaceAmount)
                .IsRequired();

            Property(t => t.OrgOrganizationalUnitId)
                .HasColumnName(ExamRoom.Fields.OrgOrganizationalUnitId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamRoom.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamRoom.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamRoom.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamRoom.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamRoom.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamRoom.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamRoom.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamRoom.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamRoom.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamRoom.Fields.ToDate)
                .IsRequired();

            Property(t => t.Name1)
                .HasColumnName(ExamRoom.Fields.Name1)
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.Name2)
                .HasColumnName(ExamRoom.Fields.Name2)
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.Name3)
                .HasColumnName(ExamRoom.Fields.Name3)
                .IsUnicode()
                .HasMaxLength(100);

            Property(t => t.StreetHouseNumber)
                .HasColumnName(ExamRoom.Fields.StreetHouseNumber)
                .IsUnicode()
                .HasMaxLength(220);

            Property(t => t.ZipCode)
                .HasColumnName(ExamRoom.Fields.ZipCode)
                .HasMaxLength(90);

            Property(t => t.ZipBox)
                .HasColumnName(ExamRoom.Fields.ZipBox)
                .HasMaxLength(90);

            Property(t => t.Box)
                .HasColumnName(ExamRoom.Fields.Box)
                .HasMaxLength(90);

            Property(t => t.City)
                .HasColumnName(ExamRoom.Fields.City)
                .IsUnicode()
                .HasMaxLength(160);

            Property(t => t.Phone1)
                .HasColumnName(ExamRoom.Fields.Phone1)
                .HasMaxLength(90);

            Property(t => t.Fax)
                .HasColumnName(ExamRoom.Fields.Fax)
                .HasMaxLength(90);

            Property(t => t.Email)
                .HasColumnName(ExamRoom.Fields.Email)
                .HasMaxLength(90);

            Property(t => t.SysCountryId)
                .HasColumnName(ExamRoom.Fields.SysCountryId)
                .IsRequired();


            //Relationships
        }
    }
}
