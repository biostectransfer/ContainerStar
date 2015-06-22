using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using ContainerStar.Contracts.Managers;

namespace ContainerStar.API.Security
{
    public class AuthorizeByPermissionsAttribute : AuthorizeAttribute
    {
        #region	Private fields
        private readonly IUserManager _userManager;
        private IRolePermissionRspManager rolePermissionRspManager;
        private int[] _permissionTypes;
        #endregion
        #region Constructor
        public AuthorizeByPermissionsAttribute()
        {
            //TODO Should be refactored
            var resolver = GlobalConfiguration.Configuration.DependencyResolver;
            _userManager = (IUserManager)resolver.GetService(typeof(IUserManager));
            rolePermissionRspManager = (IRolePermissionRspManager)resolver.GetService(typeof(IRolePermissionRspManager));
            PermissionTypes = new int[0];
        }
        #endregion
        public int[] PermissionTypes
        {
            get { return _permissionTypes; }
            set { _permissionTypes = value ?? new int[0]; }
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal == null &&
                actionContext.RequestContext.Principal.Identity == null &&
                !actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                return false;
            }

            var userName = actionContext.RequestContext.Principal.Identity.Name;

            var user = _userManager.GetByLogin(userName);

            if (user == null || user.Role == null)
            {
                return false;
            }

            var userPermissionIds = rolePermissionRspManager.GetEntities().Where(e =>
                !e.DeleteDate.HasValue 
                && user.RoleId == e.RoleId 
                && PermissionTypes.Contains(e.PermissionId));

            return userPermissionIds.Any();
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal != null &&
                actionContext.RequestContext.Principal.Identity != null &&
                actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}