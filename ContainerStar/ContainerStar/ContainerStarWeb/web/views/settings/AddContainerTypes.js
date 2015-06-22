define([
	'base/base-object-add-view',
    'l!t!Settings/ContainerTypesRelationships'
], function (BaseView , TabView) {
    'use strict';

    var view = BaseView.extend({

        tabView: TabView,
        tableName: 'ContainerTypes',
        actionUrl: '#ContainerTypes',

		bindings: function () {

            var self = this;
            var result = {
			'#name': 'name',
			'#comment': 'comment',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'name');
			this.disableInput(this, 'comment');

            return this;
        }
		,events: {
		}
    });

    return view;
});
