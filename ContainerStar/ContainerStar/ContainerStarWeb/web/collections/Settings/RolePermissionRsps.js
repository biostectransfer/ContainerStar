define([
	'base/base-collection',
	'models/Settings/RolePermissionRsp'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/RolePermissionRsps',
		model: Model
	});

	return collection;
});
