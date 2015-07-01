define([
    'base/related-object-grid-view',
    'collections/Positions',
    'l!t!Orders/AddPositions',
    'l!t!Orders/SelectContainer',
    'l!t!Orders/SelectAdditionalCosts'
], function (BaseView, Collection, AddNewModelView, SelectContainerView, SelectAdditionalCostsView) {
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

        isSellOrder: null,
        selectContainerView: SelectContainerView,
        selectAdditionalCostsView: SelectAdditionalCostsView,
        addNewModelView: AddNewModelView,
        collectionType: Collection,
        gridSelector: '.grid',
        tableName: 'Positions',

        addingInPopup: false,

        initialize: function () {
            view.__super__.initialize.apply(this, arguments);

            var self = this;

            this.defaultFiltering = [
                { field: 'orderId', operator: 'eq', value: this.model.id },
                { field: 'isSellOrder', operator: 'eq', value: self.isSellOrder }
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
                 },
                 { field: 'fromDate', title: this.resources.fromDate, format: '{0:d}' },
                 { field: 'toDate', title: this.resources.toDate, format: '{0:d}' }
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
                    
                    var model = new Backbone.Model();
                    model.isNew = function () { return true; }
                    model.url = Application.apiUrl + '/positions';
                    model.set('orderId', self.model.id);
                    model.set('containerId', item.id);
                    model.set('price', item.get('price'));
                    model.set('fromDate', item.get('fromDate'));
                    model.set('toDate', item.get('toDate'));
                    model.set('isSellOrder', self.isSellOrder);
 
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
            },
            'click .selectAdditionalCosts': function (e) {
                e.preventDefault();

                var self = this,
                    view = new SelectAdditionalCostsView(self.options);

                self.listenTo(view, 'selectAdditionalCosts', function (item) {

                    var model = new Backbone.Model();
                    model.isNew = function () { return true; }
                    model.url = Application.apiUrl + '/positions';
                    model.set('orderId', self.model.id);
                    model.set('additionalCostId', item.id);
                    model.set('price', item.get('price'));
                    model.set('amount', 1);
                    model.set('isSellOrder', self.isSellOrder);

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
		            return '<a class="k-button k-button-icontext selectContainer" style="min-width: 180px;" href="#" data-localized="' + 
                        (self.isSellOrder ? 'saleContainer' : 'rentContainer') + '"></a>' +
		                   '<a class="k-button k-button-icontext selectAdditionalCosts"  style="min-width: 120px;"href="#" data-localized="selectAdditionalCosts"></a>';
		        }
		    }];

            return result;
        }
    });

    return view;
});
