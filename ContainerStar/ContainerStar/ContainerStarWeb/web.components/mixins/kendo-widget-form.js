define([
	'kendo/kendo.multiselect',
	'kendo/kendo.dropdownlist',
	'lk!kendo/kendo.colorpicker',
	'lk!kendo/kendo.datepicker',
	'lk!kendo/kendo.upload',
	'widgets/select-box',
    'kendo/kendo.numerictextbox'
],
function () {
    'use strict';

    var mixin = {
        render: function () {
            var self = this,
                widgets = self.widgets = [];

            self.$('select').each(function (index, elem) {
                var $elem = self.$(elem);

                if ($elem.is('[data-role=multiselect]'))
                    $elem.kendoMultiSelect();
                else if ($elem.is('[data-role=selectbox]'))
                    widgets.push($elem.selectBox().data('custom-selectBox'));
                else
                    widgets.push($elem.kendoDropDownList().data('kendoDropDownList'));
            });

            self.$('input[data-role=colorpicker]').each(function (index, elem) {
                widgets.push(self.$(elem).kendoColorPicker().data('kendoColorPicker'));
            });

            self.$('input[data-role=datepicker]').each(function (index, elem) {
                widgets.push(self.$(elem).kendoDatePicker().data('kendoDatePicker'));
            });

            self.$('input[type=file]').each(function (index, elem) {
                var options = _.defaults(self.getUploadOptions($(elem)) , {
                    multiple: false,
                    showFileList: true,
                });
                
                var upload = self.$(elem).kendoUpload(options).data('kendoUpload');
                widgets.push(upload);
            });

            self.$('input[data-role=numerictextbox]').each(function (index, elem) {
                widgets.push(self.$(elem).kendoNumericTextBox({
                    format: "#",
                    decimals: 0,
                    min: 0,
                    max: 99999
                }).data('kendoNumericTextBox'));
            });
        },

        getUploadOptions: function() {
            return options;
        },

        close: function () {
            if (this.widgets) {
                _.each(this.widgets, function (widget) {
                    widget.destroy();
                });
            }
            delete this.widgets;
        }
    };

    return mixin;
});