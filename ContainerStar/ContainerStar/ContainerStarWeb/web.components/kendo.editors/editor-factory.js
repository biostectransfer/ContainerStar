define([
	'kendo.editors/multiselect',
	'kendo.editors/select-box',
	'kendo.editors/dropdownlist',
], function (MultiSelect, SelectBox, DropDownList) {
	'use strict';

	var editorFactory = function (column) {
		this.column = column;
	};

	_.extend(editorFactory.prototype, {
		multiSelect: function (container, options) {
			MultiSelect.call(this, container, options);
		},
		selectBox: function (container, options) {
			SelectBox.call(this, container, options);
		},
		dropDownList: function (container, options) {
		    DropDownList.call(this, container, options);
		}
	});

	return editorFactory;
});