define(function () {
    'use strict';

    var model = Backbone.Model.extend({
        fields: {
            containerTypeId: { type: "int", editable: true },
            fromDate: { type: "date", editable: true, validation: { required: true, date: true } },
            toDate: { type: "date", editable: true, validation: { required: true, date: true } },

        }
    });
    return model;
});