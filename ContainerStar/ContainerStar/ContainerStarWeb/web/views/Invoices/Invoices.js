define([
'base/base-object-grid-view',
'collections/Invoices/Invoices',
'l!t!Invoices/FilterInvoices',
'l!t!Invoices/InvoicesRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
    'use strict';


    var payBill = function (dataItem) {
        var self = this;

        var model = new Backbone.Model();
        model.url = Application.apiUrl + 'payBill';
        model.set('id', dataItem.id);
        model.save({}, {
            success: function (model, response) {

                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Rechnung bezahlen',
                        message: 'Ausgewählte Rechnung wurde erfolgreich bezahlt.'
                    });
                    self.addView(view);
                    self.$el.append(view.render().$el);

                    self.grid.dataSource.read();
                    self.grid.refresh();
                });
            },
            error: function (model, response) {
                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Rechnung bezahlen',
                        message: 'Ausgewählte Rechnung konnte nicht bezahlt werden.'
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
        tableName: 'Invoices',
        editUrl: '#Invoices',

        selectable: "multiple",
        showEditLinkButton: false,

        editItemTitle: function () {
	        return this.resources.edit;
	    },
		columns: function () {
			
		    return [
                {
                    title: this.resources.invoice,
                    columns: [
                        { field: 'invoiceNumber', title: this.resources.invoiceNumber },
				        { field: 'createDate', title: this.resources.createDate, format: '{0:d}' },
				        { field: 'payDate', title: this.resources.payDate, format: '{0:d}' },
                    ]
                },
                {
                    title: this.resources.customer,
                    columns: [
                        { field: 'rentOrderNumber', title: this.resources.rentOrderNumber },
                        { field: 'customerName', title: this.resources.customerName },
				        { field: 'communicationPartnerName', title: this.resources.communicationPartnerName },
                    ]
                },
			];
		},

		events: {

		    'click .pay': function (e) {

		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					items = grid.select();
                
		        if (items != undefined && items.length == 1) {

		            var dataItem = grid.dataItem(items[0]);
		            if (dataItem.payDate) {
		                require(['base/information-view'], function (View) {

		                    var view = new View({
		                        title: 'Rechnung bezahlen',
		                        message: 'Ausgewählte Rechnung ist bereits bezahlt!'
		                    });

		                    self.addView(view);
		                    self.$el.append(view.render().$el);
		                });
		            }
		            else {
		                require(['base/confirmation-view'], function (View) {
                            
		                    var view = new View({
		                        title: 'Rechnung bezahlen',
		                        message: 'Möchten Sie ausgewählte Rechnung bezahlen?'
		                    });
                            
		                    self.listenTo(view, 'continue', _.bind(payBill, self, dataItem));
		                    self.addView(view);
		                    self.$el.append(view.render().$el);
		                });
		            }
		        }
		        else {
		            require(['base/information-view'], function (View) {
		                var view = new View({
		                    title: 'Rechnung bezahlen',
		                    message: 'Wählen Sie bitte eine Rechnung aus!'
		                });
		                self.addView(view);
		                self.$el.append(view.render().$el);
		            });
		        }
		    },
		    'click .printBill': function (e) {

		        e.preventDefault();
		        var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

		        if (dataItem != undefined) {

		            location.href = Application.apiUrl + 'print/?printTypeId=1&id=' + dataItem.id;
		        }
		        else {
		            require(['base/information-view'], function (View) {
		                var view = new View({
		                    title: 'Rechnung auswählen',
		                    message: 'Wählen Sie bitte eine Rechnung aus!'
		                });
		                self.addView(view);
		                self.$el.append(view.render().$el);
		            });
		        }
		    },
		    'click .printRemaining': function (e) {

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
		                    title: 'Rechnung auswählen',
		                    message: 'Wählen Sie bitte eine Rechnung aus!'
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
		            return '<a class="k-button k-button-icontext printBill" href="#" data-localized="printBill"></a>' +
                    '<a class="k-button k-button-icontext printRemaining" href="#" data-localized="printRemaining"></a>' +
		            '<a class="k-button k-button-icontext pay" href="#" data-localized="pay"></a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
