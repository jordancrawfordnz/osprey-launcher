'use strict';

angular.module('ospreyLauncherApp')
  .controller('InfoCtrl', ['$scope','$location','$rootScope',function($scope, $location,$rootScope) {

    $scope.goBack = function()
    {
    	$scope.$apply(function() { $location.path('/'); });
    };

   	$rootScope.moveLeft = function()
	{};
	
	$rootScope.moveRight = function()
	{};
	
	$rootScope.moveUp = function()
	{};
	
	$rootScope.moveDown = function()
	{};

	$rootScope.selectKey = function()
	{
		$scope.goBack();
	};

  }]);
