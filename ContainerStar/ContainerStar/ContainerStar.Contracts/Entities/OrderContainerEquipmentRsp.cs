using ContainerStar.Contracts;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class OrderContainerEquipmentRsp: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.OrderContainer_Equipment_Rsp";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="OrderContainerEquipmentRsp.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'OrderId' for property <see cref="OrderContainerEquipmentRsp.OrderId"/>
            /// </summary>
            public static readonly string OrderId = "OrderId";
            /// <summary>
            /// Column name 'ContainerId' for property <see cref="OrderContainerEquipmentRsp.ContainerId"/>
            /// </summary>
            public static readonly string ContainerId = "ContainerId";
            /// <summary>
            /// Column name 'EquipmentId' for property <see cref="OrderContainerEquipmentRsp.EquipmentId"/>
            /// </summary>
            public static readonly string EquipmentId = "EquipmentId";
            /// <summary>
            /// Column name 'Amount' for property <see cref="OrderContainerEquipmentRsp.Amount"/>
            /// </summary>
            public static readonly string Amount = "Amount";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="OrderContainerEquipmentRsp.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="OrderContainerEquipmentRsp.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="OrderContainerEquipmentRsp.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public int OrderId{ get; set; }
        public int ContainerId{ get; set; }
        public int EquipmentId{ get; set; }
        public int Amount{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual Orders Orders{ get; set; }
        public virtual Containers Containers{ get; set; }
        public virtual Equipments Equipments{ get; set; }
        public bool HasOrders
        {
            get { return !ReferenceEquals(Orders, null); }
        }
        public bool HasContainers
        {
            get { return !ReferenceEquals(Containers, null); }
        }
        public bool HasEquipments
        {
            get { return !ReferenceEquals(Equipments, null); }
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
        public OrderContainerEquipmentRsp ShallowCopy()
        {
            return new OrderContainerEquipmentRsp {
                       OrderId = OrderId,
                       ContainerId = ContainerId,
                       EquipmentId = EquipmentId,
                       Amount = Amount,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
