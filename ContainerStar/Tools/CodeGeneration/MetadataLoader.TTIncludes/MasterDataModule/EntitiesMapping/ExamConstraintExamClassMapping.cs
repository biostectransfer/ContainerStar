using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CONSTRAINT_EXAM_CLASS_RSP to entity <see cref="ExamConstraintExamClass"/>
    /// </summary>
    internal sealed class ExamConstraintExamClassMapping: EntityTypeConfiguration<ExamConstraintExamClass>
    {
        
        public static readonly ExamConstraintExamClassMapping Instance = new ExamConstraintExamClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamConstraintExamClassMapping" /> class.
        /// </summary>
        private ExamConstraintExamClassMapping()
        {

            ToTable("DRL_EXAM_CONSTRAINT_EXAM_CLASS_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamConstraintExamClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamConstraintId)
                .HasColumnName(ExamConstraintExamClass.Fields.ExamConstraintId)
                .IsRequired();

            Property(t => t.ExamClassId)
                .HasColumnName(ExamConstraintExamClass.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamConstraintExamClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamConstraintExamClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamConstraintExamClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamConstraintExamClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamConstraintExamClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamConstraintExamClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamConstraintExamClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamConstraintExamClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamConstraintExamClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamConstraintExamClass.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamConstraintExamClasses)
                .HasForeignKey(t => t.ExamClassId);
            HasRequired(e => e.ExamConstraint)
                .WithMany(e => e.ExamConstraintExamClasses)
                .HasForeignKey(t => t.ExamConstraintId);
        }
    }
}
