using Neo4jClient;
using Newtonsoft.Json;

namespace NeoMovies.Model.Relationships
{
    class Reviewed : Relationship<ReviewedProperties>
    {
        public Reviewed(NodeReference targetNode)
            : base(targetNode, null)
        {

        }
        public Reviewed(NodeReference targetNode, ReviewedProperties data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "REVIEWED"; }
        }
    }

    class ReviewedProperties
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("rating")]
        public int Rating { get; set; }
    }
}
