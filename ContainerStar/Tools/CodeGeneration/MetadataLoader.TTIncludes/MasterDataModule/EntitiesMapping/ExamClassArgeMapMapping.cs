using MetadataLoader.TTIncludes.MasterDataModule.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MetadataLoader.TTIncludes.MasterDataModule.EntitiesMapping
{
    /// <summary>
    ///     Mappping table DATA.DRL_EXAM_CLASS_ARGE_MAP to entity <see cref="ExamClassArgeMap"/>
    /// </summary>
    internal sealed class ExamClassArgeMapMapping: EntityTypeConfiguration<ExamClassArgeMap>
    {
        
        public static readonly ExamClassArgeMapMapping Instance = new ExamClassArgeMapMapping();
        
        /// <summary>
        ///     Initializes a new instance of the <see cref="ExamClassArgeMapMapping" /> class.
        /// </summary>
        private ExamClassArgeMapMapping()
        {

            ToTable("DRL_EXAM_CLASS_ARGE_MAP", "DATA");
            // Primary Key
            HasKey(t => t.Id);

            //Properties
            Property(t => t.Id)
                .HasColumnName(ExamClassArgeMap.Fields.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(t => t.ExamNameArge)
                .HasColumnName(ExamClassArgeMap.Fields.ExamNameArge)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(10);

            Property(t => t.ExamClassId)
                .HasColumnName(ExamClassArgeMap.Fields.ExamClassId)
                .IsRequired();

            Property(t => t.CreateDate)
                .HasColumnName(ExamClassArgeMap.Fields.CreateDate)
                .IsRequired();

            Property(t => t.ChangeDate)
                .HasColumnName(ExamClassArgeMap.Fields.ChangeDate)
                .IsRequired();

            Property(t => t.DeleteDate)
                .HasColumnName(ExamClassArgeMap.Fields.DeleteDate);

            Property(t => t.OwnerOrgId)
                .HasColumnName(ExamClassArgeMap.Fields.OwnerOrgId);

            Property(t => t.VisibilityOrgId)
                .HasColumnName(ExamClassArgeMap.Fields.VisibilityOrgId);

            Property(t => t.CreateEmployeeId)
                .HasColumnName(ExamClassArgeMap.Fields.CreateEmployeeId);

            Property(t => t.ChangeEmployeeId)
                .HasColumnName(ExamClassArgeMap.Fields.ChangeEmployeeId);

            Property(t => t.Source)
                .HasColumnName(ExamClassArgeMap.Fields.Source)
                .HasMaxLength(10);


            //Relationships
            HasRequired(e => e.ExamClass)
                .WithMany(e => e.ExamClassArgeMaps)
                .HasForeignKey(t => t.ExamClassId);
        }
    }
}
