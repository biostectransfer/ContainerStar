define([
	'base/base-view'
], function (BaseView, Template) {
	'use strict';

	var view = BaseView.extend({		
		template: Template		
	});

	return view;
});