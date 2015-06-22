using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Role_Permission_Rsp to entity <see cref="RolePermissionRsp"/>
    /// </summary>
    internal sealed class RolePermissionRspMapping: EntityTypeConfiguration<RolePermissionRsp>
    {
        
        public static readonly RolePermissionRspMapping Instance = new RolePermissionRspMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="RolePermissionRspMapping" /> class.
        /// </summary>
        private RolePermissionRspMapping()
        {

            ToTable("Role_Permission_Rsp", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(RolePermissionRsp.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.RoleId)
                .HasColumnName(RolePermissionRsp.Fields.RoleId)
                .IsRequired();

            Property(t => t.PermissionId)
                .HasColumnName(RolePermissionRsp.Fields.PermissionId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(RolePermissionRsp.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(RolePermissionRsp.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(RolePermissionRsp.Fields.DeleteDate);


            //Relationships
            HasRequired(r => r.Permission)
                .WithMany(p => p.RolePermissionRsps)
                .HasForeignKey(t => t.PermissionId);
            HasRequired(r => r.Role)
                .WithMany(r => r.RolePermissionRsps)
                .HasForeignKey(t => t.RoleId);
        }
    }
}
