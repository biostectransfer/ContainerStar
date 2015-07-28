define([
    'base/related-object-grid-view',
    'collections/TransportPositions',
    'l!t!TransportOrders/AddTransportPositions',
    'l!t!TransportOrders/SelectTransportProduct',
], function (BaseView, Collection, AddNewModelView, SelectTransportProductView) {
    'use strict';

    var view = BaseView.extend({

        addNewModelView: AddNewModelView,
        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'TransportPositions',

        addingInPopup: false,

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            var self = this;

            this.defaultFiltering = [
                { field: 'transportOrderId', operator: 'eq', value: this.model.id }
            ];

            this.collection = new Collection();
        },

        columns: function () {
            return [
                 { field: 'description', title: this.resources.description, filterable: false, sortable: false, attributes: { "class": "positions-cell" } },
                 { field: 'price', title: this.resources.price, attributes: { "class": "positions-cell" } },
                 { field: 'amount', title: this.resources.amount, attributes: { "class": "positions-cell" } }
            ];
        },

        render: function () {
            var self = this;

            view.__super__.render.apply(self, arguments);

            self.grid.bind('edit', function (e) {
                e.model.transportOrderId = self.model.id;
            });

            return self;
        },

        events: {
            'click .selectTransportProduct': function (e) {
                e.preventDefault();

                var self = this,
                    view = new SelectTransportProductView(self.options);

                self.listenTo(view, 'selectTransportProduct', function (item) {
                    
                    var model = new Backbone.Model();
                    model.isNew = function () { return true; }
                    model.url = Application.apiUrl + '/TransportPositions';
                    model.set('transportOrderId', self.model.id);
                    model.set('transportProductId', item.id);
                    model.set('price', item.get('price'));
                    model.set('amount', 1);
 
                    model.save({}, {
                        data: kendo.stringify(model),
                        method: 'post',
                        contentType: 'application/json',
                        success: function (response) {
                            self.grid.dataSource.read();
                            self.grid.refresh();                            
                        },
                        error: function (model, response) {
                            //TODO
                        }
                    });
                });

                self.addView(view);
                self.$el.append(view.render().$el);
            }
        },

        toolbar: function () {
            var self = this,
		        result =
		    [{
		        template: function () {
		            return '<a class="k-button k-button-icontext selectTransportProduct"  style="min-width: 120px;"href="#" data-localized="selectTransportProduct"></a>';
		        }
		    }];

            return result;
        }
    });

    return view;
});
