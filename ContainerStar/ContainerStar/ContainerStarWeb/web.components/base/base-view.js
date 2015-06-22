define([
	'kendo.backbone/kendo.backbone.viewevents'
], function (ViewEvents) {
	'use strict';
	
	var mixin = function (from) {
		var to = this.prototype;

		// we add those methods which exists on `from` but not on `to` to the latter
		_.defaults(to, from);
		// … and we do the same for events
		_.defaults(to.events, from.events);

		// we then extend `to`'s `initialize`
		extendMethod.call(this, to, from, "initialize");
		// … and its `render`
		extendMethod.call(this, to, from, "render");

		extendMethod.call(this, to, from, "close");
	},
	// Helper method to extend an already existing method
	extendMethod = function (to, from, methodName) {
		// if the method is defined on from ...
		if (!_.isUndefined(from[methodName])) {
			var old = to[methodName];

			// ... we create a new function on to
			to[methodName] = function () {

				// wherein we first call the method which exists on `to`
				var oldReturn = old.apply(this, arguments);

				// and then call the method on `from`
				from[methodName].apply(this, arguments);

				// and then return the expected result,
				// i.e. what the method on `to` returns
				return oldReturn;

			};
		}
	},
	view = Backbone.View.extend({
		kendoUIEvents: null,

		initialize: function(options) {
			this.options = options;
		},

		render: function () {
			var self = this,
				template = _.template(self.template);

			view.__super__.render.apply(self, arguments);

			self.$el.html(template(self.model ? self.model.toJSON() : {}));

			self.$('[data-permission]').each(function (index, elem) {

				var $elem = $(elem),
					permission = $elem.data('permission'),
					userPermissions = Application.user.get('permissions');

				if (!userPermissions || userPermissions.indexOf(permission) == -1)
					$elem.hide();
			});

			ViewEvents.delegate(this);

			return this;
		},

		addView: function(view)  {
			this.views = this.views || [];

			this.views.push(view);
		},

		close: function () {

			if (this.views) {
				_.each(this.views, function (view) {
					if(!view.closed)
						view.close();
				});
			}
			
			this.trigger('close');

			ViewEvents.undelegate(this);

			this.off();
			this.remove();

			this.closed = true;
		},

		showView: function (view, selector) {
			selector = this.$(selector);

			this.addView(view);
			this.viewBySelector = this.viewBySelector || {};
			
			var oldView = this.viewBySelector[selector.selector];
			if (oldView && !oldView.closed) {
				oldView.close();
			}

			this.viewBySelector[selector.selector] = view;

			selector.html(view.render().$el);
		}		
	}, {
		mixin: mixin
	});

	return view;
});