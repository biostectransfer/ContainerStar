using System;

namespace MetadataLoader.TTIncludes.MasterDataModule.Entities
{
    /// <summary>
    ///     EN: 5.2.12.7 Legal Basis  DE: 5.2.12.7 Fahrerlaubnis - Rechtsgruende
    /// </summary>
    public partial class LegalBasis
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "DATA.DRL_LEGAL_BASIS";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'ID' for property <see cref="LegalBasis.Id"/>
            /// </summary>
            public static readonly string Id = "ID";
            /// <summary>
            /// Column name 'NAME' for property <see cref="LegalBasis.Name"/>
            /// </summary>
            public static readonly string Name = "NAME";
            /// <summary>
            /// Column name 'DESCRIPTION' for property <see cref="LegalBasis.Description"/>
            /// </summary>
            public static readonly string Description = "DESCRIPTION";
            /// <summary>
            /// Column name 'EDUCATION_CERTIFICATE_REQUIRED' for property <see cref="LegalBasis.EducationCertificateRequired"/>
            /// </summary>
            public static readonly string EducationCertificateRequired = "EDUCATION_CERTIFICATE_REQUIRED";
            /// <summary>
            /// Column name 'FIRST_ASSIGNATION' for property <see cref="LegalBasis.FirstAssignation"/>
            /// </summary>
            public static readonly string FirstAssignation = "FIRST_ASSIGNATION";
            /// <summary>
            /// Column name 'MESSAGE_REASON' for property <see cref="LegalBasis.MessageReason"/>
            /// </summary>
            public static readonly string MessageReason = "MESSAGE_REASON";
            /// <summary>
            /// Column name 'MESSAGE_REASON_STYLE' for property <see cref="LegalBasis.MessageReasonStyle"/>
            /// </summary>
            public static readonly string MessageReasonStyle = "MESSAGE_REASON_STYLE";
            /// <summary>
            /// Column name 'CREATE_DATE' for property <see cref="LegalBasis.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CREATE_DATE";
            /// <summary>
            /// Column name 'CHANGE_DATE' for property <see cref="LegalBasis.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "CHANGE_DATE";
            /// <summary>
            /// Column name 'DELETE_DATE' for property <see cref="LegalBasis.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DELETE_DATE";
            /// <summary>
            /// Column name 'OWNER_ORG_ID' for property <see cref="LegalBasis.OwnerOrgId"/>
            /// </summary>
            public static readonly string OwnerOrgId = "OWNER_ORG_ID";
            /// <summary>
            /// Column name 'VISIBILITY_ORG_ID' for property <see cref="LegalBasis.VisibilityOrgId"/>
            /// </summary>
            public static readonly string VisibilityOrgId = "VISIBILITY_ORG_ID";
            /// <summary>
            /// Column name 'CREATE_EMPLOYEE_ID' for property <see cref="LegalBasis.CreateEmployeeId"/>
            /// </summary>
            public static readonly string CreateEmployeeId = "CREATE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'CHANGE_EMPLOYEE_ID' for property <see cref="LegalBasis.ChangeEmployeeId"/>
            /// </summary>
            public static readonly string ChangeEmployeeId = "CHANGE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'SOURCE' for property <see cref="LegalBasis.Source"/>
            /// </summary>
            public static readonly string Source = "SOURCE";
            /// <summary>
            /// Column name 'FROM_DATE' for property <see cref="LegalBasis.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FROM_DATE";
            /// <summary>
            /// Column name 'TO_DATE' for property <see cref="LegalBasis.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "TO_DATE";
            /// <summary>
            /// Column name 'REPLACEMENT_ID' for property <see cref="LegalBasis.ReplacementId"/>
            /// </summary>
            public static readonly string ReplacementId = "REPLACEMENT_ID";
            /// <summary>
            /// Column name 'PRINT_NAME' for property <see cref="LegalBasis.PrintName"/>
            /// </summary>
            public static readonly string PrintName = "PRINT_NAME";
          
        }
        #endregion
        /// <summary>
        ///     EN: PK  DE: Primaerschluessel
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        ///     EN: Legal Basis (PK in old system)  DE: Rechtsgrund (Primaerschluessel im Altsystem)
        /// </summary>
        public string Name{ get; set; }
        /// <summary>
        ///     EN: Description  DE: Beschreibung
        /// </summary>
        public string Description{ get; set; }
        /// <summary>
        ///     EN: Indication whether Education Certificate is required  DE: Angabe, ob Ausbildungsbescheinigung notwendig ist
        /// </summary>
        public bool EducationCertificateRequired{ get; set; }
        /// <summary>
        ///     EN: Indication whether it is first assignation of the driver licence  DE: Angabe, ob Ersterteilung
        /// </summary>
        public int FirstAssignation{ get; set; }
        /// <summary>
        ///     EN: Statement regarding Message Reason  DE: Angabe zum Mitteilungsgrund
        /// </summary>
        public string MessageReason{ get; set; }
        /// <summary>
        ///     EN: Statement regarding ADMI2 (Type of Message)  DE: Angabe ADMI2 (Art des Miteilungsgrundes)
        /// </summary>
        public string MessageReasonStyle{ get; set; }
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
        ///     DE: Erzarz-Rechtsgrund die wird beim Wechsel von Ersterteilung nach Erweiterung benutzt (falls eine Theorie bestanden) EN: Legal basis for replacement First assignation on Extending 
        /// </summary>
        public int? ReplacementId{ get; set; }
        /// <summary>
        ///     EN: Print name
        /// </summary>
        public string PrintName{ get; set; }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public LegalBasis ShallowCopy()
        {
            return new LegalBasis {
                       Name = Name,
                       Description = Description,
                       EducationCertificateRequired = EducationCertificateRequired,
                       FirstAssignation = FirstAssignation,
                       MessageReason = MessageReason,
                       MessageReasonStyle = MessageReasonStyle,
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
                       ReplacementId = ReplacementId,
                       PrintName = PrintName,
        	           };
        }
    }
}
