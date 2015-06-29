define([
    'base/base-view',
    'mixins/bound-form',
    'mixins/kendo-widget-form',
	'mixins/kendo-validator-form'    
], function (BaseView, BoundForm, KendoWidgetForm, KendoValidatorForm) {
    'use strict'

    var toggle = function () {
        this.$('.advanced').toggle();
    },

	setFilters = function () {
		var self = this,
			dataSource = self.options.grid.dataSource,
			expression = dataSource.filter() || { filters: [], logic: 'and' };

		if (self.validateFunc(self)) {
			var sourceFilters = self.getFilters();			

			_.each(sourceFilters, function (sourceFilter) {
				if (!sourceFilter.value) {
					var targetFilter = _.findWhere(expression.filters, { field: sourceFilter.field, operator: sourceFilter.operator }),
						index = expression.filters.indexOf(targetFilter);

					if(index > -1)
						expression.filters.splice(index, 1);
				}
				else {
					var match = _.filter(expression.filters, function (targetFilter) {
						if (sourceFilter.field === targetFilter.field && sourceFilter.operator === targetFilter.operator) {
							targetFilter.value = sourceFilter.value;

							return true;
						}
					});

					if (!match.length)
						expression.filters.push(sourceFilter);
				}
			});


			dataSource.filter(expression);
		}
    },

    view = BaseView.extend({
        filter: null,

        validateFunc: function(self)
        {
            return self.validate();
        },

    	getFilters: function() {
    		
    	},

    	initialize: function () {
    		view.__super__.initialize.apply(this, arguments);

    		this.model = new this.filter();
    	},

    	render: function () {
    		var self = this;

    		view.__super__.render.apply(self, arguments);

    		self.$el.delegate('button[type=submit]', 'click.base-filter-view', function (e) {
    			e.preventDefault();

    			setFilters.call(self);
    		});

    		self.$el.delegate('button[type=reset]', 'click.base-filter-view', function (e) {
    			e.preventDefault();
    			
    			self.model.clear().set(self.model.defaults);

    			setFilters.call(self);
            });

    		self.$el.delegate('.toggle', 'click.base-filter-view', _.bind(toggle, this));

    		return self;
        },

        close: function () {
            this.off('.base-filter-view');

            view.__super__.close.apply(this, arguments);
        }
    });

    view.mixin(BoundForm);
    view.mixin(KendoValidatorForm);
    view.mixin(KendoWidgetForm);

    return view;
});