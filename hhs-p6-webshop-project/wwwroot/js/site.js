/**
 * List of month names.
 * @const
 */
var MONTH_NAMES = [
    "JANUARI", "FEBRUARI", "MAART", "APRIL", "MEI", "JUNI", "JULI", "AUGUSTUS", "SEPTEMBER", "OKTOBER", "NOVEMBER",
    "DECEMBER"
];

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
    if (prefix == undefined)
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
        formatted = dateTime.getFullYear() +
            "-" +
            ("0" + (dateTime.getMonth() + 1)).slice(-2) +
            "-" +
            ("0" + dateTime.getDate()).slice(-2);

    // Format the time
    if (formatTime)
        formatted += (formatDate ? " " : "") +
            ("0" + dateTime.getHours()).slice(-2) +
            ":" +
            ("0" + dateTime.getMinutes()).slice(-2) +
            ":" +
            ("0" + dateTime.getSeconds()).slice(-2);

    // Return the formatted string
    return formatted;
}

// Run this code when the page is finished loading
$(document).ready(function() {

    // Initialize WOW
    new WOW({
        boxClass: 'wow',
        animateClass: 'animated',
        offset: 0,
        live: true
    }).init();

    // Create an accordion of the filters container
    $('.filters-container').accordion({
        collapsible: true
    });

    /**
     * Update the price range label in the filters for products.
     */
    function updateSelectedPriceRange(min, max) {
        $("#filter-price-amount").val("€" + min + " tot €" + max);
    }

    /**
     * Get the selected price range values.
     *
     * @return {int[]} Array with the lowest and highest values in the range, in order.
     */
    function getPriceRangeSliderValues() {
        // Create an array to return
        var result = [];

        // Put the values in the result array
        for (var i = 0; i < 2; i++)
            result.push($("#filter-price-slider").slider("values", i));

        // Return the result array
        return result;
    }

    // Get the filter price slider element
    var filterPriceSliderElement = $("#filter-price-slider");

    // Configure the slider if any is found on the page
    if (filterPriceSliderElement.length > 0) {
        // Get the minimum and maximum of the range
        var rangeMin = parseInt(filterPriceSliderElement.find("input[type='hidden'][name='range-min']").val());
        var rangeMax = parseInt(filterPriceSliderElement.find("input[type='hidden'][name='range-max']").val());

        // Create the price filter range slider
        filterPriceSliderElement.slider({
            range: true,
            min: rangeMin,
            max: rangeMax,
            values: [
                rangeMin,
                rangeMax
            ],
            slide: function(event, ui) {
                updateSelectedPriceRange(ui.values[0], ui.values[1]);
            }
        });

        // Render the label when loading the page
        updateSelectedPriceRange($("#filter-price-slider").slider("values", 0),
            $("#filter-price-slider").slider("values", 1));
    }

    /**
     * Toggle the search box when the search button is clicked
     */
    $("#header_search_toggle").click(function() {
        // Make the search box and field visible
        $("#search_box").animate({ width: 'toggle' },
            350,
            function() {
                // Focus the search field
                $("#header_search").focus();
            });
    });

    /**
     * Toggle the menu when the toggle menu button is clicked
     */
    $("#menu_toggle").click(function() {
        $(this).toggleClass("glyphicon-remove glyphicon-menu-hamburger");
        $("#header_nav").slideToggle();
    });

    /**
     * Scroll down when you click the scroll down button
     */
    $(".scrollToggle").click(function() {
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
    $(window).resize(function() {
        var scrWidth = $(window).width();

        // Check for mobile devices
        if (scrWidth > 600) {
            setHomeBannerHeight();
        }
    });

    $(window).scroll(function(event) {
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
    if (calendarElement.length > 0) {
        // Create a variable for the selected date and time
        var selectedDateTime = new Date();
        selectedDateTime.setTime(selectedDateTime.getTime() + 7 * 24 * 60 * 60 * 1000);

        // Get the time container, and radio buttons
        var timeContainer = $(".time-container");
        var timeRadioButtonContainer = timeContainer.find("#select_time");

        // Get the second step button
        var secondStepButton = $("#go_to_second");

        // Create a date picker render function
        function renderDatePickerDay(date) {
            // Return if the date is undefined
            if (date == undefined)
                return [false];

            // Define the variable
            var isAvailable = false;

            // Check whether the date is available, if the unavailable dates array isn't null
            if (unavailableDates != null) {
                // Build the date string
                var dateString = formatDateTime(date, true, false);

                // Determine whether this date is in the list of occupied dates
                isAvailable = unavailableDates.indexOf(dateString) < 0;
            }

            // Return depending on whether the date is avaialble or not
            if (isAvailable)
                return [true];
            else
                return [false, "", "Deze datum is bezet."];
        }

        // Get the button to toggle to the time
        var toggleToTimeButton = $(".toggle-time-date-to-time");
        var toggleToDateButton = $(".toggle-time-date-to-date");

        /**
         * Show the date page.
         *
         * @param {boolean|undefined} [initial] True if this is the initial page (because a different animation will be used)j.
         */
        function showDatePage(initial) {
            // Animate the pages when we're not on the initial page
            if (!initial)
                $(".time-date-box").slideToggle({
                    "duration": 500,
                    "easing": "easeInOutCubic"
                });
            else
                $(".time-date-box").addClass("animated fadeIn");

            // Fade out the buttons
            timeRadioButtonContainer.find("li").addClass("animated fadeOutLeft");

            // Animate the arrow buttons
            toggleToDateButton.addClass("animated fadeOutLeft");

            // Hide the second step button
            secondStepButton.removeClass("animated fadeIn pulse").delay(1).addClass("animated fadeOut");

            // Disable the animations when they complete
            setTimeout(function() {
                    // Reset the radio button box contents, to remove the time selection radio buttons
                    timeRadioButtonContainer.html("");

                    // Remove the animations from the toggle to date button
                    toggleToDateButton.removeClass("animated fadeOutLeft").addClass("toggle-btn-disabled");

                    // Hide the second step button
                    secondStepButton.hide();
                },
                500);

            // Force select the currently selected date
            calendarElement.datepicker("setDate", selectedDateTime);
            onDateSelect(selectedDateTime);
        }

        /**
         * Called when the time page should be shown.
         */
        function showTimePage() {
            // Animate the pages
            $(".time-date-box").slideToggle({
                "duration": 500,
                "easing": "easeInOutCubic"
            });

            // Hide the second step button
            secondStepButton.removeClass("animated fadeOut").delay(0).addClass("animated fadeIn pulse");

            // Animate the arrow buttons
            toggleToTimeButton.addClass("animated fadeOutRight");
            toggleToDateButton.addClass("animated fadeInRight");

            // Disable the animations when they complete
            setTimeout(function() {
                    toggleToTimeButton.removeClass("animated fadeOutRight").addClass("toggle-btn-disabled");
                },
                500);
        }

        /**
         * Called when a date is selected.
         */
        function onDateSelect(date) {
            // Format the show date
            var showDate = date.getDate() + " " + MONTH_NAMES[date.getMonth()] + " " + date.getFullYear();

            // Get the toggle time date button and toggle it's visibility
            var toggleTimeDateButton = $(".toggle-time-date");

            // Animate the button
            toggleTimeDateButton.attr("title", "").addClass("animated fadeInLeft");

            // Update the selected date label
            $(".selected-date").each(function() {
                $(this).html(showDate);
            });

            // Set the selected date
            selectedDateTime.setFullYear(date.getFullYear());
            selectedDateTime.setMonth(date.getMonth());
            selectedDateTime.setDate(date.getDate());
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
        fetchUnavailableDates(function(err, dates) {
            // Print errors to the console
            if (err != null) {
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
                        var value = appointmentTimeObject.time.hour +
                            ":" +
                            ("0" + appointmentTimeObject.time.minute).slice(-2);

                        // Append the radio button
                        timeRadioButtonContainer.append("<li>" +
                            "<input class=\"time-option animated fadeInRight\" type=\"radio\" name=\"appointment_time\" value=\"" +
                            value +
                            "\" id=\"" +
                            uniqueId +
                            "\">" +
                            "<label class=\"time-option-label animated fadeInRight\" for=\"" +
                            uniqueId +
                            "\">" +
                            appointmentTimeObject.formattedTime +
                            " uur</label>" +
                            "</li>");
                    }

                    // Set the loading indicator
                    setLoadingIndicator(timeContainer, true);

                    // Fetch the times
                    fetchTimes(selectedDateTime,
                        function(err, times) {
                            // Print errors to the console
                            if (err != null) {
                                console.log(err);
                                return;
                            }

                            // Clear the list of radio buttons
                            timeRadioButtonContainer.html("");

                            // Loop through the times
                            var hasTime = false;
                            for (var i = 0; i < times.length; i++) {
                                // Get the time entry
                                var timeEntry = times[i];

                                // Skip the time if it's not available
                                if (!timeEntry.available)
                                    continue;

                                // Create the radio buttons
                                createTimeRadioButton(timeEntry);

                                // Set the has time flag
                                hasTime = true;
                            }

                            // Show a message if no time is available
                            if (!hasTime)
                                timeRadioButtonContainer.html("<i>Geen tijd beschikbaar op deze dag</i>");

                            // Link the radio buttons to the date time field
                            timeRadioButtonContainer.find("input.time-option").change(function() {
                                // Get the radio button element, and check whether it's selected
                                var radioButton = $(this);
                                var isSelected = radioButton.is(":checked");

                                // Return if the radio button isn't selected
                                if (!isSelected)
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

                                // Show the go to second button
                                $(".selected-time").html(" OM " + timeString);

                                // Show the second button
                                secondStepButton.show();

                                // Pulse the button once
                                setTimeout(function() {
                                        secondStepButton.removeClass("animated fadeIn").delay(0)
                                            .addClass("animated pulse");
                                },
                                750);
                            });

                            // Set the loading indicator
                            setLoadingIndicator(timeContainer, false);
                        });

                },
                500);
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
    $("#back_to_first").click(function() {
        $("#appointment_step_image").attr("src", "/images/appointment/step-1.png");
    });
    $("#go_to_second, #back_to_second").click(function() {
        // TODO: Possibly fade this
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
            if (isLeft)
                container = container.parent();

            // Update the left and right container instances, give the containers a fixed width, and update the offset
            if (isLeft) {
                if (leftOffsetHeight == undefined && panel.is(":visible"))
                    leftOffsetHeight = container.height() - panel.height();

                if (leftContainer == undefined) {
                    leftContainer = container;

//                    container.width(container.width());
                    container.height(container.height());
                }

                leftPanels.push(panel);

            } else {
                if (rightOffsetHeight == undefined && panel.is(":visible"))
                    rightOffsetHeight = container.height() - panel.height();

                if (rightContainer == undefined) {
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
            if (isLeft)
                container = container.parent();

            // Check whether the component is visible
            if (panel.is(":visible")) {
                // Toggle the fadeIn class to fadeOut
                panel.removeClass("fadeIn animated").delay(0).addClass("animated fadeOut");

                // Hide the panel when done
                setTimeout(function() {
                        panel.removeClass("fadeOut").addClass("fadeIn").hide();
                        panel.css("position", wasPos);
                    },
                    1000);

            } else {
                // Update the target height
                var height = panel.height() + (isLeft ? leftOffsetHeight : rightOffsetHeight);
                if (height > targetHeight)
                    targetHeight = height;

                // Show the panel, this will animate using CSS
                panel.show();
            }
        });

        // Properly resize the parent panel
        leftContainer.animate({
                "height": targetHeight
            },
            500);
        rightContainer.animate({
                "height": targetHeight
            },
            500);

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
            },
            1000);
    }

    $("#go_to_second, #back_to_first").click(function() {
        switchStep("#left_1, #right_1", "#left_2, #right_2");
    });

    $("#back_to_second").click(function() {
        switchStep("#left_2, #right_2", "#left_3, #right_3");
    });

    /**
     * Add Comic Sans font family to the whole body
     */
    $("#comic_sans_button").click(function() {
        $("body").toggleClass("comic-sans");
    });

    /**
     * Push the filled in data to the overview
     */
    $("#go_to_third").click(function() {
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
        if (endpoint == undefined || typeof callback !== "function") {
            callback(new Error("Endpoint or callback not specified"));
            return;
        }

        // Do an AJAX request
        // TODO: Use inline error notifications.
        $.ajax({
            url: "/api/" + endpoint,
            dataType: "json",
            type: "GET",
            data: data,
            error: function(jqXhr, textStatus) {
                // Define the error message
                var error = "Failed to fetch data.\n\nError: '" + textStatus + "'";

                // Alert the user with the error
                alert(error);

                // Call back with an error
                callback(new Error(error));
            },
            success: function(data) {
                // Call back the fetched data
                callback(null, data);
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
        fetchData("appointments/getdates",
            function(err, data) {
                // Call back errors
                if (err != null) {
                    callback(err);
                    return;
                }

                // Call back with the dates
                callback(null, data);
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
        fetchData("appointments/gettimes",
            function(err, data) {
                // Call back errors
                if (err != null) {
                    callback(err);
                    return;
                }

                // Call back with the time
                callback(null, data);
            },
            {
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
            currentOverlay.fadeOut(350,
                function() {
                    // Remove the element when fading is complete
                    $(this).remove();
                });
        }
    }

    /** Appointment banner **/
    $(".home-list li").click(function() {
        var num = $(this).attr("data-attribute");
        $(".home-list li").removeClass("active");
        $(this).addClass("active");

        $(".appointment-banner-image-counter").html(num);
        $(".appointment-banner-image").css("background-image", "url('/images/home/banner-" + num + ".jpg')");
    });

    // Find the product overview
    var productOverviewElement = $(".product-overview");

    // Define a variable to hold a pending AJAX call for filters
    var ajaxFilterRequest;

    // Load the filter logic when a product overview is available
    if (productOverviewElement.length > 0) {
        // Get the sort selection box
        var sortElement = $("#filters-sort");

        /**
         * Fetch a list of dressesk.
         * Filters are applied as specified in the sidebar.
         */
        function fetchProductsFiltered() {
            // Show a loading indiator
            setLoadingIndicator(productOverviewElement, true);

            // Create a filter object
            var filterObject = {
                Prijs: getPriceRangeSliderValues(),
                Kleur: []
            };

            // Define the API endpoint URL
            var endpointUrl = "/api/dressfinder/product/filter/partial";

            // Find the selected checkboxes, and build the filter object
            $("#filter-color").each(function() {
                // Get the filter element
                var filterElement = $(this);

                // Get the list of checked checkboxes
                var checkedBoxes = filterElement.find("input:checked");

                // Skip if no boxes are selected
                if (checkedBoxes.length <= 0)
                    return;

                // Put the checkbox IDs in the array
                checkedBoxes.each(function() {
                    filterObject.Kleur.push($(this).val());
                });
            });

            // Set the sorting parameter on the API endpoint URL
            endpointUrl += "/sort/" + sortElement.find("option:selected").val();

            // Abort pending AJAX calls for filtering
            if (ajaxFilterRequest != undefined)
                ajaxFilterRequest.abort();

            // Get the product filters element
            var productFiltersElement = $('.product-filters');

            productFiltersElement.find(":input").prop("disabled", true);

            // Filter the dresses and fetch the new list through AJAX
            ajaxFilterRequest = $.ajax({
                url: endpointUrl,
                type: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                data: JSON.stringify(filterObject),
                error: function(jqXhr, textStatus) {
                    // Define the error message
                    var error = "Failed to filter dresses.\n\nError: " + textStatus;

                    // Alert the user
                    alert(error);
                },
                success: function(data) {
                    // Find the product elements
                    var productElements = productOverviewElement.find('.product');

                    // Fade out the current elements
                    productElements.attr("data-wow-delay", "0s");
                    productElements.toggleClass('animate-product-catalog-enter animate-product-catalog-leave');

                    // Show the new elements when the previous animation is complete
                    setTimeout(function() {
                            // Remove the current list of products (that is already faded away)
                            productElements.remove();

                            // Set the new products
                            productOverviewElement.append(data);

                        },
                        300);
                },
                complete: function() {
                    // Hide the loading indiator
                    setLoadingIndicator(productOverviewElement, false);

                    // Remove the request as pending
                    ajaxFilterRequest = undefined;

                    // Show the new elements when the previous animation is complete
                    setTimeout(function() {
                            // Re-enable the buttons
                            productFiltersElement.find(":input").prop("disabled", false);

                        },
                        300);
                }
            });
        }

        // Call the product fetch function when a filter is clicked, or when the sorting is changed
        $("#filter-color").find("input[type=checkbox]").click(fetchProductsFiltered);
        $("#filter-price-slider").on("slidestop", fetchProductsFiltered);
        sortElement.change(fetchProductsFiltered);

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
        createAppointmentFormElement.keypress(function(event) {
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

    // File uploading custom button
    $('.inputfile').change(function(e) {
        // Get the input file selectors
        var inputs = document.querySelectorAll('.inputfile');

        // Loop through the selected inputs
        for (var i = 0; i < inputs.length; i++) {
            // Get the current entry
            var input = inputs[i];

            // Get the file name of the selected input (file)
            var fileName = e.target.value.split('\\').pop();

            // Append the file name to the upload image box
            if (fileName !== null)
                $('.upload-image').html(fileName);
        }
    });
});