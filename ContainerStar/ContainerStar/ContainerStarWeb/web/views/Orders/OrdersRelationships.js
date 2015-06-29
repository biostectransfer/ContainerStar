define([
	'base/base-object-tab-view',
    'kendo/kendo.tabstrip'
], function (BaseView) {
    'use strict';

    var view = BaseView.extend({

        tabs: function () {
            
            var result = [
                { view: 'l!t!Orders/RentPositions', selector: '.rentPositions' },
                { view: 'l!t!Orders/SalePositions', selector: '.salePositions' },
				                
            ];
            
            return result;
        }
    });

    return view;
});
