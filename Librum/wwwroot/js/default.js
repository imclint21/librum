// Load feather-icons
feather.replace();

// On document ready
$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip()

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

$(window).scroll(function (event) {
    var scroll = $(window).scrollTop();
    if (scroll > 200) {
        $("#like-sidebar").fadeIn();
    }
    if (scroll < 100) {
        $("#like-sidebar").fadeOut();
    }
});

// When an article is liked
function Liked() {
    if ($("a").find(".fa-heart").hasClass("liked")) {
        $("a").find(".fa-heart").removeClass("liked");
    }
    else {
        $("a").find(".fa-heart").addClass("liked");
        Snackbar.show({ pos: "bottom-center", text: "Your like has been saved." });
    }
}

// When an article is bookmarked
function Bookmarked() {
    if ($("#like-sidebar a").find(".fa-bookmark").hasClass("far")) {
        Snackbar.show({ pos: "bottom-center", text: "Article successfully bookmarked." });
        $("#like-sidebar a").find(".fa-bookmark").removeClass("far").addClass("fas");
    }
    else {
        $("#like-sidebar a").find(".fa-bookmark").addClass("far").removeClass("fas");
    }
}