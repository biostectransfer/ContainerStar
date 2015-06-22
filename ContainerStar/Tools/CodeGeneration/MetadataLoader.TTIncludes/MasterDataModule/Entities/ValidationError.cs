using System;

namespace MetadataLoader.TTIncludes.MasterDataModule.Entities
{
    /// <summary>
    ///     DE: Validierungsfehler beim Import des Pruefauftrages EN: Exam Order import Validation errors
    /// </summary>
    public partial class ValidationError
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "DATA.DRL_VALIDATION_ERROR";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'ID' for property <see cref="ValidationError.Id"/>
            /// </summary>
            public static readonly string Id = "ID";
            /// <summary>
            /// Column name 'ERROR_NUMBER' for property <see cref="ValidationError.ErrorNumber"/>
            /// </summary>
            public static readonly string ErrorNumber = "ERROR_NUMBER";
            /// <summary>
            /// Column name 'ERROR_NAME' for property <see cref="ValidationError.ErrorName"/>
            /// </summary>
            public static readonly string ErrorName = "ERROR_NAME";
            /// <summary>
            /// Column name 'IS_CRITICAL' for property <see cref="ValidationError.IsCritical"/>
            /// </summary>
            public static readonly string IsCritical = "IS_CRITICAL";
            /// <summary>
            /// Column name 'IS_POPUP_REQUIRED' for property <see cref="ValidationError.IsPopupRequired"/>
            /// </summary>
            public static readonly string IsPopupRequired = "IS_POPUP_REQUIRED";
            /// <summary>
            /// Column name 'IS_IGNORE_ALLOWED' for property <see cref="ValidationError.IsIgnoreAllowed"/>
            /// </summary>
            public static readonly string IsIgnoreAllowed = "IS_IGNORE_ALLOWED";
            /// <summary>
            /// Column name 'CREATE_DATE' for property <see cref="ValidationError.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CREATE_DATE";
            /// <summary>
            /// Column name 'CHANGE_DATE' for property <see cref="ValidationError.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "CHANGE_DATE";
            /// <summary>
            /// Column name 'DELETE_DATE' for property <see cref="ValidationError.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DELETE_DATE";
            /// <summary>
            /// Column name 'OWNER_ORG_ID' for property <see cref="ValidationError.OwnerOrgId"/>
            /// </summary>
            public static readonly string OwnerOrgId = "OWNER_ORG_ID";
            /// <summary>
            /// Column name 'VISIBILITY_ORG_ID' for property <see cref="ValidationError.VisibilityOrgId"/>
            /// </summary>
            public static readonly string VisibilityOrgId = "VISIBILITY_ORG_ID";
            /// <summary>
            /// Column name 'CREATE_EMPLOYEE_ID' for property <see cref="ValidationError.CreateEmployeeId"/>
            /// </summary>
            public static readonly string CreateEmployeeId = "CREATE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'CHANGE_EMPLOYEE_ID' for property <see cref="ValidationError.ChangeEmployeeId"/>
            /// </summary>
            public static readonly string ChangeEmployeeId = "CHANGE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'SOURCE' for property <see cref="ValidationError.Source"/>
            /// </summary>
            public static readonly string Source = "SOURCE";
            /// <summary>
            /// Column name 'IS_ARGETP_CORRECT' for property <see cref="ValidationError.IsArgetpCorrect"/>
            /// </summary>
            public static readonly string IsArgetpCorrect = "IS_ARGETP_CORRECT";
            /// <summary>
            /// Column name 'IS_ASPRO_CORRECT_ALLOWED' for property <see cref="ValidationError.IsAsproCorrectAllowed"/>
            /// </summary>
            public static readonly string IsAsproCorrectAllowed = "IS_ASPRO_CORRECT_ALLOWED";
            /// <summary>
            /// Column name 'ERROR_CLASS' for property <see cref="ValidationError.ErrorClass"/>
            /// </summary>
            public static readonly string ErrorClass = "ERROR_CLASS";
          
        }
        #endregion
        /// <summary>
        ///     DE: Primaerschluessel EN: PK
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        ///     DE: Fehlernummer EN: error number
        /// </summary>
        public int ErrorNumber{ get; set; }
        /// <summary>
        ///     DE: Fehler EN: Error
        /// </summary>
        public string ErrorName{ get; set; }
        /// <summary>
        ///     DE: Angabe ob Fehler kritisch ist EN: Indication whether error is critical
        /// </summary>
        public bool IsCritical{ get; set; }
        /// <summary>
        ///     DE: Zeigt, ob die Fehlermeldung im Popup anzuzeigen ist EN: Indication whether error should be shown in popup 
        /// </summary>
        public bool IsPopupRequired{ get; set; }
        /// <summary>
        ///     DE: Zeigt, ob die Fehler ignoriert werden darf ist EN: Indication whether error should be corrected
        /// </summary>
        public bool IsIgnoreAllowed{ get; set; }
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
        ///     EN: Is Arge TP correct?
        /// </summary>
        public bool IsArgetpCorrect{ get; set; }
        /// <summary>
        ///     EN: Is AsPro correction allowed
        /// </summary>
        public bool IsAsproCorrectAllowed{ get; set; }
        /// <summary>
        ///     Error class for error control
        /// </summary>
        public string ErrorClass{ get; set; }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public ValidationError ShallowCopy()
        {
            return new ValidationError {
                       ErrorNumber = ErrorNumber,
                       ErrorName = ErrorName,
                       IsCritical = IsCritical,
                       IsPopupRequired = IsPopupRequired,
                       IsIgnoreAllowed = IsIgnoreAllowed,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       OwnerOrgId = OwnerOrgId,
                       VisibilityOrgId = VisibilityOrgId,
                       CreateEmployeeId = CreateEmployeeId,
                       ChangeEmployeeId = ChangeEmployeeId,
                       Source = Source,
                       IsArgetpCorrect = IsArgetpCorrect,
                       IsAsproCorrectAllowed = IsAsproCorrectAllowed,
                       ErrorClass = ErrorClass,
        	           };
        }
    }
}
