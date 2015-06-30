define([
	'base/base-object-add-view',
    
], function (BaseView ) {
    'use strict';

    var view = BaseView.extend({

        
        tableName: 'InvoicePositions',
        actionUrl: '#InvoicePositions',

		bindings: function () {

            var self = this;
            var result = {
			'#price': 'price',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field

            return this;
        }
    });

    return view;
});
