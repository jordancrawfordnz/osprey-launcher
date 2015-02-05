'use strict';
var coreConfigData = function($rootScope)
{
	$rootScope.useMockBackend = true;
};

var setupConfigData = function($rootScope)
{
	
	// == Setup data ==

    // define applications
    var plex = {'name' : 'Plex',
                'img' : 'images/plex.png',
                'suspendable' : true,
                'keepOpen' : false,
                'onSelect' : $rootScope.launchableSelect,
                'restriction' : 'Kodi'};
    var mediaportal = {'name' : 'MediaPortal',
                       'img' : 'images/mediaportal.png',
                       'suspendable' : true,
                       'keepOpen' : false,
                       'onSelect' : $rootScope.launchableSelect};
    var kodi = {'name' : 'Kodi',
                'img' : 'images/kodi.png',
                'suspendable' : true,
                'keepOpen' : false,
                'restriction' : 'Plex',
                'onSelect' : $rootScope.launchableSelect};
    var desktop = {'name' : 'Desktop',
                   'img' : 'images/windowsdesktop.png',
                   'onSelect' : $rootScope.selectableSelect};
    var info = {'name' : 'Information',
                'img' : 'images/info.png',
                'url'  : 'http://192.168.1.140',
                'onSelect' : $rootScope.launchableSelect};

    var tvnzOndemand = {'name' : 'TVNZ',
                        'url'  : 'http://tvnz.co.nz/video',
                        'img'  : 'images/tvnzod.png',
                        'onSelect' : $rootScope.launchableSelect};

    var threeNow = { 'name' : '3NOW',
                 'url'  : 'http://www.tv3.co.nz/OnDemand.aspx',
                 'img'  : 'images/3now.png',
                 'onSelect' : $rootScope.launchableSelect};

    var fourOnDemand = { 'name' : 'FOUR',
                 'url'  : 'http://www.four.co.nz/TV/OnDemand.aspx',
                 'img'  : 'images/fourondemand.png',
                 'onSelect' : $rootScope.launchableSelect};

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

    $rootScope.backend.addWebpageLaunchable(tvnzOndemand.name,tvnzOndemand.url, true);
    $rootScope.backend.addWebpageLaunchable(threeNow.name,threeNow.url, true);
    $rootScope.backend.addWebpageLaunchable(fourOnDemand.name,fourOnDemand.url, true);
    $rootScope.backend.addWebpageLaunchable(info.name,info.url, false);

    // setup restrictions
    $rootScope.addRestriction(plex,kodi);
    $rootScope.addRestriction(kodi,plex);

    $rootScope.backend.setupApplicationAutomaticClosing(4*60);
    
    // define display order
    $rootScope.applications = [plex, mediaportal, kodi, tvnzOndemand, threeNow, fourOnDemand, desktop, info];

    $rootScope.makeCurrent(plex);
};