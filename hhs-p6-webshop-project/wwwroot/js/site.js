// Run this code when the page is finished loading
$(document).ready(function () {

    // Toggle the search box when the search button is clicked
    $("#header_search_toggle").click(function () {
        // Make the search box and field visible
        $("#search_box").animate({ width: 'toggle' }, 350, function () {
            // Focus the search field
            $("#header_search").focus();
        });
    });

    //Scroll down on click
    $(".scrollToggle").click(function () {
        $("html, body").animate({ scrollTop: $(window).height()}, 1200, "easeInOutCubic");
    });

    function setHomeBannerHeight() {
        var scrHeight = $(window).height();
        var headerHeight = $("#header_top").height();

        console.log(scrHeight);
        console.log(headerHeight);

        $("#home_banner").height(scrHeight - headerHeight);
    }
    setHomeBannerHeight();

    $(window).resize(function () {
        var scrWidth = $(window).width();

        // Check for mobile devices
        if (scrWidth > 600) {
            setHomeBannerHeight();
        }
    });

    /**
     * Initialize some variables for the datepicker
     */
    var monthNames = ["JANUARI", "FEBRUARI", "MAART", "APRIL", "MEI", "JUNI", "JULI", "AUGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DECEMBER"],
    dateToday = new Date();

    $("#calendar").datepicker({
        prevText: "<",
        nextText: ">",
        firstDay: 1,
        minDate: dateToday,

        onSelect: function (date) {
            var d = new Date(date);
            var inputDate = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
            var showDate = d.getDate() + ' ' + monthNames[d.getMonth()] + ' ' + d.getFullYear();

            $(".toggle-time-date").removeClass("toggle-btn-disabled").attr("title", "");

            $('#date_input').val(inputDate);
            $(".selected-date").each(function () {
                $(this).html(showDate);
            });
        }
    });

    // Remove default date class
    $('.ui-state-active').removeClass('ui-state-active');

    $(".time-option").click(function () {
        console.log($(this).val());

        var currentValue = $('#date_input').val();
        console.log(currentValue + $(this).val());

        $(".selected-time").html(" OM " + $(this).val());


        // Add data to input box
        //$('#date_input').val(currentValue + "T" + $(this).val() + ":00");
    });

    $(".toggle-time-date").click(function () {
        if (! $(this).hasClass("toggle-btn-disabled")) {
            $("#go_to_second").show();
            $(".time-date-box").slideToggle(700, "easeInOutCubic");
        }
    });

    $("#go_to_second, #back_to_first").click(function () {
        $("#left_1, #left_2, #right_1, #right_2").toggle();
    });

    $("#go_to_third, #back_to_second").click(function () {
        $("#left_2, #left_3, #right_2, #right_3").toggle();
    });

});
