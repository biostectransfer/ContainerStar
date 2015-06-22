define([
	'lr!mixins/resources/kendo-validator-form',
	'kendo/kendo.validator'
],
function (resources) {
	'use strict';

    var mixin = {
	    render: function () {
	        var self = this,
				form = self.$('form'),
				fields = Object.getPrototypeOf(self.model).fields;

	        if (_.isObject(fields)) {
	            _.each(fields, function (fieldProperties, fieldName) {
	                if (fieldName) {
	                    var $elem = form.find('[name=' + fieldName + ']'),
							validation = fieldProperties.validation;

						if(!self.$('.k-invalid-msg[data-for=' + fieldName + ']').length)
							$elem.after('<br /><span class="k-invalid-msg" data-for="' + fieldName + '"/>');

	                    if (validation && validation.required)
	                    	$elem.attr('required', true);

	                    if (validation && validation.email)
	                    	$elem.attr('type', 'email');

	                    if (validation && validation.date)
	                    	$elem.attr('data-date', true);

	                    if (validation && validation.integer)
	                    	$elem.attr('data-integer', true);

	                    if (validation && validation.maxLength)
	                        $elem.attr('data-maxlength', validation.maxLength);
	                }
	            });
	        }

			self.validator = form.kendoValidator({
				validateOnBlur: false,
				messages: {
					required: resources.required,
					email: resources.email,
					date: resources.date,
					integer: resources.integer,
					maxLength: function (input) {

					    var maxLength = Number(input.data('maxlength'));
					    return resources.maxLength.replace('%maxLength%', maxLength);
					},
				    modelState: function (input) {
				        var message = self.modelState['model.' + input.attr('name')][0];
				        if (self.resources && self.resources[message])
				            message = self.resources[message];
				        else if (self.validator.options.messages[message])
				            message = self.validator.options.messages[message];

				        return message;
				    }
				},
			    rules: { 
			        modelState: function (input) {
			            return !self.modelState || !self.modelState['model.' + input.attr('name')];
			        },
			        date: function (input) {
			        	if (input.data('date') === true) {
			        		var val = input.val();
			        		if (val)
			        			return !!kendo.parseDate(input.val());
			        	}

			        	return true;
			        },
			        integer: function (input) {
			        	if (input.data('integer') === true) {
			        		var val = input.val();
			        		if (val) {
			        			var parsed = kendo.parseFloat(val);
			        			return parsed === Math.ceil(parsed);
			        		}
			        	}

			        	return true;
			        },
			        maxLength: function (input) {

			            var maxLength = Number(input.data('maxlength'));
			            if (!isNaN(maxLength)) {
			                var val = input.val();

			                return val.length <= maxLength;
			            }

			            return true;
			        }
			    }
			}).data('kendoValidator');
		},

		validate: function () {
			return this.validator.validate();
		},

		validateResponse: function (response) {
		    if (response && response.responseJSON && response.responseJSON.modelState) {
		        this.modelState = response.responseJSON.modelState;
		        this.validate();
		        delete this.modelState;
		    }
		}
	};

	return mixin;
});