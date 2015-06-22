using System;

namespace MetadataLoader.TTIncludes.MasterDataModule.Entities
{
    /// <summary>
    ///     EN: 5.3.2.3 Exam Room  DE: 5.3.2.3 Fahrschule - Pruefplatz zuordnen
    /// </summary>
    public partial class ExamRoom
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "DATA.DRL_EXAM_ROOM";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'ID' for property <see cref="ExamRoom.Id"/>
            /// </summary>
            public static readonly string Id = "ID";
            /// <summary>
            /// Column name 'ROOM_NUMBER' for property <see cref="ExamRoom.RoomNumber"/>
            /// </summary>
            public static readonly string RoomNumber = "ROOM_NUMBER";
            /// <summary>
            /// Column name 'PLACE_AMOUNT' for property <see cref="ExamRoom.PlaceAmount"/>
            /// </summary>
            public static readonly string PlaceAmount = "PLACE_AMOUNT";
            /// <summary>
            /// Column name 'ORG_ORGANIZATIONAL_UNIT_ID' for property <see cref="ExamRoom.OrgOrganizationalUnitId"/>
            /// </summary>
            public static readonly string OrgOrganizationalUnitId = "ORG_ORGANIZATIONAL_UNIT_ID";
            /// <summary>
            /// Column name 'CREATE_DATE' for property <see cref="ExamRoom.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CREATE_DATE";
            /// <summary>
            /// Column name 'CHANGE_DATE' for property <see cref="ExamRoom.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "CHANGE_DATE";
            /// <summary>
            /// Column name 'DELETE_DATE' for property <see cref="ExamRoom.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DELETE_DATE";
            /// <summary>
            /// Column name 'OWNER_ORG_ID' for property <see cref="ExamRoom.OwnerOrgId"/>
            /// </summary>
            public static readonly string OwnerOrgId = "OWNER_ORG_ID";
            /// <summary>
            /// Column name 'VISIBILITY_ORG_ID' for property <see cref="ExamRoom.VisibilityOrgId"/>
            /// </summary>
            public static readonly string VisibilityOrgId = "VISIBILITY_ORG_ID";
            /// <summary>
            /// Column name 'CREATE_EMPLOYEE_ID' for property <see cref="ExamRoom.CreateEmployeeId"/>
            /// </summary>
            public static readonly string CreateEmployeeId = "CREATE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'CHANGE_EMPLOYEE_ID' for property <see cref="ExamRoom.ChangeEmployeeId"/>
            /// </summary>
            public static readonly string ChangeEmployeeId = "CHANGE_EMPLOYEE_ID";
            /// <summary>
            /// Column name 'SOURCE' for property <see cref="ExamRoom.Source"/>
            /// </summary>
            public static readonly string Source = "SOURCE";
            /// <summary>
            /// Column name 'FROM_DATE' for property <see cref="ExamRoom.FromDate"/>
            /// </summary>
            public static readonly string FromDate = "FROM_DATE";
            /// <summary>
            /// Column name 'TO_DATE' for property <see cref="ExamRoom.ToDate"/>
            /// </summary>
            public static readonly string ToDate = "TO_DATE";
            /// <summary>
            /// Column name 'NAME_1' for property <see cref="ExamRoom.Name1"/>
            /// </summary>
            public static readonly string Name1 = "NAME_1";
            /// <summary>
            /// Column name 'NAME_2' for property <see cref="ExamRoom.Name2"/>
            /// </summary>
            public static readonly string Name2 = "NAME_2";
            /// <summary>
            /// Column name 'NAME_3' for property <see cref="ExamRoom.Name3"/>
            /// </summary>
            public static readonly string Name3 = "NAME_3";
            /// <summary>
            /// Column name 'STREET_HOUSE_NUMBER' for property <see cref="ExamRoom.StreetHouseNumber"/>
            /// </summary>
            public static readonly string StreetHouseNumber = "STREET_HOUSE_NUMBER";
            /// <summary>
            /// Column name 'ZIP_CODE' for property <see cref="ExamRoom.ZipCode"/>
            /// </summary>
            public static readonly string ZipCode = "ZIP_CODE";
            /// <summary>
            /// Column name 'ZIP_BOX' for property <see cref="ExamRoom.ZipBox"/>
            /// </summary>
            public static readonly string ZipBox = "ZIP_BOX";
            /// <summary>
            /// Column name 'BOX' for property <see cref="ExamRoom.Box"/>
            /// </summary>
            public static readonly string Box = "BOX";
            /// <summary>
            /// Column name 'CITY' for property <see cref="ExamRoom.City"/>
            /// </summary>
            public static readonly string City = "CITY";
            /// <summary>
            /// Column name 'PHONE_1' for property <see cref="ExamRoom.Phone1"/>
            /// </summary>
            public static readonly string Phone1 = "PHONE_1";
            /// <summary>
            /// Column name 'FAX' for property <see cref="ExamRoom.Fax"/>
            /// </summary>
            public static readonly string Fax = "FAX";
            /// <summary>
            /// Column name 'EMAIL' for property <see cref="ExamRoom.Email"/>
            /// </summary>
            public static readonly string Email = "EMAIL";
            /// <summary>
            /// Column name 'SYS_COUNTRY_ID' for property <see cref="ExamRoom.SysCountryId"/>
            /// </summary>
            public static readonly string SysCountryId = "SYS_COUNTRY_ID";
          
        }
        #endregion
        /// <summary>
        ///     EN: PK  DE: Primaerschluessel
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        ///     DE: Pruefplatznummer (Primaerschluessel im Altsystem) EN: Exam Room Number (PK in old system)
        /// </summary>
        public long RoomNumber{ get; set; }
        /// <summary>
        ///     EN: Amount of places  DE: Anzahl Plaetze
        /// </summary>
        public int PlaceAmount{ get; set; }
        /// <summary>
        ///     EN: Organizational Unit  DE: TSC
        /// </summary>
        public int OrgOrganizationalUnitId{ get; set; }
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
        ///     DE: Name 1 EN: Name 1
        /// </summary>
        public string Name1{ get; set; }
        /// <summary>
        ///     DE: Name 2 EN: Name 2
        /// </summary>
        public string Name2{ get; set; }
        /// <summary>
        ///     DE: Name 3 EN: Name 3
        /// </summary>
        public string Name3{ get; set; }
        /// <summary>
        ///     DE: Strasse und Hausnummer EN: Street and house number
        /// </summary>
        public string StreetHouseNumber{ get; set; }
        /// <summary>
        ///     DE: PLZ EN: Zip code
        /// </summary>
        public string ZipCode{ get; set; }
        /// <summary>
        ///     DE: PLZ EN: Zip code of postal box
        /// </summary>
        public string ZipBox{ get; set; }
        /// <summary>
        ///     DE: Postfach EN: Postal box
        /// </summary>
        public string Box{ get; set; }
        /// <summary>
        ///     DE: Ort EN: City
        /// </summary>
        public string City{ get; set; }
        /// <summary>
        ///     DE: Telefonnummer 1 EN: Phone number 1
        /// </summary>
        public string Phone1{ get; set; }
        /// <summary>
        ///     DE: Fax EN: Fax
        /// </summary>
        public string Fax{ get; set; }
        /// <summary>
        ///     DE: E-Mail EN: Email
        /// </summary>
        public string Email{ get; set; }
        /// <summary>
        ///     EN: Country
        /// </summary>
        public int SysCountryId{ get; set; }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public ExamRoom ShallowCopy()
        {
            return new ExamRoom {
                       RoomNumber = RoomNumber,
                       PlaceAmount = PlaceAmount,
                       OrgOrganizationalUnitId = OrgOrganizationalUnitId,
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
                       Name1 = Name1,
                       Name2 = Name2,
                       Name3 = Name3,
                       StreetHouseNumber = StreetHouseNumber,
                       ZipCode = ZipCode,
                       ZipBox = ZipBox,
                       Box = Box,
                       City = City,
                       Phone1 = Phone1,
                       Fax = Fax,
                       Email = Email,
                       SysCountryId = SysCountryId,
        	           };
        }
    }
}
