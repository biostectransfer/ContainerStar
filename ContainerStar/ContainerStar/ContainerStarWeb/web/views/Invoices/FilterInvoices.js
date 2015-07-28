define([
	'base/base-object-filter-view',
	'models/Invoices/FilterInvoices'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter,

        getFilters: function () {

            var result = [],
                fromDate = this.model.get('fromDate'),
                toDate = this.model.get('toDate'),
                isPayed = this.model.get('isPayed'),
                autoDebitEntry = this.model.get('autoDebitEntry'),
                name = this.model.get('name'),
                payStatus = 1,
                autoDebitEntryStatus = 1;

            result.push({
                field: 'createDate',
                operator: 'gte',
                value: fromDate
            });

            result.push({
                field: 'createDate',
                operator: 'lte',
                value: toDate
            });

            if (isPayed)
            {
                payStatus = 2;
            }
            
            result.push({
                field: 'isPayed',
                operator: 'eq',
                value: payStatus
            });

            if (autoDebitEntry) {
                autoDebitEntryStatus = 2;
            }

            result.push({
                field: 'autoDebitEntry',
                operator: 'eq',
                value: autoDebitEntryStatus
            });

            result.push({
                field: 'name',
                operator: 'contains',
                value: this.model.get('name')
            });

            return result;
        },

        bindings: function () {

            var self = this;

            var result = {

                '#fromDate': 'fromDate',
                '#toDate': 'toDate',
                '#name': 'name',
                '#isPayed': 'isPayed',
                '#autoDebitEntry': 'autoDebitEntry',
            };

            return result;
        },

        render: function () {

            var self = this;

            view.__super__.renderWithoutBindings.apply(self, arguments);

            return self;
        }
    });

    return view;
});
