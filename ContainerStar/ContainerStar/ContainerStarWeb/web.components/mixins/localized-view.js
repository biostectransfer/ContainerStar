define(function () {
	'use strict';

	var mixin = {
		render: function () {
			var resources = _.result(this, 'resources');

			this.$('[data-localized]').each(function (index, elem) {
				var $elem = $(elem),
					parts = $elem.data('localized').split(':'),
					resourceName = parts[0],
					childSelector = parts[1];

				if (resources[resourceName]) {
					if (parts[1])
						$elem.find(parts[1]).html(resources[resourceName]);
					else
						$elem.html(resources[resourceName]);
				}
			});
		}
	};

	return mixin;
});