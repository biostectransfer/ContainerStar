define([
	'base/base-window-view',
	'mixins/kendo-validator-form',
	'mixins/localized-view',
	'mixins/bound-form',
    'lr!resources/Settings/Custom.ChangePassword',
], function (BaseView, KendoValidatorFormMixin, LocalizedViewMixin, BoundForm, Resources) {
	'use strict';

	var view = BaseView.extend({
		title: function () {
			return Resources.changePassword;
		},
		events: {
			'click button[type=submit]': function (e) {
				e.preventDefault();
				var self = this;

				if (self.validate()) {
					self.model.url = 'api/users',
					self.model.save({
						id: self.model.id,
						password: self.model.get('password'),
						passwordConfirmation: self.model.get('passwordConfirmation')
					}, {
						patch: true,
						success: function () {
						    self.trigger('passwordChangedEvent', self.model);
							self.close();
						},
						error: function (model, response) {
						    self.validateResponse(response);
						}
					});
				}
			}			
		},

		bindings: {
			'#password': 'password',
			'#passwordConfirmation': 'passwordConfirmation',
		},

		render: function () {
			view.__super__.render.apply(this, arguments);

			this.stickit();

			var self = this;

			self.$el.find('#passwordTitle').text(Resources.password);
			self.$el.find('#passwordConfirmationTitle').text(Resources.passwordConfirmation);
			self.$el.find('#change').text(Resources.change);
			self.$el.find('#cancel').text(Resources.cancel);

			return this;
		}
	});

	view.mixin(KendoValidatorFormMixin);
	view.mixin(LocalizedViewMixin);
	view.mixin(BoundForm);

	return view;
});