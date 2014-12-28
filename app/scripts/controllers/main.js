'use strict';

/**
 * @ngdoc function
 * @name ospreyLauncherApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the ospreyLauncherApp
 */
angular.module('ospreyLauncherApp')
  .controller('MainCtrl', ['$scope','$location',function($scope, $location) {

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
  		if($scope.current.link)
      {
        $scope.$apply(function() { $location.path('/' + $scope.current.name); });
      }
      else
      {
        // display loading screen
        backend.selectItem($scope.current.name);
      }
  	};

    $scope.setupInBackend = function(toSetup)
    {
      backend.addApplication(toSetup.name, toSetup.path, toSetup.suspendable, toSetup.keepOpen);
    };

    $scope.addRestriction = function(restrictionAppliesTo, restricted)
    {
      backend.addRestriction(restrictionAppliesTo.name,restricted.name);
    };

  	// == Construct ==

  	var x86system = true;

  	registerMovementNotifyee($scope); // allow key events to come through.

  	// define applications
  	var plex = {'title': 'Movies',
        				'name' : 'plex',
        				'img' : 'images/plex.png',
        				'suspendable' : true,
        				'keepOpen' : false,
                'restriction' : 'kodi'};
  	var mediaportal = {'title':'TV',
  						         'name' : 'mediaportal',
  						         'img' : 'images/mediaportal.png',
  						         'suspendable' : true,
		  				         'keepOpen' : false};
  	var kodi = {'title': 'Stream',
        				'name' : 'kodi',
        				'img' : 'images/kodi.png',
        				'suspendable' : true,
        				'keepOpen' : false,
                'restriction' : 'plex'};
  	var desktop = {'title': 'Desktop',
        					 'name' : 'desktop'};
    var info = {'title': 'Information',
                'name' : 'info',
                'link' : true};
  	var exit = {'title': 'Exit',
        				'name' : 'exit'};

  	if(x86system)
  	{
  		plex.path = 'C:\\Program Files\\Plex Home Theater\\Plex Home Theater.exe';
  		mediaportal.path = 'C:\\Program Files\\Team MediaPortal\\MediaPortal\\MediaPortal.exe';
  		kodi.path = 'C:\\Program Files\\XBMC\\XBMC.exe';
  	}
  	else
  	{
  		plex.path = 'C:\\Program Files (x86)\\Plex Home Theater\\Plex Home Theater.exe';
  		mediaportal.path = 'C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe';
  		kodi.path = 'C:\\Program Files (x86)\\XBMC\\XBMC.exe';
  	}
  	
  	// setup the selectables with the backend
    $scope.setupInBackend(plex);
    $scope.setupInBackend(mediaportal);
    $scope.setupInBackend(kodi);

  	backend.addDesktopLaunchable(desktop.name);
  	backend.addExitLaunchable(exit.name);

    // setup restrictions
    $scope.addRestriction(plex,kodi);
    $scope.addRestriction(kodi,plex);

  	// define relative applications
    plex.left = desktop;
    plex.right = mediaportal;

    mediaportal.left = plex;
    mediaportal.right = kodi;

    kodi.left = mediaportal;
    kodi.right = desktop;

    desktop.left = kodi;
    desktop.right = plex;
    desktop.up = exit;
    desktop.down = info;

    info.left = kodi;
    info.right = plex;
    info.up = desktop;
    info.down = exit;

    exit.left = kodi;
    exit.right = plex;
    exit.up = info;
    exit.down = desktop;

  	// define display order
	  $scope.applications = [plex,mediaportal,kodi];
	  $scope.extras = [desktop,info,exit];
    $scope.current = null;

  	// define the default option
  	$scope.makeCurrent(plex);
    
  }]);