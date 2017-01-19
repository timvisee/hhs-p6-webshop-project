$(document).ready(function () {

    var colorId = null;

    // Set the first color as default
    getImagePaths($(".color-circle:first-child").attr("value"));

    $(".color-circle").click(function () {
        colorId = $(this).attr("value");
        getImagePaths(colorId);

        // Update the URL hash to match the color
        window.location.hash = $(this).attr("title");
    });

    function getImagePaths(colorId) {
        // Filter the dresses and fetch the new list through AJAX
        $.ajax({
            url: "/ProductImages/GetImagePaths/" + colorId,
            type: "GET",
            headers: {
                "Content-Type": "application/json"
            },
            error: function (jqXhr, textStatus) {
                // Define the error message
                var error = "Fout bij het ophalen van de afbeeldingen!.\n\nError: " + textStatus;

                // Alert the user
                alert(error);
            },
            success: function (data) {
                $(".side-pictures").empty();
                data["data"]["paths"].forEach(renderImages);
            }
        });
    }

    function renderImages(element, index, array) {
        if (index == 0) {
            $(".big-picture img").attr("src", "/" + element);
            $(".big-picture a").attr("href", "/" + element);
        }

        $(".side-pictures").append("<div class='small-picture col-md-12 col-xs-4'><a data-lightbox='dress-images' href='/" + element + "'><img src='/" + element + "' /></a></div>")
    }

    // Pass the selected dress color along when clicking the appointment creation button
    $("#appointment-create-button").click(function(event) {
        // Get the selected color
        var colorName = window.location.hash.substring(1);

        // Get the button URL
        var targetUrl = $(this).attr("href");

        // Append the color parameter
        targetUrl += "?color=" + colorName;

        // Go to the target URL
        window.location.href = targetUrl;

        // Prevent the default event function
        event.preventDefault();
    });

    // Update the color hash when a color parameter is given
    if(window.location.search.includes("color=")) {
        // Make sure no color is set in the hash
        if (window.location.hash.length <= 1) {
            // Fetch the selected color
            var selectedColor = location.search.split('color=')[1];

            // Set the color hash
            window.location.hash = selectedColor;
        }
    }
});
