using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS_MAP to entity <see cref="ExamClassMap"/>
    /// </summary>
    internal sealed class ExamClassMapMapping: EntityTypeConfiguration<ExamClassMap>
    {
        
        public static readonly ExamClassMapMapping Instance = new ExamClassMapMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassMapMapping" /> class.
        /// </summary>
        private ExamClassMapMapping()
        {

            ToTable("DRL_EXAM_CLASS_MAP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClassMap.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamClassIdOld)
                .HasColumnName(ExamClassMap.Fields.ExamClassIdOld)
                .IsRequired();

            Property(t => t.ExamClassIdActual)
                .HasColumnName(ExamClassMap.Fields.ExamClassIdActual)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClassMap.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClassMap.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClassMap.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClassMap.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClassMap.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClassMap.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClassMap.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClassMap.Fields.Source)
                .HasMaxLength(10);


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamClassMaps)
                .HasForeignKey(t => t.ExamClassIdActual);
            HasRequired(e => e.ExamClass2)
                .WithMany(e => e.ExamClassMaps2)
                .HasForeignKey(t => t.ExamClassIdOld);
        }
    }
}
