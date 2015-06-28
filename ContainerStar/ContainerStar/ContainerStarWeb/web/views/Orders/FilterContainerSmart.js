define([
	'base/base-object-filter-view',
	'models/ContainerSmart'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter,

        getFilters: function () {

            var result = [],
                fromDate = this.model.get('fromDate'),
                toDate = this.model.get('toDate');

            result.push({
                field: 'fromDate',
                operator: 'lte',
                value: fromDate
            });

            result.push({
                field: 'toDate',
                operator: 'gte',
                value: toDate
            });

            result.push({
                field: 'name',
                operator: 'eq',
                value: this.model.get('name')
            });

            return result;
        },

        render: function () {

            var self = this;

            view.__super__.renderWithoutBindings.apply(self, arguments);

            var bindings = {
                '#fromDate': 'fromDate',
                '#toDate': 'toDate',
                '#name': 'name',
                '#containerTypeId': {
                    observe: 'containerTypeId',
                    selectOptions: {
                        labelPath: 'name', valuePath: 'id',
                        collection: function()
                        {
                            return this.options.containerTypes.
                                   map(function (item) {
                                       debugger;
                                       var a = item.toJSON();
                                       return { id: item.get('id'), value: item.get('value') };
                                   });
                        }
                    },
                },
            };

            self.stickit(self.model, bindings);

            return self;
        }
    });

    return view;
});
