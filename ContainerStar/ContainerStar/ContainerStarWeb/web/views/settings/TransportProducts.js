define([
'base/base-object-grid-view',
'collections/Settings/TransportProducts',
'l!t!Settings/FilterTransportProducts'
], function (BaseView, Collection, FilterView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        
        filterView: FilterView,
        tableName: 'TransportProducts',
        editUrl: '#TransportProducts',
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
				{ field: 'proceedsAccount', title: this.resources.proceedsAccount },
			];
		}

	});

	return view;
});
