using System;

namespace hhs_p6_webshop_project.App
{
    public sealed class PropertyDataType
    {
        /// <summary>
        /// Data type (as used by C#).
        /// </summary>
        public String dataType { get; }

        /// <summary>
        /// Display name of the type, for the front-end.
        /// </summary>
        public String displayName { get; }

        /// <summary>
        /// String type.
        /// </summary>
        public static readonly PropertyDataType STRING = new PropertyDataType("string", "Tekst");

        /// <summary>
        /// Integer type.
        /// </summary>
        public static readonly PropertyDataType INTEGER = new PropertyDataType("int", "Nummer");

        /// <summary>
        /// Decimal type.
        /// </summary>
        public static readonly PropertyDataType DECIMAL = new PropertyDataType("decimal", "Decimaal");

        /// <summary>
        /// Boolean type.
        /// </summary>
        public static readonly PropertyDataType BOOLEAN = new PropertyDataType("boolean", "Ja / Nee");

        /// <summary>
        /// List of possible values.
        /// </summary>
        private static readonly PropertyDataType[] _VALUES =
        {
            STRING,
            INTEGER,
            DECIMAL,
            BOOLEAN
        };

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dataType">Data type.</param>
        /// <param name="displayName">Display name.</param>
        public PropertyDataType(String dataType, String displayName)
        {
            this.dataType = dataType;
            this.displayName = displayName;
        }

        /// <summary>
        /// Get a property data type instance by it's data type string.
        /// </summary>
        /// <param name="dataType">String prepresentation of the data type.</param>
        /// <returns>Data type.</returns>
        /// <exception cref="Exception">Throws if the data type string is unsupported and/or invalid.</exception>
        public static PropertyDataType GetByDataType(string dataType)
        {
            // Make sure some data type is given
            if (dataType == null)
                throw new Exception("Unsupported property data type, data type is null.");

            // Trim the data type
            dataType = dataType.Trim();

            // Loop through the list of values, and return the value with the same data type
            foreach (var propertyDataType in _VALUES)
                if (propertyDataType.dataType.Equals(dataType))
                    return propertyDataType;

            // No data type found, throw an exception
            throw new Exception("Unknown property data type: '" + dataType + "'");
        }

        /// <summary>
        /// Return a list of possible property data type values.
        /// </summary>
        /// <returns>Property data type values.</returns>
        public static PropertyDataType[] GetValues()
        {
            return _VALUES;
        }

        /// <summary>
        /// Test the object for equality.
        /// </summary>
        /// <param name="other">Other object.</param>
        /// <returns>True if they equal, false if not.</returns>
        public override bool Equals(object other)
        {
            // Compare a raw string to the data type
            if (other is string)
                return this.dataType.Equals(other.ToString().Trim());

            // Compare the other object against it's data type
            return this.GetType() == other.GetType() && this.dataType.Equals(((PropertyDataType) other).dataType);
        }

        /// <summary>
        /// Get the string representation of this property data type.
        /// </summary>
        /// <returns>String representation.</returns>
        public override String ToString()
        {
            return this.displayName;
        }
    }
}