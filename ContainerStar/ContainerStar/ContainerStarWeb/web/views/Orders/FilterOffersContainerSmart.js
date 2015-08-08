define([
	'base/base-object-filter-view',
	'models/FilterOffersContainerSmart'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter,

        validateFunc: function(self)
        {
            var result = false,
                fromDate = self.model.get('fromDate'),
                toDate = self.model.get('toDate'),
                dateValidator = self.$el.find('.dateValidator');
            
            if (self.validate())
            {
                if (fromDate && toDate) {
                    if (fromDate > toDate) {
                        dateValidator.show();
                    }
                    else {
                        dateValidator.hide();
                        result = true;
                    }
                }
                else
                    result = true;
            }
            return result;
        },

        getFilters: function () {

            var result = [],
                fromDate = this.model.get('fromDate'),
                toDate = this.model.get('toDate'),
                containerTypeId = this.model.get('containerTypeId'),
                name = this.model.get('name'),
                equipments = this.model.get('equipments');
            
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
                field: 'containerTypeId',
                operator: 'eq',
                value: containerTypeId
            });

            result.push({
                field: 'name',
                operator: 'contains',
                value: this.model.get('name')
            });

            result.push({
                field: 'equipments',
                operator: 'contains',
                value: equipments ? equipments.join(',') : null
            });

            return result;
        },

        bindings: function () {

            var self = this;
  
            var result = {

                '#fromDate': 'fromDate',
                '#toDate': 'toDate',
                '#name': 'name',
                '#containerTypeId': {
                    observe: 'containerTypeId',
                    selectOptions: {
                        labelPath: 'name', valuePath: 'id',
                        collection: self.options.containerTypes,
                        defaultOption: { label: self.resources.pleaseSelect, value: null }
                    },
                },
                '#equipments': {
                    observe: 'equipments',
                    selectOptions: {
                        labelPath: 'name', valuePath: 'id',
                        collection: self.options.equipments
                    },
                }
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
