'use strict';
/* Services */
var apiAddress = 'http:\/\/localhost:5000\/api/';

var Services =
angular.module('Services', ['ngResource']);
Services.service('DevelopersHubWebApi_Proposal', ['$resource',
function ($resource) {
    return $resource(apiAddress + "Proposal", {}, {
        get: { method: 'GET', cache: false, isArray: false, transformResponse: [] },
        save: { method: 'POST', cache: false, isArray: false },
        update: { method: 'PUT', cache: false, isArray: false },
        delete: { method: 'DELETE', cache: false, isArray: false }
    });
}]);




