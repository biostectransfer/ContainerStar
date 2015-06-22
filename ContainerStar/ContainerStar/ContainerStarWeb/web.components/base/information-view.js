define([
	'base/base-window-view',
	'text!base/templates/information-view.html',
    'lr!base/resources/confirmation-view'
], function (BaseView, Template, Resources) {
	'use strict';

	var view = BaseView.extend({
		width: '400px',		

		template: Template,

		events: {
			'click .continue': function (e) {
				e.preventDefault();

				this.trigger('continue');
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

		    this.$('[data-localized=continue]').html(Resources.continue);

			return this;
		}
	});

	return view;
});