define([
    'base/related-object-grid-view',
    'collections/Positions',
    'l!t!Orders/AddPositions',
    'l!t!Orders/SelectContainer',
    'l!t!Orders/SelectAdditionalCosts'
], function (BaseView, Collection, AddNewModelView, SelectContainerView, SelectAdditionalCostsView) {
    'use strict';

    var view = BaseView.extend({

        addNewModelView: AddNewModelView,
        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'Positions',

        addingInPopup: false,

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            this.defaultFiltering = { field: 'orderId', operator: 'eq', value: this.model.id };

            this.collection = new Collection();
        },

        columns: function () {

            return [
                 { field: 'containerId', title: this.resources.containerId },
                 { field: 'price', title: this.resources.price },
            ];
        },

        render: function () {
            var self = this;

            view.__super__.render.apply(self, arguments);

            self.grid.bind('edit', function (e) {
                e.model.orderId = self.model.id;

                if (e.model.isNew()) {
                    var dt = new Date(2070, 11, 31);
                    e.model.toDate = dt;
                    var numeric = e.container.find("input[name=toDate]");

                    if (numeric != undefined && numeric.length > 0)
                        numeric[0].value = dt.toLocaleDateString();
                }
            });

            return self;
        },

        events: {
            'click .selectContainer': function (e) {
                e.preventDefault();

                var self = this,
                    view = new SelectContainerView(self.options);

                self.listenTo(view, 'selectContainer', function (item) {

                    //self.model.set('containerId', item.id);
                });

                self.addView(view);
                self.$el.append(view.render().$el);
            },
            'click .selectAdditionalCosts': function (e) {
                e.preventDefault();

                var self = this,
                    view = new SelectAdditionalCostsView(self.options);

                self.listenTo(view, 'selectAdditionalCosts', function (item) {

                    
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
		            return '<a class="k-button k-button-icontext selectContainer" href="#" data-localized="selectContainer"></a>' +
		                   '<a class="k-button k-button-icontext selectAdditionalCosts" href="#" data-localized="selectAdditionalCosts"></a>';
		        }
		    }];

            return result;
        }
    });

    return view;
});
