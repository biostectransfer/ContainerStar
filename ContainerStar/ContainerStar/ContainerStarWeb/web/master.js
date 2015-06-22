define([
	'base/base-view',
	'mixins/bound-form',
	'mixins/kendo-widget-form',
	'kendo/kendo.menu'
], function (BaseView, BoundForm, KendoWidgetForm) {
	'use strict';

	var view = BaseView.extend({
		id: 'wrapper',

		bindings: {
			'.logout': {
				observe: 'isAuthenticated',
				visible: true
			},
			'.menu': {
				observe: 'isAuthenticated',
				visible: true
			},
			'.userName': 'name'			
		},

		events: {
		    'click .language': function () {
				Backbone.trigger('language:change');
			},
			'click .logout': function() {
				var self = this,
				userModel = new Backbone.Model();

				userModel.url = '/api/logout';
				userModel.save({}, {
					data: {
					},
					success: function () {
						Backbone.trigger('logged-out');
					},
					error: function () {						
					}
				});
			}
		},

		render: function () {
			view.__super__.render.apply(this, arguments);

			this.$('.menu').kendoMenu();			

			return this;
		},

		select: function (route) {
			this.$('.menu li').removeClass('selected');

			this.$('.menu li a[href=#' + route + ']').parent().addClass('selected');
		}
	});

	view.mixin(BoundForm);
	view.mixin(KendoWidgetForm);

	return view;
});