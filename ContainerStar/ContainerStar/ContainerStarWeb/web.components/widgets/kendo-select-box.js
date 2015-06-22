define([
    'kendo/kendo.binder',
	'kendo/kendo.editable',
	'widgets/select-box'
], function () {
	'use strict';

    var kendo = window.kendo,
        ui = kendo.ui,
        Widget = ui.Widget,
        Binder = kendo.data.Binder,
        CHANGE = "change",
        BLUR = "blur",
        ns = ".kendoSelectBox";
	
    var SelectBox = Widget.extend({
    	init: function (element, options) {
            var self = this;
            Widget.fn.init.call(self, element, options);

            self.options.textField = self.options.dataTextField = $(element).data('text-field');
            self.options.valueField = self.options.dataValueField = $(element).data('value-field');
            self.options.valuePrimitive = true;

            $(element).change(function () {
                self.trigger(CHANGE);
            });
        },
        options: {
            name: "SelectBox"
        },
        value: function (values) {
            if (_.isUndefined(values)) {
                return $(this.element).selectBox('values');                
            }

            this.element.find('option').each(function (index, option) {
                $(option).prop('selected', _.any(values, function (value) { return $(option).val() == value }));
            });

            $(this.element).selectBox('restore');
        },

        events: [CHANGE],

        dataBound: function () {
            $(this.element).selectBox();
        }
    });
    ui.plugin(SelectBox);

    var binder = {
        source: kendo.data.binders.source.extend({
            init: function (widget, bindings, options) {
                kendo.data.binders.source.fn.init.call(this, widget.element.get(0), bindings, options);

                this.widget = widget;
            },
            refresh: function () {
                kendo.data.binders.source.fn.refresh.apply(this, arguments);

                this.widget.dataBound();
            }
        }),
        value: kendo.data.binders.widget.multiselect.value       
    };

    kendo.data.binders.widget['selectbox'] = binder;
});