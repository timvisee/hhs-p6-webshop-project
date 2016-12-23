/**
 * List of month names.
 * @const
 */
var MONTH_NAMES = ["JANUARI", "FEBRUARI", "MAART", "APRIL", "MEI", "JUNI", "JULI", "AUGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER", "DECEMBER"];

/**
 * List of dates that are unavailable/occupied.
 */
var unavailableDates = null;

/**
 * Unique ID index.
 */
var uniqueIdIndex = 0;

/**
 * Get an unique ID.
 */
function getUniqueId(prefix) {
    // Set the default prefix
    if(prefix == undefined)
        prefix = "unique-id";

    // Return an unique ID
    return prefix + "-" + uniqueIdIndex++;
}

// Run this code when the page is finished loading
$(document).ready(function () {

    /**
     * Toggle the search box when the search button is clicked
     */
    $("#header_search_toggle").click(function () {
        // Make the search box and field visible
        $("#search_box").animate({ width: 'toggle' }, 350, function () {
            // Focus the search field
            $("#header_search").focus();
        });
    });

    /**
     * Scroll down when you click the scroll down button
     */
    $(".scrollToggle").click(function () {
        $("html, body").animate({ scrollTop: $(window).height() }, 1200, "easeInOutCubic");
    });

    /**
     * Function for changing the height of the home banner
     */
    function setHomeBannerHeight() {
        var scrHeight = $(window).height();
        var headerHeight = $("#header_top").height();

        $("#home_banner").height(scrHeight - headerHeight);
    }
    setHomeBannerHeight();

    /**
     * Resize the height of the banner when you resize your window
     */
    $(window).resize(function () {
        var scrWidth = $(window).width();

        // Check for mobile devices
        if (scrWidth > 600) {
            setHomeBannerHeight();
        }
    });

    $(window).scroll(function (event) {
        var scroll = $(window).scrollTop() / -3;
        var scrollVid = $(window).scrollTop() / 5;
        $("#home_banner .home-text-banner, #home_banner .home-button-container").css("margin-bottom", scroll);
        $("#home_banner .banner-image").css("margin-top", scrollVid);
    });

    /**
     * Initialize some variables for the datepicker
     */
    var dateToday = new Date();

    // Get the calander elements
    var calendarElement = $("#calendar");

    // Initialize the calendar with a date picker, if any element is selected
    if(calendarElement.length > 0) {
        // Create a date picker render function
        function renderDatePickerDay(date) {
            // Return if the date is undefined
            if(date == undefined)
                return [false];

            // Define the variable
            var isAvailable = false;

            // Check whether the date is available, if the unavailable dates array isn't null
            if(unavailableDates != null) {
                // Build the date string
                var dateString = date.getFullYear() + "-" +
                        ("0" + (date.getMonth() + 1)).slice(-2) + "-" +
                        ("0" + date.getDate()).slice(-2);

                // Determine whether this date is in the list of occupied dates
                isAvailable = unavailableDates.indexOf(dateString) < 0;
            }

            // Return depending on whether the date is avaialble or not
            if(isAvailable)
                return [true];
            else
                return [false, "", "Deze datum is bezet."];
        }

        // Set up the date picker
        calendarElement.datepicker({
            prevText: "<",
            nextText: ">",
            firstDay: 1,
            minDate: dateToday,

            onSelect: function (date) {
                var d = new Date(date);
                var inputDate = d.getDate() + "/" + (d.getMonth() + 1) + "/" + d.getFullYear();
                var showDate = d.getDate() + " " + MONTH_NAMES[d.getMonth()] + " " + d.getFullYear();

                $(".toggle-time-date").removeClass("toggle-btn-disabled").attr("title", "");

                $("#date_input").val(inputDate);
                $(".selected-date").each(function () {
                    $(this).html(showDate);
                });
            },

            beforeShowDay: renderDatePickerDay
        });

        // Disable the loading indicator
        setLoadingIndicator(calendarElement, true);

        // Fetch the unavailable dates
        fetchUnavailableDates(function (err, dates) {
            // Print errors to the console
            if(err != null) {
                console.log(err);
                return ;
            }

            // Fill the list of unavailable dates
            unavailableDates = dates;

            // Refresh the date picker to update the unavailable dates
            calendarElement.datepicker("option", "beforeShowDay", renderDatePickerDay);
            calendarElement.datepicker("refresh");

            // Disable the loading indicator
            setLoadingIndicator(calendarElement, false);
        });
    }

    // Remove default date class
    $('.ui-state-active').removeClass('ui-state-active');

    $(".time-option").click(function () {
        // Show the go to second button
        $("#go_to_second").show();
        $(".selected-time").html(" OM " + $(this).val());
    });

    /**
     * Change the image based on the current step within the appointment creation
     */
    $("#back_to_first").click(function () {
        $("#appointment_step_image").attr("src", "/images/appointment/step-1.png");
    });
    $("#go_to_second, #back_to_second").click(function () {
        $("#appointment_step_image").attr("src", "/images/appointment/step-2.png");
    });
    // Third button needs the check if the fields are filled in correctly


    /**
     * Toggle the different sections for creating an appointment
     */
    $(".toggle-time-date").click(function () {
        if (!$(this).hasClass("toggle-btn-disabled")) {
            $(".time-date-box").slideToggle(500, "easeInOutCubic");
        }
    });

    // Load time data when a date is selected
    $(".toggle-time-date-to-time").click(function() {
        // Get the time container, and checkbox
        var timeContainer = $(".time-container");
        var timeCheckboxContainer = timeContainer.find("#select_time");

        // Clear the list of checkboxes
        timeCheckboxContainer.html("<i>Beschikbaarheid laden...</i>");

        // Wait for the animation to complete
        setTimeout(function() {
            // Create a function to append a checkbox to the container
            function createCheckbox(timeString, value) {
                // Generate an unique ID
                var uniqueId = getUniqueId("time-checkbox");

                // Append the checkbox
                timeCheckboxContainer.append("<li>" +
                    "<input class=\"time-option\" type=\"radio\" name=\"appointment_time\" value=\"09:30\" id=\"" + uniqueId + "\">" +
                    "<label class=\"time-option-label\" for=\"" + uniqueId + "\">" + timeString + "</label>" +
                    "</li>");
            }

            // Set the loading indicator
            setLoadingIndicator(timeContainer, true);

            // Fetch the times
            fetchTimes(null, function(err, times) {
                // Print errors to the console
                if (err != null) {
                    console.log(err);
                    return;
                }

                // Clear the list of checkboxes
                timeCheckboxContainer.html("");

                // Loop through the times
                var hasTime = false;
                for(var i = 0; i < times.length; i++) {
                    // Get the time entry
                    var timeEntry = times[i];

                    // Skip the time if it's not available
                    if(!timeEntry.available)
                        continue;

                    // Create the checkbox
                    createCheckbox(timeEntry.time + " uur", i, timeEntry.available);

                    // Set the has time flag
                    hasTime = true;
                }

                // Show a message if no time is available
                if(!hasTime)
                    timeCheckboxContainer.html("<i>Geen tijd beschikbaar op deze dag</i>");

                // Set the loading indicator
                setLoadingIndicator(timeContainer, false);
            });

        }, 500);
    });

    $("#go_to_second, #back_to_first").click(function () {
        $("#left_1, #left_2, #right_1, #right_2").toggle();
    });

    $("#back_to_second").click(function () {
        $("#left_2, #left_3, #right_2, #right_3").toggle();
    });

    /**
     * Add Comic Sans font family to the whole body
     */
    $("#comic_sans_button").click(function () {
        $("body").toggleClass("comic-sans");
    });

    /**
     * Push the filled in data to the overview
     */
    $("#go_to_third").click(function () {
        $("#overview_name").text($("#Name").val());
        $("#overview_datemarried").text($("#DateMarried").val());
        $("#overview_phone").text($("#Phone").val());
        $("#overview_mail").text($("#Mail").val());

        if ($("#Name").val().length === 0 ||
            $("#DateMarried").val().length === 0 ||
            $("#Mail").val().length === 0) {

            $("#fill_in_fields_warning").html("Je hebt niet alle verplichte velden ingevuld!");
        } else {
            $("#appointment_step_image").attr("src", "/images/appointment/step-3.png");
            $("#left_2, #left_3, #right_2, #right_3").toggle();
        }
    });

    /**
     * Enable validation for the email fields
     */
    //    $("#create_appointment_form").validate({
    //        rules: {
    //            Mail: "required",
    //            mail_verify: {
    //                equalTo: "#Mail"
    //            }
    //        }
    //    });

    /**
     * Fetch data from an AJAX endpoint.
     * This function automatically handles error reporting.
     *
     * @param {string} endpoint Endpoint to request.
     * @param {fetchDataCallback} callback Callback function.
     */
    function fetchData(endpoint, callback) {
        // Make sure an endpoint and callback is specified
        if(endpoint == undefined || typeof callback !== "function") {
            callback(new Error("Endpoint or callback not specified"));
            return;
        }

        // Do an AJAX request
        // TODO: Use inline error notifications.
        $.ajax({
            url: "/Ajax/" + endpoint,
            dataType: "json",
            method: "GET",
            error: function(jqXhr, textStatus) {
                // Define the error message
                var error = "Failed to fetch data.\n\nError: " + textStatus;

                // Alert the user
                alert(error);

                // Call back with an error
                callback(new Error(error));
            },
            success: function(data) {
                // Make sure the status is OK
                if(data.status !== "ok") {
                    // Define the error message
                    var error = "Failed to fetch data. The website returned an error.\n\nError: " + data.error.message;

                    // Alert the user
                    alert(error);

                    // Call back with an error
                    callback(new Error(error));
                }

                // Call back the fetched data
                callback(null, data.data);
            }
        });
    }

    /**
     * Fetch data callback function.
     *
     * @callback {function} fetchDataCallback
     * @param {Error|null} error Error instance if an error occurred, null if not.
     * @param {Object|undefined} response Data response.
     */

    /**
     * Fetch the unavailable dates for appointments.
     *
     * @param {fetchUnavailableDatesCallback} callback Callback function.
     */
    function fetchUnavailableDates(callback) {
        // Fetch the data
        fetchData("Appointments/GetDates", function (err, data) {
            // Call back errors
            if(err != null) {
                callback(err);
                return;
            }

            // Call back with the dates
            callback(null, data.dates);
        });
    }

    /**
     * Called when unavailable dates are fetched.
     *
     * @callback {function} fetchUnavailableDatesCallback
     * @param {Error|null} error Error instance if an error occurred, null otherwise.
     * @param {array} dates Array of dates that are occupied.
     */

    /**
     * Fetch the times that are available at the given date.
     *
     * @param {string} date Date to get the times for.
     * @param {fetchTimesCallback} callback Callback function.
     */
    function fetchTimes(date, callback) {
        // Fetch the data
        fetchData("Appointments/GetTimes", function (err, data) {
            // Call back errors
            if(err != null) {
                callback(err);
                return;
            }

            // Call back with the time
            callback(null, data.times);
        });
    }

    /**
     * Called when the times are fetched.
     *
     * @callback {function} fetchTimesCallback
     * @param {Error|null} error Error instance if an error occurred, null otherwise.
     * @param {TimeAvailabilityObject[]} times Array of objects definining available times.
     */

    /**
     * Object definining whether the given time is available or not for appointments.
     *
     * @typedef {object} TimeAvailabilityObject
     * @param {string} time The actual time.
     * @param {boolean} available True if this time is available, false if not.
     */

    /**
     * Show a loading indicator in the div.
     */
    function setLoadingIndicator(element, show) {
        // Get the loading element if there is any
        var currentOverlay = element.find(".loading-overlay");

        // Add/remove the loading indicator class
        if (show) {
            // Make sure a loading element isn't available already
            if (currentOverlay.length > 0)
                return;

            // Prepend the overlay and indicator divs
            element.prepend("<div class=\"loading-overlay\"><div class=\"loading-indicator\"></div></div>");

            // Get the overlay and indicator elements
            var overlay = element.find(".loading-overlay");
            var indicator = overlay.find(".loading-indicator");

            // Properly size the overlay
            overlay.css({
                width: element.width(),
                height: element.height()
            });

            // Center the indicator
            indicator.css({
                top: overlay.height() / 2 - indicator.height() / 2,
                left: overlay.width() / 2 - indicator.width() / 2
            });

            // Fade the overly in
            overlay.stop().hide().fadeIn(350);

        } else {
            // Remove the loading element
            currentOverlay.fadeOut(350, function () {
                // Remove the element when fading is complete
                $(this).remove();
            });
        }
    }

    /** Appointment banner **/
    $(".home-list>ol>li").click(function () {
        var counter = $(this).index() + 1;
        console.log(counter);
        $(".appointment-banner-image-counter").empty();
        $(".appointment-banner-image-counter").html(counter);
        if (counter == 1) {
            $(".appointment-banner-image-container>img").attr('src', "images/mooi-meisje-3.jpg");
        } else if (counter == 2) {
            $(".appointment-banner-image-container>img").attr('src', "images/mooi-meisje-2.jpg");
        } else if (counter == 3) {
            $(".appointment-banner-image-container>img").attr('src', "images/mooi-meisje.jpg");
        } else {
            $(".appointment-banner-image-container>img").attr('src', "images/mooi-meisje-3.jpg");
        }
    });
});
