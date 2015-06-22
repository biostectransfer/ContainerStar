define([
	'base/base-object-add-view',
    
], function (BaseView ) {
    'use strict';

    var view = BaseView.extend({

        
        tableName: 'RolePermissionRsp',
        actionUrl: '#RolePermissionRsps',

		bindings: function () {

            var self = this;
            var result = {
			'#roleId': { observe: 'roleId',
				selectOptions: { labelPath: 'name', valuePath: 'id',
				collection: self.options.role
				,defaultOption: {label: self.resources.pleaseSelect,value: null}},},
			'#permissionId': { observe: 'permissionId',
				selectOptions: { labelPath: 'name', valuePath: 'id',
				collection: self.options.permission
				,defaultOption: {label: self.resources.pleaseSelect,value: null}},},
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'roleId', 'select');
			this.disableInput(this, 'permissionId', 'select');

            return this;
        }
    });

    return view;
});
