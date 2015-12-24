'use strict';
var coreConfigData = function($rootScope)
{
	$rootScope.useMockBackend = false;
};

var setupConfigData = function($rootScope)
{
	
	// == Setup data ==

    // define launchables
    var plex = {'name' : 'Plex',
                'img' : 'images/plex.png',
                'suspendable' : true,
                'keepOpen' : false,
                'restriction' : 'Kodi'};
    var kodi = {'name' : 'Kodi',
                'img' : 'images/kodi.png',
                'suspendable' : true,
                'keepOpen' : true,
                'restriction' : 'Plex'};
    var desktop = {'name' : 'Desktop',
                   'img' : 'images/windowsdesktop.png'};
    var info = {'name' : 'Information',
                'img' : 'images/info.png',
                'url'  : 'http://192.168.1.140'};

    var youTube = {'name' : 'YouTube',
                   'url'  : 'https://youtube.com',
                   'img'  : 'images/youtube.png'};

    var spotify = {'name' : 'Spotify',
                   'url'  : 'https://play.spotify.com/',
                   'img'  : 'images/spotify.png'};

    var tvnzOndemand = {'name' : 'TVNZ',
                        'url'  : 'http://tvnz.co.nz/video',
                        'img'  : 'images/tvnzod.png'};

    var threeNow = { 'name' : '3NOW',
                 'url'  : 'http://www.tv3.co.nz/OnDemand.aspx',
                 'img'  : 'images/3now.png'};

    var fourOnDemand = { 'name' : 'FOUR',
                 'url'  : 'http://www.four.co.nz/TV/OnDemand.aspx',
                 'img'  : 'images/fourondemand.png'};

    if($rootScope.backend.isx86())
    {
      plex.path = 'C:\\Program Files\\Plex Home Theater\\Plex Home Theater.exe';
      kodi.path = 'C:\\Program Files\\Kodi\\Kodi.exe';
    }
    else
    {
      plex.path = 'C:\\Program Files (x86)\\Plex Home Theater\\Plex Home Theater.exe';
      kodi.path = 'C:\\Program Files (x86)\\Kodi\\Kodi.exe';
    }

    // setup the selectables with the backend
    $rootScope.setupInBackend(plex);
    $rootScope.setupInBackend(kodi);

    $rootScope.backend.addDesktopLaunchable(desktop.name);

    $rootScope.backend.addWebpageLaunchable(youTube.name,youTube.url, true);
    $rootScope.backend.addWebpageLaunchable(spotify.name,spotify.url, true);
    $rootScope.backend.addWebpageLaunchable(tvnzOndemand.name,tvnzOndemand.url, true);
    $rootScope.backend.addWebpageLaunchable(threeNow.name,threeNow.url, true);
    $rootScope.backend.addWebpageLaunchable(fourOnDemand.name,fourOnDemand.url, true);
    $rootScope.backend.addWebpageLaunchable(info.name,info.url, false);

    // setup restrictions
    $rootScope.addRestriction(plex,kodi);
    $rootScope.addRestriction(kodi,plex);

    $rootScope.backend.setupApplicationAutomaticClosing(4*60);
    
    // define display order
    $rootScope.launchables = [plex, kodi, desktop, youTube, spotify, tvnzOndemand, threeNow, fourOnDemand, info];

    $rootScope.makeCurrent($rootScope.launchables[0]);
};