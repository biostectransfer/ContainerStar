using ContainerStar.API.Models;
using ContainerStar.API.Models.Settings;
using ContainerStar.API.Security;
using ContainerStar.Contracts;
using ContainerStar.Contracts.Entities;
using ContainerStar.Contracts.Enums;
using ContainerStar.Contracts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace ContainerStar.API.Controllers
{
    [AuthorizeByPermissions(PermissionTypes = new[] { Permissions.Orders })]
    /// <summary>
    ///     Controller for <see cref="Orders"/> entity
    /// </summary>
    public partial class OrdersController: ClientApiController<OrdersModel, Orders, int, IOrdersManager>
    {
        private ICustomersManager customerManager { get; set; }
        public OrdersController(IOrdersManager manager, ICustomersManager customersManager): base(manager)
        {
            this.customerManager = customersManager;
        }

        protected override void EntityToModel(Orders entity, OrdersModel model)
        {
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
            model.rentOrderNumber = entity.RentOrderNumber;
            model.rentFromDate = entity.RentFromDate;
            model.rentToDate = entity.RentToDate;
            model.autoBill = entity.AutoBill;
            model.discount = entity.Discount;
            model.billTillDate = entity.BillTillDate;
            model.createDate = ((ISystemFields)entity).CreateDate;
            model.changeDate = ((ISystemFields)entity).ChangeDate;
            model.isOffer = entity.IsOffer;

            ExtraEntityToModel(entity, model);
        }
        protected override void ModelToEntity(OrdersModel model, Orders entity, ActionTypes actionType)
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
            entity.RentOrderNumber = model.rentOrderNumber;
            entity.RentFromDate = model.rentFromDate;
            entity.RentToDate = model.rentToDate;
            entity.AutoBill = model.autoBill;
            entity.Discount = model.discount;
            entity.BillTillDate = model.billTillDate;
            entity.IsOffer = model.isOffer;
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

        protected override IQueryable<Orders> Sort(IQueryable<Orders> entities, Sorting sorting)
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

        protected void ExtraEntityToModel(Orders entity, OrdersModel model)
        {
            model.customerName = entity.CustomerName;
            model.communicationPartnerTitle = entity.CommunicationPartnerTitle;
            model.customerSelectType = 1;//existing customer
        }

        protected override void Validate(OrdersModel model, Orders entity, ActionTypes actionType)
        {
            //base.Validate(model, entity, actionType);
        }
    }
}
