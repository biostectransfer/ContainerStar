define([
	'base/base-view',
	'text!templates/master-footer.html'
], function (BaseView, MasterFooterTemplate) {
	'use strict';

	var view = BaseView.extend({
		id: 'footer-bgcontent',
		template: MasterFooterTemplate,
	});

	return view;
});