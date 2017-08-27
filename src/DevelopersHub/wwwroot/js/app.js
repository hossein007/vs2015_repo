'use strict';

/* App Module */

var developersHubApp = angular.module('developersHubApp', [
    'ngRoute',
    'Controllers',
    'Services'

]);


developersHubApp.config(['$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $routeProvider.
                when('/CreateProposal', {
                    templateUrl: '/partials/CreateProposal.html',
                    controller: 'CreateProposal'
                });

        $locationProvider.html5Mode(false).hashPrefix('!');
    }]);



