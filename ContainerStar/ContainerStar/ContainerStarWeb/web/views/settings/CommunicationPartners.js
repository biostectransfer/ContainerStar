define([	
    'base/related-object-grid-view',
'collections/Settings/CommunicationPartners',
'l!t!Settings/AddCommunicationPartners'
], function (BaseView, Collection, AddNewModelView) {
	'use strict';

	var view = BaseView.extend({

		addNewModelView: AddNewModelView,
		collectionType: Collection,
		gridSelector: '.grid',
		tableName: 'CommunicationPartners',
        
        addingInPopup: false,

		initialize: function() {
			view.__super__.initialize.apply(this, arguments);

			this.defaultFiltering = { field: 'customerId', operator: 'eq', value: this.model.id };

			this.collection = new Collection();
		},

		columns: function () {
		   
		   return [
				{ field: 'name', title: this.resources.name , attributes: { "class": "detail-view-grid-cell" }},
				{ field: 'firstName', title: this.resources.firstName , attributes: { "class": "detail-view-grid-cell" }},
				{ field: 'phone', title: this.resources.phone , attributes: { "class": "detail-view-grid-cell" }},
				{ field: 'mobile', title: this.resources.mobile , attributes: { "class": "detail-view-grid-cell" }},
				{ field: 'fax', title: this.resources.fax , attributes: { "class": "detail-view-grid-cell" }},
				{ field: 'email', title: this.resources.email , attributes: { "class": "detail-view-grid-cell" }},
			];
		},
		
		render: function () {
		    var self = this;

		    view.__super__.render.apply(self, arguments);

		    self.grid.bind('edit', function (e) {
		        e.model.customerId = self.model.id;

				if (e.model.isNew()) {
                    var dt = new Date(2070, 11, 31);
		            e.model.toDate = dt;
		            var numeric = e.container.find("input[name=toDate]");
					
					if(numeric != undefined && numeric.length > 0)
						numeric[0].value = dt.toLocaleDateString();
		        }
		    });

		    return self;
		}
	});

	return view;
});
