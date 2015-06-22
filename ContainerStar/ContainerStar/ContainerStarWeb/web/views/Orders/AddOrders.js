define([
	'base/base-object-add-view',
    'l!t!Orders/OrdersRelationships',
    'l!t!Orders/SelectCustomer'
], function (BaseView, TabView, SelectCustomerView) {
    'use strict';

    var save = function () {

        var self = this,
            deferred = new $.Deferred(),
            isNew = self.model.isNew(),
			attributesToSave = _.pick(
				self.model.toJSON(),
                'id',
                'changeDate',
                'deliveryPlace',        
                'zip',
                'city',
                'street',
                'comment',
                'orderDate',
                'orderedFrom',
                'orderNumber',
                'rentOrderNumber',
                'rentFromDate',
                'rentToDate',
                'autoBill',
                'discount',
                'billTillDate',
                'customerNumber',
                'newCustomerName',
                'customerStreet',
                'customerCity',
                'customerZip',
                'customerPhone',
                'customerFax',
                'customerEmail',
                'customerSelectType',
                'customerId',
                'communicationPartnerId',
                'customerSelectType',
                'isOffer');
        
        attributesToSave["customerNumber"] = $('#customerNumber').val();
        attributesToSave["newCustomerName"] = $('#newCustomerName').val();
        attributesToSave["customerStreet"] = $('#customerStreet').val();
        attributesToSave["customerCity"] = $('#customerCity').val();
        attributesToSave["customerZip"] = $('#customerZip').val();

        if (self.validate()) {
            self.model.save({}, {
                data: kendo.stringify(attributesToSave),
                method: isNew ? 'post' : 'put',
                contentType: 'application/json',
                success: function (response) {

                    location.href = "#Orders/" + response.id;
                },
                error: function (model, response) {
                    self.validateResponse(response);
                }
            });
        }

        return deferred.promise();
    },

    validate = function(attributes)
    {

    },

    view = BaseView.extend({

        tabView: TabView,
        tableName: 'Orders',
        actionUrl: '#Orders',

        bindings: function () {

            var self = this;
            var result = {
                '#customerSelectType': 'customerSelectType',
                '#customerId': 'customerId',
                '#communicationPartnerId': {
                    observe: 'communicationPartnerId',
                    selectOptions: {
                        labelPath: 'name',
                        valuePath: 'id',
                        collection: function () {

                            return this.options.communicationPartners.where({ customerId: this.model.get('customerId') }).
                                    map(function (item) {
                                        return item.toJSON();
                                    });
                        },
                        defaultOption: {
                            label: self.resources.pleaseSelect,
                            value: null
                        }
                    },
                },
                '#customerId_Name': 'customerName',
                '#deliveryPlace': 'deliveryPlace',
                '#street': 'street',
                '#zip': 'zip',
                '#city': 'city',
                '#comment': 'comment',
                '#orderDate': 'orderDate',
                '#orderedFrom': 'orderedFrom',
                '#orderNumber': 'orderNumber',
                '#rentOrderNumber': 'rentOrderNumber',
                '#rentFromDate': 'rentFromDate',
                '#rentToDate': 'rentToDate',
                '#autoBill': 'autoBill',
                '#discount': 'discount',
                '#billTillDate': 'billTillDate',
                //'#customerNumber': 'customerNumber',
                //'#newCustomerName': 'newCustomerName',
                //'#customerStreet': 'customerStreet',
                //'#customerCity': 'customerCity',
                //'#customerZip': 'customerZip',
                '#customerPhone': 'customerPhone',
                '#customerFax': 'customerFax',
                '#customerEmail': 'customerEmail',
                '.radioButtons': {
                    observe: 'id',
                    visible: function (val) {
                        return val == undefined || val == "";
                    }
                },
                '#isOffer': 'isOffer',
            };
            
            return result;
        },

        render: function () {

            view.__super__.render.apply(this, arguments);

            this.stickit();

            var self = this;

            self.listenTo(self.model, 'change:customerId', function () {

                self.model.unset('communicationPartnerId', { silent: true });
                self.model.set('communicationPartnerId', 0);
            });

            self.listenTo(self.model, 'change:customerSelectType', function (temp) {
                var customerSelectType = self.model.get('customerSelectType');
                if(customerSelectType === "1")
                {
                    $('#selectCustomerRow').show();
                    $('#customerNumberRow').hide();
                    $('#customerAddressRow').hide();
                    $('#customerPhonesRow').hide();
                    $('#newCustomerName').val('0');
                    $('#customerNumber').val('0');
                    $('#customerStreet').val('0');
                    $('#customerCity').val('0');
                    $('#customerZip').val('0');
                    $('#customerId').val('');
                }
                else if(customerSelectType === "2")
                {
                    $('#selectCustomerRow').hide();
                    $('#customerNumberRow').show();
                    $('#customerAddressRow').show();
                    $('#customerPhonesRow').show();
                    $('#newCustomerName').val('');
                    $('#customerNumber').val('');
                    $('#customerStreet').val('');
                    $('#customerCity').val('');
                    $('#customerZip').val('');
                    $('#customerId').val('0');
                }
            });
            
            return this;
        },

        save: function () {
            
        },

        events: {
            'click .SelectCustomer': function (e) {
                e.preventDefault();

                var self = this,
                    view = new SelectCustomerView();

                self.listenTo(view, 'select', function (item) {

                    self.model.set('customerId', item.id);
                    self.$el.find('#customerId').val(item.id);
                    self.$el.find('#customerId_Name').val(item.get('name'));
                });

                self.addView(view);
                self.$el.append(view.render().$el);
            },
            'click .save': function (e) {
                e.preventDefault();

                var self = this;
                save.call(self).done(function () {

                });
            },
        }
    });

    return view;
});
