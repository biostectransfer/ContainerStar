define([
	'base/base-object-filter-view',
	'models/Settings/FilterTaxes'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter,

        getFilters: function () {
            
            var result = [],
                isProspectiveCustomer = this.model.get('isProspectiveCustomer'),
                name = this.model.get('name'),
                status = 1;

            if (isProspectiveCustomer) {
                status = 2
            }

            result.push({
                field: 'isProspectiveCustomer',
                operator: 'eq',
                value: isProspectiveCustomer
            });

            result.push({
                field: 'name',
                operator: 'Contains',
                value: this.model.get('name')
            });

            return result;
        },

        render: function () {

            var self = this;
            view.__super__.renderWithoutBindings.apply(self, arguments);


            var bindings = {
                '#isProspectiveCustomer': 'isProspectiveCustomer',
                '#name': 'name',
            };

            self.stickit(self.model, bindings);

            return self;
        }
    });

    return view;
});
