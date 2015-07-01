define([
'base/base-object-grid-view',
'collections/Orders',
'l!t!Orders/FilterOrders',
'l!t!Orders/OrdersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
    
    var generateBill = function (dataItem) {
	    var self = this;
	    
	    location.href = '#AddInvoice/4';// + dataItem.id;
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
				{ field: 'rentOrderNumber', title: this.resources.rentOrderNumber },
				{ field: 'customerName', title: this.resources.customerId },
				{ field: 'communicationPartnerTitle', title: this.resources.communicationPartnerId },
			];
		},

		events: {
		    'dblclick .k-grid tbody tr:not(k-detail-row) td:not(.k-hierarchy-cell,.k-detail-cell,.commands)': function (e) {

		        var self = this,
		            dataItem = self.grid.dataItem(e.currentTarget.parentElement);

		        if (dataItem != undefined && dataItem.id != undefined &&
                    dataItem.id != 0) {
		            location.href = self.editUrl + '/' + dataItem.id;
		        }
		    },
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
		    },
            'click .printRentOrder': function (e) {

                e.preventDefault();
                var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

                if (dataItem != undefined) {

                    location.href = Application.apiUrl + 'print/?printTypeId=0&id=' + dataItem.id;
                }
                else {
                    require(['base/information-view'], function (View) {
                        var view = new View({
                            title: 'Auftrag auswählen',
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
		            '/create" data-localized="' + self.createNewItemTitle + '"></a>' + 
                    '<a class="k-button k-button-icontext printRentOrder" href="#" data-localized="printRentOrder"></a>' +
                    '<a class="k-button k-button-icontext generateBill" href="#" data-localized="generateBill"></a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
