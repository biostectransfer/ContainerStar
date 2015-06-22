using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS_REQUIRED_CLASS_RSP to entity <see cref="ExamClassRequiredClass"/>
    /// </summary>
    internal sealed class ExamClassRequiredClassMapping: EntityTypeConfiguration<ExamClassRequiredClass>
    {
        
        public static readonly ExamClassRequiredClassMapping Instance = new ExamClassRequiredClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassRequiredClassMapping" /> class.
        /// </summary>
        private ExamClassRequiredClassMapping()
        {

            ToTable("DRL_EXAM_CLASS_REQUIRED_CLASS_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClassRequiredClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamClassId)
                .HasColumnName(ExamClassRequiredClass.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.ExamClassIdRequired)
                .HasColumnName(ExamClassRequiredClass.Fields.ExamClassIdRequired)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClassRequiredClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClassRequiredClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClassRequiredClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClassRequiredClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClassRequiredClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClassRequiredClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClassRequiredClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClassRequiredClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamClassRequiredClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamClassRequiredClass.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamClassRequiredClasses)
                .HasForeignKey(t => t.ExamClassId);
            HasRequired(e => e.ExamClass2)
                .WithMany(e => e.ExamClassRequiredClasses2)
                .HasForeignKey(t => t.ExamClassIdRequired);
        }
    }
}
