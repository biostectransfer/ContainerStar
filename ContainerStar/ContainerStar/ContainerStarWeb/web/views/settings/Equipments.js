define([
'base/base-object-grid-view',
'collections/Settings/Equipments',
'l!t!Settings/FilterEquipments'
], function (BaseView, Collection, FilterView) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        
        filterView: FilterView,
        tableName: 'Equipments',
        editUrl: '#Equipments',
		addNewModelInline: true,
		
		showEditButton: true,
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'description', title: this.resources.description },
			];
		}

	});

	return view;
});
