'use strict';

/**
 * @ngdoc overview
 * @name ospreyLauncherApp
 * @description
 * # ospreyLauncherApp
 *
 * Main module of the application.
 */
angular
  .module('ospreyLauncherApp', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngRoute',
    'ngSanitize'
  ])
  .config(function ($routeProvider) {
    $routeProvider
      .when('/', {
        templateUrl: 'views/main.html',
        controller: 'MainCtrl'
      })
      .when('/info', {
        templateUrl: 'views/info.html',
        controller: 'InfoCtrl'
      })
      .when('/loading', {
        templateUrl: 'views/loading.html',
        controller: 'LoadingCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  })
  .run(function($rootScope,$location) {
    frontend = $rootScope;
    $rootScope.backend = backend;

    $rootScope.setupInBackend = function(toSetup)
    {
      $rootScope.backend.addApplication(toSetup.name, toSetup.path, toSetup.suspendable, toSetup.keepOpen);
    };

    $rootScope.addRestriction = function(restrictionAppliesTo, restricted)
    {
      $rootScope.backend.addRestriction(restrictionAppliesTo.name,restricted.name);
    };


    $rootScope.makeCurrent = function(toMakeCurrent)
    {
      if(toMakeCurrent === undefined)
      {
        return;
      }
      if($rootScope.currentSelectable !== undefined)
      {
        $rootScope.currentSelectable.selected = false;
      }
      toMakeCurrent.selected = true;
      $rootScope.currentSelectable = toMakeCurrent;
    };

    $rootScope.reset = function()
    {
      $rootScope.$apply(function() { $location.path('/'); });
    }

    // == Setup data ==

    var x86system = false;

    // define applications
    var plex = {'title': 'Movies',
                'name' : 'Plex',
                'img' : 'images/plex.png',
                'suspendable' : true,
                'keepOpen' : false,
                'type' : 'launchable',
                'restriction' : 'Kodi'};
    var mediaportal = {'title':'TV',
                       'name' : 'MediaPortal',
                       'img' : 'images/mediaportal.png',
                       'suspendable' : true,
                       'keepOpen' : false,
                       'type' : 'launchable'};
    var kodi = {'title': 'Stream',
                'name' : 'Kodi',
                'img' : 'images/kodi.png',
                'suspendable' : true,
                'keepOpen' : false,
                'restriction' : 'Plex',
                'type' : 'launchable'};
    var desktop = {'title': 'Desktop',
                   'name' : 'Desktop',
                   'type' : 'selectable'};
    var info = {'title': 'Information',
                'name' : 'Info',
                'url'  : 'info',
                'type' : 'link'};
    var exit = {'title': 'Exit',
                'name' : 'exit',
                'type' : 'selectable'};

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
    $rootScope.applications = [plex,mediaportal,kodi];
    $rootScope.extras = [desktop,info,exit];
    
    $rootScope.makeCurrent(plex);
  });

var frontend;