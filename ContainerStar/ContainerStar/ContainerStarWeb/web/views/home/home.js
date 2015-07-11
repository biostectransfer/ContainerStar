define([
	'base/base-view',
    'calendar',
    'mixins/kendo-widget-form',
	'mixins/kendo-validator-form',
	'mixins/bound-form',
    'models/ShowContainer'
], function (BaseView, Calendar, KendoWidgetFormMixin, KendoValidatorFormMixin, BoundForm, Model) {
	'use strict';
    
	var bindCalendar = function (self) {

	    var model = self.model;

	    require(['calendarLanguages'], function () {

	        self.$el.find('#calendar').fullCalendar({
	            header: {
	                left: 'prev,next today',
	                center: 'title',
	                right: ''
	            },
	            editable: false,
	            lang: "de",
	            eventLimit: true, // allow "more" link when too many events,
                	            
	            events: function (start, end, timezone, callback) {

	                $.ajax({
	                    url: Application.apiUrl + 'showContainer',
	                    type: 'POST',
	                    data: {
	                        containerTypeId: model.get('containerTypeId'),
	                        name: model.get('name'),
	                        equipments: model.get('equipments'),
	                        startDateStr: start.date() + '.' + (start.month() + 1) + '.' + start.year(),
	                        endDateStr: end.date() + '.' + (end.month() + 1) + '.' + end.year(),
	                        searchFreeContainer: model.get('searchFreeContainer')
	                    },
	                    success: function (doc) {

	                        var events = [];
	                        $(doc).each(function () {

	                            events.push({
	                                title: this.title,
	                                start: this.start,
	                                end: this.end,
	                                url: this.url,
	                                color: model.get('searchFreeContainer') == true ? '#009D59' : ''
	                            });
	                        });
	                        callback(events);
	                    },
	                    error: function (e) {
	                        debugger;
	                        //alert('there was an error while fetching events!');
	                    }
	                });
	            }
	        });
	    });
	},


	view = BaseView.extend({

	    bindings: function () {

	        var self = this;
	        var result = {
	            '#name': 'name',
	            '#containerTypeId': {
	                observe: 'containerTypeId',
	                selectOptions: {
	                    labelPath: 'name', valuePath: 'id',
	                    collection: self.options.containerTypes,
	                    defaultOption: { label: self.resources.pleaseSelect, value: null }
	                },
	            },
	            '#equipments': {
	                observe: 'equipments',
	                selectOptions: {
	                    labelPath: 'name', valuePath: 'id',
	                    collection: self.options.equipments
	                },
	            }	            
	        };


	        return result;
	    },

	    initialize: function () {

	        view.__super__.initialize.apply(this, arguments);
	        
	        var self = this;
	        self.model = new Model();
	        self.model.set('searchFreeContainer', self.options.searchFreeContainer);
	    },

		render: function () {
		    view.__super__.render.apply(this, arguments);

		    var self = this;

		    setTimeout(function () { bindCalendar(self); }, 0);

			return this;
		},

		events: {
		    'click .apply': function (e) {

		        var self = this;
		        self.$el.find('#calendar').fullCalendar('destroy');
		        bindCalendar(self);		        
		    },
		    'click .cancel': function (e) {

		        var self = this;
		        self.model.clear().set(self.model.defaults);

		        self.$el.find('#calendar').fullCalendar('destroy');
		        bindCalendar(self);		        
		    },
		}
	});
    
	view.mixin(BoundForm);
	view.mixin(KendoValidatorFormMixin);
	view.mixin(KendoWidgetFormMixin);
    
	return view;
});