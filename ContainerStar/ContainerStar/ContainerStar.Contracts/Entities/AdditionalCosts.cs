using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class AdditionalCosts: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.AdditionalCosts";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="AdditionalCosts.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Name' for property <see cref="AdditionalCosts.Name"/>
            /// </summary>
            public static readonly string Name = "Name";
            /// <summary>
            /// Column name 'Description' for property <see cref="AdditionalCosts.Description"/>
            /// </summary>
            public static readonly string Description = "Description";
            /// <summary>
            /// Column name 'Price' for property <see cref="AdditionalCosts.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'Automatic' for property <see cref="AdditionalCosts.Automatic"/>
            /// </summary>
            public static readonly string Automatic = "Automatic";
            /// <summary>
            /// Column name 'IncludeInFirstBill' for property <see cref="AdditionalCosts.IncludeInFirstBill"/>
            /// </summary>
            public static readonly string IncludeInFirstBill = "IncludeInFirstBill";
            /// <summary>
            /// Column name 'ProceedsAccount' for property <see cref="AdditionalCosts.ProceedsAccount"/>
            /// </summary>
            public static readonly string ProceedsAccount = "ProceedsAccount";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="AdditionalCosts.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="AdditionalCosts.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="AdditionalCosts.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Description{ get; set; }
        public double Price{ get; set; }
        public bool Automatic{ get; set; }
        public bool IncludeInFirstBill{ get; set; }
        public int ProceedsAccount{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual ICollection<Positions> Positions{ get; set; }
        DateTime ISystemFields.CreateDate
        {
            get { return CreateDate; }
            set { CreateDate = value; }
        }
        DateTime ISystemFields.ChangeDate
        {
            get { return ChangeDate; }
            set { ChangeDate = value; }
        }
                
        
        /// <summary>
        /// Shallow copy of object. Exclude navigation properties and PK properties
        /// </summary>
        public AdditionalCosts ShallowCopy()
        {
            return new AdditionalCosts {
                       Name = Name,
                       Description = Description,
                       Price = Price,
                       Automatic = Automatic,
                       IncludeInFirstBill = IncludeInFirstBill,
                       ProceedsAccount = ProceedsAccount,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
