'use strict';

/**
 * @ngdoc function
 * @name ospreyLauncherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the ospreyLauncherApp
 */
angular.module('ospreyLauncherApp')
  .controller('MainCtrl', function ($scope) {
  	// == Setup functions ==

  	$scope.makeCurrent = function(toMakeCurrent)
  	{
		if($scope.current != null) $scope.current.selected = false;
		toMakeCurrent.selected = true;
		$scope.current = toMakeCurrent;
		$scope.$apply(); // update the changes on the interface
  	}

  	$scope.moveLeft = function()
	{
		$scope.makeCurrent($scope.current.left);
	};
	$scope.moveRight = function()
	{
		$scope.makeCurrent($scope.current.right);
	};
	$scope.moveUp = function()
	{
		$scope.makeCurrent($scope.current.up);
	};
	$scope.moveDown = function()
	{
		$scope.makeCurrent($scope.current.down);
	};
	$scope.selectKey = function()
	{
		alert('no function!');
	};

	// == Construct ==
	
  	registerMovementNotifyee($scope); // allow key events to come through.

  	// define applications
  	var plex = {'title': 'Movies','name' : 'plex', 'img' : 'images/plex.png', 'selected' : false};
  	var mediaportal = {'title': 'TV', 'name' : 'mediaportal', 'img' : 'images/mediaportal.png',  'selected' : false};
  	var kodi = {'title': 'Stream', 'name' : '', 'img' : 'images/kodi.png',  'selected' : false}

  	// define relative applications
  	plex.right = mediaportal;

  	mediaportal.left = plex;
  	mediaportal.right = kodi;

  	kodi.left = mediaportal;

  	// define display order
	$scope.applications = [plex,mediaportal,kodi];
  	$scope.current = null;

  	// define the default option
  	$scope.makeCurrent(plex);

    
  });
