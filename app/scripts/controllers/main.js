'use strict';
/**
 * @ngdoc function
 * @name ospreyLauncherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the ospreyLauncherApp
 */
angular.module('ospreyLauncherApp')
  .controller('MainCtrl', ['$scope','$location','$rootScope',function($scope, $location,$rootScope) {

  	// == Setup buttons ==

    $rootScope.moveLeft = function()
  	{
  		$rootScope.makeCurrent($rootScope.currentSelectable.left);
      $scope.$apply();
  	};
  	
    $rootScope.moveRight = function()
  	{
  		$rootScope.makeCurrent($rootScope.currentSelectable.right);
      $scope.$apply();
  	};
  	
    $rootScope.moveUp = function()
  	{
  		$rootScope.makeCurrent($rootScope.currentSelectable.up);
      $scope.$apply();
  	};
  	
    $rootScope.moveDown = function()
  	{
  		$rootScope.makeCurrent($rootScope.currentSelectable.down);
      $scope.$apply();
  	};

  	$rootScope.selectKey = function()
  	{
      switch($rootScope.currentSelectable.type)
      {
        case 'launchable':
        {
          $rootScope.backend.selectItem($rootScope.currentSelectable.name);
          setTimeout(function(){ $scope.$apply(function() { $location.path('/loading'); }); }, 100);
          // display loading screen, use a slight delay in-case it loads quickly.
          break;
        }
        case 'selectable':
        {
          $rootScope.backend.selectItem($rootScope.currentSelectable.name);
          break;
        }
        case 'link':
        {
          $scope.$apply(function() { $location.path('/' + $rootScope.currentSelectable.url); });
          break;
        }
      }
  	};

    // == Allow grabbing data ==

    $scope.getApplications = function()
    {
      return $rootScope.applications;
    };

    $scope.getExtras = function()
    {
      return $rootScope.extras;
    };
    
  }]);