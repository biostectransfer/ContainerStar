using ContainerStar.Contracts;
using System;

namespace ContainerStar.Contracts.Entities
{
    /// <summary>
    ///     DE: Berechtigung  EN: Permissiom
    /// </summary>
    public partial class RolePermissionRsp: IHasId<int>
        ,IRemovable
        ,ISystemFields
    {
        /// <summary>
        /// Table name
        /// </summary>
        public static readonly string EntityTableName = "dbo.Role_Permission_Rsp";
        #region Fields
        /// <summary>
        /// Columns names
        /// </summary>
        public static class Fields
        {
            /// <summary>
            /// Column name 'Id' for property <see cref="RolePermissionRsp.Id"/>
            /// </summary>
            public static readonly string Id = "Id";
            /// <summary>
            /// Column name 'RoleId' for property <see cref="RolePermissionRsp.RoleId"/>
            /// </summary>
            public static readonly string RoleId = "RoleId";
            /// <summary>
            /// Column name 'PermissionId' for property <see cref="RolePermissionRsp.PermissionId"/>
            /// </summary>
            public static readonly string PermissionId = "PermissionId";
            /// <summary>
            /// Column name 'CreateDate' for property <see cref="RolePermissionRsp.CreateDate"/>
            /// </summary>
            public static readonly string CreateDate = "CreateDate";
            /// <summary>
            /// Column name 'ChangeDate' for property <see cref="RolePermissionRsp.ChangeDate"/>
            /// </summary>
            public static readonly string ChangeDate = "ChangeDate";
            /// <summary>
            /// Column name 'DeleteDate' for property <see cref="RolePermissionRsp.DeleteDate"/>
            /// </summary>
            public static readonly string DeleteDate = "DeleteDate";
          
        }
        #endregion
        public int Id{ get; set; }
        /// <summary>
        ///     DE: Rolle  EN: Role
        /// </summary>
        public int RoleId{ get; set; }
        /// <summary>
        ///     DE: Berechtigung  EN: Permission
        /// </summary>
        public int PermissionId{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime ChangeDate{ get; set; }
        public DateTime? DeleteDate{ get; set; }
        public virtual Permission Permission{ get; set; }
        public virtual Role Role{ get; set; }
        public bool HasPermission
        {
            get { return !ReferenceEquals(Permission, null); }
        }
        public bool HasRole
        {
            get { return !ReferenceEquals(Role, null); }
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
        public RolePermissionRsp ShallowCopy()
        {
            return new RolePermissionRsp {
                       RoleId = RoleId,
                       PermissionId = PermissionId,
                       CreateDate = CreateDate,
                       ChangeDate = ChangeDate,
                       DeleteDate = DeleteDate,
        	           };
        }
    }
}
