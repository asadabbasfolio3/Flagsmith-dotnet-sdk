using Newtonsoft.Json;

namespace Flagsmith
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Feature
    {
        public Feature(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
        [JsonProperty]
        private int id;
        [JsonProperty]
        private string name = null;

        public string GetName()
        {
            return name;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
