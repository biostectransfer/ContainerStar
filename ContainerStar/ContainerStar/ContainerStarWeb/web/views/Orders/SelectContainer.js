define([
	'base/base-window-view',
    'l!t!Orders/ContainerSearch',
], function (BaseView, ContainerSearchView) {
    'use strict';

    var view = BaseView.extend({
        width: '1060px',
        title: function () { return this.resources.title; },
        render: function () {
            var self = this;

            view.__super__.render.apply(this, arguments);

            var options = {
                success: function (model) {

                    self.trigger('selectContainer', model);
                    self.close();
                },
                closeWindow: function () {
                    self.close();
                },
                containerTypes: self.options.containerTypes,
                isSellOrder: self.options.isSellOrder
            };

            var containerSearchView = new ContainerSearchView(options);
            self.showView(containerSearchView, '.containers');

            return this;
        },

        open: function (e) {
            this.kendoWindow.wrapper.css({ top: 100 });
        }
    });

    return view;
});