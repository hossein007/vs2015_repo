'use strict';

/* Controllers */

var Controllers = angular.module('Controllers', []);




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



                DevelopersHubWebApi_Proposal.save($scope.new_proposal,
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


Controllers.controller('PostForumThread', ['$scope', '$location', '$http', '$routeParams','$window', 'DevelopersHubWebApi_Forum',
    function PostForumThread($scope, $location, $http, $routeParams, $window, DevelopersHubWebApi_Forum) {




        $scope.init = function () {
            //This function is sort of private constructor for controller
            $scope.memberId = $window.memberId;
            $scope.forumId = $window.forumId;
            $scope.forumThreadId = $window.forumThreadId;
            $scope.toForumThreadId = $window.toForumThreadId;
            $scope.actionmessage = '';
            
            $scope.mode = '';

            var obj = {};

            if ($scope.memberId && $scope.forumThreadId>0) {
                var entry = DevelopersHubWebApi_Forum.getForumThread(
                    $scope.forumThreadId,
                        function (response) {

                         
                            
                            $scope.forumThread = {};
                            $scope.Id = response.id;
                            $scope.memberId = response.forumThread.mid;
                            $scope.forumId = response.forumThread.fid;
                            $scope.forumThreadId = response.forumThread.id;

                            $scope.forum = response.forum;

                            $scope.member = response.member;


                            $scope.forumThread.Text = response.forumThread.text;

                            $scope.mode = 'edit';

                            
                            console.log(response);
                            

                        },
                        function (response) {


                            console.log(arguments[0]);
                            console.log(arguments[1]);
                            console.log(arguments[2]);
                            console.log(arguments[3]);

                        }
                        
                        );
            }
            else if($scope.memberId && $scope.toForumThreadId>0)
            {
                var entry = DevelopersHubWebApi_Forum.getForumThread(
                    toForumThreadId,
                        function (response) {



                            $scope.forumThread = {};
                            $scope.to_forumThread = {};
                            $scope.Id = response.forumThread.id;
                            $scope.memberId = response.forumThread.mid;
                            $scope.forumId = response.forumThread.fid;
                            $scope.forumThreadId = response.forumThread.ftid;

                           
                            $scope.to_forumThread.Text = response.forumThread.text;

                            $scope.forumThread.Text = '';//response.forumThread.text;

                            $scope.forum = response.forum;

                            $scope.member = response.member;

                            $scope.mode = 'create';

                            console.log(response);
                            


                        },
                        function (response) {


                            console.log(arguments[0]);
                            console.log(arguments[1]);
                            console.log(arguments[2]);
                            console.log(arguments[3]);

                        }

                        );

            }
        };

        $scope.message = "Create Forum Thread";
        $scope.new_forumThread =
               {
                   "Id": -1,
                   "Text": ""
               };
        var validateForm = function () {
            var __valid = true;
            if ($scope.mode=='edit'&&$scope.forumThread.Text == '') {
                __valid = false;
            }
            if ($scope.mode == 'create' && $scope.to_forumThread.Text == '') {
                __valid = false;
            }
            return __valid;

        }

        $scope.handleSubmit = function()
        {
            if ($scope.toForumThreadId>0)
            {
                alert('new');
                $scope.newForumThread();
            }
            else if ($scope.forumThreadId > 0)
            {
                $scope.updateForumThread();
                alert('update');
            }


        }

        $scope.newForumThread = function () {

            $scope.new_forumThread.mid = $scope.memberId;
            $scope.new_forumThread.fid = $scope.forumId;
            $scope.new_forumThread.ftid = $scope.toForumThreadId;
            $scope.new_forumThread.Text = $scope.forumThread.Text;



            if (validateForm()) {



                DevelopersHubWebApi_Forum.insertForumThread($scope.new_forumThread,
                function success(response) {


                    var __newId = response.id;
                    $scope.new_forumThread =
                  {
                      "Id": -1,
                      "Text": ""
                  };


                    $('.alert').removeClass('hide').removeClass('alert-danger').addClass('alert-success');


                    $window.location.href =
                        hostAddress+
                        `Forums/PostForumThread?ForumId=${$scope.forumId}&ForumThreadId=${__newId}#!/PostForumThread`;

                    $scope.actionmessage = 'Your new forum thread was added.';



                    
                },
                function error(errorResponse) {
                    console.log("Error:" + JSON.stringify(errorResponse));
                }
                );

            }
            else {
                $('.alert').removeClass('hide').addClass('alert-danger').removeClass('alert-success');

                $scope.actionmessage = 'Please complete the form';


            }
        }


        $scope.updateForumThread = function () {

            var forumThread = {};
            //forumThread.Id = $scope.forumThreadId;
            forumThread.mid = $scope.memberId;
            forumThread.fid = $scope.forumId;
            forumThread.ftid = $scope.forumThreadId;
            forumThread.Text = $scope.forumThread.Text;
            if (validateForm()) {

                if ($scope.forumThreadId > 0) {
                    DevelopersHubWebApi_Forum.updateForumThread(forumThread,
                    function success(response) {




                        $('.alert').removeClass('hide').removeClass('alert-danger').addClass('alert-success');
                        $scope.actionmessage = 'Your forum thread was saved.';

                    },
                    function error(errorResponse) {
                        console.log("Error:" + JSON.stringify(errorResponse));
                    }
                    );

                }
                else
                {
                    alert('insert');
                    $scope.newForumThread();

                }

            }
            else {
                $('.alert').removeClass('hide').addClass('alert-danger').removeClass('alert-success');

                $scope.actionmessage = 'Please complete the form';


            }
        }


        
    }]);

