'use strict';

/* Controllers */

var Controllers = angular.module('Controllers', []);



//Controllers.controller('Proposals', ['$scope', '$location', '$http', '$routeParams', 'DevelopersHubWebApi',
//    function RecipesList($scope, $location, $http, $routeParams, RecipesWebApi) {
//        $scope.message = "List of Recipes";
//        $scope.recipes = [];

//        $scope.deleteRecipe = function (id_) {

//            RecipesWebApi.delete({ id: id_ },
//     function success(response) {


//         var id = normalizeResponse(response.toJSON())["id"];

//         for (var i in $scope.recipes) {
//             if ($scope.recipes[i].Id == id) {
//                 {
//                     $scope.recipes.splice(i, 1);
//                     return;

//                 }

//             }
//         }




//         //$scope.blogEntry = response;
//     },
//     function error(errorResponse) {
//         console.log("Error:" + JSON.stringify(errorResponse));
//     }
//     );

//        }
//        ////
//        //$routeParams.id;
//        //$http.defaults.headers.common['Access-Control-Request-Headers'] = 'accept, authorization, content-type';
//        //$http.defaults.headers.common['Access-Control-Request-Method'] = 'GET'



//        RecipesWebApi.get({},
//       function success(response) {

//           var json = normalizeResponse(response.toJSON());

//           $scope.recipes = json;
//           //for (var ee in json) {
//           //    alert(ee + ' ' + json[ee]);
//           //}
//           //$scope.blogEntry = response;
//       },
//       function error(errorResponse) {
//           console.log("Error:" + JSON.stringify(errorResponse));
//       }
//       );



//    }


//        ////

//]);

//Controllers.controller('EditRecipe', ['$scope', '$location', '$http', '$routeParams', 'RecipesWebApi',
//    function ShowCtrl($scope, $location, $http, $routeParams, RecipesWebApi) {
//        $scope.message = "Edit recipe";

//        $scope.current_recipe = null;
//        var recipeId = $routeParams.id;
//        RecipesWebApi.get({ id: recipeId },
//        function success(response) {


//            $scope.current_recipe = normalizeResponse(response.toJSON());
//            //for (var ee in response.toJSON()) {
//            //    alert(ee + ' ' + response[ee]);
//            //}
//            //$scope.blogEntry = response;
//        },
//        function error(errorResponse) {
//            console.log("Error:" + JSON.stringify(errorResponse));
//        }
//        );

//        $scope.updateRecipe = function () {

//            $scope.updated_recipe = null;

//            RecipesWebApi.update($scope.current_recipe,
//            function success(response) {

//                $scope.current_recipe = normalizeResponse(response.toJSON());

//                $scope.actionmessage = 'Your receipt info was updated.';
//                //for (var ee in response.toJSON()) {
//                //    alert(ee + ' ' + response[ee]);
//                //}
//                //$scope.blogEntry = response;
//            },
//            function error(errorResponse) {
//                console.log("Error:" + JSON.stringify(errorResponse));
//            }
//            );

//        }

//        $scope.newIngredient = function () {
//            $scope.current_recipe.Ingredients[$scope.current_recipe.Ingredients.length] =
//                 { Id: -1, Text: '' };

//        }
//        $scope.deleteIngredient = function (id) {

//            for (var i in $scope.current_recipe.Ingredients) {
//                if ($scope.current_recipe.Ingredients[i].Id == id) {
//                    {
//                        $scope.current_recipe.Ingredients.splice(i, 1);
//                        return;

//                    }

//                }
//            }
//        }
//        $scope.newStep = function () {
//            $scope.current_recipe.Steps[$scope.current_recipe.Steps.length] =
//                 { Id: -1, Text: '' };

//        }
//        $scope.deleteStep = function (id) {

//            for (var i in $scope.current_recipe.Steps) {
//                if ($scope.current_recipe.Steps[i].Id == id) {
//                    {
//                        $scope.current_recipe.Steps.splice(i, 1);
//                        return;

//                    }

//                }
//            }
//        }

//        $scope.uploadFile = function (files, fn, id) {
//            var fd = new FormData();
//            //Take the first selected file
//            fd.append("file", files[0]);
//            fd.append("fn", fn);
//            fd.append("id", id);
//            var _id_ = id;
//            $http.post(apiAddress + "File", fd, {
//                withCredentials: true,
//                headers: { 'Content-Type': undefined },
//                transformRequest: angular.identity
//            }).success(function (response) { var obj = normalizeResponse(response); $scope.current_recipe.Photo = obj['fn']; }).error(function () { alert('pp'); });

//        };

//    }]);


Controllers.controller('CreateProposal', ['$scope', '$location', '$http', '$routeParams', 'DevelopersHubWebApi_Proposal',
    function CreateProposal($scope, $location, $http, $routeParams, DevelopersHubWebApi_Proposal) {
        
      


        $scope.init = function (id) {
            //This function is sort of private constructor for controller
            $scope.memberId = id;
            $scope.actionmessage = '';
            //Based on passed argument you can make a call to resource
            //and initialize more objects
            //$resource.getMeBond(007)

           


        };

        alert('2');

        var obj = {};

        $scope.memberId = 1;

        //alert($scope.memberId);
        if ($scope.memberId) {
            obj.id = $scope.memberId;
            obj.FilterType = "member";
            DevelopersHubWebApi_Proposal.get(obj,
                    function success(response) {

                        $scope.proposals =
                      response;

                    });
        }


        
        $scope.message = "Create Proposal";
        $scope.memberId = 1;
        $scope.new_proposal =
               {
                   "Id": -1,
                   "Title": "",
                   "Description": "ggg",
                   "Technologies": ""
               };
        var validateForm = function()
        {
            var __valid = true;
            if($scope.new_proposal.Title=='')
            {
                __valid = false;
            }
            if ($scope.new_proposal.Describtion == '') {
                __valid = false;
            }
            if ($scope.new_proposal.Technologies == '') {
                __valid = false;
            }

            return __valid;

        }
        $scope.newProposal = function () {
         
            $scope.new_proposal.mid = $scope.memberId;
            if (validateForm()) {



                DevelopersHubWebApi.save($scope.new_proposal,
                function success(response) {

                    $scope.new_proposal =
                  {
                      "Id": -1,
                      "Title": "",
                      "Description": "mmm",
                      "Technologies": ""
                  };


                    $('.alert').removeClass('hide').removeClass('alert-danger').addClass('alert-success');

                    $scope.actionmessage = 'Your new proposal was added.';
                    //for (var ee in response.toJSON()) {
                    //    alert(ee + ' ' + response[ee]);
                    //}
                    //$scope.blogEntry = response;
                },
                function error(errorResponse) {
                    console.log("Error:" + JSON.stringify(errorResponse));
                }
                );

            }
            else
            {
                $('.alert').removeClass('hide').addClass('alert-danger').removeClass('alert-success');

                $scope.actionmessage = 'Please complete the form';


            }
        }
       


    }]);





//Controllers.controller('ViewRecipe', ['$scope', '$location', '$http', '$routeParams', 'RecipesWebApi',
//    function ShowCtrl($scope, $location, $http, $routeParams, RecipesWebApi) {
//        $scope.message = "Recipe";

//        $scope.current_recipe = null;
//        var recipeId = $routeParams.id;
//        RecipesWebApi.get({ id: recipeId },
//        function success(response) {

//            alert('00');
//            $scope.current_recipe = normalizeResponse(response.toJSON());

//            //for (var ee in response.toJSON()) {
//            //    alert(ee + ' ' + response[ee]);
//            //}
//            //$scope.blogEntry = response;
//        },
//        function error(errorResponse) {
//            console.log("Error:" + JSON.stringify(errorResponse));
//        }
//        );



//    }]);