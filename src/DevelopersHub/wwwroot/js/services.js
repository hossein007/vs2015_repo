'use strict';
/* Services */
var hostAddress = 'http:\/\/localhost:5000\/';
var apiAddress = hostAddress+'api/';

var Services =
angular.module('Services', ['ngResource']);

Services.factory('DevelopersHubWebApi_Forum', function ($http) {
    return {
        getForumThread: function (id, callback, callback_error) {
            $http.get(`${apiAddress}PostForumThread?id=${id}`).success(callback).error(callback_error);
        }
            ,
    

        updateForumThread: function (forumThread, callback, callback_error) {

            $http.put(
                `${apiAddress}PostForumThread_Update`,
                forumThread
               
                ).success(callback).error(callback_error);

        }
        ,
        insertForumThread: function (forumThread, callback, callback_error) {

            $http.post(
                `${apiAddress}PostForumThread`,
                forumThread
               
                ).success(callback).error(callback_error);

            }


        }
    }
);

