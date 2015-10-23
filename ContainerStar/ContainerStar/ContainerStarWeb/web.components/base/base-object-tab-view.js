define([
	'base/base-view',
    'kendo/kendo.tabstrip'
], function (BaseView) {
    'use strict';

    var createTab = function (tabPath, tabSelector, tabIndex) {
        var self = this;
        //,deferred = $.Deferred();

        require([tabPath], function (TabView) {
            if (self.$(tabSelector).data('tab-view'))
                return;

            var model = new Backbone.Model({}),
	                options = _.extend({}, self.options, {
	                    model: model
	                }),
                options = _.extend(options, self.options),
                tabView = new TabView(options);

            self.showView(tabView, tabSelector);
            $(tabSelector).data('tab-view', tabView);

            //deferred.resolve();
            self.trigger('tabCreated', tabIndex);
        });

        //return deferred.promise();
    },

    view = BaseView.extend({

        tabs: null,
        needCreateTabs: true,

        render: function () {
            var self = this;

            view.__super__.render.apply(self, arguments);

            if (self.needCreateTabs) {
                self.createTabs();
            }

            return self;
        },

        createTabs: function () {
            var self = this;

            //TODO create template for tabstrip for role model (permissions for tabs)

            var tabs = self.tabs();

            if (self.$('.tab-strip').length) {

                this.tabStrip = self.$('.tab-strip').kendoTabStrip({
                    animation: false,
                    //activate: function (e) {

                    //    var index = $(e.item).index();
                    //    createTab.call(self, tabs[index].view, tabs[index].selector, index);

                    //}
                }).data('kendoTabStrip');
            }

            for (var i in tabs) {
                createTab.call(self, tabs[i].view, tabs[i].selector, i);
            }
        },

        close: function () {
            if (this.tabstrip) {
                this.tabStrip.destroy();
            }
        }
    });

    return view;
});