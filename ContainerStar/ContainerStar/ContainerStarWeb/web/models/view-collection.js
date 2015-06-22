define(function () {
	'use strict';

	var model = Backbone.Model.extend({
		urlRoot: '/api/viewCollection',
		parse: function (response) {
			for (var prop in response) {
				response[prop] = new Backbone.Collection(response[prop]);
			}

			return response;
		},
	}, {
		load: function (collectionTypes) {
			var d = new $.Deferred();

			var viewCollection = new model();
			viewCollection.fetch({
				data: collectionTypes
			}).done(function () {
				d.resolve(viewCollection);
			});

			return d.promise();
		}
	});

	return model;
});