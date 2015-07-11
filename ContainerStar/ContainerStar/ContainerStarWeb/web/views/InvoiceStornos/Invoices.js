define([
'base/base-object-grid-view',
'collections/Invoices/Invoices',
'l!t!InvoiceStornos/FilterInvoices',
'l!t!InvoiceStornos/InvoicesRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
    'use strict';
    
    var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Invoices',
        editUrl: '#Invoices',

        showAddButton: false,
        showEditLinkButton: false,

        editItemTitle: function () {
	        return this.resources.edit;
        },

        defaultSorting: {
            field: 'invoiceNumber',
            dir: 'desc'
        },
        
        initialize: function () {

            view.__super__.initialize.apply(this, arguments);

            this.defaultFiltering = { field: 'isPayed', operator: 'eq', value: 1 };
        },

		columns: function () {
			
		    return [
                {
                    title: this.resources.invoice,
                    columns: [
                        { field: 'invoiceNumber', title: this.resources.invoiceNumber },
				        { field: 'createDate', title: this.resources.createDate, format: '{0:d}' },
				        { field: 'payDate', title: this.resources.payDate, format: '{0:d}' },
                    ]
                },
                {
                    title: this.resources.customer,
                    columns: [
                        { field: 'rentOrderNumber', title: this.resources.rentOrderNumber },
                        { field: 'customerName', title: this.resources.customerName },
				        { field: 'communicationPartnerName', title: this.resources.communicationPartnerName },
                    ]
                },
			];
		},

		events: {
		},

		toolbar: null,
	});

	return view;
});
