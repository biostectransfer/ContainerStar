using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Containers to entity <see cref="Containers"/>
    /// </summary>
    internal sealed class ContainersMapping: EntityTypeConfiguration<Containers>
    {
        
        public static readonly ContainersMapping Instance = new ContainersMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ContainersMapping" /> class.
        /// </summary>
        private ContainersMapping()
        {

            ToTable("Containers", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Containers.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.Number)
                .HasColumnName(Containers.Fields.Number)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(20);

            Property(t => t.ContainerTypeId)
                .HasColumnName(Containers.Fields.ContainerTypeId)
                .IsRequired();

            Property(t => t.Length)
                .HasColumnName(Containers.Fields.Length)
                .IsRequired();

            Property(t => t.Width)
                .HasColumnName(Containers.Fields.Width)
                .IsRequired();

            Property(t => t.Height)
                .HasColumnName(Containers.Fields.Height)
                .IsRequired();

            Property(t => t.Color)
                .HasColumnName(Containers.Fields.Color)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(50);

            Property(t => t.Price)
                .HasColumnName(Containers.Fields.Price)
                .IsRequired();

            Property(t => t.ProceedsAccount)
                .HasColumnName(Containers.Fields.ProceedsAccount)
                .IsRequired();

            Property(t => t.IsVirtual)
                .HasColumnName(Containers.Fields.IsVirtual)
                .IsRequired();

            Property(t => t.ManufactureDate)
                .HasColumnName(Containers.Fields.ManufactureDate);

            Property(t => t.BoughtFrom)
                .HasColumnName(Containers.Fields.BoughtFrom)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.BoughtPrice)
                .HasColumnName(Containers.Fields.BoughtPrice);

            Property(t => t.Comment)
                .HasColumnName(Containers.Fields.Comment)
                .IsUnicode()
                .HasMaxLength(128);

            Property(t => t.CreateDate)
                .HasColumnName(Containers.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Containers.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Containers.Fields.DeleteDate);

            Property(t => t.SellPrice)
                .HasColumnName(Containers.Fields.SellPrice)
                .IsRequired();

            Property(t => t.IsSold)
                .HasColumnName(Containers.Fields.IsSold)
                .IsRequired();

            Property(t => t.MinPrice)
                .HasColumnName(Containers.Fields.MinPrice)
                .IsRequired();


            //Relationships
            HasRequired(c => c.ContainerTypes)
                .WithMany(c => c.Containers)
                .HasForeignKey(t => t.ContainerTypeId);
        }
    }
}
