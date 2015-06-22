define([
'base/base-object-grid-view',
'collections/Orders',
'l!t!Orders/FilterOrders',
'l!t!Orders/OrdersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
    
    var generateBill = function (dataItem) {
	    var self = this;
	    
	    location.href = Application.apiUrl + 'print/?printTypeId=1&id=' + dataItem.id;
	},
    
    view = BaseView.extend({

        collectionType: Collection,
        detailView: DetailView,
        filterView: FilterView,
        tableName: 'Orders',
        editUrl: '#Orders',
        selectable: true,

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);
            
            this.defaultFiltering = { field: 'isOffer', operator: 'eq', value: false };
        },
		
		showDeleteButton: true,

	    editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
			return [
				{ field: 'customerName', title: this.resources.customerId },
				{ field: 'communicationPartnerTitle', title: this.resources.communicationPartnerId },
			];
		},

		events: {
		    'click .generateBill': function (e) {
		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

		        if (dataItem != undefined) {
		            require(['base/confirmation-view'], function (View) {

		                var view = new View({
		                    title: 'Rechnung erstellen',
		                    message: 'Möchten Sie für ausgewählten Auftrag eine Rechnung erstellen?'
		                });
		                self.listenTo(view, 'continue', _.bind(generateBill, self, dataItem));
		                self.addView(view);
		                self.$el.append(view.render().$el);
		            });
		        }
		        else {
		            require(['base/information-view'], function (View) {
		                var view = new View({
		                    title: 'Rechnung erstellen',
		                    message: 'Wählen Sie bitte ein Auftrag aus!'
		                });
		                self.addView(view);
		                self.$el.append(view.render().$el);
		            });
		        }
		    }
		},

		toolbar: function () {
		    var self = this,
		        result =
		    [{
		        template: function () {
		            return '<a class="k-button k-button-icontext" href="' + self.editUrl +
		            '/create" data-localized="' + self.createNewItemTitle + '"></a><a class="k-button k-button-icontext generateBill" href="#">Rechnung</a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
