using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Role to entity <see cref="Role"/>
    /// </summary>
    internal sealed class RoleMapping: EntityTypeConfiguration<Role>
    {
        
        public static readonly RoleMapping Instance = new RoleMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="RoleMapping" /> class.
        /// </summary>
        private RoleMapping()
        {

            ToTable("Role", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Role.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Name)
                .HasColumnName(Role.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(256);

            Property(t => t.CreateDate)
                .HasColumnName(Role.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Role.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Role.Fields.DeleteDate);


            //Relationships
        }
    }
}
