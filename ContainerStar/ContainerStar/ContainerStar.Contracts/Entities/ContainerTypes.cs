using ContainerStar.Contracts;
using CoreBase.Entities;
using System;
using System.Collections.Generic;

namespace ContainerStar.Contracts.Entities
{
    public partial class ContainerTypes: IHasId<int>
        ,IHasTitle<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.ContainerTypes";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="ContainerTypes.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'Name' for property <see cref="ContainerTypes.Name"/>
            /// </summary>
            public static readonly string Name = "Name";
            /// <summary>
            /// Column name 'Comment' for property <see cref="ContainerTypes.Comment"/>
            /// </summary>
            public static readonly string Comment = "Comment";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="ContainerTypes.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="ContainerTypes.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="ContainerTypes.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
            /// <summary>
            /// Column name 'DispositionRelevant' for property <see cref="ContainerTypes.DispositionRelevant"/>
            /// </summary>
            public static readonly string DispositionRelevant = "DispositionRelevant";
          
        }
        #endregion
        public int Id{ get; set; }
        public string Name{ get; set; }
        public string Comment{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public bool DispositionRelevant{ get; set; }
        public virtual ICollection<ContainerTypeEquipmentRsp> ContainerTypeEquipmentRsps{ get; set; }
        public virtual ICollection<Containers> Containers{ get; set; }
        string IHasTitle<int>.EntityTitle
        {
            get { return Name; }
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
        public ContainerTypes ShallowCopy()
        {
            return new ContainerTypes {
                       Name = Name,
                       Comment = Comment,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
                       DispositionRelevant = DispositionRelevant,
        	           };
        }
    }
}
