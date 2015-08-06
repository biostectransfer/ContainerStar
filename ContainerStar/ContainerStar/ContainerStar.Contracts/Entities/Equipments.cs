using ContainerStar.Contracts;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class Equipments: IHasId<int>
        ,IHasTitle<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Equipments";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="Equipments.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Description' for property <see cref="Equipments.Description"/>
            /// </summary>
            public static readonly string Description = "Description";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="Equipments.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="Equipments.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="Equipments.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        public string Description{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual ICollection<OrderContainerEquipmentRsp> OrderContainerEquipmentRsps{ get; set; }
        public virtual ICollection<ContainerTypeEquipmentRsp> ContainerTypeEquipmentRsps{ get; set; }
        public virtual ICollection<ContainerEquipmentRsp> ContainerEquipmentRsps{ get; set; }
        string IHasTitle<int>.EntityTitle
        {
            get { return Description; }
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
        public Equipments ShallowCopy()
        {
            return new Equipments {
                       Description = Description,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
