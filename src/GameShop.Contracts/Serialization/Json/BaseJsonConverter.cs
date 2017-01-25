using System;
using System.Reflection;
using GameShop.Contracts.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameShop.Contracts.Serialization.Json
{
    public abstract class BaseJsonConverter<T> : JsonConverter
    {
        public override bool CanRead => true;

        public override bool CanWrite => false;

        protected abstract T Create(Type type, JObject json);

        public override bool CanConvert(Type objectType)
        {
            return typeof(T).GetTypeInfo().IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);

            var target = Create(objectType, jsonObject);
            
            serializer.Populate(jsonObject.CreateReader(), target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}