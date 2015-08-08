define([
	
], function () {
    'use strict';

    var copyContainer = function (dataItem) {
        var self = this;

        var model = new Backbone.Model();
        model.url = Application.apiUrl + 'copyContainer';
        model.set('id', dataItem.id);
        model.save({}, {
            success: function (model, response) {

                if (model.id != 0)
                    location.href = '#Containers/' + model.id;
                else {
                    require(['base/information-view'], function (View) {
                        var view = new View({
                            title: 'Container kopieren',
                            message: "Der ausgewählte Container konnte nicht kopiert werden."
                        });
                        self.addView(view);
                        self.$el.append(view.render().$el);
                    });
                }
            },
            error: function (model, response) {

                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Container kopieren',
                        message: 'Der ausgewählte Container konnte nicht kopiert werden.'
                    });
                    self.addView(view);
                    self.$el.append(view.render().$el);
                });
            }
        });
    },

    events = {
        'dblclick .k-grid tbody tr:not(k-detail-row) td:not(.k-hierarchy-cell,.k-detail-cell,.commands,.detail-view-grid-cell)': function (e) {

            var self = this,
                dataItem = self.grid.dataItem(e.currentTarget.parentElement);

            if (dataItem != undefined && dataItem.id != undefined &&
                dataItem.id != 0) {
                location.href = self.editUrl + '/' + dataItem.id;
            }
        },
        'click .copyContainer': function (e) {
            e.preventDefault();

            var self = this,
                grid = self.grid,
                items = grid.select();

            if (items != undefined && items.length == 1) {

                var dataItem = grid.dataItem(items[0]);

                require(['base/confirmation-view'], function (View) {

                    var view = new View({
                        title: 'Container kopieren',
                        message: 'Möchten Sie ausgewählten Container kopieren?'
                    });

                    self.listenTo(view, 'continue', _.bind(copyContainer, self, dataItem));
                    self.addView(view);
                    self.$el.append(view.render().$el);
                });
            }
            else {
                require(['base/information-view'], function (View) {
                    var view = new View({
                        title: 'Container kopieren',
                        message: 'Wählen Sie bitte einen Container aus!'
                    });
                    self.addView(view);
                    self.$el.append(view.render().$el);
                });
            }
        }
    };

    return events;
});