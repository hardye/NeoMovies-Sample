using Neo4jClient;

namespace NeoMovies.Model.Relationships
{
    public class Wrote : Relationship
    {
        public Wrote()
            : base(null)
        {

        }
        public Wrote(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public Wrote(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get { return "WROTE"; }
        }
    }
}
