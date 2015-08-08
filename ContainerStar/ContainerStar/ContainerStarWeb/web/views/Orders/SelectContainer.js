define([
	'base/base-window-view',
    'l!t!Orders/ContainerSearch'
], function (BaseView, ContainerSearchView) {
    'use strict';

    var view = BaseView.extend({
        width: '1060px',
        title: function () { return this.resources.title; },        

        initialize: function () {

            view.__super__.initialize.apply(this, arguments);
        },

        render: function () {


            view.__super__.render.apply(this, arguments);

            this.stickit();

            var self = this;

            var options = {
                success: function (model) {

                    self.trigger('selectContainer', model);
                    self.close();
                },
                closeWindow: function () {
                    self.close();
                },
                containerTypes: self.options.containerTypes,
                equipments: self.options.equipments,
                isOffer: self.options.isOffer,
            };
            

            var containerSearchView = new ContainerSearchView(options);
            self.showView(containerSearchView, '.containers');

            return this;
        },

        open: function (e) {
            //this.kendoWindow.wrapper.css({ top: 100 });
        }
    });
    
    return view;
});