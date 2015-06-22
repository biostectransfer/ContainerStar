define([
	'base/base-edit-model-view',
], function (BaseView) {
	'use strict';

    var disableInput = function (e, inputName, type) {

        //TODO rework with kendo widgets
        if (type == undefined) {
            e.$('#' + inputName).attr('disabled', !Application.canTableItemBeEdit(e.tableName, inputName));
        }
        else if (type == 'date') {
            e.$('#' + inputName).data('kendoDatePicker').enable(Application.canTableItemBeEdit(e.tableName, inputName));
        }
        else if (type == 'numeric') {
            e.$('#' + inputName).data('kendoNumericTextBox').enable(Application.canTableItemBeEdit(e.tableName, inputName));
        }
        else if (type == 'select') {
            e.$('#' + inputName).data('kendoDropDownList').enable(Application.canTableItemBeEdit(e.tableName, inputName));
        }
	},

    view = BaseView.extend({

	    tabView: null,
	    containerSelector: '.relations-container',
		tableName: null,
		successAction: null,
		cancelAction: null,		

		render: function () {

		    var self = this;
		    view.__super__.render.apply(self, arguments);

		    if (!Application.canTableItemBeEdit(self.tableName))
		        self.$('.save').remove();

		    if (!Application.canTableItemBeDeleted(self.tableName))
		        self.$('.remove').remove();


		    var bindings = {
		        '.relations-container': {
		            observe: 'id',
		            visible: true,
		            updateView: true,
		            update: function ($el, value) {
		                if (!value)
		                    return;

		                var self = this,
                            model = new Backbone.Model({}),
                            options = _.extend({}, {
                                model: model
                            }),
                        options = _.extend(options, self.options),
                        detView = new self.tabView(options);

		                self.showView(detView, self.containerSelector);
		            }
		        },
                '.remove': {
		            observe: 'id',
		            visible: true
                }
		    };

		    self.stickit(self.model, bindings);
            
		    return self;
		},
        
		success: function () {
		    location.hash = this.actionUrl + '/' + this.model.id;
		},

		cancel: function () {
		    location.hash = this.actionUrl;
		},

		disableInput: function (e, inputName, type) {

		    disableInput(e, inputName, type);
		}
	});

	return view;
});