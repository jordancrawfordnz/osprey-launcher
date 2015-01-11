'use strict';

var frontend;
var mockBackend = {};
mockBackend.addApplication = function(name,path,suspendable,keepopen)
{
  return;
};

mockBackend.addDesktopLaunchable = function(name)
{
  return;
};

mockBackend.addExitLaunchable = function(name)
{
  return;
};

mockBackend.addRestriction = function(first,second)
{
  return;
};

mockBackend.isx86 = function()
{
  return false;
};

mockBackend.setupApplicationAutomaticClosing = function(timeDelay)
{ };

function checkKey(e) {

    e = e || window.event;

    if (e.keyCode === 38) {
      frontend.moveUp();
      // up arrow
    }
    else if (e.keyCode === 40) {
      frontend.moveDown();
      // down arrow
    }
    else if (e.keyCode === 37) {
      frontend.moveLeft();
      // left arrow
    }
    else if (e.keyCode === 39) {
      frontend.moveRight();
      // right arrow
    }
    else if (e.keyCode === 13) {
      frontend.selectKey();
      // enter key
    }
}

document.onkeydown = checkKey;

