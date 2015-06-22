define([
	'base/base-view',
    'lr!base/resources/base-edit-model-view',
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
	},

	deleteModel = function(e) {
	    e.preventDefault();

	    var self = this;

	    require(['base/confirmation-view'], function (Confirmation) {

	    	var confirmation = new Confirmation({
	    	    title: Resources.removeRecord,
	    	    message: Resources.removeConfirmation
	    	});

	    	self.listenToOnce(confirmation, 'continue', function () {
	    		self.model.destroy({
	    		    success: function () {
	    				location.hash = self.cancelAction;
	    			}
	    		});
	    	});

	    	self.addView(confirmation);
	    	self.$el.append(confirmation.render().$el);
	    });
	},

    cancel = function (e) {
        e.preventDefault();

        this.cancel();
    },

    showConcurrencyError = function showConcurrencyMessage(self) {
        
        require(['base/confirmation-view'], function (Confirmation) {

            var confirmation = new Confirmation({
                title: Resources.concurencyTitle,
                message: Resources.concurencyMessage
            });

            self.listenToOnce(confirmation, 'continue', function () {
                location.reload();
            });

            self.addView(confirmation);
            self.$el.append(confirmation.render().$el);

        });
    },

	view = BaseView.extend({

        cancelAction: null,
		bindings: null,		

		render: function () {
			view.__super__.render.apply(this, arguments);

			this.$el.on('click.base-edit-model-view', '.save', _.bind(addModel, this));
			this.$el.on('click.base-edit-model-view', '.remove', _.bind(deleteModel, this));
			this.$el.on('click.base-edit-model-view', '.cancel', _.bind(cancel, this));

		    this.$('[data-localized=add]').html(Resources.add);
		    this.$('[data-localized=cancel]').html(Resources.cancel);
		    this.$('[data-localized=save]').html(Resources.save);
		    this.$('[data-localized=delete]').html(Resources.delete);

			return this;
		},

		close: function () {
			this.$el.off('.base-edit-model-view');

			view.__super__.close.apply(this, arguments);

			return this;
		},

		save: function () {
			var self = this;

			self.model.save({}, {
			    success: function () {

					self.trigger('base-edit-model-view:save', self.model);
				},
			    error: function (model, response) {

			        if (response.statusText == 'Conflict') {
			            showConcurrencyError(self);
			        };
			        self.validateResponse(response);
				}
			});
		},

		cancel: function () {
		}
	});

	view.mixin(BoundForm);
	view.mixin(KendoValidatorFormMixin);
	view.mixin(KendoWidgetFormMixin);

	return view;

	
});