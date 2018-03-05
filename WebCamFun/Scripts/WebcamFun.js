$(function () {
    var $msgContainer = $("#message");
    var $errContainer = $("#error");
 
    var dashboardHub = $.connection.dashboardHub;

    dashboardHub.client.fullscreenWebCam = function (camId, message) {
        var cam = document.getElementById(camId);
        if (screenfull.enabled) {
            screenfull.request(cam);
        }

        dashboardHub.client.showMesage(message);
    };

    dashboardHub.client.showMesage = function (message) {
        $msgContainer.append("<li><strong>" + htmlEncode(message) + "</strong></li>");
    };

    dashboardHub.client.showErrorMessage = function (error) {
        $errContainer.append("<li><strong>" + htmlEncode(error) + "</strong></li>");
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        console.log("started");
    }).fail(function (e) { dashboardHub.client.showErrorMessage(e); });


});

function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}