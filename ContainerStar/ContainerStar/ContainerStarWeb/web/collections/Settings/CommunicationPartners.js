define([
	'base/base-collection',
	'models/Settings/CommunicationPartners'
], function (BaseCollection, Model) {
	'use strict';

	var collection = BaseCollection.extend({
	    url: Application.apiUrl + '/CommunicationPartners',
		model: Model
	});

	return collection;
});
