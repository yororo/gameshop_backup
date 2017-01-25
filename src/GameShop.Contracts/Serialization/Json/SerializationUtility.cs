using GameShop.Contracts.Entities.Products;
using Newtonsoft.Json;

namespace GameShop.Contracts.Serialization.Json
{
    public static class SerializationUtility
    {
        /// <summary>
        /// Product converter.
        /// </summary>
        private static readonly ProductJsonConverter _productConverter = new ProductJsonConverter();

        /// <summary>
        /// Serialize object to JSON string.
        /// </summary>
        /// <param name="value">Object value to serialize.</param>
        /// <returns>JSON representation.</returns>
        public static string SerializeToJson(object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.Indented);
        }
        
        /// <summary>
        /// Deserialize JSON string to object of the specified type.
        /// </summary>
        /// <param name="json">JSON string to deserialize.</param>
        /// <param name="converters">JSON converters.</param>
        /// <returns>Instance of T.</returns>
        public static T DeserializeJson<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Deserialize Product JSON string to instance of the specified TProduct.
        /// </summary>
        /// <param name="json">JSON string to deserialize.</param>
        /// <returns>Instance of TProduct.</returns>
        public static Product DeserializeProductJson(string json)
        {
            return JsonConvert.DeserializeObject<Product>(json, _productConverter);
        }

        /// <summary>
        /// Deserialize Product JSON string to instance of the specified TProduct.
        /// </summary>
        /// <param name="json">JSON string to deserialize.</param>
        /// <returns>Instance of TProduct.</returns>
        public static TProduct DeserializeProductJson<TProduct>(string json) where TProduct : Product
        {
            return JsonConvert.DeserializeObject<TProduct>(json, _productConverter);
        }
    }
}