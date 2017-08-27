(function () {
    'use strict';

    angular.module('masterChefApp', [
        // Angular modules 
        'ngRoute',

        // Custom modules 
        'recipesService'
        // 3rd Party Modules

    ]);

    angular.module('masterChefApp').config(['$routeProvider', '$httpProvider', '$locationProvider', function ($routeProvider, $httpProvider, $locationProvider) {
        //disable http cache
        $httpProvider.defaults.cache = false;
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        $httpProvider.defaults.headers.get['If-Modified-Since'] = '0';
        //////////////////////////////////////////////////////////////////

        $routeProvider
        .when('/', {
            templateUrl: 'partials/recipes.html',
            controller: 'recipesController'
        })
        .when('/recipes/add', {
            templateUrl: 'partials/add.html',
            controller: 'recipesAddController'
        })
        .when('/recipes/edit/:id', {
            templateUrl: 'partials/edit.html',
            controller: 'recipesEditController'
        })
        .when('/recipes/delete/:id', {
            templateUrl: 'partials/delete.html',
            controller: 'recipesDeleteController'
        })
        .when('/recipes/addStep/:id', {
            templateUrl: 'partials/addStep.html',
            controller: 'recipesAddStepController'
        })
        .when('/recipes/editStep/:id', {
            templateUrl: 'partials/editStep.html',
            controller: 'recipesEditStepController'
        })
        .when('/recipes/deleteStep/:id', {
            templateUrl: 'partials/deleteStep.html',
            controller: 'recipesDeleteStepController'
        })
        .when('/recipes/addItem/:id', {
            templateUrl: 'partials/addItem.html',
            controller: 'recipesAddItemController'
        })
        .when('/recipes/editItem/:id', {
            templateUrl: 'partials/editItem.html',
            controller: 'recipesEditItemController'
        })
        .when('/recipes/deleteItem/:id', {
            templateUrl: 'partials/deleteItem.html',
            controller: 'recipesDeleteItemController'
        });

        $locationProvider.html5Mode(true);

    }]);
})();