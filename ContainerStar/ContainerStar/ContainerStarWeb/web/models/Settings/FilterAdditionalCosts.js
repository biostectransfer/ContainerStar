define(function () {
    'use strict';

    var model = Backbone.Model.extend({
        fields: {
            name: { type: "string", editable: true },
        }
    });
    return model;
});
