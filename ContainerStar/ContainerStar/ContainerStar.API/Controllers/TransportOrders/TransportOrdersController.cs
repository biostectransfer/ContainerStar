using System;
using ContainerStar.API.Models;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using ContainerStar.Contracts.Services;
using System.Collections.Generic;
using System.Linq;
using CoreBase;

namespace ContainerStar.API.Controllers
{
    /// <summary>
    ///     Controller for <see cref="TransportOrders"/> entity
    /// </summary>
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.TransportOrders })]    
    public partial class TransportOrdersController: ClientApiController<TransportOrdersModel, TransportOrders, int, ITransportOrdersManager>
    {
        private readonly ICustomersManager customerManager;
        private readonly IUniqueNumberProvider numberProvider;

        public TransportOrdersController(
            ITransportOrdersManager manager,
            ICustomersManager customersManager,
            IUniqueNumberProvider numberProvider)
            : base(manager)
        {
            this.customerManager = customersManager;
            this.numberProvider = numberProvider;

            ActionSuccess += ClientBaseController_ActionSuccess;
        }

        private void ClientBaseController_ActionSuccess(object sender, ActionSuccessEventArgs<TransportOrders, int> e)
        {
            if (e.ActionType != ActionTypes.Delete)
            {
                var order = e.Entity;

                if (!order.IsOffer)
                {
                    if (String.IsNullOrEmpty(order.OrderNumber))
                    {
                        order.OrderNumber = numberProvider.GetNextTransportOrderNumber();
                    }

                    Manager.SaveChanges();
                }
            }
        }

        protected override void EntityToModel(TransportOrders entity, TransportOrdersModel model)
        {
            model.Id = entity.Id;
            model.customerId = entity.CustomerId;
            model.communicationPartnerId = entity.CommunicationPartnerId;
            model.deliveryPlace = entity.DeliveryPlace;
            model.street = entity.Street;
            model.zip = entity.Zip;
            model.city = entity.City;
            model.comment = entity.Comment;
            model.orderDate = entity.OrderDate;
            model.orderedFrom = entity.OrderedFrom;
            model.orderNumber = entity.OrderNumber;
            model.discount = entity.Discount;
            model.billTillDate = entity.BillTillDate;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
            model.isOffer = entity.IsOffer;
            model.customerOrderNumber = entity.CustomerOrderNumber;
            model.status = entity.Status;

            ExtraEntityToModel(entity, model);
        }
        protected override void ModelToEntity(TransportOrdersModel model, TransportOrders entity, ActionTypes actionType)
        {
            if (actionType == ActionTypes.Add && model.customerSelectType == 2)
            {
                var customer = new Customers()
                {
                    Number = model.customerNumber,
                    Name = model.newCustomerName,
                    Street = model.customerStreet,
                    City = model.customerCity,
                    Zip = model.customerZip,
                    Phone = model.customerPhone,
                    Fax = model.customerFax,
                    Email = model.customerEmail
                };
                customerManager.AddEntity(customer);
                customerManager.SaveChanges();
                entity.CustomerId = customer.Id;
            }
            else
            {
                entity.CustomerId = model.customerId;
                entity.CommunicationPartnerId = model.communicationPartnerId > 0 ? model.communicationPartnerId : (int?)null;
            }

            entity.DeliveryPlace = model.deliveryPlace;
            entity.Street = model.street;
            entity.Zip = model.zip;
            entity.City = model.city;
            entity.Comment = model.comment;
            entity.OrderDate = model.orderDate;
            entity.OrderedFrom = model.orderedFrom;
            entity.OrderNumber = model.orderNumber;
            entity.Discount = model.discount;
            entity.BillTillDate = model.billTillDate;
            entity.IsOffer = model.isOffer;
            entity.CustomerOrderNumber = model.customerOrderNumber;
            entity.Status = model.status;

            if (entity.IsNew())
            {
                entity.CreateDate = DateTime.Now;
                entity.Status = (int)OrderStatusTypes.Open;
            }
        }

        protected override string BuildWhereClause<T>(Filter filter)
        {
            if (filter.Field == "name")
            {
                var clauses = new List<string>();

                clauses.AddRange(new[] { 
        				base.BuildWhereClause<T>(new Filter { Field = "Customers.Name", Operator = filter.Operator, Value = filter.Value }),
        			});

                return string.Join(" or ", clauses);
            }

            return base.BuildWhereClause<T>(filter);
        }

        protected override IQueryable<TransportOrders> Sort(IQueryable<TransportOrders> entities, Sorting sorting)
        {
            if (sorting.Field == "customerName")
            {
                if (sorting.Direction == "desc")
                    return entities.OrderByDescending(o => o.Customers.Name);
                else
                    return entities.OrderBy(o => o.Customers.Name);
            }
            else if (sorting.Field == "communicationPartnerTitle")
            {
                if (sorting.Direction == "desc")
                    return entities.OrderByDescending(o => o.CommunicationPartners.Name).
                        ThenByDescending(o => o.CommunicationPartners.FirstName);
                else
                    return entities.OrderBy(o => o.CommunicationPartners.Name).
                        ThenBy(o => o.CommunicationPartners.FirstName);
            }

            return base.Sort(entities, sorting);
        }

        protected void ExtraEntityToModel(TransportOrders entity, TransportOrdersModel model)
        {
            model.customerName = entity.CustomerName;
            model.communicationPartnerTitle = entity.CommunicationPartnerTitle;
            model.customerSelectType = 1;//existing customer
        }

        protected override void Validate(TransportOrdersModel model, TransportOrders entity, ActionTypes actionType)
        {
            //base.Validate(model, entity, actionType);
        }
    }
}
