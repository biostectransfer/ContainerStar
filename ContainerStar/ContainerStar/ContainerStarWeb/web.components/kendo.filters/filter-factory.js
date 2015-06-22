define([
	'kendo.filters/multiselect',
    'kendo.filters/select-box'
], function (MultiSelect, SelectBox) {
	'use strict';

	var filterFactory = function (column) {
		this.column = column;
	};	
	
	_.extend(filterFactory.prototype, {
		multiSelect: function (container, options) {
			MultiSelect.call(this, container, options);
		},
		selectBox: function (container, options) {
		    SelectBox.call(this, container, options);
		}
	});

	return filterFactory;
});