// Backbone.Collection Adapter
// ---------------------------
//
// INTERNAL TYPE
//
// Wrap a Collection with DataSource configuration so that
// the two-way integration can occur without infinite loops
define(function () {
	"use strict";

	// Constructor function
	function Adapter(collection, dataSource) {
		this.collection = collection;
		this.dataSource = dataSource;

		//this.listenTo(this.collection, "add", this._addToDataSource);
		//this.listenTo(this.collection, "remove", this._removeFromDataSource);
		this.listenTo(this.collection, "reset", this._resetDataSource);
	};

	// Instance methods
	_.extend(Adapter.prototype, Backbone.Events, {
	    create: function (data, cb) {
	        var d = new $.Deferred();

	        if (this.addFromCol) {
	            // gate the add that came through the collection directly
	            return d.resolve(data).promise();
	        } else {
	            this.addFromDS = true;

	            // ensure the id is cleared out, not just
	            // an empty string
	            delete data.id;

	            // add the model to the collection, and
	            // for the return so that we can get the
	            // id from the new model, and send it to
	            // the datasource

	            var model = this.collection.create(data, {
	                success: function (model) {
	                    d.resolve(model.toJSON());
	                },
	                error: function (model, xhr) {
	                    d.reject(xhr);
	                }
	            });

	            this.addFromDS = false;
	        }

	        return d.promise();
	    },

		_addToDataSource: function (model) {
			// gate the add that came through the datasource directly
			if (!this.addFromDS) {
				this.addFromCol = true;

				var data = model.toJSON();
				this.dataSource.add(data);

				this.addFromCol = false;
			}
		},

		_removeFromDataSource: function (model) {
			var dsModel = this.dataSource.get(model.id);

			if (dsModel) {
				this.dataSource.remove(dsModel);
			}
		},

		_resetDataSource: function () {
			this.dataSource.read();
		}
	});

	return Adapter;
});