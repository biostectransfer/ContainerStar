define([
	'base/base-object-add-view',
    'l!t!Invoices/AddInvoicePositions'
], function (BaseView , TabView) {
    'use strict';

    var save = function () {

        var self = this,
            deferred = new $.Deferred();
            

        if (self.validate()) {
            self.model.save({}, {
                data: kendo.stringify(self.model),
                method: 'put',
                contentType: 'application/json',
                success: function (response) {
                    
                    return deferred.resolve();
                },
                error: function (model, response) {
                    self.validateResponse(response);
                }
            });
        }

        return deferred.promise();
    },

    view = BaseView.extend({

        tabView: TabView,
        tableName: 'Invoices',
        actionUrl: '#Invoices',

		bindings: function () {

            var self = this;
            var result = {
                '#orderId': 'orderId',
                '#rentOrderNumber': 'rentOrderNumber',
                '#customerName': 'customerName',
                '#customerAddress': 'customerAddress',
                '#invoiceNumber': 'invoiceNumber',
                '#createDate': 'createDate',
                '#withTaxes': 'withTaxes',
                '#discount': 'discount',
                '#taxValue': 'taxValue',
                '#manualPrice': 'manualPrice',
                '#totalPrice': 'totalPrice',
                '#summaryPrice': 'summaryPrice',
                '#totalPriceWithoutTax': 'totalPriceWithoutTax',
                '#totalPriceWithoutDiscountWithoutTax': 'totalPriceWithoutDiscountWithoutTax',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

            var self = this,
                            model = new Backbone.Model({}),
                            options = _.extend({}, {
                                model: model,
                            }),
                        options = _.extend(options, self.options),
                        detView = new self.tabView(options);

            self.showView(detView, '.invoicePositionsGrid');

            return this;
        },
        
        save: function(){

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

                    self.$el.find('#totalPrice').data("kendoNumericTextBox").value(self.model.get('totalPrice'));
                    self.$el.find('#summaryPrice').data("kendoNumericTextBox").value(self.model.get('summaryPrice'));
                    self.$el.find('#totalPriceWithoutTax').data("kendoNumericTextBox").value(self.model.get('totalPriceWithoutTax'));
                    self.$el.find('#totalPriceWithoutDiscountWithoutTax').data("kendoNumericTextBox").value(self.model.get('totalPriceWithoutDiscountWithoutTax'));
                });
            },
        }
    });

    return view;
});
