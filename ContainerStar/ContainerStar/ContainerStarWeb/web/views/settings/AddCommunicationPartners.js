define([
	'base/base-object-add-view',
    
], function (BaseView ) {
    'use strict';

    var view = BaseView.extend({

        
        tableName: 'CommunicationPartners',
        actionUrl: '#CommunicationPartners',

		bindings: function () {

            var self = this;
            var result = {
			'#name': 'name',
			'#firstName': 'firstName',
			'#customerId': 'customerId',
			'#phone': 'phone',
			'#mobile': 'mobile',
			'#fax': 'fax',
			'#email': 'email',
			};

            return result;
		},

        render: function () {

            view.__super__.render.apply(this, arguments);

			//TODO foreach model field
			this.disableInput(this, 'name');
			this.disableInput(this, 'firstName');
			this.disableInput(this, 'customerId', 'numeric');
			this.disableInput(this, 'phone');
			this.disableInput(this, 'mobile');
			this.disableInput(this, 'fax');
			this.disableInput(this, 'email');

            return this;
        }
    });

    return view;
});
