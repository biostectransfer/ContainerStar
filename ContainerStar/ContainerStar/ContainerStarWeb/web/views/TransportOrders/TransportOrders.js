define([
'base/base-object-grid-view',
'collections/TransportOrders',
'l!t!TransportOrders/FilterTransportOrders',
'l!t!TransportOrders/TransportOrdersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
	'use strict';		
    
    var closeOrder = function (dataItem) {
        var self = this;

        var model = new Backbone.Model();
        model.url = Application.apiUrl + 'closeTransportOrder';
        model.set('id', dataItem.id);
        model.save({}, {
            success: function (model, response) {

                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Auftrag abschließen',
                        message: 'Auftrag wurde erfolgreich abgeschlossen.'
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
                        title: 'Auftrag abschließen',
                        message: 'Ausgewählter Auftrag konnte nicht abgeschlossen werden.'
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
        tableName: 'TransportOrders',
        editUrl: '#TransportOrders',
        selectable: true,

        defaultSorting: {
            field: 'id',
            dir: 'desc'
        },

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);
            
            this.defaultFiltering = [
                { field: 'isOffer', operator: 'eq', value: false },
		        { field: 'status', operator: 'eq', value: 1 }];
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
		    'dblclick .k-grid tbody tr:not(k-detail-row) td:not(.k-hierarchy-cell,.k-detail-cell,.commands,.positions-cell)': function (e) {

		        var self = this,
		            dataItem = self.grid.dataItem(e.currentTarget.parentElement);

		        if (dataItem != undefined && dataItem.id != undefined &&
                    dataItem.id != 0) {
		            location.href = self.editUrl + '/' + dataItem.id;
		        }
		    },
            'click .printTransportOrderInvoice': function (e) {

                e.preventDefault();
                var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

                if (dataItem != undefined) {

                    location.href = Application.apiUrl + 'print/?printTypeId=5&id=' + dataItem.id;
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
            },
            'click .closeOrder': function (e) {

                e.preventDefault();
                var self = this,
                    grid = self.grid,
					items = grid.select();

                if (items != undefined && items.length == 1) {

                    var dataItem = grid.dataItem(items[0]);
                    if (dataItem.status == 2) {
                        require(['base/information-view'], function (View) {

                            var view = new View({
                                title: 'Auftrag abschließen',
                                message: 'Ausgewählter Auftrag ist bereits abgeschlossen!'
                            });

                            self.addView(view);
                            self.$el.append(view.render().$el);
                        });
                    }
                    else {
                        require(['base/confirmation-view'], function (View) {

                            var view = new View({
                                title: 'Auftrag abschließen',
                                message: 'Möchten Sie ausgewählten Auftrag abschließen?'
                            });

                            self.listenTo(view, 'continue', _.bind(closeOrder, self, dataItem));
                            self.addView(view);
                            self.$el.append(view.render().$el);
                        });
                    }
                }
                else {
                    require(['base/information-view'], function (View) {
                        var view = new View({
                            title: 'Auftrag abschließen',
                            message: 'Wählen Sie bitte einen Auftrag aus!'
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
		            return '<a class="k-button k-button-icontext" href="' + self.editUrl +
		            '/create" data-localized="' + self.createNewItemTitle + '"></a>' + 
                    '<a class="k-button k-button-icontext printTransportOrderInvoice" href="#" data-localized="printTransportOrderInvoice"></a>' +
                    '<a class="k-button k-button-icontext closeOrder" href="#" data-localized="closeOrder"></a>';
		        }
		    }];

		    return result;
		}
	});

	return view;
});
