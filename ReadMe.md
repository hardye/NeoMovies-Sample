# NeoMovies Sample #

A sample application that demonstrates accessing [Neo4j](http://neo4j.com) using C#.

## Installing and running

1. [Download and install Neo4j](http://neo4j.com/download-thanks/?edition=community&flavour=windows)
2. Start the Neo4j service.
3. Open the Neo4j Web Interface at [http://localhost:7474](http://localhost:7474)
4. Run the **Movie Graph** sample and send its first query to the server. This will create the data model for our sample application.
5. Open the NeoMovies sample application. If necessary, open `Program.cs` and adjust server and port values in the factory method `GetClient` that creates a connection to the Neo4j server.
6. Uncomment the method(s) in `Main` that you are interested in and run the sample.

## Notes

Each method contains a comment with the [Cypher](http://neo4j.com/docs/stable/cypher-query-lang.html) query that should be sent to the server followed by its implementation in C#. Example:

	// MATCH (:Movie {title: "Cloud Atlas"})<-[:ACTED_IN]-(person) RETURN person

    var query = client.Cypher
        .Match("(:Movie {title: \"Cloud Atlas\"})<-[:ACTED_IN]-(person)")
        .Return(person => person.As<Person>())
        .OrderBy("person.born"); 

The samples uses the Neo4jClient C# driver at [https://github.com/Readify/Neo4jClient](https://github.com/Readify/Neo4jClient). Other drivers can be found at [http://neo4j.com/developer/dotnet/](http://neo4j.com/developer/dotnet/).

## License
MIT license ([http://opensource.org/licenses/MIT](http://opensource.org/licenses/MIT))