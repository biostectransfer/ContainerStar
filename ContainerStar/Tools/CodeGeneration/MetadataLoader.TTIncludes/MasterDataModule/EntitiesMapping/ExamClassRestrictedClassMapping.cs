using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS_RESTRICTED_CLASS_RSP to entity <see cref="ExamClassRestrictedClass"/>
    /// </summary>
    internal sealed class ExamClassRestrictedClassMapping: EntityTypeConfiguration<ExamClassRestrictedClass>
    {
        
        public static readonly ExamClassRestrictedClassMapping Instance = new ExamClassRestrictedClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassRestrictedClassMapping" /> class.
        /// </summary>
        private ExamClassRestrictedClassMapping()
        {

            ToTable("DRL_EXAM_CLASS_RESTRICTED_CLASS_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClassRestrictedClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamClassId)
                .HasColumnName(ExamClassRestrictedClass.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.ExamClassIdRestricted)
                .HasColumnName(ExamClassRestrictedClass.Fields.ExamClassIdRestricted)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClassRestrictedClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClassRestrictedClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClassRestrictedClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClassRestrictedClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClassRestrictedClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClassRestrictedClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClassRestrictedClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClassRestrictedClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamClassRestrictedClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamClassRestrictedClass.Fields.ToDate)
                .IsRequired();


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamClassRestrictedClasses)
                .HasForeignKey(t => t.ExamClassId);
            HasRequired(e => e.ExamClass2)
                .WithMany(e => e.ExamClassRestrictedClasses2)
                .HasForeignKey(t => t.ExamClassIdRestricted);
        }
    }
}
