using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS to entity <see cref="ExamClass"/>
    /// </summary>
    internal sealed class ExamClassMapping: EntityTypeConfiguration<ExamClass>
    {
        
        public static readonly ExamClassMapping Instance = new ExamClassMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassMapping" /> class.
        /// </summary>
        private ExamClassMapping()
        {

            ToTable("DRL_EXAM_CLASS", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClass.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ExamClass.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.Description)
                .HasColumnName(ExamClass.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.IsMofa)
                .HasColumnName(ExamClass.Fields.IsMofa)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClass.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClass.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClass.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClass.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClass.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClass.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClass.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClass.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamClass.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamClass.Fields.ToDate)
                .IsRequired();

            Property(t => t.IsFsClass)
                .HasColumnName(ExamClass.Fields.IsFsClass)
                .IsRequired();

            Property(t => t.SortOrder)
                .HasColumnName(ExamClass.Fields.SortOrder)
                .IsRequired();


            //Relationships
        }
    }
}
