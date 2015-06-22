using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_HOLIDAY_ORD_FEDERAL_STATE_RSP to entity <see cref="HolidayOrdFederalState"/>
    /// </summary>
    internal sealed class HolidayOrdFederalStateMapping: EntityTypeConfiguration<HolidayOrdFederalState>
    {
        
        public static readonly HolidayOrdFederalStateMapping Instance = new HolidayOrdFederalStateMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="HolidayOrdFederalStateMapping" /> class.
        /// </summary>
        private HolidayOrdFederalStateMapping()
        {

            ToTable("DRL_HOLIDAY_ORD_FEDERAL_STATE_RSP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(HolidayOrdFederalState.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.HolidayId)
                .HasColumnName(HolidayOrdFederalState.Fields.HolidayId)
                .IsRequired();

            Property(t => t.OrdFederalStateId)
                .HasColumnName(HolidayOrdFederalState.Fields.OrdFederalStateId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(HolidayOrdFederalState.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(HolidayOrdFederalState.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(HolidayOrdFederalState.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(HolidayOrdFederalState.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(HolidayOrdFederalState.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(HolidayOrdFederalState.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(HolidayOrdFederalState.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(HolidayOrdFederalState.Fields.Source)
                .HasMaxLength(10);


            //Relationships
            HasRequired(h => h.Holiday)
                .WithMany(h => h.HolidayOrdFederalStates)
                .HasForeignKey(t => t.HolidayId);
        }
    }
}
