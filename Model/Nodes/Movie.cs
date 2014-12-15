using Newtonsoft.Json;

namespace NeoMovies.Model.Nodes
{
    class Movie
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("released")]
        public int Released { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }
    }
}
