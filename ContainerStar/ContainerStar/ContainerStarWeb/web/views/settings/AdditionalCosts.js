define([
'base/base-object-grid-view',
'collections/Settings/AdditionalCosts',
'l!t!Settings/FilterAdditionalCosts'
], function (BaseView, Collection, FilterView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        
        filterView: FilterView,
        tableName: 'AdditionalCosts',
        editUrl: '#AdditionalCosts',
		addNewModelInline: true,
		
		showEditButton: true,
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'name', title: this.resources.name },
				{ field: 'description', title: this.resources.description },
				{ field: 'price', title: this.resources.price },
				{ field: 'automatic', title: this.resources.automatic , headerTitle: this.resources.automatic, checkbox: true},
				{ field: 'includeInFirstBill', title: this.resources.includeInFirstBill , headerTitle: this.resources.includeInFirstBill, checkbox: true},
				{ field: 'proceedsAccount', title: this.resources.proceedsAccount },
			];
		}

	});

	return view;
});
