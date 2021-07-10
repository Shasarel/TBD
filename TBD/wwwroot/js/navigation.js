var lockButtons = false;
var currentUrl = null;
var documentReady = false;
function changePage(url, buttonId=null, isBackButton = false) {
    if ((!lockButtons || isBackButton) && currentUrl !== url && documentReady) {
        lockButtons = true;
        currentUrl = url;
        setActiveTab(buttonId);
        var loader = true;
        if (typeof(interval) != "undefined") {
            clearInterval(interval);
        }
        $("#page-container").fadeOut(100,
            function() {
                if (loader)
                    $("#page-container")
                        .html(
                            '<div class="loader"><div class="loader-yellow" ></div ><div class="loader-red"></div><div class="loader-blue"></div><div class="loader-violet"></div></div>')
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
    if (buttonId != null) {
        $(".navbar-link-active").removeClass("navbar-link-active");
        $("#" + buttonId).addClass("navbar-link-active");
    }
}

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

var isScreenSmall = false;

$(".toogle-menu-button").click(function () {
    $(".topbar-button").not("#logoutTabButton").toggle(200);
});
$("#page-container, .topbar-button, #sidebar").click(function () {
    if (isScreenSmall) $(".topbar-button").not("#logoutTabButton").hide(200);
});

function manageTopbarSizeChanges(x) {
    if (x.matches) { 
        $(".topbar-button").not("#logoutTabButton").hide();
        isScreenSmall = true;
    } else {
        $(".topbar-button").not("#logoutTabButton").show();
        isScreenSmall = false;
    }
}

var x = window.matchMedia("(max-width: 670px)");
manageTopbarSizeChanges(x);
x.addListener(manageTopbarSizeChanges);

