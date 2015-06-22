define([
	'base/base-object-add-view',
    'l!t!Settings/EquipmentsRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'Equipments',
        actionUrl: '#Equipments',

		bindings: function () {

            var self = this;
            var result = {
			'#description': 'description',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'description');

            return this;
        }
		,events: {
		}
    });

    return view;
});
