var tableOffset = $("#timesheet-table").offset().top;
var $header = $("#timesheet-table > thead").clone();
var $fixedHeader = $("#header-fixed").append($header);

var onScroll = function () {
    var offset = $(this).scrollTop();

    if (offset >= tableOffset && $fixedHeader.is(":hidden")) {
        $fixedHeader.show();
    }
    else if (offset < tableOffset) {
        $fixedHeader.hide();
    }
};

var onResizeSticky = function () {
    if ($('.x-navigation-panel').position().top == 0) {
        $("#header-fixed").css('top', '60px');
    } else {
        $("#header-fixed").css('top', '0px');
    }
    $("#header-fixed").show();
    tableOffset = $("#timesheet-table").offset().top;
    var wi = $("#timesheet-table").outerWidth();
    $("#header-fixed").css({ 'width': wi + "px" });

    onScroll();
};

onResizeSticky();


$(window).bind("scroll", function () {
    onScroll();
});
