using System;

namespace MetadataLoader.TTIncludes.MasterDataModule.Entities
{
    /// <summary>
    ///     EN: 5.1.8 Authority  DE: 5.1.8 FE-Behoerde
    /// </summary>
    public partial class Authority
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "DATA.DRL_AUTHORITY";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'ID' for property <see cref="Authority.Id"/>
            /// </summary>
            public static readonly string Id = "ID";
            /// <summary>
            /// Column name 'AUTHORITY_NUMBER' for property <see cref="Authority.AuthorityNumber"/>
            /// </summary>
            public static readonly string AuthorityNumber = "AUTHORITY_NUMBER";
            /// <summary>
            /// Column name 'NAME' for property <see cref="Authority.Name"/>
            /// </summary>
            public static readonly string Name = "NAME";
            /// <summary>
            /// Column name 'DESCRIPTION' for property <see cref="Authority.Description"/>
            /// </summary>
            public static readonly string Description = "DESCRIPTION";
            /// <summary>
            /// Column name 'IS_CERTIFICATE_REQUIRED' for property <see cref="Authority.IsCertificateRequired"/>
            /// </summary>
            public static readonly string IsCertificateRequired = "IS_CERTIFICATE_REQUIRED";
            /// <summary>
            /// Column name 'RETURN_TYPE' for property <see cref="Authority.ReturnType"/>
            /// </summary>
            public static readonly string ReturnType = "RETURN_TYPE";
            /// <summary>
            /// Column name 'CREATE_DATE' for property <see cref="Authority.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CREATE_DATE";
            /// <summary>
            /// Column name 'CHANGE_DATE' for property <see cref="Authority.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "CHANGE_DATE";
            /// <summary>
            /// Column name 'DELETE_DATE' for property <see cref="Authority.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DELETE_DATE";
            /// <summary>
            /// Column name 'OWNER_ORG_ID' for property <see cref="Authority.OwnerOrgId"/>
            /// </summary>
            public static readonly string OwnerOrgId = "OWNER_ORG_ID";
            /// <summary>
            /// Column name 'VISIBILITY_ORG_ID' for property <see cref="Authority.VisibilityOrgId"/>
            /// </summary>
            public static readonly string VisibilityOrgId = "VISIBILITY_ORG_ID";
            /// <summary>
            /// Column name 'CREATE_EMPLOYEE_ID' for property <see cref="Authority.CreateEmployeeId"/>
            /// </summary>
            public static readonly string CreateEmployeeId = "CREATE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'CHANGE_EMPLOYEE_ID' for property <see cref="Authority.ChangeEmployeeId"/>
            /// </summary>
            public static readonly string ChangeEmployeeId = "CHANGE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'SOURCE' for property <see cref="Authority.Source"/>
            /// </summary>
            public static readonly string Source = "SOURCE";
            /// <summary>
            /// Column name 'FROM_DATE' for property <see cref="Authority.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FROM_DATE";
            /// <summary>
            /// Column name 'TO_DATE' for property <see cref="Authority.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "TO_DATE";
            /// <summary>
            /// Column name 'ROW_VERSION' for property <see cref="Authority.RowVersion"/>
            /// </summary>
            public static readonly string RowVersion = "ROW_VERSION";
            /// <summary>
            /// Column name 'NAME_2' for property <see cref="Authority.Name2"/>
            /// </summary>
            public static readonly string Name2 = "NAME_2";
            /// <summary>
            /// Column name 'STREET_HOUSE_NUMBER' for property <see cref="Authority.StreetHouseNumber"/>
            /// </summary>
            public static readonly string StreetHouseNumber = "STREET_HOUSE_NUMBER";
            /// <summary>
            /// Column name 'ZIP_CODE' for property <see cref="Authority.ZipCode"/>
            /// </summary>
            public static readonly string ZipCode = "ZIP_CODE";
            /// <summary>
            /// Column name 'CITY' for property <see cref="Authority.City"/>
            /// </summary>
            public static readonly string City = "CITY";
            /// <summary>
            /// Column name 'SYS_COUNTRY_ID' for property <see cref="Authority.SysCountryId"/>
            /// </summary>
            public static readonly string SysCountryId = "SYS_COUNTRY_ID";
            /// <summary>
            /// Column name 'PHONE_1' for property <see cref="Authority.Phone1"/>
            /// </summary>
            public static readonly string Phone1 = "PHONE_1";
            /// <summary>
            /// Column name 'PHONE_2' for property <see cref="Authority.Phone2"/>
            /// </summary>
            public static readonly string Phone2 = "PHONE_2";
            /// <summary>
            /// Column name 'FAX' for property <see cref="Authority.Fax"/>
            /// </summary>
            public static readonly string Fax = "FAX";
          
        }
        #endregion
        /// <summary>
        ///     EN: PK  DE: Primaerschluessel
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        ///     EN: Authority Number (PK in old system)  DE: FE-Behoerdennummer (Primaerschluessel im Altsystem)
        /// </summary>
        public string AuthorityNumber{ get; set; }
        /// <summary>
        ///     EN: Authority Name  DE: Behoerdenname
        /// </summary>
        public string Name{ get; set; }
        /// <summary>
        ///     EN: Description  DE: Bezeichnung
        /// </summary>
        public string Description{ get; set; }
        /// <summary>
        ///     EN: indication whether Education Certificates should to be sent back prematurely  DE: Angabe, ob Ausbildungsbescheinigungen vorzeitig zurueckgeschickt werden sollen
        /// </summary>
        public bool IsCertificateRequired{ get; set; }
        /// <summary>
        ///     EN: way to return information back to Authority (1 - Online, 2 - Paper dokument, 3 - Disk)  DE: Ruecksendeart (1 - Online, 2 - Papier, 3 - Diskette)
        /// </summary>
        public int ReturnType{ get; set; }
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
        ///     Using for optimistic locking
        /// </summary>
        public byte[] RowVersion{ get; set; }
        /// <summary>
        ///     DE: Name 2 EN: Name 2
        /// </summary>
        public string Name2{ get; set; }
        /// <summary>
        ///     DE: Strasse und Hausnummer EN: Street and house number
        /// </summary>
        public string StreetHouseNumber{ get; set; }
        /// <summary>
        ///     DE: PLZ EN: Zip code
        /// </summary>
        public string ZipCode{ get; set; }
        /// <summary>
        ///     DE: Ort EN: City
        /// </summary>
        public string City{ get; set; }
        /// <summary>
        ///     DE: Land EN: Country
        /// </summary>
        public int? SysCountryId{ get; set; }
        /// <summary>
        ///     DE: Telefonnummer 1 EN: Phone number 1
        /// </summary>
        public string Phone1{ get; set; }
        /// <summary>
        ///     DE: Telefonnummer 2 EN: Phone number 2
        /// </summary>
        public string Phone2{ get; set; }
        /// <summary>
        ///     DE: Fax EN: Fax
        /// </summary>
        public string Fax{ get; set; }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public Authority ShallowCopy()
        {
            return new Authority {
                       AuthorityNumber = AuthorityNumber,
                       Name = Name,
                       Description = Description,
                       IsCertificateRequired = IsCertificateRequired,
                       ReturnType = ReturnType,
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
                       RowVersion = RowVersion,
                       Name2 = Name2,
                       StreetHouseNumber = StreetHouseNumber,
                       ZipCode = ZipCode,
                       City = City,
                       SysCountryId = SysCountryId,
                       Phone1 = Phone1,
                       Phone2 = Phone2,
                       Fax = Fax,
        	           };
        }
    }
}
