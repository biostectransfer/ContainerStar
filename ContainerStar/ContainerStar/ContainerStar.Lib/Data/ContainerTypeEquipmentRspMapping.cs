using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.ContainerType_Equipment_Rsp to entity <see cref="ContainerTypeEquipmentRsp"/>
    /// </summary>
    internal sealed class ContainerTypeEquipmentRspMapping: EntityTypeConfiguration<ContainerTypeEquipmentRsp>
    {
        
        public static readonly ContainerTypeEquipmentRspMapping Instance = new ContainerTypeEquipmentRspMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerTypeEquipmentRspMapping" /> class.
        /// </summary>
        private ContainerTypeEquipmentRspMapping()
        {

            ToTable("ContainerType_Equipment_Rsp", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ContainerTypeId)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.ContainerTypeId)
                .IsRequired();

            Property(t => t.EquipmentId)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.EquipmentId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.DeleteDate);

            Property(t => t.Amount)
                .HasColumnName(ContainerTypeEquipmentRsp.Fields.Amount)
                .IsRequired();


            //Relationships
            HasRequired(c => c.ContainerTypes)
                .WithMany(c => c.ContainerTypeEquipmentRsps)
                .HasForeignKey(t => t.ContainerTypeId);
            HasRequired(c => c.Equipments)
                .WithMany(e => e.ContainerTypeEquipmentRsps)
                .HasForeignKey(t => t.EquipmentId);
        }
    }
}
