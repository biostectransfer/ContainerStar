define([
'base/base-object-grid-view',
'collections/Settings/Users',
'l!t!Settings/FilterUser',
'Settings/Custom.User',
'Settings/Custom.Events.User'
], function (BaseView, Collection, FilterView, CustomColumns, CustomEvents) {
	'use strict';		
	var view = BaseView.extend({

        collectionType: Collection,
        
        filterView: FilterView,
        tableName: 'User',
        editUrl: '#Users',
		
		
		showEditButton: true,
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return $.merge( CustomColumns(),
[
				{ field: 'roleId', title: this.resources.roleId , collection: this.options.role, defaultText: this.resources.pleaseSelect},
				{ field: 'login', title: this.resources.login },
				{ field: 'name', title: this.resources.name },
			]);
		}

		,events: CustomEvents
	});

	return view;
});
