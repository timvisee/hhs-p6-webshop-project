# Dressfinder

* Producten
* Cateogries








## Semantics
* If there is more than one value for any property. Allow the user to select one of the properties. Such as: Color and size.


## Default properties
* Price
* Category
* Brand

## Object / Database structure
* Product
    * **ID** : Int (AI)
        * Name : String
            * Description : String
                * Extra:
                        * Properties are linked using the PropertyValues table
                        * PropertyType
                            * **ID** : Int
                                * Name : String
                                    * Data type : String (C# compatible)
                                        * Required : Boolean
                                            * Mutliple : Boolean
                                                * AllowCustom : Boolean
                                                * PropertySet
                                                    * **ID** : Int (AI)
                                                        * Name : String
                                                            * Value : String
                                                            * PropertyValues
                                                                * **ID**
                                                                    * Product
                                                                        * ProperyType : Foreign key to PropertType.ID
                                                                            * Value : String
                                                                            * ProductImages
                                                                                * **ID**
                                                                                    * Image path : String


## Adding a property value to a product 

