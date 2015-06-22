define([
	'base/base-object-filter-view',
	'models/Settings/FilterRole'
], function (BaseFilterView, Filter) {
    'use strict'

    var view = BaseFilterView.extend({

        filter: Filter
    });

    return view;
});
