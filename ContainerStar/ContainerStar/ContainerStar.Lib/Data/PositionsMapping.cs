using ContainerStar.Contracts.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace ContainerStar.Lib.Data
{
    /// <summary>
    ///     Mappping table dbo.Positions to entity <see cref="Positions"/>
    /// </summary>
    internal sealed class PositionsMapping: EntityTypeConfiguration<Positions>
    {
        
        public static readonly PositionsMapping Instance = new PositionsMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="PositionsMapping" /> class.
        /// </summary>
        private PositionsMapping()
        {

            ToTable("Positions", "dbo");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(Positions.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.OrderId)
                .HasColumnName(Positions.Fields.OrderId)
                .IsRequired();

            Property(t => t.IsSellOrder)
                .HasColumnName(Positions.Fields.IsSellOrder)
                .IsRequired();

            Property(t => t.ContainerId)
                .HasColumnName(Positions.Fields.ContainerId);

            Property(t => t.AdditionalCostId)
                .HasColumnName(Positions.Fields.AdditionalCostId);

            Property(t => t.Price)
                .HasColumnName(Positions.Fields.Price)
                .IsRequired();

            Property(t => t.FromDate)
                .HasColumnName(Positions.Fields.FromDate)
                .IsRequired();

            Property(t => t.ToDate)
                .HasColumnName(Positions.Fields.ToDate)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(Positions.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(Positions.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(Positions.Fields.DeleteDate);


            //Relationships
            HasRequired(p => p.Orders)
                .WithMany(o => o.Positions)
                .HasForeignKey(t => t.OrderId);
            HasOptional(p => p.Containers)
                .WithMany(c => c.Positions)
                .HasForeignKey(t => t.ContainerId);
            HasOptional(p => p.AdditionalCosts)
                .WithMany(a => a.Positions)
                .HasForeignKey(t => t.AdditionalCostId);
        }
    }
}
