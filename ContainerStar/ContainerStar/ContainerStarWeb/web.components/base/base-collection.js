define(function () {
	'use strict';

	var collection = Backbone.Collection.extend({
		parse: function (response, options) {
			this.total = response.total;

			return response.data;
		},
		saveChanged: function (models) {
			var self = this,
				CollectionAsModel = Backbone.Model.extend({
					isNew: function () { return false },
					initialize: function (options) {
						this.collection = options.collection;
					},
					toJSON: function () {
						return this.collection;
					},
					url: self.url
				});

			var model = new CollectionAsModel({ collection: models });
			model.save({
				success: function (models) {
					self.set(models, { add: false, remove: false, merge: true });
				}
			});			
		},
		removeSelected: function () {
			var deferred = new $.Deferred,
				self = this,
				models = self.where({ selected: true }),
				ids = _.pluck(models, 'id'),
				model = new Backbone.Model();

			model.url = self.url;
			model.isNew = function () { return false };
			
			model.destroy({
				data: kendo.stringify(ids),
				contentType: 'application/json',
				success: function () {
					//self.remove(models);
					deferred.resolve();
				}
			});

			return deferred.promise();
		}
	});

	return collection;
});