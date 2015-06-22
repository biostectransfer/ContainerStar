using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_POSSIBLE_RESULT to entity <see cref="ExamPossibleResult"/>
    /// </summary>
    internal sealed class ExamPossibleResultMapping: EntityTypeConfiguration<ExamPossibleResult>
    {
        
        public static readonly ExamPossibleResultMapping Instance = new ExamPossibleResultMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamPossibleResultMapping" /> class.
        /// </summary>
        private ExamPossibleResultMapping()
        {

            ToTable("DRL_EXAM_POSSIBLE_RESULT", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamPossibleResult.Fields.Id)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ExamPossibleResult.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(ExamPossibleResult.Fields.Description)
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.IsFeePayable)
                .HasColumnName(ExamPossibleResult.Fields.IsFeePayable)
                .IsRequired();

            Property(t => t.ExamCounterFlag)
                .HasColumnName(ExamPossibleResult.Fields.ExamCounterFlag)
                .IsRequired();

            Property(t => t.NextExamProductFlag)
                .HasColumnName(ExamPossibleResult.Fields.NextExamProductFlag)
                .IsRequired();

            Property(t => t.DriverLicenceFlag)
                .HasColumnName(ExamPossibleResult.Fields.DriverLicenceFlag)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamPossibleResult.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamPossibleResult.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamPossibleResult.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamPossibleResult.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamPossibleResult.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamPossibleResult.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamPossibleResult.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamPossibleResult.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ExamPossibleResult.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ExamPossibleResult.Fields.ToDate)
                .IsRequired();

            Property(t => t.IsMedicalAttestRequired)
                .HasColumnName(ExamPossibleResult.Fields.IsMedicalAttestRequired)
                .IsRequired();


            //Relationships
        }
    }
}
