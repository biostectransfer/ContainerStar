define(function () {
	'use strict';

	var model = Backbone.Model.extend({
		fields: {
			login: { type: "string", validation: { required: true } },
			password: { type: "string", validation: { required: true } },
			rememberme: { type: "boolean" }
		}
	});

	return model;
});