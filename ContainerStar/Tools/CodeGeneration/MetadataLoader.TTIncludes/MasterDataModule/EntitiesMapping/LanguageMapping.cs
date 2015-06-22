using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_LANGUAGE to entity <see cref="Language"/>
    /// </summary>
    internal sealed class LanguageMapping: EntityTypeConfiguration<Language>
    {
        
        public static readonly LanguageMapping Instance = new LanguageMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="LanguageMapping" /> class.
        /// </summary>
        private LanguageMapping()
        {

            ToTable("DRL_LANGUAGE", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Language.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.SysLanguageId)
                .HasColumnName(Language.Fields.SysLanguageId)
                .IsRequired();

            Property(t => t.OldAbbr)
                .HasColumnName(Language.Fields.OldAbbr)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.CreateDate)
                .HasColumnName(Language.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Language.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Language.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(Language.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(Language.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(Language.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(Language.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(Language.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(Language.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(Language.Fields.ToDate)
                .IsRequired();

            Property(t => t.RowVersion)
                .HasColumnName(Language.Fields.RowVersion)
                .IsRequired();


            //Relationships
        }
    }
}
