using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CONSTRAINT to entity <see cref="ExamConstraint"/>
    /// </summary>
    internal sealed class ExamConstraintMapping: EntityTypeConfiguration<ExamConstraint>
    {
        
        public static readonly ExamConstraintMapping Instance = new ExamConstraintMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamConstraintMapping" /> class.
        /// </summary>
        private ExamConstraintMapping()
        {

            ToTable("DRL_EXAM_CONSTRAINT", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamConstraint.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ExamConstraint.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(ExamConstraint.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.ConstraintType)
                .HasColumnName(ExamConstraint.Fields.ConstraintType)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamConstraint.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamConstraint.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamConstraint.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamConstraint.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamConstraint.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamConstraint.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamConstraint.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamConstraint.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamConstraint.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamConstraint.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
