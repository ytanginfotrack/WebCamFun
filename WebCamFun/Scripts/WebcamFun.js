$(function () {
    var $msgContainer = $("#message");
    var $errContainer = $("#error");
 
    var dashboardHub = $.connection.dashboardHub;

    dashboardHub.client.switchCam = function (camId, message) {

        if (camId === "0") {
            $("video").removeClass("fullScreen");
            $("video").show();
            dashboardHub.client.showMesage(message);
            return;
        }

        var $cam = $("#cam" + camId);
        var $otherCams = $("video:not([id=\"cam" + camId + "\"])");
        if ($cam.length) {
            $otherCams.remove("fullScreen");
            $otherCams.hide();
            $cam.addClass("fullScreen");
            $cam.show();
            dashboardHub.client.showMesage(message);
        } else {
            $("video").removeClass("fullScreen").show();
            dashboardHub.client.showErrorMessage("Cam " + camId + " not found");
        }
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