define([
	'kendo/kendo.multiselect'
], function () {
	'use strict';

	var editor = function (container, options) {
		$('<select />').appendTo(container).attr('name', options.field).kendoMultiSelect({
			dataSource: this.column.collection.toJSON(),
			dataTextField: this.column.dataTextField,
			dataValueField: this.column.dataValueField,
			valuePrimitive: true
		});
	};

	return editor;
});