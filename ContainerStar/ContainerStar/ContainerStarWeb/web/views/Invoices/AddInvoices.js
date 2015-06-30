define([
	'base/base-object-add-view',
    'l!t!Invoices/InvoicesRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'Invoices',
        actionUrl: '#Invoices',

		bindings: function () {

            var self = this;
            var result = {
			'#invoiceNumber': 'invoiceNumber',
			'#payDate': 'payDate',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field

            return this;
        }
		,events: {
		}
    });

    return view;
});
