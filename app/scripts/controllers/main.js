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
  		if(toMakeCurrent === undefined)
  		{
  			return;
  		}
  		if($scope.current !== null)
		{
			$scope.current.selected = false;
		}
		toMakeCurrent.selected = true;
		$scope.current = toMakeCurrent;
		$scope.$apply(); // update the changes on the interface
  	};

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
		backend.selectItem($scope.current.name);
	};

	// == Construct ==

  	registerMovementNotifyee($scope); // allow key events to come through.

  	// define applications
  	var plex = {'title': 'Movies',
  				'name' : 'plex',
  				'img' : 'images/plex.png',
  				'path' : 'C:\\Program Files (x86)\\Plex Home Theater\\Plex Home Theater.exe',
  				'suspendable' : true,
  				'keepOpen' : false,
  				'selected' : false};
  	var mediaportal = {'title':'TV',
  						'name' : 'mediaportal',
  						'img' : 'images/mediaportal.png',
  						'path' : 'C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe',
		  				'suspendable' : true,
		  				'keepOpen' : false,
  						'selected' : false};
  	var kodi = {'title': 'Stream',
  				'name' : 'kodi',
  				'img' : 'images/kodi.png',
  				'path' : 'C:\\Program Files (x86)\\XBMC\\XBMC.exe',
  				'suspendable' : true,
  				'keepOpen' : false,
  				'selected' : false};
  	var desktop = {'title': 'Desktop',
  					'name' : 'desktop',
  					'selected' : false};
  	var exit = {'title': 'Exit',
  				'name' : 'exit',
  				'selected' : false};
  	
  	// setup the selectables with the backend
  	backend.addApplication(plex.name, plex.path, plex.suspendable, plex.keepOpen);
  	backend.addApplication(mediaportal.name, mediaportal.path, mediaportal.suspendable, mediaportal.keepOpen);
  	backend.addApplication(kodi.name, kodi.path, kodi.suspendable, kodi.keepOpen);

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