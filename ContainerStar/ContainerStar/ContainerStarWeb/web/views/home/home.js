define([
	'base/base-view'
], function (BaseView) {
	'use strict';

	var view = BaseView.extend({
		render: function () {
			view.__super__.render.apply(this, arguments);

			return this;
		}
	});

	return view;
});