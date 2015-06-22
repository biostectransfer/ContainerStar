using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_STATION_EXAM_RECOGNITION_TYPE_RSP to entity <see cref="ExamStationExamRecognitionType"/>
    /// </summary>
    internal sealed class ExamStationExamRecognitionTypeMapping: EntityTypeConfiguration<ExamStationExamRecognitionType>
    {
        
        public static readonly ExamStationExamRecognitionTypeMapping Instance = new ExamStationExamRecognitionTypeMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamStationExamRecognitionTypeMapping" /> class.
        /// </summary>
        private ExamStationExamRecognitionTypeMapping()
        {

            ToTable("DRL_EXAM_STATION_EXAM_RECOGNITION_TYPE_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamStationExamRecognitionType.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamStationId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.ExamStationId)
                .IsRequired();

            Property(t => t.ExamRecognitionTypeId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.ExamRecognitionTypeId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamStationExamRecognitionType.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamStationExamRecognitionType.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamStationExamRecognitionType.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamStationExamRecognitionType.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamStationExamRecognitionType.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamStationExamRecognitionType.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamStationExamRecognitionType.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamRecognitionType)
                .WithMany(e => e.ExamStationExamRecognitionTypes)
                .HasForeignKey(t => t.ExamRecognitionTypeId);
            HasRequired(e => e.ExamStation)
                .WithMany(e => e.ExamStationExamRecognitionTypes)
                .HasForeignKey(t => t.ExamStationId);
        }
    }
}
