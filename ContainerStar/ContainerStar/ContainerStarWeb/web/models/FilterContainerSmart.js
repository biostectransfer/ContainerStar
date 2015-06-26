define(function () {
    'use strict';

    var model = Backbone.Model.extend({
        fields: {
            containerTypeId: { type: "int", editable: true },
            fromDate: { type: "date", editable: true },
            toDate: { type: "date", editable: true },

        }
    });
    return model;
});