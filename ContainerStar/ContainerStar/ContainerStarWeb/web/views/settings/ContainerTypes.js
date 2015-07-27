define([
'base/base-object-grid-view',
'collections/Settings/ContainerTypes',
'l!t!Settings/FilterContainerTypes',
'l!t!Settings/ContainerTypesRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'ContainerTypes',
        editUrl: '#ContainerTypes',
		addNewModelInline: true,
		
		showEditButton: true,
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'name', title: this.resources.name },
				{ field: 'comment', title: this.resources.comment },
				{ field: 'dispositionRelevant', title: this.resources.dispositionRelevant , headerTitle: this.resources.dispositionRelevant, checkbox: true},
			];
		}

	});

	return view;
});
