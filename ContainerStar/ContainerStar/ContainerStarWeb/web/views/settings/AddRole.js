define([
	'base/base-object-add-view',
    'l!t!Settings/RoleRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'Role',
        actionUrl: '#Roles',

		bindings: function () {

            var self = this;
            var result = {
			'#name': 'name',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'name');

            return this;
        }
		,events: {
		}
    });

    return view;
});
