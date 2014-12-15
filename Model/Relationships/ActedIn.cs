using Neo4jClient;
using Newtonsoft.Json;

namespace NeoMovies.Model.Relationships
{
    public class ActedIn : Relationship<ActedInProperties>
    {
        public ActedIn(NodeReference targetNode)
            : base(targetNode, null)
        {
        }

        public ActedIn(NodeReference targetNode, ActedInProperties data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "ACTED_IN"; }
        }
    }

    public class ActedInProperties
    {
        [JsonProperty("roles")]
        public string[] Roles { get; set; }
    }
}
