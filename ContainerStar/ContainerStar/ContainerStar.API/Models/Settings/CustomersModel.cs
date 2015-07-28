using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Settings
{
    /// <summary>
    ///     Model for <see cref="Customers"/> entity
    /// </summary>
    [DataContract]
    public partial class CustomersModel: BaseModel
    {

        /// <summary>
        ///     Model property for <see cref="Customers.Number"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public int number{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Name"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string name{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Street"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string street{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Zip"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string zip{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.City"/> entity
        /// </summary>
        [Required]
        [DataMember]
        public string city{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Country"/> entity
        /// </summary>
        [DataMember]
        public string country{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Phone"/> entity
        /// </summary>
        [DataMember]
        public string phone{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Mobile"/> entity
        /// </summary>
        [DataMember]
        public string mobile{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Fax"/> entity
        /// </summary>
        [DataMember]
        public string fax{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Email"/> entity
        /// </summary>
        [DataMember]
        public string email{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Comment"/> entity
        /// </summary>
        [DataMember]
        public string comment{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Iban"/> entity
        /// </summary>
        [DataMember]
        public string iban{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Bic"/> entity
        /// </summary>
        [DataMember]
        public string bic{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.WithTaxes"/> entity
        /// </summary>
        [DataMember]
        public bool withTaxes{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.AutoDebitEntry"/> entity
        /// </summary>
        [DataMember]
        public bool autoDebitEntry{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.AutoBill"/> entity
        /// </summary>
        [DataMember]
        public bool autoBill{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Discount"/> entity
        /// </summary>
        [DataMember]
        public double? discount{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.UstId"/> entity
        /// </summary>
        [DataMember]
        public string ustId{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Bank"/> entity
        /// </summary>
        [DataMember]
        public string bank{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.AccountNumber"/> entity
        /// </summary>
        [DataMember]
        public string accountNumber{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.Blz"/> entity
        /// </summary>
        [DataMember]
        public string blz{ get; set; }
        /// <summary>
        ///     Model property for <see cref="Customers.IsProspectiveCustomer"/> entity
        /// </summary>
        [DataMember]
        public bool isProspectiveCustomer{ get; set; }

    }
}
