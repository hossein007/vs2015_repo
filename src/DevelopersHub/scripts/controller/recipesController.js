(function () {
    'use strict';

    angular
        .module('masterChefApp')
        .controller('recipesController', recipesController)
        .controller('recipesAddController', recipesAddController)
        .controller('recipesEditController', recipesEditController)
        .controller('recipesDeleteController', recipesDeleteController)
        .controller('recipesAddStepController', recipesAddStepController)
        .controller('recipesEditStepController', recipesEditStepController)
        .controller('recipesDeleteStepController', recipesDeleteStepController)
        .controller('recipesAddItemController', recipesAddItemController)
        .controller('recipesEditItemController', recipesEditItemController)
        .controller('recipesDeleteItemController', recipesDeleteItemController);
     

    recipesController.$inject = ['$scope', 'Recipe'];

    function recipesController($scope, Recipe) {
        $scope.recipes = Recipe.query();
        $scope.expand = function (recipe) {
            recipe.show = !recipe.show;
        }
    }

    recipesAddController.$inject = ['$scope', 'Recipe', '$location'];
    function recipesAddController($scope, Recipe, $location) {
        $scope.recipe = new Recipe();
        $scope.addRecipe = function () {
            $scope.recipe.$save(function () {
                $location.path('/');
            });
        }
    }

    recipesEditController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesEditController($scope, Recipe, $location, $routeParams) {
        $scope.recipe = Recipe.get({ id: $routeParams.id });
        $scope.editRecipe = function () {
            $scope.recipe.$save(function () {
                $location.path('/');
            });
        }
    }

    recipesDeleteController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesDeleteController($scope, Recipe, $location, $routeParams) {
        $scope.recipe = Recipe.get({ id: $routeParams.id });
        $scope.deleteRecipe = function () {
            $scope.recipe.$remove({ id: $scope.recipe.id }, function () {
                $location.path('/');
            });
        };
    }

    recipesAddStepController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesAddStepController($scope, Recipe, $location, $routeParams) {
        $scope.recipeStep = new Recipe();
        $scope.recipeStep.parentId = $routeParams.id;
        $scope.addRecipeStep = function () {
            $scope.recipeStep.$saveRecipeStep(function () {
                $location.path('/');
            });
        };
    }

    recipesEditStepController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesEditStepController($scope, Recipe, $location, $routeParams) {
        $scope.recipeStep = Recipe.getRecipeStep({ id: $routeParams.id });
        $scope.editRecipeStep = function () {
            $scope.recipeStep.$saveRecipeStep(function () {
                $location.path('/');
            });
        };
    }

    recipesDeleteStepController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesDeleteStepController($scope, Recipe, $location, $routeParams) {
        $scope.recipeStep = Recipe.getRecipeStep({ id: $routeParams.id });
        $scope.deleteRecipeStep = function () {
            $scope.recipeStep.$removeRecipeStep({ id: $scope.recipeStep.id }, function () {
                $location.path('/');
            });
        };
    }

    recipesAddItemController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesAddItemController($scope, Recipe, $location, $routeParams) {
        $scope.recipeItem = new Recipe();
        $scope.recipeItem.parentId = $routeParams.id;
        $scope.addRecipeItem = function () {
            $scope.recipeItem.$saveRecipeItem(function () {
                $location.path('/');
            });
        };
    }

    recipesEditItemController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesEditItemController($scope, Recipe, $location, $routeParams) {
        $scope.recipeItem = Recipe.getRecipeItem({ id: $routeParams.id });
        $scope.editRecipeItem = function () {
            $scope.recipeItem.$saveRecipeItem(function () {
                $location.path('/');
            });
        };
    }

    recipesDeleteItemController.$inject = ['$scope', 'Recipe', '$location', '$routeParams'];
    function recipesDeleteItemController($scope, Recipe, $location, $routeParams) {
        $scope.recipeItem = Recipe.getRecipeItem({ id: $routeParams.id });
        $scope.deleteRecipeItem = function () {
            $scope.recipeItem.$removeRecipeItem({ id: $scope.recipeItem.id }, function () {
                $location.path('/');
            });
        };
    }
  
})();

