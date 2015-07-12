define([
	'base/base-object-filter-view',
	'models/TransportOrders/FilterTransportOrders'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter,

        getFilters: function () {

            var result = [],
                fromDate = this.model.get('fromDate'),
                toDate = this.model.get('toDate'),
                name = this.model.get('name');

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
