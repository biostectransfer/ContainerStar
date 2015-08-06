define([	
    'base/related-object-grid-view',
'collections/InvoiceStornos/InvoiceStornos',
], function (BaseView, Collection) {
	'use strict';

    var view = BaseView.extend({

		collectionType: Collection,
		gridSelector: '.grid',
		tableName: 'InvoiceStornos',
        
        selectable: true,

		initialize: function() {
			view.__super__.initialize.apply(this, arguments);

			this.defaultFiltering = { field: 'invoiceId', operator: 'eq', value: this.model.id };

			this.collection = new Collection();
		},

		columns: function () {
		   
		   return [
				{ field: 'proceedsAccount', title: this.resources.proceedsAccount, collection: this.options.proceedsAccounts },
				{ field: 'price', title: this.resources.price },
                { field: 'freeText', title: this.resources.freeText },
			];
		},
		
		render: function () {
		    var self = this;

		    view.__super__.render.apply(self, arguments);

		    self.grid.bind('edit', function (e) {
		        e.model.invoiceId = self.model.id;
		    });

		    return self;
		},

		events: {
		    'click .printInvoiceStorno': function (e) {

		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

		        if (dataItem != undefined) {

		            location.href = Application.apiUrl + 'print/?printTypeId=2&id=' + dataItem.id;
		        }
		        else {
		            require(['base/information-view'], function (View) {
		                var view = new View({
		                    title: 'Gutschrift auswählen',
		                    message: 'Wählen Sie bitte eine Gutschrift aus!'
		                });
		                self.addView(view);
		                self.$el.append(view.render().$el);
		            });
		        }
		    },
		},

		toolbar: function () {
		    var self = this,
		        result =
		    [{
		        template: function () {
		            return '<a class="k-button k-button-icontext k-grid-create-inline" href="#" data-localized="add"></a>' +
                    '<a class="k-button k-button-icontext printInvoiceStorno" href="#" data-localized="printInvoiceStorno"></a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
