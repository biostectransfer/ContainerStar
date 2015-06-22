define([
	'base/base-grid-view'
], function (BaseView) {
	'use strict';

	var view = BaseView.extend({

	    collectionType: null,
	    tableName: null,

	    showAddButton: true,
	    showEditButton: true,
	    showDeleteButton: true,


		initialize: function () {

		    view.__super__.initialize.apply(this, arguments);

			this.collection = new this.collectionType();
		},

		render: function () {

		    var self = this;
            
		    if (self.showAddButton)
		        self.showAddButton = Application.canTableItemBeCreated(self.tableName);

		    if(self.showDeleteButton)
		        self.showDeleteButton = Application.canTableItemBeDeleted(self.tableName);

		    if(self.showEditButton)
		        self.showEditButton = Application.canTableItemBeEdit(self.tableName);

		    view.__super__.render.apply(self, arguments);    

		    return self;
		},
	});

	return view;
});