define([
'base/base-object-grid-view',
'collections/Settings/Permissions',
'l!t!Settings/FilterPermission'
], function (BaseView, Collection, FilterView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        
        filterView: FilterView,
        tableName: 'Permission',
        editUrl: '#Permissions',
		addNewModelInline: true,
		
		showEditButton: true,
		

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'name', title: this.resources.name },
				{ field: 'description', title: this.resources.description },
			];
		}

	});

	return view;
});
