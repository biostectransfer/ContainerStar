using System.Web.Http;
using System.Web.Security;
using ContainerStar.API.Models;

namespace ContainerStar.API.Controllers
{
	public class LogoutController : ApiController
	{
		[Authorize]
		public IHttpActionResult Post()
		{
			FormsAuthentication.SignOut();

			return Ok(new LoggedUserModel());
		}
	}
}