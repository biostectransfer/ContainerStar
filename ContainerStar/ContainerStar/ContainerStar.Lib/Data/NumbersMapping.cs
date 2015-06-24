using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ContainerStar.Contracts.Entities;

namespace ContainerStar.Lib.Data
{
    public class NumbersMapping : EntityTypeConfiguration<Numbers>
    {
        public static readonly NumbersMapping Instance = new NumbersMapping();

        private NumbersMapping()  
        {

            ToTable("Numbers", "dbo");
            // Primary Key
            HasKey(t => t.Id);
            ;

            //Properties
            Property(t => t.Id)
                .HasColumnName(Numbers.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.NumberType)
                .HasColumnName(Numbers.Fields.NumberType);

            Property(t => t.CurrentNumber)
               .HasColumnName(Numbers.Fields.CurrentNumber);

        }
    }
}
