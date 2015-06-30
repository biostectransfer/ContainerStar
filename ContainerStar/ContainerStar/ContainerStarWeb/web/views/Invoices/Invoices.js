define([
'base/base-object-grid-view',
'collections/Invoices/Invoices',
'l!t!Invoices/FilterInvoices',
'l!t!Invoices/InvoicesRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Invoices',
        editUrl: '#Invoices',
		
	    editItemTitle: function () {
	        return this.resources.edit;
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
                        { field: 'customerName', title: this.resources.customerName },
				        { field: 'communicationPartnerName', title: this.resources.communicationPartnerName },
                    ]
                },
			];
		}

	});

	return view;
});
