using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.User to entity <see cref="User"/>
    /// </summary>
    internal sealed class UserMapping: EntityTypeConfiguration<User>
    {
        
        public static readonly UserMapping Instance = new UserMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="UserMapping" /> class.
        /// </summary>
        private UserMapping()
        {

            ToTable("User", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(User.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.RoleId)
                .HasColumnName(User.Fields.RoleId);

            Property(t => t.Login)
                .HasColumnName(User.Fields.Login)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.Name)
                .HasColumnName(User.Fields.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(256);

            Property(t => t.Password)
                .HasColumnName(User.Fields.Password)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.CreateDate)
                .HasColumnName(User.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(User.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(User.Fields.DeleteDate);


            //Relationships
            HasOptional(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(t => t.RoleId);
        }
    }
}
