define([
	'base/base-collection',
	'models/Settings/CommunicationPartners'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: 'api/CommunicationPartners',
		model: Model
	});

	return collection;
});
