define([
	'backbone.stickit'
], function () {
	'use strict';
	
	var selectConfig = Backbone.Stickit.getConfiguration($('<select />'));

	Backbone.Stickit.addHandler([{
		selector: 'select[multiple]',
		getVal: function ($el, event, options) {
			var selected = $el.find('option:selected');
			return _.map(selected, function (el) {
				return Number($(el).val());
			});
		},
		update: function ($el, values) {
			selectConfig.update.apply(this, arguments);
			
			var selectBox = $el.data('custom-selectBox');
			if (selectBox)
				selectBox.restore();

			var multiSelect = $el.data('kendoMultiSelect');
			if (multiSelect) {
				multiSelect.setDataSource();
				multiSelect.value(values);
			}
			
		}
	}, {
		selector: 'select:not([multiple])',
		getVal: function ($el, event, options) {
			return Number($el.find('option:selected').val());
		},
		update: function ($el, value) {
			selectConfig.update.apply(this, arguments);

			var dropdownlist = $el.data('kendoDropDownList');
			if (dropdownlist) {
			    dropdownlist.setDataSource();
			    dropdownlist.value(value);
			}
		}
	}, {
	    selector: '[data-role=datepicker]',
	    getVal: function ($el) {
	    	var value = $el.val();
	    	
	    	if (!value)
	    	    return null;

	    	return kendo.format("{0:yyyy'-'MM'-'dd'T'HH':'mm':'ss}", kendo.parseDate(value));
	    },
	    update: function ($el, value) {
	    	if (!value)
	    		$el.val('');
			else
				$el.val(kendo.format('{0:d}', kendo.parseDate(value)));
	    }
	}
	]);

	var mixin = {
		render: function () {
			this.stickit();
		},

		close: function () {
			this.unstickit();
		}
	};

	return mixin;
});