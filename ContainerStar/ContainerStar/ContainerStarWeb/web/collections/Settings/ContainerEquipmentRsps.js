define([
	'base/base-collection',
	'models/Settings/ContainerEquipmentRsp'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/ContainerEquipmentRsps',
		model: Model
	});

	return collection;
});
