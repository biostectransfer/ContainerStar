define([
	'kendo/kendo.dropdownlist'
], function () {
    'use strict';

    var editor = function (container, options) {
        $('<select required="required" />').appendTo(container).attr('name', options.field).kendoDropDownList({
            dataSource: options.values,
            dataTextField: 'text',
            dataValueField: 'value',
            valuePrimitive: true
            });

        $('<div class="k-widget k-tooltip k-tooltip-validation k-invalid-msg" style="margin: 0.5em; display: none;" data-for="' +
            options.field + '" role="alert"><span class="k-icon k-warning"> </span>Das Feld muss befüllt werden<div class="k-callout k-callout-n"></div></div>').appendTo(container)
    };

    return editor;
});