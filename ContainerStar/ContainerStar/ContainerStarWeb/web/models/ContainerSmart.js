define(function () {
    'use strict';

        var model = Backbone.Model.extend({
                urlRoot: 'api/ContainerSmart',
                fields: {
                    id: { type: "number", editable: false },
                    fromDate: {
                        type: "date",
                        editable: false
                    },
                    toDate: {
                        type: "date",
                        editable: false
                    },
                    number: {
                        type: "string",
                        editable: Application.canTableItemBeEdit('Containers', 'number'),
                        validation: { required: true, maxLength: 20 }
                    },
                    containerTypeId: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'containerTypeId'),
                        validation: { required: true }
                    },
                    length: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'length'),
                        validation: { required: true }
                    },
                    width: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'width'),
                        validation: { required: true }
                    },
                    height: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'height'),
                        validation: { required: true }
                    },
                    color: {
                        type: "string",
                        editable: Application.canTableItemBeEdit('Containers', 'color'),
                        validation: { required: true, maxLength: 50 }
                    },
                    price: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'price'),
                        validation: { required: true }
                    },
                    proceedsAccount: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'proceedsAccount'),
                        validation: { required: true }
                    },
                    isVirtual: {
                        type: "boolean",
                        editable: Application.canTableItemBeEdit('Containers', 'isVirtual'),
                        validation: { required: false }
                    },
                    manufactureDate: {
                        type: "date",
                        editable: Application.canTableItemBeEdit('Containers', 'manufactureDate'),
                        validation: { required: false, date: true }
                    },
                    boughtFrom: {
                        type: "string",
                        editable: Application.canTableItemBeEdit('Containers', 'boughtFrom'),
                        validation: { required: false, maxLength: 128 }
                    },
                    boughtPrice: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'boughtPrice'),
                        validation: { required: false }
                    },
                    comment: {
                        type: "string",
                        editable: Application.canTableItemBeEdit('Containers', 'comment'),
                        validation: { required: false, maxLength: 128 }
                    },
                    sellPrice: {
                        type: "number",
                        editable: Application.canTableItemBeEdit('Containers', 'sellPrice'),
                        validation: { required: true }
                    },
                    defaults: function() {
                        var dnf = new Date();
                        var dnt = new Date(2070, 11, 31);
                        return {
                            fromDate: dnf,
                            toDate: dnt
                        };
                    }
                }
            }
        );
    return model;
});
