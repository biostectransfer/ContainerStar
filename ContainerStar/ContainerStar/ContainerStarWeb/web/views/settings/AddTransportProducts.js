define([
	'base/base-object-add-view',
    'l!t!Settings/TransportProductsRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'TransportProducts',
        actionUrl: '#TransportProducts',

		bindings: function () {

            var self = this;
            var result = {
			'#name': 'name',
			'#description': 'description',
			'#price': 'price',
			'#proceedsAccount': 'proceedsAccount',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'name');
			this.disableInput(this, 'description');
			this.disableInput(this, 'price', 'numeric');
			this.disableInput(this, 'proceedsAccount', 'numeric');

            return this;
        }
		,events: {
		}
    });

    return view;
});
