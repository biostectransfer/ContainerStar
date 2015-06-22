using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Container_Equipment_Rsp to entity <see cref="ContainerEquipmentRsp"/>
    /// </summary>
    internal sealed class ContainerEquipmentRspMapping: EntityTypeConfiguration<ContainerEquipmentRsp>
    {
        
        public static readonly ContainerEquipmentRspMapping Instance = new ContainerEquipmentRspMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainerEquipmentRspMapping" /> class.
        /// </summary>
        private ContainerEquipmentRspMapping()
        {

            ToTable("Container_Equipment_Rsp", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ContainerEquipmentRsp.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ContainerId)
                .HasColumnName(ContainerEquipmentRsp.Fields.ContainerId)
                .IsRequired();

            Property(t => t.EquipmentId)
                .HasColumnName(ContainerEquipmentRsp.Fields.EquipmentId)
                .IsRequired();

            Property(t => t.Amount)
                .HasColumnName(ContainerEquipmentRsp.Fields.Amount)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ContainerEquipmentRsp.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ContainerEquipmentRsp.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ContainerEquipmentRsp.Fields.DeleteDate);


            //Relationships
            HasRequired(c => c.Containers)
                .WithMany(c => c.ContainerEquipmentRsps)
                .HasForeignKey(t => t.ContainerId);
            HasRequired(c => c.Equipments)
                .WithMany(e => e.ContainerEquipmentRsps)
                .HasForeignKey(t => t.EquipmentId);
        }
    }
}
