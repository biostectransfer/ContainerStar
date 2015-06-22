define(['module'], function (module) {
	var masterConfig = module.config ? module.config() : {},
		load = function (name, req, onLoad, config) {

			var parts = name.split('!'),
				language = Application.german ? '.de' : '.en',
				resourceName = name + language;

			require([resourceName], function (resources) {
				onLoad(resources);
			});
		};

	return {
		load: load
	};
})