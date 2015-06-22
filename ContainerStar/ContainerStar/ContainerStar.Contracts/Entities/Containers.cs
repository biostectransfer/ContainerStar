using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class Containers: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Containers";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Containers.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Number' for property <see cref="Containers.Number"/>
            /// </summary>
            public static readonly string Number = "Number";
            /// <summary>
            /// Column name 'ContainerTypeId' for property <see cref="Containers.ContainerTypeId"/>
            /// </summary>
            public static readonly string ContainerTypeId = "ContainerTypeId";
            /// <summary>
            /// Column name 'Length' for property <see cref="Containers.Length"/>
            /// </summary>
            public static readonly string Length = "Length";
            /// <summary>
            /// Column name 'Width' for property <see cref="Containers.Width"/>
            /// </summary>
            public static readonly string Width = "Width";
            /// <summary>
            /// Column name 'Height' for property <see cref="Containers.Height"/>
            /// </summary>
            public static readonly string Height = "Height";
            /// <summary>
            /// Column name 'Color' for property <see cref="Containers.Color"/>
            /// </summary>
            public static readonly string Color = "Color";
            /// <summary>
            /// Column name 'Price' for property <see cref="Containers.Price"/>
            /// </summary>
            public static readonly string Price = "Price";
            /// <summary>
            /// Column name 'ProceedsAccount' for property <see cref="Containers.ProceedsAccount"/>
            /// </summary>
            public static readonly string ProceedsAccount = "ProceedsAccount";
            /// <summary>
            /// Column name 'IsVirtual' for property <see cref="Containers.IsVirtual"/>
            /// </summary>
            public static readonly string IsVirtual = "IsVirtual";
            /// <summary>
            /// Column name 'ManufactureDate' for property <see cref="Containers.ManufactureDate"/>
            /// </summary>
            public static readonly string ManufactureDate = "ManufactureDate";
            /// <summary>
            /// Column name 'BoughtFrom' for property <see cref="Containers.BoughtFrom"/>
            /// </summary>
            public static readonly string BoughtFrom = "BoughtFrom";
            /// <summary>
            /// Column name 'BoughtPrice' for property <see cref="Containers.BoughtPrice"/>
            /// </summary>
            public static readonly string BoughtPrice = "BoughtPrice";
            /// <summary>
            /// Column name 'Comment' for property <see cref="Containers.Comment"/>
            /// </summary>
            public static readonly string Comment = "Comment";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Containers.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Containers.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Containers.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'SellPrice' for property <see cref="Containers.SellPrice"/>
            /// </summary>
            public static readonly string SellPrice = "SellPrice";
          
        }
        #endregion
        public int Id{ get; set; }
        public string Number{ get; set; }
        public int ContainerTypeId{ get; set; }
        public int Length{ get; set; }
        public int Width{ get; set; }
        public int Height{ get; set; }
        public string Color{ get; set; }
        public double Price{ get; set; }
        public int ProceedsAccount{ get; set; }
        public bool IsVirtual{ get; set; }
        public DateTime? ManufactureDate{ get; set; }
        public string BoughtFrom{ get; set; }
        public double? BoughtPrice{ get; set; }
        public string Comment{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public double SellPrice{ get; set; }
        public virtual ICollection<Positions> Positions{ get; set; }
        public virtual ContainerTypes ContainerTypes{ get; set; }
        public virtual ICollection<ContainerEquipmentRsp> ContainerEquipmentRsps{ get; set; }
        public bool HasContainerTypes
        {
            get { return !ReferenceEquals(ContainerTypes, null); }
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
        public Containers ShallowCopy()
        {
            return new Containers {
                       Number = Number,
                       ContainerTypeId = ContainerTypeId,
                       Length = Length,
                       Width = Width,
                       Height = Height,
                       Color = Color,
                       Price = Price,
                       ProceedsAccount = ProceedsAccount,
                       IsVirtual = IsVirtual,
                       ManufactureDate = ManufactureDate,
                       BoughtFrom = BoughtFrom,
                       BoughtPrice = BoughtPrice,
                       Comment = Comment,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       SellPrice = SellPrice,
        	           };
        }
    }
}
