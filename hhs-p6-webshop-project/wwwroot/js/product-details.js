$(document).ready(function () {

    var colorId = null;


    // Set the first color as default
    getImagePaths($(".color-circle:first-child").attr("value"));

    $(".color-circle").click(function () {
        colorId = $(this).attr("value");
        getImagePaths(colorId);
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
            },
            complete: function () {
                
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


});
