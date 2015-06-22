define(['module'], function (module) {
	var masterConfig = module.config ? module.config() : {},
		load = function (kendoWidget, req, onLoad, config) {
		    var messages = 'lr!kendo/messages/kendo.messages';
		    
		    require([kendoWidget], function () {
		        require([messages], function (setMessages) {
		            setMessages();
		            onLoad();
		        });
			});
		};

	return {
		load: load
	};
})