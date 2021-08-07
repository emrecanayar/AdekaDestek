$(document).ready(function () {

    idleTimer = null;
    idleState = false;
    idleWait = 60000;

    $('*').bind('mousemove keydown scroll', function () {

        clearTimeout(idleTimer);

        if (idleState == true) {

            // Reactivated event
        }

        idleState = false;

        idleTimer = setTimeout(function () {

            // Idle Event
            window.location.assign("/Admin/Auth/Logout");

            idleState = true;
        }, idleWait);
    });

    $("body").trigger("mousemove");

});