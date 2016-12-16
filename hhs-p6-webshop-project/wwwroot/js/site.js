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
});
