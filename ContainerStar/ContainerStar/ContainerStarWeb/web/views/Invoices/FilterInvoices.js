define([
	'base/base-object-filter-view',
	'models/Invoices/FilterInvoices'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter
    });

    return view;
});
