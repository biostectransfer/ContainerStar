using System;
using System.Collections.Generic;

namespace MetadataLoader.TTIncludes.MasterDataModule.Entities
{
    /// <summary>
    ///     EN: 5.2.12.5 Class  DE: 5.2.12.5 Fahrerlaubnis - Klassen
    /// </summary>
    public partial class ExamClass
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "DATA.DRL_EXAM_CLASS";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'ID' for property <see cref="ExamClass.Id"/>
            /// </summary>
            public static readonly string Id = "ID";
            /// <summary>
            /// Column name 'NAME' for property <see cref="ExamClass.Name"/>
            /// </summary>
            public static readonly string Name = "NAME";
            /// <summary>
            /// Column name 'DESCRIPTION' for property <see cref="ExamClass.Description"/>
            /// </summary>
            public static readonly string Description = "DESCRIPTION";
            /// <summary>
            /// Column name 'IS_MOFA' for property <see cref="ExamClass.IsMofa"/>
            /// </summary>
            public static readonly string IsMofa = "IS_MOFA";
            /// <summary>
            /// Column name 'CREATE_DATE' for property <see cref="ExamClass.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CREATE_DATE";
            /// <summary>
            /// Column name 'CHANGE_DATE' for property <see cref="ExamClass.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "CHANGE_DATE";
            /// <summary>
            /// Column name 'DELETE_DATE' for property <see cref="ExamClass.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DELETE_DATE";
            /// <summary>
            /// Column name 'OWNER_ORG_ID' for property <see cref="ExamClass.OwnerOrgId"/>
            /// </summary>
            public static readonly string OwnerOrgId = "OWNER_ORG_ID";
            /// <summary>
            /// Column name 'VISIBILITY_ORG_ID' for property <see cref="ExamClass.VisibilityOrgId"/>
            /// </summary>
            public static readonly string VisibilityOrgId = "VISIBILITY_ORG_ID";
            /// <summary>
            /// Column name 'CREATE_EMPLOYEE_ID' for property <see cref="ExamClass.CreateEmployeeId"/>
            /// </summary>
            public static readonly string CreateEmployeeId = "CREATE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'CHANGE_EMPLOYEE_ID' for property <see cref="ExamClass.ChangeEmployeeId"/>
            /// </summary>
            public static readonly string ChangeEmployeeId = "CHANGE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'SOURCE' for property <see cref="ExamClass.Source"/>
            /// </summary>
            public static readonly string Source = "SOURCE";
            /// <summary>
            /// Column name 'FROM_DATE' for property <see cref="ExamClass.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FROM_DATE";
            /// <summary>
            /// Column name 'TO_DATE' for property <see cref="ExamClass.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "TO_DATE";
            /// <summary>
            /// Column name 'IS_FS_CLASS' for property <see cref="ExamClass.IsFsClass"/>
            /// </summary>
            public static readonly string IsFsClass = "IS_FS_CLASS";
            /// <summary>
            /// Column name 'SORT_ORDER' for property <see cref="ExamClass.SortOrder"/>
            /// </summary>
            public static readonly string SortOrder = "SORT_ORDER";
          
        }
        #endregion
        /// <summary>
        ///     EN: PK  DE: Primaerschluessel
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        ///     DE: FE-Klasse (Primaerschluessel im Altsystem) EN: Class (PK in old system)
        /// </summary>
        public string Name{ get; set; }
        /// <summary>
        ///     EN: Description  DE: Beschreibung
        /// </summary>
        public string Description{ get; set; }
        /// <summary>
        ///     DE: Angabe, dass es sich um MOFA Prüfbescheinigung handelt EN: Indication that it is MOFA
        /// </summary>
        public bool IsMofa{ get; set; }
        /// <summary>
        ///     ANLAGEDATUM (INSERT-DATUM)
        /// </summary>
        public DateTime CreateDate{ get; set; }
        /// <summary>
        ///     AENDERUNGSDATUM (UPDATE-DATUM)
        /// </summary>
        public DateTime ChangeDate{ get; set; }
        /// <summary>
        ///     LOESCHDATUM FUER LOGISCHE LOESCHUNG VON DATENSAETZEN (DELETE-DATUM)
        /// </summary>
        public DateTime? DeleteDate{ get; set; }
        /// <summary>
        ///     OWNER (SCHLUESSEL EINER ORGANISATIONSEINHEIT, WELCHE FUER DIE PFLEGE EINES DATENSATZES
        /// </summary>
        public int? OwnerOrgId{ get; set; }
        /// <summary>
        ///     SICHTBARKEIT (SCHLUESSEL EINER ORGANISATIONSEINHEIT, AB WELCHER IM HIERACHIEBAUM ABWAERTS EIN DATENSATZ SICHTBAR IST)
        /// </summary>
        public int? VisibilityOrgId{ get; set; }
        /// <summary>
        ///     ANLEGER (PERSONALNUMMER DES MITARBEITERS, DER DEN DATENSATZ ANGELEGT HAT)
        /// </summary>
        public int? CreateEmployeeId{ get; set; }
        /// <summary>
        ///     AENDERER (PERSONALNUMMER DES MITARBEITERS, DER DEN DATENSATZ ALS LETZTES GEAENDERT HAT, BEI NEUANLAGE IST DIESER WERT GLEICH DEM ANLEGER)
        /// </summary>
        public int? ChangeEmployeeId{ get; set; }
        /// <summary>
        ///     QUELLSYSTEM
        /// </summary>
        public string Source{ get; set; }
        /// <summary>
        ///     VON-DATUM DER GUELTIGKEIT
        /// </summary>
        public DateTime FromDate{ get; set; }
        /// <summary>
        ///     ENDE-DATUM DER GUELTIGKEIT
        /// </summary>
        public DateTime ToDate{ get; set; }
        /// <summary>
        ///     DE: Zeigt an, ob die Klasse als Fuehrerschein-Klasse anzuzeigen ist EN: Indication whether class should be showed as driver licence class
        /// </summary>
        public bool IsFsClass{ get; set; }
        /// <summary>
        ///     DE: Auflistungsreihenfolge EN: Sort order for GUI
        /// </summary>
        public int SortOrder{ get; set; }
        public virtual ICollection<ExamRecognitionTypeExamClass> ExamRecognitionTypeExamClasses{ get; set; }
        public virtual ICollection<ExamClassArgeMap> ExamClassArgeMaps{ get; set; }
        public virtual ICollection<ExamClassInclusiveClass> ExamClassInclusiveClasses{ get; set; }
        public virtual ICollection<ExamClassInclusiveClass> ExamClassInclusiveClasses2{ get; set; }
        public virtual ICollection<ExamClassMap> ExamClassMaps{ get; set; }
        public virtual ICollection<ExamClassMap> ExamClassMaps2{ get; set; }
        public virtual ICollection<ExamClassRequiredClass> ExamClassRequiredClasses{ get; set; }
        public virtual ICollection<ExamClassRequiredClass> ExamClassRequiredClasses2{ get; set; }
        public virtual ICollection<ExamClassRestrictedClass> ExamClassRestrictedClasses{ get; set; }
        public virtual ICollection<ExamClassRestrictedClass> ExamClassRestrictedClasses2{ get; set; }
        public virtual ICollection<ExamConstraintExamClass> ExamConstraintExamClasses{ get; set; }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public ExamClass ShallowCopy()
        {
            return new ExamClass {
                       Name = Name,
                       Description = Description,
                       IsMofa = IsMofa,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       OwnerOrgId = OwnerOrgId,
                       VisibilityOrgId = VisibilityOrgId,
                       CreateEmployeeId = CreateEmployeeId,
                       ChangeEmployeeId = ChangeEmployeeId,
                       Source = Source,
                       FromDate = FromDate,
                       ToDate = ToDate,
                       IsFsClass = IsFsClass,
                       SortOrder = SortOrder,
        	           };
        }
    }
}
