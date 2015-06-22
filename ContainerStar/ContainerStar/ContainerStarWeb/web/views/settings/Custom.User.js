define([
	'lr!resources/Settings/Custom.User',
], function (Resources) {
    'use strict';

    var extraColumns = function () {
        return [
            {
                attributes: { 'class': 'change-password' },
                command: { name: 'change-password', text: Resources.changePassword }
            }
        ];
    };

    return extraColumns;
});