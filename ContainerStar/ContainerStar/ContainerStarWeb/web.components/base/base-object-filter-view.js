define([
	'base/base-filter-view',
], function (BaseView) {
	'use strict';

	var view = BaseView.extend({
        
		getFilters: function () {

		    var result = [];

		    result.push({
		        field: 'name',
		        operator: 'contains',
		        value: this.model.get('name')
		    });		    
            
		    return result;
		},

		render: function () {

		    var self = this;
		    view.__super__.render.apply(self, arguments);

            
		    var bindings = {
		        '#name': 'name',
		    };

		    self.stickit(self.model, bindings);

		    return self;
		},

		renderWithoutBindings: function () {

		    var self = this;
		    view.__super__.render.apply(self, arguments);

		    return self;
		}
	});

	return view;
});