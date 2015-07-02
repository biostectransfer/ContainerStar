define([
	'base/base-window-view',
	'text!templates/Orders/CreateInvoiceConfirmation.html'
], function (BaseView, Template, Resources) {
	'use strict';

	var view = BaseView.extend({
		width: '400px',		

		template: Template,

		events: {
			'click .cancel': function (e) {
				e.preventDefault();

				this.close();
			},
			'click .montlyInvoice': function (e) {
				e.preventDefault();

				this.trigger('montlyInvoice');
				this.close();
			},
			'click .completeInvoice': function (e) {
				e.preventDefault();

				this.trigger('completeInvoice');
				this.close();
			}
		},

		initialize: function () {
			view.__super__.initialize.apply(this, arguments);

			this.title = this.options.title;
			this.model = new Backbone.Model({
				message: this.options.message
			});
		},

        render: function () {
			view.__super__.render.apply(this, arguments);

		    //this.$('[data-localized=continue]').html(Resources.continue);
		    //this.$('[data-localized=cancel]').html(Resources.cancel);

			return this;
		}
	});

	return view;
});