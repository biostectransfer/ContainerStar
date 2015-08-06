define([
	'base/base-collection',
	'models/Equipments'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/OrderContainerEquipments',
		model: Model
	});

	return collection;
});
