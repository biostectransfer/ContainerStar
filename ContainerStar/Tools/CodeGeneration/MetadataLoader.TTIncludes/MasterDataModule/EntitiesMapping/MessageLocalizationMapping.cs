using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_MESSAGE_LOCALIZATION to entity <see cref="MessageLocalization"/>
    /// </summary>
    internal sealed class MessageLocalizationMapping: EntityTypeConfiguration<MessageLocalization>
    {
        
        public static readonly MessageLocalizationMapping Instance = new MessageLocalizationMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="MessageLocalizationMapping" /> class.
        /// </summary>
        private MessageLocalizationMapping()
        {

            ToTable("DRL_MESSAGE_LOCALIZATION", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(MessageLocalization.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ValidationErrorNumber)
                .HasColumnName(MessageLocalization.Fields.ValidationErrorNumber)
                .IsRequired();

            Property(t => t.SysLanguageId)
                .HasColumnName(MessageLocalization.Fields.SysLanguageId)
                .IsRequired();

            Property(t => t.Message)
                .HasColumnName(MessageLocalization.Fields.Message)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(500);

            Property(t => t.CreateDate)
                .HasColumnName(MessageLocalization.Fields.CreateDate);

            Property(t => t.ChangeDate)
                .HasColumnName(MessageLocalization.Fields.ChangeDate);

            Property(t => t.DeleteDate)
                .HasColumnName(MessageLocalization.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(MessageLocalization.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(MessageLocalization.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(MessageLocalization.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(MessageLocalization.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(MessageLocalization.Fields.Source)
                .HasMaxLength(10);


            //Relationships
        }
    }
}
