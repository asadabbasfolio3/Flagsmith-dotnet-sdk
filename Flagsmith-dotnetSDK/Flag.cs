using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace Flagsmith
{
    [JsonConverter(typeof(FlagJsonConverter))]
    public class Flag
    {
        public Flag() { }
        public Flag(string name, bool enabled, string value, int featureId = default)
        {
            this.Enabled = enabled;
            this.Value = value;
            this.Name = name;
            this.FeatureId = featureId;

        }
        [JsonProperty("id")]
        public int Id { get; private set; }

        [JsonProperty("feature.id")]
        public int FeatureId { get; private set; }

        [JsonProperty("feature.name")]
        public string Name { get; private set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; private set; }

        [JsonProperty("feature_state_value")]
        public string Value { get; private set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
    /// <summary>
    /// For derserializing flag response from api to plain flag object.
    /// </summary>
    internal class FlagJsonConverter : JsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            object targetObj = Activator.CreateInstance(objectType);//create instance of the type which use this attribute i.e  [JsonConverter(typeof(FlagJsonConverter))]
            foreach (PropertyInfo prop in objectType.GetProperties().Where(p => p.CanRead && p.CanWrite)) //using refeection retrieve all the properties from the type.
            {
                JsonPropertyAttribute att = prop.GetCustomAttributes(true)// from each property retrieve json property attribute i.e  [JsonProperty("id")]
                                                .OfType<JsonPropertyAttribute>()
                                                .FirstOrDefault();

                string jsonPath = (att != null ? att.PropertyName : prop.Name);//if there is json proprty attrbitue defined then the relative value used otherwise the original property name will be used.
                JToken token = jo.SelectToken(jsonPath); //SelectToken is a method on JToken and takes a string path to a child token. i.e feature.name
                // convert the token to object and set the proprty.
                if (token != null && token.Type != JTokenType.Null)
                {
                    object value = token.ToObject(prop.PropertyType, serializer);
                    prop.SetValue(targetObj, value, null);
                }
            }
            //return the fully mapped object.
            return targetObj;
        }

        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called when [JsonConverter] attribute is used
            return false;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
