define([
'base/base-object-grid-view',
'collections/Settings/Roles',
'l!t!Settings/FilterRole',
'l!t!Settings/RoleRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Role',
        editUrl: '#Roles',
		addNewModelInline: true,
		showAddButton: true,
		showEditButton: true,
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'name', title: this.resources.name },
			];
		}

	});

	return view;
});
