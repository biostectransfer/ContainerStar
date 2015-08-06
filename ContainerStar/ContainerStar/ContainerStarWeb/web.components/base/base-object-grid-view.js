define([
	'base/base-grid-view',
    'base/base-view'
], function (BaseView, DetailViewContainer) {
	'use strict';

    var initDetailView = function (e) {

        var self = this;

        var options = _.extend({}, self.options, { model: e.data }),
            view = new self.detailView(options);

        self.addView(view);
        e.detailRow.find('.detailsContainer').append(view.render().$el);

        var extendedView = '<a href="#" onclick="event.preventDefault(); location.href=' + "'" + self.editUrl + '/' + e.data.id +
        "'" + '" class="gotoObjectDetail"><label>' + self.editItemTitle() + '</label>' +
        '</a><div class="relations-container"></div>';

        if (!self.showEditLinkButton)
        {            
            extendedView = '<div class="relations-container"></div>';
        }

        e.detailRow.find('.extendedDetailsContainer').append(extendedView);

        e.masterRow.data('detail-view', view);
    },

	view = BaseView.extend({

        collectionType: null,
	    
        filterView: null,
		showDeleteButton: false,
		showEditButton: false,
		showAddButton: false,
		tableName: null,
		editUrl: null,
		createNewItemTitle: 'add',
		editItemTitle: null,
	    gridSelector: '.grid',
	    filterSelector: '.filter',
	    addNewModelInline: false,
	    showEditLinkButton: true,

	    initDetailView: initDetailView,
        
	    excel: {
	        allPages: true
	    },
        
		toolbar: function () {

		    var self = this;

		    if (Application.canTableItemBeCreated(self.tableName)) {
		        var result = [
                    {
                        template: function () {
                            return self.addNewModelInline ? 
                                '<a class="k-button k-button-icontext k-grid-create-inline" href="#" data-localized="add"></a>' :
                                '<a class="k-button k-button-icontext" href="' + self.editUrl +
                                    '/create" data-localized="' + self.createNewItemTitle + '"></a>';
                        }
                    }
		        ];

		        return result;
		    }
		},

		events: {
		    'dblclick .k-grid tbody tr:not(k-detail-row) td:not(.k-hierarchy-cell,.k-detail-cell,.commands,.detail-view-grid-cell)': function (e) {
		        
		        var self = this,
		            dataItem = self.grid.dataItem(e.currentTarget.parentElement);

		        if (dataItem != undefined && dataItem.id != undefined &&
                    dataItem.id != 0) {
		            location.href = self.editUrl + '/' + dataItem.id;
		        }
			}
		},

		initialize: function () {

			view.__super__.initialize.apply(this, arguments);

			this.collection = new this.collectionType();
		},

		render: function () {

		    var self = this;
		    view.__super__.render.apply(self, arguments);

		    if (self.filterView != undefined) {
		        self.showView(new self.filterView({ grid: self.grid }),
                    self.filterSelector);
		    }

		    if (self.addNewModelInline) {
		        if (self.showDeleteButton)
		            self.showDeleteButton = Application.canTableItemBeDeleted(self.tableName);

		        if (self.showEditButton)
		            self.showEditButton = Application.canTableItemBeEdit(self.tableName);
		    }

		    return self;
		},

		detailTemplate: '<div class="extendedDetailsContainer"></div><div class="detailsContainer"></div>',
	});

	return view;
});