using ContainerStar.API.Models;
using ContainerStar.API.Models.Invoices;
using ContainerStar.API.Security;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using CoreBase;
using System;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using ContainerStar.API.Models.Settings;

namespace ContainerStar.API.Controllers
{

    /// <summary>
    ///     Controller for copy container
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Containers })]
    public partial class CopyContainerController : ApiController
    {
        private readonly IContainersManager manager;

        public CopyContainerController(IContainersManager manager)
        {
            this.manager = manager;
        }

        public IHttpActionResult Put(ContainersModel model)
        {
            var container = manager.GetById(model.Id);

            var newContainer = new Containers()
            {
                BoughtFrom = container.BoughtFrom,
                BoughtPrice = container.BoughtPrice,
                Color = container.Color,
                Comment = container.Comment,
                ContainerTypeId = container.ContainerTypeId,
                Height = container.Height,
                IsSold = false, //container.IsSold,
                IsVirtual = container.IsVirtual,
                Length = container.Length,
                ManufactureDate = container.ManufactureDate,
                MinPrice = container.MinPrice,
                NewContainerPrice = container.NewContainerPrice,
                Price = container.Price,
                ProceedsAccount = container.ProceedsAccount,
                SellPrice = container.SellPrice,
                Width = container.Width,
                Number = String.Empty,
                ContainerEquipmentRsps = new List<ContainerEquipmentRsp>(),
                CreateDate = DateTime.Now,
                ChangeDate = DateTime.Now,
            };
            
            manager.AddEntity(newContainer);

            foreach(var equipment in container.ContainerEquipmentRsps.Where(o => !o.DeleteDate.HasValue).ToList())
            {
                var newPosition = new ContainerEquipmentRsp()
                {
                    Amount = equipment.Amount,
                    EquipmentId = equipment.EquipmentId,
                    Containers = newContainer
                };

                //positionManager.AddEntity(newPosition);
                newContainer.ContainerEquipmentRsps.Add(newPosition);
            }

            manager.SaveChanges();

            return Ok(new { id = newContainer.Id });
        }
    }
}
