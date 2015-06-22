define([
    'lr!kendo.filters/resources/multiselect',
	'kendo/kendo.multiselect'
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
		
		var multiSelect = $('<select />').insertAfter(menu.find(".k-filter-help-text")).kendoMultiSelect({
			dataTextField: self.column.dataTextField,
			dataValueField: self.column.dataValueField,
			dataSource: self.column.collection.toJSON()
		}).data("kendoMultiSelect");

		menu.find("[type=submit]").off();
		menu.find("[type=submit]").on("click", function (e) {
			e.preventDefault();

			var values = multiSelect.value();
			if (values.length) {
				var filter = {
					field: self.column.field,
					value: values,
					operator: function (items) {
						var result = _.any(items, function (item) { return _.contains(values, item); });
						return result;
					}
				};
				
				$(self.column.gridSelector).data("kendoGrid").dataSource.filter(filter);
			}

			kendoPopup.close();
		});
	};

	return filter;
});