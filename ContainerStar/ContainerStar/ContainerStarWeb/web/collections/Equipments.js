define([
	'base/base-collection',
	'models/Equipments'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/OrderContainerEquipments',
		model: Model
	});

	return collection;
});
