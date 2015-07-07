define([
'base/base-object-grid-view',
'collections/Orders',
'l!t!Orders/FilterOrders',
'l!t!Orders/OrdersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
    
    var generateMonthlyInvoice = function (dataItem, isSell) {
        generateInvoice(dataItem, true, isSell);
    },

    generateCompleteInvoice = function (dataItem, isSell) {
        generateInvoice(dataItem, false, isSell);
    },

    generateInvoice = function (dataItem, isMonthlyInvoice, isSell) {
        var self = this;

        var model = new Backbone.Model();
        model.url = Application.apiUrl + 'addInvoices';
        model.set('orderId', dataItem.id); 
        model.set('isMonthlyInvoice', isMonthlyInvoice);
        model.set('isSell', isSell);
        model.save({}, {
            success: function (model, response) {

                location.href = '#Invoices/' + model.id;
            },
            error: function (model, response) {

                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Rechnung erstellen',
                        message: 'Für den ausgewählten Auftrag konnte die Rechnung nicht erstellt werden.'
                    });
                    self.addView(view);
                    self.$el.append(view.render().$el);
                });
            }
        });
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
		    'click .generateSellInvoice': function (e) {
		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

		        if (dataItem != undefined) {
		            require(['base/confirmation-view'], function (View) {

		                var view = new View({
		                    title: 'Rechnung erstellen',
		                    message: 'Möchten Sie für den ausgewählten Auftrag eine Rechnung erstellen?'
		                });
		                self.listenTo(view, 'continue', _.bind(generateCompleteInvoice, self, dataItem, true));
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
		    'click .generateRentInvoice': function (e) {
		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

		        if (dataItem != undefined) {
		            require(['l!t!Orders/CreateInvoiceConfirmation'], function (View) {

		                var view = new View({
		                    title: 'Rechnung erstellen',
		                    message: 'Möchten Sie für den ausgewählten Auftrag eine Rechnung erstellen?'
		                });
		                self.listenTo(view, 'montlyInvoice', _.bind(generateMonthlyInvoice, self, dataItem, false));
		                self.listenTo(view, 'completeInvoice', _.bind(generateCompleteInvoice, self, dataItem, false));
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
                    '<a class="k-button k-button-icontext generateSellInvoice" href="#" data-localized="generateSellInvoice"></a>' +
		            '<a class="k-button k-button-icontext generateRentInvoice" href="#" data-localized="generateRentInvoice"></a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
