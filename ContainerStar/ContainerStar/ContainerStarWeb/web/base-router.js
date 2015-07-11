define([
], function () {
	'use strict';

    var createView = function (baseRouter, viewPath, collectionTypes, options) {

        var d = $.Deferred(); 
	    require([viewPath], function (View) {

	        if (!!collectionTypes) {

	            require(['models/view-collection'], function (ViewCollection) {

	                ViewCollection.load(collectionTypes).done(function (viewCollection) {

	                    options = _.extend({}, options, viewCollection.toJSON());
	                    d.resolve(new View(options));
	                });
	            });
	        }
	        else
	            d.resolve(new View(options));
	    });
        
	    return d.promise();
	},
	createViewWithModel = function (baseRouter, viewPath, modelPath, collectionTypes, id, options) {
	    var d = $.Deferred();

	    require([modelPath], function (Model) {
	        var model = new Model();
	        if (_.isUndefined(id) || _.isNull(id)) {
	            createView(baseRouter, viewPath, collectionTypes, { model: model }).done(function (view) {
	                if (options != null && options != undefined) {
	                    _.extend(model.attributes, options);
	                }

	                d.resolve(view);
	            });
	        }
	        else {
	            model.set(model.idAttribute, id);
	            model.fetch().done(function () {
	                createView(baseRouter, viewPath, collectionTypes, { model: model }).done(function (view) {
	                    if (options != null && options != undefined) {
	                        _.extend(model.attributes, options);
	                    }

	                    d.resolve(view);
	                });
	            });
	        }
	    });

	    return d.promise();
	},
	showView = function (baseRouter, viewPath, collectionTypes, options) {
        
	    var self = this;
	    if (Application.user.get('isAuthenticated'))
	        createView(baseRouter, viewPath, collectionTypes, options).done(
                function (view) {
                    baseRouter.trigger('router:view-created', view);
                });
	    else
	        showNotAuthenticated(baseRouter);
	},

	showViewWithModel = function (baseRouter, viewPath, modelPath, collectionTypes, id, options) {
	    var self = this;
	    if (Application.user.get('isAuthenticated'))
	        createViewWithModel(baseRouter, viewPath, modelPath, collectionTypes, id, options).done(function (view)
	        { baseRouter.trigger('router:view-created', view); });
	    else
	        showNotAuthenticated(baseRouter);
	},

	showNotAuthenticated = function (baseRouter) {
	    var self = this;

	    createView(baseRouter, 't!l!home/login').done(function (view) {
	        baseRouter.trigger('router:view-created', view);
	    });
	},

	result = 
    {
        createView: function (baseRouter, viewPath, collectionTypes, options) {
            
            return createView(baseRouter, viewPath, collectionTypes, options);
        },

        createViewWithModel: function (baseRouter, viewPath, modelPath, collectionTypes, id) {
            
            return createViewWithModel(baseRouter, viewPath, modelPath, collectionTypes, id);
        },

        showView: function (baseRouter, viewPath, collectionTypes, options) {

            return showView(baseRouter, viewPath, collectionTypes, options);
        },

        showViewWithModel: function (baseRouter, viewPath, modelPath, collectionTypes, options, id) {

            return showViewWithModel(baseRouter, viewPath, modelPath, collectionTypes, id, options);
        },

        showNotAuthenticated: function (baseRouter) {
            
            return showNotAuthenticated(baseRouter);
        }
    };

	return result;
});