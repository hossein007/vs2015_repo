(function () {
    'use strict';

    var recipesService = angular.module('recipesService', ['ngResource']);

    recipesService.factory('Recipe', ['$resource', function ($resource) {
        return $resource('/api/recipes/:id', {}, {
            getRecipeStep: { method: 'GET', url: '/api/recipes/step/:id' },
            saveRecipeStep: { method: 'POST', url: '/api/recipes/step' },
            removeRecipeStep: { method: 'DELETE', url: '/api/recipes/step/:id' },
            getRecipeItem: { method: 'GET', url: '/api/recipes/item/:id' },
            saveRecipeItem: { method: 'POST', url: '/api/recipes/item' },
            removeRecipeItem: { method: 'DELETE', url: '/api/recipes/item/:id' }
        });
    }]);

})();