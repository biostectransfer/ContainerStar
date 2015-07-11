define([	
    'base/related-object-grid-view',
'collections/InvoiceStornos/InvoiceStornos',
], function (BaseView, Collection) {
	'use strict';

    var view = BaseView.extend({

		collectionType: Collection,
		gridSelector: '.grid',
		tableName: 'InvoiceStornos',

		initialize: function() {
			view.__super__.initialize.apply(this, arguments);

			this.defaultFiltering = { field: 'invoiceId', operator: 'eq', value: this.model.id };

			this.collection = new Collection();
		},

		columns: function () {
		   
		   return [
				{ field: 'proceedsAccount', title: this.resources.proceedsAccount },
				{ field: 'price', title: this.resources.price },
			];
		},
		
		render: function () {
		    var self = this;

		    view.__super__.render.apply(self, arguments);

		    self.grid.bind('edit', function (e) {
		        e.model.invoiceId = self.model.id;
		    });

		    return self;
		}
	});

	return view;
});
