define(function () {
	"use strict";

	function processFilter(filters) {
	    for (var i = 0; i < filters.length; i++) {
	        var filter = filters[i];
	        if (filter.value && filter.value instanceof Date)
	            filter.value = kendo.format("{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss}", filter.value);
	        else if (filter.filters)
	            processFilter(filter.filters);
	    }
	}

	function Transport(colWrap) {
		this.colWrap = colWrap;
	};

	_.extend(Transport.prototype, {

	    create: function (options) {
	        var data = options.data;

	        this.colWrap.create(data).done(function (model) {
	            options.success({
	                total: 1,
	                data: [model]
	            });
	        }).fail(function (xhr) {
	            options.error.apply(this, arguments);
	        });
	    },

		read: function (options) {
		    var collection = this.colWrap.collection;

		    if (options.data.filter)
		        processFilter(options.data.filter.filters);
			
			collection.fetch({
				data: options.data,
				success: function () {
					options.success({
						total: collection.total,
						data: collection.toJSON()
					});
				}
			});
		},

		update: function (options) {
			var model = this.colWrap.collection.get(options.data.cid || options.data.id);

			for (var fieldName in options.data) {
			    var value = options.data[fieldName];
			    if (value instanceof Date) {
			        
			        options.data[fieldName] = kendo.format("{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss}", value);
			    }
			}

		    model.save(options.data).done(function (data) {
			    options.success({
			        data: data
			    });
			}).fail(function (xhr) {
				options.error.apply(this, arguments);
			});
		},

		destroy: function (options) {
			var model = this.colWrap.collection.get(options.data.cid || options.data.id);

			model.destroy({
				wait: true
			}).done(function (xhr) {
				options.success(xhr.responseJSON);
			}).fail(function () {
				options.error.apply(this, arguments);
			});
		}
	});

	return Transport;
});