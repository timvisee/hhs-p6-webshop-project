$(document).ready(function () {

    var colorId = null;
    var parentDiv = null;

    $(".color-circle").on("mouseenter", function () {
        colorId = $(this).attr("value");

        // Get the parent .product div so you can find the image class per product
        parentDiv = $(this).parent(".product-colors").parent("a").parent(".product");

        getImagePaths(colorId, parentDiv);
    });

    function getImagePaths(colorId, parentDiv) {
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
                // Set the product image source
                parentDiv.find(".product-image").attr("src", data["data"]["paths"][0]);
            },
            complete: function () {
                
            }
        });
    }
});
