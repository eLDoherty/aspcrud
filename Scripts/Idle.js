var idleTime = 0

var logoutUrl = window.location.origin + "/Auth/Logout";

function timerIncrement() {
   // idleTime = idleTime + 1;
    if (idleTime == 1) { 
        alert("You are logging off");
        window.location.href = logoutUrl;
    }
}

jQuery(document).ready(function ($) {

    // Increment the idle time counter every minute.
    var idleInterval = setInterval(timerIncrement, 10000); // ms

    // Zero the idle timer on mouse movement.
    $(this).mousemove(function (e) {
        idleTime = 0;
    });
    $(this).keypress(function (e) {
        idleTime = 0;
    });

});


