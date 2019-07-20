// Load feather-icons
feather.replace();

// On document ready
$(document).ready(function () {
    $(".markdown").find("h2,h3,h4,h5,h6").addClass("clickable-link")

    $(".clickable-link").click(function () {
        if (history.pushState) {
            history.pushState(null, null, "#" + $(this).attr("id"));
        }
        else {
            location.hash = $(this).attr("id");
        }
        $('html, body').animate({ scrollTop: $(this).position().top }, "slow");
    });
})