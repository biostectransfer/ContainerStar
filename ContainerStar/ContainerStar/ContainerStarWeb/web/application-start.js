define(['kendo/kendo.core'], function () {
    'use strict';

    require(['kendo/cultures/kendo.culture.de-DE'], function () {
        kendo.culture('de-DE');
    });

    $(document).ajaxError(function (event, xhr) {
        if (xhr.status == 401)
            location.reload();
        else if (xhr.status == 403)
            Backbone.trigger('forbidden');
    });
    
    require(['router', 't!l!master', 'master-footer', 'jqueryui'], function (Router, MasterView, MasterFooterView) {

        var masterView = new MasterView({ model: Application.user }),
			masterFooterView = new MasterFooterView(),
			router = new Router({ masterView: masterView });


        $('body').append(masterView.render().$el).tooltip({
        	show: { delay: 1000 },
        	hide: false,
            content: function () {
                var content = $(this).prop('title'),
					encodedContent = $('<div/>').text(content).html();

                encodedContent = encodedContent.replace(/\[br\]/g, function () { return '<br />'; });
                encodedContent = encodedContent.replace(/\[checkbox\]/g, function () { return '<input type="checkbox" checked />'; });

                return encodedContent;
            }
        });

        masterView.$el.prepend('<div style="height: 20px; position: relative;"></div>');
        masterView.$el.append('<div style="height: 20px; position: relative;"></div>');
        masterView.$el.find('#wrapperbody').css('min-height', Math.max(600, $(window).outerHeight() - 60));

        //$('body').append(masterFooterView.render().$el);

        masterView.listenTo(Backbone, 'router:navigate', function (route) {
            router.navigate(route, { trigger: true });
        });

        masterView.listenTo(Backbone, 'language:change', function () {
            localStorage.setItem('language', !Application.german);
            location.reload();
        });

        Backbone.history.start();

        if (!Backbone.history.fragment)
            router.navigate('home', { replace: true, trigger: true });

        router.on('router:view-created', function (view) {
            masterView.select(location.hash.split('/')[0]);
            masterView.showView(view, '.content');
        });
    });
});