'use strict';

angular.module('ospreyLauncherApp')
  .controller('InfoCtrl', ['$scope','$location',function($scope, $location) {

    $scope.goBack = function()
    {
    	$scope.$apply(function() { $location.path('/'); });
    };

   	$scope.moveLeft = function()
	{
	};
	
	$scope.moveRight = function()
	{
	};
	
	$scope.moveUp = function()
	{
	};
	
	$scope.moveDown = function()
	{
	};

	$scope.selectKey = function()
	{
		$scope.goBack();
	};

    registerMovementNotifyee($scope); // allow key events to come through.

  }]);
