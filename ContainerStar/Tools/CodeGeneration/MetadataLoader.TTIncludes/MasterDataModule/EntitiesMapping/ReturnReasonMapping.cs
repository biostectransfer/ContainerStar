using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_RETURN_REASON to entity <see cref="ReturnReason"/>
    /// </summary>
    internal sealed class ReturnReasonMapping: EntityTypeConfiguration<ReturnReason>
    {
        
        public static readonly ReturnReasonMapping Instance = new ReturnReasonMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReturnReasonMapping" /> class.
        /// </summary>
        private ReturnReasonMapping()
        {

            ToTable("DRL_RETURN_REASON", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ReturnReason.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(ReturnReason.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Description)
                .HasColumnName(ReturnReason.Fields.Description)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(250);

            Property(t => t.Text1)
                .HasColumnName(ReturnReason.Fields.Text1)
                .IsUnicode()
                .HasMaxLength(200);

            Property(t => t.Text2)
                .HasColumnName(ReturnReason.Fields.Text2)
                .IsUnicode()
                .HasMaxLength(200);

            Property(t => t.CreateDate)
                .HasColumnName(ReturnReason.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ReturnReason.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ReturnReason.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ReturnReason.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ReturnReason.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ReturnReason.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ReturnReason.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ReturnReason.Fields.Source)
                .HasMaxLength(10);

            Property(t => t.FromDate)
                .HasColumnName(ReturnReason.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(ReturnReason.Fields.ToDate)
                .IsRequired();


            //Relationships
        }
    }
}
