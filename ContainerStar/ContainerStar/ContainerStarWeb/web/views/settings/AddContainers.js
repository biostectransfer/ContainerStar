define([
	'base/base-object-add-view',
    'l!t!Settings/ContainersRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'Containers',
        actionUrl: '#Containers',

		bindings: function () {

            var self = this;
            var result = {
			'#number': 'number',
			'#containerTypeId': { observe: 'containerTypeId',
				selectOptions: { labelPath: 'name', valuePath: 'id',
				collection: self.options.containerTypes
				,defaultOption: {label: self.resources.pleaseSelect,value: null}},},
			'#length': 'length',
			'#width': 'width',
			'#height': 'height',
			'#color': 'color',
			'#price': 'price',
			'#proceedsAccount': 'proceedsAccount',
			'#isVirtual': 'isVirtual',
			'#manufactureDate': 'manufactureDate',
			'#boughtFrom': 'boughtFrom',
			'#boughtPrice': 'boughtPrice',
			'#comment': 'comment',
			'#sellPrice': 'sellPrice',
			'#isSold': 'isSold',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'number');
			this.disableInput(this, 'containerTypeId', 'select');
			this.disableInput(this, 'length', 'numeric');
			this.disableInput(this, 'width', 'numeric');
			this.disableInput(this, 'height', 'numeric');
			this.disableInput(this, 'color');
			this.disableInput(this, 'price', 'numeric');
			this.disableInput(this, 'proceedsAccount', 'numeric');
			this.disableInput(this, 'isVirtual');
			this.disableInput(this, 'manufactureDate', 'date');
			this.disableInput(this, 'boughtFrom');
			this.disableInput(this, 'boughtPrice', 'numeric');
			this.disableInput(this, 'comment');
			this.disableInput(this, 'sellPrice', 'numeric');
			this.disableInput(this, 'isSold');

            return this;
        }
		,events: {
		}
    });

    return view;
});
