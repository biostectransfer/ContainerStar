using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_VALIDATION_ERROR to entity <see cref="ValidationError"/>
    /// </summary>
    internal sealed class ValidationErrorMapping: EntityTypeConfiguration<ValidationError>
    {
        
        public static readonly ValidationErrorMapping Instance = new ValidationErrorMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ValidationErrorMapping" /> class.
        /// </summary>
        private ValidationErrorMapping()
        {

            ToTable("DRL_VALIDATION_ERROR", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ValidationError.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ErrorNumber)
                .HasColumnName(ValidationError.Fields.ErrorNumber)
                .IsRequired();

            Property(t => t.ErrorName)
                .HasColumnName(ValidationError.Fields.ErrorName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.IsCritical)
                .HasColumnName(ValidationError.Fields.IsCritical)
                .IsRequired();

            Property(t => t.IsPopupRequired)
                .HasColumnName(ValidationError.Fields.IsPopupRequired)
                .IsRequired();

            Property(t => t.IsIgnoreAllowed)
                .HasColumnName(ValidationError.Fields.IsIgnoreAllowed)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ValidationError.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ValidationError.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ValidationError.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ValidationError.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ValidationError.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ValidationError.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ValidationError.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ValidationError.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.IsArgetpCorrect)
                .HasColumnName(ValidationError.Fields.IsArgetpCorrect)
                .IsRequired();

            Property(t => t.IsAsproCorrectAllowed)
                .HasColumnName(ValidationError.Fields.IsAsproCorrectAllowed)
                .IsRequired();

            Property(t => t.ErrorClass)
                .HasColumnName(ValidationError.Fields.ErrorClass)
                .IsUnicode()
                .HasMaxLength(2)
                .IsFixedLength();


            //Relationships
        }
    }
}
