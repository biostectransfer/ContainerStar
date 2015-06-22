define(function () {
	"use strict";

	function Transport(colWrap) {
		this.colWrap = colWrap;
	};

	_.extend(Transport.prototype, {

		create: function (options) {
			var data = options.data;

			this.colWrap.add(data, function (model) {
				options.success(model);
			});
		},

		read: function (options) {
			var collection = this.colWrap.collection;
			options.success({
				total: collection.length,
				data: collection.toJSON()
			});
		},

		update: function (options) {
			model.set(options.data);
			options.success(options.data);
		},

		destroy: function (options) {
			var model = this.colWrap.collection.get(options.data.cid || options.data.id);
			this.colWrap.collection.remove(model);
		}
	});

	return Transport;
});