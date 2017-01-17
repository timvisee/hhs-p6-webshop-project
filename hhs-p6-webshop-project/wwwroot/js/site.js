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

/**
 * Format a date to ISO.
 *
 * @param {Date} dateTime Date time object.
 * @param {boolean} formatDate True to format the date, false if not.
 * @param {boolean} formatTime True to format the time, false if not.
 */
function formatDateTime(dateTime, formatDate, formatTime) {
    // Create a variable to store the formatted date time in
    var formatted = "";

    // Format the date
    if (formatDate)
        formatted = dateTime.getFullYear() + "-" +
                ("0" + (dateTime.getMonth() + 1)).slice(-2) + "-" +
                ("0" + dateTime.getDate()).slice(-2);

    // Format the time
    if (formatTime)
        formatted = (formatDate ? " " : "") +
                ("0" + dateTime.getHours()).slice(-2) + ":" +
                ("0" + dateTime.getMinutes()).slice(-2) + ":" +
                ("0" + dateTime.getSeconds()).slice(-2);

    // Return the formatted string
    return formatted;
}

// Run this code when the page is finished loading
$(document).ready(function () {

    // Initialize WOW
    new WOW({
        boxClass: 'wow',
        animateClass: 'animated',
        offset: 0,
        live: true
    }).init();

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
     * Toggle the menu when the toggle menu button is clicked
     */
    $("#menu_toggle").click(function() {
        $("#header_nav").slideToggle();
    });

    /**
     * Scroll down when you click the scroll down button
     */
    $(".scrollToggle").click(function () {
        $("html, body").animate({ scrollTop: $(window).height() - 90 }, 1200, "easeInOutCubic");
    });

    /**
     * Function for changing the height of the home banner
     */
    function setHomeBannerHeight() {
        var scrHeight = $(window).height();
        var headerHeight = $("#header_top").height();

        $("#home_banner").height(scrHeight - 150);
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
        var scroll = $(window).scrollTop() / -2;
        var scrollVid = $(window).scrollTop() / 3;
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
        // Create a variable for the selected date and time
        var selectedDateTime = null;

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
                var dateString = formatDateTime(date, true, false);

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

            onSelect: function(date) {
                // Direct the function to onDateSelect with a proper date object
                onDateSelect(new Date(date));
            },

            beforeShowDay: renderDatePickerDay
        });

        // Show the date page
        showDatePage(true);

        // Disable the loading indicator
        setLoadingIndicator(calendarElement, true);

        // Fetch the unavailable dates
        fetchUnavailableDates(function (err, dates) {
            // Print errors to the console
            if(err != null) {
                console.log(err);
                return;
            }

            // Fill the list of unavailable dates
            unavailableDates = dates;

            // Refresh the date picker to update the unavailable dates
            calendarElement.datepicker("option", "beforeShowDay", renderDatePickerDay);
            calendarElement.datepicker("refresh");

            // Disable the loading indicator
            setLoadingIndicator(calendarElement, false);
        });

        // Load time data when a date is selected
        $(".toggle-time-date-to-time").click(function() {
            // Show the time page
            showTimePage();

            // Clear the list of radio buttons
            timeRadioButtonContainer.html("<i>Beschikbaarheid laden...</i>");

            // Wait for the slide animation to complete
            setTimeout(function() {
                // Create a function to append a radio button to the container
                function createTimeRadioButton(appointmentTimeObject) {
                    // Generate an unique ID
                    var uniqueId = getUniqueId("time-button");

                    // Determine the value
                    var value = appointmentTimeObject.time.hour + ":" + appointmentTimeObject.time.minute;

                    // Append the radio button
                    timeRadioButtonContainer.append("<li>" +
                        "<input class=\"time-option animated fadeInRight\" type=\"radio\" name=\"appointment_time\" value=\"" + value + "\" id=\"" + uniqueId + "\">" +
                        "<label class=\"time-option-label animated fadeInRight\" for=\"" + uniqueId + "\">" + appointmentTimeObject.formattedTime + " uur</label>" +
                        "</li>");
                }

                // Set the loading indicator
                setLoadingIndicator(timeContainer, true);

                // Fetch the times
                fetchTimes(selectedDateTime, function(err, times) {
                    // Print errors to the console
                    if (err != null) {
                        console.log(err);
                        return;
                    }

                    // Clear the list of radio buttons
                    timeRadioButtonContainer.html("");

                    // Loop through the times
                    var hasTime = false;
                    for(var i = 0; i < times.length; i++) {
                        // Get the time entry
                        var timeEntry = times[i];

                        // Skip the time if it's not available
                        if(!timeEntry.available)
                            continue;

                        // Create the radio buttons
                        createTimeRadioButton(timeEntry);

                        // Set the has time flag
                        hasTime = true;
                    }

                    // Show a message if no time is available
                    if(!hasTime)
                        timeRadioButtonContainer.html("<i>Geen tijd beschikbaar op deze dag</i>");

                    // Link the radio buttons to the date time field
                    timeRadioButtonContainer.find("input.time-option").change(function () {
                        // Get the radio button element, and check whether it's selected
                        var radioButton = $(this);
                        var isSelected = radioButton.is(":checked");

                        // Return if the radio button isn't selected
                        if(!isSelected)
                            return;

                        // Parse the time
                        var timeString = radioButton.val();
                        var hour = parseInt(timeString.split(":")[0]);
                        var minute = parseInt(timeString.split(":")[1]);

                        // Modify the selected date
                        selectedDateTime.setHours(hour);
                        selectedDateTime.setMinutes(minute);
                        selectedDateTime.setSeconds(0);

                        // Set the date input field value
                        $("#date_input").val(formatDateTime(selectedDateTime, true, true));
                    });

                    $(".time-option").click(function () {
                        // Show the go to second button
                        $(".selected-time").html(" OM " + timeString);

                        // Show the second button
                        secondStepButton.show();

                        // Pulse the button once
                        setTimeout(function () {
                            secondStepButton.removeClass("animated fadeIn").delay(0).addClass("animated pulse");
                        }, 750);
                    });

                    // Set the loading indicator
                    setLoadingIndicator(timeContainer, false);
                });

            }, 500);
        });

        // Load time data when a date is selected
        $(".toggle-time-time-to-date").click(function() {
            // Show the date page
            showDatePage();
        });
    }

    // Remove default date class
    $('.ui-state-active').removeClass('ui-state-active');

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
    var leftOffsetHeight;
    var rightOffsetHeight;

    function switchStep(fromElements, toElements) {
        // Get the list of panels
        var panels = $(fromElements + ", " + toElements);

        // Create a variable for the left and right container
        var leftContainer;
        var rightContainer;

        // Get the left and right panels
        var leftPanels = [];
        var rightPanels = [];

        // Loop through each panel to handle it manually
        panels.each(function() {
            var panel = $(this);
            var isLeft = !panel.hasClass("right-box");
            var container = panel.parent();
            if(isLeft)
                container = container.parent();

            // Update the left and right container instances, give the containers a fixed width, and update the offset
            if(isLeft) {
                if(leftOffsetHeight == undefined && panel.is(":visible"))
                    leftOffsetHeight = container.height() - panel.height();

                if(leftContainer == undefined) {
                    leftContainer = container;

//                    container.width(container.width());
                    container.height(container.height());
                }

                leftPanels.push(panel);

            } else {
                if(rightOffsetHeight == undefined && panel.is(":visible"))
                    rightOffsetHeight = container.height() - panel.height();

                if(rightContainer == undefined) {
                    rightContainer = container;

//                    container.width(container.outerWidth());
                    container.height(container.height());
                }

                rightPanels.push(panel);
            }
        });

        // Store the current position state to revert it to after animating
        var wasPos = panels.eq(0).css("position");

        // Define the target height
        var targetHeight = 0;

        // Loop through the panels to update their visibility
        panels.each(function() {
            var panel = $(this);
            var isLeft = !panel.hasClass("right-box");
            var container = panel.parent();
            if(isLeft)
                container = container.parent();

            // Check whether the component is visible
            if(panel.is(":visible")) {
                // Toggle the fadeIn class to fadeOut
                panel.removeClass("fadeIn animated").delay(0).addClass("animated fadeOut");

                // Hide the panel when done
                setTimeout(function() {
                    panel.removeClass("fadeOut").addClass("fadeIn").hide();
                    panel.css("position", wasPos);
                }, 1000);

            } else {
                // Update the target height
                var height = panel.height() + (isLeft ? leftOffsetHeight : rightOffsetHeight);
                if(height > targetHeight)
                    targetHeight = height;

                // Show the panel, this will animate using CSS
                panel.show();
            }
        });

        // Properly resize the parent panel
        leftContainer.animate({
                "height": targetHeight
        }, 500);
        rightContainer.animate({
                "height": targetHeight
        }, 500);

        // Make the position absolute of the first elements
        var bigPanelLeft = leftPanels[0].index() < leftPanels[1].index() ? leftPanels[0] : leftPanels[1];
        var bigPanelRight = rightPanels[0].index() < rightPanels[1].index() ? rightPanels[0] : rightPanels[1];
//        bigPanelLeft.width(bigPanelLeft.width());
//        bigPanelLeft.width(bigPanelRight.width());
        bigPanelLeft.css("position", "absolute");
        bigPanelRight.css("position", "absolute");

        // Reset the position of the element
        setTimeout(function() {
            bigPanelLeft.css("position", wasPos);
            bigPanelRight.css("position", wasPos);
        }, 1000);
    }

    $("#go_to_second, #back_to_first").click(function () {
        switchStep("#left_1, #right_1", "#left_2, #right_2");
    });

    $("#back_to_second").click(function () {
        switchStep("#left_2, #right_2", "#left_3, #right_3");
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
            switchStep("#left_2, #right_2", "#left_3, #right_3");
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
     * @param {Object} [data] Optional data.
     */
    function fetchData(endpoint, callback, data) {
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
            type: "GET",
            data: data,
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

        }, {
            date: formatDateTime(date, true, false)
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
                top: Math.min(overlay.height() / 2 - indicator.height() / 2, 225),
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
        $(".home-list>ol>li").removeClass("active");
        var counter = $(this).index() + 1;
        console.log(counter);
        $(".appointment-banner-image-counter").empty();
        $(".appointment-banner-image-counter").html(counter);
        $(".home-list>ol>li").eq(counter - 1).addClass("active");
        $(".appointment-banner-image-container>img").attr('src', "images/mooi-meisje-" + counter + ".jpg");
    });

    // Find the product overview
    var productOverviewElement = $(".product-overview");

    // Load the filter logic when a product overview is available
    if (productOverviewElement.length > 0) {
        /**
         * Fetch a list of dressesk.
         * Filters are applied as specified in the sidebar.
         */
        function fetchProductsFiltered() {
            // Show a loading indiator
            setLoadingIndicator(productOverviewElement, true);

            // Create a filter object
            var filterObject = {
                values: {}
            };

            // Find the selected checkboxes, and build the filter object
            $(".filter").each(function () {
                // Get the filter element
                var filterElement = $(this);

                // Get the list of checked checkboxes
                var checkedBoxes = filterElement.find("input:checked");

                // Skip if no boxes are selected
                if (checkedBoxes.length <= 0)
                    return;

                // Get the product ID for this filter section as key
                var key = String(filterElement.find("input.field-property-id").val());

                // Create an entry in the filter object
                filterObject.values[key] = [];

                // Put the checkbox IDs in the array
                checkedBoxes.each(function () {
                    filterObject.values[key].push($(this).val());
                });
            });

            // Filter the dresses and fetch the new list through AJAX
            $.ajax({
                url: "/api/dressfinder/product/filter/partial",
                type: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                data: JSON.stringify(filterObject),
                error: function (jqXhr, textStatus) {
                    // Define the error message
                    var error = "Failed to filter dresses.\n\nError: " + textStatus;

                    // Alert the user
                    alert(error);
                },
                success: function (data) {
                    // Find the product elements
                    var productElements = productOverviewElement.find('.product');

                    // Fade out the current elements
                    productElements.attr("data-wow-delay", "0s");
                    productElements.toggleClass('animate-product-catalog-enter animate-product-catalog-leave');

                    // Show the new elements when the previous animation is complete
                    setTimeout(function () {
                        // Remove the current list of products (that is already faded away)
                        productElements.remove();

                        // Set the new products
                        productOverviewElement.append(data);

                    }, 300);
                },
                complete: function () {
                    // Hide the loading indiator
                    setLoadingIndicator(productOverviewElement, false);
                }
            });
        }

        // Call the product fetch function when a filter is clicked
        $(".filter").find("input[type=checkbox]").click(fetchProductsFiltered);

        // Filter once on page load
        fetchProductsFiltered();
    }

    // Get the appointment creation form
    var createAppointmentFormElement = $("#create_appointment_form");

    // Execute the appointment creation form logic when it's available on the page
    if (createAppointmentFormElement.length > 0) {
        // Get the button element to go to step 3
        var stepThreeButton = $("#go_to_third");

        // Handle key press events, and prevent the enter key from submitting the form
        createAppointmentFormElement.keypress(function (event) {
            // Continue if this wasn't the enter key that is pressed
            if (event.keyCode !== 13)
                return true;

            // Validate the form
            createAppointmentFormElement.validate();
            var isValid = createAppointmentFormElement.valid();

            // Go to step 3 if the form is valid
            if (isValid)
                stepThreeButton.click();

            // The enter key is pressed, prevent the default action
            event.preventDefault();
            return false;
        });
    }

    //File uploading custom button
    $('.inputfile').change(function (e) {
        var inputs = document.querySelectorAll('.inputfile');
        Array.prototype.forEach.call(inputs, function (input) {
            var fileName = '';
            fileName = e.target.value.split('\\').pop();
            console.log(fileName);
            $('.upload-image').html(fileName);
        });
    });
});