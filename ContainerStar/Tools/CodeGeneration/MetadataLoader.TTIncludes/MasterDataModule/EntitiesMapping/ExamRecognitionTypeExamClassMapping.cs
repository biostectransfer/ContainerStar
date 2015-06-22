using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_RECOGNITION_TYPE_EXAM_CLASS_RSP to entity <see cref="ExamRecognitionTypeExamClass"/>
    /// </summary>
    internal sealed class ExamRecognitionTypeExamClassMapping: EntityTypeConfiguration<ExamRecognitionTypeExamClass>
    {
        
        public static readonly ExamRecognitionTypeExamClassMapping Instance = new ExamRecognitionTypeExamClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamRecognitionTypeExamClassMapping" /> class.
        /// </summary>
        private ExamRecognitionTypeExamClassMapping()
        {

            ToTable("DRL_EXAM_RECOGNITION_TYPE_EXAM_CLASS_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamRecognitionTypeId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.ExamRecognitionTypeId)
                .IsRequired();

            Property(t => t.ExamClassId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamRecognitionTypeExamClass.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamRecognitionTypeExamClasses)
                .HasForeignKey(t => t.ExamClassId);
            HasRequired(e => e.ExamRecognitionType)
                .WithMany(e => e.ExamRecognitionTypeExamClasses)
                .HasForeignKey(t => t.ExamRecognitionTypeId);
        }
    }
}
