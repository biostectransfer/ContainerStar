define([
	'base/base-collection',
	'models/Settings/ContainerTypeEquipmentRsp'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/ContainerTypeEquipmentRsps',
		model: Model
	});

	return collection;
});
