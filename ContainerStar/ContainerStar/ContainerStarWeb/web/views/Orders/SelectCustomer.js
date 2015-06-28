define([
	'base/base-window-view',
    'l!t!Orders/CustomerSearch',
], function (BaseView, CustomersView) {
    'use strict';

    var view = BaseView.extend({
        width: '1060px',
        title: function () { return this.resources.title; },
        render: function () {
            var self = this;

            view.__super__.render.apply(this, arguments);

            var options = {
                success: function (model) {
                    self.trigger('select', model);
                    self.close();
                },
                closeWindow: function () {
                    self.close();
                }
            };

            var customersView = new CustomersView(options);
            self.showView(customersView, '.customers');

            return this;
        },

        open: function (e) {
            //this.kendoWindow.wrapper.css({ top: 100 });
        }
    });

    return view;
});