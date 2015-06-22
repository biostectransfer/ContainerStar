using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_STATION to entity <see cref="ExamStation"/>
    /// </summary>
    internal sealed class ExamStationMapping: EntityTypeConfiguration<ExamStation>
    {
        
        public static readonly ExamStationMapping Instance = new ExamStationMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamStationMapping" /> class.
        /// </summary>
        private ExamStationMapping()
        {

            ToTable("DRL_EXAM_STATION", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamStation.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.OrdFederalStateId)
                .HasColumnName(ExamStation.Fields.OrdFederalStateId);

            Property(t => t.Place)
                .HasColumnName(ExamStation.Fields.Place)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(ExamStation.Fields.Description)
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.CreateDate)
                .HasColumnName(ExamStation.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamStation.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamStation.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamStation.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamStation.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamStation.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamStation.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamStation.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamStation.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamStation.Fields.ToDate)
                .IsRequired();

            Property(t => t.SortOrder)
                .HasColumnName(ExamStation.Fields.SortOrder)
                .IsRequired();


            //Relationships
        }
    }
}
