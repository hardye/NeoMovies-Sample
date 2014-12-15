using Neo4jClient;

namespace NeoMovies.Model.Relationships
{
    public class Directed : Relationship
    {
        public Directed()
            : base(null)
        {

        }
        public Directed(NodeReference targetNode)
            : base(targetNode)
        {
        }

        public Directed(NodeReference targetNode, object data)
            : base(targetNode, data)
        {
        }

        public override string RelationshipTypeKey
        {
            get
            {
                return "DIRECTED";
            }
        }
    }
}
