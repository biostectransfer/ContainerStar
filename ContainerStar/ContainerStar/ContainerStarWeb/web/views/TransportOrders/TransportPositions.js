define([
    'base/related-object-grid-view',
    'collections/TransportPositions',
    'l!t!TransportOrders/AddTransportPositions',
    'l!t!TransportOrders/SelectTransportProduct',
], function (BaseView, Collection, AddNewModelView, SelectTransportProductView) {
    'use strict';

    var amountEditor = function (container, options) {
        if (options.model.get('containerId') == null) {
            //$('<input data-role="numerictextbox" required data-text-field="' + options.field + '" data-value-field="' + options.field + '" data-bind="value:' + options.field + '"/>')
            //    .appendTo(container);

            $('<span class="k-widget k-numerictextbox">' +  
                '<span class="k-numeric-wrap k-state-default">' +
                    '<input tabindex="0" class="k-formatted-value k-input" aria-disabled="false" aria-readonly="false" style="display: inline-block;" type="text">' + 
                    '<input class="k-input" role="spinbutton" aria-disabled="false" aria-readonly="false" aria-valuenow="-1" style="display: none;" required="required" type="text" data-role="numerictextbox" data-bind="value:amount" data-value-field="amount" data-text-field="amount">' + 
                    '<span class="k-select"><span class="k-link" style="-ms-touch-action: double-tap-zoom pinch-zoom;" unselectable="on">' + 
                        '<span title="Wert erhöhen" class="k-icon k-i-arrow-n" unselectable="on">Wert erhöhen</span>' + 
                    '</span>' +
                    '<div class="k-widget k-tooltip k-tooltip-validation k-invalid-msg" role="alert" style="margin: 0.5em; display: none;" data-for="_amount_"><span class="k-icon k-warning"> </span>Das Feld muss befüllt werden<div class="k-callout k-callout-n"></div></div>'+
                    '<span class="k-link" style="-ms-touch-action: double-tap-zoom pinch-zoom;" unselectable="on">' + 
                        '<span title="Wert verkleinern" class="k-icon k-i-arrow-s" unselectable="on">Wert verkleinern</span>' + 
                    '</span>' + 
                '</span>' + 
            '</span></span>'+
            '<div class="k-widget k-tooltip k-tooltip-validation k-invalid-msg" style="margin: 0.5em; display: none;" data-for="' +
                options.field + '" role="alert"><span class="k-icon k-warning"> </span>Das Feld muss befüllt werden<div class="k-callout k-callout-n"></div></div>').appendTo(container);
            //$('<div class="k-widget k-tooltip k-tooltip-validation k-invalid-msg" style="margin: 0.5em; display: none;" data-for="' +
            //    options.field + '" role="alert"><span class="k-icon k-warning"> </span>Das Feld muss befüllt werden<div class="k-callout k-callout-n"></div></div>').appendTo(container);
        }
        else
        {
            $('<span>1</span>').appendTo(container);
        }
    },

    view = BaseView.extend({

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
                 { field: 'description', title: this.resources.description, filterable: false, sortable: false },
                 { field: 'price', title: this.resources.price },
                 {
                     field: 'amount',
                     editor: amountEditor, template: "#=amount#",
                     title: this.resources.amount
                 }
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
                            debugger;
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
