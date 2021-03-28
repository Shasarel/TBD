﻿var lockButtons = false;
var currentUrl = null;
var documentReady = false;
function changePage(url, buttonId, isBackButton = false) {
    if ((!lockButtons || isBackButton) && currentUrl !== url && documentReady) {
        lockButtons = true;
        currentUrl = url;
        setActiveTab(buttonId);
        var loader = true;
        $("#page-container").fadeOut(100,
            function() {
                if (loader)
                    $("#page-container")
                        .html(
                            '<div class="loader"><div class="yellow" ></div ><div class="red"></div><div class="blue"></div><div class="violet"></div></div>')
                        .fadeIn(100);
            });
        $.ajax({
            url: url,
            type: "GET",
            beforeSend: function(request) {
                request.setRequestHeader("NoLayout", "true");
            },
            success: function(data) {
                $("#page-container").fadeOut(100,
                    function() {
                        loader = true;
                        $("#page-container").html(data);
                        $("#page-container").fadeIn(100);
                        document.title = $("#title").html() + " - TBD";
                        if(!isBackButton)
                            history.pushState({ 'buttonId': buttonId, "url": url }, "", url);
                        lockButtons = false;
                    });
            }
        });
    }
}

function setActiveTab(buttonId) {
    $(".navbar-link-active").removeClass("navbar-link-active");
    $("#" + buttonId).addClass("navbar-link-active");
}

$(document).ready(function () {
    $(window).on({
        'load': function () {
            window.history.replaceState({ 'buttonId': $(".navbar-link-active").attr("id"), "url": document.URL }, "", document.URL);
            documentReady = true;
        },
        'popstate': function (e) {
            var oState = e.originalEvent.state;
            if (oState) {
                changePage(oState["url"], oState["buttonId"], true);
            }
        }
    });
});
