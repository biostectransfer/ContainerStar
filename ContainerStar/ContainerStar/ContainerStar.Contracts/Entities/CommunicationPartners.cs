using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class CommunicationPartners: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.CommunicationPartners";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="CommunicationPartners.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Name' for property <see cref="CommunicationPartners.Name"/>
            /// </summary>
            public static readonly string Name = "Name";
            /// <summary>
            /// Column name 'FirstName' for property <see cref="CommunicationPartners.FirstName"/>
            /// </summary>
            public static readonly string FirstName = "FirstName";
            /// <summary>
            /// Column name 'CustomerId' for property <see cref="CommunicationPartners.CustomerId"/>
            /// </summary>
            public static readonly string CustomerId = "CustomerId";
            /// <summary>
            /// Column name 'Phone' for property <see cref="CommunicationPartners.Phone"/>
            /// </summary>
            public static readonly string Phone = "Phone";
            /// <summary>
            /// Column name 'Mobile' for property <see cref="CommunicationPartners.Mobile"/>
            /// </summary>
            public static readonly string Mobile = "Mobile";
            /// <summary>
            /// Column name 'Fax' for property <see cref="CommunicationPartners.Fax"/>
            /// </summary>
            public static readonly string Fax = "Fax";
            /// <summary>
            /// Column name 'Email' for property <see cref="CommunicationPartners.Email"/>
            /// </summary>
            public static readonly string Email = "Email";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="CommunicationPartners.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="CommunicationPartners.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="CommunicationPartners.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string FirstName{ get; set; }
        public int CustomerId{ get; set; }
        public string Phone{ get; set; }
        public string Mobile{ get; set; }
        public string Fax{ get; set; }
        public string Email{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual ICollection<Orders> Orders{ get; set; }
        public virtual Customers Customers{ get; set; }
        public bool HasCustomers
        {
            get { return !ReferenceEquals(Customers, null); }
        }
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
        public CommunicationPartners ShallowCopy()
        {
            return new CommunicationPartners {
                       Name = Name,
                       FirstName = FirstName,
                       CustomerId = CustomerId,
                       Phone = Phone,
                       Mobile = Mobile,
                       Fax = Fax,
                       Email = Email,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
