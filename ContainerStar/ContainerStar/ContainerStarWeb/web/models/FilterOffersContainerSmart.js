define(function () {
    'use strict';

    var model = Backbone.Model.extend({
        fields: {
            containerTypeId: { type: "number", editable: true, validation: { required: false, } },
            fromDate: { type: "date", editable: true, validation: { required: false, date: true } },
            toDate: { type: "date", editable: true, validation: { required: false, date: true } },
            name: { type: "string", editable: true, validation: { required: false, } },
            equipments: {},
        }
    });
    return model;
});