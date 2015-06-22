using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS_INCLUSIVE_CLASS_RSP to entity <see cref="ExamClassInclusiveClass"/>
    /// </summary>
    internal sealed class ExamClassInclusiveClassMapping: EntityTypeConfiguration<ExamClassInclusiveClass>
    {
        
        public static readonly ExamClassInclusiveClassMapping Instance = new ExamClassInclusiveClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassInclusiveClassMapping" /> class.
        /// </summary>
        private ExamClassInclusiveClassMapping()
        {

            ToTable("DRL_EXAM_CLASS_INCLUSIVE_CLASS_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClassInclusiveClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamClassId)
                .HasColumnName(ExamClassInclusiveClass.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.ExamClassIdInclusive)
                .HasColumnName(ExamClassInclusiveClass.Fields.ExamClassIdInclusive)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClassInclusiveClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClassInclusiveClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClassInclusiveClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClassInclusiveClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClassInclusiveClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClassInclusiveClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClassInclusiveClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClassInclusiveClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamClassInclusiveClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamClassInclusiveClass.Fields.ToDate)
                .IsRequired();

            Property(t => t.IsConditional)
                .HasColumnName(ExamClassInclusiveClass.Fields.IsConditional)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamClassInclusiveClasses)
                .HasForeignKey(t => t.ExamClassId);
            HasRequired(e => e.ExamClass2)
                .WithMany(e => e.ExamClassInclusiveClasses2)
                .HasForeignKey(t => t.ExamClassIdInclusive);
        }
    }
}
