define([
	'base/base-view',
    'lr!base/resources/base-grid-view',
	'lr!mixins/resources/kendo-validator-form',
	'kendo.backbone/backbone.datasource',
	'kendo.editors/editor-factory',
	'kendo.filters/filter-factory',
	'lk!kendo/kendo.grid'
], function (BaseView, Resources, ValidatorResources, DataSource, EditorFactory, FilterFactory) {
    'use strict';

    var processColumns = function (columns) {
        var self = this;

        _.each(columns, function (column) {
            if (_.isArray(column.columns)) {
                processColumns.call(self, column.columns);
                return;
            }

            var options = _.extend({
                dataTextField: 'name',
                dataValueField: 'id',
                gridSelector: self.gridSelector
            }, column),
            filterFactory = new FilterFactory(options),
			editorFactory = new EditorFactory(options);

            if (column.selectBox) {
                _.extend(column, {                    
                    template: _.partial(selectBoxColumnTemplate, options),
                    editor: _.bind(editorFactory.selectBox, editorFactory),
                    filterable: {
                        ui: _.bind(filterFactory.selectBox, filterFactory)
                    }
                });

                column.attributes = column.attributes || {};
                column.attributes['class'] = 'multi ' + (column.attributes['class'] ? column.attributes['class'] : '');
            }
            
            // Dropdown column
            if (column.collection && !column.selectBox) {

                var dropDownListCollection = column.collection;
                if (column.defaultText) {
                    var dropDownListCollection = column.collection.clone();
                    dropDownListCollection.unshift(new Backbone.Model({ id: '', name: column.defaultText }));
                }
                _.extend(column, {
                    editor: _.bind(editorFactory.dropDownList, editorFactory),
                    values: dropDownListCollection.map(function (item) {
                        return { text: item.get(options.dataTextField), value: item.get(options.dataValueField) };
                    })
                });
            }

            // Checkbox column
            if (column.checkbox) {

                var field = column.field,
                    headerAttributes = _.extend({ title: column.headerTitle }, column.headerAttributes);

                _.extend(column, {
                    template: _.partial(function (dataItem) {

                        var self = this,
                            checked = dataItem.get(field) ? ' checked="checked"' : '';

                        return '<input disabled type="checkbox"' + checked + ' />';
                    }),
                    filterable: false,
                    sortable: false,
                    width: '45px',
                    headerAttributes: headerAttributes,
                    headerTitle: null
                });
            }
        });

        if (this.allowToSelect) {
            columns.unshift({
                field: 'selected',
                title: '&nbsp;',
                filterable: false,
                sortable: false,
                template: '<input type="checkbox" data-model-id="#=id#" class="selected" #= typeof selected != "undefined" && selected ? checked="checked" : "" #/>',
                width: '20px',
            });
        }

        if (self.showEditButton || self.showDeleteButton) {
            columns.push({
                filterable: false,
                sortable: false,
                attributes: {
                    'class': 'commands'
                },
                template: function (dataItem) {
                    var result = '';

                    if (self.showEditButton)
                        result += '<span class="k-icon k-edit" title="' + Resources.edit + '"></span>';

                    if (self.showDeleteButton)
                        result += '&nbsp;<span class="k-icon k-delete" title="' + Resources.remove + '"></span>';

                    return result;
                }
            });
        }

        return columns;
    },
    selectBoxColumnTemplate = function (column, dataItem) {
        var items = _.map(column.collection.filter(function (collectionItem) {
            return _.contains(dataItem[column.field], collectionItem[column.dataValueField]);
        }), function (collectionItem) { return collectionItem.get(column.dataTextField) }),
		text = items.join(', '),
		title = items.join('[br] [checkbox] ').replace(/"/g, "&quot;");

        if (title)
            title = '[checkbox] ' + title;

        var result = '<span title="' + title + '">' + text + '</span>';

        return result;
    },
	selectItem = function (e) {
	    var $checkBox = $(e.target),
			modelId = $checkBox.data('model-id');

	    this.collection.get(modelId).set('selected', $checkBox.is(':checked'));
	},

	createPopup = function (e) {
	    e.preventDefault();

	    if (_.isFunction(this.addNewModel)) {
	    	this.addNewModel();
	    }
	    else if (this.addNewModelView) {
	            var grid = this.grid,
	        	model = new this.collection.model(),
	        	options = _.extend({ model: model, collection: this.collection }, this.options),
	        	view = new this.addNewModelView(options),
	        	collection = this.collection;

	            view.resources = _.extend({}, view.resources, this.resources);

	            this.listenTo(view, 'base-add-model-view:save', function (model) {
	                grid.dataSource.read();
	                grid.refresh();
	            });

	            this.addView(view);
	            this.$el.append(view.render().$el);
	    }

	},

    createNewModel = function (e) {
        
        e.preventDefault();
        this.grid.addRow();
    },

	//removeItems = function (e) {
	//    e.preventDefault();
	//    var self = this;
	//    if (confirm(Resources.removeConfirmation)) {
	//        self.collection.removeSelected().done(function () {
	//            self.grid.dataSource.read();
	//            self.grid.refresh();
	//        });
	//    }
	//},

    editRow = function (e) {
        this.grid.editRow($(e.target).closest('tr'));
    },

    saveRow = function (e) {
        this.grid.saveRow();
    },

    cancelRow = function (e) {

        this.grid.cancelRow();
    },

	removeRow = function (e) {

	    e.preventDefault();

	    var self = this;

	    require(['base/confirmation-view'], function (Confirmation) {

	        var confirmation = new Confirmation({
	            title: Resources.removeRecord,
	            message: Resources.removeConfirmation
	        });

	        self.listenToOnce(confirmation, 'continue', function () {
	            self.grid.removeRow($(e.target).closest('tr'));
	        });

	        self.addView(confirmation);
	        self.$el.append(confirmation.render().$el);
	    });
	},

	view = BaseView.extend({
	    gridSelector: null,

	    addNewModelView: null,
		addNewModel: null,
		addNewModelInline: null,
		addingInPopup: false,

		detailView: null,
		initDetailView: null,

	    pageSizes: [10, 20, 50, 100],
	    pageSize: 10,

	    selectable: false,

	    pageable: {
	        refresh: true,
	        pageSizes: self.pageSizes
	    },

	    sortable: true,
	    filterable: true,

	    columns: null,

	    showAddButton: true,
	    showDeleteButton: true,
	    showEditButton: true,

	    defaultSorting: null,
		defaultFitering: null,

	    allowToSelect: false,

	    remoteDataSource: true,

	    toolbar: function () {
	        var toolbar = [];

	        if (this.showAddButton) {
	            if (this.addingInPopup) {
	                toolbar.push({ name: 'create-popup', text: Resources.add });
	            }
	            else {
	                toolbar.push({ name: 'create-inline', text: Resources.add });
	            }
	        }
	        return toolbar.length ? toolbar : null;
	    },

        excel: null,

	    render: function () {
	        view.__super__.render.apply(this, arguments);

	        var self = this;


	        self.gridSelector = !self.gridSelector ? self.$el : self.$(self.gridSelector);

	        self.gridSelector.on('click.base-grid-view', '> table > tbody > tr > td > .selected', _.bind(selectItem, this));	        
	        self.gridSelector.on('click.base-grid-view', '> table > tbody > tr > td.commands > .k-edit', _.bind(editRow, this));
	        self.gridSelector.on('click.base-grid-view', '> table > tbody > tr > td.commands > .k-update', _.bind(saveRow, this));
	        self.gridSelector.on('click.base-grid-view', '> table > tbody > tr > td.commands > .k-cancel', _.bind(cancelRow, this));
	        self.gridSelector.on('click.base-grid-view', '> table > tbody > tr > td.commands > .k-delete', _.bind(removeRow, this));

	        if (self.addingInPopup) {
	            self.gridSelector.on('click.base-grid-view', '> .k-grid-toolbar > .k-grid-create-popup', _.bind(createPopup, this));
	        }
	        else {
	            self.gridSelector.on('click.base-grid-view', '> .k-grid-toolbar > .k-grid-create-inline', _.bind(createNewModel, this));
	        }

	        //this.$el.on('click.base-grid-view', '.k-grid-delete', _.bind(removeItems, this));
	        
            
	        var fields = self.collection.model.prototype.fields;

	        if (self.allowToSelect)
	            fields['selected'] = { type: 'boolean', editable: false };

	        var dataSource = new DataSource({
	            collection: self.collection,
	            schema: {
	                model: {
	                    id: self.collection.model.prototype.idAttribute,
	                    fields: fields
	                },
	                data: 'data',
	                total: 'total'
	            },
	            sort: _.result(self, 'defaultSorting'),
	            filter: _.result(self, 'defaultFiltering'),
	            pageSize: self.pageSize,
	            error: function (e) {
	                if (e.xhr.responseJSON.modelState) {
	                    self.modelState = e.xhr.responseJSON.modelState;
	                    $('.k-grid-edit-row').data('kendoValidator').validate();
	                    delete self.modelState;
	                }
	                else
	                    self.grid.cancelChanges();
	            },
	            remoteDataSource: self.remoteDataSource
	        });

	       
	        self.grid = self.gridSelector.kendoGrid({
	            dataSource: dataSource,
	            toolbar: _.result(self, 'toolbar'),
	            excel: self.excel,
	            columns: processColumns.call(self, _.result(self, 'columns')),
	            editable: {
	                mode: 'inline'
	            },
	            sortable: self.sortable,
	            filterable: self.filterable,
	            pageable: self.pageable,
	            selectable: self.selectable,
	            scrollable: false,
	            edit: function (e) {
	                e.container.find('> td.commands').html(
                        '<span class="k-icon k-update" title="' + Resources.saveChanges +
                        '"></span>&nbsp;<span class="k-icon k-cancel" title="' + Resources.cancelChanges + '"></span>');

					var fields = Object.getPrototypeOf(e.model).fields;
	                if (_.isObject(fields)) {
	                	_.each(fields, function (fieldProperties, fieldName) {
	                		if (fieldName) {
	                			var $elem = e.container.find('[name=' + fieldName + ']'),
									validation = fieldProperties.validation;

	                			if (validation && validation.date)
	                				$elem.attr('data-date', true);

	                			if (validation && validation.integer)
	                			    $elem.attr('data-integer', true);

	                			if (validation && validation.maxLength)
	                			    $elem.attr('data-maxlength', validation.maxLength);
	                		}
	                	});
	                }

	                var validator = e.container.data('kendoValidator'),
                        rules = validator.options.rules,
                        messages = validator.options.messages;

	                messages.required = ValidatorResources.required;
	                messages.date = ValidatorResources.date;
	                messages.integer = ValidatorResources.integer;
	                messages.maxLength = function (input) {

	                    var maxLength = Number(input.data('maxlength'));
	                    return ValidatorResources.maxLength.replace('%maxLength%', maxLength);
	                };

	                messages.modelState = function (input) {
	                	var message = self.modelState['model.' + input.attr('name')][0];
	                	if (self.resources && self.resources[message])
	                		message = self.resources[message];
	                	else if (validator.options.messages[message])
	                		message = self.validator.options.messages[message];

	                	return message;
	                };	              

	                rules.modelState = function (input) {
	                    return !self.modelState || !self.modelState['model.' + input.attr('name')];
	                };
	                rules.date = function (input) {
	                	if (input.data('date') === true) {
	                	    var val = input.val();
	                		if (val)
	                			return !!kendo.parseDate(input.val());
	                	}

	                	return true;
	                };
	                rules.integer = function (input) {
	                	if (input.data('integer') === true) {
	            			var val = input.val();
	            			if (val) {
	            				var parsed = kendo.parseFloat(val);
	            				return parsed === Math.ceil(parsed);
	            			}
	            		}

	            		return true;
	                },
	                rules.maxLength = function (input) {

	                    var maxLength = Number(input.data('maxlength'));
	                    if (!isNaN(maxLength)) {
	                        var val = input.val();

	                        return val.length <= maxLength;
	                    }

	                    return true;
	                }
	            },
	            dataBinding: function () {
	                // To destroy all child widgets
	                self.$('tbody tr').remove();
	            },
	            dataBound: function (e) {
	                self.dataBound(e);
	            },
	            change: function (e) {
	                self.change(e);
	            },	            
	            detailTemplate: !self.detailView ? null :
                    '<div class="extendedDetailsContainer"></div><div class="detailsContainer"></div>',
	            detailInit: !self.detailView ? null : function (e) {

	                if (self.initDetailView) {
	                    self.initDetailView(e);
	                }
	                else {
	                    var options = _.extend({}, self.options, { model: e.data }),
                            view = new self.detailView(options);

	                    self.addView(view);
	                    e.detailRow.find('.detailsContainer').append(view.render().$el);
	                    e.masterRow.data('detail-view', view);
	                }
	            },

	        }).data('kendoGrid');

	        if (self.$('.k-grid-pager').length)
	        	self.$('.k-grid-toolbar').insertAfter(self.$('.k-grid-pager'));
	        else
	        	self.$('.k-grid-toolbar').insertAfter(self.$('table'));

	        return this;
	    },

	    close: function () {
	        this.grid.destroy();

	        this.$el.off('.base-grid-view');

	        view.__super__.close.apply(this, arguments);
	    },

	    dataBound: function (e) {
	    },

	    change: function (e) {
          
	    }
	});

    return view;
});