define([
	'base/base-object-add-view',
    'l!t!Invoices/AddInvoicePositions'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'Invoices',
        actionUrl: '#Invoices',

		bindings: function () {

            var self = this;
            var result = {
                '#orderId': 'orderId',
			    '#invoiceNumber': 'invoiceNumber',
			    '#payDate': 'payDate',
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

        events: {
		}
    });

    return view;
});
