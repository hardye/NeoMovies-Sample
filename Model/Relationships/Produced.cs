using Neo4jClient;

namespace NeoMovies.Model.Relationships
{
    public class Produced : Relationship
    {
        public Produced()
            : base(null)
        {

        }
        public Produced(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public Produced(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get
            {
                return "PRODUCED";
            }
        }
    }
}
