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
      .when('/options', {
        templateUrl: 'views/options.html',
        controller: 'OptionsCtrl'
      })
      .otherwise({
        redirectTo: '/'
      });
  })
  .run(function($rootScope,$location) {
    frontend = $rootScope;

    $rootScope.launchableSelect = function()
    {
      $rootScope.backend.selectItem($rootScope.currentSelectable.name);
      setTimeout(function(){ $rootScope.$apply(function() { $location.path('/loading'); }); }, 100);
      // display loading screen, use a slight delay in-case it loads quickly.
    };

    $rootScope.selectableSelect = function()
    {
      $rootScope.backend.selectItem($rootScope.currentSelectable.name);
    };

    $rootScope.linkSelect = function()
    {
      $rootScope.$apply(function() { $location.path('/' + $rootScope.currentSelectable.url); });
    };

    $rootScope.optionsSelect = function()
    {
      $rootScope.$apply(function() { $location.path('/options'); });
    };


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

    coreConfigData($rootScope);

    if($rootScope.useMockBackend)
    {
      $rootScope.backend = mockBackend;
    }
    else
    {
      $rootScope.backend = backend;
    }

    $rootScope.exit = function()
    {
        $rootScope.backend.selectItem("exit");
    };
    $rootScope.backend.addExitLaunchable("exit");

    setupConfigData($rootScope);

  });