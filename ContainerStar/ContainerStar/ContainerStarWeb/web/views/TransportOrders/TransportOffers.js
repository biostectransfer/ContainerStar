define([
'base/base-object-grid-view',
'collections/TransportOrders',
'l!t!TransportOrders/FilterTransportOffers',
'l!t!TransportOrders/TransportOrdersRelationships'
], function (BaseView, Collection, FilterView, DetailView) {
    'use strict';

    var convertOfferToOrder = function (dataItem) {
        var self = this;

        var model = new Backbone.Model();
        model.url = Application.apiUrl + 'transportOffers';
        model.set('id', dataItem.id);
        model.save({}, {
            success: function (model, response) {

                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Angebot -> Auftrag',
                        message: 'Ausgewählte Angebot wurde erfolgreich in Auftrag verwandelt.'
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
                        title: 'Angebot -> Auftrag',
                        message: 'Ausgewählte Angebot konnte nicht in Auftrag verwandelt werden.'
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
        editUrl: '#TransportOffers',
        selectable: true,

        defaultSorting: {
            field: 'id',
            dir: 'desc'
        },

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            this.defaultFiltering = { field: 'isOffer', operator: 'eq', value: true };
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
            'click .offerToOrder': function (e) {

                e.preventDefault();
                var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

                if (dataItem != undefined) {
                    require(['base/confirmation-view'], function (View) {

                        var view = new View({
                            title: 'Angebot -> Auftrag',
                            message: 'Möchten Sie dieses Angebot in den Auftrag verwandeln?'
                        });
                        self.listenTo(view, 'continue', _.bind(convertOfferToOrder, self, dataItem));
                        self.addView(view);
                        self.$el.append(view.render().$el);
                    });
                }
                else {
                    require(['base/information-view'], function (View) {
                        var view = new View({
                            title: 'Angebot auswählen',
                            message: 'Wählen Sie bitte ein Angebot aus!'
                        });
                        self.addView(view);
                        self.$el.append(view.render().$el);
                    });
                }
            },
            'click .printOffer': function (e) {

                e.preventDefault();
                var self = this,
                    grid = self.grid,
					dataItem = grid.dataItem(grid.select());

                if (dataItem != undefined) {

                    location.href = Application.apiUrl + 'print/?printTypeId=6&id=' + dataItem.id;
                }
                else {
                    require(['base/information-view'], function (View) {
                        var view = new View({
                            title: 'Angebot auswählen',
                            message: 'Wählen Sie bitte ein Angebot aus!'
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
                    '<a class="k-button k-button-icontext printOffer" href="#" data-localized="printOffer"></a>' +
                    '<a class="k-button k-button-icontext offerToOrder" href="#" data-localized="offerToOrder"></a>';
		        }
		    }];

            return result;
        }
    });

    return view;
});
