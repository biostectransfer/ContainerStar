define([
    'base-router',
    'router-masterdata'
], function (BaseRouter, MasterDataRouter) {
	'use strict';

	var router = Backbone.Router.extend({
		initialize: function (options) {
			var self = this;
			this.masterView = options.masterView;
			this.listenTo(Backbone, 'logged-in', function () {

				location.reload();
			});
			this.listenTo(Backbone, 'logged-out', function () {
				location.reload();
			});
			this.listenTo(Backbone, 'forbidden', function () {
			    BaseRouter.showView.call(self, self, 'l!t!errors/forbidden');
			});
		},

		routes: function() {

		    var commonRoutes =
            {
                'home': _.partial(BaseRouter.showView, this, 'l!t!home/home'),
                'Settings': _.partial(BaseRouter.showView, this, 'l!t!Settings/Settings'),
                'Offers': _.partial(BaseRouter.showView, this, 'l!t!Orders/Offers', { ContainerTypes: true, }, { isOffer: true }),
                'Offers/create': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddOrders', 'models/Orders', { CommunicationPartners: true }, { isOffer: true }),
                'Offers/:id': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddOrders', 'models/Orders', { CommunicationPartners: true }, { isOffer: true }),
                'Orders': _.partial(BaseRouter.showView, this, 'l!t!Orders/Orders', { ContainerTypes: true, }, { isOffer: false }),
                'Orders/create': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddOrders', 'models/Orders', { CommunicationPartners: true }, { isOffer: false }),
                'Orders/:id': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddOrders', 'models/Orders', { CommunicationPartners: true }, { isOffer: false }),
                'Positions': _.partial(BaseRouter.showView, this, 'l!t!Orders/Positions', false),
                'Positions/create': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddPositions', 'models/Positions', false),
                'Positions/:id': _.partial(BaseRouter.showViewWithModel, this, 'l!t!Orders/AddPositions', 'models/Positions', false),
            };
		    
		    var result = $.extend({}, commonRoutes, MasterDataRouter.getAllMasterDataRoutes(this));

		    return result;
		}		
	});

	return router;
});