define([
'base/base-object-grid-view',
'collections/Settings/Customers',
'l!t!Settings/FilterCustomers',
'l!t!Settings/CustomersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Customers',
        editUrl: '#Customers',
		
		
		
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'number', title: this.resources.number },
				{ field: 'name', title: this.resources.name },
				{ field: 'street', title: this.resources.street },
				{ field: 'zip', title: this.resources.zip },
				{ field: 'city', title: this.resources.city },
				{ field: 'withTaxes', title: this.resources.withTaxes , headerTitle: this.resources.withTaxes, checkbox: true},
				{ field: 'autoBill', title: this.resources.autoBill , headerTitle: this.resources.autoBill, checkbox: true},
				{ field: 'discount', title: this.resources.discount },
				{ field: 'isProspectiveCustomer', title: this.resources.isProspectiveCustomer , headerTitle: this.resources.isProspectiveCustomer, checkbox: true},
			];
		}

	});

	return view;
});
