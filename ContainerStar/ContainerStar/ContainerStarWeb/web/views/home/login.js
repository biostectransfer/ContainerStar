define([
	'base/base-view',
	'models/login',
	'mixins/kendo-validator-form'
], function (BaseView, Model, KendoValidatorFormMixin) {
	'use strict';

    var login = function () {
        var self = this,
					userModel = new Backbone.Model();

        self.$('.summary').hide();

        if (!self.validate())
            return;

        userModel.url = '/api/login';
        userModel.save({}, {
            data: kendo.stringify(self.model.toJSON()),
            contentType: 'application/json',
            success: function () {
                Backbone.trigger('logged-in');
            },
            error: function (model, xhr) {
                self.$('.summary').html(
                    '<span class="k-icon k-warning"></span>' +
                    self.resources.invalid).show();
            }
        });
	},

	view = BaseView.extend({
		initialize: function() {
			view.__super__.initialize.apply(this, arguments);

			this.model = new Model();			
		},

		bindings: {
			'#login': 'login',
			'#password': 'password',
			'#rememberMe': 'rememberMe'
		},

		events: {
		    'click .login': function (e) {
		        e.preventDefault();
			    login.call(this);
			}
		},

		render: function () {
			view.__super__.render.apply(this, arguments);

			this.stickit();

			return this;
		},
	});

	view.mixin(KendoValidatorFormMixin);

	return view;
});