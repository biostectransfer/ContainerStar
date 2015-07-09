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

		            events: function (start, end, timezone, callback) {
		                debugger;
		                $.ajax({
		                    url: Application.apiUrl + 'disposition',
		                    type: 'POST',
		                    data: {
		                        containerTypeId: 555,
		                        name: "LoL",
		                        startDate: start.toString(),
		                        endDate: end.toString()
		                    },
		                    success: function (doc) {

		                        var events = [];
		                        $(doc).each(function () {

		                            events.push({
		                                title: $(this).attr('title'),
		                                start: $(this).attr('start'),
		                                end: $(this).attr('end'),
		                                url: $(this).attr('url'),
		                            });
		                        });
		                        callback(events);
		                    },
		                    error: function (e) {
		                        debugger;
		                        //alert('there was an error while fetching events!');
		                    },
		                });
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