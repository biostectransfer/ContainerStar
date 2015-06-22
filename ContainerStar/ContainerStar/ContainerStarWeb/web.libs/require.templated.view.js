define(['module'], function (module) {
	var masterConfig = module.config ? module.config() : {},
		load = function (name, req, onLoad, config) {
			var parts = name.split('!'),
				templateName = 'text!templates/' + parts[parts.length - 1] + '.html';

			require([name, templateName], function (View, Template) {
				var ViewWithTemplate = View.extend({
					template: Template
				});

				onLoad(ViewWithTemplate);
			});
		};

	return {
		load: load
	};
})