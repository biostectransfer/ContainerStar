define([
'base/base-object-grid-view',
'collections/Settings/Containers',
'l!t!Settings/FilterContainers',
'l!t!Settings/ContainersRelationships',
'Settings/Custom.Events.Containers',
'Settings/Custom.Toolbar.Containers'
], function (BaseView, Collection, FilterView, DetailView, CustomEvents, CustomToolbar) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Containers',
        editUrl: '#Containers',
		
		
		
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'number', title: this.resources.number },
				{ field: 'containerTypeId', title: this.resources.containerTypeId , collection: this.options.containerTypes, defaultText: this.resources.pleaseSelect},
				{ field: 'length', title: this.resources.length },
				{ field: 'width', title: this.resources.width },
				{ field: 'height', title: this.resources.height },
				{ field: 'color', title: this.resources.color },
				{ field: 'price', title: this.resources.price },
				{ field: 'isVirtual', title: this.resources.isVirtual , headerTitle: this.resources.isVirtual, checkbox: true},
				{ field: 'sellPrice', title: this.resources.sellPrice },
				{ field: 'isSold', title: this.resources.isSold , headerTitle: this.resources.isSold, checkbox: true},
				{ field: 'minPrice', title: this.resources.minPrice },
			];
		}

		,events: CustomEvents
		,toolbar: CustomToolbar
		,selectable: true
	});

	return view;
});
