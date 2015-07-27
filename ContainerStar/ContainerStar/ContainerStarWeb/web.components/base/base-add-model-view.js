define([
	'base/base-window-view',
    'lr!base/resources/base-add-model-view',
    'mixins/kendo-widget-form',
	'mixins/kendo-validator-form',
	'mixins/bound-form'
], function (BaseView, Resources, KendoWidgetFormMixin, KendoValidatorFormMixin, BoundForm) {
	'use strict';	

	var addModel = function(e) {
		e.preventDefault();

		var self = this;
		if (self.validate())
			self.save();
		else
			self.kendoWindow.center();
	},

	view = BaseView.extend({
	    title: function() {
	        return Resources.title;
	    },
		bindings: null,		

		render: function () {
			view.__super__.render.apply(this, arguments);

			this.$el.on('click.base-add-model-view', 'button[type=submit]', _.bind(addModel, this));

		    this.$('[data-localized=add]').html(Resources.add);
		    this.$('[data-localized=cancel]').html(Resources.cancel);

			return this;
		},

		close: function () {
			this.$el.off('.base-add-model-view');

			view.__super__.close.apply(this, arguments);

			return this;
		},

		save: function () {
			var self = this;

			self.model.url = self.collection.url;
			self.model.save({}, {
			    success: function () {
			        self.trigger('base-add-model-view:save', self.model);
			        self.close();

			        //debugger;
			        //if (!self.model.isNew()) {
			        //require(['base/information-view'], function (View) {
			        //    var view = new View({
			        //        title: 'Information speichern',
			        //        message: 'Information wurde erfolgreich gespeichert.'
			        //    });
			        //    self.addView(view);
			        //    self.$el.append(view.render().$el);
			        //});
			        //}
				},
				error: function (model, response) {
					self.validateResponse(response);
				}
			});
		}
	});

	view.mixin(BoundForm);
	view.mixin(KendoValidatorFormMixin);
	view.mixin(KendoWidgetFormMixin);

	return view;
});