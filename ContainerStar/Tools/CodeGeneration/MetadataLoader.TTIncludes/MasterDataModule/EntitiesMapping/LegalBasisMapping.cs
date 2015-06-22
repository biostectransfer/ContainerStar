using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_LEGAL_BASIS to entity <see cref="LegalBasis"/>
    /// </summary>
    internal sealed class LegalBasisMapping: EntityTypeConfiguration<LegalBasis>
    {
        
        public static readonly LegalBasisMapping Instance = new LegalBasisMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="LegalBasisMapping" /> class.
        /// </summary>
        private LegalBasisMapping()
        {

            ToTable("DRL_LEGAL_BASIS", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(LegalBasis.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(LegalBasis.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(LegalBasis.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.EducationCertificateRequired)
                .HasColumnName(LegalBasis.Fields.EducationCertificateRequired)
                .IsRequired();

            Property(t => t.FirstAssignation)
                .HasColumnName(LegalBasis.Fields.FirstAssignation)
                .IsRequired();

            Property(t => t.MessageReason)
                .HasColumnName(LegalBasis.Fields.MessageReason)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.MessageReasonStyle)
                .HasColumnName(LegalBasis.Fields.MessageReasonStyle)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.CreateDate)
                .HasColumnName(LegalBasis.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(LegalBasis.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(LegalBasis.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(LegalBasis.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(LegalBasis.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(LegalBasis.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(LegalBasis.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(LegalBasis.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(LegalBasis.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(LegalBasis.Fields.ToDate)
                .IsRequired();

            Property(t => t.ReplacementId)
                .HasColumnName(LegalBasis.Fields.ReplacementId);

            Property(t => t.PrintName)
                .HasColumnName(LegalBasis.Fields.PrintName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);


            //Relationships
        }
    }
}
