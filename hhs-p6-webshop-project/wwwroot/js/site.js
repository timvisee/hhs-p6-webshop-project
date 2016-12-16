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

    /**
     * Initialize some variables for the datepicker
     */
    var monthNames = ["JANUARI", "FEBRUARI", "MAART", "APRIL", "MEI", "JUNI", "JULI", "AUGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DECEMBER"],
    dateToday = new Date();

    $("#calendar").datepicker({
        prevText: "<",
        nextText: ">",
        onSelect: function (date) {
            var d = new Date(date);
            var inputDate = d.getDate() + '/' + (d.getMonth() + 1) + '/' + d.getFullYear();
            var showDate = d.getDate() + ' ' + monthNames[d.getMonth()] + ' ' + d.getFullYear();

            $('#date_input').val(inputDate);
            $(".selected-time").html(showDate);
        },
        minDate: dateToday
    });



});
