define([
	'module',
	'mixins/localized-view'
], function (module, LocalizedViewMixin) {
	var masterConfig = module.config ? module.config() : {},
		load = function (name, req, onLoad, config) {
			var parts = name.split('!'),
				resourceName = 'lr!resources/' + parts[parts.length - 1];

			require([name, resourceName], function (View, resources) {
				var LocalizedView = View.extend({
					resources: resources
				});
				LocalizedView.mixin(LocalizedViewMixin);

				onLoad(LocalizedView);
			});
		};

	return {
		load: load
	};
})