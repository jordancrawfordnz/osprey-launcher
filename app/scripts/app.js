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
  .config(function ($routeProvider,$httpProvider) {
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

    var useMockBackend = false;

    if(useMockBackend)
    {
      $rootScope.backend = mockBackend;
    }
    else
    {
      $rootScope.backend = backend;
    }

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
    };

    // == Setup data ==


    $rootScope.launchableSelect = function()
    {
      $rootScope.backend.selectItem($rootScope.currentSelectable.name);
      setTimeout(function(){ $rootScope.$apply(function() { $location.path('/loading'); }); }, 100);
      // display loading screen, use a slight delay in-case it loads quickly.
    }

    $rootScope.selectableSelect = function()
    {
      $rootScope.backend.selectItem($rootScope.currentSelectable.name);
    }

    $rootScope.linkSelect = function()
    {
      $rootScope.$apply(function() { $location.path('/' + $rootScope.currentSelectable.url); });
    }

    // define applications
    var plex = {'title': 'Movies',
                'name' : 'Plex',
                'img' : 'images/plex.png',
                'suspendable' : true,
                'keepOpen' : false,
                'onSelect' : $rootScope.launchableSelect,
                'restriction' : 'Kodi'};
    var mediaportal = {'title':'TV',
                       'name' : 'MediaPortal',
                       'img' : 'images/mediaportal.png',
                       'suspendable' : true,
                       'keepOpen' : false,
                       'onSelect' : $rootScope.launchableSelect};
    var kodi = {'title': 'Stream',
                'name' : 'Kodi',
                'img' : 'images/kodi.png',
                'suspendable' : true,
                'keepOpen' : false,
                'restriction' : 'Plex',
                'onSelect' : $rootScope.launchableSelect};
    var desktop = {'title': 'Desktop',
                   'name' : 'Desktop',
                   'onSelect' : $rootScope.selectableSelect};
    var info = {'title': 'Information',
                'name' : 'Info',
                'url'  : 'info',
                'onSelect' : $rootScope.linkSelect};
    var exit = {'title': 'Exit',
                'name' : 'exit',
                'onSelect' : $rootScope.selectableSelect};

    if($rootScope.backend.isx86())
    {
      plex.path = 'C:\\Program Files\\Plex Home Theater\\Plex Home Theater.exe';
      mediaportal.path = 'C:\\Program Files\\Team MediaPortal\\MediaPortal\\MediaPortal.exe';
      kodi.path = 'C:\\Program Files\\XBMC\\XBMC.exe';
    }
    else
    {
      plex.path = 'C:\\Program Files (x86)\\Plex Home Theater\\Plex Home Theater.exe';
      mediaportal.path = 'C:\\Program Files (x86)\\Team MediaPortal\\MediaPortal\\MediaPortal.exe';
      kodi.path = 'C:\\Program Files (x86)\\Kodi\\Kodi.exe';
    }

    // setup the selectables with the backend
    $rootScope.setupInBackend(plex);
    $rootScope.setupInBackend(mediaportal);
    $rootScope.setupInBackend(kodi);

    $rootScope.backend.addDesktopLaunchable(desktop.name);
    $rootScope.backend.addExitLaunchable(exit.name);

    // setup restrictions
    $rootScope.addRestriction(plex,kodi);
    $rootScope.addRestriction(kodi,plex);

    $rootScope.backend.setupApplicationAutomaticClosing(120);

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