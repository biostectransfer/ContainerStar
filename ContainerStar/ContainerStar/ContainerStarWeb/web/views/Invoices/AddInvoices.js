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
                '#payInDays': {
                    observe: 'payInDays',
                    selectOptions: {
                        labelPath: 'name', valuePath: 'id',
                        collection: self.options.paymentIntervals,
                        //defaultOption: { label: self.resources.pleaseSelect, value: 10 }
                    },
                },
                '#payPartOne': 'payPartOne',
                '#payPartTwo': 'payPartTwo',
                '#payPartTree': 'payPartTree',
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
        
        save: function () {

            var self = this;

            require(['base/information-view'], function (View) {
                var view = new View({
                    title: 'Information speichern',
                    message: 'Information wurde erfolgreich gespeichert.'
                });
                self.addView(view);
                self.$el.append(view.render().$el);
            });
        },

        events: {
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
            'click .print': function (e) {
                e.preventDefault();
                var self = this;

                location.href = Application.apiUrl + 'print/?printTypeId=1&id=' + self.model.id;
            }
        }
    });

    return view;
});
