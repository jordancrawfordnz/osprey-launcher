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

  	// $rootScope.selectKey = function()
  	// {
   //    $rootScope.currentSelectable.onSelect();
  	// };

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