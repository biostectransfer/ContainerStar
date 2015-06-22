define([
	'base/base-view',
	'kendo/kendo.window'
], function (BaseView) {
	'use strict';

    var view = BaseView.extend({
    	title: '',
        width: null,
        height: null,
		attributes: {
			style: 'display: none'
		},
		render: function () {
			view.__super__.render.apply(this, arguments);

			var self = this;

			self.$el.on('click.base-window-view', 'button[type=reset]', function (e) {
				e.preventDefault();
				self.close();
			});

			setTimeout(function () {
			    self.kendoWindow = self.$el.kendoWindow({
			        width: self.width,
			        height: self.height,
					actions: ["Close"],
					modal: true,
					title: _.result(self, 'title'),
					resizable: false,
					open: _.bind(self.open, self),
					close: function () {
						self.close();
					}
				}).data('kendoWindow');

				self.kendoWindow.center().open();
			}, 0);

			return this;
		},

		open: function() {

		},

		close: function () {
			this.$el.off('.base-window-view');

			view.__super__.close.apply(this, arguments);

			if (this.kendoWindow) {
				this.kendoWindow.destroy();
				delete this.kendoWindow;
			}
		}
	});

	return view;
});