define([
    'base/related-object-grid-view',
    'collections/Positions'
], function (BaseView, Collection) {
    'use strict';

    var view = BaseView.extend({

        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'Positions',
        showAddButton: false,      

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            var self = this;

            this.defaultFiltering = [
                { field: 'orderId', operator: 'eq', value: this.model.get('orderId') },
            ];

            this.collection = new Collection();
        },

        columns: function () {
            return [
                 { field: 'description', title: this.resources.description, filterable: false, sortable: false },
                 { field: 'price', title: this.resources.price },
                 { field: 'amount', title: this.resources.amount },
                 { field: 'fromDate', title: this.resources.fromDate, format: '{0:d}' },
                 { field: 'toDate', title: this.resources.toDate, format: '{0:d}' }
            ];
        },

        render: function () {
            var self = this;

            view.__super__.render.apply(self, arguments);

            return self;
        }
    });

    return view;
});
