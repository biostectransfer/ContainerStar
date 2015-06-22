define([	
    'widgets/kendo-select-box'
], function () {
	'use strict';

	var editor = function (container, options) {
		var self = this,
			sourceName = 'collection_' + options.field,
			$select = $('<select data-role="selectbox" multiple="multiple" data-bind="source: ' + sourceName + '" data-text-field="name" data-value-field="id" />').appendTo(container).attr('name', options.field);

		options.model[sourceName] = self.column.collection.toJSON();
	};

	return editor;
});