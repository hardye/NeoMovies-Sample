using Newtonsoft.Json;

namespace NeoMovies.Model.Nodes
{
    class Person
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("born")]
        public int Born { get; set; }
    }
}
