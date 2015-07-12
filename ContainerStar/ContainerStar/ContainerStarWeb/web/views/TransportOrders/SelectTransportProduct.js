define([
	'base/base-window-view',
    'l!t!TransportOrders/TransportProductSearch',
], function (BaseView, TransportProductSearchView) {
    'use strict';

    var view = BaseView.extend({
        width: '1060px',
        title: function () { return this.resources.title; },
        render: function () {
            var self = this;

            view.__super__.render.apply(this, arguments);

            var options = {
                success: function (model) {
 
                    self.trigger('selectTransportProduct', model);
                    self.close();
                },
                closeWindow: function () {
                    self.close();
                }
            };

            var transportProductSearchView = new TransportProductSearchView(options);
            self.showView(transportProductSearchView, '.transportProducts');

            return this;
        },

        open: function (e) {
            //this.kendoWindow.wrapper.css({ top: 100 });
        }
    });

    return view;
});