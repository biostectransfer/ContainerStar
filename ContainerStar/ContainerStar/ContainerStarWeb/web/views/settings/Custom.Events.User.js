define([
	
], function () {
    'use strict';

    var events = {
        'click .change-password a': function (e) {
            e.preventDefault();

            var self = this,
                dataItem = self.grid.dataItem($(e.target).closest('tr'));

            require(['models/settings/user', 't!Settings/ChangePassword'], function (Model, View) {
                var view = new View({ model: new Model({ id: dataItem.id }) });

                self.listenTo(view, 'passwordChangedEvent', function (item) {
                    self.grid.dataSource.read();
                    self.grid.refresh();
                });

                self.addView(view);
                self.$el.append(view.render().$el);
            });            
        }
    };

    return events;
});