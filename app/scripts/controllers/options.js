'use strict';

angular.module('ospreyLauncherApp')
  .controller('OptionsCtrl', ['$scope','$rootScope',function($scope,$rootScope) {
   	$scope.options = [];

   	$scope.options.push({text : "Exit Launcher", action : $rootScope.exit});
   	$scope.options.push({text : "Force Close [App] (Placeholder)", action : $rootScope.reset});
   	$scope.options.push({text : "Keep [App] Open (Placeholder)", action : $rootScope.reset});
   	$scope.options.push({text : "Cancel", action : $rootScope.reset});
   	
   	function changeCurrent(changeBy)
   	{
   		var newIndex = currentSelected + changeBy;
   		if(newIndex < 0)
   		{
   			newIndex = ($scope.options.length - 1);
   		}
   		if(newIndex >= $scope.options.length)
   		{
   			newIndex = 0;
   		}
   		setCurrent(newIndex);
   	}

	var currentSelected = null;
   	function setCurrent(index)
   	{
   		if(currentSelected !== null)
   		{
   			$scope.options[currentSelected].selected = false;
   		}
   		currentSelected = index;
   		$scope.options[currentSelected].selected = true;
   		$scope.$apply();
   	}

   	setCurrent(0);

   	$rootScope.moveLeft = function()
	{};
	
	$rootScope.moveRight = function()
	{};
	
	$rootScope.moveUp = function()
	{
		changeCurrent(-1);
	};
	
	$rootScope.moveDown = function()
	{
		changeCurrent(1);
	};

	$rootScope.selectKey = function()
	{
		$scope.options[currentSelected].action();
	};

  }]);
