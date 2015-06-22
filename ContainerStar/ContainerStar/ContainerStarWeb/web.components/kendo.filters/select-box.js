define([
    'lr!kendo.filters/resources/select-box',
	'widgets/select-box'
], function (Resources) {
	'use strict';

	var filter = function (element) {
		var self = this,
			menu = $(element).parent(),
			kendoPopup = menu.parent().data('kendoPopup');

		if (!menu.length)
			return;

		menu.find("[data-role=dropdownlist]").remove();
		menu.find("input[type=text]").remove();
		menu.find(".k-filter-help-text").text(Resources.title);
		
		var $select = $('<select multiple />').insertAfter(menu.find(".k-filter-help-text")).selectBox({
			dataTextField: self.column.dataTextField,
			dataValueField: self.column.dataValueField,
			dataSource: self.column.collection.toJSON()
		});

		menu.find("[type=submit]").off();
		menu.find("[type=submit]").on("click", function (e) {
			e.preventDefault();

			var values = $select.selectBox('values');
			if (values.length) {
				var filter = {
					field: self.column.field,
					value: values.join(','),
					operator: 'contains'
				};
				
				$(self.column.gridSelector).data("kendoGrid").dataSource.filter(filter);
			}

			kendoPopup.close();
		});
	};

	return filter;
});