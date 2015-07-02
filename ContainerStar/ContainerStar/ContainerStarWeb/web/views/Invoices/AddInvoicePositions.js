define([
    'base/related-object-grid-view',
    'collections/Invoices/AddInvoicePositions'
], function (BaseView, Collection) {
    'use strict';

    var dateEditor = function (container, options) {

        if (options.model.get('isCointainerPosition')) {
            $('<input data-role="datepicker" required data-text-field="' + options.field + '" data-value-field="' + options.field + '" data-bind="value:' + options.field + '"/>')
                .appendTo(container);
        }
        else {
            var textValue = options.model.get(options.field),
                value = kendo.format("{0:d}", value == null ? "" : value);

            $('<span>' + value + '</span>').appendTo(container);
        }
    },

    view = BaseView.extend({

        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'Positions',
        showAddButton: false,      

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            var self = this;

            this.defaultFiltering = [
                { field: 'invoiceId', operator: 'eq', value: this.model.id },
            ];

            this.collection = new Collection();
        },

        columns: function () {
            return [
                 { field: 'description', title: this.resources.description, filterable: false, sortable: false },
                 { field: 'price', title: this.resources.price },
                 { field: 'totalPrice', title: this.resources.totalPrice },
                 { field: 'amount', title: this.resources.amount, filterable: false, sortable: false },
                 {
                     field: 'fromDate',
                     editor: dateEditor,
                     template: '#= kendo.format("{0:d}", data.fromDate == null ? "" : data.fromDate) #',
                     title: this.resources.fromDate,
                     format: '{0:d}'
                 },
                 {
                     field: 'toDate',
                     editor: dateEditor,
                     template: '#= kendo.format("{0:d}", data.toDate == null ? "" : data.toDate) #',
                     title: this.resources.toDate,
                     format: '{0:d}'
                 }
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
