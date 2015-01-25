'use strict';
var coreConfigData = function($rootScope)
{
	$rootScope.useMockBackend = false;
};

var setupConfigData = function($rootScope)
{
	
	// == Setup data ==

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

    if($rootScope.backend.isx86())
    {
      plex.path = 'C:\\Program Files\\Plex Home Theater\\Plex Home Theater.exe';
      mediaportal.path = 'C:\\Program Files\\Team MediaPortal\\MediaPortal\\MediaPortal.exe';
      kodi.path = 'C:\\Program Files\\Kodi\\Kodi.exe';
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

    // setup restrictions
    $rootScope.addRestriction(plex,kodi);
    $rootScope.addRestriction(kodi,plex);

    $rootScope.backend.setupApplicationAutomaticClosing(4*60);
    
    // define relative applications
    plex.left = desktop;
    plex.right = mediaportal;

    mediaportal.left = plex;
    mediaportal.right = kodi;

    kodi.left = mediaportal;
    kodi.right = desktop;

    desktop.left = kodi;
    desktop.right = plex;
    desktop.up = info;
    desktop.down = info;

    info.left = kodi;
    info.right = plex;
    info.up = desktop;
    info.down = desktop;

    // define display order
    $rootScope.applications = [plex,mediaportal,kodi];
    $rootScope.extras = [desktop,info];

    $rootScope.makeCurrent(plex);
};