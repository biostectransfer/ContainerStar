using ContainerStar.Contracts;
using System;

namespace ContainerStar.Contracts.Entities
{
    public partial class ContainerTypeEquipmentRsp: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.ContainerType_Equipment_Rsp";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="ContainerTypeEquipmentRsp.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'ContainerTypeId' for property <see cref="ContainerTypeEquipmentRsp.ContainerTypeId"/>
            /// </summary>
            public static readonly string ContainerTypeId = "ContainerTypeId";
            /// <summary>
            /// Column name 'EquipmentId' for property <see cref="ContainerTypeEquipmentRsp.EquipmentId"/>
            /// </summary>
            public static readonly string EquipmentId = "EquipmentId";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="ContainerTypeEquipmentRsp.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="ContainerTypeEquipmentRsp.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="ContainerTypeEquipmentRsp.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'Amount' for property <see cref="ContainerTypeEquipmentRsp.Amount"/>
            /// </summary>
            public static readonly string Amount = "Amount";
          
        }
        #endregion
        public int Id{ get; set; }
        public int ContainerTypeId{ get; set; }
        public int EquipmentId{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public int Amount{ get; set; }
        public virtual ContainerTypes ContainerTypes{ get; set; }
        public virtual Equipments Equipments{ get; set; }
        public bool HasContainerTypes
        {
            get { return !ReferenceEquals(ContainerTypes, null); }
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
        public ContainerTypeEquipmentRsp ShallowCopy()
        {
            return new ContainerTypeEquipmentRsp {
                       ContainerTypeId = ContainerTypeId,
                       EquipmentId = EquipmentId,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       Amount = Amount,
        	           };
        }
    }
}
