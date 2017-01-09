# Dressfinder

## Features
* Products
* Product categories
* Product properties
    * Such as: Color, size
* Product images
* Filtering
    
## Default properties
* Price: single estimated price as `int`
* Category: list of category names `string`
* Brand: list of brands `string`

## Images
How images are handled:
* Images are coupled to static product property values. These are, the static values defined in the `PropertySet` table, and not the custom values.
* When a product is shown, show all images that are linked to the properties that are currently selected.

Example:
A product has a color property. This property uses preconfigured/fixed values, and no custom values.
These color values are configured in the `PropertySet` table.
Images for the various colors are linked to the correct `PropertySet` entry.
When a product is shown with *red* as selected color property, only the pictures for the red variant are shown.
    
## Page structure
### Catalog page
Page showing multiple products, catalog page:
* Filters sidebar
    * Filters should dynamically be fetched from the `PropertyType` database.
    * Filter type (radio buttons, range slider) should be determined based on the `PropertyType` data type.
* Allow to filter on categories
    
### Product page
Page showing a specific product:
* Show product name and description.
* Show relevant product images.
    * Images are linked to properties. Select all images linked to the currently selected properties.
* Show all configured properties for the product.
* If there is more than one value for any property. Allow the user to select one of the properties.
    * Such as: Color and size.

## Object / Database structure
* `Product`
    * **`ID`** : `Int` *(AI)*
    * `Name` : `String`
    * `Description` : `String`
    * `Values[]` : Foreign keys -> `PropertyValue.ID`
    * *Notice*:
        * *Properties are linked using the PropertyValues table*
* `PropertyType`
    * **`ID`** : `Int` *(AI)*
    * `Name` : `String`
    * `DataType` : `String` (C# compatible)
    * `Required` : `Bool`
    * `Mutliple` : `Bool`
    * `AllowCustom` : `Bool`
* `PropertySet`
    * **`ID`** : `Int` *(AI)*
    * `PropertyType `: Foreign key -> `PropertyType.ID`
    * `Value` : `String`
* `PropertyValue`
    * **`ID`**: `Int` *(AI)*
    * `ProperyType` : Foreign key -> `PropertType.ID`
    * `Value` : `String`
* `ProductImage`
    * **`ID`**: `Int` *(AI)*
    * `Path` : `String`
    
## What's happening on edit
### Create product
Page:
* Show page with name and description field.
* Show fields for required properties.
* Show button to add non-required and/or new properties.
* Show button to create the product.
* **TODO:** How are we handling image uploads? Images should be linked to a property.

Logic:
* Create product model.
* Create entries in `PropertyValue` table for each configured property.

### Edit product
* Should be the same as the `Create product` page, but with filled-in fields.
