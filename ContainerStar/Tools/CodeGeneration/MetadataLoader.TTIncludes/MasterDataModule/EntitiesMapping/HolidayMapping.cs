using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_HOLIDAY to entity <see cref="Holiday"/>
    /// </summary>
    internal sealed class HolidayMapping: EntityTypeConfiguration<Holiday>
    {
        
        public static readonly HolidayMapping Instance = new HolidayMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="HolidayMapping" /> class.
        /// </summary>
        private HolidayMapping()
        {

            ToTable("DRL_HOLIDAY", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Holiday.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(Holiday.Fields.Name)
                .IsUnicode()
                .HasMaxLength(90);

            Property(t => t.Date)
                .HasColumnName(Holiday.Fields.Date)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(Holiday.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Holiday.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Holiday.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(Holiday.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(Holiday.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(Holiday.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(Holiday.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(Holiday.Fields.Source)
                .HasMaxLength(10);


            //Relationships
        }
    }
}
