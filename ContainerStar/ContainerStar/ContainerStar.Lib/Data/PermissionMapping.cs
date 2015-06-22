using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Permission to entity <see cref="Permission"/>
    /// </summary>
    internal sealed class PermissionMapping: EntityTypeConfiguration<Permission>
    {
        
        public static readonly PermissionMapping Instance = new PermissionMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="PermissionMapping" /> class.
        /// </summary>
        private PermissionMapping()
        {

            ToTable("Permission", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Permission.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(Permission.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(256);

            Property(t => t.Description)
                .HasColumnName(Permission.Fields.Description)
                .IsUnicode();

            Property(t => t.CreateDate)
                .HasColumnName(Permission.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Permission.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Permission.Fields.DeleteDate);


            //Relationships
        }
    }
}
