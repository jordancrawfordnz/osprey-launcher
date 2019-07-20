'use strict';

angular.module('ospreyLauncherApp')
  .controller('LoadingCtrl', ['$scope','$rootScope',function($scope,$rootScope) {
   	$rootScope.moveLeft = function()
	{};
	
	$rootScope.moveRight = function()
	{};
	
	$rootScope.moveUp = function()
	{};
	
	$rootScope.moveDown = function()
	{};

	$rootScope.selectKey = function()
	{};
	
	$rootScope.onReset = function()
    {};

	$scope.getLaunchable = function()
	{
		return $rootScope.currentSelectable;
	};

  }]);
