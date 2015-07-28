using ContainerStar.API.Validation;
using ContainerStar.Contracts.Entities;
using System;
using System.Runtime.Serialization;
// ReSharper disable InconsistentNaming

namespace ContainerStar.API.Models.Invoices
{
    /// <summary>
    ///     Model for <see cref="Invoices"/> entity
    /// </summary>
    [DataContract]
    public partial class InvoicesModel: BaseModel
    {
        [Required]
        [DataMember]
        public int orderId { get; set; }
        
        [DataMember]
        public string invoiceNumber{ get; set; }

        [DataMember]
        public DateTime? payDate{ get; set; }

        [DataMember]
        public string customerName { get; set; }

        [DataMember]
        public string customerAddress { get; set; }
        
        [DataMember]
        public string rentOrderNumber { get; set; }

        [DataMember]
        public string communicationPartnerName { get; set; }

        [DataMember]
        public bool withTaxes { get; set; }

        [DataMember]
        public double discount { get; set; }

        [DataMember]
        public double taxValue { get; set; }

        [DataMember]
        public double? manualPrice { get; set; }

        [DataMember]
        public double totalPriceWithoutDiscountWithoutTax { get; set; }

        [DataMember]
        public double totalPriceWithoutTax { get; set; }

        [DataMember]
        public double totalPrice { get; set; }
        
        [DataMember]
        public double summaryPrice { get; set; }
        
        [DataMember]
        public bool isPayed { get; set; }

        [Required]
        [DataMember]
        public int payInDays { get; set; }

        [DataMember]
        public int? payPartOne { get; set; }

        [DataMember]
        public int? payPartTwo { get; set; }

        [DataMember]
        public int? payPartTree { get; set; }

        [DataMember]
        public int reminderCount { get; set; }

        [DataMember]
        public DateTime? lastReminderDate { get; set; }
    }
}
