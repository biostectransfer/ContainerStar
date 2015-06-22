using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_RECOGNITION_TYPE to entity <see cref="ExamRecognitionType"/>
    /// </summary>
    internal sealed class ExamRecognitionTypeMapping: EntityTypeConfiguration<ExamRecognitionType>
    {
        
        public static readonly ExamRecognitionTypeMapping Instance = new ExamRecognitionTypeMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamRecognitionTypeMapping" /> class.
        /// </summary>
        private ExamRecognitionTypeMapping()
        {

            ToTable("DRL_EXAM_RECOGNITION_TYPE", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamRecognitionType.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ExamRecognitionType.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(ExamRecognitionType.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.CreateDate)
                .HasColumnName(ExamRecognitionType.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamRecognitionType.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamRecognitionType.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamRecognitionType.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamRecognitionType.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamRecognitionType.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamRecognitionType.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamRecognitionType.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamRecognitionType.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamRecognitionType.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
