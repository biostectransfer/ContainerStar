define([
	'base/base-view',
    'calendar',
    'calendarLanguages',
    'mixins/kendo-widget-form',
	'mixins/kendo-validator-form',
	'mixins/bound-form'
], function (BaseView, Calendar, CalendarLanguages, KendoWidgetFormMixin, KendoValidatorFormMixin, BoundForm) {
	'use strict';
    
	var view = BaseView.extend({

	    bindings: function () {

	        var self = this;
	        var result = {
	            
	        };


	        return result;
	    },

	    initialize: function () {

	        view.__super__.initialize.apply(this, arguments);

	        this.model = new Backbone.Model();
	    },

		render: function () {
		    view.__super__.render.apply(this, arguments);

		    var self = this;

		    setTimeout(function () {
		        self.$el.find('#calendar').fullCalendar({
		            header: {
		                left: 'prev,next today',
		                center: 'title',
		                right: ''
		            },
		            editable: false,
		            lang: "de",
		            eventLimit: true, // allow "more" link when too many events

		            events: {
		                url: Application.apiUrl + 'disposition/?containerTypeId=777&name=ololo',
		                type: 'POST',
		                //data: {
		                //    containerTypeId: 1,
		                //    name: 'somethingelse'
		                //},
		                error: function (e) {
		                    debugger;
		                    //alert('there was an error while fetching events!');
		                },
		                //color: 'yellow',   // a non-ajax option
		                //textColor: 'black' // a non-ajax option
		            }
		        });
		    }, 0);

			return this;
		}
	});
    
	view.mixin(BoundForm);
	view.mixin(KendoValidatorFormMixin);
	view.mixin(KendoWidgetFormMixin);
    
	return view;
});