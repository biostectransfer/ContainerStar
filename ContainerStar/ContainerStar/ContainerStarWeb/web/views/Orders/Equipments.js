define([
    'base/related-object-grid-view',
    'collections/Equipments',
    'l!t!Settings/AddContainerEquipmentRsp'
], function (BaseView, Collection, AddNewModelView) {
    'use strict';

    var view = BaseView.extend({

        addNewModelView: AddNewModelView,
        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'OrderContainerEquipmentRsps',

        addingInPopup: false,

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            this.defaultFiltering = [
                { field: 'orderId', operator: 'eq', value: this.model.get('orderId') },
                { field: 'containerId', operator: 'eq', value: this.model.get('containerId') }];

            this.collection = new Collection();
        },

        columns: function () {

            return [
                 { field: 'equipmentId', title: this.resources.equipmentId, collection: this.options.equipments, defaultText: this.resources.pleaseSelect, attributes: { "class": "detail-view-grid-cell" } },
                 { field: 'amount', title: this.resources.amount, attributes: { "class": "detail-view-grid-cell" } },
            ];
        },

        render: function () {
            var self = this;

            view.__super__.render.apply(self, arguments);

            self.grid.bind('edit', function (e) {

                e.model.orderId = self.model.get('orderId');
                e.model.containerId = self.model.get('containerId');
            });

            return self;
        }
    });

    return view;
});
