using ContainerStar.API.Models.Settings;
using ContainerStar.Contracts.Entities;
using System.Web.Http;
using CoreBase;
using CoreBase.Models;
using CoreBase.Controllers;

namespace ContainerStar.API.Controllers.Settings
{
    public partial class UsersController
    {
        public IHttpActionResult Patch(PasswordModel model)
        {
            var entity = Manager.GetById(model.Id);
            if (entity == null)
                return NotFound();

            ValidatePassword(model);

            if (ModelState.IsValid)
            {
                entity.Password = StringHelper.GetMD5Hash(model.password);

                Manager.SaveChanges();

                return Ok(model);
            }
            else
                return BadRequest(ModelState);
        }

        protected override void Validate(UserModel model, User entity, ActionTypes actionType)
        {
            if (!string.Equals(model.login, entity.Login, System.StringComparison.InvariantCultureIgnoreCase))
            {
                if (Manager.GetByLogin(model.login) != null)
                    ModelState.AddModelError("model.login", "login-unique");
            }

            if (actionType == ActionTypes.Add)
            {
                if (string.IsNullOrEmpty(model.password))
                    ModelState.AddModelError("model.password", "required");
            }
        }
        
        private void ValidatePassword(IPasswordModel model)
        {
            if (string.IsNullOrEmpty(model.password))
                ModelState.AddModelError("model.password", "required");

            if (string.IsNullOrEmpty(model.passwordConfirmation))
                ModelState.AddModelError("model.passwordConfirmation", "required");

            if (model.password != model.passwordConfirmation)
            {
                ModelState.AddModelError("model.password", "required");
                ModelState.AddModelError("model.passwordConfirmation", "Passwort und Passwortwiederholung müssen übereinstimmen!");
            }
        }

        protected void ExtraModelToEntity(User entity, UserModel model, ActionTypes actionType)
        {
            if (actionType == ActionTypes.Add)
            {
                entity.Password = StringHelper.GetMD5Hash(model.password);
            }
        }
    }
}
